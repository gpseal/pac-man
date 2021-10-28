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
        private const int PACSTARTY = 13;
        private const int CELLS = 22;
        private const int FRAMECOUNT = 3;
        private const int RIGHT = 3;
        private const int LEFT = 0;
        private const int UP = 6;
        private const int DOWN = 9;
        private const int DEAD = 12;



        private int rotation;
        private bool rotate;
        private Point position;
        private int aniFrame;
        private int frameStart;
        private int frameFin;
        private Direction direction;
        private int stringPos;
        private bool dead;

        public PacMan(List<Bitmap> frames, Maze maze, Random random)
            :base(frames, maze)
        {
            this.frames = frames;
            this.maze = maze;
            direction = Direction.Left;

            position = new Point(PACSTARTX, PACSTARTY);

            aniFrame = 0;
            frameStart = 0;
            frameFin = FRAMECOUNT;

            rotate = false;

            rotation = 0;

            dead = false;
        }

        public override void Draw()
        {
            aniFrame++;
            if (aniFrame == frameFin)
            {
                aniFrame = frameStart;
            }
            maze.Rows[position.Y].Cells[position.X].Value = frames[aniFrame];
        }

        public override void Move()
        {
            int headX = position.X;
            int headY = position.Y;
            stringPos = (headY * CELLS) + headX;
            maze.CurrentMap1[stringPos] = 'b';


            switch (direction)
            {
                case Direction.Right:
                    rotateSprite(RIGHT);

                    if (stringPos == 240)
                    {
                        headX = headX-19;
                    }

                    else if (maze.CurrentMap1[stringPos + 1] != 'w')
                    {
                        headX++;
                    }
                    break;

                case Direction.Left:

                    rotateSprite(LEFT);

                    if (maze.CurrentMap1[stringPos-1] != 'w')
                    {
                        headX--;
                    }

                    
                    break;

                case Direction.Up:

                    rotateSprite(UP);

                    if (maze.CurrentMap1[stringPos - 22] != 'w')
                    {
                        headY--;
                    }
                        
                    break;

                case Direction.Down:

                    rotateSprite(DOWN); //9 represents start frame of downward animation sequence

                    if (maze.CurrentMap1[stringPos + 22] != 'w')
                    {
                        headY++;
                    }
                    break;

                default:
                    break;
            }

            position = new Point(headX, headY);


        }

        public void Dead()
        {
            frameStart = DEAD;
            frameFin = DEAD + 12;
            //dead = true;
        }

        private void rotateSprite(int frame)
        {
            if (rotate == true)
            {
                aniFrame = frame;
                frameStart = frame;
                frameFin = frame + FRAMECOUNT;
                rotate = false;
            }
        }

        public bool HitOpponent(Point opponentPosition)
        {
            bool hitOpponent = false;

            if (position == opponentPosition)
            {
                hitOpponent = true;
            }

            return hitOpponent;
        }

        public Direction Direction { get => direction; set => direction = value; }
        public bool Rotate { get => rotate; set => rotate = value; }
        public Point Position { get => position; set => position = value; }
        public int StringPos { get => stringPos; set => stringPos = value; }
        public bool Dead1 { get => dead; set => dead = value; }
    }
}
