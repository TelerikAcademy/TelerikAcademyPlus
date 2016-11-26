using System;
using System.Diagnostics;
using System.Threading;

namespace IV.Race.Condition.Solved
{
    public class Startup
    {
        private static volatile int counter = 0;
        private static object counterLock = new object();

        public static void Main(string[] args)
        {
            // Run the operation 10 times
            // To get more results as a proof that the problem no longer exists
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(new string('*', 60));
                StartCounterConcurrently();
                counter = 0;
            }
        }

        public static void StartCounterConcurrently()
        {
            var t1 = new Thread(() => Run(ConsoleColor.Yellow));
            var t2 = new Thread(() => Run(ConsoleColor.Green));

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

                lock (counterLock)
                {
                    // We check the value of the counter again
                    // because there is a chance that while we were locking the section -
                    // the other thread already changed the counter value
                    if (counter < 10)
                    {
                        counter++;
                        Console.ForegroundColor = foregroundColor;
                        Console.WriteLine($"[Thread:{threadId}] - {counter}");
                    }

                    // Remove the 'if-clause' in order to see the behavior described above
                }
            }
        }
    }
}
