using System;
using System.IO;
using System.Threading;

namespace VIII.Background.Threads
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

                    while (counter <= 100)
                    {
                        Thread.Sleep(300);

                        var message = $"Count: {counter++}";
                        Console.WriteLine(message);
                        writer.WriteLine(message);
                    }
                };
            });

            // Background threads are not awaited by the main thread
            // This means that when the main thread finishes execution
            // All background threads will be suspended automatically
            thread.IsBackground = true;
            thread.Start();

            // Simulate work for 5 seconds
            Thread.Sleep(5000);

            // Main thread finishes execution here
            // As result all background threads are being suspended
        }
    }
}
