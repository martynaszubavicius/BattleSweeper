using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.DesignPatternClasses
{
    abstract class AbstractGraphicsAdapter
    {
        public abstract void DrawImage(Image image, RectangleF rectangle);
        public abstract void FillRectangle(SolidBrush brush, RectangleF bounds);
        public abstract void DrawString(string text, Font drawFont, SolidBrush drawBrush, PointF point, StringFormat drawFormat);
        public abstract void DrawLine(Pen pen, PointF start, PointF end);
        public abstract void FillPolygon(Brush brush, PointF[] points);
        public abstract void Dispose();
    }
}
