using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class GameNullHandler : AbstractHandler
    {
        public override object Handle(object request, string text)
        {
            if ((request as Game) != null)
            {
                text += " GameNull";
                return base.Handle(request, text);
            }
            else
            {
                return null;
            }
        }
    }
}
