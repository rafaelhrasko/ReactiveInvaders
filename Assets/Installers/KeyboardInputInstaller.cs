using SpaceInvaders.Game;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class KeyboardInputInstaller: MonoInstaller
    {
        [SerializeField] private KeyboardInputController _keyboardInputController;
        public override void InstallBindings()
        {
            Container
                .Bind<IInputController>()
                .FromInstance(_keyboardInputController);
        }
    }
}