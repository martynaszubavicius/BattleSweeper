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
            players.Add(key, player);
        }

        public Player GetPlayer(string key)
        {
            return ((Player)players[key]);
        }

        public bool CheckPlayerName(string name)
        {
            //TODO: for testing purposes
            Delete("tester123");

            //TODO: if statement to check player identifier if it's the same person
            if (players.ContainsKey(name))
                return true;
            else
                return false;
        }

        public void Delete(string key)
        {
            players.Remove(key);
        }
    }
}
