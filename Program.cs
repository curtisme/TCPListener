using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCPListener
{

    public class Program
    {
        public static void Main(string[] args)
        {
            int port;
            if (args.Length < 1 || !int.TryParse(args[0], out port))
            {
                Console.WriteLine("Incorrect usage");
                return;
            }
            UTF8Encoding utf8 = new UTF8Encoding();
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, port);
            StringBuilder sb = new StringBuilder();
            byte[] recv = new byte[10];
            int numBytes;
            using(Socket listener = new Socket(SocketType.Stream, ProtocolType.Tcp))
            {
                listener.Bind(ip);
                listener.Listen(0);
                using(Socket accept = listener.Accept())
                    while((numBytes = accept.Receive(recv)) > 0)
                        sb.Append(utf8.GetString(recv, 0, numBytes));
            }
            Console.WriteLine(sb);
        }
    }
}
