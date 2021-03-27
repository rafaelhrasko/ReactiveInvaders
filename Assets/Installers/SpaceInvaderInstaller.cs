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
            BindGameStructure();
            BindGameRules();
            BindViewProviders();
        }

        private void BindGameStructure()
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
                .Bind<ILevelProvider>()
                .To<MockedLevelProvider>()
                .AsTransient();
        }

        private void BindGameRules()
        {
            Container
                .Bind<ILevelBehaviour>()
                .To<ClassicLevelBehaviour>()
                .AsTransient();

            Container
                .Bind<IGenerateMissile>()
                .To<GenerateMissile>()
                .AsTransient();

            Container
                .Bind<ILevelSetup>()
                .To<LevelSetup>()
                .AsTransient();

            Container
                .Bind<IAddScore>()
                .To<AddScore>()
                .AsTransient();
            
            Container
                .Bind<IExplosionDispatcher>()
                .To<ExplosionDispatcher>()
                .AsTransient();
        }

        private void BindViewProviders()
        {
            Container
                .Bind<IPrefabsFactory>()
                .FromInstance(_prefabsFactory);
            
            Container
                .Bind<IViewProvider<IMissileView>>()
                .To<ViewProvider<IMissileView, MissileBehaviour>>()
                .AsSingle();

            Container
                .Bind<IViewProvider<IInvaderView>>()
                .To<ViewProvider<IInvaderView, InvaderBehaviour>>()
                .AsSingle();

            Container
                .Bind<IViewProvider<IExplosionView>>()
                .To<ViewProvider<IExplosionView, ExplosionView>>()
                .AsSingle();
        }
    }
}