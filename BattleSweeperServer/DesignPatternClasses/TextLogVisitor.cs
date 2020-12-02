using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class TextLogVisitor : LogVisitor
    {
        public override string VisitEndTurn(EndTurnCommand command)
        {
            return "Player "+ command.GetPlayerName() + " completed their turn.\n";
        }

        public override string VisitMine(MineCommand command)
        {
            return "Player " + command.GetPlayerName() + " set a mine at " + command.Point + ".\n";
        }

        public override string VisitShot(ShotCommand command)
        {
            string shotType = "";
            switch (command.Info.Data)
            {
                case "SSingleShot":
                    shotType = "single shot";
                    break;
                case "SFourShot":
                    shotType = "four shot";
                    break;
                case "SNineShot":
                    shotType = "nine shot";
                    break;
                case "CLineShot":
                    shotType = "line shot";
                    break;
                case "CScatterShot":
                    shotType = "scatter shot";
                    break;
                default:
                    break;
            }
            return "Player " + command.GetPlayerName() + " shot " + shotType + " at " + command.Point + ".\n";
        }
    }
}
