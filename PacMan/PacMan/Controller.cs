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
        private const int GHOULFRAMECOUNT = 2;

        private Random random;
        private PacMan pacman;
        //private Ghoul ghoul1;
        //private Ghoul ghoul2;
        //private Ghoul ghoul3;
        //private Ghoul ghoul4;
        private Maze maze;
        private Bitmap frame;
        private List<Bitmap> pacmanFrames;
        private List<Bitmap> ghoul1Frames;
        private List<Bitmap> ghoul2Frames;
        private List<Bitmap> ghoul3Frames;
        private List<Bitmap> ghoul4Frames;
        private List<Ghoul> ghouls;
        private TextBox textBox1;
        private int score;
        private int ghoulStart;


        public Controller(Maze maze, Random random, TextBox textBox1)
        {
            this.maze = maze;
            this.random = random;
            this.textBox1 = textBox1;
            score = 0;
            textBox1.Text = score.ToString();
            pacmanFrames = new List<Bitmap>(); //creating list of animation framse for pacman
            for (int i = 1; i < PACFRAMECOUNT+1; i++)
            {
                Bitmap frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("pacMan_" + i.ToString());
                pacmanFrames.Add(new Bitmap(frame));
            }

            ghoul1Frames = new List<Bitmap>(); //creating list of animation framse for ghouls
            ghoul2Frames = new List<Bitmap>(); //creating list of animation framse for ghouls
            ghoul3Frames = new List<Bitmap>(); //creating list of animation framse for ghouls
            ghoul4Frames = new List<Bitmap>(); //creating list of animation framse for ghouls


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



            pacman = new PacMan(pacmanFrames, maze, random, new Point(12, 13), Direction.Left);

            ghouls = new List<Ghoul>();
            ghouls.Add(new Ghoul(ghoul1Frames, maze, random, new Point(12, 11), Direction.Up));
            ghouls.Add(new Ghoul(ghoul2Frames, maze, random, new Point(11, 11), Direction.Up));
            ghouls.Add(new Ghoul(ghoul3Frames, maze, random, new Point(10, 11), Direction.Up));
            ghouls.Add(new Ghoul(ghoul4Frames, maze, random, new Point(9, 11), Direction.Up));

            ghoulStart = 9;
            //ghoul2 = new Ghoul(ghoul2Frames, maze, random, new Point(11, 11))
            //ghoul3 = new Ghoul(ghoul3Frames, maze, random, new Point(10, 11))
            //ghoul4 = new Ghoul(ghoul4Frames, maze, random, new Point(9, 11))
        }

        public void StartNewGame()
        {
            pacman = null;
            pacman = new PacMan(pacmanFrames, maze, random, new Point(12, 13), Direction.Left);
            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Position = new Point(ghoulStart, 11);
                ghoulStart++;
            }

            ghoulStart = 9;
        }

        public void PlayGame()
        {
            maze.Draw();
            foreach (Ghoul ghoul in ghouls)
            {

                ghoul.Draw();
                ghoul.PacManPosition(pacman.Position.Y);
                if (pacman.HitOpponent(ghoul.Position))
                {
                    pacman.Dead1 = true;
                    pacman.Dead();
                }

                if (pacman.Dead1 == false)
                {
                    ghoul.Move();
                    ghoul.ChangeDirection();
                    ghoul.CheckForGaps();
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
                textBox1.Text = score.ToString();
            }

            foreach (Ghoul ghoul in ghouls)
            {
                

                if (pacman.HitOpponent(ghoul.Position))
                {
                    pacman.Dead1 = true;
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



    }
}
