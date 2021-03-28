using System;
using System.Linq;
using SpaceInvaders.Configuration;
using SpaceInvaders.Ui;
using UniRx;

namespace SpaceInvaders.Game
{
    public class GameFlow : IGameFlow
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly IGameNotifications _gameNotifications;
        private readonly IGameModeBehaviour _gameModeBehaviour;
        private readonly IGameModeConfigurationProvider _gameModeConfigurationProvider;
        private readonly IInputController _inputController;
        private readonly IUiViewProvider _uiViewProvider;
        private readonly ILevelSetup _levelSetup;
        private readonly ILevelBehaviour _levelBehaviour;
        private readonly IPlayerBehaviour _playerBehaviour;
        private readonly IAddScore _addScore;

        private ILetterboardView _letterboardView;
        private IChangeSceneView _changeSceneView;
        
        public GameFlow(
            IGameStateProvider gameStateProvider,
            IGameNotifications gameNotifications,
            IGameModeBehaviour gameModeBehaviour,
            IGameModeConfigurationProvider gameModeConfigurationProvider,
            IInputController inputController,
            IUiViewProvider uiViewProvider,
            ILevelSetup levelSetup,
            ILevelBehaviour levelBehaviour,
            IPlayerBehaviour playerBehaviour,
            IAddScore addScore)
        {
            _gameStateProvider = gameStateProvider;
            _gameNotifications = gameNotifications;
            _gameModeBehaviour = gameModeBehaviour;
            _gameModeConfigurationProvider = gameModeConfigurationProvider;
            _inputController = inputController;
            _uiViewProvider = uiViewProvider;
            _levelSetup = levelSetup;
            _levelBehaviour = levelBehaviour;
            _playerBehaviour = playerBehaviour;
            _addScore = addScore;
        }

        public IObservable<Unit> Execute()
        {
            return InitializeGame()
                .ContinueWith(
                    _inputController.OnPlayerStart().First().ContinueWith(ExecuteRound())
                    .Repeat()
                    .TakeWhile(_ => _gameStateProvider.Current.PlayerLives > 0))
                .DoOnCompleted(() => _letterboardView.ShowText("GAME OVER"))
                .DoOnCompleted(() => _changeSceneView.Show())
                .DoOnCompleted(() => _inputController.Disable())
                .Catch<Unit, Exception>(LogException)
                .DoOnError(e => UnityEngine.Debug.LogError(e.ToString()));
        }

        private void HideLeaderBoard()
        {
            _letterboardView.Hide();
        }
        
        private IObservable<Unit> LogException(Exception e)
        {
            UnityEngine.Debug.LogError(e.ToString());
            return Observable.Defer(() =>
            {
                UnityEngine.Debug.LogError(e.ToString());
                return Observable.ReturnUnit();
            });
        }
        
        private IObservable<Unit> WaitForPlayerDeath()
        {
            return Observable.FromEvent(
                handler => _gameNotifications.PlayerDeath+= handler,
                handler => _gameNotifications.PlayerDeath-= handler
                )
                .First()
                .Do(_ => _gameStateProvider.Current.PlayerLives -= 1)
                .Do(_ => _letterboardView.ShowText("Remaining lives: "+_gameStateProvider.Current.PlayerLives.ToString()));
        }

        private IObservable<Unit> WaitAllInvadersAreDead()
        {
            return _gameModeBehaviour.Execute()
                .First()
                .Do(_ => _gameStateProvider.Current.CurrentLevel += 1)
                .Do(_ => AddLevelCompletedScore())
                .Do(_ => _letterboardView.ShowText("Level Completed!"))
                .Do(_ => _gameNotifications.RoundEnd())
                .AsUnitObservable();
        }

        private void AddLevelCompletedScore()
        {
            var pointsPerLevel = _gameModeConfigurationProvider.Get().PointsPerLevel;
            _addScore.Add(_gameStateProvider.Current.CurrentLevel*pointsPerLevel);
        }

        private IObservable<Unit> ExecuteRound()
        {
            return Observable.Merge(
                WaitAllInvadersAreDead(),
                _playerBehaviour.Execute().IgnoreElements(),
                Observable.Defer(() =>
                {
                    HideLeaderBoard();
                    _levelSetup.Setup();
                    _levelBehaviour.Initialize();
                    _gameNotifications.RoundStart();
                    return WaitForPlayerDeath();
                }))
                .First()
                .Do(_ => _levelBehaviour.Disable())
                .Delay(TimeSpan.FromSeconds(1.5f))
                .Do(_ => _letterboardView.ShowText("Touch to Continue."));
        }
        
        
        private IObservable<Unit> InitializeGame()
        {
            return Observable.ReturnUnit()
                .Do(_ => _letterboardView = _uiViewProvider.Provide<ILetterboardView>())
                .Do(_ => _changeSceneView = _uiViewProvider.Provide<IChangeSceneView>())
                .Do(_ => _letterboardView.ShowText("Touch to Start"))
                .Do(_ => _changeSceneView.Hide())
                .Do(_ => _addScore.Add(0))
                .Do(_ => _gameStateProvider.Current.PlayerLives = _gameModeConfigurationProvider.Get().PlayerLives);
        }
    }
}