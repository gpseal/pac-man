/*
 * Creates and controls Ghoul character.
 * Animates sprite and moves around the playing area
 * Establishes if PacMan is powered up, and if Ghoul is eaten by PacMan.
 */

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
        //Constants
        private const int FRAMECOUNT = 2;

        //fields
        private Random random;
        private int stringPos;
        private bool change;
        private int pacManYPos;
        private bool scared;
        private bool jail;
        private Point startPosition;

        //constructor
        public Ghoul(List<Bitmap> frames, Maze maze, Random random, Point position, Direction direction, int frameFin, int aniframe, int frameStart)
            : base(frames, maze, position, direction, frameFin, aniframe, frameStart)
        {
            this.frameStart = frameStart;
            this.frameFin = frameFin;
            startPosition = position;
            this.random = random;
            stringPos = (position.Y * CELLS) + position.X;
            change = false;
            scared = false;
            jail = false;
        }

        //resets ghoul if eaten by pacman
        public void Dead()
        {
            aniFrame = 0;
            frameStart = 0;
            frameFin = frameStart + FRAMECOUNT;
            position = startPosition;
            scared = false;
            jail = true;
        }
        
        // activates if pacman eats a power up, changes animation frames depending on ghoul status
        public void ScaredGhost()
        {
            if (scared == true)
            {
                aniFrame = 2;
                frameStart = 2;
                frameFin = frameStart + FRAMECOUNT;
            }

            else
            {
                aniFrame = 0;
                frameStart = 0;
                frameFin = frameStart + FRAMECOUNT;
            }
        }

        //gets pacmans y position
        public void PacManPosition(int YPos)
        {
            pacManYPos = YPos;
        }

        //check to see if pacman is above or below ghoul, sets direction accordingly
        private void findPacMan()  
        {
            if ((position.Y > pacManYPos) && (maze.CurrentMap1[stringPos - 22] != 'w'))
            {
                direction = Direction.Up;
            }

            if ((position.Y < pacManYPos) && (maze.CurrentMap1[stringPos + 22] != 'w') && (maze.CurrentMap1[stringPos] != 'j'))  //forces ghouls to leave jail  at beginning of level
            {
                direction = Direction.Down;
            }

        }

        //changes direction that ghoul is moving
        public void ChangeDirection()
        {
            int headX = position.X;
            int headY = position.Y;
            stringPos = (headY * CELLS) + headX;

            switch (direction)                                         //checking to see if direction change is necessary (change if a wall is in the ghouls immediate trajectory)
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
                    if ((maze.CurrentMap1[stringPos + 22] == 'w') || (maze.CurrentMap1[stringPos + 22] == 'j'))
                    {
                    change = true;
                    }
                    break;
            }

            if (change == true)
            {
                int newDirection = random.Next(4);

                switch (newDirection)                               //sets new direction if necessary
                {
                    case 0:
                        change = false;
                        direction = Direction.Right;
                        break;

                    case 1:
                        change = false;
                        direction = Direction.Left;
                        break;

                    case 2:
                        change = false;
                        direction = Direction.Up;
                        break;

                    case 3:
                        change = false;
                        direction = Direction.Down;
                        break;
                }
            }
        }

        //enables ghosts to pick routes that are not in corners of maze
        public void CheckForGaps()  
        {
            int newDirection = random.Next(4);                      //in each timer tick, ghouls will use random number to set direction, if no wall is present,
                                                                    //ghoul will move in this direction, otherwise continue on their path
            findPacMan();                                           //ghoul will set y direction to find pacman, but will only move in selected direction if no wall is present
                                                                    // and depending on the result of the below switch

            switch (newDirection)
            {
                case 0:
                    if ((maze.CurrentMap1[stringPos + 1] != 'w') && (direction != Direction.Left))
                    {
                        direction = Direction.Right;
                    }
                    break;

                case 1:
                    if ((maze.CurrentMap1[stringPos - 1] != 'w') && (direction != Direction.Right))
                    {
                        direction = Direction.Left;
                    }
                    break;

                case 2:
                    if ((maze.CurrentMap1[stringPos - 22] != 'w') && (direction != Direction.Down))
                    {
                        direction = Direction.Up;
                    }
                    break;

                case 3:
                    if ((maze.CurrentMap1[stringPos + 22] != 'w') && (direction != Direction.Up))
                    {
                        direction = Direction.Down;
                    }
                    break;
            }

        }

        public int StringPos { get => stringPos; set => stringPos = value; }
        public Point Position { get => position; set => position = value; }
        public bool Scared { get => scared; set => scared = value; }
        public bool Jail { get => jail; set => jail = value; }
    }
}
