using System;
using System.Linq;
using UniRx;

namespace SpaceInvaders.Game
{
    public class ClassicGameModeBehaviour : IGameModeBehaviour
    {
        private readonly IGameNotifications _gameNotifications;
        private readonly IGameStateProvider _gameStateProvider;

        public ClassicGameModeBehaviour(IGameNotifications gameNotifications,
            IGameStateProvider gameStateProvider)
        {
            _gameNotifications = gameNotifications;
            _gameStateProvider = gameStateProvider;
        }
        
        public IObservable<Unit> Execute()
        {
            return Observable.FromEvent(
                    handler => _gameNotifications.InvaderDeath += handler,
                    handler => _gameNotifications.InvaderDeath -= handler
                )
                .Select(_ => _gameStateProvider.Current.InvaderViews)
                .Where(invadersViews => invadersViews.All(view => !view.IsActive()))
                .AsUnitObservable();
        }
    }
}