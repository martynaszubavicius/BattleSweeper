using System.Data.Entity.Core.Mapping;

namespace BattleSweeperServer.Models
{
    public abstract class Mine
    {
        public string ImageName { get; set; }

        public Mine()
        {

        }

        public virtual void OnReveal(Board board, int x, int y)
        {

        }
    }
}
