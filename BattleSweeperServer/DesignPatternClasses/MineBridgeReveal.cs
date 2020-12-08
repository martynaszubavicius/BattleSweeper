﻿using System;
using System.Collections.Generic;
using System.Linq;
using BattleSweeperServer.Models;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class MineBridgeReveal
    {
        public abstract ChangePoint OnReveal(Board board, int x, int y);

        public override bool Equals(object obj)
        {
            MineBridgeReveal other = obj as MineBridgeReveal;
            if (other == null)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
