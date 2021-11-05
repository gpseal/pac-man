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
        private const int STARTLIVES = 3;
        private const int PACSTARTX = 10;
        private const int PACSTARTY = 13;
        private const int CELLS = 22;
        private const int FRAMECOUNT = 3;
        private const int RIGHT = 3;
        private const int LEFT = 0;
        private const int UP = 6;
        private const int DOWN = 9;
        private const int DEAD = 12;

        private bool powerUp;
        private int lives;

        private int rotation;
        private bool rotate;
        //private Point position;
        //private int aniFrame;
        //private int frameStart;
        //private int frameFin;
        //private Direction direction;
        //private int stringPos;
        private bool dead;
        private SoundPlayer pacDeath;

        public PacMan(List<Bitmap> frames, Maze maze, Random random, Point position, Direction direction, int frameFin, int aniframe, int frameStart)
            :base(frames, maze, position, direction, frameFin, aniframe, frameStart)
        {
            this.aniFrame = aniFrame;
            this.frameStart = frameStart;
            this.frameFin = frameFin;
            this.frames = frames;
            this.maze = maze;
            direction = Direction.Left;
            this.position = position;
            //position = new Point(PACSTARTX, PACSTARTY);

            //aniFrame = 0;
            //frameStart = 0;

            rotate = false;

            rotation = 0;

            dead = false;
            powerUp = false;
            lives = STARTLIVES;
            pacDeath = new SoundPlayer(Properties.Resources.collide);
        }


        public override void rotateSprite()
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
            rotate = false;
        }

        public bool EatKibble() //checks to see if pacman has landed on a square with kibble
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
            }
            return eat;
        }

        public void PacmanReset()
        {
            position = new Point(12, 13);
            direction = Direction.Left;
            dead = false;
            aniFrame = 0;
            frameStart = aniFrame;
            frameFin = aniFrame + FRAMECOUNT;
            //rotate = false;
        }

        public bool PowerUp() //checks to see if pacman has landed on a square with kibble
        {
            bool powerUp = false;
            if (maze.CurrentMap1[stringPos] == 'P')
            { 
                powerUp = true;
                maze.CurrentMap1[stringPos] = 'b';
            }
            return powerUp;
        }

        public void Dead()
        {
            pacDeath.Play();
            if (dead == false)
            {
                frameStart = DEAD;
                aniFrame = frameStart;
                frameFin = DEAD + 12;
                dead = true;
                lives--;
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
        public int AniFrame { get => aniFrame; set => aniFrame = value; }
        public int Lives { get => lives; set => lives = value; }
    }
}
