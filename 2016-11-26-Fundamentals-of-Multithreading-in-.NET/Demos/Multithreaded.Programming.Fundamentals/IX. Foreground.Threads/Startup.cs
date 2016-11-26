using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Foreground.Threads
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var thread = new Thread(() =>
            {
                using (var writer = new StreamWriter(@"..\..\thread.txt"))
                {
                    writer.AutoFlush = true;
                    var counter = 1;

                    while (counter <= 50)
                    {
                        Thread.Sleep(300);

                        var message = $"Count: {counter++}";
                        Console.WriteLine(message);
                        writer.WriteLine(message);
                    }
                };
            });

            // Foreground threads are awaited by the main thread
            // This means that the process cannot be suspended 
            // If a foreground thread is still running
            thread.IsBackground = false;
            thread.Start();

            Console.WriteLine("Main thread finished execution");
            // Main thread finishes execution here
            // But the CLR waits for all foreground threads to finish their work
            // In order to suspend the process
        }
    }
}
