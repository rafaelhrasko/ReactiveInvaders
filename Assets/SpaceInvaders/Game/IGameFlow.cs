using System;
using UniRx;

namespace SpaceInvaders.Game
{
    public interface IGameFlow
    {
        IObservable<Unit> Execute();
    }
}