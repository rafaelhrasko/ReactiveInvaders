using UnityEngine;

namespace SpaceInvaders.View
{
    public interface IView
    {
        Vector3 GetCurrentPosition();
        void SetInitialPosition(Vector3 position);
        bool IsActive();
        void Activate();
        void Deactivate();
    }
}