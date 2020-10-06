using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class BattleSweeperContext : DbContext
    {
        public BattleSweeperContext(DbContextOptions<BattleSweeperContext> options)
            : base(options)
        {

        }

        public DbSet<Chat> ChatItems { get; set; }
        public DbSet<Game> GameItems { get; set; }
        public DbSet<Player> PlayerItems { get; set; }

    }
}
