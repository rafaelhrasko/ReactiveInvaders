using System;
using UnityEngine;

namespace SpaceInvaders.Configuration
{
    [Serializable]
    public class SerializableLevelConfiguration
    {
        public string LevelName;
        [TextArea]
        public string InitialLevelSlots;
        public float InvaderSlotXDistance;
        public float InvaderSlotYDistance;
        public float InitialYPosition;
    }
}