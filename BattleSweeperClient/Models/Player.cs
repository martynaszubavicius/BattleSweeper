using Newtonsoft.Json;
using System;

namespace BattleSweeperClient.Models
{
    public class Player
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Identifier")]
        public string Identifier { get; set; }

        [JsonProperty("AmmoCount")]
        public int AmmoCount { get; set; }

        [JsonProperty("Board")]
        public Board Board { get; set; }

        public Player()
        {
            
        }
    }
}
