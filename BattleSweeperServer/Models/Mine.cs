using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;

namespace BattleSweeperServer.Models
{
    public abstract class Mine
    {
        public string ImageName { get; set; }

        public Mine()
        {

        }

        public virtual List<Point> OnReveal(Board board, int x, int y)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(x, y));
            return points;
        }
    }
}
