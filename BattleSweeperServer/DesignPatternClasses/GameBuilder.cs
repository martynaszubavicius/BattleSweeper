using BattleSweeperServer.Models;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class GameBuilder
    {
        private Game game;

        public string Key { get { return this.game.Key; } }

        public bool LastOpSuccessful { get; private set; }

        public GameBuilder(int id)
        {
            game = new Game() {Id = id };
        }

        public GameBuilder SetSettings(GameSettings settings)
        {
            game.Settings = settings;
            LastOpSuccessful = true;
            return this;
        }

        public GameBuilder RegisterPlayer(Player player, bool randomBoard = false)
        {
            if (game.Player1 == null)
            {
                player.CreateIdentifier(1);
                game.Player1 = player;
                LastOpSuccessful = true;
                player.InGame = true;
            }
            else if (game.Player2 == null)
            {
                player.CreateIdentifier(2);
                game.Player2 = player;
                LastOpSuccessful = true;
                player.InGame = true;
            }
            else
            {
                LastOpSuccessful = false;
            }

            // Generate player - prototype should start here if we use it for board generation
            if (LastOpSuccessful)
            {
                player.AmmoCount = game.Settings.ShotsPerTurn;

                if (randomBoard)
                    player.CreateRandomBoard(game.Settings);
                else
                    player.CreateBoard(game.Settings);
            }

            return this;
        }

        public Game Finalize(int gameId)
        {
            if (game.Settings != null && game.Player1 != null && game.Player2 != null)
            {
                this.game.Id = gameId;
                LastOpSuccessful = true;
                return this.game;
            }
            else
            {
                LastOpSuccessful = false;
                return null;
            }
        }

        public GameBuilder SetDebugMode()
        {
            game.State = new GameStateDebug(game.State.HistoryObserver);
            LastOpSuccessful = true;
            return this;
        }
    }
}
