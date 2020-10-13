using BattleSweeperServer.DesignPatternClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class LineShot : CustomShot
    {
        public LineShot()
        {
            this.ammoCost = 4;
            this.shotBeh = new LineShotBehaviour(5);
        }
    }
}
