﻿using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class WideReveal : MineBridgeReveal
    {
        public override List<ChangePoint> OnReveal(Board board, int x, int y)
        {
            List<ChangePoint> points = new List<ChangePoint>();
            int radius = 2;
            points.Add(new ChangePoint(x, y));

            for (int i = x - radius + 1; i < x + radius; i++)
                for (int j = y - radius + 1; j < y + radius; j++)
                    if (board.WithinBounds(i, j))
                        points = points.Concat(board.RevealTile(i, j)).ToList();
            return points;
        }
    }
}
