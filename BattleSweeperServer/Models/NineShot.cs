﻿using BattleSweeperServer.DesignPatternClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class NineShot : SquareShot
    {
        public NineShot()
        {
            this.ammoCost = 7;
            this.shotBeh = new SquareShotBehaviour(3);
        }
    }
}
