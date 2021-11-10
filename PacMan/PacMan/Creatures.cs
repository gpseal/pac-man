/*
 * Parent Class of PacMan and Ghouls.
 * Draws and moves characters while detecting walls.  Detects if PacMan and a ghoul have come into contact
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public abstract class Creature
    {
        //Constants
        protected const int CELLS = 22;

        //fields
        protected List<Bitmap> frames;
        protected Maze maze;
        protected Point position;
        protected int stringPos;
        protected Direction direction;
        protected bool change;
        protected int frame;
        protected bool rotate;
        protected int aniFrame;
        protected int frameFin;
        protected int frameStart;

        //constructor
        public Creature(List<Bitmap> frames, Maze maze, Point position, Direction direction, int frameFin, int aniFrame, int frameStart)//do this with aniframe frameFin frameStart
        {
            this.aniFrame = aniFrame;
            this.frameStart = frameStart;
            this.frameFin = frameFin;
            this.position = position;
            this.frames = frames;
            this.maze = maze;
            this.direction = direction;
        }

        //draws creature
        public void Draw()
        {
            aniFrame++;
            if (aniFrame == frameFin)
            {
                aniFrame = frameStart;
            }
        maze.Rows[position.Y].Cells[position.X].Value = frames[aniFrame];
        }

        //moves creature
        public void Move()
        {
            int headX = position.X;
            int headY = position.Y;
            stringPos = (headY * CELLS) + headX; //finds equivalent position in maze string

            switch (direction)
            {
                case Direction.Right:

                    if (maze.CurrentMap1[stringPos + 1] == 'p') //teleport square
                    {
                        headX = headX - 19;
                    }

                    else if (maze.CurrentMap1[stringPos + 1] != 'w')//allows creasture to move right if a wall is not in the next square
                    {
                        headX++;
                    }

                    break;

                case Direction.Left:

                    if (maze.CurrentMap1[stringPos - 1] == 'p') //teleport square
                    {
                        headX = headX + 19;

                    }

                    else if (maze.CurrentMap1[stringPos - 1] != 'w') //allows creasture to move left if a wall is not in the next square
                    {
                        headX--;
                    }

                    break;

                case Direction.Up:

                    if (maze.CurrentMap1[stringPos - 22] != 'w') //allows creasture to move up if a wall is not in the square above
                    {
                        headY--;
                    }

                    break;

                case Direction.Down:

                    if ((maze.CurrentMap1[stringPos + 22] != 'w') && (maze.CurrentMap1[stringPos + 22] != 'j'))//allows creasture to move down if a wall or jail is not in the square below
                    {
                        headY++;
                    }

                    break;

                default:
                    break;
            }

            position = new Point(headX, headY);
        }

        //checks to see if creature is on the same square as an opponent
        public bool HitOpponent(Point opponentPosition)
        {
            bool hitOpponent = false;

            if (position == opponentPosition)
            {
                hitOpponent = true;
            }
            return hitOpponent;
        }

    }


}
