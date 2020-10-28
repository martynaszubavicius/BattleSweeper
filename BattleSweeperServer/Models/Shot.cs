using BattleSweeperServer.DesignPatternClasses;

namespace BattleSweeperServer.Models
{
    public abstract class Shot
    {
        public int ammoCost { get;set; }

        public ShotBehaviour shotBeh { get; set; }
        
    }
}
