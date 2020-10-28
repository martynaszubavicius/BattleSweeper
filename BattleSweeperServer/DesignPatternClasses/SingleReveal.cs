using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class SingleReveal : MineBridgeReveal
    {
        public override List<ChangePoint> OnReveal(Board board, int x, int y)
        {
            List<ChangePoint> points = new List<ChangePoint>();
            points.Add(new ChangePoint(x, y));
            return points;
        }
    }
}
