using SpaceInvaders.Game;
using Zenject;

namespace Installers
{
    public class ClassicGameInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameModeBehaviour>()
                .To<ClassicGameModeBehaviour>()
                .AsTransient();
        }
    }
}