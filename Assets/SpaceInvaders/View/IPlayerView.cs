using UnityEngine;

namespace SpaceInvaders.View
{
    public interface IPlayerView: IView
    {
        void MoveLeft();
        void MoveRight();
        Transform GetFiringTransform();
    }
}