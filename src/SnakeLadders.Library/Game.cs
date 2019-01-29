using SnakeLadders.Library.Abstractions;
using SnakeLadders.Library.Settings;

namespace SnakeLadders.Library
{
    public class Game : IGame
    {
        private readonly Board _board;
        private readonly IDice _dice;
        private readonly GameSettings _gameSettings;

        public Game(Board board, IDice dice, GameSettings gameSettings)
        {
            _board = board;
            _dice = dice;
            _gameSettings = gameSettings;
        }

        public void Start()
        {
            var moveResult = new MoveResult { NewSquare = _gameSettings.InitialSquare, Status = MoveStatus.Moved };

            foreach (var playerToken in _board.PlayerTokens)
            {
                playerToken.Move(moveResult);
            }
        }

        public MoveResult Move(PlayerToken playerToken)
        {
            var diceResult = _dice.Roll();

            if (!ValidateMove(playerToken, diceResult))
                return new MoveResult { NewSquare = playerToken.GetCurrentSquare(), Status = MoveStatus.Rejected };

            playerToken.Move(diceResult);

            if (IsPlayerWon(playerToken))
                return new MoveResult { NewSquare = playerToken.GetCurrentSquare(), Status = MoveStatus.GameWon };

            return new MoveResult { NewSquare = playerToken.GetCurrentSquare(), Status = MoveStatus.Moved };
        }
        
        private bool ValidateMove(PlayerToken playerToken, int diceResult)
        {
            return playerToken.GetCurrentSquare() + diceResult <= _gameSettings.WinnerSquare;
        }

        private bool IsPlayerWon(PlayerToken playerToken)
        {
            return playerToken.GetCurrentSquare() == _gameSettings.WinnerSquare;
        }
    }
}
