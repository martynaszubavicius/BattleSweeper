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

        //public virtual List<ChangePoint> OnReveal(Board board, int x, int y)
        //{
        //    List<ChangePoint> points = new List<ChangePoint>();
        //    points.Add(new ChangePoint(x, y));
        //    return points;
        //}
    }
}
