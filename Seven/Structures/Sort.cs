using System;

namespace Seven.Structures
{
  /// <summary>Contains static sorting algorithms.</summary>
  public static class Sort
  {
    /// <summary>Sorts an entire array in non-decreasing order using the bubble sort algorithm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="compare">The compare function (returns a positive value if left is greater than right).</param>
    /// <param name="array">the array to be sorted</param>
    /// <remarks>Runtime: Omega(n), average(n^2), O(n^2). Memory: in place. Stability: yes.</remarks>
    public static void Bubble<Type>(Compare<Type> compare, Type[] array)
    {
      for (int i = 0; i < array.Length; i++)
        for (int j = 0; j < array.Length - 1; j++)
          if (compare(array[j], array[j + 1]) == Comparison.Greater)
          {
            Type temp = array[j + 1];
            array[j + 1] = array[j];
            array[j] = temp;
          }
    }

    /// <summary>Sorts an entire array in non-decreasing order using the selection sort algoritm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="compare">Returns negative if the left is less than the right.</param>
    /// <param name="array">the array to be sorted</param>
    /// <remarks>Runtime: Omega(n^2), average(n^2), O(n^2). Memory: in place. Stablity: no.</remarks>
    public static void Selection<Type>(Compare<Type> compare, Type[] array)
    {
      for (int i = 0; i < array.Length; i++)
      {
        int min = i;
        for (int j = i + 1; j < array.Length; j++)
          if (compare(array[j], array[min]) == Comparison.Less)
          {
            min = j;
            Type temp = array[i];
            array[i] = array[min];
            array[min] = temp;
          }
      }
    }

    /// <summary>Sorts an entire array in non-decreasing order using the insertion sort algorithm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="compare">Returns positive if left greater than right.</param>
    /// <param name="array">the array to be sorted</param>
    /// <remarks>Runtime: Omega(n), average(n^2), O(n^2). Memory: in place. Stablity: yes.</remarks>
    public static void Insertion<Type>(Compare<Type> compare, Type[] array)
    {
      for (int i = 1; i < array.Length; i++)
      {
        Type temp = array[i];
        int j;
        for (j = i; j > 0 && compare(array[j - 1], temp) == Comparison.Greater; j--)
          array[j] = array[j - 1];
        array[j] = temp;
      }
    }

    /// <summary>Sorts an entire array in non-decreasing order using the quick sort algorithm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="compare">The method of compare to be sorted by.</param>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n^2). Memory: ln(n). Stablity: no.</remarks>
    public static void Quick<Type>(Compare<Type> compare, Type[] array)
    {
      Sort.Quick(compare, array, 0, array.Length);
    }

    private static void Quick<Type>(Compare<Type> compare, Type[] array, int start, int len)
    {
      if (len > 1)
      {
        Type pivot = array[start];
        int i = start;
        int j = start + len - 1;
        int k = j;
        while (i <= j)
        {
          if (compare(array[j], pivot) == Comparison.Less)
          {
            Type temp = array[i];
            array[i++] = array[j];
            array[j] = temp;
          }
          else if (compare(array[j], pivot) == Comparison.Equal)
            j--;
          else
          {
            Type temp = array[k];
            array[k--] = array[j];
            array[j--] = temp;
          }
        }
        Sort.Quick(compare, array, start, i - start);
        Sort.Quick(compare, array, k + 1, start + len - (k + 1));
      }
    }

    /// <summary>Sorts up to an array in non-decreasing order using the merge sort algorithm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="compare">Returns zero or negative if the left is less than or equal to the right.</param>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n*ln(n)). Memory: n. Stablity: yes.</remarks>
    public static void Merge<Type>(Compare<Type> compare, Type[] array)
    {
      Merge<Type>(compare, array, 0, array.Length);
    }

    private static void Merge<Type>(Compare<Type> compare, Type[] array, int start, int len)
    {
      if (len > 1)
      {
        int half = len / 2;
        Sort.Merge<Type>(compare, array, start, half);
        Sort.Merge<Type>(compare, array, start + half, len - half);
        Type[] sorted = new Type[len];
        int i = start;
        int j = start + half;
        int k = 0;
        while (i < start + half && j < start + len)
        {
          if (compare(array[i], array[j]) == Comparison.Greater)
            sorted[k++] = array[j++];
          else
            sorted[k++] = array[i++];
        }
        for (int h = 0; h < start + half - i; h++)
          sorted[k + h] = array[i + h];
        for (int h = 0; h < start + len - j; h++)
          sorted[k + h] = array[j + h];
        for (int h = 0; h < len; h++)
          array[start + h] = sorted[0 + h];
      }
    }

