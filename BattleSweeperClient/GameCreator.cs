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

        private async void newGameButton_Click(object sender, EventArgs e)
        {
            Game game = new Game(Int32.Parse(boardSizeTextBox.Text));
            Game createdGame = await APIAccessorSingleton.Instance.CreateGame(game);
            gameIdTextBox.Text = createdGame.Key;
        }

        private async void joinGameButton_Click(object sender, EventArgs e)
        {
            string gameKey = gameIdTextBox.Text;
            Player player = new Player(nameTextBox.Text);
            
            if (await APIAccessorSingleton.Instance.RegisterPlayerToGame(gameKey, player))
            {
                new BattleSweeperWindow().ShowDialog();
                //Application.Run(new BattleSweeperWindow());
            }
        }
    }
}
