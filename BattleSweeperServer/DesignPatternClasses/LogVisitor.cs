using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class LogVisitor
    {
        public abstract string VisitEndTurn(EndTurnCommand command);
        public abstract string VisitMine(MineCommand command);
        public abstract string VisitShot(ShotCommand command);
    }
}
