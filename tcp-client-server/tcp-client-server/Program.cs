using System;
using System.Net.Sockets;
using System.Text;

namespace tcp_client_server
{
    public class Program
    {
        private const int Port = 11000;
        private const string Server = "127.0.0.1";

        public static void Main()
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(Server, Port);

                var data = new byte[256];
                var response = new StringBuilder();
                var stream = client.GetStream();

                do
                {
                    var bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    Console.WriteLine(response.ToString());
                    if (response.ToString() == "exit")
                    {
                        break;
                    }
                } while (true);

                stream.Close();
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            Console.WriteLine("Запрос завершен...");
            Console.Read();
        }
    }
}