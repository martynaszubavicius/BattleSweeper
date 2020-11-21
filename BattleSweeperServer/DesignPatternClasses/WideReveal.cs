using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class WideReveal : MineBridgeReveal
    {
        public override ChangePoint OnReveal(Board board, int x, int y)
        {
            ChangePoint point = new ChangePointComposite(x, y);

            int currIndex = board.GetIndex(x, y);
            int radius = 2;

            for (int i = x - radius + 1; i < x + radius; i++)
                for (int j = y - radius + 1; j < y + radius; j++)
                    if (board.WithinBounds(i, j) && currIndex != board.GetIndex(i, j))
                        point.Add(board.RevealTile(i, j));

            return point;
        }
    }
}
