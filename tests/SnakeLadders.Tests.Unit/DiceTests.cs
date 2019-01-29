using NUnit.Framework;
using SnakeLadders.Library;
using SnakeLadders.Library.Settings;

namespace SnakeLadders.Tests.Unit
{
    public class DiceTests
    {
        [Test]
        [TestCase(1, 6)]
        [Repeat(100)]
        public void DiceSettingsSet_DiceRolled_ValueWithinGivenSettings(int minDiceValue, int maxDiceValue)
        {
            // Arrange
            var diceSettings = new DiceSettings { MinValue = minDiceValue, MaxValue = maxDiceValue };

            var dice = new Dice(diceSettings);

            // Act
            var diceResult = dice.Roll();

            // Assert
            Assert.True(minDiceValue <= diceResult);
            Assert.True(maxDiceValue >= diceResult);
        }
    }
}
