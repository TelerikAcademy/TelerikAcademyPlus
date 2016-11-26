using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace III.Merge.Sort.Parallel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Loading array from file...");
            var array = LoadArrayFromFile("largeArray.txt");
            Console.WriteLine("Array loaded");

            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine("Started sorting the array");
            var processorsCount = Environment.ProcessorCount;
            var elementsToSlice = array.Length / processorsCount;
            var elementsLeft = (array.Length % processorsCount) - 1;
            var arraySlices = new List<int[]>(processorsCount);
            var threads = new List<Thread>(processorsCount);

            for (int i = 0; i < processorsCount; i++)
            {
                // Cache the value of 'i' and use it
                // in the newly created thread
                var index = i;

                var thread = new Thread(() =>
                {
                    int[] arraySlice;
                    if (index == processorsCount - 1)
                    {
                        arraySlice = array.SubArray(index * elementsToSlice, elementsToSlice + elementsLeft);
                    }
                    else
                    {
                        arraySlice = array.SubArray(index * elementsToSlice, elementsToSlice);
                    }

                    arraySlices.Add(arraySlice);

                    var mergeSort = new MergeSort();
                    mergeSort.Sort(arraySlice);
                });
                thread.Start(); // Doesn't execute immediately
                threads.Add(thread);
            }

            // Wait for all threads to finish their work
            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Join();
            }

            // Merge results
            var result = RecursiveMerge(arraySlices);

            stopwatch.Stop();
            Console.WriteLine("Array sorted");

            // Elapsed time: 700-800 ms
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

        public static List<int[]> RecursiveMerge(List<int[]> arraySlices)
        {
            if (arraySlices.Count == 1)
            {
                return arraySlices;
            }

            var newArraySlices = new List<int[]>(arraySlices.Count / 2);

            for (int i = 0, j = arraySlices.Count - 1; i < arraySlices.Count / 2; i++, j--)
            {
                var mergedArray = Merge(arraySlices[i], arraySlices[j]);
                newArraySlices.Add(mergedArray);
            }

            return RecursiveMerge(newArraySlices);
        }

        public static int[] Merge(int[] leftPart, int[] rightPart)
        {
            int leftSize = leftPart.Length;
            int rightSize = rightPart.Length;
            int totalSize = leftSize + rightSize;
            int[] array = new int[totalSize];

            int i = 0, j = 0, k = 0;

            while (i < leftSize && j < rightSize)
            {
                if (leftPart[i] < rightPart[j])
                {
                    array[k++] = leftPart[i++];
                }
                else
                {
                    array[k++] = rightPart[j++];
                }
            }
            while (i < leftSize)
            {
                array[k++] = leftPart[i++];
            }
            while (j < rightSize)
            {
                array[k++] = rightPart[j++];
            }

            return array;
        }
    }
}
