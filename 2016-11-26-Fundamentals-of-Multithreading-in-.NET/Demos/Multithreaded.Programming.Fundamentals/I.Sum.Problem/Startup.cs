using System;
using System.Diagnostics;

namespace I.Sum.Problem
{
    public class Startup
    {
        static void Main(string[] args)
        {
            // Build array
            var arraySize = 50000000; // 50 000 000
            var array = GetArray(arraySize);

            // Run ONE THREAD to deal with ONE LARGE PROBLEM
            var stopwatch = Stopwatch.StartNew();

            var startIndex = 0;
            var elementsToProcessCount = arraySize;

            var arrayProcessor = new ArrayProcessor(array, startIndex, elementsToProcessCount);

            arrayProcessor.CalculateSum();
            var totalSum = arrayProcessor.CalculatedSum;

            stopwatch.Stop();

            // Еlapsed time: 6700-7100 ms
            // Sum: 1249999975000000
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Sum: {totalSum}");
        }

        public static int[] GetArray(int size)
        {
            var array = new int[size];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            return array;
        }
    }
}
