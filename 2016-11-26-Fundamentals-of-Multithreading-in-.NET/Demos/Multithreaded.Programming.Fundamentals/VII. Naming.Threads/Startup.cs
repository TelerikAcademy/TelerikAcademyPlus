using System;
using System.Threading;

namespace VI.Naming.Threads
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var cukiThread = new Thread(SayWhatsup);
            var koceThread = new Thread(SayWhatsup);

            //Setup Cuki thread
            var cukiThreadStartOptions = new ThreadStartOptions { Color = ConsoleColor.Magenta, SleepTime = 1000 };
            cukiThread.Name = "Cuki";
            cukiThread.Start(cukiThreadStartOptions);

            //Setup Koce thread
            var koceThreadStartOptions = new ThreadStartOptions { Color = ConsoleColor.Yellow, SleepTime = 200 };
            koceThread.Name = "Koce";
            koceThread.Start(koceThreadStartOptions);
        }

        public static void SayWhatsup(object threadStartOptions)
        {
            var opts = threadStartOptions as ThreadStartOptions;
            var currentThreadName = Thread.CurrentThread.Name;

            while (true)
            {
                Thread.Sleep(opts.SleepTime);
                Console.ForegroundColor = opts.Color;
                Console.WriteLine($"Thread [{currentThreadName}] said: Hello");
            }
        }
    }

    public class ThreadStartOptions
    {
        public ConsoleColor Color { get; set; }

        public int SleepTime { get; set; }
    }
}
