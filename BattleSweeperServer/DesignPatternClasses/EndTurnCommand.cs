using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class EndTurnCommand : Command
    {
        public EndTurnCommand(CoordInfo info, string playerId) : base(info, playerId)
        {

        }

        public override void Execute(Game game)
        {
            // execute does not do anything here - that is handled by state
        }

        public override void Undo(Game game)
        {
            throw new MethodAccessException("This command cannot and should not be undone");
        }
    }
}
