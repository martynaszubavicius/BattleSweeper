using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class GameStateFinished : GameState
    {
        public GameStateFinished(Observer observer) : base(observer)
        { }

        protected override bool CanExecuteCommand(Game game, Command command)
        {
            return false;
        }

        protected override void TransitionStates(Game game, Command command)
        {
            // nothing here
        }
    }
}
