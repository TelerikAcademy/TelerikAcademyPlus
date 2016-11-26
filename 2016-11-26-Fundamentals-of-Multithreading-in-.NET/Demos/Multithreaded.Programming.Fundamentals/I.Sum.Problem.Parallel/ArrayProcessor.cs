using System;
using System.Linq;
using System.Numerics;

namespace I.Sum.Problem.Parallel
{
    public class ArrayProcessor
    {
        private int[] array;
        private int startIndex;
        private int elementsToProcessCount;

        public ArrayProcessor(int[] array, int startIndex, int elementsToProcessCount)
        {
            this.array = array;
            this.startIndex = startIndex;
            this.elementsToProcessCount = elementsToProcessCount;
        }

        public BigInteger CalculatedSum { get; set; } = 0;

        public void CalculateSum()
        {
            var lastIndex = this.startIndex + this.elementsToProcessCount;

            for (int i = this.startIndex; i < lastIndex; i++)
            {
                this.CalculatedSum += this.array[i];
            }
        }
    }
}