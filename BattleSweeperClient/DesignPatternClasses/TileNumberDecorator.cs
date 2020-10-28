using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.DesignPatternClasses
{
    public class TileNumberDecorator : TileDecorator
    {
        private int number;

        public TileNumberDecorator(DrawableTile tile, int number) : base(tile) 
        {
            this.number = number;
        }

        public override Image GetImage(Dictionary<string, Image> textures)
        {
            Image img_old = tile.GetImage(textures);
            if (number == 0) return img_old;
            return OverlayImages(img_old, textures[string.Format("decorator_{0}", number)]);
        }
    }
}
