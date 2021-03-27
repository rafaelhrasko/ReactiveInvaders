namespace SpaceInvaders.Game
{
    public class MockedLevelProvider : ILevelProvider
    {
        public LevelConfiguration GetConfiguration(int level)
        {
            return new LevelConfiguration()
            {
                InitialLevelSlots = InitialLevelSlotParser.Parse("11111\n11111"),
                InvaderSlotXDistance = 120,
                InvaderSlotYDistance = 100,
                InitialYPosition = 400,
            };
        }

        public InitialLevelSlot[][] Get(int level)
        {
            return InitialLevelSlotParser.Parse("11111\n11111");
        }
    }
}