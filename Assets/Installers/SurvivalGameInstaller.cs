using SpaceInvaders.Game;
using Zenject;

namespace Installers
{
    public class SurvivalGameInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameModeBehaviour>()
                .To<SurvivalGameModeBehaviour>()
                .AsTransient();
        }
    }
}