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
            this.State = -1;
        }

        public Tile(int state)
        {
            this.State = state;
        }

        // Prototype pattern: shallow and deep copy of tile
        public Tile ShallowCopy()
        {
            return (Tile)this.MemberwiseClone();
        }

        public Tile DeepCopy()
        {
            Tile clone = (Tile)this.MemberwiseClone();
            clone.State = State;
            clone.Mine = Mine;
            return clone;
        }

    
        public override string ToString()
        {
            return string.Format("State: {0}, Mine: {1}", State, Mine == null ? 0 : 1);
        }
    }
}
