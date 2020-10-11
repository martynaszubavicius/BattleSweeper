using Newtonsoft.Json;

namespace BattleSweeperServer.Models
{
    public class Tile
    {
        [JsonProperty("State")]
        public int State { get; set; } // -1 unrevealed, 0 empty or bomb, >0 nearby mine count

        [JsonProperty("Mine")]
        public Mine Mine { get; set; }

        public Tile()
        {
            
        }

        public Tile(int state)
        {
            this.State = state;
        }
    }
}
