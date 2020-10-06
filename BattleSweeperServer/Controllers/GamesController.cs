using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattleSweeperServer.Models;
using System.Diagnostics;

namespace BattleSweeperServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly BattleSweeperContext _context;

        public GamesController(BattleSweeperContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGameItems()
        {
            return await _context.GameItems.ToListAsync();
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

        [HttpPost("register/{id}")]
        public async Task<ActionResult<Player>> RegisterPlayer(int id, Player player)
        {
            var game = await _context.GameItems.FindAsync(id);
            _context.Entry(game).Reference(p => p.PLayer1).Load();
            _context.Entry(game).Reference(p => p.PLayer2).Load();


            if (game == null)
            {
                return NotFound();
            }
            if (game.RegisterPlayer(player))
            {
                player.CreateIdentifier();
                _context.PlayerItems.Add(player);
                await _context.SaveChangesAsync();

                _context.Entry(game).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest(new { error = "game full" });
            }
            
            
            

            return player;
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, Game game)
        //{
        //    if (id != game.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(game).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}





        //// PUT: api/Games/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, Game game)
        //{
        //    if (id != game.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(game).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Games
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.GameItems.Add(game);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        //// DELETE: api/Games/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Game>> DeleteGame(int id)
        //{
        //    var game = await _context.GameItems.FindAsync(id);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.GameItems.Remove(game);
        //    await _context.SaveChangesAsync();

        //    return game;
        //}

        private bool GameExists(int id)
        {
            return _context.GameItems.Any(e => e.Id == id);
        }
    }
}
