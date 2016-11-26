using System;
using System.Threading;

namespace V.Creating.Threads
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var thread = new Thread(() =>
            {
                // Infinite loop in the new thread
                while (true)
                {
                    Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Second thread working");
                }
            });

            thread.Start();

            // Infinite loop in the main thread
            while (true)
            {
                Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Main thread working");
            }
        }
    }
}
