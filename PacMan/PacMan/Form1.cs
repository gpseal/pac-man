/* Program name: 	    PacMan
   Project file name:   PacMan.sln
   Author:		        Greg Seal
   Date:	            11/11/2021
   Language:		    C#
   Platform:		    Microsoft Visual Studio 2019
   Purpose:		        A game of PacMan
   Description:		    Player moves PacMan through the maze collecting kibbles while being pursued by ghouls.  Player wins when all kibbles
                        are collected.  Player loses when  
   Known Bugs:		    
   Additional Features: 3 lives
                        Ghosts search for PacMan
                        PacMan animates at four angles
                        Grid is 22 X 22 to enable a 20 x 20 playing area
                        Player can reset game if necessary
                        PacMan can power up and consume ghosts.
                        Characters can teleport on appropriate squares
                        Ending music has been added
*/

using PacMan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Pacman
{
    public partial class Form1 : Form
    {
        //Constants
        private const int FORMHEIGHT = 680;
        private const int FORMWIDTH = 960;

        private Maze maze;
        private Random random;
        private Controller controller;

        //SOUND EFFECTS
        private SoundPlayer gameOver;
        private SoundPlayer victory;
        private SoundPlayer gameStartMusic;

        private int counter;

        //constructor
        public Form1()
        {
            InitializeComponent();

            // set the Properties of the form:
            Top = 0;
            Left = 0;
            Height = FORMHEIGHT;
            Width = FORMWIDTH;

            KeyPreview = true;
            random = new Random();
            maze = new Maze();
            Controls.Add(maze); // important, need to add the maze object to the list of controls on the form
            controller = new Controller(maze, random, textBox1, textBox2);
            timer1.Enabled = true;
            counter = 0;

            gameOver = new SoundPlayer(PacMan.Properties.Resources.game_over);
            gameStartMusic = new SoundPlayer(PacMan.Properties.Resources.pacman_beginning);
            victory = new SoundPlayer(PacMan.Properties.Resources.victory);
        }

        //timer tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (controller.PlayGame())
            {
                case ErrorMessage.playWins:
                    {
                        timer1.Enabled = false;
                        controller.Music();
                        victory.Play();
                        pictureBox2.Image = PacMan.Properties.Resources.winScreen;
                        pictureBox2.Visible = true;
                        break;
                    }
                    
                case ErrorMessage.playerLoses:
                    {
                        pictureBox2.Image = PacMan.Properties.Resources.loseScreen;
                        pictureBox2.Visible = true;
                        controller.Music();
                        gameOver.Play();
                        timer1.Enabled = false;
                        break;
                    }
                    
                default:
                    {
                        break;
                    }
            }

            if (counter == 1) //Play intro music at start of game
            {
                gameStartMusic.PlaySync();//Playsync pauses games until music is complete  https://docs.microsoft.com/en-us/dotnet/api/system.media.soundplayer.playsync?view=windowsdesktop-5.0
            }
            counter++;
        }

        //setting direction of pacman
        private void Form1_KeyDown_1(object sender, KeyEventArgs e) 
        {
            if (controller.pacManDead() == false) //if pacman is alive he can change direction
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        controller.SetPacManDirection(Direction.Left);
                        break;

                    case Keys.Right:
                        controller.SetPacManDirection(Direction.Right);
                        break;

                    case Keys.Up:
                        controller.SetPacManDirection(Direction.Up);
                        break;

                    case Keys.Down:
                        controller.SetPacManDirection(Direction.Down);
                        break;

                    default:
                        break;
                }
            }
        }

        //Menu item to reset game
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)  
        {
            controller.Reset();
            timer1.Enabled = true;
            pictureBox2.Visible = false;
            counter = 0;
        }

        //Menu item to quite game
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)  
        {
            System.Windows.Forms.Application.ExitThread();
        }
    }
}
