using System;
using UniRx;

namespace SpaceInvaders.Game
{
    public interface IGameModeBehaviour
    {
        IObservable<Unit> Execute();
    }
}