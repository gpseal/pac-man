using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public abstract class Creature
    {
        //fields
        protected List<Bitmap> frames;
        protected Maze maze;

        //constructor
        public Creature(List<Bitmap> frames, Maze maze)
        {

            this.frames = frames;
            this.maze = maze;
        }

        public abstract void Draw();
    }
}
