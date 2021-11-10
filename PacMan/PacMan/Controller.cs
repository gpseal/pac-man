using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using System.IO;


namespace PacMan
{
    public class Controller
    {
        private const int PACFRAMECOUNT = 24;
        private const int GHOULFRAMECOUNT = 4;
        private const int PACMANSTARTX = 11;
        private const int PACMANSTARTY = 13;
        private const int GHOULSTARTX = 9;
        private const int GHOULSTARTY = 11;

        private Random random;
        private PacMan pacman;
        private Maze maze;
        
        private List<Bitmap> pacmanFrames;
        private List<Bitmap> ghoul1Frames;
        private List<Bitmap> ghoul2Frames;
        private List<Bitmap> ghoul3Frames;
        private List<Bitmap> ghoul4Frames;
        private List<Ghoul> ghouls;
        private TextBox textBox1;
        private TextBox textBox2;
        private int score;
        private int ghoulStart;
        private int counter;
        private bool pacPower;
        private SoundPlayer dead;
        private SoundPlayer gameOver;
        private WindowsMediaPlayer Player;
        private WindowsMediaPlayer powerPlayer;
        private string path;
        private string wakkaSound;
        private string wakkaPath;
        private string scaredGhosts;
        private bool powerMusic;
        private int musicCounter;

        //Constructor
        public Controller(Maze maze, Random random, TextBox textBox1, TextBox textBox2)
        {
            this.maze = maze;
            this.random = random;
            this.textBox1 = textBox1;
            this.textBox2 = textBox2;

            score = 0;
            pacmanFrames = new List<Bitmap>(); //creating list of animation framse for pacman
            for (int i = 1; i < PACFRAMECOUNT+1; i++)//populating animation list
            {
                Bitmap frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("pacMan_" + i.ToString());
                pacmanFrames.Add(new Bitmap(frame));
            }

            ghoul1Frames = new List<Bitmap>(); //creating list of animation framse for ghoul1
            ghoul2Frames = new List<Bitmap>(); //creating list of animation framse for ghoul2
            ghoul3Frames = new List<Bitmap>(); //creating list of animation frames for ghoul3
            ghoul4Frames = new List<Bitmap>(); //creating list of animation frames for ghoul4

            for (int i = 0; i < GHOULFRAMECOUNT; i++)//populating animation lists
            {
                Bitmap frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoul" + i.ToString());
                ghoul1Frames.Add(new Bitmap(frame));

                frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoulB" + i.ToString());
                ghoul2Frames.Add(new Bitmap(frame));

                frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoulC" + i.ToString());
                ghoul3Frames.Add(new Bitmap(frame));

                frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoulD" + i.ToString());
                ghoul4Frames.Add(new Bitmap(frame));
            }

            pacman = new PacMan(pacmanFrames, maze, random, new Point(PACMANSTARTX, PACMANSTARTY), Direction.Left, 3, 0, 0);

            ghouls = new List<Ghoul>();
            ghouls.Add(new Ghoul(ghoul1Frames, maze, random, new Point(GHOULSTARTX, GHOULSTARTY), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul2Frames, maze, random, new Point(GHOULSTARTX + 1, GHOULSTARTY), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul3Frames, maze, random, new Point(GHOULSTARTX + 2, GHOULSTARTY), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul4Frames, maze, random, new Point(GHOULSTARTX + 3, GHOULSTARTY), Direction.Up, 2, 0, 0));

            ghoulStart = GHOULSTARTX;
            counter = 0;
            pacPower = false;

            dead = new SoundPlayer();
            dead.Stream = Properties.Resources.KibbleEat;
            gameOver = new SoundPlayer(Properties.Resources.EndGame);

            path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName); //https://stackoverflow.com/questions/14899422/how-to-navigate-a-few-folders-up
            wakkaSound = "/Resources/wakka.wav";
            wakkaPath = path + "/Resources/wakka.wav";
            scaredGhosts = path + "/Resources/pacman_intermission.wav";

            musicCounter = 0;
            powerMusic = false;
            Player = new WMPLib.WindowsMediaPlayer(); //https://docs.microsoft.com/en-us/windows/win32/wmp/embedding-the-windows-media-player-control-in-a-c--solution
            Player.URL = wakkaPath;
            powerPlayer = new WMPLib.WindowsMediaPlayer();
            powerPlayer.URL = scaredGhosts;
            powerPlayer.controls.stop();
            Player.settings.setMode("loop", true);
            powerPlayer.settings.setMode("loop", true);
        }

        //resets game
        public void Reset()
        {
            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Position = new Point(ghoulStart, 11);
                ghoulStart++;
            }

