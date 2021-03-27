using UnityEngine;

namespace SpaceInvaders.View
{
    public class ExplosionView: MonoBehaviour, IExplosionView
    {
        [SerializeField] private Animator _animator;
        private static readonly int ExplosionType = Animator.StringToHash("ExplosionType");

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

        public void SetExplosion(int explosionSize)
        {
            _animator.SetInteger(ExplosionType, explosionSize);
        }
    }
}