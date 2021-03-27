using System;
using SpaceInvaders.Game;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.View
{
    public class InvaderBehaviour: MonoBehaviour, IInvaderView
    {
        [Inject] private DiContainer _diContainer;

        private IGameNotifications _gameNotifications;
        public int Points;
        
        
        void Start()
        {
            _gameNotifications = _diContainer.Resolve<IGameNotifications>();
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
            return gameObject.activeSelf && gameObject.layer == 11;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            if (other.gameObject.tag == "Finish")
            {
                _gameNotifications.InvaderReachedEdge();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _gameNotifications.InvaderHitPlayer(this, other.gameObject.GetComponent<PlayerBehaviour>());
            }
        }
    }
}