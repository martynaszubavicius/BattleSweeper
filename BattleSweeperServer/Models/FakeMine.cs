﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.Models
{
    public class FakeMine : Mine
    {
        public FakeMine()
        {
            ImageName = "gray_bomb";
        }
    }
}