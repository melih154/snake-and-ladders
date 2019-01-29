using SnakeLadders.Library.Abstractions;

namespace SnakeLadders.Library
{
    public class PlayerToken : IPlayerToken
    {
        private int currentSquare;

        public int GetCurrentSquare()
        {
            return currentSquare;
        }

        public void Move(MoveResult moveResult)
        {
            if (moveResult?.Status == MoveStatus.Moved || moveResult?.Status == MoveStatus.GameWon)
                currentSquare = moveResult.NewSquare;
        }

        public void Move(int diceResult)
        {
            currentSquare += diceResult; 
        }
    }
}
