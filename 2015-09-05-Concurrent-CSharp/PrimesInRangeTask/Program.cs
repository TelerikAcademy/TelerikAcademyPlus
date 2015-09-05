using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimesInRangeTask
{
    class Program
    {
        static List<int> PrimesInRange(int rangeFirst, int rangeLast)
        {
            List<int> primes = new List<int>();

            for (int number = rangeFirst; number < rangeLast; number++)
            {
                bool isPrime = true;
                for (int divider = 2; divider < number; divider++)
                {
                    if (number % divider == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(number);
                }
            }

            return primes;
        }

        static async void PrintPrimesInRangeSumAsync(int rangeFirst, int rangeLast)
        {
            List<int> primes = 
                await Task.Run(() => PrimesInRange(rangeFirst, rangeLast));
            
            Task<long> calculatedPrimesSumTask = Task.Run(() =>
                {
                    long primesSum = 0;
                    foreach (var prime in primes)
                    {
                        primesSum += prime;
                    }

                    return primesSum;
                });

            long calculatedPrimesSum = await calculatedPrimesSumTask;

            Console.WriteLine(calculatedPrimesSum);
        }

        static void Main(string[] args)
        {
            PrintPrimesInRangeSumAsync(0, 100000);

            while (true)
            {
                Console.WriteLine("What's up?");
                Console.ReadLine();
            }
        }
    }
}
