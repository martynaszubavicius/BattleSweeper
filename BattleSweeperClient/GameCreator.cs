using BattleSweeperClient.DesignPatternClasses;
using BattleSweeperClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
                
            //Game game = await APIAccessorSingleton.Instance.GetNewGameFromSettings(settings);
            string gameKey = await APIAccessorSingleton.Instance.GetNewGameFromSettings(settings, debugMode.Checked);

            if (gameKey != default)
            {
                gameIdTextBox.Text = gameKey;
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
            string playerName = nameTextBox.Text;

            try
            {
                Player player = await APIAccessorSingleton.Instance.RegisterPlayerToGame(gameKey, new Player() { Name = playerName }, randomBoard.Checked);

                if (secondPlayerForTesting.Checked)
                {
                    try
                    {
                        await APIAccessorSingleton.Instance.RegisterPlayerToGame(gameKey, new Player() { Name = "tester123" }, true, true);
                    }
                    catch (APIAccessException) { }; // we dont care about the result since this player does nothing
                }

                new BattleSweeperWindow(gameKey).ShowDialog();
                errorLabel.Text = "";
            }
            catch (APIAccessException exc)
            {
                errorLabel.Text = exc.Message;
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
