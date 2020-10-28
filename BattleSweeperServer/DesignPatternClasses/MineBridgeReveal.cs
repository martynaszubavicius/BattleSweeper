using System;
using System.Collections.Generic;
using System.Linq;
using BattleSweeperServer.Models;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class MineBridgeReveal
    {
        public abstract List<ChangePoint> OnReveal(Board board, int x, int y);
    }
}
