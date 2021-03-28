using UnityEngine;

namespace SpaceInvaders.Configuration
{
    [CreateAssetMenu(menuName = "Scriptable Object/GameMode Configuration Provider")]
    public class GameModeConfigurationProvider : ScriptableObject, IGameModeConfigurationProvider
    {
        [SerializeField] private GameModeConfiguration _configuration;
        public GameModeConfiguration Get()
        {
            return _configuration;
        }
    }
}