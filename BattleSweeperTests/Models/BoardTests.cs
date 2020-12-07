using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperTests.Models
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void GetIndex_Test()
        {
            // Arrange
            Board board = new Board(10);
            int x = 0;
            int y = 0;
            int exp = 0;

            // Act
            var result = board.GetIndex(x, y);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exp, result);
        }

        [TestMethod]
        public void GetEnemyView_Test()
        {
            // Arrange
            Board board = new Board(20);
            int exp = 20;

            // Act
            Board result = board.GetEnemyView();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exp, result.GetEnemyView().Size);
        }

        [TestMethod]
        public void RevealTile_Test()
        {
            // Arrange
            Board board = new Board(10);
            int x = 0;
            int y = 0;

            // Act
            ChangePoint result = board.RevealTile(x,y);
            int exp = -1;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(board.Tiles[board.GetIndex(x, y)].State > exp);
        }

        [TestMethod]
        public void WithinBounds_Test()
        {
            // Arrange
            Board board = new Board(1);
            int x = 0;
            int y = 10;

            // Act
            bool result = board.WithinBounds(x, y);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CountAllMines_Test()
        {
            // Arrange
            Player player = new Player();
            GameSettings settings = new GameSettings()
            {
                Title = "Slow",
                BoardSize = 30,
                ShotsPerTurn = 5,
                SimpleMineCount = 80,
                WideMineCount = 20,
                FakeMineCount = 10
            };
            Board board = player.CreateRandomBoard(settings);
            bool countRevealed = false;
            bool countFake = false;
            int exp = 100;

            // Act
            int result = board.CountAllMines(
                countRevealed,
                countFake);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exp, result);
        }

        [TestMethod]
        public void CountMineType_Test()
        {
            // Arrange
            var player = new Player();
            GameSettings settings = new GameSettings()
            {
                Title = "Slow",
                BoardSize = 30,
                ShotsPerTurn = 5,
                SimpleMineCount = 80,
                WideMineCount = 20,
                FakeMineCount = 10
            };
            Board board = player.CreateRandomBoard(settings);
            int exp = 80;

            // Act
            int result = board.CountMineType<SimpleMine>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exp, result);
        }
    }
}
