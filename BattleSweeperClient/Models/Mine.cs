﻿using Newtonsoft.Json;

namespace BattleSweeperServer.Models
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
