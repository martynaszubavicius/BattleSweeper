using Newtonsoft.Json;
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

        public int GetIndex(int x, int y)
        {
            return y * this.Size + x;
        }
    }
}
