using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperServerTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void CreateIdentifier_TestForNull()
        {
            // Arrange
            var player = new Player();
            int seed = 0;

            // Act
            var result = player.CreateIdentifier(
                seed);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateIdentifier_TestForIdentifier()
        {
            // Arrange
            var player = new Player();
            int seed = 0;
            player.Name = "test";

            // Act
            var result = player.CreateIdentifier(
                seed);
            var expected = "ABCDABCDABCDABCD" + 0 + "test";

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CreateBoard_Test()
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

            var expBoardSize = 30;

            // Act
            var result = player.CreateBoard(
                settings);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Size, expBoardSize);
        }

        [TestMethod]
        public void CreateRandomBoard_Test()
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

            // Act
            var result = player.CreateRandomBoard(
                settings);
            var exp = 900;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Tiles.Count, exp);
        }

        [TestMethod]
        public void GetEnemyView_Test()
        {
            // Arrange
            var player = new Player();
            player.AmmoCount = 10;
            player.Name = "test";
            player.Board = new Board(10);
            var exp = 10;

            // Act
            var result = player.GetEnemyView();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.AmmoCount, exp);
        }

        [TestMethod]
        public void CreateMemento_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var player = new Player();
            player.Name = "test";
            player.CreateIdentifier(0);
            player.AmmoCount = 10;
            player.Board = new Board(10);
            var exp = 10;

            // Act
            var result = player.CreateMemento();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.AmmoCountState(), exp);
        }

        [TestMethod]
        public void SetMemento_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var player = new Player();
            Memento memento = new Memento("ABCDABCDABCDABCD0test", 10, "test", new Board(10));

            var exp = new Memento("ABCDABCDABCDABCD0test", 10, "test", new Board(10));

            // Act
            player.SetMemento(
                memento);

            // Assert
            Assert.IsNotNull(player);
            Assert.AreEqual(player.Identifier, exp.IdentifierState());
        }
    }
}
