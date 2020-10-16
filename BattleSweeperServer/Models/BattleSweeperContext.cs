//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Proxies;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BattleSweeperServer.Models
//{
//    public class BattleSweeperContext : DbContext
//    {
//        public BattleSweeperContext(DbContextOptions<BattleSweeperContext> options)
//            : base(options)
//        {
            
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseLazyLoadingProxies();
//        }
        
//        public DbSet<Chat> ChatItems { get; set; }
//        //public DbSet<Game> GameItems { get; set; }
//        //public DbSet<Player> PlayerItems { get; set; }
//        //public DbSet<Board> BoardItems { get; set; }
//        //public DbSet<Tile> TileItems { get; set; }
//        //public DbSet<Mine> MineItems { get; set; }
//    }
//}
