using Newtonsoft.Json;

namespace BattleSweeperServer.Models
{
    //TODO: Edvinas, Shoot class has to be changed to abstract(constructor shouldn't be in the abstract class)
    public  class Shot
    {
        [JsonProperty("positionX")]
        public int positionX { get; set; }

        [JsonProperty("positionY")]
        public int positionY { get; set; }

        public Shot()
        {

        }

        public Shot(int x, int y)
        {
            positionX = x;
            positionY = y;
        }
    }
}
