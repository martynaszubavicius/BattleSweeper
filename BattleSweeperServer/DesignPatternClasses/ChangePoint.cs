using BattleSweeperServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class ChangePoint : IEnumerable<ChangePoint>
    {
        [JsonProperty("X")]
        public int X { get; set; }

        [JsonProperty("Y")]
        public int Y { get; set; }

        protected ChangePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public abstract void Add(ChangePoint point);
        public abstract void Print();
        
        //public abstract void Remove(ChangePoint point); // Won't need this one i think?

        public abstract IEnumerator<ChangePoint> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
