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
        public CoordInfo Info { get; set; }
        public List<Point> Points { get; set; }
        public string PlayerId { get; set; }

        public Command(CoordInfo info, string playerId)
        {
            Info = info;
            PlayerId = playerId;
        }
        public abstract void Execute(Game game);
    }
}
