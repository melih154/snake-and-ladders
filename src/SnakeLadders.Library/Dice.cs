using SnakeLadders.Library.Abstractions;
using SnakeLadders.Library.Settings;
using System;

namespace SnakeLadders.Library
{
    public class Dice : IDice
    {
        private static readonly Random Random = new Random();

        private readonly DiceSettings _diceSettings;

        public Dice(DiceSettings diceSettings)
        {
            _diceSettings = diceSettings;
        }
        
        public int Roll()
        {
            return Random.Next(_diceSettings.MinValue, _diceSettings.MaxValue);
        }
    }
}
