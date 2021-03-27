namespace SpaceInvaders.Game
{
    public interface ILevelProvider
    {
        InitialLevelSlot[][] Get(int level);
    }
}