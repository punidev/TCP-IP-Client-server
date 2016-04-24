using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using tcp;

namespace server
{
    class Program
    {
        private static void Main()
        {
            var tcp = new Listen();
            try
            {
                tcp.ListenFromSocket(11000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
   
    }
}
