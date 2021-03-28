using System;
using SpaceInvaders.Configuration;
using SpaceInvaders.Game;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.View
{
    public class InvaderBehaviour: MonoBehaviour, IInvaderView
    {
        [Inject] private DiContainer _diContainer;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private InvaderConfiguration _invaderConfiguration;
        
        private IGameNotifications _gameNotifications;

        public int Points
        {
            get
            {
                return _invaderConfiguration.Points;
            }
        }

        private int _swapCount = 0;
        
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

        public void Setup(InvaderConfiguration invaderConfiguration)
        {
            _invaderConfiguration = invaderConfiguration;
            SwapSprite();
        }

        public void SwapSprite()
        {
            var spriteIndex = _swapCount%_invaderConfiguration.Sprites.Length;
            _swapCount++;
            _spriteRenderer.sprite = _invaderConfiguration.Sprites[spriteIndex];
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
                _gameNotifications.InvaderHitPlayer(this, other.gameObject.GetComponent<PlayerView>());
            }
        }
    }
}