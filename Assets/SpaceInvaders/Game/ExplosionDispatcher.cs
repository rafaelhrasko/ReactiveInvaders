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
        }
        
        public void Disable()
        {
            _gameNotifications.MissileHitPlayer -= MissileHitPlayer;
            _gameNotifications.MissileHitInvader -= MissileHitInvader;
            _gameNotifications.InvaderHitPlayer -= InvaderHitPlayer;
        }

        private void InvaderHitPlayer(InvaderBehaviour invader, PlayerBehaviour player)
        {
            ExplodeView(invader,3);
            ExplodeView(player,3);
        }

        private void MissileHitInvader(IMissileView missile, InvaderBehaviour invader)
        {
            ExplodeView(missile,1);
            ExplodeView(invader,2);
        }

        private void MissileHitPlayer(IMissileView missile, PlayerBehaviour player)
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