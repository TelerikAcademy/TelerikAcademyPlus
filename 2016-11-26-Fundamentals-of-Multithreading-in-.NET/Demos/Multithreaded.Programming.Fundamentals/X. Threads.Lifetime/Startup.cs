using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace X.Threads.Lifetime
{
    public class Startup
    {
        static void Main(string[] args)
        {
            if(true)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(300);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Still alive");
                    }
                }).Start();
            }

            // Sleep the main thread in order to give some time for the new thread to start execution
            // Before forcing the Garbage Collector
            Thread.Sleep(2000);

            // No reference to the newly created Thread object here
            // The Thread object is scheduled for Garbage Collection
            // We force a garbage collection and wait for any pending finalizers
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("All unreferenced objects are collected!");

            // After the Thread object is collected, we no longer have access to the newly created thread
            // However the actual OS thread is not suspended until it finishes execution
            while (true)
            {
                Thread.Sleep(930);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Main thread");
            }
        }
    }
}
