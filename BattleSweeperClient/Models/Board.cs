using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BattleSweeperClient.Models
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


        public int CountAllMines(bool countRevealed, bool countFake)
        {
            IEnumerable<Tile> result = Tiles.Where(x => x.Mine != null);

            if (!countRevealed)
                result = result.Where(x => x.State == -1);

            if (!countFake)
                result = result.Where(x => x.Mine.ImageName != "gray_bomb");


            return result.Count();
        }

        public int CountMineType<T>(bool countRevealed = true) where T : Mine
        {
            IEnumerable<Tile> result = Tiles.Where(x => x.Mine is T);

            if (!countRevealed)
                result = result.Where(x => x.State == -1);

            return result.Count();
        }

    }
}
