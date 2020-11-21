using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class GameState : TemplateGameState
    {
        public Observer HistoryObserver { get; set; }

        public GameState(Observer observer)
        {
            this.HistoryObserver = observer;
        }

        // This is a template method
        public override sealed void ExecuteCommand(Game game, Command command)
        {
            if (CanExecuteCommand(game, command) && !(command is EndTurnCommand))
            {
                command.Execute(game);
                this.HistoryObserver.Add(command);
            }
            TransitionStates(game, command);
        }

        protected abstract bool CanExecuteCommand(Game game, Command command);
        protected abstract void TransitionStates(Game game, Command command);
    }
}
