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
            Player p3 = new Player() { Name = "c" };

            // Act
            GameBuilder b = new GameBuilder(0);
            b.SetSettings(settings);
            b.RegisterPlayer(p1);
            b.RegisterPlayer(p2);
            b.RegisterPlayer(p3, true);

            Assert.IsTrue(b.LastOpSuccessful == false);
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

            Assert.IsTrue(b.Finalize(0) == null);

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
            Assert.IsTrue(game.GetEnemyByIdentifier(p1.Identifier) == p2);
            Assert.IsTrue(game.GetEnemyByIdentifier(p2.Identifier) == p1);
        }

        [TestMethod]
        public void GetPlayerView_TestExpectedBehavior()
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
            b.RegisterPlayer(p1, true);
            b.RegisterPlayer(p2, true);
            b.SetDebugMode();
            Game game = b.Finalize(0);

            Game p1_view = game.GetPlayerView(p1.Identifier);
            Game p2_view = game.GetPlayerView(p2.Identifier, 0);

            Assert.IsTrue(p1_view.Player2.Board.CountAllMines(true, true) == 0);
            Assert.IsTrue(p1_view.Player1.Board.CountMineType<SimpleMine>() == settings.SimpleMineCount);
            Assert.IsTrue(p2_view.RedrawPoints.Count == 0);
        }

        [TestMethod]
        public void CompleteGame_TestExpectedBehaviour()
        {
            // Arrange
            GameSettings settings = new GameSettings()
            {
                Id = 0,
                Title = "Fast",
                BoardSize = 3,
                ShotsPerTurn = 999,
                SimpleMineCount = 2,
                WideMineCount = 1,
                FakeMineCount = 0
            };

            Player p1 = new Player() { Name = "a" };
            Player p2 = new Player() { Name = "b" };

            GameBuilder b = new GameBuilder(0);
            b.SetSettings(settings);
            b.RegisterPlayer(p1);
            b.RegisterPlayer(p2);

            Game game = b.Finalize(0);


            // p1 mines
            game.AddExecuteCommand(new MineCommand(
                new CoordInfo() { CommandType = "mine", Data = "", PositionX = 0, PositionY = 0 }, 
                p1.Identifier
                ));
            game.AddExecuteCommand(new MineCommand(
                new CoordInfo() { CommandType = "mine", Data = "", PositionX = 0, PositionY = 0 },
                p1.Identifier
                ));
            game.AddExecuteCommand(new MineCommand(
                new CoordInfo() { CommandType = "mine", Data = "", PositionX = 2, PositionY = 2 },
                p1.Identifier
                ));
            game.AddExecuteCommand(new MineCommand(
                new CoordInfo() { CommandType = "mine", Data = "", PositionX = 1, PositionY = 1 },
                p1.Identifier
                ));
            
            Assert.IsTrue(game.Player1.Board.CountAllMines(true, true) == 3);


            // p2 mines
            game.AddExecuteCommand(new MineCommand(
                new CoordInfo() { CommandType = "mine", Data = "", PositionX = 2, PositionY = 0 },
                p2.Identifier
                ));
            game.AddExecuteCommand(new MineCommand(
                new CoordInfo() { CommandType = "mine", Data = "", PositionX = 2, PositionY = 0 },
                p2.Identifier
                ));
            game.AddExecuteCommand(new MineCommand(
                new CoordInfo() { CommandType = "mine", Data = "", PositionX = 1, PositionY = 2 },
                p2.Identifier
                ));
            game.AddExecuteCommand(new MineCommand(
                new CoordInfo() { CommandType = "mine", Data = "", PositionX = 0, PositionY = 1 },
                p2.Identifier
                ));

            Assert.IsTrue(game.Player2.Board.CountAllMines(true, true) == 3);


            // end placing
            game.AddExecuteCommand(new EndTurnCommand(
                new CoordInfo() { CommandType = "endTurn", Data = "", PositionX = 0, PositionY = 0 },
                p1.Identifier
                ));
            game.AddExecuteCommand(new EndTurnCommand(
                new CoordInfo() { CommandType = "endTurn", Data = "", PositionX = 0, PositionY = 0 },
                p2.Identifier
                ));

            Assert.IsTrue(game.State is GameStatePlayerTurn);

            // p1 shoots once

            game.AddExecuteCommand(new ShotCommand(
                new CoordInfo() { CommandType = "shot", Data = "SSingleShot", PositionX = 0, PositionY = 0 },
                p1.Identifier
                ));

            game.AddExecuteCommand(new EndTurnCommand(
                new CoordInfo() { CommandType = "endTurn", Data = "", PositionX = 0, PositionY = 0 },
                p1.Identifier
                ));
  

            // p2 shoots and wins

            game.AddExecuteCommand(new ShotCommand(
                new CoordInfo() { CommandType = "shot", Data = "SNineShot", PositionX = 0, PositionY = 0 },
                p2.Identifier
                ));

            Assert.IsTrue(game.State is GameStateFinished);
        }
    }
}
