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
            this.playerBoard = new System.Windows.Forms.Panel();
            this.playerMinesLeft = new System.Windows.Forms.Panel();
            this.playerAmmo = new System.Windows.Forms.Panel();
            this.enemyBoard = new System.Windows.Forms.Panel();
            this.enemyMinesLeft = new System.Windows.Forms.Panel();
            this.enemyAmmo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // playerBoard
            // 
            this.playerBoard.Location = new System.Drawing.Point(85, 144);
            this.playerBoard.Name = "playerBoard";
            this.playerBoard.Size = new System.Drawing.Size(240, 240);
            this.playerBoard.TabIndex = 0;
            this.playerBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.board1_Paint);
            this.playerBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // playerMinesLeft
            // 
            this.playerMinesLeft.Location = new System.Drawing.Point(85, 115);
            this.playerMinesLeft.Name = "playerMinesLeft";
            this.playerMinesLeft.Size = new System.Drawing.Size(39, 23);
            this.playerMinesLeft.TabIndex = 1;
            this.playerMinesLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.playerMinesLeft_Paint);
            // 
            // playerAmmo
            // 
            this.playerAmmo.Location = new System.Drawing.Point(286, 115);
            this.playerAmmo.Name = "playerAmmo";
            this.playerAmmo.Size = new System.Drawing.Size(39, 23);
            this.playerAmmo.TabIndex = 2;
            this.playerAmmo.Paint += new System.Windows.Forms.PaintEventHandler(this.playerAmmo_Paint);
            // 
            // enemyBoard
            // 
            this.enemyBoard.Location = new System.Drawing.Point(397, 144);
            this.enemyBoard.Name = "enemyBoard";
            this.enemyBoard.Size = new System.Drawing.Size(240, 240);
            this.enemyBoard.TabIndex = 1;
            this.enemyBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.enemyBoard_Paint);
            // 
            // enemyMinesLeft
            // 
            this.enemyMinesLeft.Location = new System.Drawing.Point(397, 115);
            this.enemyMinesLeft.Name = "enemyMinesLeft";
            this.enemyMinesLeft.Size = new System.Drawing.Size(39, 23);
            this.enemyMinesLeft.TabIndex = 2;
            this.enemyMinesLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.enemyMinesLeft_Paint);
            // 
            // enemyAmmo
            // 
            this.enemyAmmo.Location = new System.Drawing.Point(598, 115);
            this.enemyAmmo.Name = "enemyAmmo";
            this.enemyAmmo.Size = new System.Drawing.Size(39, 23);
            this.enemyAmmo.TabIndex = 3;
            this.enemyAmmo.Paint += new System.Windows.Forms.PaintEventHandler(this.enemyAmmo_Paint);
            // 
            // BattleSweeperWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(881, 572);
            this.Controls.Add(this.enemyAmmo);
            this.Controls.Add(this.enemyMinesLeft);
            this.Controls.Add(this.enemyBoard);
            this.Controls.Add(this.playerAmmo);
            this.Controls.Add(this.playerMinesLeft);
            this.Controls.Add(this.playerBoard);
            this.Name = "BattleSweeperWindow";
            this.Text = "GameWindow";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel playerBoard;
        private System.Windows.Forms.Panel playerMinesLeft;
        private System.Windows.Forms.Panel playerAmmo;
        private System.Windows.Forms.Panel enemyBoard;
        private System.Windows.Forms.Panel enemyMinesLeft;
        private System.Windows.Forms.Panel enemyAmmo;
    }
}

