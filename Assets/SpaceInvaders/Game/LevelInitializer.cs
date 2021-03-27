using System.Collections;
using SpaceInvaders.View;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.Game
{
    public class LevelInitializer: MonoBehaviour
    {
        [Inject] private IViewProvider<IMissileView> _missileViewProvider;
        [Inject] private IViewProvider<IInvaderView> _invaderViewProvider;
        [Inject] private IViewProvider<IExplosionView> _explosionViewProvider;
        [Inject] private ILevelSetup _levelSetup;
        [Inject] private ILevelBehaviour _levelBehaviour;
        [Inject] private IGameNotifications _gameNotifications;

        [SerializeField] private float _invaderSlotXDistance;
        [SerializeField] private float _invaderSlotYDistance;
        [SerializeField] private float _initialYPosition;

        IEnumerator Start()
        {
            var missiles = _missileViewProvider.Initialize(20);
            var explosions = _explosionViewProvider.Initialize(10);
            var invaderViews = _invaderViewProvider.Initialize(10);
            for (int i = 0; i < invaderViews.Length; i++)
            {
                var invaderView = invaderViews[i] as InvaderBehaviour;
                invaderView.transform.parent = transform;
            }
            _levelSetup.Setup(_invaderSlotXDistance, _invaderSlotYDistance, _initialYPosition);
            _levelBehaviour.Initialize();
            yield return new WaitForSeconds(1);
            _gameNotifications.RoundStart();
        }

        void OnDestroy()
        {
            _levelBehaviour.Disable();
        }
    }
}