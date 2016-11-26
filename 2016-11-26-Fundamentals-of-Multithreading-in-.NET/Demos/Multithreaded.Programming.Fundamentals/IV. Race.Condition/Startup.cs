using System;
using System.Diagnostics;
using System.Threading;

namespace IV.Race.Condition
{
    public class Startup
    {
        private static volatile int counter = 0;
        private static object counterLock = new object();

        public static void Main(string[] args)
        {
            // Run the operation 10 times
            // To get more clear results exposing the problem
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(new string('*',60));
                StartCounterConcurrently();
                counter = 0;
            }
        }

        public static void StartCounterConcurrently()
        {
            var t1 = new Thread(()=> Run(ConsoleColor.Green));
            var t2 = new Thread(()=> Run(ConsoleColor.Yellow));

            var stopwatch = Stopwatch.StartNew();
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
            stopwatch.Stop();

            Console.ResetColor();
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }

        public static void Run(ConsoleColor foregroundColor)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            while (counter < 10)
            {
                Thread.Sleep(100);

                // This section has some operations that include non-atomic variable assignment
                // This means that while we are reading the variable value, increment it, and save it back to the register where the previous value was, someone else might have already change it.
                counter++;
                Console.ForegroundColor = foregroundColor;

                // Another anomaly is expected in the last statement
                // While we are beginning to execute the WriteLine() method
                // The other thread might have already changed the Console.ForegroundColor property
                // So we think, that our WriteLine method will display the message with for example a YELLOW color, but the other thread just changed it to RED, and we will experience an inconsistent behavior
                Console.WriteLine($"[Thread:{threadId}] - {counter}");
            }
        }
    }
}
