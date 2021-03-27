namespace SpaceInvaders.Game
{
    public class GameStateProvider : IGameStateProvider
    {
        public GameState Current { get; set; } = new GameState();
    }
}