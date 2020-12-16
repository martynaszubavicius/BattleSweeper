using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    abstract public class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request, string text)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request, text);
            }
            else
            {
                return null;
            }
        }
    }
}
