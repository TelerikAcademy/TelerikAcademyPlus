using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingAndStoppingParallelFor
{
    class Program
    {
        public static List<int> GetNValues(List<int> values, int countToTake)
        {
            ConcurrentStack<int> valuesToReturn = new ConcurrentStack<int>();

            Parallel.For(0, values.Count, (index, loopState) =>
            {
                if(index < countToTake) 
                {
                    valuesToReturn.Push(values[index]);
                }
                else
                {
                    loopState.Stop();
                }
            });

            return valuesToReturn.ToList();
        }

        public static List<int> GetValuesBeforeValue(List<int> values, int stopperValue)
        {
            ConcurrentStack<int> valuesToReturn = new ConcurrentStack<int>();

            Parallel.For(0, values.Count, (index, loopState) =>
            {
                if (values[index] != stopperValue)
                {
                    valuesToReturn.Push(values[index]);
                }
                else
                {
                    loopState.Break();
                }
            });

            return valuesToReturn.ToList();
        }

        static void Main(string[] args)
        {
            List<int> values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            Console.WriteLine(String.Join(" ", GetValuesBeforeValue(values, 8)));
        }
    }
}
