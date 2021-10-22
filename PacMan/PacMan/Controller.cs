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
        private const int PACFRAMECOUNT = 3;
        private const int GHOULFRAMECOUNT = 2;

        private PacMan pacman;
        private Ghoul1 ghoul1;
        private Maze maze;
        private Bitmap frame;
        private List<Bitmap> pacmanFrames;
        private List<Bitmap> ghoul1Frames;

        public Controller(Maze maze, Random random)
        {
            this.maze = maze;

            pacmanFrames = new List<Bitmap>(); //creating list of animation framse for pacman
            for (int i = 0; i < PACFRAMECOUNT; i++)
            {
                Bitmap frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("pacMan" + i.ToString());
                pacmanFrames.Add(new Bitmap(frame));
            }

            ghoul1Frames = new List<Bitmap>(); //creating list of animation framse for ghouls
            for (int i = 0; i < GHOULFRAMECOUNT; i++)
            {
                Bitmap frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoul" + i.ToString());
                ghoul1Frames.Add(new Bitmap(frame));
            }

            pacman = new PacMan(pacmanFrames, maze);

            ghoul1 = new Ghoul1(ghoul1Frames, maze);
        }

        public void StartNewGame()
        {


        }

        public void PlayGame()
        {
            maze.Draw();
            pacman.Draw();
            ghoul1.Draw();
        }

    }
}
