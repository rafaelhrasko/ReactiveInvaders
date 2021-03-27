using SpaceInvaders.Game;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.View
{
    public class PlayerBehaviour: MonoBehaviour, IPlayerView
    {
        [Inject] private IInputController _inputController;
        [Inject] private IGenerateMissile _generateMissile;

        [SerializeField] private Transform _firingTransform;

        [SerializeField] private float _firingSpeed = 3;
        [SerializeField] private float _cooldownSeconds = 1;

        private float _nextFiring;
        private CompositeDisposable _disables = new CompositeDisposable();
        private void OnEnable()
        {
            _disables.Add(
            _inputController
                .OnPlayerFired()
                .Do(_ => OnPlayerFired())
                .Subscribe());
            _disables.Add(_inputController
                .OnPlayerMovedLeft()
                .Do(_ => OnPlayerMovedLeft())
                .Subscribe());
            _disables.Add(_inputController
                .OnPlayerMovedRight()
                .Do(_ => OnPlayerMovedRight())
                .Subscribe());
        }
        
        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }

        public void SetInitialPosition(Vector3 position)
        {
            transform.position = position;
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void OnPlayerMovedLeft()
        {
            transform.position += Vector3.left;
        }
        
        private void OnPlayerMovedRight()
        {
            transform.position += Vector3.right;
        }

        private void OnDisable()
        {
            _disables.Dispose();
        }

        private void OnPlayerFired()
        {
            var currentTime = Time.time;
            if (_nextFiring > currentTime)
            {
                return;
            }
            _generateMissile.Generate(_firingTransform.position, _firingTransform.up*_firingSpeed);
            _nextFiring = currentTime + _cooldownSeconds;
        }
    }
}