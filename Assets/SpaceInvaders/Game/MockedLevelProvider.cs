namespace SpaceInvaders.Game
{
    public class MockedLevelProvider : ILevelProvider
    {
        public InitialLevelSlot[][] Get(int level)
        {
            return InitialLevelSlotParser.Parse("11111\n11111");
        }
    }
}