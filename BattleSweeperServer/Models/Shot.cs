using Newtonsoft.Json;
using System.Reflection;

namespace BattleSweeperServer.Models
{
    //TODO: Edvinas, Shoot class has to be changed to abstract(constructor shouldn't be in the abstract class)
    public abstract class Shot
    {
        public int ammoCost { get;set; }

        // ShotBehaviour shotBeh = ...

        //[JsonProperty("positionX")]
        //public int positionX { get; set; }

        //[JsonProperty("positionY")]
        //public int positionY { get; set; }
    }
}
