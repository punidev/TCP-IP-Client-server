using System;
using tcp;

namespace client
{
    class Program
    {
        private static void Main()
        {
            var tcp = new Write();
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Введите название пользователяя: ");
                Console.ForegroundColor = ConsoleColor.White;
                string name = Console.ReadLine();
                tcp.SendMessageFromSocket(11000, name);
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
