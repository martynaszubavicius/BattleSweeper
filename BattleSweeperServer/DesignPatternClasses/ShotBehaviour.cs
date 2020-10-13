using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class ShotBehaviour
    {
        public abstract void Shoot(Board board, int x, int y);
    }
}
