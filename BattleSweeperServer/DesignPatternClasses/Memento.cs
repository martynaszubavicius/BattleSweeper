using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class Memento
    {
        private string Identifier;
        private int AmmoCount;
        private Board Board;
        private string Name;

        public Memento(string identifier, int ammoCount, string name, Board board)
        {
            Identifier = identifier;
            AmmoCount = ammoCount;
            Name = name;
            Board = board;
        }

        public string IdentifierState()
        {
            return Identifier;
        }

        public int AmmoCountState()
        {
            return AmmoCount;
        }

        public Board BoardState()
        {
            return Board;
        }

        public string NameState()
        {
            return Name;
        }
    }
}
