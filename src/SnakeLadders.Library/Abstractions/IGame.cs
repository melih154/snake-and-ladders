namespace SnakeLadders.Library.Abstractions
{
    interface IGame
    {
        void Start();
        MoveResult Move(PlayerToken playerToken);
    }
}
