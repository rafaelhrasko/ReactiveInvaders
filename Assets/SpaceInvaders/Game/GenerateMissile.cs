using SpaceInvaders.View;
using UnityEngine;

namespace SpaceInvaders.Game
{
    public class GenerateMissile : IGenerateMissile
    {
        private readonly IViewProvider<IMissileView> _missileViewProvider;

        public GenerateMissile(IViewProvider<IMissileView> missileViewProvider)
        {
            _missileViewProvider = missileViewProvider;
        }
        
        public void Generate(Vector3 firingPosition, Vector3 velocity, MissileType type)
        {
            var missile = _missileViewProvider.Get();
            missile.SetType(type);
            missile.SetInitialPosition(firingPosition);
            missile.SetVelocity(velocity);
        }
    }
}