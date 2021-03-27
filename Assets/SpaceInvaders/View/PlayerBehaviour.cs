using SpaceInvaders.Game;
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
        
        private void OnEnable()
        {
            _inputController.OnPlayerFired += OnPlayerFired;
            _inputController.OnPlayerMovedLeft += OnPlayerMovedLeft;
            _inputController.OnPlayerMovedRight += OnPlayerMovedRight;
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
            _inputController.OnPlayerFired -= OnPlayerFired;
            _inputController.OnPlayerMovedLeft -= OnPlayerMovedLeft;
            _inputController.OnPlayerMovedRight -= OnPlayerMovedRight;
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