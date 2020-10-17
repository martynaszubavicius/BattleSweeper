using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.DesignPatternClasses
{
    public abstract class DecoratedTile
    {
        public abstract Image GetImage(Dictionary<string, Image> textures);
    }
}
