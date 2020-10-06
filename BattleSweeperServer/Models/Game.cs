using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Key { get { return this.Id.ToString(); } }
        public virtual Player PLayer1 { get; private set; }
        public virtual Player PLayer2 { get; private set; }
        public int BoardSize { get; set; }


        public Game()
        {

        }

        public bool RegisterPlayer(Player player)
        {
            lock (this)
            {
                if (PLayer1 == null)
                {
                    PLayer1 = player;
                    return true;
                }
                else if (PLayer2 == null)
                {
                    PLayer2 = player;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
