﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static int port = 8888;
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listenSocket.Bind(ipPoint);

            listenSocket.Listen(10);

            Console.WriteLine("Server started...");

            while (true)
            {
                Socket handler = listenSocket.Accept();
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];

                do
                {
                    bytes = handler.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (handler.Available > 0);

                Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + "You got the message:" + builder.ToString());

                string message = "Your message has been delivered";
                data = Encoding.Unicode.GetBytes(message);
                handler.Send(data);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

        }
    }
}
