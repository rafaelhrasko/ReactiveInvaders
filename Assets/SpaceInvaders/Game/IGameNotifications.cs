using System;
using SpaceInvaders.View;

namespace SpaceInvaders.Game
{
    public interface IGameNotifications
    {
        Action InvaderReachedEdge { get; set; }
        Action PlayerDeath { get; set; }
        Action InvaderDeath { get; set; }
        Action RoundStart { get; set; }
        Action RoundEnd { get; set; }
        Action<int> ScoreChanged { get; set; }
        Action<IMissileView, IMissileView> MissileHitMissile { get; set; }
        Action<IMissileView, InvaderBehaviour> MissileHitInvader { get; set; }
        Action<IMissileView, PlayerView> MissileHitPlayer { get; set; }
        Action<InvaderBehaviour, PlayerView> InvaderHitPlayer { get; set; }
    }
}