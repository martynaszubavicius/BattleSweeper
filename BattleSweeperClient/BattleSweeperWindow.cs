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
        Board playerBoardData = new Board(15);
        Board enemyBoardData = new Board(15);
        // testing shit over


        public BattleSweeperWindow()
        {
            InitializeComponent();
            LoadTextures("../../Textures");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = playerBoard.CreateGraphics();

            SetNumberForPanel(playerAmmo, random.Next(0, 1000), 3);
            g.DrawImage(textures["bomb"], new Rectangle(getGridStart(e.X, 16), getGridStart(e.Y, 16), 16, 16));

            //g.DrawRectangle(System.Drawing.Pens.Red, new Rectangle(getGridStart(e.X, 16), getGridStart(e.Y, 16), 30, 30));

            g.Dispose();
        }

        private int getGridStart(int raw, int gridSize)
        {
            return raw / gridSize * gridSize;
        }

        private void board1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = playerBoard.CreateGraphics();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    g.DrawImage(textures["tile"], new Rectangle(16 * i, 16 * j, 16, 16));
                }
            }
            g.Dispose();
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

        private void playerMinesLeft_Paint(object sender, PaintEventArgs e)
        {
            SetNumberForPanel(playerMinesLeft, 701, 3);
        }

        private void playerAmmo_Paint(object sender, PaintEventArgs e)
        {
            SetNumberForPanel(playerAmmo, 084, 3);
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

        private void enemyMinesLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void enemyBoard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void enemyAmmo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
