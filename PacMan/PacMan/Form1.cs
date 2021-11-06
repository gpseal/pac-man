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
        private const int FORMHEIGHT = 835;
        private const int FORMWIDTH = 1175;

        //declare the Maze object so it can be used throughout the form
        private Maze maze;
        private Random random;
        private Controller controller;
        private int deadCount;
        private Bitmap gameEnd;

        private Bitmap wall;
        private SoundPlayer gameOver;
        private SoundPlayer victory;
        private SoundPlayer gameStartMusic;

        private int counter;

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

            // create an instance of a Maze:
            maze = new Maze();
            //SetUpDataGridView();// settings for maze grid

            // important, need to add the maze object to the list of controls on the form
            Controls.Add(maze);

            controller = new Controller(maze, random, textBox1, textBox2);
            //controller.StartNewGame();
            deadCount = 0;

            // remember the Timer Enabled Property is set to false as a default
            timer1.Enabled = true;
            gameOver = new SoundPlayer(PacMan.Properties.Resources.EndGame);
            gameStartMusic = new SoundPlayer(PacMan.Properties.Resources.pacman_beginning);
            victory = new SoundPlayer(PacMan.Properties.Resources.victory);

            //gameOver.Stream = PacMan.Properties.Resources.EndGame;
            counter = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            controller.PlayGame();

            if (counter == 1)
            {
                //controller.BackgroundMusic();
                gameStartMusic.PlaySync();
            }

            if (controller.playerLose() == true)
            {
                pictureBox2.Image = PacMan.Properties.Resources.loseScreen;
                pictureBox2.Visible = true;
                gameOver.Play();
                timer1.Enabled = false;
            }

            if (controller.playerWin() == true)
            {
                timer1.Enabled = false;
                victory.Play();
                pictureBox2.Image = PacMan.Properties.Resources.winScreen;
                pictureBox2.Visible = true;
            }
            counter++;
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (controller.pacManDead() == false)
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

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            controller.Reset();
            timer1.Enabled = true;
            pictureBox2.Visible = false;
            counter = 0;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
    }
}
