using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class SingleShot : SquareShot
    {
        public SingleShot()
        {
            this.ammoCost = 1;
        }
    }
}
