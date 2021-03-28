using UnityEngine;
using Zenject;

namespace SpaceInvaders.View
{
    public class ExplosionView: MonoBehaviour, IExplosionView
    {
        [Inject] private IViewProvider<IExplosionView> _explosionViewProvider;
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

        //Called by animation. Ideally animations should not be responsible for this
        //but since Unity does not provide a reliable way to known when an animation ends,
        //We have to leave it like this for now.
        public void Return()
        {
            _explosionViewProvider.Return(this);
        }
    }
}