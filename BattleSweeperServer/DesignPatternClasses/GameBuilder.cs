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

        public int Id { get { return this.game.Id; } }
        public string Key { get { return this.game.Key; } }

        public bool LastOpSuccessful { get; private set; }

        public GameBuilder(int id) // TODO: different solution for Id - repeating id's since games list doesnt get stuff added to it. Use gameBuilders id and switch on finalize
        {
            game = new Game() {Id = id };
        }

        public GameBuilder SetSettings(GameSettings settings)
        {
            game.Settings = settings;
            LastOpSuccessful = true;
            return this;
        }

        public GameBuilder RegisterPlayer(Player player)
        {
            if (game.Player1 == null)
            {
                player.CreateIdentifier(1);
                game.Player1 = player;
                LastOpSuccessful = true;
            }
            else if (game.Player2 == null)
            {
                player.CreateIdentifier(2);
                game.Player2 = player;
                LastOpSuccessful = true;
            }
            else
            {
                LastOpSuccessful = false;
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
    }
}
