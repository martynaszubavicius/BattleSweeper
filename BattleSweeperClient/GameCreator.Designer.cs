namespace BattleSweeperClient
{
    partial class GameCreator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.newGameButton = new System.Windows.Forms.Button();
            this.joinGameButton = new System.Windows.Forms.Button();
            this.gameIdTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.gameSettingscomboBox = new System.Windows.Forms.ComboBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(100, 183);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(151, 23);
            this.newGameButton.TabIndex = 0;
            this.newGameButton.Text = "Create new game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // joinGameButton
            // 
            this.joinGameButton.Location = new System.Drawing.Point(134, 280);
            this.joinGameButton.Name = "joinGameButton";
            this.joinGameButton.Size = new System.Drawing.Size(75, 23);
            this.joinGameButton.TabIndex = 1;
            this.joinGameButton.Text = "Join game";
            this.joinGameButton.UseVisualStyleBackColor = true;
            this.joinGameButton.Click += new System.EventHandler(this.joinGameButton_Click);
            // 
            // gameIdTextBox
            // 
            this.gameIdTextBox.Location = new System.Drawing.Point(13, 280);
            this.gameIdTextBox.Name = "gameIdTextBox";
            this.gameIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.gameIdTextBox.TabIndex = 2;
            this.gameIdTextBox.TextChanged += new System.EventHandler(this.joinGameButton_Validate);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(109, 33);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.TextChanged += new System.EventHandler(this.joinGameButton_Validate);
            // 
            // gameSettingscomboBox
            // 
            this.gameSettingscomboBox.FormattingEnabled = true;
            this.gameSettingscomboBox.Location = new System.Drawing.Point(32, 76);
            this.gameSettingscomboBox.Name = "gameSettingscomboBox";
            this.gameSettingscomboBox.Size = new System.Drawing.Size(121, 21);
            this.gameSettingscomboBox.TabIndex = 5;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(301, 327);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(35, 13);
            this.errorLabel.TabIndex = 6;
            this.errorLabel.Text = "label1";
            // 
            // GameCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 349);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.gameSettingscomboBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.gameIdTextBox);
            this.Controls.Add(this.joinGameButton);
            this.Controls.Add(this.newGameButton);
            this.Name = "GameCreator";
            this.Text = "GameCreator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Button joinGameButton;
        private System.Windows.Forms.TextBox gameIdTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox gameSettingscomboBox;
        private System.Windows.Forms.Label errorLabel;
    }
}