namespace SpaceInvaders.Game
{
    public interface ILevelSetup
    {
        void Setup(float invaderSlotXDistance, float invaderSlotYDistance, float initialYPosition);
    }
}