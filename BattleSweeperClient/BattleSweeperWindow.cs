using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        private int boardSize;
        private string gameKey;
        private GameSettings gameSettings;

        public BattleSweeperWindow(string gameKey)
        {
            this.gameKey = gameKey;
            this.boardSize = 10;

            InitializeComponent();
            LoadTextures("../../Textures");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            gameSettings = await APIAccessorSingleton.Instance.GetObject<GameSettings>("BattleSweeper/Game/{0}/Settings", gameKey);
            boardSize = gameSettings.BoardSize;
            gameUpdateTimer.Start();
        }

        

        private async void gameUpdateTimer_Tick(object sender, EventArgs e)
        {
            gameUpdateTimer.Stop();

            Game game = await APIAccessorSingleton.Instance.GetGameState(this.gameKey);

            if (game.Player1 != null)
                PaintBoardForPanel(playerBoard, game.Player1.Board);
            if (game.Player2 != null)
                PaintBoardForPanel(enemyBoard, game.Player2.Board);

            // TODO: update numbers as well you trash
            SetNumberForPanel(playerAmmo, 084, 3);
            SetNumberForPanel(playerMinesLeft, 701, 3);

            gameUpdateTimer.Start();
        }

        private int getGridStart(int raw, int gridSize)
        {
            return raw / gridSize * gridSize;
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

        private void LoadTextures(string path)
        {
            textures = new Dictionary<string, Image>();
            string[] files = Directory.GetFiles(path, "*.png");
            foreach (string imgFile in files)
                textures[string.Concat(imgFile.Split(new char[] { '\\' }).Last().Reverse().Skip(4).Reverse())] = new Bitmap(imgFile);
        }

        private void PaintBoardForPanel(Panel panel, Board board)
        {
            float cellSizeX = (float)panel.Width / board.Size;
            float cellSizeY = (float)panel.Height / board.Size;

            Graphics g = panel.CreateGraphics();
            for (int x = 0; x < board.Size; x++)
            {
                for (int y = 0; y < board.Size; y++)
                {
                    Tile tile = board.Tiles[board.GetIndex(x, y)];

                    Image img;
                    if (tile.Mine != null)
                        img = textures["bomb"];
                    else if (tile.State >= 0)
                        img = textures[string.Format("empty{0}", tile.State)];
                    else // -1
                        img = textures["tile"];

                    g.DrawImage(img, new RectangleF(cellSizeX * x, cellSizeY * y, cellSizeX, cellSizeY));
                }
            }
            g.Dispose();
        }

        private void SetNumberForPanel(Panel panel, int number, int digits)
        {
            Graphics g = panel.CreateGraphics();
            string[] fontNumbers = TranslateNumberToFontEntries(number, digits);
            for (int i = 0; i < digits; i++)
            {
                g.DrawImage(textures[fontNumbers[i]], new Rectangle(13 * i, 0, 13, 23));
            }
            g.Dispose();
        }

        private async void enemyBoard_MouseClick(object sender, MouseEventArgs e)
        {
            // TODO: Move these somewhere global
            float cellSizeX = (float)enemyBoard.Width / boardSize;
            float cellSizeY = (float)enemyBoard.Height / boardSize;

            // TODO: Edge cases - right now bottom right corner won't work
            int x = (int)(e.X / cellSizeX);
            int y = (int)(e.Y / cellSizeY);

            await APIAccessorSingleton.Instance.PostObject<Shot>(string.Format("BattleSweeper/Game/{0}/TestShot", gameKey), new Shot(x, y));
            gameUpdateTimer_Tick(null, null); // TODO: Quick dirty hack, update board properly
        }

        private async void playerBoard_MouseClick(object sender, MouseEventArgs e)
        {
            // TODO: Move these somewhere global
            float cellSizeX = (float)enemyBoard.Width / boardSize;
            float cellSizeY = (float)enemyBoard.Height / boardSize;

            // TODO: Edge cases - right now bottom right corner won't work
            int x = (int)(e.X / cellSizeX);
            int y = (int)(e.Y / cellSizeY);

            await APIAccessorSingleton.Instance.PostObject<Shot>(string.Format("BattleSweeper/Game/{0}/TestMineCycle", gameKey), new Shot(x, y));
            gameUpdateTimer_Tick(null, null); // TODO: Quick dirty hack, update board properly
        }
    }
}
