using SpaceInvaders.Game;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.View
{
    public class MissileBehaviour: MonoBehaviour, IMissileView
    {
        [SerializeField] private Rigidbody2D _cachedRigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Inject] private DiContainer _diContainer;

        private IGameNotifications _gameNotifications;
        private IViewProvider<IMissileView> _viewProvider;

        private void Start()
        {
            _gameNotifications = _diContainer.Resolve<IGameNotifications>();
            _viewProvider = _diContainer.Resolve<IViewProvider<IMissileView>>();
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
                if (_gameNotifications.MissileHitInvader != null)
                {
                    var invader = other.gameObject.GetComponent<InvaderBehaviour>();
                    _gameNotifications.MissileHitInvader(this, invader);
                }
            }
            if (tag == "Player")
            {
                if (_gameNotifications.MissileHitPlayer != null)
                {
                    var player = other.gameObject.GetComponent<PlayerView>();
                    _gameNotifications.MissileHitPlayer(this, player);
                }
            }
            if (tag == "Barrier")
            {
                other.gameObject.SetActive(false);
                _viewProvider.Return(this);
            }
            if (tag == "Missile")
            {
                if (gameObject.activeInHierarchy
                    && _gameNotifications.MissileHitMissile != null)
                {
                    var missile = other.gameObject.GetComponent<MissileBehaviour>();
                    _gameNotifications.MissileHitMissile(this, missile);
                }
                _viewProvider.Return(this);
            }
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }

        public void SetInitialPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetType(MissileType type)
        {
            switch (type)
            {
                case MissileType.Player:
                    gameObject.layer = LayerMask.NameToLayer("PlayerMissile");
                    _spriteRenderer.color = Color.blue;
                    break;
                case MissileType.Invader:
                    gameObject.layer = LayerMask.NameToLayer("InvaderMissile");
                    _spriteRenderer.color = Color.red;
                    break;
            }
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
            transform.rotation = Quaternion.identity;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}