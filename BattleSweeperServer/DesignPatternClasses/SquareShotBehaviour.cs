using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public override ChangePoint Shoot(Board board, int x, int y) // x y 
        {
            ChangePoint point = new ChangePointComposite(x, y);

            for (int i = x; i < x + width; i++)
                for (int j = y; j < y + width; j++)
                    if (board.WithinBounds(i, j))
                        point.Add(board.RevealTile(i, j));

            return point;
        }

        public override bool Equals(object obj)
        {
            SquareShotBehaviour other = obj as SquareShotBehaviour;
            if (other == null)
            {
                return false;
            }
            return (this.width == other.width);
        }

        public override int GetHashCode()
        {
            return 33 * width.GetHashCode();
        }
    }
}
