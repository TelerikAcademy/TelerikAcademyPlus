using System;

namespace III.Merge.Sort.Parallel
{
    public static class ArrayExtensions
    {
        public static int[] SubArray(this int[] data, int index, int length)
        {
            int[] result = new int[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
