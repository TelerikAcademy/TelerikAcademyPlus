using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatingAndContinuingTasks
{
    class CreatingAndContinuingTasks
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
                foreach (var entry in loadTask.Result)
                {
                    destinationTable[entry.Key] = entry.Value;
                }
            });
        }

        static void Main(string[] args)
        {
            ConcurrentDictionary<int, double> numberSquareRoots = new ConcurrentDictionary<int, double>();
            LoadSquareRootsLookupTableAsync("squareRootLookupTable.txt", numberSquareRoots);

            int number = 0;
            while (true)
            {
                //Note: the table loads pretty fast, to make it easier to see the asynchronous load, uncomment this lines (and comment the console read)
                //number++;
                number = int.Parse(Console.ReadLine());
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
