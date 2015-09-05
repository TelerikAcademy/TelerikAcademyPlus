using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsInParallelLoops
{
    class ExceptionsInParallelLoops
    {
        private static IDictionary<int, double> ProcessSquareRootsParallel(int[] integers)
        {
            //Note: this could safely be an array, where the indices are actually the "integers" and the values are their square roots, 
            //because even if there is a race condition, the end value is always going to be the same.
            ConcurrentDictionary<int, double> integerSquareRoots = new ConcurrentDictionary<int, double>();
            Parallel.ForEach(integers, integer =>
            {
                if (integer >= 0)
                {
                    integerSquareRoots[integer] = Math.Sqrt(integer);
                    Console.WriteLine("Computed for " + integer);
                }
                else
                {
                    throw new ArgumentException("All integers must be positive. Cannot compute sqrt of negative numbers.", "integers");
                }
            });

            return integerSquareRoots;
        }


        static void Main(string[] args)
        {
            var integers = Enumerable.Range(0, 100).ToList();
            integers.Insert(3, -1); //Inserting a negative to force the loop to fail when it executes for the element at index 3

            IDictionary<int, double> squareRootsByInteger;
            try
            {
                squareRootsByInteger = ProcessSquareRootsParallel(integers.ToArray());
                foreach (var entry in squareRootsByInteger)
                {
                    Console.WriteLine("sqrt(" + entry.Key + ") = " + entry.Value);
                }
            }
            catch (AggregateException)
            {
                Console.WriteLine("One or more of square roots for the numbers failed to be calculated.");
            }
        }
    }
}
