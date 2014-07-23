using System;

using Seven;
using Seven.Mathematics;
using Seven.Structures;

using System.Diagnostics;

namespace Testing
{
  class Program
  {
    public static Comparison Compare(int left, int right)
    {
      int comparison = left.CompareTo(right);
      if (comparison < 0)
        return Comparison.Less;
      else if (comparison > 0)
        return Comparison.Greater;
      else
        return Comparison.Equal;
    }

    // Simple comparison function for doubles
    public static Comparison Compare(double left, double right)
    {
      // NOTE: "Comparison" is jsut an enum
      int comparison = left.CompareTo(right);
      if (comparison < 0)
        return Comparison.Less;
      else if (comparison > 0)
        return Comparison.Greater;
      else
        return Comparison.Equal;
    }

    static void Main(string[] args)
    {
      int test = 10;


      Console.WriteLine("Welcome To SevenFramework!!!!");
      Console.WriteLine();
      Console.WriteLine(@"The 'Testing' project is simply a little testing environment for the code in the 'Seven' project (the entire SevenFramework is within the 'Seven' project at this time).");
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Data Structures=======================================");
      Console.WriteLine("______________________________________________________");
      Console.WriteLine();
      Console.WriteLine("Storing " + (test) + " items in each structure (except link)");
      Console.WriteLine();

      Console.WriteLine(" Testing Link--------------------------------");
      Console.WriteLine("  Size: 6");
      Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
      Console.Write("   Delegate: ");
      link.Foreach((dynamic current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (object value in link)
        Console.Write(value);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing Array_Array<int>-------------------");
      Array<int> array = new Array_Array<int>(test);
      for (int i = 0; i < test; i++)
        array[i] = i;
      Console.Write("   Delegate: ");
      array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing List_Array<int>--------------------");
      List<int> list_array = new List_Array<int>(test);
      for (int i = 0; i < test; i++)
        list_array.Add(i);
      Console.Write("   Delegate: ");
      list_array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in list_array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing List_Linked<int>-------------------");
      List<int> list_linked = new List_Linked<int>();
      for (int i = 0; i < test; i++)
        list_linked.Add(i);
      Console.Write("   Delegate: ");
      list_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in list_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing Stack_Linked<int>------------------");
      Stack<int> stack_linked = new Stack_Linked<int>();
      for (int i = 0; i < test; i++)
        stack_linked.Push(i);
      Console.Write("   Delegate: ");
      stack_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in stack_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing Queue_Linked<int>------------------");
      Queue<int> queue_linked = new Queue_Linked<int>();
      for (int i = 0; i < test; i++)
        queue_linked.Enqueue(i);
      Console.Write("   Delegate: ");
      queue_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in queue_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing Heap_Array<int>--------------------");
      Heap<int> heap_array = new Heap_Array<int>(Compare);
      for (int i = 0; i < test; i++)
        heap_array.Enqueue(i);
      Console.Write("   Delegate: ");
      heap_array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in heap_array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing AvlTree_Linked<int>----------------");
      AvlTree<int> avlTree_linked = new AvlTree_Linked<int>(Compare);
      for (int i = 0; i < test; i++)
        avlTree_linked.Add(i);
      Console.Write("   Delegate: ");
      avlTree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in avlTree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing RedBlack_Linked<int>---------------");
      RedBlackTree<int> redBlackTree_linked = new RedBlackTree_Linked<int>(Compare);
      for (int i = 0; i < test; i++)
        redBlackTree_linked.Add(i);
      Console.Write("   Delegate: ");
      redBlackTree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in redBlackTree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing HashTable_Linked<int, int>---------");
      Map<int, int> hashTable_linked = new Map_Linked<int, int>(
        (int left, int right) => { return left == right; },
        (int integer) => { return integer; });
      for (int i = 0; i < test; i++)
        hashTable_linked.Add(i, i);
      Console.Write("   Look Ups: ");
      for (int i = 0; i < test; i++)
        Console.Write(hashTable_linked[i]);
      Console.WriteLine();
      Console.Write("   Delegate: ");
      hashTable_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in hashTable_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("The quadtree has a minor bug. I'm already working on it.");
      Console.WriteLine(" Testing Quadtree_Linked<int, double>-------");
      Quadtree<int> quadtree_linked = new Quadtree_Linked<int, double>(
        -test - 1, -test - 1, // minimum dimensions of the quadtree
        test + 1, test + 1, // maximum dimensions of the quadtree
        test + 1, // load factor
        (int i, out double x, out double y) => { x = i; y = i; },
        Program.Compare,
        (double left, double right) => { return (left + right) * 0.5d; });
      for (int i = 0; i < test; i++)
        quadtree_linked.Add(i);
      Console.Write("   Delegate: ");
      quadtree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in quadtree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("The octree has a minor bug. I'm already working on it.");
      Console.WriteLine(" Testing Octree_Linked<int, double>---------");
      Octree<int> octree_linked = new Octree_Linked<int, double>(
        -test - 1, -test - 1, -test - 1, // minimum dimensions of the octree
        test + 1, test + 1, test + 1, // maximum dimensions of the octree
        test + 1, // load factor
        (int i, out double x, out double y, out double z) => { x = i; y = i; z = i; },
        Program.Compare,
        (double left, double right) => { return (left + right) * 0.5d; });
      for (int i = 0; i < test; i++)
        octree_linked.Add(i);
      Console.Write("   Delegate: ");
      octree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in octree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      // The octree has a minor bug. I'm already working on it.
      Console.WriteLine(" Testing Omnitree_Linked<int, double>-------");
      Omnitree<int, double> omnitree_linked = new Omnitree_Linked<int, double>(
        new double[] { -test - 1, -test - 1, -test - 1 }, // minimum dimensions of the omnitree
        new double[] { test + 1, test + 1, test + 1 }, // maximum dimensions of the omnitree
        (int i, out double[] location) => { location = new double[] { i, i, i }; },
        Program.Compare, // comparison function
        (double left, double right) => { return (left + right) * 0.5d; }); // average function
      for (int i = 0; i < test; i++)
        omnitree_linked.Add(i);
      Console.Write("   Delegate: ");
      omnitree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in omnitree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

			#region Omnitree_Array

			Console.WriteLine(" Testing Omnitree_Array<int, double>---------------");
			Omnitree<int, double> omnitree_array = new Omnitree_Array<int, double>(
				new double[] { -test - 1, -test - 1, -test - 1 }, // minimum dimensions of the omnitree
				new double[] { test + 1, test + 1, test + 1 }, // maximum dimensions of the omnitree
				(int i, out double[] location) => { location = new double[] { i, i, i }; },
				Program.Compare, // comparison function
				(double left, double right) => { return (left + right) * 0.5d; }); // average function
			Console.WriteLine("     Origin: [" + omnitree_array.Origin[0] + ", " + omnitree_array.Origin[1] + ", " + omnitree_array.Origin[2] + "]");
			Console.WriteLine("     Minimum: [" + omnitree_array.Min[0] + ", " + omnitree_array.Min[1] + ", " + omnitree_array.Min[2] + "]");
			Console.WriteLine("     Maximum: [" + omnitree_array.Max[0] + ", " + omnitree_array.Max[1] + ", " + omnitree_array.Max[2] + "]");
			Console.WriteLine("     Dimensions: " + omnitree_array.Dimensions);
			Console.WriteLine("     Count: " + omnitree_array.Count);
			Console.Write("   Adding 0-" + test + ": ");
			for (int i = 0; i < test; i++)
				omnitree_array.Add(i);
			omnitree_array.Foreach((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.WriteLine("     Count: " + omnitree_array.Count);
			Console.Write("   Foreach (delegate) [ALL]: ");
			omnitree_array.Foreach((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.Write("   Foreach (delegate) [" + (test / 2) + "-" + test + "]: ");
			omnitree_array.Foreach((int current) => { Console.Write(current); },
				new double[] { test / 2, test / 2, test / 2 },
				new double[] { test, test, test });
			Console.WriteLine();
			Console.Write("   Remove 0-" + test / 3 + ": ");
			omnitree_array.Remove(
				new double[] { 0, 0, 0 },
				new double[] { test / 3, test / 3, test / 3 });
			omnitree_array.Foreach((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.WriteLine("     Count: " + omnitree_array.Count);
			Console.Write("   Clear: ");
			omnitree_array.Clear();
			omnitree_array.Foreach((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.WriteLine("     Count: " + omnitree_array.Count);
			Console.WriteLine();

			#endregion

      //Console.WriteLine("Speed Testing===========================================");
      //Console.WriteLine();
      //Console.WriteLine("RUN EXE OUTSIDE VISUAL STUDIO FOR FAIR COMPARISON.");
      //Console.WriteLine("May take upt to a minute or two...");
      //Console.WriteLine("Units: ticks");
      //int iterations = 10000000;
      //Console.WriteLine("Test Size: " + iterations * 2);
      //Console.WriteLine();
      //Test_LIST(iterations);
      //Test_OMNITREE_ARRAY(iterations);
      //Test_OMNITREE_LINKED(iterations);

      Console.WriteLine("Mathematics===========================================");
      Console.WriteLine("______________________________________________________");
      Console.WriteLine();

      Console.WriteLine("  Linear Algebra------------------");
      Console.WriteLine();
      Console.WriteLine();

      Random random = new Random();

      double[,] A = new double[4,4];

      for (int i = 0; i < A.GetLength(0); i++)
        for (int j = 0; j < A.GetLength(1); j++)
          A[i, j] = random.NextDouble();
      // float casting... poor mans double formatter :)
      Console.WriteLine("    Matrix A (randomized 4x4): ");
      for (int i = 0; i < A.GetLength(0); i++)
      {
        Console.Write("      ");
        for (int j = 0; j < A.GetLength(1); j++)
          Console.Write(string.Format("{0:0.00}", A[i, j]) + " ");
        Console.WriteLine();
      }
      Console.WriteLine();

      double[,] AplusA = A.Add(A);
      Console.WriteLine("    A + A (aka 2A): ");
      for (int i = 0; i < AplusA.GetLength(0); i++)
      {
        Console.Write("      ");
        for (int j = 0; j < AplusA.GetLength(1); j++)
          Console.Write(string.Format("{0:0.00}", AplusA[i, j]) + " ");
        Console.WriteLine();
      }
      Console.WriteLine();

      double[,] AxA = A.Multiply(A);
      Console.WriteLine("    A * A (aka A ^ 2): ");
      for (int i = 0; i < AxA.GetLength(0); i++)
      {
        Console.Write("      ");
        for (int j = 0; j < AxA.GetLength(1); j++)
          Console.Write(string.Format("{0:0.00}", AxA[i, j]) + " ");
        Console.WriteLine();
      }
      Console.WriteLine();

      double[,] rref_A = A.ReducedEchelon();
      Console.WriteLine("    rref(A): ");
      for (int i = 0; i < rref_A.GetLength(0); i++)
      {
        Console.Write("      ");
        for (int j = 0; j < rref_A.GetLength(1); j++)
          Console.Write(string.Format("{0:0.00}", rref_A[i, j]) + " ");
        Console.WriteLine();
      }
      Console.WriteLine();

      double determinent = A.Determinent();
      Console.WriteLine("    determinent(A): ");
      Console.Write("      " + string.Format("{0:0.00}", determinent));

      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine("--------------------------------------------");
      Console.WriteLine("============================================");
      Console.WriteLine("____________________________________________");
      Console.WriteLine(" Testing Complete...");
      Console.ReadLine();
    }

    public static void SpeedTest()
    {
      int test = 19999999;

      Stopwatch timer = new Stopwatch();

      Console.WriteLine("Speed Testing List==========================");
      Console.WriteLine();
      Console.WriteLine("RUN THE EXE OUTSIDE VISUAL STUDIO FOR SPEED ANALYSIS");
      Console.WriteLine("Testing Size: " + test);
      Console.WriteLine("Units: seconds");
      Console.WriteLine();

      Console.WriteLine(" .Net Array----------------------------------");
      int[] array_net = new int[test];
      for (int i = 0; i < test; i++)
        array_net[i] = i;
      timer.Restart();
      foreach (int i in array_net)
        continue;
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (IEnumerable)");
      Console.WriteLine("   Traversal: N/A   (Delegate)");

      Console.WriteLine(" Seven Array_Array---------------------------");
      Array<int> array_array = new Array_Array<int>(test);
      for (int i = 0; i < test; i++)
        array_array[i] = i;
      timer.Restart();
      foreach (int i in array_array)
        continue;
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (IEnumerable)");
      timer.Restart();
      array_array.Foreach((int current) => { });
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (Delegate)");
      Console.WriteLine();

      Console.WriteLine(" .Net List-----------------------------------");
      timer.Restart();
      System.Collections.Generic.List<int> list_net =
        new System.Collections.Generic.List<int>();
      for (int i = 0; i < test; i++)
        list_net.Add(i);
      timer.Stop();
      Console.WriteLine("   Insertion: " + (float)timer.ElapsedMilliseconds / 1000.0f);
      timer.Restart();
      foreach (int i in list_net)
        continue;
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (IEnumerable)");
      timer.Restart();
      list_net.ForEach((int current) => { return; });
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (Delegate)");

      Console.WriteLine(" Seven List_Array----------------------------");
      timer.Restart();
      List<int> list_array = new List_Array<int>();
      for (int i = 0; i < test; i++)
        list_array.Add(i);
      timer.Stop();
      Console.WriteLine("   Insertion: " + (float)timer.ElapsedMilliseconds / 1000.0f);
      timer.Restart();
      foreach (int i in list_array)
        continue;
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (IEnumerable)");
      timer.Restart();
      list_array.Foreach((int current) => { return; });
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (Delegate)");
      Console.WriteLine();

      Console.WriteLine(" .Net LinkedList------------------------------");
      timer.Restart();
      System.Collections.Generic.LinkedList<int> linkedlist_net =
        new System.Collections.Generic.LinkedList<int>();
      for (int i = 0; i < test; i++)
        linkedlist_net.AddLast(i);
      timer.Stop();
      Console.WriteLine("   Insertion: " + (float)timer.ElapsedMilliseconds / 1000.0f);
      timer.Restart();
      foreach (int current in linkedlist_net)
        continue;
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (IEnumerable)");
      Console.WriteLine("   Traversal: N/A   (Delegate)");

      Console.WriteLine(" Seven List_Linked--------------------------");
      timer.Restart();
      List<int> list_linked = new List_Linked<int>();
      for (int i = 0; i < test; i++)
        list_linked.Add(i);
      timer.Stop();
      Console.WriteLine("   Insertion: " + (float)timer.ElapsedMilliseconds / 1000.0f);
      timer.Restart();
      foreach (int i in list_linked)
        continue;
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (IEnumerable)");
      timer.Restart();
      list_linked.Foreach((int current) => { return; });
      timer.Stop();
      Console.WriteLine("   Traversal: " + (float)timer.ElapsedMilliseconds / 1000.0f + " (Delegate)");
      Console.WriteLine();
    }

		public static void Test_OMNITREE_ARRAY(int iterations)
    {
      Stopwatch timer = new Stopwatch();

      Console.WriteLine("OMNITREE (ARRAY)-------------------");
      Omnitree<Tuple<double, double, double>, double> omnitree_array =
        new Omnitree_Array<Tuple<double, double, double>, double>(
        new double[] { -iterations - 1, -iterations - 1, -iterations - 1 },
        new double[] { iterations + 1, iterations + 1, iterations + 1 },
        (Tuple<double, double, double> i, out double[] location) => { location = new double[] { i.Item1, i.Item2, i.Item3 }; },
        Program.Compare, // just a comparison function...
        (double left, double right) => { return (left + right) * 0.5d; });

      timer.Restart();
      for (int i = -iterations; i < iterations; i++)
				(omnitree_array as Omnitree_Array<Tuple<double, double, double>, double>).Add(new Tuple<double, double, double>(i, i, i));
      timer.Stop();
			Console.WriteLine("ADD TIME:");
			Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      int count2 = 0;
      omnitree_array.Foreach(
        (Tuple<double, double, double> current) => { count2++; });
      timer.Stop();
      Console.WriteLine("FOREACH (FULL):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count2 = 0;
      omnitree_array.Foreach(
        (Tuple<double, double, double> current) => { count2++; },
        new double[] { 0, 0, 0 },
        new double[] { 25000000, 25000000, 25000000 });
      timer.Stop();
      Console.WriteLine("FOREACH (250000):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count2 = 0;
      omnitree_array.Foreach(
        (Tuple<double, double, double> current) => { count2++; },
        new double[] { 0, 0, 0 },
        new double[] { 250000, 250000, 250000 });
      timer.Stop();
      Console.WriteLine("FOREACH (250000):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count2 = 0;
      omnitree_array.Foreach(
        (Tuple<double, double, double> current) => { count2++; },
        new double[] { 0, 0, 0 },
        new double[] { 2500, 2500, 2500 });
      timer.Stop();
      Console.WriteLine("FOREACH (2500):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count2 = 0;
      omnitree_array.Foreach(
        (Tuple<double, double, double> current) => { count2++; },
        new double[] { 0, 0, 0 },
        new double[] { 25, 25, 25 });
      timer.Stop();
      Console.WriteLine("FOREACH (25):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

			timer.Restart();
			omnitree_array.Remove(
				new double[] { 0, 0, 0 },
				new double[] { 2500, 2500, 2500 });
			timer.Stop();
			Console.WriteLine("REMOVE (2500):");
			Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

			timer.Restart();
			count2 = 0;
			omnitree_array.Foreach(
				(Tuple<double, double, double> current) => { count2++; });
			timer.Stop();
			Console.WriteLine("FOREACH (FULL):");
			Console.WriteLine("  Items Found: " + count2);
			Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);
    }

    public static void Test_OMNITREE_LINKED(int iterations)
    {
      Stopwatch timer = new Stopwatch();

      Console.WriteLine("OMNITREE (LINKED)-------------------");
      Omnitree<Tuple<double, double, double>, double> omnitree_linked =
        new Omnitree_Linked<Tuple<double, double, double>, double>(
        new double[] { -iterations - 1, -iterations - 1, -iterations - 1 },
        new double[] { iterations + 1, iterations + 1, iterations + 1 },
        (Tuple<double, double, double> i, out double[] location) => { location = new double[] { i.Item1, i.Item2, i.Item3 }; },
        Program.Compare, // just a comparison function...
        (double left, double right) => { return (left + right) * 0.5d; });

      timer.Restart();
      for (int i = -iterations; i < iterations; i++)
        omnitree_linked.Add(new Tuple<double, double, double>(i, i, i));
      timer.Stop();
			Console.WriteLine("ADD TIME:");
			Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      int count2 = 0;
      omnitree_linked.Foreach(
        (Tuple<double, double, double> current) => { count2++; });
      timer.Stop();
      Console.WriteLine("FOREACH (FULL):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count2 = 0;
      omnitree_linked.Foreach(
        (Tuple<double, double, double> current) => { count2++; },
        new double[] { 0, 0, 0 },
        new double[] { 25000000, 25000000, 25000000 });
      timer.Stop();
      Console.WriteLine("FOREACH (250000):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count2 = 0;
      omnitree_linked.Foreach(
        (Tuple<double, double, double> current) => { count2++; },
        new double[] { 0, 0, 0 },
        new double[] { 250000, 250000, 250000 });
      timer.Stop();
      Console.WriteLine("FOREACH (250000):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count2 = 0;
      omnitree_linked.Foreach(
        (Tuple<double, double, double> current) => { count2++; },
        new double[] { 0, 0, 0 },
        new double[] { 2500, 2500, 2500 });
      timer.Stop();
      Console.WriteLine("FOREACH (2500):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count2 = 0;
      omnitree_linked.Foreach(
        (Tuple<double, double, double> current) => { count2++; },
        new double[] { 0, 0, 0 },
        new double[] { 25, 25, 25 });
      timer.Stop();
      Console.WriteLine("FOREACH (25):");
      Console.WriteLine("  Items Found: " + count2);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);
    }

		public static void Test_LIST(int iterations)
    {
      Stopwatch timer = new Stopwatch();

      Console.WriteLine("LIST----------------------------");
      System.Collections.Generic.List<Tuple<double, double, double>> list =
        new System.Collections.Generic.List<Tuple<double, double, double>>(1);

      timer.Restart();
      for (int i = -iterations; i < iterations; i++)
        list.Add(new Tuple<double, double, double>(i, i, i));
      timer.Stop();
      Console.WriteLine("ADD TIME:");
			Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      int count = 0;
      foreach (Tuple<double, double, double> i in list)
      {
        count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (FULL):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in list)
      {
        if (
          i.Item1 >= 0 && i.Item1 <= 25000000 &&
          i.Item2 >= 0 && i.Item2 <= 25000000 &&
          i.Item3 >= 0 && i.Item3 <= 25000000)
          count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (25000000):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in list)
      {
        if (
          i.Item1 >= 0 && i.Item1 <= 250000 &&
          i.Item2 >= 0 && i.Item2 <= 250000 &&
          i.Item3 >= 0 && i.Item3 <= 250000)
          count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (250000):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in list)
      {
        if (
          i.Item1 >= 0 && i.Item1 <= 2500 &&
          i.Item2 >= 0 && i.Item2 <= 2500 &&
          i.Item3 >= 0 && i.Item3 <= 2500)
          count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (2500):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in list)
      {
        if (
          i.Item1 >= 0 && i.Item1 <= 25 &&
          i.Item2 >= 0 && i.Item2 <= 25 &&
          i.Item3 >= 0 && i.Item3 <= 25)
          count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (25):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);


      Console.WriteLine("LIST (IEnumerable)------------------------");
      System.Collections.Generic.IEnumerable<Tuple<double, double, double>> enumerable = list;

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in enumerable)
      {
        count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (FULL):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in enumerable)
      {
        if (
          i.Item1 >= 0 && i.Item1 <= 25000000 &&
          i.Item2 >= 0 && i.Item2 <= 25000000 &&
          i.Item3 >= 0 && i.Item3 <= 25000000)
          count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (25000000):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in enumerable)
      {
        if (
          i.Item1 >= 0 && i.Item1 <= 250000 &&
          i.Item2 >= 0 && i.Item2 <= 250000 &&
          i.Item3 >= 0 && i.Item3 <= 250000)
          count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (250000):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in enumerable)
      {
        if (
          i.Item1 >= 0 && i.Item1 <= 2500 &&
          i.Item2 >= 0 && i.Item2 <= 2500 &&
          i.Item3 >= 0 && i.Item3 <= 2500)
          count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (2500):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

      timer.Restart();
      count = 0;
      foreach (Tuple<double, double, double> i in enumerable)
      {
        if (
          i.Item1 >= 0 && i.Item1 <= 25 &&
          i.Item2 >= 0 && i.Item2 <= 25 &&
          i.Item3 >= 0 && i.Item3 <= 25)
          count++;
      }
      timer.Stop();
      Console.WriteLine("FOREACH (25):");
      Console.WriteLine("  Items Found: " + count);
      Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

			timer.Restart();
			count = 0;

			System.Collections.Generic.List<Tuple<double, double, double>> list2 =
				new System.Collections.Generic.List<Tuple<double, double, double>>();

			for (int i = 0; i < list.Count; i++)
				if (
						!(list[i].Item1 >= 0 && list[i].Item1 <= 2500 &&
						list[i].Item2 >= 0 && list[i].Item2 <= 2500 &&
						list[i].Item3 >= 0 && list[i].Item3 <= 2500))
					list2.Add(list[i]);
			timer.Stop();
			Console.WriteLine("REMOVE (2500):");
			Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);

			timer.Restart();
			count = 0;
			foreach (Tuple<double, double, double> i in list)
			{
				count++;
			}
			timer.Stop();
			Console.WriteLine("FOREACH (FULL):");
			Console.WriteLine("  Items Found: " + count);
			Console.WriteLine("  Time Elapsed (ticks): " + timer.ElapsedTicks);
			Console.WriteLine("  Time Elapsed (sec): " + timer.ElapsedMilliseconds / 1000.0d);
    }
  }
}