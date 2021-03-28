using SpaceInvaders.Game;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.View
{
    public class PlayerView: MonoBehaviour, IPlayerView
    {
        [Inject] private IGenerateMissile _generateMissile;

        [SerializeField] private Transform _firingTransform;

        [SerializeField] private float _firingSpeed = 3;
        [SerializeField] private float _cooldownSeconds = 1;
        
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

        public void MoveLeft()
        {
            transform.position += Vector3.left;
        }
        
        public void MoveRight()
        {
            transform.position += Vector3.right;
        }

        public void FireMissile(float firingSpeed)
        {
            _generateMissile.Generate(_firingTransform.position, _firingTransform.up*firingSpeed);
            /*var currentTime = Time.time;
            if (_nextFiring > currentTime)
            {
                return;
            }
            
            _nextFiring = currentTime + _cooldownSeconds;*/
        }
    }
}