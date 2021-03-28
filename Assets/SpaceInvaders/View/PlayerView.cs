using SpaceInvaders.Game;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.View
{
    public class PlayerView: MonoBehaviour, IPlayerView
    {
        [Inject] private IGenerateMissile _generateMissile;

        [SerializeField] private Transform _firingTransform;

        public Transform GetFiringTransform()
        {
            return _firingTransform;
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
            transform.rotation = Quaternion.identity;
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
    }
}