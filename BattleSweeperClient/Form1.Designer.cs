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
            this.board1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // board1
            // 
            this.board1.Location = new System.Drawing.Point(148, 159);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(240, 240);
            this.board1.TabIndex = 0;
            this.board1.Paint += new System.Windows.Forms.PaintEventHandler(this.board1_Paint);
            this.board1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // BattleSweeperWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(881, 572);
            this.Controls.Add(this.board1);
            this.Name = "BattleSweeperWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel board1;
    }
}

