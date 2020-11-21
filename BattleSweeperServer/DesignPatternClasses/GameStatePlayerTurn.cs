using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class GameStatePlayerTurn : GameState
    {
        private bool player1;

        public GameStatePlayerTurn(Observer observer, bool player1 = true) : base(observer)
        {
            this.player1 = player1;
        }

        protected override bool CanExecuteCommand(Game game, Command command)
        {
            if (!(command is ShotCommand))
                return false;
            if ((player1 ? game.Player1 : game.Player2).Identifier == command.PlayerId)
                return true;
            else
                return false;
        }

        protected override void TransitionStates(Game game, Command command)
        {
            if (false) // TODO: check if the game is finished here
                game.State = new GameStateFinished(HistoryObserver);

            if (command is EndTurnCommand && (player1 ? game.Player1 : game.Player2).Identifier == command.PlayerId)
                player1 = !player1;
        }
    }
}
