using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class Ghoul : Creature
    {
        private const int FRAMECOUNT = 2;
        private const int GHOULSTARTX = 10;
        private const int GHOULSTARTY = 9;
        private const int RIGHT = 2;
        private const int LEFT = 0;
        private const int UP = 4;
        private const int DOWN = 6;

        private const int CELLS = 22;
        private Point position;
        private int frame;
        private Direction direction;
        private bool rotate;
        private int aniFrame;
        private int frameStart;
        private int frameFin;
        private Random random;
        private int stringPos;
        private bool change;

        public Ghoul(List<Bitmap> frames, Maze maze, Random random, Point position)
            : base(frames, maze)
        {
            this.frames = frames;
            this.maze = maze;
            this.position = position;
            this.random = random;
            //position = new Point(GHOULSTARTX, GHOULSTARTY);
            direction = Direction.Up;
            frame = 0;

            aniFrame = 0;
            frameStart = 0;
            frameFin = FRAMECOUNT;

            rotate = false;

            change = false;
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

        public void Move()
        {
            int headX = position.X;
            int headY = position.Y;
            stringPos = (headY * CELLS) + headX;
            //maze.CurrentMap1[stringPos] = 'b';

            change = true;
            checkForGaps();

            switch (direction)
            {
                case Direction.Right:
                    rotateSprite(RIGHT);

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
                        changeDirection();
                    }

                    break;

                case Direction.Left:

                    rotateSprite(LEFT);

                    if (maze.CurrentMap1[stringPos - 1] != 'w')
                    {
                        headX--;
                    }

                    else
                    {
                        change = true;
                        changeDirection();
                    }

                    break;

                case Direction.Up:

                    rotateSprite(UP);

                    if (maze.CurrentMap1[stringPos - 22] != 'w')
                    {
                        headY--;
                    }

                    else
                    {
                        change = true;
                        changeDirection();
                    }

                    break;

                case Direction.Down:

                    rotateSprite(DOWN); //9 represents start frame of downward animation sequence

                    if (maze.CurrentMap1[stringPos + 22] != 'w')
                    {
                        headY++;
                    }

                    else
                    {
                        change = true;
                        changeDirection();
                    }

                    break;

                default:
                    break;
            }

            position = new Point(headX, headY);
        }

        private void changeDirection()
        {
            do
            {
                int newDirection = random.Next(4);

                switch (newDirection)
                {
                    case 0:
                    if (maze.CurrentMap1[stringPos + 1] != 'w')
                    {
                        change = false;
                        direction = Direction.Right;
                    }
                    break;

                    case 1:
                        if (maze.CurrentMap1[stringPos - 1] != 'w')
                        {
                            change = false;
                            direction = Direction.Left;
                        }
                        break;

                    case 2:
                        if (maze.CurrentMap1[stringPos - 22] != 'w')
                        {
                            change = false;
                            direction = Direction.Up;
                        }
                        break;

                    case 3:
                        if (maze.CurrentMap1[stringPos + 22] != 'w')
                        {
                            change = false;
                            direction = Direction.Down;
                        }
                        break;
                }


            } while (change == true);
            
            
        }

        private void checkForGaps()  //enables ghosts to pick routes that are not in corners of maze
        {

            int newDirection = random.Next(4);

            switch (newDirection)
            {
                case 0:
                    if ((maze.CurrentMap1[stringPos + 1] != 'w') && direction != Direction.Left)
                    {
                        direction = Direction.Right;
                    }
                    break;

                case 1:
                    if ((maze.CurrentMap1[stringPos - 1] != 'w') && direction != Direction.Right)
                    {
                        direction = Direction.Left;
                    }
                    break;

                case 2:
                    if ((maze.CurrentMap1[stringPos - 22] != 'w') && direction != Direction.Down)
                    {
                        direction = Direction.Up;
                    }
                    break;

                case 3:
                    if ((maze.CurrentMap1[stringPos + 22] != 'w') && direction != Direction.Up)
                    {
                        direction = Direction.Down;
                    }
                    break;
            }

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
    }
}
