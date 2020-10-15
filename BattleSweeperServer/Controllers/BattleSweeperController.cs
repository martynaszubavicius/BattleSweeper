using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSweeperServer.DesignPatternClasses;
using BattleSweeperServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleSweeperServer.Controllers
{
    [Route("BattleSweeper")]
    [ApiController]
    public class BattleSweeperController : ControllerBase
    {
        private static List<Game> games = new List<Game>();
        private static List<GameSettings> gameSettings = new List<GameSettings>();


        public BattleSweeperController()
        {
            gameSettings.Add(new GameSettings()
            {
                Id = gameSettings.Count,
                Title = "Fast",
                BoardSize = 10,
                ShotsPerTurn = 3,
                SimpleMineCount = 15,
                WideMineCount = 0,
                FakeMineCount = 0
            });
            gameSettings.Add(new GameSettings()
            {
                Id = gameSettings.Count,
                Title = "Normal",
                BoardSize = 15,
                ShotsPerTurn = 4,
                SimpleMineCount = 30,
                WideMineCount = 5,
                FakeMineCount = 5
            });
            gameSettings.Add(new GameSettings()
            {
                Id = gameSettings.Count,
                Title = "Slow",
                BoardSize = 30,
                ShotsPerTurn = 5,
                SimpleMineCount = 80,
                WideMineCount = 20,
                FakeMineCount = 10
            });
        }

        // https://localhost:44337/BattleSweeper
        //
        // GET  GameTypes - all the game types available
        // POST CreateGame - new game data
        // POST Game/{id}/RegisterPlayer - register player to a game with id
        // 
        // Requests below require "PlayerIdentifier" Header
        // 
        // GET  Game/{id}/State - get current game state
        // POST Game/{id}/TestMineCycle - cycle a mine on players board
        // POST Game/{id}/TestShot - reveal tile on enemys board

        [HttpGet("GameSettings")]
        public ActionResult<IEnumerable<GameSettings>> GetGameSettings()
        {
            return gameSettings;
        }

        // TODO: Deprecated, to be removed. Functionality moved to GetNewGameFromSettings
        [HttpPost("CreateGame")]
        public ActionResult<Game> CreateGame(Game game)
        {
            lock (games)
            {
                game.Id = games.Count;
                games.Add(game);
            }

            return CreatedAtAction("CreateGame", new { id = game.Id }, game);
        }

        [HttpGet("GetNewGameFromSettings/{id}")]
        public ActionResult<Game> CreateGameFromSettings(int Id)
        {
            GameSettings settings = gameSettings.Find(st => st.Id == Id);
            if (settings == null)
                return NotFound();

            // TODO: Will use builder instead
            Game game = new Game() {
                Settings = settings
            };

            lock (games)
            {
                game.Id = games.Count;
                games.Add(game);
            }

            return game;
        }

        [HttpPost("Game/{id}/RegisterPlayer")]
        public ActionResult<Player> RegisterPlayer(int id, Player player)
        {
            Game game = games.Find(game => game.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            if (game.RegisterPlayer(player))
            {
                player.AmmoCount = 3;
                Board board = player.CreateBoard(game.Settings.BoardSize);
                
                // temporary for testing
                Random rnd = new Random();
                int random_nr;
                board.Tiles = new List<Tile>();
                MineFactory mineFactory = new MineFactory();
                for (int i = 0; i < board.Size * board.Size; i++)
                {
                    board.Tiles.Add(new Tile());
                    random_nr = rnd.Next(0, 9);
                    if (random_nr == 0)
                        board.Tiles[i].Mine = mineFactory.CreateMine(0); // Simple Mine
                    if (random_nr == 1)
                        board.Tiles[i].Mine = mineFactory.CreateMine(1); // Wide Mine
                    if (random_nr == 2)
                        board.Tiles[i].Mine = mineFactory.CreateMine(2); // Fake Mine
                }
                //for (int x = 0; x < board.Size; x++)
                //{
                //    for (int y = 0; y < board.Size; y++)
                //    {
                //        board.RevealTile(x, y);
                //    }
                //}
                // done yay
            }
            else
            {
                return BadRequest(new { error = "game full" });
            }

            return player;
        }

        [HttpGet("Game/{id}/Settings")]
        public ActionResult<GameSettings> GetGameSettings(int id)
        {
            Game game = games.Find(game => game.Id == id);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            return game.Settings;
        }

        [HttpGet("Game/{id}/State")]
        public ActionResult<Game> GetGameState(int id)
        {
            Game game = games.Find(game => game.Id == id);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            return game.GetPlayerView(Request.Headers["PlayerIdentifier"]);
        }


        [HttpPost("Game/{id}/TestShot")]
        public ActionResult TestShot(int id, CoordInfo info)
        {
            Game game = games.Find(game => game.Id == id);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            //Testing purposes
            //info.Data = "SSingleShot";
            //info.Data = "SFourShot";
            //info.Data = "CLineShot";

            ShotAbstractFactory shotFactory;
            switch (info.Data[0])
            {
                case 'C':
                    shotFactory = new CustomShotFactory();
                    break;
                case 'S':
                    shotFactory = new SquareShotFactory();
                    break;
                default:
                    return NotFound();
            }

            Shot shot = shotFactory.CreateShot(info.Data.Substring(1));

            if (game.Player1 == null || game.Player2 == null)
                return BadRequest("Both players havent registered yet");

            lock (game)
            {
                shot.shotBeh.Shoot(game.GetEnemyByIdentifier(Request.Headers["PlayerIdentifier"]).Board, info.PositionX, info.PositionY);
            }

            return StatusCode(200); //CreatedAtAction("TestShot", new { id = game.Id }, game);
        }

        [HttpPost("Game/{id}/TestMineCycle")]
        public ActionResult TestMineCycle(int id, CoordInfo info)
        {
            Game game = games.Find(game => game.Id == id);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            lock (game)
            {
                game.GetPlayerByIdentifier(Request.Headers["PlayerIdentifier"]).Board.CycleMine(info.PositionX, info.PositionY);
            }

            return StatusCode(200);
        }

        // TODO: Will be a decorator later i think?
        private ActionResult EnsureIntegrity(Game game)
        {
            if (game == null)
                return NotFound();
            if (!Request.Headers.ContainsKey("PlayerIdentifier"))
                return StatusCode(403);
            if (!game.HasPlayerWithIdentifier(Request.Headers["PlayerIdentifier"]))
                return StatusCode(403);
            return null;
        }
    } 
}
