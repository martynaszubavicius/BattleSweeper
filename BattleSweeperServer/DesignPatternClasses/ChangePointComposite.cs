using BattleSweeperServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class ChangePointComposite : ChangePoint
    {
        [JsonIgnore]
        private List<ChangePoint> children = new List<ChangePoint>();

        public ChangePointComposite(int x, int y) : base(x, y) { }

        public override void Add(ChangePoint point)
        {
            if (point != null) children.Add(point);
        }

        public override IEnumerator<ChangePoint> GetEnumerator()
        {
            var changePoints = new Queue<ChangePoint>(new[] { this });
            while (changePoints.Any())
            {
                ChangePoint point = changePoints.Dequeue();
                yield return point;
                ChangePointComposite compos = point as ChangePointComposite;
                if (compos != null)
                    foreach (ChangePoint p in compos.children) changePoints.Enqueue(p);
            }
        }

        public override void Print()
        {
            Console.WriteLine("composite " + X + " " + Y);
        }
        public override string ToString()
        {
            return "X: " + this.X + ", Y: " + this.Y;
        }
    }
}
