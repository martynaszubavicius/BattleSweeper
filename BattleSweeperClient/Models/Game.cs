using Newtonsoft.Json;

namespace BattleSweeperServer.Models
{
    public class Game
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Key")]
        public string Key { get { return this.Id.ToString(); } } // TODO: use this instead of id in requests

        [JsonProperty("Player1")]
        public Player Player1 { get; private set; }

        [JsonProperty("Player2")]
        public Player Player2 { get; private set; }
        
        [JsonProperty("BoardSize")]
        public int BoardSize { get; set; }


        public Game()
        {

        }

        public Game(int boardSize)
        {
            BoardSize = boardSize;
        }

        public bool RegisterPlayer(Player player)
        {
            lock (this)
            {
                if (Player1 == null)
                {
                    player.CreateIdentifier(1);
                    Player1 = player;
                    return true;
                }
                else if (Player2 == null)
                {
                    player.CreateIdentifier(2);
                    Player2 = player;
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
