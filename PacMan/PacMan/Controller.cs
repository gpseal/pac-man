using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class Controller
    {
        private const int PACFRAMECOUNT = 11;
        private const int GHOULFRAMECOUNT = 2;

        private Random random;
        private PacMan pacman;
        private Ghoul ghoul1;
        private Ghoul ghoul2;
        private Ghoul ghoul3;
        private Ghoul ghoul4;
        private Maze maze;
        private Bitmap frame;
        private List<Bitmap> pacmanFrames;
        private List<Bitmap> ghoul1Frames;
        private List<Bitmap> ghoul2Frames;
        private List<Bitmap> ghoul3Frames;
        private List<Bitmap> ghoul4Frames;

        public Controller(Maze maze, Random random)
        {
            this.maze = maze;
            this.random = random;

            pacmanFrames = new List<Bitmap>(); //creating list of animation framse for pacman
            for (int i = 0; i < PACFRAMECOUNT+1; i++)
            {
                Bitmap frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("pacMan" + i.ToString());
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



            pacman = new PacMan(pacmanFrames, maze, random);

            ghoul1 = new Ghoul(ghoul1Frames, maze, random, new Point(12, 11));
            ghoul2 = new Ghoul(ghoul2Frames, maze, random, new Point(11, 11));
            ghoul3 = new Ghoul(ghoul3Frames, maze, random, new Point(10, 11));
            ghoul4 = new Ghoul(ghoul4Frames, maze, random, new Point(9, 11));
        }

        public void StartNewGame()
        {


        }

        public void PlayGame()
        {

            pacman.Move();
            
            maze.Draw();
            
            pacman.Draw();
            ghoul1.Move();
            ghoul1.Draw();
            ghoul2.Draw();
            ghoul2.Move();
            ghoul3.Move();
            ghoul3.Draw();
            ghoul4.Move();
            ghoul4.Draw();
        }

        public void SetPacManDirection(Direction direction)
        {
            pacman.Rotate = true;
            pacman.Direction = direction;
        }



    }
}
