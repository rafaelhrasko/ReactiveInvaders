using UnityEngine;

namespace SpaceInvaders.View
{
    public interface IPrefabsFactory
    {
        TBehaviour Instantiate<TBehaviour>() 
            where TBehaviour : MonoBehaviour;
    }
}