using BattleSweeperClient.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient.DesignPatternClasses
{
    public class GraphicsAdapter
    {
        private Graphics graphics;
        private Dictionary<string, Image> textures;

        public GraphicsAdapter(Panel panel, Dictionary<string, Image> textures)
        {
            this.graphics = panel.CreateGraphics();
            this.textures = textures;
        }

        public void DrawImage(string textureName, RectangleF bounds)
        {
            this.graphics.DrawImage(textures[textureName], bounds);
        }

        public void DrawBattleSweeperNumbers(int number, int digits, RectangleF bounds)
        {
            string[] fontNumbers = TranslateNumberToFontEntries(number, digits);
            for (int i = 0; i < digits; i++)
            {
                this.graphics.DrawImage(textures[fontNumbers[i]], new RectangleF(bounds.X + 13 * i, bounds.Y, 13, 23));
            }
        }
        
        public void DrawBattleSweeperBoard(Board board, RectangleF bounds, float boardCellSize, int startLine = 0, int lineCount = 0)
        {
            if (board == null) return;

            for (int x = startLine; x < (lineCount > 0 ? Math.Min(startLine + lineCount, board.Size) : board.Size); x++)
            {
                for (int y = 0; y < board.Size; y++)
                {
                    Tile tile = board.Tiles[board.GetIndex(x, y)];
                    DecoratedTile decoTile = tile;

                    if (tile.State != -1)
                    {
                        decoTile = new TileRevealedDecorator(decoTile);
                        
                        if (tile.Mine != null)
                        {
                            decoTile = new TileBombDecorator(decoTile, tile.Mine.ImageName);
                            decoTile = new TileCrossDecorator(decoTile);
                        }
                        else
                            decoTile = new TileNumberDecorator(decoTile, tile.State);
                    }
                    else
                    {
                        if (tile.Mine != null)
                            decoTile = new TileBombDecorator(decoTile, tile.Mine.ImageName);
                    }
                    
                    this.graphics.DrawImage(decoTile.GetImage(this.textures), new RectangleF(bounds.X + boardCellSize * x, bounds.Y + boardCellSize * y, boardCellSize, boardCellSize));
                }
            }
        }

        public void DrawBattleSweeperBoard(Board board, RectangleF bounds, float boardCellSize, List<Command> commands)
        {
            if (board == null) return;

            foreach (Command cmd in commands)
            {
                foreach (ChangePoint point in cmd.Points)
                {
                    Tile tile = board.Tiles[board.GetIndex(point.X, point.Y)];
                    DecoratedTile decoTile = tile;

                    if (tile.State != -1)
                    {
                        decoTile = new TileRevealedDecorator(decoTile);

                        if (tile.Mine != null)
                        {
                            decoTile = new TileBombDecorator(decoTile, tile.Mine.ImageName);
                            decoTile = new TileCrossDecorator(decoTile);
                        }
                        else
                            decoTile = new TileNumberDecorator(decoTile, tile.State);
                    }
                    else
                    {
                        if (tile.Mine != null)
                            decoTile = new TileBombDecorator(decoTile, tile.Mine.ImageName);
                    }

                    this.graphics.DrawImage(decoTile.GetImage(this.textures), new RectangleF(bounds.X + boardCellSize * point.X, bounds.Y + boardCellSize * point.Y, boardCellSize, boardCellSize));
                }

            }
        }

        public void SetBackground(Color color, RectangleF bounds)
        {
            this.graphics.FillRectangle(new SolidBrush(Color.Silver), bounds);
        }

        public void DrawBattleSweeperText(string text, int fontSize, Color color, PointF point)
        {
            SolidBrush drawBrush = new SolidBrush(color);
            StringFormat drawFormat = new StringFormat();
            Font drawFont = new Font("Arial", fontSize, FontStyle.Bold);

            this.graphics.DrawString("BattleSweeper", drawFont, drawBrush, point.X, point.Y, drawFormat);

            drawFont.Dispose();
            drawBrush.Dispose();
            drawFormat.Dispose();
        }

        public void DrawLine(Pen pen, PointF start, PointF end)
        {
            this.graphics.DrawLine(pen, start, end);
        }

        public void DrawBattleSweeperBorder(int borderWidth, RectangleF bounds)
        {
            this.graphics.FillPolygon(Brushes.Gray, new PointF[] {
                new PointF(bounds.X - borderWidth, bounds.Y - borderWidth), // top left outside
                new PointF(bounds.X + bounds.Width + borderWidth, bounds.Y - borderWidth), // top right outside
                new PointF(bounds.X + bounds.Width, bounds.Y), // top right inside
                new PointF(bounds.X, bounds.Y), // top left inside
                new PointF(bounds.X, bounds.Y + bounds.Height), // bottom left inside
                new PointF(bounds.X - borderWidth, bounds.Y + bounds.Height + borderWidth) // bottom left outside
            });
            this.graphics.FillPolygon(Brushes.White, new PointF[] {
                new PointF(bounds.X + bounds.Width + borderWidth, bounds.Y + bounds.Height + borderWidth), // bottom right outside
                new PointF(bounds.X + bounds.Width + borderWidth, bounds.Y - borderWidth), // top right outside
                new PointF(bounds.X + bounds.Width, bounds.Y), // top right inside
                new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height), // bottom right inside
                new PointF(bounds.X, bounds.Y + bounds.Height), // bottom left inside
                new PointF(bounds.X - borderWidth, bounds.Y + bounds.Height + borderWidth), // bottom left outside
            });
        }

        private string[] TranslateNumberToFontEntries(int number, int digitCount)
        {
            string[] translation = new string[digitCount];

            for (int i = digitCount - 1; i >= 0; i--)
            {
                int digit = number % 10;
                number /= 10;
                translation[i] = string.Format("font{0}", digit);
            }

            return translation;
        }

        public void Dispose()
        {
            this.graphics.Dispose();
        }
    }
}
