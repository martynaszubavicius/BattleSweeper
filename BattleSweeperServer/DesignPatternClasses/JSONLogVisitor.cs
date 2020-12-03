using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class JSONLogVisitor : LogVisitor
    {
        public override string VisitEndTurn(EndTurnCommand command)
        {
            string json = "  \"command\": {\n";
            json += "    \"player\": \"" + command.GetPlayerName() + "\",\n";
            json += "    \"type\": \"end turn\",\n";
            json += "  },\n";
            return json;
        }

        public override string VisitMine(MineCommand command)
        {
            string json = "  \"command\": {\n";
            json += "    \"player\": \"" + command.GetPlayerName() + "\",\n";
            json += "    \"type\": \"set mine\",\n";
            json += "    \"x\": " + command.Point.X + ",\n";
            json += "    \"y\": " + command.Point.Y + ",\n";
            json += "  },\n";
            return json;
        }

        public override string VisitShot(ShotCommand command)
        {
            string shotType = GetShotType(command.Info.Data);
            string json = "  \"command\": {\n";
            json += "    \"player\": \"" + command.GetPlayerName() + "\",\n";
            json += "    \"type\": \"shoot mine\",\n";
            json += "    \"shot\": \"" + shotType + "\",\n";
            json += "    \"x\": " + command.Point.X + ",\n";
            json += "    \"y\": " + command.Point.Y + ",\n";
            json += "  },\n";
            return json;
        }
    }
}
