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

        [SerializeField] private Sprite _sprite;

        private InvaderConfiguration _invaderConfiguration;
        
        private IGameNotifications _gameNotifications;
        private IInvaderConfigurationProvider _invaderConfigurationProvider;

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
            _invaderConfigurationProvider = _diContainer.Resolve<IInvaderConfigurationProvider>();
            _gameNotifications.InvaderMovementTick += InvaderMovementTick;
        }
        
        private void OnDestroy()
        {
            _gameNotifications.InvaderMovementTick -= InvaderMovementTick;
        }

        private void InvaderMovementTick()
        {
            SwapSprite();
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

        public void Setup(InitialLevelSlot slot)
        {
            _invaderConfiguration = _invaderConfigurationProvider.Get(slot);
            SwapSprite();
        }

        public void SwapSprite()
        {
            var spriteIndex = _swapCount%_invaderConfiguration.Sprites.Length;
            _swapCount++;
            _sprite = _invaderConfiguration.Sprites[spriteIndex];
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