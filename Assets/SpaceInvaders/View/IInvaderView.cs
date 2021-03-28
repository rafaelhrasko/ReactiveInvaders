using SpaceInvaders.Configuration;

namespace SpaceInvaders.View
{
    public interface IInvaderView: IView
    {
        void Setup(InvaderConfiguration invaderConfiguration);
        void SwapSprite();
    }
}