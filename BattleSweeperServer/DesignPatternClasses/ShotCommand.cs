using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Points = shot.shotBeh.Shoot(game.GetEnemyByIdentifier(PlayerId).Board, Info.PositionX, Info.PositionY);
            }
        }
    }
}
