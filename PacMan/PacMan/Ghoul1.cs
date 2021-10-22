using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class Ghoul1 : Creature
    {
        private const int FRAMECOUNT = 2;
        private const int GHOULSTARTX = 10;
        private const int GHOULSTARTY = 9;

        private const int CELLS = 20;
        private Point position;
        private int frame;

        public Ghoul1(List<Bitmap> frames, Maze maze)
            : base(frames, maze)
        {
            this.frames = frames;
            this.maze = maze;

            position = new Point(GHOULSTARTX, GHOULSTARTY);

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
