using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class GameStateNew : GameState
    {
        private bool p1Done = false;
        private bool p2Done = false;

        public GameStateNew() : base(new Observer())
        { }

        protected override bool CanExecuteCommand(Game game, Command command)
        {
            if (!(command is MineCommand))
                return false;

            if ((!p1Done && game.Player1.Identifier == command.PlayerId) || (!p2Done && game.Player2.Identifier == command.PlayerId))
                return true;
            else
                return false;
        }

        protected override void TransitionStates(Game game, Command command)
        {
            if (command is EndTurnCommand)
            {
                if (game.Player1.Identifier == command.PlayerId) p1Done = true;
                if (game.Player2.Identifier == command.PlayerId) p2Done = true;
            }

            if (p1Done && p2Done)
            {
                game.State = new GameStatePlayerTurn(HistoryObserver);
                game.Player1.AmmoCount += game.Settings.ShotsPerTurn;
            }
                
        }
    }
}
