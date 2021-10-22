using PacMan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pacman
{
    public partial class Form1 : Form
    {
        private const int FORMHEIGHT = 780;
        private const int FORMWIDTH = 758;

        //declare the Maze object so it can be used throughout the form
        private Maze maze;
        private Random random;
        private Controller controller;

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

            controller = new Controller(maze, random);
            controller.StartNewGame();

            // remember the Timer Enabled Property is set to false as a default
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            controller.PlayGame();
        }

        //private void SetUpDataGridView()// settings for maze grid https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.datagridview.gridcolor?view=windowsdesktop-5.0
        //{
        //    //this.Controls.Add(maze);
        //    //maze.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //}
    }
}
