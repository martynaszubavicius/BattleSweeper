using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class Observer
    {
        List<Command> History = new List<Command>();

        public void Add(Command command)
        {
            History.Add(command);
        }



        //private List<Command> GetPlayerViewCommands(string playerIdentifier, int historyStartIndex)
        //{
        //    List<Command> commands = this.History
        //        .GetRange(historyStartIndex, this.History.Count - historyStartIndex)
        //        .Where(item => !(item.PlayerId != playerIdentifier && item.Info.CommandType == "mine"))
        //        .ToList();

        //    // clean up the player id's from enemy commands - in order to not ruin data, 
        //    // we need to create new command objects with null values in place of id
        //    for (int i = 0; i < commands.Count; i++)
        //    {
        //        if (commands[i].Info.CommandType == "shot" && commands[i].PlayerId != playerIdentifier)
        //            commands[i] = new ShotCommand(commands[i].Info, default) { Points = commands[i].Points };
        //    }

        //    return commands;
        //}
    }
}
