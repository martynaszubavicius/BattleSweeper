using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class Tile
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Revealed { get; set; }
        public virtual Mine Mine { get; set; }

        public Tile()
        {

        }

        public Tile(int x, int y, bool revealed)
        {
            this.X = x;
            this.Y = y;
            this.Revealed = revealed;
        }

        public Mine SetMine()
        {
            this.Mine = new Mine();
            return this.Mine;
        }
    }
}
