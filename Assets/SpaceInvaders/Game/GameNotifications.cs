﻿using System;
using SpaceInvaders.View;

namespace SpaceInvaders.Game
{
    public class GameNotifications : IGameNotifications
    {
        public Action InvaderReachedEdge { get; set; }
        public Action<IMissileView, InvaderBehaviour> MissileHitInvader { get; set; }
        public Action<IMissileView, PlayerBehaviour> MissileHitPlayer { get; set; }
        public Action<InvaderBehaviour, PlayerBehaviour> InvaderHitPlayer { get; set; }
    }
}