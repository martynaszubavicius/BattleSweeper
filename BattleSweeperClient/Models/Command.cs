using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.Models
{
    public class Command
    {
        [JsonProperty("Info")]
        public CoordInfo Info { get; set; }

        [JsonProperty("Points")]
        public List<Point> Points { get; set; }

        [JsonProperty("PlayerId")]
        public string PlayerId { get; set; }

        public Command()
        {

        }
    }
