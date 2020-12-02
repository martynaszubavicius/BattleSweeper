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
            Point = game.GetPlayerByIdentifier(PlayerId).Board.CycleMine(Info.PositionX, Info.PositionY, game.Settings);
        }

        public override void Undo(Game game)
        {
            this.Undone = true;
            Point = game.GetPlayerByIdentifier(PlayerId).Board.CycleMine(Info.PositionX, Info.PositionY, game.Settings, true);
        }

        public override string Accept(LogVisitor v)
        {
            return v.VisitMine(this);
        }
    }
}
