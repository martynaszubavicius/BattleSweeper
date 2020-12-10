using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSweeperServer.Models;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class ChatMediator : Mediator
    {
        public List<Message> Messages = new List<Message>();
        
        public override void Send(Message message, Colleague from)
        {
            if (from is Player)
            {
                Messages.Add(message);
            }

            //if (from is Game)
            //{
            //    message.Author = new Player();
            //    message.Author.Name = "Game";
            //    Messages.Add(message);
            //}
        }
    }
}
