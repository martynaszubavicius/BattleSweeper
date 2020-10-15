using BattleSweeperServer.Models;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient
{
    public partial class BattleSweeperWindow : Form
    {
        private Dictionary<string, Image> textures; // Font 13x23, Tile 16x16

        // TODO delete when this shit is properly done, only for testing
        Random random = new Random();
        // testing shit over
        
        // Game settings 
        private string gameKey;
        private List<string> shotTypes;
        private GameSettings gameSettings; // TODO: calculate bounds sometimes gets called before this is set somehow, or select doesnt innitialise properly for some reason

        // Player bounds
        private RectangleF playerBoardBounds;
        private RectangleF playerMinesBounds; // 39x23
        private RectangleF playerAmmoBounds;

        // Enemy bounds
        private RectangleF enemyBoardBounds;
        private RectangleF enemyMinesBounds;
        private RectangleF enemyAmmoBounds;

        private float boardCellSize;

        private bool enableClicks = false;

        public BattleSweeperWindow(string gameKey, GameSettings gameSettings)
        {
            this.gameKey = gameKey;
            shotTypes = new List<string> { "SSingleShot", "SFourShot" };
            this.gameSettings = gameSettings; // TODO: this is an ugly fix, rethink this whole thing

            //this.MinimumSize = new Size(700, 400);
            //this.Size = new Size(700, 400);

            InitializeComponent();
            LoadTextures("../../Textures");
        }

        private async void BattleSweeperWindow_Load(object sender, EventArgs e)
        {
            gameSettings = await APIAccessorSingleton.Instance.GetObject<GameSettings>("BattleSweeper/Game/{0}/Settings", gameKey);
            CalculateBounds();

            gameUpdateTimer.Start();
        }

        private async void gameUpdateTimer_Tick(object sender, EventArgs e)
        {
            gameUpdateTimer.Stop();

            Game game = await APIAccessorSingleton.Instance.GetGameState(this.gameKey);
            DrawGame(game);

            gameUpdateTimer.Start();
        }

        private void LoadTextures(string path)
        {
            textures = new Dictionary<string, Image>();
            string[] files = Directory.GetFiles(path, "*.png");
            foreach (string imgFile in files)
                textures[string.Concat(imgFile.Split(new char[] { '\\' }).Last().Reverse().Skip(4).Reverse())] = new Bitmap(imgFile);
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

        private void CalculateBounds()
        {
            int topBarWidth = 80;
            int spacer = 9;

            int split1 = topBarWidth + spacer;
            int split2 = split1 + 2 * spacer + 23;

            float boardSize = Math.Min(gameWindow.Height - topBarWidth - spacer * 3f - 23, gameWindow.Width / 2f - spacer * 2f);

            playerBoardBounds = new RectangleF(gameWindow.Width / 4f - boardSize / 2f, (gameWindow.Height - topBarWidth - 23 - spacer) / 2f - boardSize / 2f + topBarWidth + 23 + spacer, boardSize, boardSize);
            playerMinesBounds = new RectangleF(playerBoardBounds.X, playerBoardBounds.Y - spacer - 23, 39, 23);
            playerAmmoBounds = new RectangleF(playerBoardBounds.X + boardSize - 39, playerBoardBounds.Y - spacer - 23, 39, 23);

            enemyBoardBounds = new RectangleF(gameWindow.Width / 4f * 3f - boardSize / 2f, (gameWindow.Height - topBarWidth - 23 - spacer) / 2f - boardSize / 2f + topBarWidth + 23 + spacer, boardSize, boardSize);
            enemyMinesBounds = new RectangleF(enemyBoardBounds.X, enemyBoardBounds.Y - spacer - 23, 39, 23);
            enemyAmmoBounds = new RectangleF(enemyBoardBounds.X + boardSize - 39, enemyBoardBounds.Y - spacer - 23, 39, 23);



            boardCellSize = boardSize / gameSettings.BoardSize;
        }

        private void DrawGame(Game game, bool fullRedraw = false)
        {
            Graphics g = gameWindow.CreateGraphics();

            DrawBoardInBounds(g, game.Player1.Board, playerBoardBounds);
            if (game.Player2 != null)
                DrawBoardInBounds(g, game.Player2.Board, enemyBoardBounds);

            DrawNumbersInBounds(123, 3, g, playerMinesBounds);
            DrawNumbersInBounds(456, 3, g, playerAmmoBounds);
            DrawNumbersInBounds(789, 3, g, enemyMinesBounds);
            DrawNumbersInBounds(010, 3, g, enemyAmmoBounds);

            g.Dispose();
        }

        private void DrawWindow()
        {
            // TODO: optimize board drawing, atm big boards chug when being drawn
            Graphics g = gameWindow.CreateGraphics();

            // fill background
            g.FillRectangle(new SolidBrush(Color.Silver), gameWindow.DisplayRectangle);

            SolidBrush drawBrush = new SolidBrush(Color.Red);
            StringFormat drawFormat = new StringFormat();

            // Draw title bar
            Font drawFont = new Font("Arial", 45, FontStyle.Bold);
            g.DrawString("BattleSweeper", drawFont, drawBrush, 5, 5, drawFormat);
            drawFont.Dispose();

            // Draw by who text
            drawFont = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString("by MELV team", drawFont, drawBrush, 430, 45, drawFormat);
            drawFont.Dispose();

            drawBrush.Dispose();
            drawFormat.Dispose();

            // Draw divider
            g.DrawLine(Pens.Gray, 0, 80, gameWindow.ClientSize.Width, 80);

            // Draw Board frames
            DrawBorderForBounds(g, playerBoardBounds, 4);
            DrawBorderForBounds(g, enemyBoardBounds, 4);


            g.Dispose();
        }

        private void DrawButtonInBounds()
        {

        }

        private void DrawNumbersInBounds(int number, int digits, Graphics g, RectangleF bounds)
        {
            string[] fontNumbers = TranslateNumberToFontEntries(number, digits);

            for (int i = 0; i < digits; i++)
            {
                g.DrawImage(textures[fontNumbers[i]], new RectangleF(bounds.X +13 * i, bounds.Y, 13, 23));
            }
        }

        private void DrawBoardInBounds(Graphics g, Board board, RectangleF bounds)
        {
            for (int x = 0; x < board.Size; x++)
            {
                for (int y = 0; y < board.Size; y++)
                {
                    Tile tile = board.Tiles[board.GetIndex(x, y)];

                    Image img;
                    if (tile.Mine != null)
                        img = textures[tile.Mine.ImageName];
                    else if (tile.State >= 0)
                        img = textures[string.Format("empty{0}", tile.State)];
                    else // -1
                        img = textures["tile"];

                    g.DrawImage(img, new RectangleF(bounds.X + boardCellSize * x, bounds.Y + boardCellSize * y, boardCellSize, boardCellSize));
                }
            }
        }

        private void DrawBorderForBounds(Graphics g, RectangleF bounds, int borderWidth)
        {
            g.FillPolygon(Brushes.Gray, new PointF[] {
                new PointF(bounds.X - borderWidth, bounds.Y - borderWidth), // top left outside
                new PointF(bounds.X + bounds.Width + borderWidth, bounds.Y - borderWidth), // top right outside
                new PointF(bounds.X + bounds.Width, bounds.Y), // top right inside
                new PointF(bounds.X, bounds.Y), // top left inside
                new PointF(bounds.X, bounds.Y + bounds.Height), // bottom left inside
                new PointF(bounds.X - borderWidth, bounds.Y + bounds.Height + borderWidth) // bottom left outside
            });
            g.FillPolygon(Brushes.White, new PointF[] {
                new PointF(bounds.X + bounds.Width + borderWidth, bounds.Y + bounds.Height + borderWidth), // bottom right outside
                new PointF(bounds.X + bounds.Width + borderWidth, bounds.Y - borderWidth), // top right outside
                new PointF(bounds.X + bounds.Width, bounds.Y), // top right inside
                new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height), // bottom right inside
                new PointF(bounds.X, bounds.Y + bounds.Height), // bottom left inside
                new PointF(bounds.X - borderWidth, bounds.Y + bounds.Height + borderWidth), // bottom left outside
            });
        }

        private void ProcessWindowClick(object sender, MouseEventArgs e)
        {
            // determine which bounds
            Console.WriteLine();
            if (playerBoardBounds.Contains(e.Location))
            {
                ProcessBoardClick(playerBoardBounds, false, e);
            }
            else if (enemyBoardBounds.Contains(e.Location))
            {
                ProcessBoardClick(enemyBoardBounds, true, e);
            }
        }

        private async void ProcessBoardClick(RectangleF bounds, bool enemy, MouseEventArgs e)
        {
            int x = (int)((e.X - bounds.X) / boardCellSize);
            int y = (int)((e.Y - bounds.Y) / boardCellSize);

            if (enemy)
            {
                await APIAccessorSingleton.Instance.PostObject<CoordInfo>(string.Format("BattleSweeper/Game/{0}/TestShot", gameKey),
                    new CoordInfo() { PositionX = x, PositionY = y, Data = "testShot" });
            }
            else
            {
                await APIAccessorSingleton.Instance.PostObject<CoordInfo>(string.Format("BattleSweeper/Game/{0}/TestMineCycle", gameKey),
                    new CoordInfo() { PositionX = x, PositionY = y, Data = "testShot" });
            }

            gameUpdateTimer_Tick(null, null); // TODO: Quick dirty hack, update board properly
        }

        private void gameWindow_Paint(object sender, PaintEventArgs e)
        {
            // TODO: These lines below
            // paint redraws non game elements, and disables the click event handler;
            // next request re-enables it when you get game data again


            CalculateBounds();
            DrawWindow();

            enableClicks = false;
        }

        private void gameWindow_Resize(object sender, EventArgs e)
        {
            CalculateBounds();
            DrawWindow();
        }
    }
}
