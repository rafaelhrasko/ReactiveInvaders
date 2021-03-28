using System;
using UniRx;

namespace SpaceInvaders.Game
{
    public interface IInputController
    {
        IObservable<Unit> OnPlayerStart();
        IObservable<Unit> OnPlayerFired();
        IObservable<Unit> OnPlayerMovedLeft();
        IObservable<Unit> OnPlayerMovedRight();
    }
}