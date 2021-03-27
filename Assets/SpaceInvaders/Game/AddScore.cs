namespace SpaceInvaders.Game
{
    public class AddScore : IAddScore
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly IGameNotifications _gameNotifications;

        public AddScore(IGameStateProvider gameStateProvider, IGameNotifications gameNotifications)
        {
            _gameStateProvider = gameStateProvider;
            _gameNotifications = gameNotifications;
        }
        
        public void Add(int points)
        {
            _gameStateProvider.Current.Score += points;
            _gameNotifications.ScoreChanged(_gameStateProvider.Current.Score);
        }
    }
}