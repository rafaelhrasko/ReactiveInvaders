using System;
using SpaceInvaders.View;

namespace SpaceInvaders.Game
{
    public class GameNotifications : IGameNotifications
    {
        public Action InvaderReachedEdge { get; set; }
        public Action PlayerDeath { get; set; }
        public Action InvaderDeath { get; set; }
        public Action RoundStart { get; set; }
        public Action RoundEnd { get; set; }
        public Action<int> ScoreChanged { get; set; }
        public Action<IMissileView, IMissileView> MissileHitMissile { get; set; }
        public Action<IMissileView, InvaderBehaviour> MissileHitInvader { get; set; }
        public Action<IMissileView, PlayerView> MissileHitPlayer { get; set; }
        public Action<InvaderBehaviour, PlayerView> InvaderHitPlayer { get; set; }
    }
}