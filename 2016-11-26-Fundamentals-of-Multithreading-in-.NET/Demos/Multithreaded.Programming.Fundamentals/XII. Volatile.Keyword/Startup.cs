using System;
using System.Threading;

namespace XII.Volatile.Keyword
{
    public class Startup
    {
        // Run this example in 'Debug' mode 
        // Run this example in 'Release' mode
        // Notice the differences in the execution workflow
        // In 'Debug' mode, the compiler does not optimize the code and the potential problem is not revealed
        // However, In 'Release' mode, the compiler optimizes the code and the problem is revealed
        public static void Main(string[] args)
        {
            var shared = new SharedControlClass();
            shared.IsRunning = true;

            var thread = new Thread(DoControlledWork);
            thread.Start(shared);

            // Sleep for 1 second to allow the new thread to begin execution
            Thread.Sleep(1000);

            // Change the value of 'IsRunning' to false
            // In order to interrupt the 'while' loop in the new thread
            shared.IsRunning = false;
            Console.WriteLine("Value of 'isRunning' set to false");
        }

        public static void DoControlledWork(object arg)
        {
            var sharedObject = arg as SharedControlClass;

            Console.WriteLine("Entered 'while' loop");
            while (sharedObject.IsRunning)
            {
                // Cycle untill the variable is set to 'false'
            }
            Console.WriteLine("Exited 'while' loop");
        }
    }

    // Run the code with 'volatile' modifier on the 'isRunning' field, 
    // and without 'volatile' modifier in both 'Debug' and 'Release' modes
    public class SharedControlClass
    {
        //private volatile bool isRunning;
        private bool isRunning;

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }

            set
            {
                this.isRunning = value;
            }
        }
    }
}
