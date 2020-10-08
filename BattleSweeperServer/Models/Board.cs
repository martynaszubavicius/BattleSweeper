using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleSweeperServer.Models
{
    public class Board
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public virtual List<Tile> Tiles { get; set; }

        public Board()
        {

        }

        public Board(int size)
        {
            this.Size = size;
            this.Tiles = new List<Tile>();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Tile tile = new Tile(i, j, false);
                    Tiles.Add(tile);
                }
            }
        }
    }
}
