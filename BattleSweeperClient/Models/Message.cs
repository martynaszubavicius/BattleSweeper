using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BattleSweeperClient.Models
{
    class Message
    {
        [JsonProperty("Author")]
        public string Author { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        public Message(string player, string content)
        {
            Author = player;
            Content = content;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}\n", Author, Content);
        }
    }
}
