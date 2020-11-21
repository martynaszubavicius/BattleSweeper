using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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


        public override ChangePoint Shoot(Board board, int x, int y)
        {
            ChangePoint point = new ChangePointComposite(x, y);

            for (int i = x - (width / 2); i < x + width - width / 2; i++)
            {
                if (board.WithinBounds(i, y))
                    point.Add(board.RevealTile(i, y));
            }

            return point;
        }
    }
}
