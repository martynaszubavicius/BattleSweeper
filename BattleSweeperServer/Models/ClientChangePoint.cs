using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class ClientChangePoint
    {
        [JsonProperty("X")]
        public int X { get; set; }

        [JsonProperty("Y")]
        public int Y { get; set; }

        public ClientChangePoint() { }

        public ClientChangePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
