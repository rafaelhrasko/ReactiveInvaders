using SpaceInvaders.Game;
using SpaceInvaders.View;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SpaceInvaderInstaller: MonoInstaller
    {
        [SerializeField] private PrefabsFactory _prefabsFactory;
        public override void InstallBindings()
        {
            Container
                .Bind<IGameStateProvider>()
                .To<GameStateProvider>()
                .AsSingle();
            
            Container
                .Bind<IGameNotifications>()
                .To<GameNotifications>()
                .AsSingle();
            
            Container
                .Bind<IGenerateMissile>()
                .To<GenerateMissile>()
                .AsTransient();
            
            Container
                .Bind<ILevelProvider>()
                .To<MockedLevelProvider>()
                .AsTransient();
            
            Container
                .Bind<ILevelSetup>()
                .To<LevelSetup>()
                .AsTransient();

            Container
                .Bind<IPrefabsFactory>()
                .FromInstance(_prefabsFactory);

            Container
                .Bind<IViewProvider<IMissileView>>()
                .To<ViewProvider<IMissileView,MissileBehaviour>>()
                .AsSingle();

            Container
                .Bind<IViewProvider<IInvaderView>>()
                .To<ViewProvider<IInvaderView,InvaderBehaviour>>()
                .AsSingle();
        }
    }
}