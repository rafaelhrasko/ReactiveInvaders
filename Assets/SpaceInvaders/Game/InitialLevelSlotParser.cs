namespace SpaceInvaders.Game
{
    public static class InitialLevelSlotParser
    {
        public static InitialLevelSlot[][] Parse(string serialized)
        {
            var lines = serialized.Split('\n');
            var initialLevelSlots = new InitialLevelSlot[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                initialLevelSlots[i] = new InitialLevelSlot[line.Length];
                for (int j = 0; j < line.Length; j++)
                {
                    initialLevelSlots[i][j] = (InitialLevelSlot) int.Parse(line[j].ToString());
                }
            }

            return initialLevelSlots;
        }
    }
}