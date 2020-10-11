namespace BattleSweeperServer.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Key { get { return this.Id.ToString(); } } // TODO: use this instead of id in requests
        public Player PLayer1 { get; private set; }
        public Player PLayer2 { get; private set; }
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
                    player.CreateIdentifier(1);
                    PLayer1 = player;
                    return true;
                }
                else if (PLayer2 == null)
                {
                    player.CreateIdentifier(2);
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
