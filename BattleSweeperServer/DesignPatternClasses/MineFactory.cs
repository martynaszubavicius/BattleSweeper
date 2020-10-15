using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class MineFactory
    {
        public Mine CreateMine(int mineType)
        {
            switch (mineType)
            {
                case 0:
                    return new SimpleMine();
                case 1:
                    return new WideMine();
                case 2:
                    return new FakeMine();
                default:
                    return null;
            }
        }
    }
}
