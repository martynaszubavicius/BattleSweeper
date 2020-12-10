﻿using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            this.newGameButton = new System.Windows.Forms.Button();
            this.joinGameButton = new System.Windows.Forms.Button();
            this.gameIdTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.gameSettingscomboBox = new System.Windows.Forms.ComboBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.secondPlayerForTesting = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.debugMode = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.randomBoard = new System.Windows.Forms.CheckBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.chatUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.createPlayerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(100, 212);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(151, 23);
            this.newGameButton.TabIndex = 0;
            this.newGameButton.Text = "Create new game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // joinGameButton
            // 
            this.joinGameButton.Location = new System.Drawing.Point(176, 265);
            this.joinGameButton.Name = "joinGameButton";
            this.joinGameButton.Size = new System.Drawing.Size(75, 23);
            this.joinGameButton.TabIndex = 1;
            this.joinGameButton.Text = "Join game";
            this.joinGameButton.UseVisualStyleBackColor = true;
            this.joinGameButton.Click += new System.EventHandler(this.joinGameButton_Click);
            // 
            // gameIdTextBox
            // 
            this.gameIdTextBox.Location = new System.Drawing.Point(16, 265);
            this.gameIdTextBox.Name = "gameIdTextBox";
            this.gameIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.gameIdTextBox.TabIndex = 2;
            this.gameIdTextBox.TextChanged += new System.EventHandler(this.joinGameButton_Validate);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(130, 10);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(121, 20);
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.TextChanged += new System.EventHandler(this.createPlayerButton_Validate);
            // 
            // gameSettingscomboBox
            // 
            this.gameSettingscomboBox.FormattingEnabled = true;
            this.gameSettingscomboBox.Location = new System.Drawing.Point(130, 36);
            this.gameSettingscomboBox.Name = "gameSettingscomboBox";
            this.gameSettingscomboBox.Size = new System.Drawing.Size(121, 21);
            this.gameSettingscomboBox.TabIndex = 5;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(12, 297);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(35, 13);
            this.errorLabel.TabIndex = 6;
            this.errorLabel.Text = "label1";
            // 
            // secondPlayerForTesting
            // 
            this.secondPlayerForTesting.AutoSize = true;
            this.secondPlayerForTesting.Location = new System.Drawing.Point(46, 128);
            this.secondPlayerForTesting.Name = "secondPlayerForTesting";
            this.secondPlayerForTesting.Size = new System.Drawing.Size(100, 17);
            this.secondPlayerForTesting.TabIndex = 7;
            this.secondPlayerForTesting.Text = "Add fake player";
            this.secondPlayerForTesting.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Player name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Game difficulty:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Debug options:";
            // 
            // debugMode
            // 
            this.debugMode.AutoSize = true;
            this.debugMode.Location = new System.Drawing.Point(46, 151);
            this.debugMode.Name = "debugMode";
            this.debugMode.Size = new System.Drawing.Size(87, 17);
            this.debugMode.TabIndex = 11;
            this.debugMode.Text = "Debug mode";
            this.debugMode.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Create random board:";
            // 
            // randomBoard
            // 
            this.randomBoard.AutoSize = true;
            this.randomBoard.Checked = true;
            this.randomBoard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randomBoard.Location = new System.Drawing.Point(236, 66);
            this.randomBoard.Name = "randomBoard";
            this.randomBoard.Size = new System.Drawing.Size(15, 14);
            this.randomBoard.TabIndex = 13;
            this.randomBoard.UseVisualStyleBackColor = true;
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.SystemColors.Window;
            this.chatBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chatBox.Location = new System.Drawing.Point(325, 10);
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.Size = new System.Drawing.Size(515, 271);
            this.chatBox.TabIndex = 14;
            this.chatBox.Text = "";
            // 
            // messageBox
            // 
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageBox.Location = new System.Drawing.Point(325, 287);
            this.messageBox.MaxLength = 100;
            this.messageBox.Multiline = false;
            this.messageBox.Name = "messageBox";
            this.messageBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.messageBox.Size = new System.Drawing.Size(515, 20);
            this.messageBox.TabIndex = 15;
            this.messageBox.Text = "";
            this.messageBox.WordWrap = false;
            this.messageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.messageBox_KeyUp);
            // 
            // chatUpdateTimer
            // 
            this.chatUpdateTimer.Enabled = true;
            this.chatUpdateTimer.Tick += new System.EventHandler(this.chatUpdateTimer_Tick);
            // 
            // createPlayerButton
            // 
            this.createPlayerButton.Enabled = false;
            this.createPlayerButton.Location = new System.Drawing.Point(100, 183);
            this.createPlayerButton.Name = "createPlayerButton";
            this.createPlayerButton.Size = new System.Drawing.Size(151, 23);
            this.createPlayerButton.TabIndex = 16;
            this.createPlayerButton.Text = "Create player";
            this.createPlayerButton.UseVisualStyleBackColor = true;
            this.createPlayerButton.Click += new System.EventHandler(this.createPlayerButton_Click);
            this.createPlayerButton.Click += new System.EventHandler(this.joinGameButton_Validate);
            // 
            // GameCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 319);
            this.Controls.Add(this.createPlayerButton);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.randomBoard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.debugMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.secondPlayerForTesting);
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
        private System.Windows.Forms.CheckBox secondPlayerForTesting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox debugMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox randomBoard;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.Timer chatUpdateTimer;
        private Button createPlayerButton;
    }
}