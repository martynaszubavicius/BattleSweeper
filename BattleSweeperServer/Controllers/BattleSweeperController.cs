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
        // GET Game/{id}/State - get current game state
        // 

        [HttpGet("Game/{id}/State")]
        public ActionResult<Game> GetGameState(int id)
        {
            var req = Request;

            if (!req.Headers.ContainsKey("PlayerIdentifier"))
                return StatusCode(403);

            Game game = games.Find(game => game.Id == id);

            // TODO: limit game information based on player requesting it, do it through headers

            if (game == null)
                return NotFound();
            else
                return game;
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
                Board board = player.CreateBoard(game.BoardSize);
            }
            else
            {
                return BadRequest(new { error = "game full" });
            }

            return player;
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
