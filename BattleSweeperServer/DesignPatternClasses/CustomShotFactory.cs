using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class CustomShotFactory : ShotAbstractFactory
    {
        public override Shot CreateShot(string shotType)
        {
            switch(shotType)
            {
                case "LineShot":
                    return new LineShot();
                case "ScatterShot":
                    return new ScatterShot();
                default :
                    return null;
            }
        }
    }
}
