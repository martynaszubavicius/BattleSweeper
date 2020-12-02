using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class ShotCommand : Command
    {
        public Shot shot { get; private set; }

        public ShotCommand(CoordInfo info, string playerId) : base(info, playerId)
        {
            ShotAbstractFactory shotFactory;
            switch (Info.Data[0])
            {
                case 'C':
                    shotFactory = new CustomShotFactory();
                    break;
                case 'S':
                    shotFactory = new SquareShotFactory();
                    break;
                default:
                    return;
            }

            shot = shotFactory.CreateShot(Info.Data.Substring(1));
        }
        public override void Execute(Game game)
        {
            Point = shot.shotBeh.Shoot(game.GetEnemyByIdentifier(PlayerId).Board, Info.PositionX, Info.PositionY);
            if (!(game.State is GameStateDebug))
                game.GetPlayerByIdentifier(PlayerId).AmmoCount -= shot.ammoCost;
        }

        public override void Undo(Game game)
        {
            this.Undone = true;
            Board board = game.GetEnemyByIdentifier(PlayerId).Board;
            foreach (ChangePoint pnt in Point)
                board.Tiles[board.GetIndex(pnt.X, pnt.Y)].State = -1;  
        }

        public override string Accept(LogVisitor v)
        {
            return v.VisitShot(this);
        }
    }
}
