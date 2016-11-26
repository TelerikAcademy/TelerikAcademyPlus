using System;
using System.Numerics;
using System.Threading;

namespace XI.Threads.Priority
{
    public class Startup
    {
        private static object lockObject = new object();

        public static void Main(string[] args)
        {
            // Execute this code 20 times for more accurate result set
            for (int i = 0; i < 1; i++)
            {
                var highPriorityThread = new Thread(DoWork);
                highPriorityThread.Priority = ThreadPriority.Highest;
                highPriorityThread.Name = "High priority thread";
                highPriorityThread.Start(ConsoleColor.Red);

                var lowPriorityThread = new Thread(DoWork);
                lowPriorityThread.Priority = ThreadPriority.Lowest;
                lowPriorityThread.Name = "Low priority thread";
                lowPriorityThread.Start(ConsoleColor.Yellow);

                highPriorityThread.Join();


                Console.WriteLine("After the JOIN");

                lowPriorityThread.Join();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(new string('*', 60));
            }
            
            // The High priority thread receives more execution time from the scheduler
            // An average of 50% more than the Low priority thread
            // However his isn't deterministic and depends on many factors
        }

        public static void DoWork(object color)
        {
            var foregroundColor = (ConsoleColor)color;
            var sum = new BigInteger(0);
            var counter = 0;

            while (counter < 2000000)
            {
                sum += counter;
                counter++;
            }

            lock(lockObject)
            {
                Console.ForegroundColor = foregroundColor;
                Console.WriteLine($"{Thread.CurrentThread.Name} done!");
            }
        }
    }
}
