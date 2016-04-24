using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server
{
    public class Program
    {
        private const int Port = 11000;

        private static void Main()
        {
            TcpListener server = null;
            try
            {
                var localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, Port);
                server.Start();
                while (true)
                { 
                    Console.WriteLine("Ожидание подключений... ");
                    var client = server.AcceptTcpClient();
                    new Thread(ClientServeThread).Start(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }

        private static void ClientServeThread(object obj)
        {
            var client = (TcpClient) obj;

            Console.WriteLine("Подключен клиент. Выполнение запроса...");

            var stream = client.GetStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var writer = new StreamWriter(stream,Encoding.UTF8);
            while (true)
            {
                var response = reader.ReadLine();
                if (response != null)
                {
                    writer.Write("Echo repply: ");
                    writer.WriteLine(response);
                    writer.Flush();
                }
                Console.WriteLine("Отправлено сообщение: {0}", response);
                if (response == "exit")
                {
                    writer.WriteLine("exit");
                    writer.Flush();
                    break;
                }
            }
            client.Close();
        }
    }
}