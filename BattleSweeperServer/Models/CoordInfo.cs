using Newtonsoft.Json;

namespace BattleSweeperServer.Models
{
    public class CoordInfo
    {
        [JsonProperty("positionX")]
        public int PositionX { get; set; }

        [JsonProperty("positionY")]
        public int PositionY { get; set; }
        
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("commandType")]
        public string CommandType { get; set; }

        public CoordInfo()
        {

        }
    }
}
