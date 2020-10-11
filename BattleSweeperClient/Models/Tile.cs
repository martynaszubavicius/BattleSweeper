namespace BattleSweeperServer.Models
{
    public class Tile
    {
        public int State { get; set; }
        public Mine Mine { get; set; }

        public Tile()
        {
            
        }

        public Tile(int state)
        {
            this.State = state;
        }
    }
}
