using SpaceInvaders.Game;
using UnityEngine;

namespace SpaceInvaders.Configuration
{
    [CreateAssetMenu(menuName = "Scriptable Object/Invaders Configuration")]
    public class InvadersConfiguration: ScriptableObject, IInvaderConfigurationProvider
    {
        [SerializeField] private InvaderConfiguration[] _configurations;
        public InvaderConfiguration Get(InitialLevelSlot slot)
        {
            for (int i = 0; i < _configurations.Length; i++)
            {
                var invaderConfiguration = _configurations[i];
                if (invaderConfiguration.Slot == slot)
                {
                    return invaderConfiguration;
                }
            }

            UnityEngine.Debug.LogError(" invaderConfiguration Slot not found: " + slot);
            return null;
        }
    }
}