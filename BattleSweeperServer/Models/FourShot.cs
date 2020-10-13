using BattleSweeperServer.DesignPatternClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class FourShot : SquareShot
    {
        public FourShot()
        {
            this.ammoCost = 3;
            this.shotBeh = new SquareShotBehaviour(2);
        }
    }
}
