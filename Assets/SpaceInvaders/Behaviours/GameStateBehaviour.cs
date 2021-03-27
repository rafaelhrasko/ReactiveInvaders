using UnityEngine;
using Zenject;

namespace SpaceInvaders.Game
{
    public class GameStateBehaviour: MonoBehaviour
    {
        public enum GameState
        {
            Idle,
            WalkingLeft,
            WalkingDown,
            WalkingRight
        }
        
        [Inject] private IGameNotifications _gameNotifications;
        
        [SerializeField] private float _secondsBetweenTicks = 1.5f;
        [SerializeField] private Vector3 _down;
        [SerializeField] private Vector3 _left;
        [SerializeField] private Vector3 _right;
        
        private GameState _currentGameState;
        private float _delayedProcessTime;
        
        void Start()
        {
            _gameNotifications.InvaderReachedEdge += InvaderReachedEdge;
            _gameNotifications.PlayerDeath += PlayerDeath;
            _gameNotifications.RoundStart += RoundStart;
        }

        private void RoundStart()
        {
            _currentGameState = GameState.WalkingLeft;
        }

        private void PlayerDeath()
        {
            _currentGameState = GameState.Idle;
        }

        void OnDestroy()
        {
            _gameNotifications.InvaderReachedEdge -= InvaderReachedEdge;
            _gameNotifications.PlayerDeath -= PlayerDeath;
            _gameNotifications.RoundStart -= RoundStart;
        }
        
        private void InvaderReachedEdge()
        {
            _currentGameState = GameState.WalkingDown;
        }

        public void Update()
        {
            var time = Time.time;
            if (_delayedProcessTime > time)
            {
                return;
            }

            _delayedProcessTime = time + _secondsBetweenTicks;
            ProcessState();
            if (_gameNotifications.InvaderMovementTick != null)
            {
                _gameNotifications.InvaderMovementTick();
            }
        }

        private void ProcessState()
        {
            switch (_currentGameState)
            {
                case GameState.WalkingDown: 
                    transform.position += _down;
                    ChooseNextState();
                    SpeedUp();
                    break;
                case GameState.WalkingLeft: transform.position += _left; break;
                case GameState.WalkingRight: transform.position += _right; break;
            }
        }
        
        private void SpeedUp()
        {
            _secondsBetweenTicks = _secondsBetweenTicks * 0.9f;
        }

        private void ChooseNextState()
        {
            if (transform.position.x > 0)
            {
                _currentGameState = GameState.WalkingLeft;
            }
            else
            {
                _currentGameState = GameState.WalkingRight;
            }
        }
        
    }
}