using Moq;
using NUnit.Framework;
using SnakeLadders.Library;
using SnakeLadders.Library.Abstractions;
using SnakeLadders.Library.Settings;
using SnakeLadders.Tests.Unit.Infra;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class PlayerTokenTests
    {
        [Test]
        [TestCase(1)]
        public void GivenGameStarted_WhenTokenPlacedOnBoard_TokenOnInitialSqaure(int initalSquare)
        {
            // Arrange
            var playerToken1 = new PlayerToken();
            var playerToken2 = new PlayerToken();

            var board = new Board(new List<IPlayerToken> { playerToken1, playerToken2 });

            var gameSettings = new GameSettings { InitialSquare = initalSquare };
            var game = new Game(board, null, gameSettings);

            // Act
            game.Start();

            // Assert
            Assert.AreEqual(initalSquare, playerToken1.GetCurrentSquare());
            Assert.AreEqual(initalSquare, playerToken2.GetCurrentSquare());
        }

        [Test]
        [TestCase(3)]
        public void GivenTokenIsOnBoard_WhenDiceRolledAndNoRejection_TokenMovedDiceResult(int diceResult, int initialSquare = 1, int winnerSquare = 100)
        {
            // Arrange
            var playerToken1 = new PlayerToken();
            var board = new Board(new List<IPlayerToken> { playerToken1});

            var gameSettings = new GameSettings { InitialSquare = initialSquare, WinnerSquare = winnerSquare };

            var mockDice = new Mock<IDice>();
            mockDice
                .Setup(x => x.Roll())
                .Returns(diceResult);

            var game = new Game(board, mockDice.Object, gameSettings);
            game.Start();

            // Act
            var moveResult = game.Move(playerToken1);

            // Assert
            Assert.AreEqual(initialSquare + diceResult, playerToken1.GetCurrentSquare());
            Assert.AreEqual(initialSquare + diceResult, moveResult.NewSquare);
            Assert.AreEqual(MoveStatus.Moved, moveResult.Status);
        }

        [Test]
        [TestCase(new int[] {3, 4})]
        public void GivenTokenIsOnBoard_WhenDiceRolledTwiceAndNoRejection_TokenMoveOnBothDiceResults(int[] diceResults, int initialSquare = 1, int winnerSquare = 100)
        {
            // Arrange

            if (initialSquare + diceResults.Sum() > winnerSquare)
                Assert.Fail("Sum of given dice results exceeds the winner square. Please update dice results to fix that.");

            var playerToken1 = new PlayerToken();
            var board = new Board(new List<IPlayerToken> { playerToken1});

            var gameSettings = new GameSettings { InitialSquare = initialSquare, WinnerSquare = winnerSquare };

            var mockDice = new Mock<IDice>();
            mockDice
                .Setup(x => x.Roll())
                .ReturnsInOrder(diceResults);

            var game = new Game(board, mockDice.Object, gameSettings);
            game.Start();
            
            MoveResult moveResult = null;
            // Act
            for (int i = 0; i < diceResults.Length; i++)
                moveResult = game.Move(playerToken1);

            // Assert
            Assert.AreEqual(initialSquare + diceResults.Sum(), playerToken1.GetCurrentSquare());
            Assert.AreEqual(initialSquare + diceResults.Sum(), moveResult?.NewSquare);
            Assert.AreEqual(MoveStatus.Moved, moveResult?.Status);
        }

        [Test]
        [TestCase(1, 6, new int[] { 5 })]
        public void GivenTokenIsOnBoard_WhenWinningDiceRolled_GameWon(int initialSquare, int winnerSquare, int[] diceResults)
        {
            // Arrange

            var playerToken1 = new PlayerToken();
            var board = new Board(new List<IPlayerToken> { playerToken1 });

            var gameSettings = new GameSettings { InitialSquare = initialSquare, WinnerSquare = winnerSquare };

            var mockDice = new Mock<IDice>();
            mockDice
                .Setup(x => x.Roll())
                .ReturnsInOrder(diceResults);

            var game = new Game(board, mockDice.Object, gameSettings);
            game.Start();

            MoveResult moveResult = null;
            // Act
            for (int i = 0; i < diceResults.Length; i++)
                moveResult = game.Move(playerToken1);

            // Assert
            Assert.AreEqual(initialSquare + diceResults.Sum(), playerToken1.GetCurrentSquare());
            Assert.AreEqual(initialSquare + diceResults.Sum(), moveResult?.NewSquare);
            Assert.AreEqual(MoveStatus.GameWon, moveResult?.Status);
        }

        [Test]
        [TestCase(1, 6, new int[] { 6 })]
        public void GivenTokenIsOnBoard_WhenWinningDiceRolled_GameNotWon(int initialSquare, int winnerSquare, int[] diceResults)
        {
            // Arrange

            if (initialSquare + diceResults.Sum() - diceResults.Last() > winnerSquare)
                Assert.Fail("Sum of given dice results, excluding the last one, exceeds the winner square. Please update dice results to fix that.");

            var playerToken1 = new PlayerToken();
            var board = new Board(new List<IPlayerToken> { playerToken1 });

            var gameSettings = new GameSettings { InitialSquare = initialSquare, WinnerSquare = winnerSquare };

            var mockDice = new Mock<IDice>();
            mockDice
                .Setup(x => x.Roll())
                .ReturnsInOrder(diceResults);

            var game = new Game(board, mockDice.Object, gameSettings);
            game.Start();

            MoveResult moveResult = null;
            // Act
            for (int i = 0; i < diceResults.Length; i++)
                moveResult = game.Move(playerToken1);

            // Assert
            Assert.AreEqual(initialSquare + diceResults.Sum() - diceResults.Last(), playerToken1.GetCurrentSquare());
            Assert.AreEqual(initialSquare + diceResults.Sum() - diceResults.Last(), moveResult?.NewSquare);
            Assert.AreEqual(MoveStatus.Rejected, moveResult?.Status);
        }
    }
}