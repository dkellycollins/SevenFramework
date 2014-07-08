using System;

using Seven;
using Seven.Structures;
using System.Diagnostics;

using System.Linq;
using System.Runtime.InteropServices;

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

    //public static Comparison Compare(double left, double right)
    //{
    //  int comparison = left.CompareTo(right);
    //  if (comparison < 0)
    //    return Comparison.Less;
    //  else if (comparison > 0)
    //    return Comparison.Greater;
    //  else
    //    return Comparison.Equal;
    //}



    public class Model
    {
      public double X { get; set; }
      public double Y { get; set; }
      public double Z { get; set; }

      public Model(double x, double y, double z) { X = x; Y = y; Z = z; }
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

      string message =
      @"HELLO! WELCOME TO THE SEVEN FRAMEWORK! 

The framework is a general framework to help with any form of programming. Currently, the most useful component of the framwork is the library of data structures. This library includes the folloing: Links (aka Tuples), Arrays, Lists, Queues, Stacks, Heaps, HashTables, AvlTrees, RedBlackTrees, Octrees, and SkipLists. The followoing data is testing for each structure:";

      //// Example of constructing an Omnitree:
      //Omnitree<Model, double> omni = new Omnitree_Linked<Model, double>(
      //  new double[] { -1000, -1100, -1200 },
      //  new double[] { 1200, 1100, 1000 },
      //  (Model m, out double[] location) => { location = new double[] { m.X, m.Y, m.Z }; },
      //  Compare, // the function above
      //  (double left, double right) => { return (left + right) * 0.5d; });

      //// Nows lets add a few Models to the tree
      //omni.Add(new Model(100.0d, -10.743d, 7.0d));
      //omni.Add(new Model(777.77d, -777.7d, 77d));

      //// Now lets get all the models within a specific area
      //// The area we are selecting is a rectangular prism
      //// NOTE: only the first model we added will be within these coordinates
      //omni.Foreach(
      //  (Model m) => { Console.WriteLine(m.X + " " + m.Y + " " + m.Z); },
      //  new double[] { -1000, -1000, -1000 }, // minimum values of the rectangular prism
      //  new double[] { 500, 500, 500 }); // maximum values of the rectangular prism

      //Console.ReadLine();
      //return;


      //Console.WriteLine(message);
      //Console.WriteLine();

      //Console.WriteLine("Functionality Testing=======================");
      //Console.WriteLine();
      //Console.WriteLine("Storing " + (test) + " items in each structure (except link)");
      //Console.WriteLine();

      //Console.WriteLine(" Testing Link--------------------------------");
      //Console.WriteLine("  Size: 6");
      //Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
      //Console.Write("   Delegate: ");
      //link.Foreach((dynamic current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (object value in link)
      //  Console.Write(value);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing Array_Array<int>-------------------");
      //Array<int> array = new Array_Array<int>(test);
      //for (int i = 0; i < test; i++)
      //  array[i] = i;
      //Console.Write("   Delegate: ");
      //array.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in array)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing List_Array<int>--------------------");
      //List<int> list_array = new List_Array<int>(test);
      //for (int i = 0; i < test; i++)
      //  list_array.Add(i);
      //Console.Write("   Delegate: ");
      //list_array.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in list_array)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing List_Linked<int>-------------------");
      //List<int> list_linked = new List_Linked<int>();
      //for (int i = 0; i < test; i++)
      //  list_linked.Add(i);
      //Console.Write("   Delegate: ");
      //list_linked.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in list_linked)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing Stack_Linked<int>------------------");
      //Stack<int> stack_linked = new Stack_Linked<int>();
      //for (int i = 0; i < test; i++)
      //  stack_linked.Push(i);
      //Console.Write("   Delegate: ");
      //stack_linked.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in stack_linked)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing Queue_Linked<int>------------------");
      //Queue<int> queue_linked = new Queue_Linked<int>();
      //for (int i = 0; i < test; i++)
      //  queue_linked.Enqueue(i);
      //Console.Write("   Delegate: ");
      //queue_linked.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in queue_linked)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing Heap_Array<int>--------------------");
      //Heap<int> heap_array = new Heap_Array<int>(Compare);
      //for (int i = 0; i < test; i++)
      //  heap_array.Enqueue(i);
      //Console.Write("   Delegate: ");
      //heap_array.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in heap_array)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing AvlTree_Linked<int>----------------");
      //AvlTree<int> avlTree_linked = new AvlTree_Linked<int>(Compare);
      //for (int i = 0; i < test; i++)
      //  avlTree_linked.Add(i);
      //Console.Write("   Delegate: ");
      //avlTree_linked .Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in avlTree_linked)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing RedBlack_Linked<int>---------------");
      //RedBlackTree<int> redBlackTree_linked = new RedBlackTree_Linked<int>(Compare);
      //for (int i = 0; i < test; i++)
      //  redBlackTree_linked.Add(i);
      //Console.Write("   Delegate: ");
      //redBlackTree_linked.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in redBlackTree_linked)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //Console.WriteLine(" Testing HashTable_Linked<int, int>---------");
      //Map<int, int> hashTable_linked = new Map_Linked<int, int>(
      //  (int left, int right) => { return left == right; },
      //  (int integer) => { return integer; });
      //for (int i = 0; i < test; i++)
      //  hashTable_linked.Add(i, i);
      //Console.Write("   Look Ups: ");
      //for (int i = 0; i < test; i++)
      //  Console.Write(hashTable_linked[i]);
      //Console.WriteLine();
      //Console.Write("   Delegate: ");
      //hashTable_linked.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in hashTable_linked)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //// The quadtree has a minor bug. I'm already working on it.
      //Console.WriteLine(" Testing Quadtree_Linked<int, double>-------");
      //Quadtree<int> quadtree_linked = new Quadtree_Linked<int, double>(
      //  -test - 1, -test - 1, // minimum dimensions of the quadtree
      //  test + 1, test + 1, // maximum dimensions of the quadtree
      //  test + 1, // load factor
      //  (int i, out double x, out double y) => { x = i; y = i; },
      //  Program.Compare,
      //  (double left, double right) => { return (left + right) * 0.5d; });
      //for (int i = 0; i < test; i++)
      //  quadtree_linked.Add(i);
      //Console.Write("   Delegate: ");
      //quadtree_linked.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in quadtree_linked)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //// The octree has a minor bug. I'm already working on it.
      //Console.WriteLine(" Testing Octree_Linked<int, double>---------");
      //Octree<int> octree_linked = new Octree_Linked<int, double>(
      //  -test - 1, -test - 1, -test - 1, // minimum dimensions of the octree
      //  test + 1, test + 1, test + 1, // maximum dimensions of the octree
      //  test + 1, // load factor
      //  (int i, out double x, out double y, out double z) => { x = i; y = i; z = i; },
      //  Program.Compare,
      //  (double left, double right) => { return (left + right) * 0.5d; });
      //for (int i = 0; i < test; i++)
      //  octree_linked.Add(i);
      //Console.Write("   Delegate: ");
      //octree_linked.Foreach((int current) => { Console.Write(current); });
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //foreach (int current in octree_linked)
      //  Console.Write(current);
      //Console.WriteLine();
      //Console.WriteLine();

      //// The octree has a minor bug. I'm already working on it.
      //Console.WriteLine(" Testing Omnitree_Linked<int, double>-------");
      //Omnitree<int, double> omnitree_linked = new Omnitree_Linked<int, double>(
      //  new double[] { -1000000 - 1, -1000000 - 1, -1000000 - 1, -1000000 - 1 }, // minimum dimensions of the omnitree
      //  new double[] { 1000000 + 1, 1000000 + 1, 1000000 + 1, 1000000 + 1 }, // maximum dimensions of the omnitree
      //  //test - 1, // load factor
      //  (int i, out double[] location) => { location = new double[] { i, i, i, i }; },
      //  Program.Compare, // comparison function
      //  (double left, double right) => { return (left + right) * 0.5d; }); // average function
      ////4); // number of dimensions

      //for (int i = -1000000; i < 1000000; i++)
      //  omnitree_linked.Add(i);

      //int j = 0;
      //Console.Write("   Delegate: ");
      //omnitree_linked.Foreach((int current) => { j++; });
      //Console.Write(j);
      //Console.WriteLine();
      //j = 0;
      //Console.Write("   Delegate (+): ");
      //omnitree_linked.Foreach((int current) => { j++; }, new double[] { -500000, -500000, -500000, -500000 }, new double[] { 500000, 500000, 500000, 500000 });
      //Console.Write(j);
      //Console.WriteLine();
      //Console.Write("   IEnumerator: ");
      //j = 0;
      //foreach (int current in omnitree_linked)
      //  j++;
      //Console.Write(j);
      //Console.WriteLine();
      //Console.WriteLine();

      // The octree has a minor bug. I'm already working on it.
      Console.WriteLine(" Testing Omnitree_Linked<int, double>-------");
      Omnitree<int, double> omnitree_linked = new Omnitree_Linked<int, double>(
        new double[] { -10 - 1, -10 - 1, -10 - 1, -10 - 1 }, // minimum dimensions of the omnitree
        new double[] { 10 + 1, 10 + 1, 10 + 1, 10 + 1 }, // maximum dimensions of the omnitree
        //test - 1, // load factor
        (int i, out double[] location) => { location = new double[] { i, i, i, i }; },
        Program.Compare, // comparison function
        (double left, double right) => { return (left + right) * 0.5d; }); // average function
      //4); // number of dimensions

      for (int i = -10; i < 10; i++)
        omnitree_linked.Add(i);

      int j = 0;
      Console.Write("   Delegate: ");
      omnitree_linked.Foreach((int current) => { 
        //j++; 
        Console.Write(current);
      });
      Console.Write(j);
      Console.WriteLine();
      j = 0;
      Console.Write("   Delegate (+): ");
      omnitree_linked.Foreach(
        //(int current) => { j++; },
        (int current) => { Console.Write(current); },
        new double[] { 0, 0, 0, 0 }, new double[] { 10, 10, 10, 10 });
      Console.Write(j);
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      j = 0;
      foreach (int current in omnitree_linked)
        //j++;
        Console.Write(current);
      //Console.Write(j);
      Console.WriteLine();
      Console.WriteLine();

      //Console.WriteLine(" SPEED TEST");
      //SpeedTest();

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
  }
}