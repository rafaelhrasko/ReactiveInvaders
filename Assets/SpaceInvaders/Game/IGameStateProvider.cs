namespace SpaceInvaders.Game
{
    public interface IGameStateProvider
    {
        GameState Current { get; set; }
    }
}