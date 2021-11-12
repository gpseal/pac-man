/*
 * controls events within each timer tick of the game
 * Resets game, begins new life, checks to see if game has been completed
 */

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
        //Constants
        private const int PACFRAMECOUNT = 24;
        private const int PACMANSTARTX = 11;
        private const int PACMANSTARTY = 13;

        //fields
        private Random random;
        private Maze maze;
        private PacMan pacman;
        private GhoulManager ghoulManager;
        private List<Bitmap> pacmanFrames;
        private TextBox textBox1;
        private TextBox textBox2;
        private int score;
        private int counter;
        private int startCounter;
        private bool pacPower;
        private Sound sound;
        private int level;

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

            pacman = new PacMan(pacmanFrames, maze, random, new Point(PACMANSTARTX, PACMANSTARTY), Direction.Left, 3, 0, 0);

            ghoulManager = new GhoulManager(maze, random);

            counter = 0;
            pacPower = false;
            sound = new Sound();
            startCounter = 0;
            level = 1;
        }

        //resets game
        public void Reset()
        {
            level = 1;
            sound.PowerMusic1 = false;
            pacPower = false;
            ghoulManager.reset();
            pacman = null;
            pacman = new PacMan(pacmanFrames, maze, random, new Point(PACMANSTARTX, PACMANSTARTY), Direction.Left, 3, 0, 0);
            maze.Reset(level);
            score = 0;
            startCounter = 0;
        }

        public void NextLevel()
        {
            sound.PowerMusic1 = false;
            pacPower = false;
            pacman.PacmanReset();
            maze.Reset(level);
            ghoulManager.reset();
            startCounter = 0;
        }

        //returns sprites to starting positions
        public void StartNewLife()
        {
            ghoulManager.reset();
            pacman.PacmanReset();
        }
        
        //runs game
        public ErrorMessage PlayGame()
        {
            textBox2.Text = pacman.Lives.ToString().PadLeft(7, '0');
            counter++;
            maze.Draw();

            if (startCounter == 0) //stops movement before start music plays
            {
                pacman.Draw();
                ghoulManager.Draw();
            }

            else
            {
                if (pacPower == true) //changes ghoul state so that they can be eaten
                {
                    ghoulManager.Scared();
                    counter = 0;//counter for PacMan power up
                    pacPower = false;
                    sound.PowerMusic1 = true;//changes background music
                }

                if (counter > 45)  //turns off PacMan powerup mode
                {
                    sound.PowerMusic1 = false;
                    ghoulManager.NotScared();
                }

                ghoulManager.MoveGhoul(pacman.Dead1, counter, pacman.Position.Y, pacman);

                if (pacman.Dead1 == false)
                {
                    pacman.Move();
                }

                pacman.Draw();

                if (pacman.EatKibble() == true)  //redraws game if PacMan eats kibble
                {
                    reDraw();
                    score++;
                    textBox1.Text = score.ToString().PadLeft(7, '0');
                }

                if (pacman.PowerUp() == true) //checks to see if PacMan has collected a power up
                {
                    pacPower = true;
                    reDraw();
                }

                ghoulManager.CheckHit(pacman);

                if (pacman.AniFrame == 23)//PacMan dies after death animation completes
                {
                    if (pacman.Lives != 0)
                    {
                        StartNewLife();
                    }
                }
                Music();
            }

            startCounter++;

            ErrorMessage message = ErrorMessage.noError;

            if (levelComplete() == true)
            {
                message = ErrorMessage.completeLevel;
            }

            if (playerWin() == true)
            {
                message = ErrorMessage.playWins;
            }

            if (playerLose() == true)
            {
                message = ErrorMessage.playerLoses;
            }
            return message;
        }

        //sets direction of PacMan
        public void SetPacManDirection(Direction direction)
        {
            pacman.Direction = direction;
            pacman.rotateSprite();
        }

        //checks if PacMan has lost all lives
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

        //checks if PacMan has collected all kibble
        public bool playerWin()
        {
            bool win = false;

            if (level == 2)
            {
                if (maze.NKibbles == 0)
                {
                    sound.PowerMusic1 = false;
                    win = true;
                }
            }

            return win;
        }

        public bool levelComplete()
        {
            bool complete = false;

            if (level < 2 && maze.NKibbles == 0)
            {
                sound.PowerMusic1 = false;
                level++;
                NextLevel();

                complete = true;
            }

            return complete;
        }

        //checks to see if PacMan has lost a life
        public bool pacManDead()
        {
            bool pacManDead = false;

            if (pacman.Dead1 == true)
            {
                pacManDead = true;
            }
            return pacManDead;
        }

        //redraws map and sprites
        private void reDraw()
        {
            maze.Draw();
            pacman.Draw();
            ghoulManager.Draw();
        }

        //controls what background music/effects should be played
        public void Music()
        {
            sound.BackgroundMusic(maze.NKibbles, playerLose(), pacman.Dead1);
        }

        public int Level { get => level; set => level = value; }

    }
}
