using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class FinalHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            return true;
        }
    }
}
