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
                    if( map[i,j]==1 )
                    {
                        g.DrawImage(imageBlack, starty + i * distance, startx + j * distance, 64, 64);
                    }
                    else if( map[i,j]==2)
                    {
                        g.DrawImage(imageWhite, starty + i * distance, startx + j * distance, 64, 64);
                    }
                }
            }
        }

        public void UpdateBoard(int i, int j, int whichSide)
        {
            if( whichSide==1)
            {
                g.DrawImage(imageBlack, starty + i * distance, startx + j * distance, 64, 64);
            }
            else if( whichSide==2 )
            {
                g.DrawImage(imageWhite, starty + i * distance, startx + j * distance, 64, 64);
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

        private int getI(String output)
        {
            String[] words = output.Split(' ');
            return 15 - Int32.Parse(words[0]);
        }
        private int getJ(String output)
        {
            String[] words = output.Split(' ');
            return (words[1][0] - 'A');
        }
        
    }
}
