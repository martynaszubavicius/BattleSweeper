using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();



            int a = e.X / 30;
            int b = e.Y / 30;

            

            g.DrawRectangle(System.Drawing.Pens.Red, new Rectangle(getGridStart(e.X, 30), getGridStart(e.Y, 30), 30, 30));

            g.Dispose();
        }

        private int getGridStart(int raw, int gridSize)
        {
            return raw / gridSize * gridSize;
        }
    }
}
