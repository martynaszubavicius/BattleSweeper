using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class XMLLogVisitor : LogVisitor
    {
        public override string VisitEndTurn(EndTurnCommand command)
        {
            throw new NotImplementedException();
        }

        public override string VisitMine(MineCommand command)
        {
            throw new NotImplementedException();
        }

        public override string VisitShot(ShotCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
