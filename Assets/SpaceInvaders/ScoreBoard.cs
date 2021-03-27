namespace SpaceInvaders
{
    public class ScoreBoard
    {
        private int _points;

        public void AddPoints(int points)
        {
            _points += points;
        }
        
        public int GetTotal()
        {
            return _points;
        }

        public void Reset()
        {
            _points = 0;
        }
    }
}