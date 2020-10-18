using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class ChangePoint
    {
        [JsonProperty("X")]
        public int X { get; set; }

        [JsonProperty("Y")]
        public int Y { get; set; }

        public ChangePoint() { }

        public ChangePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
