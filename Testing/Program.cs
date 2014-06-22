using System;

using Seven;
using Seven.Structures;
using System.Diagnostics;

using System.Linq;

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

    public static double Prioritize(int item)
    {
      return -item;
    }

    static void Main(string[] args)
    {
      int test = 10;

      Console.WriteLine("Testing Link");
      Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
      Console.Write("Delegate: ");
      link.Foreach((dynamic current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (object value in link)
        Console.Write(value);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing Array_Array<Type>");
      Array<int> array = new Array_Array<int>(10);
      for (int i = 0; i < test; i++)
        array[i] = i;
      Console.Write("Delegate: ");
      array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing List_Array<Type>");
      List<int> list_array = new List_Array<int>(10);
      for (int i = 0; i < test; i++)
        list_array.Add(i);
      Console.Write("Delegate: ");
      list_array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in list_array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing List_Linked<Type>");
      List<int> list_linked = new List_Linked<int>();
      for (int i = 0; i < test; i++)
        list_linked.Add(i);
      Console.Write("Delegate: ");
      list_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in list_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();
      
      Console.WriteLine("Testing Stack_Linked<Type>");
      Stack<int> stack_linked = new Stack_Linked<int>();
      for (int i = 0; i < test; i++)
        stack_linked.Push(i);
      Console.Write("Delegate: ");
      stack_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in stack_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing Queue_Linked<Type>");
      Queue<int> queue_linked = new Queue_Linked<int>();
      for (int i = 0; i < test; i++)
        queue_linked.Enqueue(i);
      Console.Write("Delegate: ");
      queue_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in queue_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing Heap_Array<Type>");
      Heap<int> heap_array = new Heap_Array<int>(Compare);
      for (int i = 0; i < test; i++)
        heap_array.Enqueue(i);
      Console.Write("Delegate: ");
      heap_array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in heap_array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing AvlTree_Linked<Type>");
      AvlTree<int> avlTree_linked = new AvlTree_Linked<int>(Compare);
      for (int i = 0; i < test; i++)
        avlTree_linked.Add(i);
      Console.Write("Delegate: ");
      avlTree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in avlTree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing RedBlack_Linked<Type>");
      RedBlackTree<int> redBlackTree_linked = new RedBlackTreeLinked<int>(Compare);
      for (int i = 0; i < test; i++)
        redBlackTree_linked.Add(i);
      Console.Write("Delegate: ");
      redBlackTree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in redBlackTree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      //Console.WriteLine("SPEED TEST");
      SpeedTest();

      Console.WriteLine("Testing Complete...");
      Console.ReadLine();
    }

    public static void SpeedTest()
    {
      int test = 10000000;

      Stopwatch timer = new Stopwatch();

      Console.WriteLine("Speed Testing List:");
      Console.WriteLine("BE SURE TO RUN THE EXE OUTSIDE VISUAL STUDIO FOR FAIR TESTING");

      timer.Restart();
      System.Collections.Generic.List<int> list_net =
        new System.Collections.Generic.List<int>(test);
      for (int i = 0; i < test; i++)
        list_net.Add(i);
      System.Collections.Generic.List<int> list_net2 =
        new System.Collections.Generic.List<int>(test);
      list_net.ForEach((int current) => { list_net2.Add(current); });
      timer.Stop();
      Console.WriteLine("System.Collections.Generic.List<Type>: " + (float)timer.ElapsedMilliseconds / 1000.0f);

      timer.Restart();
      List<int> list_array = new List_Array<int>(test);
      for (int i = 0; i < test; i++)
        list_array.Add(i);
      List<int> list_array2 = new List_Array<int>(test);
      list_array.Foreach((int current) => { list_array2.Add(current); });
      timer.Stop();
      Console.WriteLine("Seven.Structures.List_Array<Type>: " + (float)timer.ElapsedMilliseconds / 1000.0f);

      timer.Restart();
      System.Collections.Generic.LinkedList<int> linkedlist_net =
        new System.Collections.Generic.LinkedList<int>();
      for (int i = 0; i < test; i++)
        linkedlist_net.AddLast(i);
      System.Collections.Generic.LinkedList<int> linkedlist_net2 =
        new System.Collections.Generic.LinkedList<int>();
      foreach (int current in linkedlist_net)
        linkedlist_net2.AddLast(current);
      timer.Stop();
      Console.WriteLine("System.Collections.Generic.LinkedList<Type>: " + (float)timer.ElapsedMilliseconds / 1000.0f);

      timer.Restart();
      List<int> list_linked = new List_Linked<int>();
      for (int i = 0; i < test; i++)
        list_linked.Add(i);
      List<int> list_linked2 = new List_Linked<int>();
      list_linked.Foreach((int current) => { list_linked2.Add(current); });
      timer.Stop();
      Console.WriteLine("Seven.Structures.List_Linked<Type>: " + (float)timer.ElapsedMilliseconds / 1000.0f);
    }
  }
}
