using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace tcp
{
    public class Write
    {
        public void SendMessageFromSocket(int port, string name)
        {
            var ipHost = Dns.GetHostEntry("localhost");
            var ipAddr = ipHost.AddressList[0];
            var ipEndPoint = new IPEndPoint(ipAddr, port);
            var sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(ipEndPoint);
            while (true) { 
            Console.Write("Введите сообщение: ");
            var message = Console.ReadLine();
            var msg = Encoding.UTF8.GetBytes(name + "@" + "windows#~ " + message);
            sender.Send(msg);
            sender.Receive(msg);

            var socket = sender.Accept();
            string data = null;
            var bytes = new byte[1024];
            var bytesRec = socket.Receive(bytes);

            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Console.Write("receive" + data + "\n");

            if (message.IndexOf("exit") == -1)
                SendMessageFromSocket(port, name);
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();}
        }
    }
}
