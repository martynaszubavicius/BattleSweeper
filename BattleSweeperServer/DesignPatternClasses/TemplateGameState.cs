using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class TemplateGameState
    {
        public abstract void ExecuteCommand(Game game, Command command);
    }
}
