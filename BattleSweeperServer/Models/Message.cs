using BattleSweeperServer.DesignPatternClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleSweeperServer.Models
{
    public class Message
    {
        [JsonProperty("Author")]
        public string Author { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        //[JsonProperty("Identifier")]
        //public string Identifier { get; set; }

        public Message(string player, string content, string identifier)
        {
            Author = player;
            Content = content;
            //Identifier = identifier;
        }
    }
}
