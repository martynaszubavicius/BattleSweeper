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
        private static List<GameBuilder> gameBuilders = new List<GameBuilder>();
        private static List<Message> messages = new List<Message>();
        private static FlyweightFactory flyweightFactory = new FlyweightFactory();

        public BattleSweeperController()
        {
            if (gameSettings.Count > 0)
                return;

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
        // GET  GameSettings - get all allowed game settings
        // POST GetNewGameFromSettings/{id} - create a new game from a settings id
        // POST Game/{key}/RegisterPlayer - register player to a game with id
        // 
        // Requests below require "PlayerIdentifier" Header
        // 
        // POST Game/{id}/ExecuteCommand - post a command to cycle mine or shoot a shot
        // GET  Game/{id}/State - get current game state
        // GET  Game/{id}/State/{stateNr} - get current game state and commands since state nr with their changes
        // GET  Game/{id}/Settings - get the settings for the current game

        [HttpGet("GameSettings")]
        public ActionResult<IEnumerable<GameSettings>> GetGameSettings()
        {
            return gameSettings;
        }

        [HttpGet("GetNewGameFromSettings/{id}")]
        public ActionResult<string> CreateGameFromSettings(int Id, bool debug = false)
        {
            GameSettings settings = gameSettings.Find(st => st.Id == Id);
            if (settings == null)
                return NotFound();

            GameBuilder builder;

            lock (gameBuilders)
            {
                builder = new GameBuilder(gameBuilders.Count)
                    .SetSettings(settings);

                if (debug) builder = builder.SetDebugMode();
                gameBuilders.Add(builder);
            }

            return builder.Key;
        }

        [HttpGet("GetNewGameFromSettings/{id}/Debug")]
        public ActionResult<string> CreateGameFromSettings(int Id)
        {
            return CreateGameFromSettings(Id, true);
        }

        [HttpPost("Game/{key}/RegisterPlayer")]
        public ActionResult<Player> RegisterPlayer(string key, Player player1, bool randomBoard = false)
        {
            //TODO: Fix replace player with just playername 
            Player player = flyweightFactory.GetPlayer(player1.Name, 
                Request.Headers.ContainsKey("PlayerIdentifier") ? Request.Headers["PlayerIdentifier"].ToString() : "");
            
            if (player == null)
                return BadRequest("Name is taken. Go away thief");

            //Check
            Game OldGame = games.Find(game => game.Key == key);

            if(OldGame != null)
            {
                if (!Request.Headers.ContainsKey("PlayerIdentifier"))
                    return StatusCode(403);
                if (!OldGame.HasPlayerWithIdentifier(Request.Headers["PlayerIdentifier"]))
                    return StatusCode(403);

                OldGame.RebuildGame(player1);
                return player1 = OldGame.Player1;

            }
            //--------------------------------------------------------------

            GameBuilder builder = gameBuilders.Find(b => b.Key == key);

            if (builder == null)
                return NotFound();

            lock (gameBuilders)
            {
                builder.RegisterPlayer(player, randomBoard);
                if (!builder.LastOpSuccessful)
                {
                    return BadRequest("game full");
                }
                    
                lock (games)
                {
                    Game game = builder.Finalize(games.Count);
                    if (game != null)
                        games.Add(game);
                }
            }

            return player;
        }

        [HttpPost("Game/{key}/RegisterPlayerWithBoard")]
        public ActionResult<Player> RegisterPlayer(string key, Player player)
        {
            return RegisterPlayer(key, player, true);
        }

        [HttpGet("Game/{key}/Settings")]
        public ActionResult<GameSettings> GetGameSettings(string key)
        {
            Game game = games.Find(game => game.Key == key);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            return game.Settings;
        }

        [HttpGet("Game/{key}/State")]
        public ActionResult<Game> GetGameState(string key)
        {
            Game game = games.Find(game => game.Key == key);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            return game.GetPlayerView(Request.Headers["PlayerIdentifier"]);
        }

        [HttpGet("Game/{key}/State/{stateNr}")]
        public ActionResult<Game> GetChangedGameState(string key, int stateNr)
        {
            Game game = games.Find(game => game.Key == key);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            return game.GetPlayerView(Request.Headers["PlayerIdentifier"], stateNr);
        }

        // TODO: POST with no info?
        [HttpPost("Game/{key}/UndoLastCommand")]
        public ActionResult UndoLastCommand(string key)
        {
            Game game = games.Find(game => game.Key == key);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            if (game.Player1 == null || game.Player2 == null)
                return BadRequest("Game has not started yet");

            if (!(game.State is GameStateDebug))
                return BadRequest("Game is not in debug mode");

            lock (games)
                game.UndoLastPlayerCommand(Request.Headers["PlayerIdentifier"]);

            return StatusCode(200);
        }

        [HttpGet("Game/{key}/ExportLog/{format}")]
        public ActionResult<string> ExportLog(string key, string format)
        {
            Game game = games.Find(game => game.Key == key);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            if (game.Player1 == null || game.Player2 == null)
                return BadRequest("Game has not started yet");

            return game.GetCommandsLog(format);
        }

        [HttpPost("CreateMessage")]
        public ActionResult SaveMessage(Message message)
        {
            messages.Add(message);

            return StatusCode(200);
        }

        [HttpGet("GetMessages")]
        public ActionResult<List<Message>> GetMessages()
        {
            return messages;
        }

        [HttpPost("Game/{key}/ExecuteCommand")]
        public ActionResult ExecuteCommand(string key, CoordInfo info)
        {
            Game game = games.Find(game => game.Key == key);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            if (game.Player1 == null || game.Player2 == null)
                return BadRequest("Both players haven't registered yet");

            Command cmd = null;

            switch (info.CommandType)
            {
                case "mine":
                    cmd = new MineCommand(info, Request.Headers["PlayerIdentifier"]);
                    break;
                case "shot":
                    cmd = new ShotCommand(info, Request.Headers["PlayerIdentifier"]);
                    break;
                case "endTurn":
                    cmd = new EndTurnCommand(info, Request.Headers["PlayerIdentifier"]);
                    break;
                default:
                    return NotFound();
            }

            lock (games)
                game.AddExecuteCommand(cmd);

            return StatusCode(200);
        }

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
