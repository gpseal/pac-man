using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public class Controller
    {
        private const int PACFRAMECOUNT = 24;
        private const int GHOULFRAMECOUNT = 4;

        private Random random;
        private PacMan pacman;
        //private Ghoul ghoul1;
        //private Ghoul ghoul2;
        //private Ghoul ghoul3;
        //private Ghoul ghoul4;
        private Maze maze;
        private Bitmap frame;
        private int pacManLives;
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

        public Controller(Maze maze, Random random, TextBox textBox1, TextBox textBox2)
        {
            pacManLives = 3;
            this.maze = maze;
            this.random = random;
            this.textBox1 = textBox1;
            this.textBox2 = textBox2;
            score = 0;
            textBox2.Text = "test";
            //textBox1.Text = score.ToString();
            pacmanFrames = new List<Bitmap>(); //creating list of animation framse for pacman
            for (int i = 1; i < PACFRAMECOUNT+1; i++)
            {
                Bitmap frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("pacMan_" + i.ToString());
                pacmanFrames.Add(new Bitmap(frame));
            }

            ghoul1Frames = new List<Bitmap>(); //creating list of animation framse for ghouls
            ghoul2Frames = new List<Bitmap>(); //creating list of animation framse for ghouls
            ghoul3Frames = new List<Bitmap>(); //creating list of animation frames for ghouls
            ghoul4Frames = new List<Bitmap>(); //creating list of animation frames for ghouls


            for (int i = 0; i < GHOULFRAMECOUNT; i++)
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



            pacman = new PacMan(pacmanFrames, maze, random, new Point(12, 13), Direction.Left, 3, 0, 0);

            ghouls = new List<Ghoul>();
            ghouls.Add(new Ghoul(ghoul1Frames, maze, random, new Point(12, 11), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul2Frames, maze, random, new Point(11, 11), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul3Frames, maze, random, new Point(10, 11), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul4Frames, maze, random, new Point(9, 11), Direction.Up, 2, 0, 0));

            ghoulStart = 9;
            counter = 0;
            pacPower = false;
        }

        public void StartNewGame()
        {

            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Position = new Point(ghoulStart, 11);
                ghoulStart++;
            }
            //pacman = new PacMan(pacmanFrames, maze, random, new Point(12, 13), Direction.Left, 3, 0, 0);
            pacman.PacmanReset();


            ghoulStart = 9;
        }

        public void PlayGame()
        {
            textBox2.Text = pacman.Lives.ToString().PadLeft(7, '0');

            counter++;
            maze.Draw();

            if (pacman.PowerUp() == true)
            {
                pacPower = true;
            }

            if (pacPower == true)
            {

                foreach (Ghoul ghoul in ghouls)
                {
                    ghoul.Scared = true;
                    ghoul.ScaredGhost();
                }


                counter = 0;
                pacPower = false;
            }

            if (counter > 45)
            {
                foreach (Ghoul ghoul in ghouls)
                {
                    ghoul.Scared = false;
                    ghoul.ScaredGhost();
                    ghoul.Jail = false;
                }

            }


            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Draw();
                ghoul.PacManPosition(pacman.Position.Y);

                if (pacman.HitOpponent(ghoul.Position)&& ghoul.Scared == true)
                {
                    ghoul.Dead();
                }

                else if (pacman.HitOpponent(ghoul.Position) && pacman.Dead1 == false)
                {
                    //pacman.Dead1 = true;
                    pacman.Dead();
                }

                if (pacman.Dead1 == false && ghoul.Jail == false)
                {
                    if ((ghoul.Scared == true) && (counter%2 == 0))
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
                
            }

            pacman.Draw();
            if (pacman.Dead1 == false)
            {
                pacman.Move();
            }

            if (pacman.EatKibble() == true)
            {
                score++;

                textBox1.Text = score.ToString().PadLeft(7, '0');
            }

            foreach (Ghoul ghoul in ghouls)
            {
                if (pacman.HitOpponent(ghoul.Position) && ghoul.Scared == true)
                {
                    ghoul.Dead();
                }

                else if (pacman.HitOpponent(ghoul.Position))
                {
                    //pacman.Dead1 = true;
                    pacman.Dead();
                }
            }

            if (pacman.AniFrame == 23)
            {
                StartNewGame();
            }
        }

        public void SetPacManDirection(Direction direction)
        {
            pacman.Rotate = true;
            pacman.Direction = direction;
            pacman.rotateSprite();
        }


        public bool pacManDead()
        {
            bool pacManDead = false;

            if (pacman.Dead1 == true)
            {
                pacManDead = true;
            }

            return pacManDead;
        }
    }
}
