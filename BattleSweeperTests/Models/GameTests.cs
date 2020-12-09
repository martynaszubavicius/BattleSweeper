using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleSweeperTests.Models
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameBuilder_TestExpectedBehaviour()
        {
            // Arrange
            GameSettings settings = new GameSettings()
            {
                Id = 0,
                Title = "Fast",
                BoardSize = 10,
                ShotsPerTurn = 3,
                SimpleMineCount = 15,
                WideMineCount = 0,
                FakeMineCount = 0
            };

            Player p1 = new Player() { Name = "a" };
            Player p2 = new Player() { Name = "b" };

            // Act
            GameBuilder b = new GameBuilder(0);
            b.SetSettings(settings);
            b.RegisterPlayer(p1);
            b.RegisterPlayer(p2);
            b.SetDebugMode();
            Game game = b.Finalize(0);


            // Assert
            Assert.IsTrue(game.Settings == settings);
            Assert.IsTrue(game.Player1 == p1);
            Assert.IsTrue(game.Player2 == p2);
            Assert.IsTrue(game.State is GameStateDebug);
        }

        [TestMethod]
        public void HasPlayerWithIdentifier_TestExpectedBehaviour()
        {
            // Arrange
            GameSettings settings = new GameSettings()
            {
                Id = 0,
                Title = "Fast",
                BoardSize = 10,
                ShotsPerTurn = 3,
                SimpleMineCount = 15,
                WideMineCount = 0,
                FakeMineCount = 0
            };

            Player p1 = new Player() { Name = "a" };
            Player p2 = new Player() { Name = "b" };

            GameBuilder b = new GameBuilder(0);
            b.SetSettings(settings);
            b.RegisterPlayer(p1);
            b.RegisterPlayer(p2);
            b.SetDebugMode();
            Game game = b.Finalize(0);

            // Assert
            Assert.IsTrue(game.HasPlayerWithIdentifier(p1.Identifier));
            Assert.IsTrue(game.HasPlayerWithIdentifier(p2.Identifier));
        }

        [TestMethod]
        public void GetPlayerByIdentifier_TestExpectedBehavior()
        {
            // Arrange
            GameSettings settings = new GameSettings()
            {
                Id = 0,
                Title = "Fast",
                BoardSize = 10,
                ShotsPerTurn = 3,
                SimpleMineCount = 15,
                WideMineCount = 0,
                FakeMineCount = 0
            };

            Player p1 = new Player() { Name = "a" };
            Player p2 = new Player() { Name = "b" };

            GameBuilder b = new GameBuilder(0);
            b.SetSettings(settings);
            b.RegisterPlayer(p1);
            b.RegisterPlayer(p2);
            b.SetDebugMode();
            Game game = b.Finalize(0);

            // Assert
            Assert.IsTrue(game.GetPlayerByIdentifier(p1.Identifier) == p1);
            Assert.IsTrue(game.GetPlayerByIdentifier(p2.Identifier) == p2);
        }

        [TestMethod]
        public void GetEnemyByIdentifier_TestExpectedBehavior()
        {
            // Arrange
            GameSettings settings = new GameSettings()
            {
                Id = 0,
                Title = "Fast",
                BoardSize = 10,
                ShotsPerTurn = 3,
                SimpleMineCount = 15,
                WideMineCount = 0,
                FakeMineCount = 0
            };

            Player p1 = new Player() { Name = "a" };
            Player p2 = new Player() { Name = "b" };

            GameBuilder b = new GameBuilder(0);
            b.SetSettings(settings);
            b.RegisterPlayer(p1);
            b.RegisterPlayer(p2);
            b.SetDebugMode();
            Game game = b.Finalize(0);

            // Assert
            Assert.IsTrue(game.GetPlayerByIdentifier(p1.Identifier) == p2);
            Assert.IsTrue(game.GetPlayerByIdentifier(p2.Identifier) == p1);
        }













        [TestMethod]
        public void GetPlayerView_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var game = new Game();
            string playerIdentifier = null;
            int historyStartIndex = 0;

            // Act
            var result = game.GetPlayerView(
                playerIdentifier,
                historyStartIndex);

            // Assert
            Assert.Fail();
        }

        



        [TestMethod]
        public void AddExecuteCommand_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var game = new Game();
            Command command = null;

            // Act
            game.AddExecuteCommand(
                command);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void UndoLastPlayerCommand_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var game = new Game();
            string playerIdentifier = null;

            // Act
            game.UndoLastPlayerCommand(
                playerIdentifier);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetCommandsLog_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var game = new Game();
            string format = null;

            // Act
            var result = game.GetCommandsLog(
                format);

            // Assert
            Assert.Fail();
        }
    }
}
