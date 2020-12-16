using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class RegisteredPlayerHandler : AbstractHandler
    {
        public override object Handle(object request, string text)
        {
            if ((request as Game).Player1 == null || (request as Game).Player2 == null)
            {
                return null;
            }
            else
            {
                text += " RegisteredPlayer";
                return base.Handle(request, text);
            }
        }
    }
}
