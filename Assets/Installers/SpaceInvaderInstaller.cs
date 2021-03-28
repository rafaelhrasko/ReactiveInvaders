using SpaceInvaders.Configuration;
using SpaceInvaders.Game;
using SpaceInvaders.Ui;
using SpaceInvaders.View;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SpaceInvaderInstaller: MonoInstaller
    {
        [SerializeField] private PrefabsFactory _prefabsFactory;
        [SerializeField] private LevelProvider _levelProvider;
        [SerializeField] private InvadersConfiguration _invadersConfiguration;
        public override void InstallBindings()
        {
            BindGameConfigurations();
            BindGameStructure();
            BindGameRules();
            BindViewProviders();
        }

        private void BindGameConfigurations()
        {
            Container
                .Bind<IPrefabsFactory>()
                .FromInstance(_prefabsFactory);
            Container
                .Bind<ILevelProvider>()
                .FromInstance(_levelProvider);
            Container
                .Bind<IInvaderConfigurationProvider>()
                .FromInstance(_invadersConfiguration);
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
        }

        private void BindGameRules()
        {
            Container
                .Bind<IGameFlow>()
                .To<GameFlow>()
                .AsTransient();
            
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
                .Bind<IViewProvider<IPlayerView>>()
                .To<ViewProvider<IPlayerView, PlayerView>>()
                .AsSingle();

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
            
            Container
                .Bind<IUiViewProvider>()
                .To<UiViewProvider>()
                .AsSingle();
        }
    }
}