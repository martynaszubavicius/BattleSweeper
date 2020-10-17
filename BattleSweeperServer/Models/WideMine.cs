using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class WideMine : Mine
    {
        public WideMine()
        {
            ImageName = "red_bomb";
        }

        public override void OnReveal(Board board, int x, int y)
        {
            int radius = 2;

            for (int i = x - radius + 1; i < x + radius; i++)
                for (int j = y - radius + 1; j < y + radius; j++)
                    if (board.WithinBounds(i, j))
                        board.RevealTile(i, j);
        }
    }
}