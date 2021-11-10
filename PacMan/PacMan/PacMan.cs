/*
 * Creates and controls PacMan character.
 * Animates sprite and changes frames depending on direction of character.
 * Establishes if PacMan eats kibble, resets PacMan, kills PacMan, powers Up PacMan
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
    public class PacMan : Creature
    {
        //Constants
        private const int STARTLIVES = 3;
        private const int FRAMECOUNT = 3;

        //fields
        private const int RIGHT = 3;
        private const int LEFT = 0;
        private const int UP = 6;
        private const int DOWN = 9;
        private const int DEAD = 12;
        private int lives;
        private bool dead;
        private SoundPlayer pacDeath;
        private SoundPlayer eatKibble;
        private SoundPlayer eatGhost;

        //constructor
        public PacMan(List<Bitmap> frames, Maze maze, Random random, Point position, Direction direction, int frameFin, int aniFrame, int frameStart)
            :base(frames, maze, position, direction, frameFin, aniFrame, frameStart)
        {
            this.aniFrame = aniFrame;
            this.frameStart = frameStart;
            this.frameFin = frameFin;
            this.frames = frames;
            this.maze = maze;
            direction = Direction.Left;
            this.position = position;
            dead = false;
            lives = STARTLIVES;

            // sound effects
            pacDeath = new SoundPlayer(Properties.Resources.collide);
            eatKibble = new SoundPlayer(Properties.Resources.eat);
            eatGhost = new SoundPlayer(Properties.Resources.eatGhost);
        }

        //changes animation frames depending on what direction PacMan is facing
        public void rotateSprite()
        {
            switch (direction)
            {
                case Direction.Right:
                    frame = RIGHT;
                    break;

                case Direction.Left:
                    frame = LEFT;
                    break;

                case Direction.Up:
                    frame = UP;

                    break;

                case Direction.Down:
                    frame = DOWN;
                    break;

                default:
                    break;
            }
            aniFrame = frame;
            frameStart = frame;
            frameFin = frame + FRAMECOUNT;
        }

        //plays eat ghost sound effect
        public void EatGhost()
        {
            eatGhost.Play();
        }

        //checks to see if PacMan has landed on a square with kibble
        public bool EatKibble() 
        {
            bool eat = false;
            if ((maze.CurrentMap1[stringPos] == 'k')||
                (maze.CurrentMap1[stringPos] == 'l') || 
                (maze.CurrentMap1[stringPos] == 'i') || 
                (maze.CurrentMap1[stringPos] == 'c'))
            {
                eat = true;
                maze.CurrentMap1[stringPos] = 'b';
                maze.NKibbles--;
                eatKibble.Play();
            }
            return eat;
        }

        //resets position of Pacman
        public void PacmanReset()
        {
            position = new Point(12, 13);
            direction = Direction.Left;
            dead = false;
            aniFrame = 0;
            frameStart = aniFrame;
            frameFin = aniFrame + FRAMECOUNT;
        }

        //checks to see if pacman has landed on a square with a power up
        public bool PowerUp() 
        {
            bool powerUp = false;
            if (maze.CurrentMap1[stringPos] == 'P')
            { 
                powerUp = true;
                maze.CurrentMap1[stringPos] = 'b';
            }
            return powerUp;
        }

        //if pacman has been killed, plays death sequence
        public void Dead()
        {
            if (dead == false)
            {
                frameStart = DEAD;
                aniFrame = frameStart;
                frameFin = DEAD + 12;
                dead = true;
                lives--;
                pacDeath.Play();
            }
        }

        public Direction Direction { get => direction; set => direction = value; }
        public Point Position { get => position; set => position = value; }
        public int StringPos { get => stringPos; set => stringPos = value; }
        public bool Dead1 { get => dead; set => dead = value; }
        public int AniFrame { get => aniFrame; set => aniFrame = value; }
        public int Lives { get => lives; set => lives = value; }
    }
}
