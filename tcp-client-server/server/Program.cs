using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
                while (true)
                { 
                    server.Start();
                    Console.WriteLine("Ожидание подключений... ");
                    var client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");
                    var stream = client.GetStream();
                    while (true)
                    {
                        var response = Console.ReadLine();
                        if (response != null)
                        {
                            var data = Encoding.UTF8.GetBytes(response);
                            stream.Write(data, 0, data.Length);
                        }
                        Console.WriteLine("Отправлено сообщение: {0}", response);
                        if (response == "exit")
                        {
                            break;
                        }
                    }
                    client.Close();
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
    }
}