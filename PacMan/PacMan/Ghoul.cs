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
        //private Point position;
        private int frame;
        //private Direction direction;
        private bool rotate;
        //private int aniFrame;
        //private int frameStart;
        //private int frameFin;
        private Random random;
        private int headX;
        private int headY;
        private int stringPos;
        private bool change;
        private int pacManYPos;

        public Ghoul(List<Bitmap> frames, Maze maze, Random random, Point position, Direction direction, int frameFin, int aniframe, int frameStart)
            : base(frames, maze, position, direction, frameFin, aniframe, frameStart)
        {
            this.aniFrame = aniFrame;
            this.frameStart = frameStart;
            this.frameFin = frameFin;
            this.frames = frames;
            this.maze = maze;
            this.position = position;
            this.random = random;
            //position = new Point(GHOULSTARTX, GHOULSTARTY);
            //direction = Direction.Up;
            this.direction = direction;
            frame = 0;
            stringPos = (position.Y * CELLS) + position.X;
            //aniFrame = 0;
            //frameStart = 0;
            rotate = false;
            change = false;

            //frameFin = 2;
        }



        //public override void Draw()
        //{
        //    aniFrame++;
        //    if (aniFrame == frameFin)
        //    {
        //        aniFrame = frameStart;
        //    }
        //    maze.Rows[position.Y].Cells[position.X].Value = frames[aniFrame];
        //}


        public override void rotateSprite()
        {
            if (rotate == true)
            {
                aniFrame = frame;
                frameStart = frame;
                frameFin = frame + FRAMECOUNT;
                rotate = false;
            }
        }

        //public void Move()
        //{
        //    //int headX = position.X;
        //    //int headY = position.Y;
        //    //stringPos = (headY * CELLS) + headX;

        //    ////change = true;

        //    //switch (direction)
        //    //{
        //    //    case Direction.Right:
        //    //        rotateSprite(RIGHT);

        //    //        if (stringPos == 240)
        //    //        {
        //    //            headX = headX - 19;
        //    //        }

        //    //        else if (maze.CurrentMap1[stringPos + 1] != 'w')
        //    //        {
        //    //            headX++;
        //    //        }

        //    //        else
        //    //        {
        //    //            change = true;
        //    //        }

        //    //        break;

        //    //    case Direction.Left:

        //    //        rotateSprite(LEFT);

        //    //        if (maze.CurrentMap1[stringPos - 1] != 'w')
        //    //        {
        //    //            headX--;
        //    //        }

        //    //        else
        //    //        {
        //    //            change = true;
        //    //        }

        //    //        break;

        //    //    case Direction.Up:

        //    //        rotateSprite(UP);

        //    //        if (maze.CurrentMap1[stringPos - 22] != 'w')
        //    //        {
        //    //            headY--;
        //    //        }

        //    //        else
        //    //        {
        //    //            change = true;
        //    //        }

        //    //        break;

        //    //    case Direction.Down:

        //    //        rotateSprite(DOWN); //9 represents start frame of downward animation sequence

        //    //        if (maze.CurrentMap1[stringPos + 22] != 'w' && maze.CurrentMap1[stringPos + 22] != 'j')
        //    //        {
        //    //            headY++;
        //    //        }

        //    //        else
        //    //        {
        //    //            change = true;
        //    //        }

        //    //        break;

        //    //    default:
        //    //        break;
        //    //}

        //    //position = new Point(headX, headY);
        //    changeDirection();
        //    checkForGaps();
        //}

        public void PacManPosition(int YPos)
        {
            pacManYPos = YPos;
        }

        private void findPacMan()  //check to see if pacman is above or below ghoul
        {
            if (position.Y > pacManYPos && maze.CurrentMap1[stringPos - 22] != 'w')
            {
                direction = Direction.Up;
            }

            if (position.Y < pacManYPos && maze.CurrentMap1[stringPos + 22] != 'w' && maze.CurrentMap1[stringPos] != 'j')  //forces ghouls to leave jail  at beginning of level
            {
                direction = Direction.Down;
            }

        }

        public void ChangeDirection()
        {
            int headX = position.X;
            int headY = position.Y;
            stringPos = (headY * CELLS) + headX;

            switch (direction)
            {
                case Direction.Right:
                    if (maze.CurrentMap1[stringPos + 1] == 'w')
                    {
                    change = true;
                    }
                    break;

                case Direction.Left:
                    if (maze.CurrentMap1[stringPos - 1] == 'w')
                    {
                    change = true;
                    }
                    break;

                case Direction.Up:
                    if (maze.CurrentMap1[stringPos - 22] == 'w')
                    {
                    change = true;
                    }
                    break;

                case Direction.Down:
                    if (maze.CurrentMap1[stringPos + 22] == 'w' || maze.CurrentMap1[stringPos + 22] == 'j')
                    {
                    change = true;
                    }
                    break;
            }

            if (change == true)
            {
                int newDirection = random.Next(4);

                switch (newDirection)
                {
                    case 0:
                        //if (maze.CurrentMap1[stringPos + 1] != 'w')
                        //{
                        change = false;
                        direction = Direction.Right;
                        //}
                        break;

                    case 1:
                        //if (maze.CurrentMap1[stringPos - 1] != 'w')
                        //{
                        change = false;
                        direction = Direction.Left;
                        //}
                        break;

                    case 2:
                        //if (maze.CurrentMap1[stringPos - 22] != 'w')
                        //{
                        change = false;
                        direction = Direction.Up;
                        //}
                        break;

                    case 3:
                        //if (maze.CurrentMap1[stringPos + 22] != 'w')
                        //{
                        change = false;
                        direction = Direction.Down;
                        //}
                        break;
                }
            }
            
            
            
        }

        public void CheckForGaps()  //enables ghosts to pick routes that are not in corners of maze
        {

            int newDirection = random.Next(4);
            findPacMan(); //ghouls will search for pacman position but only in wall gaps, not in maze corners

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



        public int StringPos { get => stringPos; set => stringPos = value; }
        public Point Position { get => position; set => position = value; }
    }
}
