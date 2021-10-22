using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class PacMan : Creature
    {
        private const int PACSTARTX = 10;
        private const int PACSTARTY = 12;
        private const int CELLS = 20;
        private const int FRAMECOUNT = 3;

        private Point position;
        private int frame;

        public PacMan(List<Bitmap> frames, Maze maze)
            :base(frames, maze)
        {
            this.frames = frames;
            this.maze = maze;

            position = new Point(PACSTARTX, PACSTARTY);

            frame = 0;
        }

        public override void Draw()
        {
            frame++;
            if (frame == FRAMECOUNT)
            {
                frame = 0;
            }
            maze.Rows[position.Y].Cells[position.X].Value = frames[frame];
        }
    }
}
