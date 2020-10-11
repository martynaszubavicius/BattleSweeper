namespace BattleSweeperServer.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
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
