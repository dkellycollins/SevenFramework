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

    static void Main(string[] args)
    {
      int test = 10;

      Console.WriteLine("Functionality Testing=======================");
      Console.WriteLine();
      Console.WriteLine("items to store (int): 0 - " + test);
      Console.WriteLine();

      Console.WriteLine(" Testing Link--------------------------------");
      Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
      Console.Write("   Delegate: ");
      link.Foreach((dynamic current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (object value in link)
        Console.Write(value);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing Array_Array<Type>-------------------");
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

      Console.WriteLine(" Testing List_Array<Type>--------------------");
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

      Console.WriteLine(" Testing List_Linked<Type>-------------------");
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

      Console.WriteLine(" Testing Stack_Linked<Type>------------------");
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

      Console.WriteLine(" Testing Queue_Linked<Type>------------------");
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

      Console.WriteLine(" Testing Heap_Array<Type>--------------------");
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

      Console.WriteLine(" Testing AvlTree_Linked<Type>----------------");
      AvlTree<int> avlTree_linked = new AvlTree_Linked<int>(Compare);
      for (int i = 0; i < test; i++)
        avlTree_linked.Add(i);
      Console.Write("   Delegate: ");
      avlTree_linked .Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("   IEnumerator: ");
      foreach (int current in avlTree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine(" Testing RedBlack_Linked<Type>---------------");
      RedBlackTree<int> redBlackTree_linked = new RedBlackTreeLinked<int>(Compare);
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

      // I just introduced a bug in my Octree... Ill fix soon
      //Console.WriteLine(" Testing Octree_Linked<Type>----------------");
      //Octree<int> octree_linked = new Octree_Linked<int>(0, 0, 0, test + 1, 3,
      //  (int i, out double x, out double y, out double z) => { x = i; y = i; z = i; });
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