            pacman = null;
            pacman = new PacMan(pacmanFrames, maze, random, new Point(PACMANSTARTX, PACMANSTARTY), Direction.Left, 3, 0, 0);
            maze.Reset();
            score = 0;
            ghoulStart = GHOULSTARTX;
        }

        //returns sprites to starting positions
        public void StartNewLife()
        {
            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Position = new Point(ghoulStart, 11);
                ghoulStart++;
            }
            pacman.PacmanReset();
            ghoulStart = GHOULSTARTX;
        }

        //plays & changes background music
        public void BackgroundMusic()
        {
            if (powerMusic == true)
            {
                Player.controls.stop();
                powerPlayer.controls.play();
            }

            else if (maze.NKibbles != 0 && playerLose() == false && pacman.Dead1 == false)
            {
                Player.controls.play();
                powerPlayer.controls.stop();
            }

            else
            {
                Player.controls.stop();
                powerPlayer.controls.stop();
            }
        }

        //runs game
        public void PlayGame()
        {
            textBox2.Text = pacman.Lives.ToString().PadLeft(7, '0');
            counter++;
            maze.Draw();

            if (pacman.PowerUp() == true) //checks to see if pacman has collected a power up
            {
                pacPower = true;
            }

            if (pacPower == true) //changes ghoul state so that they can be eaten
            {
                foreach (Ghoul ghoul in ghouls)
                {
                    ghoul.Scared = true;
                    ghoul.ScaredGhost();
                }
                counter = 0;//counter for pacman power up
                pacPower = false;
                powerMusic = true;//changes background music
            }

            if (counter > 45)  //turns off pacman powerup mode
            {
                powerMusic = false;
                foreach (Ghoul ghoul in ghouls)
                {
                    ghoul.Scared = false;
                    ghoul.ScaredGhost();
                    ghoul.Jail = false; //enables ghoul movement in jail
                }
            }

            foreach (Ghoul ghoul in ghouls)
            {
                if (pacman.Dead1 == false && ghoul.Jail == false)
                {
                    if ((ghoul.Scared == true) && (counter % 2 == 0))//slows movement of ghouls by preventing movement every second frame
                    {

                    }
                    else
                    {
                        ghoul.Move();
                    }
                    ghoul.ChangeDirection();
                    ghoul.CheckForGaps();
                    ghoul.PacManPosition(pacman.Position.Y);
                }

                ghoul.Draw();
                ghoul.PacManPosition(pacman.Position.Y);

                if (pacman.HitOpponent(ghoul.Position)&& ghoul.Scared == true) //ghoul gets eaten
                {
                    ghoul.Dead();
                    pacman.EatGhost();
                    score += 10;
                }

                else if (pacman.HitOpponent(ghoul.Position) && pacman.Dead1 == false) //pacman dies
                {
                    pacman.Dead();
                }
            }

            if (pacman.Dead1 == false)
            {
                pacman.Move();
            }

            pacman.Draw();

            if (pacman.EatKibble() == true)  //redraws game if pacman eats kibble
            {
                maze.Draw();
                pacman.Draw();
                foreach (Ghoul ghoul in ghouls)
                {
                    ghoul.Draw();
                }
                score++;
                textBox1.Text = score.ToString().PadLeft(7, '0');
            }

            foreach (Ghoul ghoul in ghouls)
            {
                if (pacman.HitOpponent(ghoul.Position) && ghoul.Scared == true)
                {
                    ghoul.Dead();
                    pacman.EatGhost();
                    score += 10;
                }

                else if (pacman.HitOpponent(ghoul.Position))
                {
                    pacman.Dead();
                }
            }

            if (pacman.AniFrame == 23)//pacman dies after death animation completes
            {
                if (pacman.Lives != 0)
                {
                    StartNewLife();
                }
            }
            BackgroundMusic();
        }

        //sets direction of pacman
        public void SetPacManDirection(Direction direction)
        {
            pacman.Direction = direction;
            pacman.rotateSprite();
        }

        //checks if pacman has lost all lives
        public bool playerLose()
        {
            bool lose = false;
            if (pacman.Lives == 0)
            {
                lose = true;
                textBox2.Text = pacman.Lives.ToString().PadLeft(7, '0');
            }
            return lose;
        }

        //checks if pacman has collected all kibble
        public bool playerWin()
        {
            bool win = false;

            if (maze.NKibbles == 0)
            {
                win = true;
            }
            return win;
        }

        //checks to see if pacman has lost a life
        public bool pacManDead()
        {
            bool pacManDead = false;

            if (pacman.Dead1 == true)
            {
                pacManDead = true;
            }
            return pacManDead;
        }

        public int MusicCounter { get => musicCounter; set => musicCounter = value; }
        public bool PowerMusic { get => powerMusic; set => powerMusic = value; }
    }
}
