using BattleSweeperServer.DesignPatternClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BattleSweeperServer.Models
{
    public class Player
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Identifier")]
        public string Identifier { get; set; }

        [JsonProperty("AmmoCount")]
        public int AmmoCount { get; set; }

        [JsonProperty("Board")]
        public Board Board { get; set; }

        public Player()
        {
            
        }

        public string CreateIdentifier(int seed)
        {
            // TODO: implement better random identifier,  for now same for everyone, which is stupid. I am stupid
            this.Identifier = string.Format("{0}{1}","ABCDABCDABCDABCD" , seed);
            return this.Identifier;
        }

        public Board CreateBoard(GameSettings settings)
        {
            this.Board = new Board(settings.BoardSize);
            return this.Board;
        }

        public Board CreateRandomBoard(GameSettings settings)
        {
            this.Board = new Board(settings.BoardSize);

            Random rnd = new Random();
            List<int> choices = Enumerable.Range(0, settings.BoardSize * settings.BoardSize).ToList();
            MineFactory mineFactory = new MineFactory();

            Tile tileWithMine0 = new Tile();
            tileWithMine0.Mine = mineFactory.CreateMine(0);

            Tile tileWithMine1 = new Tile();
            tileWithMine1.Mine = mineFactory.CreateMine(1);

            Tile tileWithMine2 = new Tile();
            tileWithMine2.Mine = mineFactory.CreateMine(2);

            for (int i = 0; i < settings.SimpleMineCount; i++)
            {
                int choicesIndex = rnd.Next(0, choices.Count);
                this.Board.Tiles[choices[choicesIndex]] = (Tile)tileWithMine0.Clone(); // Simple Mine
                choices.RemoveAt(choicesIndex);
            }
            for (int i = 0; i < settings.WideMineCount; i++)
            {
                int choicesIndex = rnd.Next(0, choices.Count);
                this.Board.Tiles[choices[choicesIndex]] = (Tile)tileWithMine1.Clone(); // Wide Mine
                choices.RemoveAt(choicesIndex);
            }
            for (int i = 0; i < settings.FakeMineCount; i++)
            {
                int choicesIndex = rnd.Next(0, choices.Count);
                this.Board.Tiles[choices[choicesIndex]] = (Tile)tileWithMine2.Clone(); // Fake Mine
                choices.RemoveAt(choicesIndex);
            }


            //for (int i = 0; i < settings.SimpleMineCount; i++)
            //{
            //    int choicesIndex = rnd.Next(0, choices.Count);
            //    this.Board.Tiles[choices[choicesIndex]].Mine = mineFactory.CreateMine(0); // Simple Mine
            //    choices.RemoveAt(choicesIndex);
            //}
            //for (int i = 0; i < settings.WideMineCount; i++)
            //{
            //    int choicesIndex = rnd.Next(0, choices.Count);
            //    this.Board.Tiles[choices[choicesIndex]].Mine = mineFactory.CreateMine(1); // Wide Mine
            //    choices.RemoveAt(choicesIndex);
            //}
            //for (int i = 0; i < settings.FakeMineCount; i++)
            //{
            //    int choicesIndex = rnd.Next(0, choices.Count);
            //    this.Board.Tiles[choices[choicesIndex]].Mine = mineFactory.CreateMine(2); // Fake Mine
            //    choices.RemoveAt(choicesIndex);
            //}

            return this.Board;
        }

        public Player GetEnemyView()
        {
            Player enemyView = new Player
            {
                Name = this.Name,
                Board = this.Board.GetEnemyView()
            };

            return enemyView;
        }
    }
}
