using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class MineCommand : Command
    {

        public MineCommand(CoordInfo info, string playerId) : base(info, playerId)
        {

        }

        public override void Execute(Game game)
        {
            Points = new List<Point>();
            lock (game)
            {
                Points.Add(game.GetPlayerByIdentifier(PlayerId).Board.CycleMine(Info.PositionX, Info.PositionY));
            }
        }
    }
}
