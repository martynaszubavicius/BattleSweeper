using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Identifier { get; set; }

        public Player()
        {

        }

        public Player(string name, string identifier)
        {
            this.Name = name;
            this.Identifier = identifier;
        }

        public string CreateIdentifier()
        {
            // TODO implement better random identifier
            this.Identifier = string.Format("{0}{1}","ABCDABCDABCDABCD" , this.Id.ToString());
            return this.Identifier;
        }

    }
}
