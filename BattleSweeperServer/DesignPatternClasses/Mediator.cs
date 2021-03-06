﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using BattleSweeperServer.Models;
using SQLitePCL;

namespace BattleSweeperServer.DesignPatternClasses
{
    public abstract class Mediator
    {
        public abstract void Send(Message message, Colleague from);
    }
}