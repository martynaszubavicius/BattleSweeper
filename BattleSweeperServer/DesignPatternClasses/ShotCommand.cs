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

        public ShotCommand(CoordInfo info, string playerId) : base(info, playerId)
        {
        }
        public override void Execute(Game game)
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

            Shot shot = shotFactory.CreateShot(Info.Data.Substring(1));

            lock (game)
            {
                Point = shot.shotBeh.Shoot(game.GetEnemyByIdentifier(PlayerId).Board, Info.PositionX, Info.PositionY);
            }
        }

        public override void Undo(Game game)
        {
            this.Undone = true;
            Board board = game.GetEnemyByIdentifier(PlayerId).Board;
            foreach (ChangePoint pnt in Point)
                board.Tiles[board.GetIndex(pnt.X, pnt.Y)].State = -1;  
        }
    }
}
