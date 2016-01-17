using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;

namespace Gomoku
{
    public partial class ShowBoard : Form
    {
        Graphics g;
        Image imageBlack, imageWhite, imageBoard;

        public int[,] map;
        int boardX = 28, boardY = 28, boardLength = 480;
        int distance = 32, startx = 15, starty = 15;

        public ShowBoard()
        {
            InitializeComponent();

            g = this.CreateGraphics();

            imageBoard = Image.FromFile("../../board.jpg");
            imageBlack = Image.FromFile("../../black.png");
            imageWhite = Image.FromFile("../../white.png");

            //map = new int[15, 15];
            //for (int i = 0; i < 15; i += 1)
            //    for (int j = 0; j < 15; j += 1)
            //        map[i, j] = 0;
        }

        public void UpdateBoard()
        {
            g.DrawImage(imageBoard, boardX, boardY, boardLength, boardLength);
            for (int i = 0; i < map.GetLength(0); i += 1)
            {
                for (int j = 0; j < map.GetLength(1); j += 1)
                {
                    UpdateBoard(i, j, map[i, j]);
                }
            }
        }

        public void UpdateBoard(int i, int j, int whichSide)
        {
            if( whichSide==1)
            {
                g.DrawImage(imageBlack, startx + j * distance, starty + i * distance, 64, 64);
            }
            else if( whichSide==2 )
            {
                g.DrawImage(imageWhite, startx + j * distance, starty + i * distance, 64, 64);
            }
            else
            {
                MessageBox.Show("Unknown Side");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateBoard();
        }

    }
}
