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
        // TODO: This is never serialised i think? remove the useless json tags

        [JsonProperty("Info")]
        public CoordInfo Info { get; set; }

        [JsonProperty("Point")]
        public ChangePoint Point { get; set; }

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
        public abstract string Accept(LogVisitor v);

        public string GetPlayerName()
        {
            int idLength = 17;
            return this.PlayerId.Substring(idLength);
        }
    }
}
