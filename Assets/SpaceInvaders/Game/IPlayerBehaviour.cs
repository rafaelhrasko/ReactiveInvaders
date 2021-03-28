using System;
using UniRx;

namespace SpaceInvaders.Game
{
    public interface IPlayerBehaviour
    {
        IObservable<Unit> Execute();
    }
}