﻿using System;
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
using System.Threading;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        NetSocket client;
        StrHandler msgHandler;
        String localep;
        bool isLogin;
        string acc_ = "", strPath, temprecord = "";

        ShowBoard board;
        int whichSide = CommandWords.NOONE;
        int whoWin = CommandWords.NOONE;
        int[,] map = new int[15, 15];
        int[,] step = new int[15, 15];
        int stepcnt;
        Process myProcess = new Process();
        StreamWriter myStreamWriter;
        StreamReader myStreamReader;

        public delegate void allMessageHandler(String str);
        public allMessageHandler allmh;

        public Form1()
        {
            //Form.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();

            msgHandler = this.addMsg;
            isLogin = false;
            Login.Enabled = false;
            fileName.ReadOnly = true;

            //allmh = new allMessageHandler(AddAllMessage);

            // 初始化map 空=0 黑=1 白=2 觀察者=3
            stepcnt = 0;
            for(int i=0; i<map.GetLength(0); i += 1)
            {
                for(int j=0; j<map.GetLength(1); j += 1)
                {
                    map[i, j] = CommandWords.NOONE;
                    step[i, j] = 0;
                }
            }
        }
        ~Form1()
        {
            this.Close();
            Application.Exit();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)//按下ESC
            {
                Application.Exit();//關閉程式
            }
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            Connect.Enabled = false;
            // 設定目標server IP
            if(IPaddr.Text.Length > 0)
            {
                NetSetting.serverIp = IPaddr.Text;
            }

            client = NetSocket.connect(NetSetting.serverIp);
            if (client == null)
            {
                AllMessage.AppendText("Cannont connect to this Server" + Environment.NewLine);
                Connect.Enabled = true;
            }
            else
            {
                AllMessage.AppendText("Connect to Server: " + IPaddr.Text + Environment.NewLine);
                Login.Enabled = true;
                localep = client.socket.LocalEndPoint.ToString();
            }
        }
        //public void AddAllMessage(String str)
        //{
        //    AllMessage.AppendText(str);
        //}
        

        private void Login_Click(object sender, EventArgs e)
        {
            // 防止空字串
            if (account().Length == 0)
            {
                AllMessage.AppendText("請輸入名字!");
                return;
            }
            client.newListener(processMsgComeIn);
            if (!isLogin)
            {
                // command sending
                client.send(localep + " " +
                            CommandWords.command + " " +
                            CommandWords.command_login + " " +
                            account());
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


        private void identityBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selected = identityBox.SelectedItem.ToString();
            if (selected[0] == 'B')
            {
                whichSide = CommandWords.BLACK;
            }
            else if (selected[0] == 'W')
            {
                whichSide = CommandWords.WHITE;
            }
            else
            {
                // 觀察者
                whichSide = CommandWords.NOONE;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            client.send(localep + " " + 
                        CommandWords.command + " " +
                        CommandWords.command_clear + " " +
                        account());
        }

        private void Ready_Click(object sender, EventArgs e)
        {
            if (Connect.Enabled == true || Login.Enabled == true || strPath == "")
            {
                AllMessage.AppendText("請檢查所有設定再開始" + Environment.NewLine);
                return;
            }
            myProcess.StartInfo.FileName = strPath;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardOutput = true;

            myProcess.Start();
            myStreamWriter = myProcess.StandardInput;
            myStreamReader = myProcess.StandardOutput;

            client.send(localep + " " + 
                        CommandWords.command + " " +
                        CommandWords.command_ready + " " +
                        whichSide);
            textBoxMsg.Text = "";

            if (whichSide == CommandWords.BLACK)
                temprecord = "B" + Environment.NewLine;
            else if (whichSide == CommandWords.WHITE)
                temprecord = "W" + Environment.NewLine;
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



        public void sendMsg()
        {
            if (msg().Length > 0)
            {
                client.send(account() + " : " + msg());
                textBoxMsg.Text = "";
            }
        }
        //public void sendMsg(String msgstr)
        //{
        //    if (msgstr.Length > 0)
        //    {
        //        client.send(account() + " : " + msgstr);
        //    }
        //}

        public String processMsgComeIn(String msg)
        {
            this.Invoke(msgHandler, new Object[] { msg });
            return "OK";
        }

        // analyze come in messages
        public String addMsg(String msg)
        {
            String[] words = msg.Split(' ');
            // message start with word: command
            if( words[0] == CommandWords.command )
            {
                if( words[1]== CommandWords.command_loginSuce )
                {
                    AllMessage.AppendText("Login Successfully!!" + Environment.NewLine);
                    isLogin = true;
                    Login.Enabled = false;
                    Account.Enabled = false;
                }
                else if(words[1] == CommandWords.command_loginFail )
                {
                    AllMessage.AppendText("Login Failed!!" + Environment.NewLine);
                }
                else if(words[1] == CommandWords.command_ready )
                {
                    board = new ShowBoard();
                    board.UpdateName(words[2], words[3]);
                    board.UpdateAIName(account());
                    board.map = map;
                    board.step = step;


                    // board.Show(this);
                    board.Show();
                    board.FormClosed += new FormClosedEventHandler(ShowBoard_FormClosed);
                    board.UpdateBoard();
                    this.Hide();

                    if (whichSide == CommandWords.BLACK)
                        myStreamWriter.WriteLine(CommandWords.play_startInfoBlack);
                    else if (whichSide == CommandWords.WHITE)
                        myStreamWriter.WriteLine(CommandWords.play_startInfoWhite);

                    if( whichSide == CommandWords.BLACK )
                    {
                        Thread threadToGetNextStep = new Thread(new ThreadStart(myTurn));
                        threadToGetNextStep.Name = "my turn thread";
                        threadToGetNextStep.Start();
                    }
                }
            }
            // message start with word: play 
            else if(words[1]==CommandWords.playing && words[0]!=localep && whoWin==0)
            {
                int I = getI(words[2]);
                int J = getJ(words[3]);
                map[I, J] = otherSide();
                step[I, J] = ++stepcnt;
                board.UpdateBoard(I, J, otherSide());
                if (otherSide() == 1)
                {
                    board.Invoke(board.mth, stepcnt.ToString() + "-B: " + words[2] + " " + words[3]);
                    //board.AddMessage("Black: " + words[2] + " " + words[3]);
                    temprecord += words[2] + " " + words[3] + Environment.NewLine;
                }
                else if (otherSide() == 2)
                {
                    board.Invoke(board.mth, stepcnt.ToString() + "-W: " + words[2] + " " + words[3]);
                    //board.AddMessage("White: " + words[2] + " " + words[3]);
                    temprecord += words[2] + " " + words[3] + Environment.NewLine;
                }
                File.WriteAllText(@"temp.txt", temprecord);

                if ( checkWin(I, J, otherSide()) )
                {
                    // end case
                    if (otherSide() == CommandWords.BLACK)
                    {
                        MessageBox.Show(board.blackName + "(Black Side) Win");
                        board.AddMessage(Environment.NewLine + "==========" + Environment.NewLine + board.blackName + "(Black Side) Win");
                    }
                    else if (otherSide() == CommandWords.WHITE)
                    {
                        MessageBox.Show(board.whiteName + "(White Side) Win");
                        board.AddMessage(Environment.NewLine + "==========" + Environment.NewLine + board.whiteName + "(White Side) Win");
                    }
                    whoWin = otherSide();
                }
                else
                {
                    myStreamWriter.WriteLine(words[2] + " " + words[3]);
                    Thread threadToGetNextStep = new Thread(new ThreadStart(myTurn));
                    threadToGetNextStep.Name = "my turn thread";
                    threadToGetNextStep.Start();
                }
            }
            return "OK";
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
            client.send(localep + " " + CommandWords.playing + " " + output);
            map[I, J] = whichSide;
            step[I, J] = ++stepcnt;
            board.UpdateBoard(I, J, whichSide);

            if (whichSide == CommandWords.BLACK)
            {
                board.Invoke(board.mth, stepcnt.ToString() + "-B: " + output);
                //board.AddMessage("Black: " + output);
                temprecord += output + Environment.NewLine;
            }
            else if (whichSide == CommandWords.WHITE)
            {
                board.Invoke(board.mth, stepcnt.ToString() + "-W: " + output);
                //board.AddMessage("White: " + output);
                temprecord += output + Environment.NewLine;
            }
            File.WriteAllText(@"temp.txt", temprecord);


            if ( checkWin(I, J, whichSide) )
            {
                // end case
                if (whichSide == CommandWords.BLACK)
                    MessageBox.Show("Black Side Win!!!");
                else if (whichSide == CommandWords.WHITE)
                    MessageBox.Show("White Side Win!!!");
                whoWin = whichSide;
            }
        }


        void ShowBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShowBoard sub = (ShowBoard)sender;
            this.Show();
            myProcess.CloseMainWindow();
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

        // some helper function
        private String account()
        {
            if(acc_!="" && acc_!=null) 
                return acc_;
            for (int i = 0; i < Account.Text.Trim().Length; i += 1)
            {
                if (Account.Text[i] == ' ')
                    acc_ += '_';
                else
                    acc_ += Account.Text.Trim()[i];
            }
            return acc_;
        }

        private String msg()
        {
            return textBoxMsg.Text;
        }
        private int otherSide()
        {
            if (whichSide == CommandWords.BLACK) return CommandWords.WHITE;
            if (whichSide == CommandWords.WHITE) return CommandWords.BLACK;
            MessageBox.Show("Cannot verify the side.");
            return 0;
        }
        // translate words to int position
        //asld fjsadlk jfasldk jfkasd jlkfasldf jaksld jfalksd fjlaskdjfaksdfjasldkfjaskldfjlaskdjfklasdjflasdjfklasdjkflasjdfljasdklfj
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

        //asld fjsadlk jfasldk jfkasd jlkfasldf jaksld jfalksd fjlaskdjfaksdfjasldkfjaskldfjlaskdjfklasdjflasdjfklasdjkflasjdfljasdklfj
    }
}
