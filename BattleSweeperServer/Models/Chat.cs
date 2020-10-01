using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: '{1}' says: \"{2}\"", this.Id, this.Author, this.Text);
        }
    }
}
