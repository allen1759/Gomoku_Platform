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
        String blackSideIP, whiteSideIP;
        Dictionary<string, int> IPmapping = new Dictionary<string, int>();
        Dictionary<string, string> accountmapping = new Dictionary<string, string>();
        int countingIP = 0;

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
              Console.WriteLine(client.remoteEndPoint.ToString() + Environment.NewLine);
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
            String targetIP = words[0];
            if (words[1] == CommandWords.command) 
            {
                // 不檢查帳號密碼
                if (words[2] == CommandWords.command_login) 
                {
                    // 沒有檢查重複帳號登入
                    String accountName = words[3];
                    accountmapping[targetIP] = accountName;
                    IPmapping[targetIP] = ++countingIP;

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(words[3] + " Login 成功");
                    foreach (NetSocket client in clientList)
                    {
                        if (targetIP==client.remoteEndPoint.ToString())
                        //if (client.name=="")
                        {
                            client.name = words[3];
                            Console.WriteLine("Send personal meessage to " + accountName + "(" + client.remoteEndPoint.ToString() + ") :" + msg);
                            //Console.WriteLine("Send personal meessage to " + client.name + "(" + client.remoteEndPoint.ToString() + ") :" + msg);
                            client.send(CommandWords.command + " " + CommandWords.command_loginSuce);
                            break;
                        }
                    }
                }
                // 沒有失敗的case
                //else if(words[2] == CommandWords.command_login)
                //{
                //    Console.ForegroundColor = ConsoleColor.DarkYellow;
                //    Console.WriteLine(words[3] + " Login 失敗");
                //    foreach (NetSocket client in clientList)
                //    {
                //        if(client.name=="")
                //        {
                //            // Console.WriteLine("Send personal meessage to " + client.name + "(" + client.remoteEndPoint.ToString() + ") :" + msg);
                //            client.send(CommandWords.command + " " + CommandWords.command_loginFail);
                //            break;
                //        }
                //    }
                //}
                else if(words[2] == CommandWords.command_clear)
                {
                    blackSideIP = "";
                    whiteSideIP = "";
                }
                else if(words[2] == CommandWords.command_ready)
                {
                    int whichside = Int32.Parse(words[3]);
                    if (whichside == CommandWords.BLACK)
                    {
                        if (blackSideIP != null && blackSideIP != "")
                        {
                            // sould send invalid operation here!!!
                            return "OK";
                        }
                        if (whiteSideIP == targetIP)
                        {
                            whiteSideIP = "";
                        }
                        blackSideIP = targetIP;
                    }
                    else if (whichside == CommandWords.WHITE)
                    {
                        if (whiteSideIP != null && whiteSideIP != "")
                        {
                            // sould send invalid operation here!!!
                            return "OK";
                        }
                        if (blackSideIP == targetIP)
                        {
                            blackSideIP = "";
                        }
                        whiteSideIP = targetIP;
                    }
                    
                    
                    if( blackSideIP!="" && blackSideIP!= null && whiteSideIP!="" && whiteSideIP!=null )
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("雙方成功設置");
                        broadCast(CommandWords.command + " " + CommandWords.command_ready + " " + accountmapping[blackSideIP] + " " + accountmapping[whiteSideIP] + " .");
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
