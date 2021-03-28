using SpaceInvaders.Game;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TouchInputInstaller: MonoInstaller<TouchInputInstaller>
    {
        [SerializeField] private TouchInputController _prefab;
        
        public override void InstallBindings()
        {
            var instance = Instantiate(_prefab);

            Container.Bind<IInputController>()
                .FromInstance(instance)
                .AsSingle();
        }
    }
}