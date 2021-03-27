using SpaceInvaders.View;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.Game
{
    public class LevelBehaviour: MonoBehaviour
    {
        public enum State
        {
            WalkingLeft,
            WalkingDown,
            WalkingRight
        }
        
        [Inject] private IViewProvider<IMissileView> _missileViewProvider;
        [Inject] private IViewProvider<IInvaderView> _invaderViewProvider;
        [Inject] private ILevelSetup _levelSetup;
        [Inject] private IGameStateProvider _gameStateProvider;
        [Inject] private IGameNotifications _gameNotifications;

        [SerializeField] private float _invaderSlotXDistance;
        [SerializeField] private float _invaderSlotYDistance;
        [SerializeField] private float _initialYPosition;
        [SerializeField] private float _secondsBetweenTicks = 1.5f;
        [SerializeField] private Vector3 _down;
        [SerializeField] private Vector3 _left;
        [SerializeField] private Vector3 _right;
        [SerializeField] private State _currentState;

        private State _previousState;
        private float _delayedProcessTime;
        
        void Start()
        {
            var missiles = _missileViewProvider.Initialize(20);
            var invaderViews = _invaderViewProvider.Initialize(10);
            for (int i = 0; i < invaderViews.Length; i++)
            {
                var invaderView = invaderViews[i] as InvaderBehaviour;
                invaderView.transform.parent = transform;
            }
            _levelSetup.Setup(_invaderSlotXDistance, _invaderSlotYDistance, _initialYPosition);
            _gameNotifications.MissileHitInvader += MissileHitInvader;
            _gameNotifications.InvaderReachedEdge += InvaderReachedEdge;
            _gameNotifications.MissileHitInvader += MissileHitInvader;
        }

        void OnDestroy()
        {
            _gameNotifications.MissileHitInvader -= MissileHitInvader;
            _gameNotifications.InvaderReachedEdge -= InvaderReachedEdge;
        }

        private void InvaderReachedEdge()
        {
            _currentState = State.WalkingDown;
        }

        private void MissileHitInvader(IMissileView missile, InvaderBehaviour invader)
        {
            _gameStateProvider.Current.AddScore(invader.Points);
            _missileViewProvider.Return(missile);
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
        }

        private void ProcessState()
        {
            switch (_currentState)
            {
                case State.WalkingDown: 
                    transform.position += _down;
                    ChooseNextState();
                    SpeedUp();
                    break;
                case State.WalkingLeft: transform.position += _left; break;
                case State.WalkingRight: transform.position += _right; break;
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
                _currentState = State.WalkingLeft;
            }
            else
            {
                _currentState = State.WalkingRight;
            }
        }
    }
}