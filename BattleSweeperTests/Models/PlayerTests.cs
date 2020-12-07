using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperServerTests.Models
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void CreateIdentifier_TestForNull()
        {
            // Arrange
            Player player = new Player();
            int seed = 0;

            // Act
            string result = player.CreateIdentifier(seed);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateIdentifier_TestForIdentifier()
        {
            // Arrange
            Player player = new Player();
            int seed = 0;
            player.Name = "test";

            // Act
            string result = player.CreateIdentifier(seed);
            string expected = "ABCDABCDABCDABCD" + 0 + "test";

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreateBoard_Test()
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

            int expBoardSize = 30;

            // Act
            Board result = player.CreateBoard(settings);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expBoardSize, result.Size);
        }

        [TestMethod]
        public void CreateRandomBoard_Test()
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

            // Act
            Board result = player.CreateRandomBoard(settings);
            int exp = 900;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exp, result.Tiles.Count);
        }

        [TestMethod]
        public void GetEnemyView_Test()
        {
            // Arrange
            Player player = new Player();
            player.AmmoCount = 10;
            player.Name = "test";
            player.Board = new Board(10);
            int exp = 10;

            // Act
            Player result = player.GetEnemyView();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exp, result.AmmoCount);
        }

        [TestMethod]
        public void CreateMemento_Test()
        {
            // Arrange
            Player player = new Player();
            player.Name = "test";
            player.CreateIdentifier(0);
            player.AmmoCount = 10;
            player.Board = new Board(10);
            int exp = 10;

            // Act
            Memento result = player.CreateMemento();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exp, result.AmmoCountState());
        }

        [TestMethod]
        public void SetMemento_Test()
        {
            // Arrange
            Player player = new Player();
            Memento memento = new Memento("ABCDABCDABCDABCD0test", 10, "test", new Board(10));

            Memento exp = new Memento("ABCDABCDABCDABCD0test", 10, "test", new Board(10));

            // Act
            player.SetMemento(memento);

            // Assert
            Assert.IsNotNull(player);
            Assert.AreEqual(exp.IdentifierState(), player.Identifier);
        }
    }
}
