/*
 * Controls background sounds and music
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace PacMan
{
    class Sound
    {
        private SoundPlayer dead;
        private SoundPlayer gameOver;
        private WindowsMediaPlayer Player;
        private WindowsMediaPlayer powerPlayer;
        private string path;
        private string wakkaSound;
        private string wakkaPath;
        private string scaredGhosts;
        private bool powerMusic;

        public bool PowerMusic1 { get => powerMusic; set => powerMusic = value; }

        public Sound()
        {
            dead = new SoundPlayer();
            dead.Stream = Properties.Resources.KibbleEat;
            gameOver = new SoundPlayer(Properties.Resources.EndGame);

            path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName); //https://stackoverflow.com/questions/14899422/how-to-navigate-a-few-folders-up
            wakkaSound = "/Resources/wakka.wav";
            wakkaPath = path + "/Resources/wakka.wav";
            scaredGhosts = path + "/Resources/pacman_intermission.wav";            
            Player = new WMPLib.WindowsMediaPlayer(); //https://docs.microsoft.com/en-us/windows/win32/wmp/embedding-the-windows-media-player-control-in-a-c--solution
            Player.URL = wakkaPath;
            powerPlayer = new WMPLib.WindowsMediaPlayer();
            powerPlayer.URL = scaredGhosts;
            powerPlayer.controls.stop();
            Player.settings.setMode("loop", true);
            powerPlayer.settings.setMode("loop", true);
            powerMusic = false;
        }

        public void PowerMusic()
        {
            Player.controls.stop();
            powerPlayer.controls.play();
        }

        public void Waka()
        {
            Player.controls.play();
            powerPlayer.controls.stop();
        }

        public void Stop()
        {
            Player.controls.stop();
            powerPlayer.controls.stop();
        }

        public void BackgroundMusic(int kibbles, bool playerLose, bool dead)
        {
            if (powerMusic == true)
            {
                PowerMusic();
            }

            else if ((kibbles != 0) && (playerLose == false) && (dead == false))
            {
                Waka();
            }

            else
            {
                Stop();
            }
        }



    }
}
