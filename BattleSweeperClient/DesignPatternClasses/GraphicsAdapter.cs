using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient.DesignPatternClasses
{
    class GraphicsAdapter : AbstractGraphicsAdapter
    {
        private Graphics graphics;

        public GraphicsAdapter(Panel panel)
        {
            this.graphics = panel.CreateGraphics();
        }

        public override void DrawImage(Image image, RectangleF rectangle)
        {
            this.graphics.DrawImage(image, rectangle);
        }

        public override void FillRectangle(SolidBrush brush, RectangleF bounds)
        {
            this.graphics.FillRectangle(brush, bounds);
        }

        public override void DrawString(string text, Font drawFont, SolidBrush drawBrush, PointF point, StringFormat drawFormat)
        {
            this.graphics.DrawString(text, drawFont, drawBrush, point.X, point.Y, drawFormat);
        }

        public override void DrawLine(Pen pen, PointF start, PointF end)
        {
            this.graphics.DrawLine(pen, start, end);
        }

        public override void FillPolygon(Brush brush, PointF[] points)
        {
            this.graphics.FillPolygon(brush, points);
        }

        public override void Dispose()
        {
            this.graphics.Dispose();
        }
    }
}
