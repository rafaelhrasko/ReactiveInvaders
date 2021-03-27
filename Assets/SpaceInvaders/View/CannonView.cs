using UnityEngine;

namespace SpaceInvaders.View
{
    public class CannonView: MonoBehaviour, ICannonView
    {
        public Vector3 FiringPosition
        {
            get
            {
                return transform.position;
            }
        }
    }
}