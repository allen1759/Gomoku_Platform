using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Gomoku
{
    public class NetSetting
    {
        public static String serverIp = "127.0.0.1";
        public static int port = 9876;
    }

    public delegate String StrHandler(String str);

    public class NetSocket
    {
        public Socket socket;
        public NetworkStream stream;
        public StreamReader reader;
        public StreamWriter writer;
        public StrHandler inHandler;
        public EndPoint remoteEndPoint;
        public bool isDead = false;
        public String name = "";

        public NetSocket(Socket s)
        {
            socket = s;
            stream = new NetworkStream(s);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            remoteEndPoint = socket.RemoteEndPoint;
        }

        public String receive()
        {
            return reader.ReadLine();
        }

        public NetSocket send(String line)
        {
            writer.WriteLine(line);
            writer.Flush();
            return this;
        }

        public static NetSocket connect(String ip)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), NetSetting.port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipep);
            return new NetSocket(socket);
        }

        public Thread newListener(StrHandler pHandler)
        {
            inHandler = pHandler;

            Thread listenThread = new Thread(new ThreadStart(listen));
            listenThread.Start();
            return listenThread;
        }

        public void listen()
        {
            try
            {
                while (true)
                {
                    String line = receive();
                    inHandler(line);
                }
            }
            catch (Exception ex)
            {
                isDead = true;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
