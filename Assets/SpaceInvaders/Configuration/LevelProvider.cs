using SpaceInvaders.Game;
using UnityEngine;

namespace SpaceInvaders.Configuration
{
    [CreateAssetMenu(menuName = "Scriptable Object/Levels Provider")]
    public class LevelProvider: ScriptableObject, ILevelProvider
    {
        public SerializableLevelConfiguration[] _levels;
        
        public LevelConfiguration GetConfiguration(int level)
        {
            var serializable = _levels[level % _levels.Length];
            return new LevelConfiguration()
            {
                InitialLevelSlots = InitialLevelSlotParser.Parse(serializable.InitialLevelSlots),
                InitialYPosition = serializable.InitialYPosition,
                InvaderSlotXDistance = serializable.InvaderSlotXDistance,
                InvaderSlotYDistance = serializable.InvaderSlotYDistance
            };
        }
    }
}