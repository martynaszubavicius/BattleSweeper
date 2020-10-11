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
        private Dictionary<string, Image> textures;

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
            Graphics g = board1.CreateGraphics();


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
            Graphics g = board1.CreateGraphics();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    g.DrawImage(textures["tile"], new Rectangle(16 * i, 16 * j, 16, 16));
                }
            }
            g.Dispose();
        }

        private void LoadTextures(string path)
        {
            textures = new Dictionary<string, Image>();
            string[] files = Directory.GetFiles(path, "*.png");
            foreach (string imgFile in files)
                textures[string.Concat(imgFile.Split(new char[] { '\\' }).Last().Reverse().Skip(4).Reverse())] = new Bitmap(imgFile);
        }
    }
}
