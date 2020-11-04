using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient.DesignPatternClasses
{
    class TitleAdditionsSpecialEffects : SpecialEffects
    {
        BattleSweeperWindow form;
        private string old_title;

        private Dictionary<string, DateTime> texts;

        private Timer timer;
        
        public TitleAdditionsSpecialEffects(BattleSweeperWindow form)
        {
            this.form = form;
            this.old_title = form.Text;
            this.timer = new Timer();
            timer.Interval = 10;
            timer.Tick += TimerTick;
            timer.Start();
            texts = new Dictionary<string, DateTime>();
        }

        public override void BoardClick(RectangleF boardBounds, float cellSize, int x, int y)
        {
            texts["BoardClick"] = DateTime.Now.AddSeconds(1);
        }

        public override void ButtonClick(RectangleF buttonBounds)
        {
            texts["ButtonClick"] = DateTime.Now.AddSeconds(1);
        }

        public override void Loss()
        {
            texts["Loss"] = DateTime.MaxValue;
        }

        public override void StartBackgroundEffect()
        {
            texts["BackgroundMusic"] = DateTime.MaxValue;
        }

        public override void StopBackgroundEffect()
        {
            texts["BackgroundMusic"] = DateTime.Now;
        }

        public override void Victory()
        {
            texts["Victory"] = DateTime.MaxValue;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            List<string> accum = new List<string>();
            
            foreach (KeyValuePair<string, DateTime> entry in texts)
            {
                if (entry.Value >= DateTime.Now)
                    accum.Add(entry.Key);
            }

            string all = string.Join(", ", accum);
            form.Text = this.old_title + (all.Length > 0 ? " (Playing: " + all + ")" : "");
        }
    }
}
