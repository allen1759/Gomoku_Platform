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

        //public Form1 fatherForm;
        //public int whichSide;
        public int[,] map;
        //public Process myProcess;
        //public StreamWriter myStreamWriter;
        //public StreamReader myStreamReader;

        public ShowBoard()
        {
            InitializeComponent();

            g = this.CreateGraphics();

            imageBoard = Image.FromFile("../../board.jpg");
            imageBlack = Image.FromFile("../../black.png");
            imageWhite = Image.FromFile("../../white.png");

            map = new int[15, 15];
            for (int i = 0; i < 15; i += 1)
                for (int j = 0; j < 15; j += 1)
                    map[i, j] = 0;
            UpdateBoard();
        }

        public void UpdateBoard()
        {
            g.DrawImage(imageBoard, 28, 28, 480, 480);
            int distance = 32, startx = 15, starty = 15;
            for (int i = 0; i < map.GetLength(0); i += 1)
            {
                for (int j = 0; j < map.GetLength(1); j += 1)
                {
                    g.DrawImage(imageBlack, starty + i * distance, startx + j * distance, 64, 64);
                    g.DrawImage(imageWhite, starty + i * distance, startx + j * distance, 64, 64);
                }
            }
            //boardMsg.Text = "";
            //for (int i = 0; i < map.GetLength(0); i += 1)
            //{
            //    for (int j = 0; j < map.GetLength(1); j += 1)
            //    {
            //        if (map[i, j] == 1)
            //            boardMsg.Text += "B ";
            //        else if (map[i, j] == 2)
            //            boardMsg.Text += "W ";
            //        else
            //            boardMsg.Text += "  ";
            //    }
            //    boardMsg.Text += "\r\n";
            //}
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
