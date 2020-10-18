using BattleSweeperClient.DesignPatternClasses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

namespace BattleSweeperClient.Models
{
    public class Tile : DecoratedTile
    {
        [JsonProperty("State")]
        public int State { get; set; } // -1 unrevealed, 0 empty or bomb, >0 nearby mine count

        [JsonProperty("Mine")]
        public Mine Mine { get; set; }

        public Tile()
        {

        }

        public override string ToString()
        {
            return string.Format("State: {0}, Mine: {1}", State, Mine == null ? 0 : 1);
        }

        public override Image GetImage(Dictionary<string, Image> textures)
        {
            return textures["tile"];
        }
    }
}
