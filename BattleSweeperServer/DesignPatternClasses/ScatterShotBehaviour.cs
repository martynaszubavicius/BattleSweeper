using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class ScatterShotBehaviour : ShotBehaviour
    {
        private bool reveal = true;
        //TODO: Check if it's still within bounds!!!!!!
        public override void Shoot(Board board, int x, int y) // x y 
        {
            for (int i = x - 2; i < x + 3; i++)
            {
                for (int j = y - 2; j < y + 3; j++)
                {
                    if (board.WithinBounds(i, j) && reveal == true)
                    {
                        board.RevealTile(i, j);
                        reveal = false;
                    }
                    else
                    {
                        reveal = true;
                    }
                }
            }
            //TODO: square shoot implementation

        }
    }
}
