using System;
using SpaceInvaders.Game;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.View
{
    public class MissileBehaviour: MonoBehaviour, IMissileView
    {
        private Rigidbody2D _cachedRigidbody2D;

        [Inject] private DiContainer _diContainer;
        private IGameNotifications _gameNotifications;
        private IViewProvider<IMissileView> _viewProvider;

        private void Start()
        {
            _gameNotifications = _diContainer.Resolve<IGameNotifications>();
            _viewProvider = _diContainer.Resolve<IViewProvider<IMissileView>>();
        }
        
        private void OnEnable()
        {
            if (_cachedRigidbody2D == null)
            {
                _cachedRigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            if (other.gameObject.tag == "Finish")
            {
                _viewProvider.Return(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var tag = other.gameObject.tag;
            if (tag == "Invader")
            {
                var invader = other.gameObject.GetComponent<InvaderBehaviour>();
                _gameNotifications.MissileHitInvader(this, invader);
            }
            if (tag == "Player")
            {
                var player = other.gameObject.GetComponent<PlayerView>();
                _gameNotifications.MissileHitPlayer(this, player);
            } }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }

        public void SetInitialPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetVelocity(Vector3 velocity)
        {
            _cachedRigidbody2D.velocity = velocity;
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

        public void AnimateDestroy()
        {
            
        }
    }
}