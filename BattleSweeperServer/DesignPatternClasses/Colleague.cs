using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Xsl;
using BattleSweeperServer.Models;

namespace BattleSweeperServer.DesignPatternClasses
{
    public interface Colleague
    {
        public Mediator Chat { get; set; }

        public void Send(Message message);
    }
}
