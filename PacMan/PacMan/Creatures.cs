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
        protected const int CELLS = 22;

        //fields
        protected List<Bitmap> frames;
        protected Maze maze;
        protected Point position;
        protected int stringPos;
        protected Direction direction;
        protected bool change;
        //private int DOWN;
        //private int RIGHT;
        //private int LEFT;
        //private int UP;
        protected int frame;
        protected bool rotate;
        protected int aniFrame;
        protected int frameFin;
        protected int frameStart;


        //constructor
        public Creature(List<Bitmap> frames, Maze maze, Point position, Direction direction, int frameFin, int aniframe, int frameStart)//do this with aniframe frameFin frameStart
        {
            this.aniFrame = aniFrame;
            this.frameStart = frameStart;
            this.frameFin = frameFin;
            this.position = position;
            this.frames = frames;
            this.maze = maze;
            this.direction = direction;
        }

        public void Draw()
        {
            aniFrame++;
            if (aniFrame == frameFin)
            {
                aniFrame = frameStart;
            }
        maze.Rows[position.Y].Cells[position.X].Value = frames[aniFrame];
        }


        public abstract void rotateSprite();

        public void Move()
        {
            int headX = position.X;
            int headY = position.Y;
            stringPos = (headY * CELLS) + headX;

            //change = true;

            switch (direction)
            {
                case Direction.Right:
                    //rotateSprite(RIGHT);

                    if (stringPos == 240)
                    {
                        headX = headX - 19;
                    }

                    else if (maze.CurrentMap1[stringPos + 1] != 'w')
                    {
                        headX++;
                    }

                    else
                    {
                        change = true;
                    }

                    break;

                case Direction.Left:

                    //rotateSprite(LEFT);


                    if (stringPos == 221)
                    {
                        headX = headX + 19;
                    }

                    else if (maze.CurrentMap1[stringPos - 1] != 'w')
                    {
                        headX--;
                    }

                    else
                    {
                        change = true;
                    }

                    break;

                case Direction.Up:

                    //rotateSprite(UP);

                    if (maze.CurrentMap1[stringPos - 22] != 'w')
                    {
                        headY--;
                    }

                    else
                    {
                        change = true;
                    }

                    break;

                case Direction.Down:

                    //rotateSprite(DOWN); //9 represents start frame of downward animation sequence

                    if (maze.CurrentMap1[stringPos + 22] != 'w' && maze.CurrentMap1[stringPos + 22] != 'j')
                    {
                        headY++;
                    }

                    else
                    {
                        change = true;
                    }

                    break;

                default:
                    break;
            }

            position = new Point(headX, headY);
        }



        //public bool HitOpponent(Point opponentPosition)
        //{
        //    bool hitOpponent = false;

        //    if (positions[0] == frogPosition)
        //    {
        //        eatenfrog = true;
        //    }

        //    return hitOpponent;
        //}

    }


}
