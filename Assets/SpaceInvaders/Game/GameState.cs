using SpaceInvaders.View;

namespace SpaceInvaders
{
    public class GameState
    {
        public int Score;
        public int PlayerLives;
        public int CurrentLevel;
        public IInvaderView[] InvaderViews;
    }
}