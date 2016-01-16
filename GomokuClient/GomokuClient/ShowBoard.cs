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
        Image imageBlack, imageWhite;

        public Form1 fatherForm;
        public int whichSide;
        public int[,] map;
        public Process myProcess;
        public StreamWriter myStreamWriter;
        public StreamReader myStreamReader;

        public ShowBoard()
        {
            InitializeComponent();

            //imageBlack = Image.FromFile("../../black.png");
            //imageWhite = Image.FromFile("../../white.png");
        }

        public void UpdateBoard()
        {
            // for(int i=0)
            boardMsg.Text = "";
            for (int i = 0; i < map.GetLength(0); i += 1)
            {
                for (int j = 0; j < map.GetLength(1); j += 1)
                {
                    if (map[i, j] == 1)
                        boardMsg.Text += "B ";
                    else if (map[i, j] == 2)
                        boardMsg.Text += "W ";
                    else
                        boardMsg.Text += "  ";
                }
                boardMsg.Text += "\r\n";
            }
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
