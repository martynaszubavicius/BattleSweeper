using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class LineShotBehaviour : ShotBehaviour
    {
        private int width;

        public LineShotBehaviour(int width)
        {
            this.width = width;
        }


        public override void Shoot(Board board, int x, int y) // x y 
        {
            for (int i = x-(width/2); i < x + width - width / 2; i++)
            {
                if (board.WithinBounds(i, y))
                    board.RevealTile(i, y);
            }
        }
    }
}
