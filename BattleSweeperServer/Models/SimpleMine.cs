﻿using BattleSweeperServer.DesignPatternClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class SimpleMine : Mine
    {
        public SimpleMine()
        {
            ImageName = "bomb";
            this.mineReveal = new SingleReveal();
        }
    }
}
