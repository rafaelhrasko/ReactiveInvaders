using System;
using System.Linq;
using SpaceInvaders.Ui;
using SpaceInvaders.View;
using UniRx;
using UnityEngine;

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
        private readonly IPlayerBehaviour _playerBehaviour;

        private ILetterboardView _letterboardView;
        
        public GameFlow(
            IGameStateProvider gameStateProvider,
            IGameNotifications gameNotifications,
            IInputController inputController,
            IUiViewProvider uiViewProvider,
            ILevelSetup levelSetup,
            ILevelBehaviour levelBehaviour,
            IPlayerBehaviour playerBehaviour)
        {
            _gameStateProvider = gameStateProvider;
            _gameNotifications = gameNotifications;
            _inputController = inputController;
            _uiViewProvider = uiViewProvider;
            _levelSetup = levelSetup;
            _levelBehaviour = levelBehaviour;
            _playerBehaviour = playerBehaviour;
        }

        public IObservable<Unit> Execute()
        {
            return InitializeGame()
                .ContinueWith(
                    _inputController.OnPlayerFired().First().ContinueWith(ExecuteRound())
                    .Repeat()
                    .TakeWhile(_ => _gameStateProvider.Current.PlayerLives > 0))
                .DoOnCompleted(() => _letterboardView.ShowText("GAME OVER"))
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
                .Do(_ => _letterboardView.ShowText("Touch to Start. Remaining lives: "+_gameStateProvider.Current.PlayerLives.ToString()));
        }

        private IObservable<Unit> WaitAllInvadersAreDead()
        {
            return Observable.FromEvent(
                    handler => _gameNotifications.InvaderDeath+= handler,
                    handler => _gameNotifications.InvaderDeath-= handler
                )
                .Select(_ => _gameStateProvider.Current.InvaderViews)
                .Where(invadersViews => invadersViews.All(view => !view.IsActive()))
                .First()
                .Do(_ => _gameStateProvider.Current.CurrentLevel += 1)
                .Do(_ => _letterboardView.ShowText("Level Completed! Touch to Start."))
                .Do(_ => _gameNotifications.RoundEnd())
                .AsUnitObservable();
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
                .Do(_ => _levelBehaviour.Disable());
        }
        
        
        private IObservable<Unit> InitializeGame()
        {
            return Observable.ReturnUnit()
                .Do(_ => _letterboardView = _uiViewProvider.Provide<ILetterboardView>())
                .Do(_ => _letterboardView.ShowText("Touch to Start"))
                .Do(_ => _gameStateProvider.Current.PlayerLives = 3);
        }
    }
}