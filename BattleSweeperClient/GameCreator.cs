using BattleSweeperServer.Models;
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
    public partial class GameCreator : Form
    {
        public GameCreator()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {

        }

        private void joinGameButton_Click(object sender, EventArgs e)
        {
            Player player = new Player(nameTextBox.Text);
        }
    }
}
