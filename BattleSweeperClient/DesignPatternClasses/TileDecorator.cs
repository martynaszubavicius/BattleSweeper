using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.DesignPatternClasses
{
    public abstract class TileDecorator : DecoratedTile
    {
        protected DecoratedTile tile;

        public TileDecorator(DecoratedTile tile)
        {
            this.tile = tile;
        }

        protected Image OverlayImages(Image img1, Image img2)
        {
            Image clone = (Image)img1.Clone();
            using (Graphics g = Graphics.FromImage(clone))
            {
                g.DrawImage(img2, 0, 0);
            }

            return clone;
        }
    }
}
