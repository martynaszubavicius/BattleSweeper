using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class SquareShotBehaviour : ShotBehaviour
    {
        private int width;

        public SquareShotBehaviour(int width)
        {
            this.width = width;
        }

        //TODO: NineShot shot needs to be in the center 
        public override void Shoot(Board board, int x, int y) // x y 
        {
            for (int i = x; i < x + width; i++)
                for (int j = y; j < y + width; j++)
                    if(board.WithinBounds(i, j))
                        board.RevealTile(i, j);

        }
    }
}
