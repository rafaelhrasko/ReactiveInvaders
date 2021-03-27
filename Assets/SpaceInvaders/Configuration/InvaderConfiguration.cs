using System;
using SpaceInvaders.Game;
using UnityEngine;

namespace SpaceInvaders.Configuration
{
    [Serializable]
    public class InvaderConfiguration
    {
        public InitialLevelSlot Slot;
        public Sprite[] Sprites;
        public int Points;
    }
}