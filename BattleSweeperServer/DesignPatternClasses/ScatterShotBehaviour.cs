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
        //TODO: NineShot shot needs to be in the center 

        //TODO: Check if it's still within bounds!!!!!!
        public override void Shoot(Board board, int x, int y) // x y 
        {
            //for (int i = x; i < x + width; i++)
             //   for (int j = y; j < y + width; j++)
              //      board.RevealTile(i, j);
            //TODO: square shoot implementation

        }
    }
}
