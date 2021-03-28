using SpaceInvaders.View;
using UniRx;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public class PlayerBehaviour: IPlayerBehaviour
    {
        private readonly IViewProvider<IPlayerView> _viewProvider;
        private readonly IInputController _inputController;
        private readonly IGameNotifications _gameNotifications;
        private readonly IGenerateMissile _generateMissile;

        private IPlayerView _playerView;

        private const int firingSpeed = 300;
        private const float cooldownSeconds = 0.5f;

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
            _playerView = _viewProvider.Get();
            _playerView.SetInitialPosition(new Vector3(0,-400,0));
            _inputController
                .OnPlayerFired()
                .Where(_ => _lastTimeFired < Time.time)
                .Do(_ => FireMissile())
                .Do(_ => _lastTimeFired = Time.time + cooldownSeconds)
                .Subscribe();
            _inputController
                .OnPlayerMovedLeft()
                .Do(_ => _playerView.MoveLeft())
                .Subscribe();
            _inputController
                .OnPlayerMovedRight()
                .Do(_ => _playerView.MoveRight())
                .Subscribe();
        }

        private void FireMissile()
        {
            var firingTransform = _playerView.GetFiringTransform();
            _generateMissile.Generate(firingTransform.position, firingTransform.up*firingSpeed);
        }
    }
}