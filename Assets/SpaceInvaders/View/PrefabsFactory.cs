using ModestTree;
using UnityEngine;

namespace SpaceInvaders.View
{
    [CreateAssetMenu(menuName = "Scriptable Object/Prefabs Factory")]
    public class PrefabsFactory: ScriptableObject, IPrefabsFactory
    {
        [SerializeField] private MissileBehaviour _missilePrefab;
        [SerializeField] private InvaderBehaviour _invaderPrefab;
        public TBehaviour Instantiate<TBehaviour>() where TBehaviour : MonoBehaviour
        {
            var type = typeof(TBehaviour);
            if (type.DerivesFrom(typeof(IMissileView)))
            {
                return Instantiate(_missilePrefab).GetComponent<TBehaviour>();
            }
            if (type.DerivesFrom(typeof(IInvaderView)))
            {
                return Instantiate(_invaderPrefab).GetComponent<TBehaviour>();
            }
            
            UnityEngine.Debug.LogError("[PrefabsFactory] Unknown TBehaviour. Fullname=" + type.FullName);
            return null;
        }
    }
}