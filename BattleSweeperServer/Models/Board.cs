using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BattleSweeperServer.Models
{
    public class Board
    {
        [JsonProperty("Size")]
        public int Size { get; set; }

        [JsonProperty("Tiles")]
        public List<Tile> Tiles { get; set; }

        public Board()
        {

        }

        public Board(int size)
        {
            this.Size = size;
            this.Tiles = new List<Tile>();

            for (int i = 0; i < this.Size * this.Size; i++)
                Tiles.Add(new Tile(-1));
        }

        public int GetIndex(int x, int y)
        {
            return y * this.Size + x;
        }
    }
}
