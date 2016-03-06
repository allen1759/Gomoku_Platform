using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Gomoku
{
    public class GomokuServer
    {
        List<NetSocket> clientList = new List<NetSocket>();
        String playerName1, playerName2;
        int whichSide1 = CommandWords.NOONE;
        int whichSide2 = CommandWords.NOONE;

        public static void Main(String[] args)
        {
            GomokuServer gomokuServer = new GomokuServer();
            gomokuServer.run();
        }

        public void run()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, NetSetting.port);

            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            newsock.Listen(10);

            //取得本機名稱
            string hostName = Dns.GetHostName();
            Console.WriteLine("host name = " + hostName);
            //取得本機IP
            //System.Net.IPHostEntry IPHost = System.Net.Dns.GetHostEntry(Environment.MachineName);
            //if (IPHost.AddressList.Length > 0)
            //{
            //    Console.WriteLine("host IP = " + IPHost.AddressList[0].ToString());
            //}

            while (true)
            {
                Socket socket = newsock.Accept();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("接受一個新連線!");
                NetSocket client = new NetSocket(socket);
                try
                {
                    clientList.Add(client);
                    client.newListener(processMsgComeIn);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                //                clientList.Remove(client);
            }
            //	  newsock.Close();
        }

        public String processMsgComeIn(String msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("收到訊息：" + msg);
            Console.ForegroundColor = ConsoleColor.Gray;

            String[] words = msg.Split(' ');
            if (words[0] == CommandWords.command) 
            {
                String currIP = words[1];
                // 不檢查帳號密碼
                if (words[2] == CommandWords.command_login) 
                {
                    // 沒有檢查重複帳號登入
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(words[3] + " Login 成功");
                    foreach (NetSocket client in clientList)
                    {
                        if (client.name=="")
                        {
                            client.name = words[3];
                            Console.WriteLine("Send personal meessage to " + client.name + "(" + client.remoteEndPoint.ToString() + ") :" + msg);
                            client.send(CommandWords.command + " " + CommandWords.command_loginSuce);
                            break;
                        }
                    }
                    //broadCast("cmd " + words[1]);
                }
                // 沒有失敗的case
                else if(words[2] == CommandWords.command_login)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(words[3] + " Login 失敗");
                    foreach (NetSocket client in clientList)
                    {
                        if(client.name=="")
                        {
                            // Console.WriteLine("Send personal meessage to " + client.name + "(" + client.remoteEndPoint.ToString() + ") :" + msg);
                            client.send(CommandWords.command + " " + CommandWords.command_loginFail);
                            break;
                        }
                    }
                }
                else if(words[2] == CommandWords.command_clear)
                {
                }
                else if(words[2] == CommandWords.command_ready)
                {
                    if (playerName1 == words[3] || playerName2 == words[3])
                        return "OK";
                    if( whichSide1==0 )
                    {
                        playerName1 = words[3];
                        whichSide1 = Int32.Parse(words[4]);
                    }
                    else if( whichSide2==0 )
                    {
                        playerName2 = words[3];
                        whichSide2 = Int32.Parse(words[4]);
                    }

                    if( whichSide1!=0 && whichSide2!=0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("雙方成功設置");
                        broadCast(CommandWords.command + " " + CommandWords.command_ready);
                    }
                }
                else
                {
                    Console.WriteLine(words[1]);
                    Console.WriteLine("==========ERROR!!! UNKNOWN CASE!!!==========");
                }
            }
            else if(words[0]==CommandWords.playing)
            {
                broadCast(msg);
            }
            // unkown case
            else
            {
                broadCast(msg);
            }
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;
            return "OK";
        }

        public void broadCast(String msg)
        {
            Console.WriteLine("廣播訊息給 " + msg + " 線上使用者共" + clientList.Count + "個人!");
            foreach (NetSocket client in clientList)
            {
                if (!client.isDead)
                {
                    Console.WriteLine("Send to " + client.remoteEndPoint.ToString() + ":" + msg);
                    client.send(msg);
                }
            }
            deleteDead(clientList);
        }

        private void deleteDead(List<NetSocket> li)
        {
            for(int i=0; i<li.Count; i+=1)
            {
                if( li[i].isDead )
                {
                    li.RemoveAt(i);
                    deleteDead(li);
                    return;
                }
            }
        }
    }
}
