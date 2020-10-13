using BattleSweeperServer.DesignPatternClasses;

namespace BattleSweeperServer.Models
{
    public abstract class Shot
    {
        public int ammoCost { get;set; }

        public ShotBehaviour shotBeh { get; set; }
        

        // ShotBehaviour shotBeh = ...

        //[JsonProperty("positionX")]
        //public int positionX { get; set; }

        //[JsonProperty("positionY")]
        //public int positionY { get; set; }
    }
}
