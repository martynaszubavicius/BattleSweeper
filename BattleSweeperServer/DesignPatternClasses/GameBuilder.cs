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
        private bool generateRandomBoards = false;

        public int Id { get { return this.game.Id; } }
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

            // Generate player - prototype should start here if we use it for board generation
            if (LastOpSuccessful)
            {
                player.AmmoCount = game.Settings.ShotsPerTurn;
                Board board = player.CreateBoard(game.Settings.BoardSize);

                if (generateRandomBoards)
                {
                    Random rnd = new Random();
                    MineFactory mineFactory = new MineFactory();
                    for (int i = 0; i < board.Size * board.Size; i++)
                    {
                        board.Tiles.Add(new Tile());
                        int random_nr = rnd.Next(0, 19);
                        if (random_nr == 0)
                            board.Tiles[i].Mine = mineFactory.CreateMine(0); // Simple Mine
                        if (random_nr == 1)
                            board.Tiles[i].Mine = mineFactory.CreateMine(1); // Wide Mine
                        if (random_nr == 2)
                            board.Tiles[i].Mine = mineFactory.CreateMine(2); // Fake Mine
                    }
                }
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

        public GameBuilder RandomBoardGeneration(bool value)
        {
            generateRandomBoards = value;
            return this;
        }
    }
}
