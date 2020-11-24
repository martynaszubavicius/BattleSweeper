using BattleSweeperServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class FlyweightFactory
    {
        //Registered players
        private Hashtable players = new Hashtable();

        public Player GetPlayer(string name, string identifier)
        {
            if (!players.ContainsKey(name))
            {
                Player newP = new Player() { Name = name };
                if (name != "tester123")
                    players.Add(name, newP);
                return newP;
            }

            Player p = (Player)players[name];

            if (p.Identifier == identifier || p.Identifier == "")
                return p;  // TODO: Memento logic should move be here and preserve the current board on the current game
            else
                return null;
        }
    }
}
