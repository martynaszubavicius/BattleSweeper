namespace BattleSweeperClient
{
    partial class BattleSweeperWindow
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
            this.gameUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.gameWindow = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // gameUpdateTimer
            // 
            this.gameUpdateTimer.Interval = 250;
            this.gameUpdateTimer.Tick += new System.EventHandler(this.gameUpdateTimer_Tick);
            // 
            // gameWindow
            // 
            this.gameWindow.BackColor = System.Drawing.Color.Silver;
            this.gameWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameWindow.Location = new System.Drawing.Point(0, 0);
            this.gameWindow.Name = "gameWindow";
            this.gameWindow.Size = new System.Drawing.Size(684, 361);
            this.gameWindow.TabIndex = 4;
            // 
            // BattleSweeperWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.gameWindow);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "BattleSweeperWindow";
            this.Text = "BattleSweeper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BattleSweeperWindow_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer gameUpdateTimer;
        private System.Windows.Forms.Panel gameWindow;
    }
}

