using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class SingleReveal : MineBridgeReveal
    {
        public override ChangePoint OnReveal(Board board, int x, int y)
        {
            return new ChangePointLeaf(x, y);
        }
    }
}
