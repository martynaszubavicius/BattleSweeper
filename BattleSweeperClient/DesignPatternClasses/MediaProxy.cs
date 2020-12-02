using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BattleSweeperClient.DesignPatternClasses
{
    class MediaProxy : MediaPlayer
    {
		Uri path;
		MediaPlayer mediaPlayer;
		bool opened = false;

		public MediaProxy()
		{
			mediaPlayer = new MediaPlayer();
		}

		public void Open(Uri newPath)
        {
			this.path = newPath;
        }

        public void Play()
        {
			if (!opened)
			{
				mediaPlayer.Open(path);
				opened = true;
			}
			this.mediaPlayer.Play();
		}

		public void Stop()
        {
			this.mediaPlayer.Stop();
		}
	}
}
