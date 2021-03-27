using UnityEngine;

namespace SpaceInvaders.View
{
    public interface IView
    {
        void SetInitialPosition(Vector3 position);
        bool IsActive();
        void Activate();
        void Deactivate();
    }
}