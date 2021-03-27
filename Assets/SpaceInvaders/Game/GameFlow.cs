using System;
using SpaceInvaders.Ui;
using UniRx;

namespace SpaceInvaders.Game
{
    public class GameFlow : IGameFlow
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly IGameNotifications _gameNotifications;
        private readonly IInputController _inputController;
        private readonly IUiViewProvider _uiViewProvider;
        private readonly ILevelSetup _levelSetup;
        private readonly ILevelBehaviour _levelBehaviour;

        private ILetterboardView _letterboardView;
        
        public GameFlow(
            IGameStateProvider gameStateProvider,
            IGameNotifications gameNotifications,
            IInputController inputController,
            IUiViewProvider uiViewProvider,
            ILevelSetup levelSetup,
            ILevelBehaviour levelBehaviour)
        {
            _gameStateProvider = gameStateProvider;
            _gameNotifications = gameNotifications;
            _inputController = inputController;
            _uiViewProvider = uiViewProvider;
            _levelSetup = levelSetup;
            _levelBehaviour = levelBehaviour;
        }

        public IObservable<Unit> Execute()
        {
            return WaitForPlayerInput()
                .ContinueWith(InitializeLevel())
                .Do(_ => HideLeaderBoard())
                .ContinueWith(WaitForPlayerDeath())
                .Catch<Unit,Exception>(LogException)
                .DoOnError( e => UnityEngine.Debug.LogError(e.ToString()));
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
                .Do(_ => _gameStateProvider.Current.PlayerLives -= 1)
                .Do(_ => _levelSetup.Setup());
        }
        
        private IObservable<Unit> InitializeLevel()
        {
            return Observable.Defer(() =>
            {
                _levelSetup.Setup();
                _levelBehaviour.Initialize();
                _gameNotifications.RoundStart();
                return Observable.ReturnUnit();
            });
        }

        private IObservable<Unit> WaitForPlayerInput()
        {
            return Observable.ReturnUnit()
                .Do(_ => _letterboardView = _uiViewProvider.Provide<ILetterboardView>())
                .Do(_ => _letterboardView.ShowText("Touch to Start"))
                .Do(_ => _gameStateProvider.Current.PlayerLives = 3)
                .ContinueWith(_inputController.OnPlayerFired())
                .First();
        }
    }
}