using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient
{
    class FormPainter
    {
        private Panel panel;
        private GameSettings gameSettings;

        // Player bounds
        private RectangleF playerBoard;
        private RectangleF playerMines; // 39x23
        private RectangleF playerAmmo;

        // Enemy bounds
        private RectangleF enemyBoard;
        private RectangleF enemyMines;
        private RectangleF enemyAmmo;


        public FormPainter(Panel panel, GameSettings gameSettings)
        {
            this.panel = panel;
            this.gameSettings = gameSettings;
        }

        

        public void DrawGame(Game game)
        {

        }



    }
}
