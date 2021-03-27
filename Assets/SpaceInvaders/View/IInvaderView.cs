using SpaceInvaders.Game;

namespace SpaceInvaders.View
{
    public interface IInvaderView: IView
    {
        void Setup(InitialLevelSlot slot);
        void SwapSprite();
    }
}