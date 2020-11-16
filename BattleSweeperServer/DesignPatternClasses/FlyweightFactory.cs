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

        public void Add(string key, Player player)
        {
            players.Add(key ,player);
        }

        public Player GetPlayer(string key)
        {
            return ((Player)players[key]);
        }

        public bool CheckPlayerName(string name)
        {
            if (players.ContainsKey(name))
                return true;
            else
                return false;
        }
    }
}
