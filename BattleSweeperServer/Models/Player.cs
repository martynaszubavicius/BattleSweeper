using Newtonsoft.Json;

namespace BattleSweeperServer.Models
{
    public class Player
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Identifier")]
        public string Identifier { get; set; }

        [JsonProperty("Board")]
        public Board Board { get; set; }

        public Player()
        {

        }

        public Player(string name)
        {
            this.Name = name;
        }

        public string CreateIdentifier(int seed)
        {
            // TODO: implement better random identifier,  for now same for everyone, which is stupid. I am stupid
            this.Identifier = string.Format("{0}{1}","ABCDABCDABCDABCD" , seed);
            return this.Identifier;
        }

        public Board CreateBoard(int size)
        {
            this.Board = new Board(size);
            return this.Board;
        }

    }
}
