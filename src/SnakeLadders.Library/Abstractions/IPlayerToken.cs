namespace SnakeLadders.Library.Abstractions
{
    public interface IPlayerToken
    {
        void Move(MoveResult moveResult);
        void Move(int diceResult);
        int GetCurrentSquare();
    }
}
