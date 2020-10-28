﻿using BattleSweeperServer.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class Observer
    {
        List<Command> History = new List<Command>();
        
        public int CommandCount { get { return History.Count; } }

        public Observer()
        {
        }

        public void Add(Command command)
        {
            History.Add(command);
        }

        public void Undo(Game game, string playerIdentifier)
        {
            for (int i = History.Count - 1; i >= 0; i--)
                if (History[i].PlayerId == playerIdentifier && !History[i].Undone)
                {
                    History[i].Undo(game);
                    History.Add(History[i]);
                    break;
                }
        }

        public List<ChangePoint> GetPlayerViewCommands(string playerIdentifier, int historyStartIndex)
        {
            List<Command> commands = this.History
                .GetRange(historyStartIndex, this.History.Count - historyStartIndex)
                .Where(item => !(item.PlayerId != playerIdentifier && item.Info.CommandType == "mine"))
                .ToList();

            // clean up the player id's from enemy commands - in order to not ruin data, 
            // we need to create new command objects with null values in place of id
            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].Info.CommandType == "shot" && commands[i].PlayerId != playerIdentifier)
                    commands[i] = new ShotCommand(commands[i].Info, default) { Points = commands[i].Points };
            }

            return commands.Aggregate(new List<ChangePoint>(), (accum, cmd) => { return accum.Concat(cmd.Points).ToList(); });
        }
    }
}
