using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class ChangePointLeaf : ChangePoint
    {
        public ChangePointLeaf(int x, int y) : base(x, y) { }

        public override void Add(ChangePoint point)
        {
            throw new NotSupportedException();
        }

        public override IEnumerator<ChangePoint> GetEnumerator()
        {
            yield return this;
        }

        public override void Print()
        {
            Console.WriteLine("leaf " + X + " " + Y);
        }
        public override string ToString()
        {
            return "X: " + this.X + ", Y: " + this.Y;
        }
        public override bool Equals(object obj)
        {
            ChangePointLeaf other = obj as ChangePointLeaf;
            if (other == null)
            {
                return false;
            }
            return (this.X == other.X) && (this.Y == other.Y);
        }

        public override int GetHashCode()
        {
            return 33 * X.GetHashCode() + Y.GetHashCode();
        }
    }
}
