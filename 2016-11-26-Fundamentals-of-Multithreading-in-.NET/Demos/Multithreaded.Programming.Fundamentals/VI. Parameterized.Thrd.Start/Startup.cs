using System;
using System.Threading;

namespace VI.Parameterized.Thrd.Start
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var banana = GetBananaASCII();

            var thread = new Thread((opts) =>
            {
                var threadStartOptions = opts as ThreadStartOptions;
                if (threadStartOptions.IsNull())
                {
                    throw new ArgumentException("Invalid ThreadStartOptions argument.");
                }

                // Infinite loop on the new thread
                while (true)
                {
                    Thread.Sleep(threadStartOptions.SleepTime);
                    Console.ForegroundColor = threadStartOptions.ForegroundColor;
                    Console.WriteLine(threadStartOptions.Message);
                }
            });

            // Setup and run the new thread
            var threadStartOpions = new ThreadStartOptions
            {
                Message = banana,
                SleepTime = 1000,
                ForegroundColor = ConsoleColor.Yellow
            };
            thread.Start(threadStartOpions);

            // Run another infinite loop on the main thread
            while (true)
            {
                Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Main thread working now");
            }
        }

        public static string GetBananaASCII()
        {
            return @"
                                 _
                                //\
                                V  \
                                 \  \_
                                  \,'.`-.
                                   |\ `. `.       
                                   ( \  `. `-.                        _,.-:\
                                    \ \   `.  `-._             __..--' ,-';/
                                     \ `.   `-.   `-..___..---'   _.--' ,'/
                                      `. `.    `-._        __..--'    ,' /
                                        `. `-_     ``--..''       _.-' ,'
                                          `-_ `-.___        __,--'   ,'
                                             `-.__  `----'''    __.- '
                                                  `--..____..--'";
        }
    }

    public class ThreadStartOptions
    {
        public string Message { get; set; }

        public int SleepTime { get; set; }

        public ConsoleColor ForegroundColor { get; set; }
    }

    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
