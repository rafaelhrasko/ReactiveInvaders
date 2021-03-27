using UnityEngine;

namespace SpaceInvaders
{
    public interface IInvaderPositionProvider
    {
        void Initialize(Vector3[] positions);
        void MoveOffset(Vector3 offset);
        Vector3 GetForId(int id);
    }
}