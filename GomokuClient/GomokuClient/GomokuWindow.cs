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
    public partial class Form1 : Form
    {
        NetSocket client;
        StrHandler msgHandler;
        bool isLogin;
        string strPath;

        ShowBoard board;
        // 1 = black, 2 = white
        int whichSide = 0;
        int whoWin = 0;
        int[,] map = new int[15, 15];
        Process myProcess = new Process();
        StreamWriter myStreamWriter;
        StreamReader myStreamReader;

        public Form1()
        {
            InitializeComponent();

            msgHandler = this.addMsg;
            isLogin = false;
            Login.Enabled = false;
            fileName.ReadOnly = true;

            whichSide = 0;
            // 初始化map 空=0 黑=1 白=2
            for(int i=0; i<map.GetLength(0); i += 1)
            {
                for(int j=0; j<map.GetLength(1); j += 1)
                {
                    map[i, j] = 0;
                }
            }
            blackButton.Enabled = false;
            whiteButton.Enabled = false;
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            // 設定目標server IP
            if(IPaddr.Text.Length > 0)
            {
                NetSetting.serverIp = IPaddr.Text;
            }
            client = NetSocket.connect(NetSetting.serverIp);
            if (client == null)
            {
                AllMessage.AppendText("Cannont connect to this Server\r\n");
            }
            else
            {
                AllMessage.AppendText("Connect to Server: " + IPaddr.Text + "\r\n");
                Connect.Enabled = false;
                Login.Enabled = true;
            }
        }
        private void Login_Click(object sender, EventArgs e)
        {
            // 防止空字串
            if (account().Length == 0)
            {
                AllMessage.AppendText("請輸入帳號!");
                return;
            }
            client.newListener(processMsgComeIn);
            if (!isLogin)
            {
                client.send("cmd login " + account() + " " + pass());
            }
        }

        // 按下 Send Button 可以傳送訊息
        private void buttonSend_Click(object sender, EventArgs e)
        {
            sendMsg();
        }
        // 按下 Enter 也能傳送訊息
        private void textBoxMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                sendMsg();
        }

        public String account()
        {
            return Account.Text.Trim();
        }
        public String pass()
        {
            return Password.Text.Trim();
        }

        public String msg()
        {
            return textBoxMsg.Text;
        }

        public void sendMsg()
        {
            if (msg().Length > 0)
            {
                client.send(account() + " : " + msg());
                textBoxMsg.Text = "";
            }
        }
        public void sendMsg(String msgstr)
        {
            if (msgstr.Length > 0)
            {
                client.send(account() + " : " + msgstr);
            }
        }

        public String processMsgComeIn(String msg)
        {
            this.Invoke(msgHandler, new Object[] { msg });
            return "OK";
        }

        public String addMsg(String msg)
        {
            String[] words = msg.Split(' ');
            if( words[0] == "cmd" )
            {
                if(words[1]== "loginsucess")
                {
                    AllMessage.AppendText("Login Successfully!!\r\n");
                    isLogin = true;
                    Login.Enabled = false;
                    Account.Enabled = false;
                    Password.Enabled = false;

                    blackButton.Enabled = true;
                    whiteButton.Enabled = true;
                }
                else if(words[1] == "loginfail")
                {
                    AllMessage.AppendText("Login Failed!!\r\n");
                }
                else if(words[1] == "ready")
                {
                    board = new ShowBoard();
                    board.map = map;

                    // board.Show(this);
                    board.Show();
                    board.FormClosed += new FormClosedEventHandler(ShowBoard_FormClosed);
                    board.UpdateBoard();
                    this.Hide();
                    
                    if( whichSide==1 )
                        myTurn();
                }
            }
            else if(words[0]=="play" && words[1]!=account() && whoWin==0)
            {
                int I = getI(words[2]);
                int J = getJ(words[3]);
                map[I, J] = otherSide();
                board.UpdateBoard(I, J, otherSide());
                if( checkWin(I, J, otherSide()) )
                {
                    // end case
                    if (otherSide() == 1)
                        MessageBox.Show("Black Side Win!!!");
                    else if (otherSide() == 2)
                        MessageBox.Show("White Side Win!!!");
                    whoWin = otherSide();
                }
                else
                {
                    myStreamWriter.WriteLine(words[2] + " " + words[3]);
                    myTurn();
                }
            }
            return "OK";
        }

        private void blackButton_Click(object sender, EventArgs e)
        {
            whichSide = 1;
            blackButton.Enabled = false;
            whiteButton.Enabled = false;
        }
        private void whiteButton_Click(object sender, EventArgs e)
        {
            whichSide = 2;
            blackButton.Enabled = false;
            whiteButton.Enabled = false;
        }

        private void Ready_Click(object sender, EventArgs e)
        {
            if( Connect.Enabled==true || Login.Enabled==true || blackButton.Enabled==true || strPath=="")
            {
                AllMessage.AppendText("請檢查所有設定再開始\r\n");
                return;
            }
            myProcess.StartInfo.FileName = strPath;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardOutput = true;

            myProcess.Start();
            myStreamWriter = myProcess.StandardInput;
            myStreamReader = myProcess.StandardOutput;

            client.send("cmd ready " + account() + " " + whichSide);
            textBoxMsg.Text = "";
        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            if (this.openFileDlg.ShowDialog() == DialogResult.OK)
            {
                strPath = openFileDlg.FileName;
                // fileName.Text = strPath;
                fileName.Text = Path.GetFileName(strPath);
            }
        }

        private int otherSide()
        {
            if (whichSide == 1) return 2;
            if (whichSide == 2) return 1;
            return 0;
       } 
        private int getI(String word)
        {
            try
            {
                return 15 - Int32.Parse(word);
            }
            catch
            {
                return -1;
            }
        }
        private int getJ(String word)
        {
            if (word[0] > 'O' || word[0] < 'A') return -1;
            return (word[0] - 'A');
        }

        private void myTurn()
        {
            String output;
            String[] words;
            int I, J;
            while( true )
            {
                output = myStreamReader.ReadLine();
                words = output.Split(' ');
                I = getI(words[0]);
                J = getJ(words[1]);
                if (I == -1 || J == -1 || map[I,J]!=0)
                {
                    myStreamWriter.WriteLine("-1 I");
                }
                else break;
            }
            client.send("play " + account() + " " + output);
            map[I, J] = whichSide;
            board.UpdateBoard(I, J, whichSide);

            if( checkWin(I, J, whichSide) )
            {
                // end case
                if (whichSide == 1)
                    MessageBox.Show("Black Side Win!!!");
                else if (whichSide == 2)
                    MessageBox.Show("White Side Win!!!");
                whoWin = whichSide;
            }
        }

        void ShowBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShowBoard sub = (ShowBoard)sender;
            this.Show();
            myProcess.Close();
        }

        bool checkWin(int I, int J, int currside)
        {
            int i, j, cnt, size = map.GetLength(0);

            // i-1, i, i+1
            i = I; j = J; cnt = 0;
            i -= 1;
            while( i>=0 )
            {
                if (map[i, j] == currside) cnt += 1;
                else break;
                i -= 1;
            }
            i = I; j = J;
            i += 1;
            while( i<size )
            {
                if (map[i, j] == currside) cnt += 1;
                else break;
                i += 1;
            }
            if (cnt >= 4) return true;

            // j-1, j, j+1
            i = I; j = J; cnt = 0;
            j -= 1;
            while( j>=0 )
            {
                if (map[i, j] == currside) cnt += 1;
                else break;
                j -= 1;
            }
            i = I; j = J;
            j += 1;
            while( j<size )
            {
                if (map[i, j] == currside) cnt += 1;
                else break;
                j += 1;
            }
            if (cnt >= 4) return true;

            // --, 0, ++ 
            i = I; j = J; cnt = 0;
            i -= 1;  j -= 1;
            while( i>=0 && j>=0 )
            {
                if (map[i, j] == currside) cnt += 1;
                else break;
                i -= 1; j -= 1;
            }
            i = I; j = J;
            i += 1; j += 1;
            while( i<size && j<size )
            {
                if (map[i, j] == currside) cnt += 1;
                else break;
                i += 1; j += 1;
            }
            if (cnt >= 4) return true;

            // -+, 0, +-
            i = I; j = J; cnt = 0;
            i -= 1;  j += 1;
            while( i>=0 && j<size )
            {
                if (map[i, j] == currside) cnt += 1;
                else break;
                i -= 1; j += 1;
            }
            i = I; j = J;
            i += 1; j -= 1;
            while( i<size && j>=0 )
            {
                if (map[i, j] == currside) cnt += 1;
                else break;
                i += 1; j -= 1;
            }
            if (cnt >= 4) return true;

            return false;
        }
    }
}
