using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace III.Merge.Sort
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Loading array from file...");
            var array = LoadArrayFromFile("largeArray.txt");
            Console.WriteLine("Array loaded");

            Console.WriteLine("Started sorting the array");
            var stopwatch = Stopwatch.StartNew();

            var mergeSort = new MergeSort();
            mergeSort.Sort(array);

            stopwatch.Stop();
            Console.WriteLine("Array sorted");

            // Elapsed time: 7600 ms
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }

        public static int[] GetArray(int size)
        {
            var random = new Random();
            var array = new int[size];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 100);
            }

            return array;
        }

        public static string ArrayToString(int[] array)
        {
            var random = new Random();
            var builder = new StringBuilder(array.Length + 3);

            for (int i = 0; i < array.Length; i++)
            {
                if (i < array.Length - 1)
                {
                    builder.Append($"{random.Next(1, 200)},");
                }
                else
                {
                    builder.Append($"{random.Next(1, 200)}");
                }
            }

            return builder.ToString();
        }

        public static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("{0}, ", array[i]);
            }
        }

        public static int[] LoadArrayFromFile(string fileName)
        {
            return File.ReadAllText(fileName)
                .Split(',')
                .Select(x => int.Parse(x.Trim()))
                .ToArray();
        }
    }
}
