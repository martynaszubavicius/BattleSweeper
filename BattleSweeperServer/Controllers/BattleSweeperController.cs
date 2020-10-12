using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSweeperServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleSweeperServer.Controllers
{
    // TODO: issiaiskint del inheritance ir POST/GET su skirtingais tipais

    [Route("BattleSweeper")]
    [ApiController]
    public class BattleSweeperController : ControllerBase
    {
        private static List<Game> games = new List<Game>();

        public BattleSweeperController()
        {

        }

        // https://localhost:44337/BattleSweeper
        //
        // POST CreateGame - new game data
        // POST Game/{id}/RegisterPlayer - register player to a game with id
        // 
        // Requests below require "PlayerIdentifier" Header
        // 
        // GET  Game/{id}/State - get current game state
        // POST Game/{id}/TestMineCycle - cycle a mine on players board
        // POST Game/{id}/TestShot - reveal tile on enemys board

        [HttpGet("Game/{id}/State")]
        public ActionResult<Game> GetGameState(int id)
        {
            Game game = games.Find(game => game.Id == id);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            return game.GetPlayerView(Request.Headers["PlayerIdentifier"]);
        }

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
                Board board = player.CreateBoard(game.BoardSize);
                
                // temporary for testing
                Random rnd = new Random();
                board.Tiles = new List<Tile>();
                for (int i = 0; i < board.Size * board.Size; i++)
                {
                    board.Tiles.Add(new Tile());
                    if (rnd.Next(0, 9) == 0)
                        board.Tiles[i].Mine = new Mine();
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

        [HttpPost("Game/{id}/TestShot")]
        public ActionResult TestShot(int id, Shot shot)
        {
            Game game = games.Find(game => game.Id == id);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            lock (game)
            {
                game.GetEnemyByIdentifier(Request.Headers["PlayerIdentifier"]).Board.RevealTile(shot.positionX, shot.positionY);
            }

            return StatusCode(200); //CreatedAtAction("TestShot", new { id = game.Id }, game);
        }

        [HttpPost("Game/{id}/TestMineCycle")]
        public ActionResult TestMineCycle(int id, Shot shot)
        {
            Game game = games.Find(game => game.Id == id);

            ActionResult error = EnsureIntegrity(game);
            if (error != null)
                return error;

            lock (game)
            {
                game.GetPlayerByIdentifier(Request.Headers["PlayerIdentifier"]).Board.CycleMine(shot.positionX, shot.positionY);
            }

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

    /*
    public class GamesController : ControllerBase
    {


        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGameItems()
        {
            var games = await _context.GameItems.ToListAsync();
            //foreach (Game g in games)
            //{
            //    _context.Entry(g).Reference(p => p.PLayer1).Load();
            //    _context.Entry(g).Reference(p => p.PLayer2).Load();
            //}
            return games;
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.GameItems.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }



        [HttpPost("setmine/{id}")]
        public async Task<ActionResult<Tile>> SetMine(int id, [FromBody] dynamic data)
        {
            var game = await _context.GameItems.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            if (data.X == null || data.Y == null || data.PlayerId == null)
            {
                return BadRequest(new { error = "not enough args" });
            }

            Tile tile = new Tile();
            if (data.PlayerId == game.PLayer1.Id)
            {
                tile = (Tile)game.PLayer1.Board.Tiles.Where(x => x.X == data.X && x.Y == data.Y);
            }
            else if (data.PlayerId == game.PLayer2.Id)
            {
                tile = (Tile)game.PLayer2.Board.Tiles.Where(x => x.X == data.X && x.Y == data.Y);
            }
            else
            {
                return BadRequest(new { error = "player not found" });
            }


            var mine = tile.SetMine();
            if (mine != null)
            {
                _context.MineItems.Add(mine);
                await _context.SaveChangesAsync();

                _context.Entry(game).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return tile;
            }

            return BadRequest(new { error = "failed to set mine" });
        }


        private bool GameExists(int id)
        {
            return _context.GameItems.Any(e => e.Id == id);
        }
    }*/
}
