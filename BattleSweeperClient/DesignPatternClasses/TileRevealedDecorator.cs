using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.DesignPatternClasses
{
    class TileRevealedDecorator : TileDecorator
    {
        public TileRevealedDecorator(DrawableTile tile) : base(tile) { }

        public override Image GetImage(Dictionary<string, Image> textures)
        {
            Image img_old = tile.GetImage(textures);
            return OverlayImages(img_old, textures["empty"]);
        }
    }
}
