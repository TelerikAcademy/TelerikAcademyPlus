using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContunuingTasksWithExceptionHandling
{
    class ContunuingTasksWithExceptionHandling
    {
        static Dictionary<int, double> ReadSquareRootsLookupTable(string filename)
        {
            Dictionary<int, double> loadedNumberSquareRoots = new Dictionary<int, double>();
            foreach (string line in System.IO.File.ReadAllLines(filename))
            {
                int number;
                double sqRoot;

                string[] numberAndRoot = line.Split(' ');
                number = int.Parse(numberAndRoot[0]);
                sqRoot = double.Parse(numberAndRoot[1]);

                loadedNumberSquareRoots[number] = sqRoot;
            }

            return loadedNumberSquareRoots;
        }

        private static Task<Dictionary<int, double>> RunReadSquareRootsLookupTableTask(string filename)
        {
            return Task.Run(() =>
            {
                return ReadSquareRootsLookupTable(filename);
            });
        }

        static void LoadSquareRootsLookupTableAsync(string filename, ConcurrentDictionary<int, double> destinationTable)
        {
            RunReadSquareRootsLookupTableTask(filename)
            .ContinueWith((loadTask) =>
            {
                if(loadTask.IsFaulted)
                {
                    //Note: See the problem here?
                    Console.WriteLine("The lookup table failed to load due to: " + loadTask.Exception);
                }

                foreach (var entry in loadTask.Result)
                {
                    destinationTable[entry.Key] = entry.Value;
                }
            });
        }

        static void Main(string[] args)
        {
            ConcurrentDictionary<int, double> numberSquareRoots = new ConcurrentDictionary<int, double>();
            LoadSquareRootsLookupTableAsync("corrupt-squareRootLookupTable.txt", numberSquareRoots);

            while (true)
            {
                int number = int.Parse(Console.ReadLine());
                if (numberSquareRoots.ContainsKey(number))
                {
                    var squareRoot = numberSquareRoots[number];
                    Console.WriteLine("Square root of " + number + " = " + squareRoot);
                }
                else
                {
                    Console.WriteLine("Not yet loaded, please try again later");
                }
            }
        }
    }
}
