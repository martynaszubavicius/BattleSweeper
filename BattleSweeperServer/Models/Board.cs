using System.Collections.Generic;

namespace BattleSweeperServer.Models
{
    public class Board
    {
        public int Size { get; set; }
        public List<Tile> Tiles { get; set; }

        public Board()
        {

        }

        public Board(int size)
        {
            this.Size = size;
            this.Tiles = new List<Tile>();

            for (int i = 0; i < this.Size * this.Size; i++)
                Tiles.Add(new Tile(0));
        }

        public int GetIndex(int x, int y)
        {
            return y * this.Size + x;
        }
    }
}
