using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace BattleSweeperClient.DesignPatternClasses
{
    class SoundSpecialEffects : SpecialEffects
    {
        private Dictionary<string, SoundPlayer> sounds = new Dictionary<string, SoundPlayer>();

        public SoundSpecialEffects(string soundsDirectory)
        {
            LoadSounds(soundsDirectory);
        }

        public override void BoardClick(RectangleF boardBounds, float cellSize, int x, int y)
        {
            sounds["chimes"].Play();
        }

        public override void ButtonClick(RectangleF buttonBounds)
        {
            sounds["chimes"].Play();
        }

        public override void Loss()
        {
            sounds["chimes"].Play();
        }

        public override void StartBackgroundEffect()
        {
            sounds["chimes"].Play();
        }

        public override void StopBackgroundEffect()
        {
            sounds["chimes"].Play();
        }

        public override void Victory()
        {
            sounds["chimes"].Play();
        }

        private void LoadSounds(string path)
        {
            string[] files = Directory.GetFiles(path, "*.wav");
            foreach (string wavFile in files)
                sounds[string.Concat(wavFile.Split(new char[] { '\\' }).Last().Reverse().Skip(4).Reverse())] = new SoundPlayer(wavFile);
        }
    }
}
