using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
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
            FinishInitialization();
        }

        private async void FinishInitialization()
        {
            newGameButton.Enabled = false;
            joinGameButton.Enabled = false;

            errorLabel.Text = "";

            gameSettingscomboBox.DataSource = await APIAccessorSingleton.Instance.GetObjects<GameSettings>(APIAccessorSingleton.GameSettingsRoute);
            gameSettingscomboBox.DisplayMember = "DisplayName";
            gameSettingscomboBox.ValueMember = "Id";

            newGameButton.Enabled = true;
        }

        private async void newGameButton_Click(object sender, EventArgs e)
        {
            GameSettings settings = (GameSettings)gameSettingscomboBox.SelectedItem;

            if (settings == null)
            {
                errorLabel.Text = "Invalid settings selected";
                return;
            }
                
            Game game = await APIAccessorSingleton.Instance.GetNewGameFromSettings(settings);

            if (game != null)
            {
                gameIdTextBox.Text = game.Key;
                errorLabel.Text = "";
            }
            else
            {
                errorLabel.Text = "No game created fuck you";
            }
        }

        private async void joinGameButton_Click(object sender, EventArgs e)
        {
            string gameKey = gameIdTextBox.Text;
            Player player = new Player() { Name = nameTextBox.Text };

            if (await APIAccessorSingleton.Instance.RegisterPlayerToGame(gameKey, player))
            {
                new BattleSweeperWindow(gameKey).ShowDialog();
                errorLabel.Text = "";
            }
            else
            {
                errorLabel.Text = "You didn't get registered into the game because you're too fat";
            }

        }

        private void joinGameButton_Validate(object sender, EventArgs e)
        {
            if (gameIdTextBox.Text.Length > 0 && nameTextBox.Text.Length > 0)
                joinGameButton.Enabled = true;
            else
                joinGameButton.Enabled = false;
        }
    }
}
