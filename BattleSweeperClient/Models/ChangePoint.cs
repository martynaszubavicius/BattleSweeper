using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.Models
{
    public class ChangePoint
    {
        [JsonProperty("X")]
        public int X { get; set; }

        [JsonProperty("Y")]
        public int Y { get; set; }

    }
}
