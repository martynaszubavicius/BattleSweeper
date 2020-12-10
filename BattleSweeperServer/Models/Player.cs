﻿using BattleSweeperServer.DesignPatternClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BattleSweeperServer.Models
{
    public class Player : Colleague
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Identifier")]
        public string Identifier { get; set; }

        [JsonProperty("AmmoCount")]
        public int AmmoCount { get; set; }

        [JsonProperty("Board")]
        public Board Board { get; set; }

        [JsonProperty("Chat")]
        public Mediator Chat { get; set; }

        public bool InGame = false;

        public Player()
        {
            Identifier = ""; // in order for flyweight to correctly allow player names to be taken again if they weren't registered in the game
        }

        public string CreateIdentifier(int seed)
        {
            // TODO: implement better random identifier,  for now same for everyone, which is stupid. I am stupid
            this.Identifier = string.Format("{0}{1}{2}", "ABCDABCDABCDABCD", seed, Name);
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

            // Simple Mine
            Tile tileWithMine0 = new Tile();
            tileWithMine0.Mine = mineFactory.CreateMine(0);

            // Wide Mine
            Tile tileWithMine1 = new Tile();
            tileWithMine1.Mine = mineFactory.CreateMine(1);

            // Fake Mine
            Tile tileWithMine2 = new Tile();
            tileWithMine2.Mine = mineFactory.CreateMine(2);

            for (int i = 0; i < settings.SimpleMineCount; i++)
            {
                int choicesIndex = rnd.Next(0, choices.Count);
                this.Board.Tiles[choices[choicesIndex]] = (Tile)tileWithMine0.Clone();
                choices.RemoveAt(choicesIndex);
            }
            for (int i = 0; i < settings.WideMineCount; i++)
            {
                int choicesIndex = rnd.Next(0, choices.Count);
                this.Board.Tiles[choices[choicesIndex]] = (Tile)tileWithMine1.Clone();
                choices.RemoveAt(choicesIndex);
            }
            for (int i = 0; i < settings.FakeMineCount; i++)
            {
                int choicesIndex = rnd.Next(0, choices.Count);
                this.Board.Tiles[choices[choicesIndex]] = (Tile)tileWithMine2.Clone();
                choices.RemoveAt(choicesIndex);
            }

            return this.Board;
        }

        public Player GetEnemyView()
        {
            Player enemyView = new Player
            {
                Name = this.Name,
                Board = this.Board.GetEnemyView(),
                AmmoCount = this.AmmoCount
            };

            return enemyView;
        }

        //---------------Memento-----------------
        public Memento CreateMemento()
        {
            return (new Memento(Identifier, AmmoCount, Name, Board));
        }

        //RestoreToOriginalState
        public void SetMemento(Memento memento)
        {
            this.Name = memento.NameState();
            this.Identifier = memento.IdentifierState();
            this.AmmoCount = memento.AmmoCountState();
            this.Board = memento.BoardState();
        }

        public void Send(Message message)
        {
            this.Chat.Send(message, this);
        }
    }
}
