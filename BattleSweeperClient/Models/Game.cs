using Newtonsoft.Json;
using System.Collections.Generic;

namespace BattleSweeperClient.Models
{
    public class Game
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Key")]
        public string Key { get { return this.Id.ToString(); } } // TODO: use this instead of id in requests

        [JsonProperty("Player1")]
        public Player Player1 { get; private set; }

        [JsonProperty("Player2")]
        public Player Player2 { get; private set; }
        
        [JsonProperty("Settings")]
        public GameSettings Settings { get; set; }

        [JsonProperty("RedrawPoints")]
        public List<ChangePoint> RedrawPoints { get; set; }

        [JsonProperty("HistoryLastIndex")]
        public int HistoryLastIndex { get; set; }

        public Game()
        {

        }
    }
}
