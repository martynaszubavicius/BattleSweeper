using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class FakeCommand : Command
    {
        public FakeCommand(CoordInfo info, ChangePoint changePoint) : base(info, "")
        {
            this.Undone = true;
            Point = changePoint;
        }

        public override void Execute(Game game)
        {

        }

        public override void Undo(Game game)
        {
           
        }

        public override string Accept(LogVisitor v)
        {
            throw new NotImplementedException();
        }
    }
}
