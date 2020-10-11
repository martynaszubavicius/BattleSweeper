using Newtonsoft.Json;

namespace BattleSweeperServer.Models
{
    public class Shot
    {
        [JsonProperty("positionX")]
        public int positionX { get; set; }

        [JsonProperty("positionY")]
        public int positionY { get; set; }

        public Shot()
        {

        }

        public Shot(int x, int y)
        {
            positionX = x;
            positionY = y;
        }
    }
}
