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
        }
        
        private void OnDisable()
        {
            _inputController.OnPlayerFired -= OnPlayerFired;
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