    /// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="compare">The method of compare for the sort.</param>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n^2). Memory: in place. Stablity: no.</remarks>
    public static void Heap<Type>(Compare<Type> compare, Type[] array)
    {
      int heapSize = array.Length;
      for (int i = (heapSize - 1) / 2; i >= 0; i--)
        Sort.MaxHeapify(compare, array, heapSize, i);
      for (int i = array.Length - 1; i > 0; i--)
      {
        Type temp = array[0];
        array[0] = array[i];
        array[i] = temp;
        heapSize--;
        Sort.MaxHeapify(compare, array, heapSize, 0);
      }
    }

    private static void MaxHeapify<Type>(Compare<Type> compare, Type[] array, int heapSize, int index)
    {
      int left = (index + 1) * 2 - 1;
      int right = (index + 1) * 2;
      int largest = 0;
      if (left < heapSize && compare(array[left], array[index]) == Comparison.Greater)
        largest = left;
      else
        largest = index;
      if (right < heapSize && compare(array[right], array[largest]) == Comparison.Greater)
        largest = right;
      if (largest != index)
      {
        Type temp = array[index];
        array[index] = array[largest];
        array[largest] = temp;
        Sort.MaxHeapify(compare, array, heapSize, largest);
      }
    }

    /// <summary>Method specifically for computing object keys in the Counting Sort algorithm.</summary>
    /// <typeparam name="Type">The type of instances in the array to be sorted.</typeparam>
    /// <param name="instance">The instance to compute a counting key for.</param>
    /// <returns>The counting key computed from the provided instance.</returns>
    public delegate int ComputeCountingKey<Type>(Type instance);

    /// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="computeCountingKey">Method specifically for computing object keys in the Counting Sort algorithm.</param>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Theta(Max(key)). Memory: Max(key). Stablity: yes.</remarks>
    public static void Counting<Type>(ComputeCountingKey<Type> computeCountingKey, Type[] array)
    {
      int[] count = new int[array.Length];
      int maxKey = 0;
      for (int i = 0; i < array.Length; i++)
      {
        int key = computeCountingKey(array[i]) / array.Length;
        count[key] += 1;
        if (key > maxKey)
          maxKey = key;
      }

      int total = 0;
      for (int i = 0; i < maxKey; i++)
      {
        int oldCount = count[i];
        count[i] = total;
        total += oldCount;
      }

      Type[] output = new Type[maxKey];
      for (int i = 0; i < array.Length; i++)
      {
        int key = computeCountingKey(array[i]);
        output[count[key]] = array[i];
        count[computeCountingKey(array[i])] += 1;
      }
    }

    /// <summary>Sorts an entire array in a randomized order.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="array">The aray to shuffle.</param>
    /// <remarks>Runtime: O(n). Memory: in place. Stable: N/A (not a comparative sort).</remarks>
    public static void Shuffle<Type>(Type[] array)
    {
      Random random = new Random();
      for (int i = 0; i < array.Length; i++)
      {
        int randomIndex = random.Next(0, array.Length);
        Type temp = array[i];
        array[i] = array[randomIndex];
        array[randomIndex] = temp;
      }
    }

    /// <summary>Sorts an entire array in non-decreasing order using the slow sort algorithm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="compare">The method of compare for the sort.</param>
    /// <param name="array">The array to be sorted.</param>
    /// <remarks>Runtime: Omega(n), average(n*n!), O(infinity). Memory: in place. Stablity: no.</remarks>
    public static void Bogo<Type>(Compare<Type> compare, Type[] array)
    {
      while (!BogoCheck<Type>(compare, array))
        Sort.Shuffle(array);
    }

    private static bool BogoCheck<Type>(Compare<Type> compare, Type[] array)
    {
      for (int i = 0; i < array.Length - 1; i++)
        if (compare(array[i], array[i + 1]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Sorts an entire array of in non-decreasing order using the slow sort algorithm.</summary>
    /// <typeparam name="Type">The type of objects stored within the array.</typeparam>
    /// <param name="compare">The method of compare for the sort.</param>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n), average(n*n!), O(n*n!). Memory: in place. Stablity: N/A (not yet analyzed).</remarks>
    public static void Slow<Type>(Compare<Type> compare, Type[] array)
    {
      Sort.Slow<Type>(compare, array, 0, array.Length);
    }

    private static void Slow<Type>(Compare<Type> compare, Type[] array, int i, int j)
    {
      if (i >= j)
        return;
      int m = (i + j) / 2;
      Sort.Slow(compare, array, i, m);
      Sort.Slow(compare, array, m + 1, j);
      if (compare(array[m], array[j]) == Comparison.Less)
      {
        Type temp = array[j];
        array[j] = array[m];
        array[m] = temp;
      }
      Sort.Slow(compare, array, i, j - 1);
    }
  }
}
