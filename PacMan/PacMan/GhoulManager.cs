/*
 * Manages ghoul sprites
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    public class GhoulManager
    {
        //constants
        private const int GHOULSTARTX = 9;
        private const int GHOULSTARTY = 11;
        private const int GHOULFRAMECOUNT = 4;

        //fields
        private Random random;
        private Maze maze;
        private List<Bitmap> ghoul1Frames;
        private List<Bitmap> ghoul2Frames;
        private List<Bitmap> ghoul3Frames;
        private List<Bitmap> ghoul4Frames;
        private List<Ghoul> ghouls;
        private int ghoulStart;

        public GhoulManager(Maze maze, Random random)
        {
            this.maze = maze;
            this.random = random;

            ghoul1Frames = new List<Bitmap>(); //creating list of animation frame for ghoul1
            ghoul2Frames = new List<Bitmap>(); //creating list of animation frame for ghoul2
            ghoul3Frames = new List<Bitmap>(); //creating list of animation frames for ghoul3
            ghoul4Frames = new List<Bitmap>(); //creating list of animation frames for ghoul4

            for (int i = 0; i < GHOULFRAMECOUNT; i++)//populating animation lists
            {
                Bitmap frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoul" + i.ToString());
                ghoul1Frames.Add(new Bitmap(frame));

                frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoulB" + i.ToString());
                ghoul2Frames.Add(new Bitmap(frame));

                frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoulC" + i.ToString());
                ghoul3Frames.Add(new Bitmap(frame));

                frame = (Bitmap)Properties.Resources.ResourceManager.GetObject("ghoulD" + i.ToString());
                ghoul4Frames.Add(new Bitmap(frame));
            }

            ghouls = new List<Ghoul>();
            ghouls.Add(new Ghoul(ghoul1Frames, maze, random, new Point(GHOULSTARTX, GHOULSTARTY), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul2Frames, maze, random, new Point(GHOULSTARTX + 1, GHOULSTARTY), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul3Frames, maze, random, new Point(GHOULSTARTX + 2, GHOULSTARTY), Direction.Up, 2, 0, 0));
            ghouls.Add(new Ghoul(ghoul4Frames, maze, random, new Point(GHOULSTARTX + 3, GHOULSTARTY), Direction.Up, 2, 0, 0));

            ghoulStart = GHOULSTARTX;
        }

        //resets ghouls
        public void reset()
        {
            NotScared();
            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Position = new Point(ghoulStart, 11);
                ghoulStart++;
            }
            ghoulStart = GHOULSTARTX;
        }

        //activates if PacMan eats a power-up
        public void Scared()
        {
            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Scared = true;
                ghoul.ScaredGhost();
            }
        }

        //deactivates scared status
        public void NotScared()
        {
            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Scared = false;
                ghoul.ScaredGhost();
                ghoul.Jail = false; //enables ghoul movement in jail
            }
        }

        //moves ghouls accordingly
        public void MoveGhoul(bool pacDead, int counter, int pacPositionY, PacMan pacMan)
        {
            foreach (Ghoul ghoul in ghouls)
            {
                if ((pacDead == false) && (ghoul.Jail == false))
                {
                    if ((ghoul.Scared == true) && (counter % 2 == 0))//slows movement of ghouls by preventing movement every second frame
                    {

                    }
                    else
                    {
                        ghoul.Move();
                    }
                    ghoul.ChangeDirection();
                    ghoul.CheckForGaps();
                    ghoul.PacManPosition(pacPositionY);
                }

                ghoul.Draw();
                ghoul.PacManPosition(pacPositionY);
            }

            CheckHit(pacMan);
        }

        //checks to see if ghoul has touched PacMan
        public void CheckHit(PacMan pacMan)
        {
            foreach (Ghoul ghoul in ghouls)
            {
                if ((pacMan.HitOpponent(ghoul.Position)) && (ghoul.Scared == true))
                {
                    ghoul.Dead();
                    pacMan.EatGhost();
                    //score += 10;
                }

                else if (pacMan.HitOpponent(ghoul.Position))
                {
                    pacMan.Dead();
                }
            }
        }

        //Draws ghouls
        public void Draw()
        {
            foreach (Ghoul ghoul in ghouls)
            {
                ghoul.Draw();
            }
        }

    }
}
