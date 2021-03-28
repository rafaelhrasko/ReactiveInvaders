using SpaceInvaders.View;

namespace SpaceInvaders.Game
{
    public class ClassicLevelBehaviour : ILevelBehaviour
    {
        private readonly IViewProvider<IMissileView> _missileViewProvider;
        private readonly IViewProvider<IInvaderView> _invaderViewProvider;
        private readonly IGameNotifications _gameNotifications;
        private readonly IAddScore _addScore;
        private readonly IExplosionDispatcher _explosionDispatcher;

        public ClassicLevelBehaviour(
            IViewProvider<IMissileView> missileViewProvider,
            IViewProvider<IInvaderView> invaderViewProvider,
            IGameNotifications gameNotifications,
            IAddScore addScore,
            IExplosionDispatcher explosionDispatcher)
        {
            _missileViewProvider = missileViewProvider;
            _invaderViewProvider = invaderViewProvider;
            _gameNotifications = gameNotifications;
            _addScore = addScore;
            _explosionDispatcher = explosionDispatcher;
        }
        
        public void Initialize()
        {
            _gameNotifications.MissileHitInvader += MissileHitInvader;
            _gameNotifications.InvaderHitPlayer += InvaderHitPlayer;
            _explosionDispatcher.Initialize();
        }

        public void Disable()
        {
            _gameNotifications.MissileHitInvader -= MissileHitInvader;
            _gameNotifications.InvaderHitPlayer -= InvaderHitPlayer;
            _explosionDispatcher.Disable();
        }
        
        private void InvaderHitPlayer(InvaderBehaviour invader, PlayerView player)
        {
            _gameNotifications.PlayerDeath();
        }

        private void MissileHitInvader(IMissileView missile, InvaderBehaviour invader)
        {
            _addScore.Add(invader.Points);
            _missileViewProvider.Return(missile);
            _invaderViewProvider.Return(invader);
        }
        
    }
}