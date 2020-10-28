using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.DesignPatternClasses
{
    public class TileBombDecorator : TileDecorator
    {
        private string bombType;
        public TileBombDecorator(DrawableTile tile, string bombType) : base(tile) 
        {
            this.bombType = bombType;
        }

        public override Image GetImage(Dictionary<string, Image> textures)
        {
            Image img_old = tile.GetImage(textures);
            return OverlayImages(img_old, textures[string.Format("decorator_{0}", bombType)]);
        }
    }
}
