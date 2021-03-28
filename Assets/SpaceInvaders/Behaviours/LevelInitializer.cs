using SpaceInvaders.View;
using UnityEngine;
using Zenject;
using UniRx;

namespace SpaceInvaders.Game
{
    public class LevelInitializer: MonoBehaviour
    {
        [Inject] private IViewProvider<IMissileView> _missileViewProvider;
        [Inject] private IViewProvider<IInvaderView> _invaderViewProvider;
        [Inject] private IViewProvider<IExplosionView> _explosionViewProvider;
        [Inject] private IViewProvider<IPlayerView> _playerViewProvider;
        [Inject] private IGameFlow _gameFlow;
        [Inject] private IGameStateProvider _gameStateProvider;

        void Start()
        {
            var plaeyr = _playerViewProvider.Initialize(1);
            var missiles = _missileViewProvider.Initialize(20);
            var explosions = _explosionViewProvider.Initialize(10);
            var invaderViews = _invaderViewProvider.Initialize(30);
            for (int i = 0; i < invaderViews.Length; i++)
            {
                var invaderView = invaderViews[i] as InvaderBehaviour;
                invaderView.transform.parent = transform;
            }

            _gameStateProvider.Current.InvaderViews = invaderViews;
            
            _gameFlow
                .Execute()
                .Subscribe();
        }
    }
}