using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class GameStateDebug : GameState
    {
        public GameStateDebug(Observer observer) : base (observer)
        { }

        protected override bool CanExecuteCommand(Game game, Command command)
        {
            return true;
        }

        protected override void TransitionStates(Game game, Command command)
        {
            // nothing here - but maybe it can go to finished state via shortcut to make testing easier?
        }
    }
}
