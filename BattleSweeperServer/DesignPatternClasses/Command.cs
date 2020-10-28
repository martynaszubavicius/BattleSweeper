using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using BattleSweeperServer.Models;
using Newtonsoft.Json;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class Command
    {
        [JsonProperty("Info")]
        public CoordInfo Info { get; set; }

        [JsonProperty("Points")]
        public List<ChangePoint> Points { get; set; }

        [JsonProperty("PlayerId")]
        public string PlayerId { get; set; }

        public bool Undone { get; protected set; }

        public Command(CoordInfo info, string playerId)
        {
            Info = info;
            PlayerId = playerId;
            Undone = false;
        }
        public abstract void Execute(Game game);
        public abstract void Undo(Game game);
    }
}
