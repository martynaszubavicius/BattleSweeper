using BattleSweeperServer.Models;
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
        List<Command> history = new List<Command>();
        
        public int CommandCount { get { return history.Count; } }

        public Observer()
        {
        }

        public void Add(Command command)
        {
            history.Add(command);
        }

        public void Undo(Game game, string playerIdentifier)
        {
            for (int i = history.Count - 1; i >= 0; i--)
                if (history[i].PlayerId == playerIdentifier && !history[i].Undone)
                {
                    history[i].Undo(game);
                    history.Add(history[i]);
                    break;
                }
        }

        // returns a list rather than a single point because this is used to serialize it for client, and the client expects a simple array
        public List<ChangePoint> GetPlayerViewCommands(string playerIdentifier, int historyStartIndex)  
        {
            List<Command> commands = this.history
                .GetRange(historyStartIndex, this.history.Count - historyStartIndex)
                .Where(item => !((item.PlayerId != playerIdentifier && item.Info.CommandType == "mine") || item.Info.CommandType == "endTurn"))
                .ToList();

            // clean up the player id's from enemy commands - in order to not ruin data, 
            // we need to create new command objects with null values in place of id
            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].Info.CommandType == "shot" && commands[i].PlayerId != playerIdentifier)
                    commands[i] = new ShotCommand(commands[i].Info, default) { Point = commands[i].Point };
            }

            return commands.Aggregate(new List<ChangePoint>(), (accum, cmd) => { return accum.Concat(cmd.Point).ToList(); });
        }

        public string GetCommandsLog(string format)
        {
            string output = "";
            LogVisitor logVisitor = new TextLogVisitor();
            if (format == "xml")
            {
                output += "<commands>\n";
                logVisitor = new XMLLogVisitor();
            }
            else if (format == "json")
            {
                output += "{\n";
                logVisitor = new JSONLogVisitor();
            }
            
            foreach (Command command in this.history)
            {
               output += command.Accept(logVisitor);
            }

            if (format == "xml")
                output += "</commands>\n";
            else if (format == "json")
                output += "}\n";

            return output;
        }
    }
}
