using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient.DesignPatternClasses
{
    public abstract class SpecialEffects
    {
        // TODO: to be majorly expanded
        public abstract void ButtonClick(RectangleF buttonBounds);
        public abstract void BoardClick(RectangleF boardBounds, float cellSize, int x, int y);
        public abstract void Victory();
        public abstract void Loss();
        public abstract void StartBackgroundEffect();
        public abstract void StopBackgroundEffect();
    }
}
