﻿using System.Collections.Generic;
using BattleSweeperServer.Models;
using System.Data.Entity.Core.Mapping;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using BattleSweeperServer.DesignPatternClasses;

namespace BattleSweeperServer.Models
{
    public abstract class Mine
    {
        public string ImageName { get; set; }

        public MineBridgeReveal mineReveal { get; set; }

        public Mine()
        {

        }

        public virtual ChangePoint OnReveal(Board board, int x, int y)
        {
            return this.mineReveal.OnReveal(board, x, y);

            //List<ChangePoint> points = new List<ChangePoint>();
            //points.Add(new ChangePoint(x, y));
            //return points;
        }
        public override bool Equals(object obj)
        {
            SimpleMine other = obj as SimpleMine;
            if (other == null)
            {
                return false;
            }
            return (this.ImageName == other.ImageName) && (this.mineReveal.Equals(other.mineReveal));
        }

        public override int GetHashCode()
        {
            return 33 * ImageName.GetHashCode() + mineReveal.GetHashCode();
        }
    }
}
