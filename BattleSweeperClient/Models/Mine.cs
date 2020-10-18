using Newtonsoft.Json;

namespace BattleSweeperClient.Models
{
    public class Mine
    {
        [JsonProperty("ImageName")]
        public string ImageName { get; set; }

        public Mine()
        {

        }
    }
}
