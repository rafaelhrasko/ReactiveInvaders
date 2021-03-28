namespace SpaceInvaders.View
{
    public interface IPlayerView: IView
    {
        void MoveLeft();
        void MoveRight();
        void FireMissile(float firingSpeed);
    }
}