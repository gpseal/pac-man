using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public class Maze : DataGridView
    {
        private const int NROWSCOLUMNS = 22;                          // Number of cells in each row and column
        private const int CELLSIZE = 37;
        private const int SPACESIZE = 4;
        private const int NKIBBLES = 208;

        private const string STARTMAP = "wwwwwwwwwwwwwwwwwwwwww" +
                                "wckPkkkkkkwwckkkkkPkkw" +
                                "wlwwlwwwwlwwlwwwwlwwlw" +
                                "wlwwlwwwwlwwlwwwwlwwlw" +
                                "wlkkikkkkikkikkkkikkiw" +
                                "wlwwwlwlwwwwwwlwlwwwlw" +
                                "wlwwwlwlkkwwckiwlwwwlw" +
                                "wlkkkiwwwlwwlwwwlkkkiw" +
                                "wwwwwlwckikkikkwlwwwww" +
                                "wwwwwlwlwjjjjwlwlwwwww" +
                                "pkkkkikiwjjjjwlkikkkkp" +
                                "wwwwwlwlwjjjjwlwlwwwww" +
                                "wwwwwlwlwwwwwwlwlwwwww" +
                                "wckkkiwlkkkkkkiwlkkkkw" +
                                "wlwwwlwlwwwwwwlwlwwwlw" +
                                "wlkwlikikkwwckikikwciw" +
                                "wwlwlwwwwlwwlwwwwlwlww" +
                                "wwlwlkkkkikkikkkkiwlww" +
                                "wcikiwwlwwwwwwlwwlkkkw" +
                                "wlwwwwwlkkwwckiwwwwwlw" +
                                "wlkkPkkiwlkkiwlkkPkkiw" +
                                "wwwwwwwwwwwwwwwwwwwwww";

        //fields
        private char[] currentMap;
        private int nKibbles;
        private Bitmap wall;
        private Bitmap kibble;
        private Bitmap blank;
        private Bitmap kibbleLeft;
        private Bitmap kibbleCorner;
        private Bitmap kibbleIntersection;
        private Bitmap pipe;
        private Bitmap power;

        //constructor
        public Maze()
            : base()
        {

            CellBorderStyle = DataGridViewCellBorderStyle.None;
            //initialise fields

            currentMap = STARTMAP.ToCharArray();
            wall = Properties.Resources.wall;
            kibble = Properties.Resources.kibble;
            blank = Properties.Resources.blank;
            kibbleLeft = Properties.Resources.kibbleLeft;
            kibbleCorner = Properties.Resources.kibbleCorner;
            kibbleIntersection = Properties.Resources.kibbleIntersection;
            pipe = Properties.Resources.pipe;
            power = Properties.Resources.power;
            nKibbles = NKIBBLES;


            // set position of maze on the Form
            Top = 0;
            Left = -30;

            // setup the columns to display images. We want to display images, so we set 5 columns worth of Image columns
            for (int x = 0; x < NROWSCOLUMNS; x++)
            {
                Columns.Add(new DataGridViewImageColumn());
            }
            // then we can tell the grid the number of rows we want to display
            RowCount = NROWSCOLUMNS;

            // set the properties of the Maze(which is a DataGridView object)
            Height = NROWSCOLUMNS * CELLSIZE + SPACESIZE;
            Width = NROWSCOLUMNS * CELLSIZE + SPACESIZE;
            ScrollBars = ScrollBars.None;
            ColumnHeadersVisible = false;
            RowHeadersVisible = false;

            // set size of cells:
            foreach (DataGridViewRow r in this.Rows)
                r.Height = CELLSIZE;

            foreach (DataGridViewColumn c in this.Columns)
                c.Width = CELLSIZE;

            // rows and columns should never resize themselves to fit cell contents
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // prevent user from resizing rows or columns
            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
        }

        

        //to draw the maze, the string character is used to load the corresponding image into the DataGridView cell
        public void Draw()
        {
            int wCount = 1;
            int totalCells = NROWSCOLUMNS * NROWSCOLUMNS;
            int count = 0;
            for (int i = 0; i < totalCells; i++)
            {
                
                //count++;
                int nRow = i / NROWSCOLUMNS;
                int nColumn = i % NROWSCOLUMNS;


                switch (currentMap[i]) //i = start position, 1 is how many after (0 based)
                {
                    case 'w':
                        Rows[nRow].Cells[nColumn].Value = wall;
                        wCount++;
                        break;
                    case 'k':
                        Rows[nRow].Cells[nColumn].Value = kibble;
                        break;
                    case 'l':
                        Rows[nRow].Cells[nColumn].Value = kibbleLeft;
                        break;
                    case 'c':
                        Rows[nRow].Cells[nColumn].Value = kibbleCorner;
                        break;
                    case 'i':
                        Rows[nRow].Cells[nColumn].Value = kibbleIntersection;
                        break;
                    case 'p':
                        Rows[nRow].Cells[nColumn].Value = pipe;
                        break;
                    case 'b':
                        Rows[nRow].Cells[nColumn].Value = blank;
                        break;
                    case 'j':
                        Rows[nRow].Cells[nColumn].Value = blank;
                        break;
                    case 'P':
                        Rows[nRow].Cells[nColumn].Value = power;
                        break;
                    default:
                        MessageBox.Show("Unidentified value in string");
                        break;
                }

                

            }
        }

        public void Reset()
        {
            currentMap = STARTMAP.ToCharArray();
        }

        //public string CurrentMap { get => currentMap; set => currentMap = value; }
        public Bitmap Kibble { get => kibble; set => kibble = value; }
        public char[] CurrentMap1 { get => currentMap; set => currentMap = value; }
        public int NKibbles { get => nKibbles; set => nKibbles = value; }
    }
}
