using UnityEngine;

namespace SpaceInvaders.View
{
    public interface IMissileView: IView
    {
        void SetVelocity(Vector3 velocity);
    }
}