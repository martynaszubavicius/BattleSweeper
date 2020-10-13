using BattleSweeperServer.Models;
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
        private int boardSize;
        private string gameKey;
        private GameSettings gameSettings;

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

        public BattleSweeperWindow(string gameKey)
        {
            this.gameKey = gameKey;
            this.boardSize = 10;

            //this.MinimumSize = new Size(700, 400);
            //this.Size = new Size(700, 400);

            InitializeComponent();
            LoadTextures("../../Textures");
        }

        private async void BattleSweeperWindow_Load(object sender, EventArgs e)
        {
            gameSettings = await APIAccessorSingleton.Instance.GetObject<GameSettings>("BattleSweeper/Game/{0}/Settings", gameKey);
            boardSize = gameSettings.BoardSize;
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
            int spacer = 5;

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

            DrawBoardInBounds(game.Player1.Board, g, playerBoardBounds);
            if (game.Player2 != null)
                DrawBoardInBounds(game.Player2.Board, g, enemyBoardBounds);

            DrawNumbersInBounds(123, 3, g, playerMinesBounds);
            DrawNumbersInBounds(456, 3, g, playerAmmoBounds);
            DrawNumbersInBounds(789, 3, g, enemyMinesBounds);
            DrawNumbersInBounds(010, 3, g, enemyAmmoBounds);

            g.Dispose();
        }

        private void DrawWindow()
        {
            Graphics g = gameWindow.CreateGraphics();

            // fill background
            g.FillRectangle(new SolidBrush(Color.Silver), gameWindow.DisplayRectangle);

            g.Dispose();
        }

        private void DrawNumbersInBounds(int number, int digits, Graphics g, RectangleF bounds)
        {
            string[] fontNumbers = TranslateNumberToFontEntries(number, digits);

            for (int i = 0; i < digits; i++)
            {
                g.DrawImage(textures[fontNumbers[i]], new RectangleF(bounds.X +13 * i, bounds.Y, 13, 23));
            }
        }

        private void DrawBoardInBounds(Board board, Graphics g, RectangleF bounds)
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

        private void ProcessWindowClick(object sender, MouseEventArgs e)
        {
            // determine which bounds
            Console.WriteLine();
            if (playerBoardBounds.Contains(e.Location))
            {
                ProcessBoardClick(playerBoardBounds, false, e);
            }
            if (enemyBoardBounds.Contains(e.Location))
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
