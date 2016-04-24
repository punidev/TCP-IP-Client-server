using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tcp
{
    public class Listen
    {
        public Listen()
        {
        }
        public void ListenFromSocket(int port)
        {
            var ipHost = Dns.GetHostEntry("localhost");
            var ipAddr = ipHost.AddressList[0];
            var ipEndPoint = new IPEndPoint(ipAddr, port);
            var sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //sListener.Connect(ipEndPoint);
            sListener.Bind(ipEndPoint);
            sListener.Listen(10);
            
            Console.WriteLine("Соединение с сервером {0}", ipEndPoint);
            while (true)
            {
                var socket = sListener.Accept();
                string data = null;
                var bytes = new byte[1024];

                var bytesRec = socket.Receive(bytes);
                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                Console.Write(data + "\n");
                socket.Send(Encoding.UTF8.GetBytes(data));
                if (data.IndexOf("exit") > -1)
                {
                    Console.WriteLine("Сервер завершил соединение с клиентом.");
                    break;
                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}
