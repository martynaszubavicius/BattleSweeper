using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperClient.Models
{
    public class GameSettings
    {
        [JsonProperty("Id")]
        public int Id { get; set; }


        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("BoardSize")]
        public int BoardSize { get; set; }

        [JsonProperty("ShotsPerTurn")]
        public int ShotsPerTurn { get; set; }


        [JsonProperty("SimpleMineCount")]
        public int SimpleMineCount { get; set; }


        [JsonProperty("WideMineCount")]
        public int WideMineCount { get; set; }

        [JsonProperty("FakeMineCount")]
        public int FakeMineCount { get; set; }

        [JsonIgnore]
        public string DisplayName { get { return String.Format("{0} ({1}x{2})", Title, BoardSize, BoardSize); } }

        public GameSettings()
        {

        }
    }
}
