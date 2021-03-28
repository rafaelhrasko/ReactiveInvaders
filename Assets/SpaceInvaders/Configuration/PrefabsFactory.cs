using ModestTree;
using SpaceInvaders.View;
using UnityEngine;

namespace SpaceInvaders.Configuration
{
    [CreateAssetMenu(menuName = "Scriptable Object/Prefabs Factory")]
    public class PrefabsFactory: ScriptableObject, IPrefabsFactory
    {
        [SerializeField] private MissileBehaviour _missilePrefab;
        [SerializeField] private InvaderBehaviour _invaderPrefab;
        [SerializeField] private ExplosionView _explosionPrefab;
        [SerializeField] private PlayerView _playerPrefab;
        
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
            if (type.DerivesFrom(typeof(IExplosionView)))
            {
                return Instantiate(_explosionPrefab).GetComponent<TBehaviour>();
            }
            if (type.DerivesFrom(typeof(IPlayerView)))
            {
                return Instantiate(_playerPrefab).GetComponent<TBehaviour>();
            }
            
            UnityEngine.Debug.LogError("[PrefabsFactory] Unknown TBehaviour. Fullname=" + type.FullName);
            return null;
        }
    }
}