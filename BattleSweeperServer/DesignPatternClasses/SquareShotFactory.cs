using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class SquareShotFactory : ShotAbstractFactory
    {
        public override Shot CreateShot(string shotType)
        {
            switch (shotType)
            {
                case "SingleShot":
                    return new SingleShot();
                case "FourShot":
                    return new FourShot();
                case "NineShot":
                    return new NineShot();
                default:
                    return null;
            }
        }
    }
}
