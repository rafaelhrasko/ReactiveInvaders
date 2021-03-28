using System;
using SpaceInvaders.View;
using UniRx;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public class PlayerBehaviour: IPlayerBehaviour
    {
        private const int firingSpeed = 300;
        private const float cooldownSeconds = 0.5f;
        
        private readonly IViewProvider<IPlayerView> _viewProvider;
        private readonly IInputController _inputController;
        private readonly IGameNotifications _gameNotifications;
        private readonly IGenerateMissile _generateMissile;

        private IPlayerView _playerView;
        private float _lastTimeFired;
        
        public PlayerBehaviour(IViewProvider<IPlayerView> viewProvider,
            IInputController inputController,
            IGameNotifications gameNotifications,
            IGenerateMissile generateMissile)
        {
            _viewProvider = viewProvider;
            _inputController = inputController;
            _gameNotifications = gameNotifications;
            _generateMissile = generateMissile;
        }

        public void Initialize()
        {
            Execute().Subscribe();
        }

        private void InitializeView()
        {
            _playerView = _viewProvider.Get();
            _playerView.SetInitialPosition(new Vector3(0, -400, 0));
        }

        private IObservable<Unit> WaitForRoundStart()
        {
            return Observable.FromEvent(handler => _gameNotifications.RoundStart += handler,
                handler => _gameNotifications.RoundStart -= handler)
                .First()
                .Do(_ => InitializeView());
        }
        
        private IObservable<Unit> WaitForRoundEnd()
        {
            return Observable.FromEvent(handler => _gameNotifications.PlayerDeath += handler,
                handler => _gameNotifications.PlayerDeath -= handler)
                .Do(_ => _viewProvider.Return(_playerView))
                .First();
        }

        public IObservable<Unit> Execute()
        {
            return WaitForRoundStart()
                .ContinueWith(Observable.Merge(
                    _inputController.OnPlayerFired()
                        .Where(_ => _lastTimeFired < Time.time)
                        .Do(_ => FireMissile())
                        .Do(_ => _lastTimeFired = Time.time + cooldownSeconds),
                    _inputController.OnPlayerMovedLeft().Do(_ => _playerView.MoveLeft()),
                    _inputController.OnPlayerMovedRight().Do(_ => _playerView.MoveRight())
                ))
                .TakeUntil(WaitForRoundEnd());
        }

        private void FireMissile()
        {
            var firingTransform = _playerView.GetFiringTransform();
            _generateMissile.Generate(firingTransform.position, firingTransform.up*firingSpeed);
        }
    }
}