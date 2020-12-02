using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Media;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BattleSweeperClient.DesignPatternClasses
{
    class SoundSpecialEffects : SpecialEffects
    {
        private Dictionary<string, MediaProxy> sounds = new Dictionary<string, MediaProxy>();

        public SoundSpecialEffects(string soundsDirectory)
        {
            LoadSounds(soundsDirectory);
        }

        public override void BoardClick(RectangleF boardBounds, float cellSize, int x, int y)
        {
            PlayMedia("chimes", 1);
        }

        public override void ButtonClick(RectangleF buttonBounds)
        {
            PlayMedia("chimes", 1);
        }

        public override void Loss()
        {
            PlayMedia("chimes", 1);
        }

        public override void StartBackgroundEffect()
        {
            PlayMedia("HardBass", 0.1, true);
        }

        public override void StopBackgroundEffect()
        {
            sounds["HardBass"].Stop();
        }

        public override void Victory()
        {
            PlayMedia("chimes", 1);
        }

        private void LoadSounds(string path)
        {
            string executableFilePath = Assembly.GetExecutingAssembly().Location;
            string executableDirectoryPath = Path.GetDirectoryName(executableFilePath);
            string SoundsDerectoryPath = Path.Combine(executableDirectoryPath, path);

            string[] files = Directory.GetFiles(path, "*.wav");
            foreach (string wavFile in files)
            {
                MediaProxy player = new MediaProxy();
                string audioFilePath = Path.Combine(SoundsDerectoryPath, wavFile);
                player.Open(new Uri(audioFilePath));
                sounds[string.Concat(wavFile.Split(new char[] { '\\' }).Last().Reverse().Skip(4).Reverse())] = player;
            }   
        }

        private void PlayMedia(string name, double volume, bool looping = false)
        {
            this.sounds[name].Position = TimeSpan.Zero;
            this.sounds[name].Volume = volume;
            if (looping) this.sounds[name].MediaEnded += LoopingMediaCallback;
            this.sounds[name].Stop();
            this.sounds[name].Play();
        }

        private void LoopingMediaCallback(object sender, EventArgs e)
        {
            MediaProxy player = (MediaProxy)sender;
            player.Position = TimeSpan.Zero;
            player.Play();
        }
    }
}
