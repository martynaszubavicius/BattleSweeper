using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class ShotAbstractFactory
    {
        public abstract Shot CreateShot(string shotType);
    }
}
