using System;
using SpaceInvaders.View;

namespace SpaceInvaders.Game
{
    public interface IGameNotifications
    {
        Action InvaderReachedEdge { get; set; }
        Action<IMissileView, InvaderBehaviour> MissileHitInvader { get; set; }
        Action<IMissileView, PlayerBehaviour> MissileHitPlayer { get; set; }
        Action<InvaderBehaviour, PlayerBehaviour> InvaderHitPlayer { get; set; }
    }
}