using SpaceInvaders.Game;

namespace SpaceInvaders.Configuration
{
    public interface IInvaderConfigurationProvider
    {
        InvaderConfiguration Get(InitialLevelSlot slot);
    }
}