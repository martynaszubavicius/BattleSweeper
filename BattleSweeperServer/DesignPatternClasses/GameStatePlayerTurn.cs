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
            if (player(game).Identifier != command.PlayerId)
                return false;

            if (player(game).AmmoCount < (command as ShotCommand).shot.ammoCost)
                return false;
            
            return true;
        }

        protected override void TransitionStates(Game game, Command command)
        {
            if ( enemy(game).Board.CountAllMines(false, false) == 0)
            {
                player(game).AmmoCount = 999;
                player(game).InGame = false;
                enemy(game).AmmoCount = 0;
                enemy(game).InGame = false;
                
                game.State = new GameStateFinished(HistoryObserver);
            }
            
            if (command is EndTurnCommand && player(game).Identifier == command.PlayerId)
            {
                enemy(game).AmmoCount += game.Settings.ShotsPerTurn;
                player1 = !player1;
            }
        }

        private Player player(Game game) { return player1 ? game.Player1 : game.Player2; }
        private Player enemy(Game game) { return player1 ? game.Player2 : game.Player1; }
    }
}
