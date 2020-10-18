using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSweeperServer.DesignPatternClasses;

namespace BattleSweeperServer.Models
{
    public class TileUpdateHistory
    {
        public int Id { get; set; }
        public List<CoordInfo> History { get; }

        public TileUpdateHistory()
        {
            History = new List<CoordInfo>();
        }

        public void SaveChanges(CoordInfo command)
        {
            History.Add(command);
            Id = History.Count;
        }
    }
}
