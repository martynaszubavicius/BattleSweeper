using BattleSweeperClient.DesignPatternClasses;
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
using System.Runtime.CompilerServices;
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
        private List<string> shotTypes = new List<string> { "SSingleShot", "SFourShot", "SNineShot", "CLineShot", "CScatterShot" };
        private GameSettings gameSettings;
        private int selectedShotType = 0;

        // Player bounds
        private RectangleF playerBoardBounds;
        private RectangleF playerMinesBounds; // 39x23
        private RectangleF playerAmmoBounds;
        private RectangleF shotTypeSelectorBounds; // 50x19

        // Enemy bounds
        private RectangleF enemyBoardBounds;
        private RectangleF enemyMinesBounds;
        private RectangleF enemyAmmoBounds;

        private float boardCellSize;

        private bool enableClicks = false;
        private bool redrawButton = true;

        // TODO: do the same for paint/resize/click? 

        // TODO: right now UI thread hangs on timer tick - yay. Drawing needs to happen on UI thread, and so do click events
        // TODO: probable solution - cache game data and only draw changes, keeping UI intensive tasks to a minimum. 
        Action timerTickAction = () => {  };
        
        public BattleSweeperWindow(string gameKey)
        {
            this.gameKey = gameKey;

            // Load Form
            InitializeComponent();
            LoadTextures("../../Textures");

            // Keep asking for game settings until you receive them, then draw game window
            timerTickAction = () => { SetupGame(); };
            
            this.gameUpdateTimer.Start();
        }

        private void gameUpdateTimer_Tick(object sender, EventArgs e)
        {
            // actions themselves should stop and restart the timer once done with their work 
            // in order to not make 2 actions at the same time if it takes too long
            timerTickAction();
        }

        private async void SetupGame()
        {
            gameUpdateTimer.Stop();

            this.gameSettings = await APIAccessorSingleton.Instance.GetObject<GameSettings>("BattleSweeper/Game/{0}/Settings", gameKey);

            if (this.gameSettings != null)
            {                
                CalculateBounds();
                DrawWindow();
                redrawButton = true;

                // once we get the game settings, switch to getting game state on a timer
                timerTickAction = () => { UpdateGame(); };

                // add in event handlers to redraw the form if it has been resized, since it was actually drawn now
                this.gameWindow.Paint += new PaintEventHandler(this.gameWindow_Paint);
                this.gameWindow.MouseClick += new MouseEventHandler(this.ProcessWindowClick);
                this.gameWindow.Resize += new EventHandler(this.gameWindow_Resize);
            }
            else
            {
                // TODO: Draw an innitial screen saying it is waiting for server, or 2nd player or whatever
            }

            gameUpdateTimer.Start();
        }

        private async void UpdateGame()
        {
            gameUpdateTimer.Stop();

            Game game = await APIAccessorSingleton.Instance.GetGameState(this.gameKey);

            if (game != null)
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

        private void CalculateBounds()
        {
            int topBarWidth = 80;
            int spacer = 9;

            int split1 = topBarWidth + spacer;
            int split2 = split1 + 2 * spacer + 23;

            float boardSize = Math.Min(gameWindow.Height - topBarWidth - spacer * 3f - 23, gameWindow.Width / 2f - spacer * 2f);

            // player
            playerBoardBounds = new RectangleF(gameWindow.Width / 4f - boardSize / 2f, (gameWindow.Height - topBarWidth - 23 - spacer) / 2f - boardSize / 2f + topBarWidth + 23 + spacer, boardSize, boardSize);
            playerMinesBounds = new RectangleF(playerBoardBounds.X, playerBoardBounds.Y - spacer - 23, 39, 23);
            playerAmmoBounds = new RectangleF(playerBoardBounds.X + boardSize - 39, playerBoardBounds.Y - spacer - 23, 39, 23);

            shotTypeSelectorBounds = new RectangleF(playerBoardBounds.X + boardSize / 2f - 25, playerBoardBounds.Y - spacer - 23 + 2, 50, 19);

            // enemy
            enemyBoardBounds = new RectangleF(gameWindow.Width / 4f * 3f - boardSize / 2f, (gameWindow.Height - topBarWidth - 23 - spacer) / 2f - boardSize / 2f + topBarWidth + 23 + spacer, boardSize, boardSize);
            enemyMinesBounds = new RectangleF(enemyBoardBounds.X, enemyBoardBounds.Y - spacer - 23, 39, 23);
            enemyAmmoBounds = new RectangleF(enemyBoardBounds.X + boardSize - 39, enemyBoardBounds.Y - spacer - 23, 39, 23);

            boardCellSize = boardSize / gameSettings.BoardSize;
        }

        private void DrawGame(Game game, bool fullRedraw = false)
        {
            GraphicsAdapter g = new GraphicsAdapter(gameWindow, textures);

            if (redrawButton)
            {
                g.DrawImage(shotTypes[selectedShotType], shotTypeSelectorBounds);
                redrawButton = false;
            }

            g.DrawBattleSweeperNumbers(123, 3, playerMinesBounds);
            g.DrawBattleSweeperNumbers(456, 3, playerAmmoBounds);
            g.DrawBattleSweeperNumbers(789, 3, enemyMinesBounds);
            g.DrawBattleSweeperNumbers(010, 3, enemyAmmoBounds);

            // TODO: this is slow as fuck, implement staggering to keep UI responsive? Changes redrawn only?
            g.DrawBattleSweeperBoard(game.Player1.Board, playerBoardBounds, boardCellSize);
            g.DrawBattleSweeperBoard(game.Player2.Board, enemyBoardBounds, boardCellSize);

            g.Dispose();
        }

        private void DrawWindow()
        {
            GraphicsAdapter g = new GraphicsAdapter(gameWindow, textures);

            g.SetBackground(Color.Silver, gameWindow.DisplayRectangle);
            g.DrawBattleSweeperText("BattleSweeper", 45, Color.Red, new PointF(5, 5));
            g.DrawBattleSweeperText("by MELV team", 12, Color.Red, new PointF(430, 45));
            g.DrawLine(Pens.Gray, new PointF(0, 80), new PointF(gameWindow.ClientSize.Width, 80));
            g.DrawBattleSweeperBorder(4, playerBoardBounds);
            g.DrawBattleSweeperBorder(4, enemyBoardBounds);
            g.DrawBattleSweeperBorder(2, shotTypeSelectorBounds);

            g.Dispose();
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
            else if (shotTypeSelectorBounds.Contains(e.Location))
            {
                selectedShotType = (selectedShotType + 1) % shotTypes.Count;
                redrawButton = true;
            }

        }

        private async void ProcessBoardClick(RectangleF bounds, bool enemy, MouseEventArgs e)
        {
            int x = (int)((e.X - bounds.X) / boardCellSize);
            int y = (int)((e.Y - bounds.Y) / boardCellSize);

            if (enemy)
            {
                await APIAccessorSingleton.Instance.PostObject<CoordInfo>(string.Format("BattleSweeper/Game/{0}/TestShot", gameKey),
                    new CoordInfo() { PositionX = x, PositionY = y, Data = shotTypes[selectedShotType] });
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
            gameUpdateTimer.Stop();

            CalculateBounds();
            DrawWindow();
            redrawButton = true;

            gameUpdateTimer.Start();
        }

        private void gameWindow_Resize(object sender, EventArgs e)
        {
            gameUpdateTimer.Stop();

            CalculateBounds();
            DrawWindow();
            redrawButton = true;

            gameUpdateTimer.Start();
        }
    }
}
