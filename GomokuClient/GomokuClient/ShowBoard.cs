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
        Font ArialFont = new System.Drawing.Font("Arial", 16);
        StringFormat drawFormat = new StringFormat();
        SolidBrush drawBrushBlack = new SolidBrush(Color.Black);
        SolidBrush drawBrushWhite = new SolidBrush(Color.White);

        public int[,] map;
        public int[,] step;
        public string blackName, whiteName;
        int boardX = 58, boardY = 88, boardLength = 480;
        int distance = 32, startx, starty;
        int numDistx = 11, numDisty = 16;

        public delegate void myTurnHandler(String str);

        public myTurnHandler mth;

        public ShowBoard()
        {
            InitializeComponent();

            startx = boardX - 13;
            starty = boardY - 13;
            g = this.CreateGraphics();

            imageBoard = Image.FromFile("board.jpg");
            imageBlack = Image.FromFile("black.png");
            imageWhite = Image.FromFile("white.png");

            mth = new myTurnHandler(AddMessage);

            // for test
            //step = new int[15, 15];
            //for (int i = 0; i < 15; i += 1)
            //    for (int j = 0; j < 15; j += 1)
            //        step[i, j] = 0;
            //map = new int[15, 15];
            //for (int i = 0; i < 15; i += 1)
            //    for (int j = 0; j < 15; j += 1)
            //        map[i, j] = 0;
            //int[] xs = new int[] { 8, 7, 6, 5, 6, 8, 5, 9, 4, 7 };
            //int[] ys = new int[] { 0, 1, 1, 1, 2, 3, 3, 4, 4, 5 };
            //int cnt = 0;
            //for (int i = 0; i < 10; i += 1)
            //{
            //    map[xs[i], ys[i]] = CommandWords.BLACK;
            //    step[xs[i], ys[i]] = ++cnt;
            //}

            //xs = new int[] { 4, 7, 9, 8, 7, 6, 5, 10, 3 };
            //ys = new int[] { 1, 2, 3, 4, 4, 4, 4, 5, 5 };
            //for (int i = 0; i < 9; i += 1)
            //{
            //    map[xs[i], ys[i]] = CommandWords.WHITE;
            //    step[xs[i], ys[i]] = ++cnt;
            //}

            //UpdateBoard();
            //Black: 8 B
            //White: 8 E
            //Black: 7 D
            //White: 7 E
            //Black: 6 E
            //White: 6 D
            //Black: 8 F
            //White: 5 F
            //Black: 9 B
            //White: 8 C
            //Black: 10 D
            //White: 9 E
            //Black: 10 B
            //White: 10 E
            //Black: 11 E
            //White: 11 B
            //Black: 9 C
            //White: 12 F
        }

        public void UpdateAIName(String n)
        {
            AILabel.Text += n;
        }
        public void UpdateName(String b, String w)
        {
            blackName = b;
            whiteName = w;
            blackNameText.Text += b;
            whiteNameText.Text += w;
        }

        public void UpdateBoard()
        {
            g.DrawImage(imageBoard, boardX, boardY, boardLength, boardLength);
            for (int i = 0; i < 15; i += 1)
            {
                string drawString = string.Format("{0,2}", 15-i);
                g.DrawString(drawString, ArialFont, drawBrushBlack, startx - 25, starty + numDisty + i * distance, drawFormat);
                //g.DrawString(drawString, ArialFont, drawBrushBlack, startx + boardLength + 22, starty + numDisty + i * distance, drawFormat);

                drawString = Convert.ToChar('A' + i).ToString();
                g.DrawString(drawString, ArialFont, drawBrushBlack, startx + numDistx + i * distance + 5, starty + boardLength + 25, drawFormat);
            }
            for (int i = 0; i < map.GetLength(0); i += 1)
            {
                for (int j = 0; j < map.GetLength(1); j += 1)
                {
                    if (map[i, j] != 0)
                    {
                        UpdateBoard(i, j, map[i, j]);
                    }
                }
            }
        }

        public void UpdateBoard(int i, int j, int whichSide)
        {
            if( whichSide==CommandWords.BLACK )
            {
                g.DrawImage(imageBlack, startx + j * distance, starty + i * distance, 64, 64);
                string drawString = step[i, j].ToString("00");
                g.DrawString(drawString, ArialFont, drawBrushWhite, startx + numDistx + j * distance, starty + numDisty + i * distance, drawFormat);
            }
            else if( whichSide==CommandWords.WHITE )
            {
                g.DrawImage(imageWhite, startx + j * distance, starty + i * distance, 64, 64);
                string drawString = step[i, j].ToString("00");
                g.DrawString(drawString, ArialFont, drawBrushBlack, startx + numDistx + j * distance, starty + numDisty + i * distance, drawFormat);
            }
            else
            {
                MessageBox.Show("Unknown Side");
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            String file = blackName + " vs " + whiteName + DateTime.Now.ToString("yyyyMMddHHmmss");
            String title = "==================================================" + Environment.NewLine +
                           blackNameText.Text + Environment.NewLine +
                           whiteNameText.Text + Environment.NewLine +
                           "==================================================" + Environment.NewLine;
            File.WriteAllText(@file + ".txt", title + battle.Text);
            MessageBox.Show("Save to [" + file + ".txt]");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateBoard();
        }

        //public void AddMessageInvoke(TextBox tb, string text)
        //{
        //    if(tb.InvokeRequired)   
        //    {
        //        myTurnHandler myHand = new myTurnHandler(AddMessageInvoke);
        //        tb.Invoke(myHand, tb, text);
        //    }
        //    else
        //    {
        //        tb.Text += text + Environment.NewLine;
        //    }
        //}
        public void AddMessage(String str)
        {
            battle.AppendText(str + Environment.NewLine);
        }
    }
}
