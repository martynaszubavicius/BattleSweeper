using BattleSweeperClient.DesignPatternClasses;
using BattleSweeperClient.Models;
using Newtonsoft.Json;
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
        
        // Game settings 
        private string gameKey;
        private List<string> shotTypes = new List<string> { "SSingleShot", "SFourShot", "SNineShot", "CLineShot", "CScatterShot" };
        private GameSettings gameSettings;
        private List<SpecialEffects> effects = new List<SpecialEffects>();
        private int selectedShotType = 0;
        private int lastState = -1;

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

        // TODO: Should this still be here?
        private bool enableClicks = false;
        private bool redrawButton = true;

        Action timerTickAction = () => { };
        
        public BattleSweeperWindow(string gameKey)
        {
            this.gameKey = gameKey;

            // Load Form
            InitializeComponent();
            LoadTextures("../../Resources/Textures");

            // Load up some dank effects
            //effects.Add(new SoundSpecialEffects("../../Resources/Sounds"));
            //effects.Add(new TitleAdditionsSpecialEffects(this));

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

                effects.ForEach(i => i.StartBackgroundEffect());

                // once we get the game settings, switch to getting game state on a timer
                // timerTickAction = () => { UpdateGame(); };
                timerTickAction = () => { StaggeredUpdateGame(true); };

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

        private async void StaggeredUpdateGame(bool fullRedraw = false)
        {
            gameUpdateTimer.Stop();

            


            Game game = await APIAccessorSingleton.Instance.GetGameState(this.gameKey, fullRedraw ? -1 : this.lastState);
            this.lastState = game.HistoryLastIndex;

            if (game != null)
            {
                GraphicsFacade g = new GraphicsFacade(gameWindow, textures);
                gameUpdateTimer.Interval = 1;

                timerTickAction = () => {
                    StaggeredDrawGame(game, g, fullRedraw, 0, 2);
                };
            }

            gameUpdateTimer.Start();
        }

        private void StaggeredDrawGame(Game game, GraphicsFacade g, bool fullRedraw, int startLine, int lineCount)
        {
            gameUpdateTimer.Stop();

            if (redrawButton)
            {
                g.DrawImage(shotTypes[selectedShotType], shotTypeSelectorBounds);
                redrawButton = false;
            }

            g.DrawBattleSweeperNumbers(game.Player1.Board.CountAllMines(false, false), 3, playerMinesBounds);
            g.DrawBattleSweeperNumbers(game.Player1.AmmoCount, 3, playerAmmoBounds);
            g.DrawBattleSweeperNumbers(game.Settings.SimpleMineCount + game.Settings.WideMineCount - game.Player2.Board.CountAllMines(true, false), 3, enemyMinesBounds);
            g.DrawBattleSweeperNumbers(game.Player2.AmmoCount, 3, enemyAmmoBounds);

            if (fullRedraw)
            {
                g.DrawBattleSweeperBoard(game.Player1.Board, playerBoardBounds, boardCellSize, startLine, lineCount);
                g.DrawBattleSweeperBoard(game.Player2.Board, enemyBoardBounds, boardCellSize, startLine, lineCount);
            }
            else
            {
                g.DrawBattleSweeperBoard(game.Player1.Board, playerBoardBounds, boardCellSize, game.RedrawPoints);
                g.DrawBattleSweeperBoard(game.Player2.Board, enemyBoardBounds, boardCellSize, game.RedrawPoints);
            }
            

            if (startLine + lineCount >= gameSettings.BoardSize || !fullRedraw)
            {
                // we done, next update after 15ms - can afford to do that now since the ui thread doesnt hang as much
                gameUpdateTimer.Interval = 15;
                timerTickAction = () => { StaggeredUpdateGame(); };
                g.Dispose();
            }
            else
            {
                timerTickAction = () => {
                    StaggeredDrawGame(game, g, fullRedraw, startLine + lineCount, lineCount);
                };
            }

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
            GraphicsFacade g = new GraphicsFacade(gameWindow, textures);

            if (redrawButton)
            {
                g.DrawImage(shotTypes[selectedShotType], shotTypeSelectorBounds);
                redrawButton = false;
            }

            g.DrawBattleSweeperNumbers(game.Player1.Board.CountAllMines(false, false), 3, playerMinesBounds);
            g.DrawBattleSweeperNumbers(game.Player1.AmmoCount , 3, playerAmmoBounds);
            g.DrawBattleSweeperNumbers(game.Settings.SimpleMineCount + game.Settings.WideMineCount - game.Player2.Board.CountAllMines(true, false), 3, enemyMinesBounds);
            g.DrawBattleSweeperNumbers(game.Player2.AmmoCount, 3, enemyAmmoBounds);

            g.DrawBattleSweeperBoard(game.Player1.Board, playerBoardBounds, boardCellSize);
            g.DrawBattleSweeperBoard(game.Player2.Board, enemyBoardBounds, boardCellSize);

            g.Dispose();
        }

        private void DrawWindow()
        {
            GraphicsFacade g = new GraphicsFacade(gameWindow, textures);

            g.SetBackground(gameWindow.DisplayRectangle);
            g.DrawBattleSweeperText("BattleSweeper", 45, new PointF(5, 5));
            g.DrawBattleSweeperText("by MELV team", 12, new PointF(430, 45));
            g.DrawLine(new PointF(0, 80), new PointF(gameWindow.ClientSize.Width, 80));
            g.DrawBattleSweeperBorder(4, playerBoardBounds);
            g.DrawBattleSweeperBorder(4, enemyBoardBounds);
            g.DrawBattleSweeperBorder(2, shotTypeSelectorBounds);

            g.Dispose();
        }

        private async void ProcessWindowClick(object sender, MouseEventArgs e)
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
                effects.ForEach(i => i.ButtonClick(shotTypeSelectorBounds));
            }
            else if (playerMinesBounds.Contains(e.Location))
            {
                effects.ForEach(i => i.ButtonClick(playerMinesBounds));
                try
                {
                    await APIAccessorSingleton.Instance.PostObject<object, object>(string.Format("BattleSweeper/Game/{0}/UndoLastCommand", gameKey), default);
                }
                catch (APIAccessException) { }
            }
            else if (playerAmmoBounds.Contains(e.Location))
            {
                effects.ForEach(i => i.ButtonClick(playerAmmoBounds));
                CoordInfo coords = new CoordInfo() { PositionX = 0, PositionY = 0, Data = "switchingTurns", CommandType = "endTurn" };
                await APIAccessorSingleton.Instance.PostObject<CoordInfo, CoordInfo>(string.Format("BattleSweeper/Game/{0}/ExecuteCommand", gameKey), coords);
            }
            else if (enemyAmmoBounds.Contains(e.Location))
            {
                string format = "txt";
                //string format = "xml";
                //string format = "json";
                string output = await APIAccessorSingleton.Instance.GetLogOutput(gameKey, format);
                SaveFile(output, format);
            }
        }

        private async void ProcessBoardClick(RectangleF bounds, bool enemy, MouseEventArgs e)
        {
            int x = (int)((e.X - bounds.X) / boardCellSize);
            int y = (int)((e.Y - bounds.Y) / boardCellSize);

            effects.ForEach(i => i.BoardClick(bounds, boardCellSize, x, y));

            CoordInfo coords;
            if (enemy)
            {
                coords = new CoordInfo() { PositionX = x, PositionY = y, Data = shotTypes[selectedShotType], CommandType = "shot" };
            }
            else
            {
                coords = new CoordInfo() { PositionX = x, PositionY = y, Data = "testShot", CommandType = "mine" };
            }

            await APIAccessorSingleton.Instance.PostObject<CoordInfo, CoordInfo>(string.Format("BattleSweeper/Game/{0}/ExecuteCommand", gameKey), coords);

            gameUpdateTimer_Tick(null, null); // TODO: Quick dirty hack, update board properly
        }

        private void OnResize()
        {
            gameUpdateTimer.Stop();

            CalculateBounds();
            DrawWindow();
            redrawButton = true;

            // Whole game needs to be redrawn, so we do that, except the timer interval is short
            // to update it immediately
            gameUpdateTimer.Interval = 1;
            timerTickAction = () => { StaggeredUpdateGame(true); };
            gameUpdateTimer.Start();
        }

        private void gameWindow_Paint(object sender, PaintEventArgs e)
        {
            OnResize();
        }

        private void gameWindow_Resize(object sender, EventArgs e)
        {
            OnResize();
        }

        private void BattleSweeperWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            effects.ForEach(i => i.StopBackgroundEffect());
        }

        private void SaveFile(string output, string format)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "*."+ format;
            saveFileDialog.DefaultExt = format;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(fileStream);

                sw.Write(output);
                sw.Close();
                fileStream.Close();
            }
        }
    }
}
