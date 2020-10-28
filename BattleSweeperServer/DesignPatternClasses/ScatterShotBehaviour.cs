using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class ScatterShotBehaviour : ShotBehaviour
    {
        private bool reveal = true;
        public override List<ChangePoint> Shoot(Board board, int x, int y) // x y 
        {
            List<ChangePoint> points = new List<ChangePoint>();
            for (int i = x - 2; i < x + 3; i++)
            {
                for (int j = y - 2; j < y + 3; j++)
                {
                    if (board.WithinBounds(i, j) && reveal == true)
                        points = points.Concat(board.RevealTile(i, j)).ToList();
                    reveal = !reveal;
                }
            }
            return points;
        }
    }
}
