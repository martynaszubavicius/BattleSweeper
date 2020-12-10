using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BattleSweeperClient.Models
{
    class Message
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

        public override string ToString()
        {
            return string.Format("{0}: {1}\n", Author.Name, Content);
        }
    }
}