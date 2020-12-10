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
        public Player Author { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }
        
        public Message(Player player, string content)
        {
            Author = player;
            Content = content;
        }
    }
}