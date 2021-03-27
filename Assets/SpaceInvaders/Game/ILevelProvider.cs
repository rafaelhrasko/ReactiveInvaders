namespace SpaceInvaders.Game
{
    public interface ILevelProvider
    {
        LevelConfiguration GetConfiguration(int level);
        InitialLevelSlot[][] Get(int level);
    }
}