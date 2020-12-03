using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class XMLLogVisitor : LogVisitor
    {
        public override string VisitEndTurn(EndTurnCommand command)
        {
            string xml = "<command>\n";
            xml += "  <player>" + command.GetPlayerName() + "</player>\n";
            xml += "  <type>End turn</type>\n";
            xml += "</command>\n";
            return xml;
        }

        public override string VisitMine(MineCommand command)
        {
            string xml = "<command>\n";
            xml += "  <player>" + command.GetPlayerName() + "</player>\n";
            xml += "  <type>Set mine</type>\n";
            xml += "  <x>" + command.Point.X + "</x>\n";
            xml += "  <y>" + command.Point.Y + "</y>\n";
            xml += "</command>\n";
            return xml;
        }

        public override string VisitShot(ShotCommand command)
        {
            string shotType = GetShotType(command.Info.Data);
            string xml = "<command>\n";
            xml += "  <player>" + command.GetPlayerName() + "</player>\n";
            xml += "  <type>Shoot mine</type>\n";
            xml += "  <shot>" + shotType + "</shot>\n";
            xml += "  <x>" + command.Point.X + "</x>\n";
            xml += "  <y>" + command.Point.Y + "</y>\n";
            xml += "</command>\n";
            return xml;
        }
    }
}
