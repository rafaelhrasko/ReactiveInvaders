using SpaceInvaders.View;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public interface IGenerateMissile
    {
        void Generate(Vector3 firingPosition, Vector3 velocity, MissileType type);
    }
}