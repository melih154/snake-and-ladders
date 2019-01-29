using SnakeLadders.Library.Abstractions;
using System.Collections.Generic;

namespace SnakeLadders.Library
{
    public class Board
    {
        private readonly IEnumerable<IPlayerToken> _playerTokens;

        public Board(IEnumerable<IPlayerToken> playerTokens)
        {
            _playerTokens = playerTokens;
        }

        internal IEnumerable<IPlayerToken> PlayerTokens => _playerTokens;
    }
}
