using SpaceInvaders.View;

namespace SpaceInvaders.Game
{
    public class ExplosionDispatcher : IExplosionDispatcher
    {
        private readonly IViewProvider<IExplosionView> _explosionViewProvider;
        private readonly IGameNotifications _gameNotifications;

        public ExplosionDispatcher(
            IViewProvider<IExplosionView> explosionViewProvider,
            IGameNotifications gameNotifications)
        {
            _explosionViewProvider = explosionViewProvider;
            _gameNotifications = gameNotifications;
        }
        
        public void Initialize()
        {
            _gameNotifications.MissileHitPlayer += MissileHitPlayer;
            _gameNotifications.MissileHitInvader += MissileHitInvader;
            _gameNotifications.InvaderHitPlayer += InvaderHitPlayer;
            _gameNotifications.MissileHitMissile += MissileHitMissile;
        }

        private void MissileHitMissile(IMissileView arg1, IMissileView arg2)
        {
            ExplodeView(arg1,1);
            ExplodeView(arg2,1);
        }

        public void Disable()
        {
            _gameNotifications.MissileHitPlayer -= MissileHitPlayer;
            _gameNotifications.MissileHitInvader -= MissileHitInvader;
            _gameNotifications.InvaderHitPlayer -= InvaderHitPlayer;
            _gameNotifications.MissileHitMissile -= MissileHitMissile;
        }

        private void InvaderHitPlayer(InvaderBehaviour invader, PlayerView player)
        {
            ExplodeView(invader,3);
            ExplodeView(player,3);
        }

        private void MissileHitInvader(IMissileView missile, InvaderBehaviour invader)
        {
            ExplodeView(missile,1);
            ExplodeView(invader,2);
        }

        private void MissileHitPlayer(IMissileView missile, PlayerView player)
        {
            ExplodeView(missile,1);
            ExplodeView(player,3);
        }

        private void ExplodeView(IView view, int size)
        {
            var position = view.GetCurrentPosition();
            var explosion = _explosionViewProvider.Get();
            explosion.SetInitialPosition(position);
            explosion.SetExplosion(size);
        }
        
    }
}