using UnityEngine;

namespace SpaceInvaders.View
{
    public interface IMissileView: IView
    {
        void SetType(MissileType type);
        void SetVelocity(Vector3 velocity);
    }
}