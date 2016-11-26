namespace III.Merge.Sort.Parallel
{
    public class MergeSort
    {
        public void Sort(int[] array)
        {
            int arraySize = array.Length;
            int mid = (arraySize / 2);
            int[] leftPart = new int[mid];
            int[] rightPart = new int[arraySize - mid];

            if (arraySize < 2)
            {
                return;
            }

            for (int i = 0; i < mid; i++)
            {
                leftPart[i] = array[i];
            }
            for (int i = mid; i < arraySize; i++)
            {
                rightPart[i - mid] = array[i];
            }

            Sort(leftPart);
            Sort(rightPart);
            Merge(array, leftPart, rightPart);
        }

        public void Merge(int[] array, int[] leftPart, int[] rightPart)
        {
            int leftSize = leftPart.Length;
            int rightSize = rightPart.Length;
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
        }
    }
}
