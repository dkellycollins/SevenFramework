// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using System;

namespace Seven.Structures
{
  /// <summary>The one data structure to rule them all. Made by Zachary Patten.</summary>
  public interface Omnitree<T, M> : Structure<T>
  {
    int Count { get; }
    bool IsEmpty { get; }
    void Add(T addition);
    //void Move(KeyType moving);
    //void Update();
    void Foreach(Foreach<T> function, M[] min, M[] max);
  }

  /// <summary>The one data structure to rule them all. Made by Zachary Patten.</summary>
  /// <typeparam name="T">The generice type of items to be stored in this octree.</typeparam>
  [Serializable]
  public class Omnitree_Linked<T, M> : Omnitree<T, M>
  {
    /// <summary>Represents a single node of the octree. Includes references both upwards and
    /// downwards the tree.</summary>
    private abstract class Node
    {
      private M[] _min;
      private M[] _max;
      private Branch _parent;

      internal M[] Min { get { return _min; } }
      internal M[] Max { get { return _max; } }
      internal Branch Parent { get { return _parent; } }

      internal Node(M[] min, M[] max, Branch parent)
      {
        this._min = min;
        this._max = max;
        this._parent = parent;
      }
    }

    /// <summary>Represents a single node of the octree. Includes references both upwards and
    /// downwards the tree.</summary>
    private class Leaf : Node
    {
      private T[] _contents;
      private int _count;

      internal T[] Contents { get { return _contents; } }
      internal int Count { get { return _count; } set { _count = value; } }
      internal bool IsFull { get { return _count == _contents.Length; } }

      internal Leaf(M[] min, M[] max, Branch parent, int loadFactor)
        : base(min, max, parent)
      { _contents = new T[loadFactor]; }

      internal Leaf Add(T addition)
      {
        //Console.WriteLine("Placing " + addition + ", in " + this.MinX + ", " + this.MinY + ", " + this.MinZ);
        if (_count == _contents.Length)
          throw new Error("There is a glitch in my octree, sorry...");
        _contents[_count++] = addition;
        return this;
      }
    }

    /// <summary>Represents a single node of the octree. Includes references both upwards and
    /// downwards the tree.</summary>
    private class Branch : Node
    {
      public Node[] _children;

      public Node[] Children { get { return this._children; } }

      internal bool IsEmpty
      {
        get
        {
          foreach (Node child in this._children)
            if (child != null)
              return false;
          return true;
        }
      }

      internal Branch(M[] min, M[] max, Branch parent, int children)
        : base(min, max, parent)
      {
        this._children = new Node[children];
      }
    }

    public delegate void Locate<Type>(Type item, out M[] ms);
    public delegate Type Average<Type>(Type left, Type right);

    private Locate<T> _locate;
    private Average<M> _average;
    private Compare<M> _compare;
    private int _loadFactor;
    private int _count;
    private Node _top;
    private int _dimensions;
    private int _children;

    public int Count { get { return _count; } }
    public bool IsEmpty { get { return _count == 0; } }

    public Omnitree_Linked(
      M[] min, M[] max,
      Locate<T> locate,
      Compare<M> compare,
      Average<M> average)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");
      this._loadFactor = 5;
      this._top = new Leaf(min, max, null, _loadFactor);
      this._count = 0;
      this._locate = locate;
      this._average = average;
      this._compare = compare;
      this._dimensions = min.Length;
      this._children = (int)Math.Pow(2, this._dimensions);
    }

    /// <summary>Adds an item to the Octree.</summary>
    /// <param name="id">The id associated with the addition.</param>
    /// <param name="addition">The addition.</param>
    public void Add(T addition)
    {
      //this._loadFactor = System.Math.Max(5, (int)System.Math.Log(this._count));

      M[] ms; ;
      this._locate(addition, out ms);

      if (ms.Length != this._dimensions)
        throw new Error("the location function for omnitree is invalid.");

      for (int i = 0; i < ms.Length; i++)
        if (ms[i] == null)
          throw new Error("the location function for omnitree is invalid.");

      for (int i = 0; i < this._dimensions; i++)
        if (
          _compare(ms[i], _top.Min[i]) == Comparison.Less ||
          _compare(ms[i], _top.Max[i]) == Comparison.Greater)
          throw new Error("out of bounds during addition");

      this.Add(addition, _top, ms);
      this._count++;
    }

    /// <summary>Recursively adds an item to the octree and returns the node where the addition was placed
    /// and adjusts the octree structure as needed.</summary>
    private void Add(T addition, Node node, M[] ms)
    {
      //Console.WriteLine("Adding " + addition + " to " + octreeNode.MinX + ", " + octreeNode.MinY + ", " + octreeNode.MinZ);

      // If the node is a leaf we have reached the bottom of the tree
      if (node is Leaf)
      {
        Leaf leaf = (Leaf)node;
        if (!leaf.IsFull)
        {
          // We found a proper leaf, and the leaf has room, just add it
          leaf.Add(addition);

          Console.Write("Adding " + addition + " to ");
          foreach (M m in leaf.Min) Console.Write(m + " ");
          foreach (M m in leaf.Max) Console.Write(m + " ");
          Console.WriteLine();

          return;
        }
        else
        {
          // The leaf is full so we need to grow out the tree
          Branch parent = node.Parent;
          Branch growth;
          T[] children = ((Leaf)node).Contents;
          if (parent == null)
          {
            //T[] children = ((Leaf)node).Contents;
            growth = (Branch)(_top = new Branch(_top.Min, _top.Max, null, this._children));
            //Add(addition, growth, ms);
          }
          else
          {
            growth = GrowBranch(parent, leaf.Min, leaf.Max, this.DetermineChild(parent, ms));
          }

          for (int i = 0; i < children.Length; i++)
          {
            M[] child_ms;
            this._locate(children[i], out child_ms);
            Add(children[i], growth, child_ms);

            Console.Write("Adding " + addition + " to ");
            foreach (M m in leaf.Min) Console.Write(m + " ");
            foreach (M m in leaf.Max) Console.Write(m + " ");
            Console.WriteLine();

          }

          Add(addition, growth, ms);
          return;
        }
      }
      // We are still traversing the tree, determine the next move
      else
      {
        Branch branch = (Branch)node;
        int child = this.DetermineChild(branch, ms);
        // If the leaf is null, we must grow one before attempting to add to it
        if (branch.Children[child] == null)
        {
          Leaf leaf = GrowLeaf(branch, child);
          leaf.Add(addition);

          Console.Write("Adding " + addition + " to ");
          foreach (M m in leaf.Min) Console.Write(m + " ");
          foreach (M m in leaf.Max) Console.Write(m + " ");
          Console.WriteLine();

          return;
        }
        Add(addition, branch.Children[child], ms);
        return;
      }
    }

    // Grows a branch on the tree at the desired location
    private Branch GrowBranch(Branch branch, M[] min, M[] max, int child)
    {
      // values for the new node
      branch.Children[child] = new Branch(min, max, branch, this._children);

      Console.Write("Growing branch ");
      foreach (M t in min) Console.Write(t + " ");
      foreach (M t in max) Console.Write(t + " ");

      return (Branch)branch.Children[child];
    }

    // Grows a leaf on the tree at the desired location
    private Leaf GrowLeaf(Branch branch, int child)
    {
      if (branch.Children[child] != null)
        throw new Error("My octree has a glitched, sorry.");
      // values for new node
      M[] min, max;
      this.DetermineChildBounds(branch, child, out min, out max);
      branch.Children[child] = new Leaf(min, max, branch, _loadFactor);

      Console.Write("Growing leaf ");
      foreach (M t in min) Console.Write(t + " ");
      foreach (M t in max) Console.Write(t + " ");

      return (Leaf)branch.Children[child];
    }

    private int DetermineChild(Node node, M[] ms)
    {
      int child = 0;
      for (int i = 0; i < this._dimensions; i++)
        if (!(_compare(ms[i], _average(node.Min[i], node.Max[i])) == Comparison.Less))
          child += (int)Math.Pow(2, i);

      //Console.Write("Child Check: mins");
      //foreach (M m in node.Min) Console.Write(m + " ");
      //Console.Write("maxs ");
      //foreach (M m in node.Max) Console.Write(m + " ");
      //Console.Write("loc ");
      //foreach (M m in ms) Console.Write(m + " ");
      //Console.WriteLine("is " + child);

      return child;
    }

    private void DetermineChildBounds(Node node, int child, out M[] min, out M[] max)
    {
      //int woah = child;

      min = new M[this._dimensions];
      max = new M[this._dimensions];
      for (int i = _dimensions - 1; i >= 0; i--)
      {
        //int temp = (int)Math.Pow(2, i + 1);
        int temp = (int)Math.Pow(2, i);
        if (child >= temp)
        {
          min[i] = _average(node.Min[i], node.Max[i]);
          max[i] = node.Max[i];
          child -= temp;
        }
        else
        {
          min[i] = node.Min[i];
          max[i] = _average(node.Min[i], node.Max[i]);
        }
      }

      //Console.WriteLine("Bounds: child " + woah);
      //foreach (M m in node.Min) Console.Write(" " + m);
      //foreach (M m in node.Max) Console.Write(" " + m);
      //Console.WriteLine();
      //foreach (M m in min) Console.Write(" " + m);
      //foreach (M m in max) Console.Write(" " + m);
      //Console.WriteLine();
    }

    private bool ContainsBounds(Node node, M[] min, M[] max)
    {
      if (node == null)
        return false;
      for (int i = 0; i < this._dimensions; i++)
        if (
          _compare(max[i], node.Min[i]) != Comparison.Less ||
          _compare(min[i], node.Max[i]) != Comparison.Greater)
          return true;
      return false;
    }

    private bool ContainsCoordinate(Node node, M[] ms)
    {
      if (node == null)
        return false;
      for (int i = 0; i < this._dimensions; i++)
        if (
          _compare(ms[i], node.Min[i]) != Comparison.Less ||
          _compare(ms[i], node.Max[i]) != Comparison.Greater)
          return true;
      return false;
    }

    private void PluckLeaf(Branch branch, int child)
    {
      if (!(branch.Children[child] is Leaf) || ((Leaf)branch.Children[child]).Count > 1)
        throw new Error("There is a glitch in my octree, sorry.");
      branch.Children[child] = null;
      while (branch.IsEmpty)
      {
        ChopBranch(branch.Parent, this.DetermineChild(branch.Parent, branch.Min));
        branch = branch.Parent;
      }
    }

    private void ChopBranch(Branch branch, int child)
    {
      if (branch.Children[child] == null)
        throw new Error("There is a glitch in my octree, sorry...");
      branch.Children[child] = null;
    }

    /// <summary>Iterates through the entire tree and ensures each item is in the proper node.</summary>
    public void Update()
    {
      throw new NotImplementedException("Sorry, I'm still working on the update function.");
    }

    public void Foreach(Foreach<T> function)
    {
      Foreach(function, _top);
    }
    private void Foreach(Foreach<T> function, Node node)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
            function(items[i]);
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < branch.Children.Length; i++)
            Foreach(function, branch.Children[i]);
        }
      }
    }

    public void Foreach(ForeachRef<T> function)
    {
      Foreach(function, _top);
    }
    private void Foreach(ForeachRef<T> function, Node node)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
            function(ref items[i]);
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < branch.Children.Length; i++)
            Foreach(function, branch.Children[i]);
        }
      }
    }

    public ForeachStatus Foreach(ForeachBreak<T> function)
    {
      return Foreach(function, _top);
    }
    private ForeachStatus Foreach(ForeachBreak<T> function, Node node)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
            if (function(items[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < branch.Children.Length; i++)
            if (Foreach(function, branch.Children[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
      }
      return ForeachStatus.Continue;
    }

    public ForeachStatus Foreach(ForeachRefBreak<T> function)
    {
      return Foreach(function, _top);
    }
    private ForeachStatus Foreach(ForeachRefBreak<T> function, Node node)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
            if (function(ref items[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < branch.Children.Length; i++)
            if (Foreach(function, branch.Children[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Performs a functional paradigm traversal of the octree with data structure optimization.</summary>
    /// <param name="function">The function to perform per iteration.</param>
    /// <param name="xMin">The minimum x of a rectangular prism to query the octree.</param>
    /// <param name="yMin">The minimum y of a rectangular prism to query the octree.</param>
    /// <param name="zMin">The minimum z of a rectangular prism to query the octree.</param>
    /// <param name="xMax">The maximum x of a rectangular prism to query the octree.</param>
    /// <param name="yMax">The maximum y of a rectangular prism to query the octree.</param>
    /// <param name="zMax">The maximum z of a rectangular prism to query the octree.</param>
    public ForeachStatus Foreach(ForeachBreak<T> function, M[] min, M[] max)
    {
      throw new NotImplementedException();
      //return Foreach(function, _top, min, max);
    }
    private ForeachStatus Foreach(ForeachBreak<T> function, Node node, M[] min, M[] max)
    {
      throw new NotImplementedException();
      //if (node != null)
      //{
      //  if (node is Leaf)
      //  {
      //    int count = ((Leaf)node).Count;
      //    T[] items = ((Leaf)node).Contents;

      //    for (int i = 0; i < count; i++)
      //    {
      //      for (int j = 0; j < _dimensions; j++)
      //        if (
      //          _compare(max[j], node.Min[j]) != Comparison.Less ||
      //          _compare(min[j], node.Max[j]) != Comparison.Greater)
      //          goto Continue;

      //      if (function(items[i]) == ForeachStatus.Break)
      //        return ForeachStatus.Break;

      //    Continue:

      //      continue;
      //    }
      //  }
      //  else
      //  {
      //    Branch branch = (Branch)node;
      //    for (int i = 0; i < 8; i++)
      //      if (Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
      //        return ForeachStatus.Break;
      //    return ForeachStatus.Continue;
      //  }
      //}
      //return ForeachStatus.Continue;
    }

    /// <summary>Performs a functional paradigm traversal of the octree with data structure optimization.</summary>
    /// <param name="function">The function to perform per iteration.</param>
    /// <param name="xMin">The minimum x of a rectangular prism to query the octree.</param>
    /// <param name="yMin">The minimum y of a rectangular prism to query the octree.</param>
    /// <param name="zMin">The minimum z of a rectangular prism to query the octree.</param>
    /// <param name="xMax">The maximum x of a rectangular prism to query the octree.</param>
    /// <param name="yMax">The maximum y of a rectangular prism to query the octree.</param>
    /// <param name="zMax">The maximum z of a rectangular prism to query the octree.</param>
    public ForeachStatus Foreach(ForeachRefBreak<T> function, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      throw new NotImplementedException();
      //return Foreach(function, _top, xMin, yMin, zMin, xMax, yMax, zMax);
    }
    private ForeachStatus Foreach(ForeachRefBreak<T> function, Node node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      throw new NotImplementedException();
      //if (node != null)
      //{
      //  if (node is Leaf)
      //  {
      //    int count = ((Leaf)node).Count;
      //    T[] items = ((Leaf)node).Contents;
      //    for (int i = 0; i < count; i++)
      //    {
      //      M x, y, z;
      //      this._locate(items[i], out x, out y, out z);
      //      if (
      //        _compare(x, node.MinX) == Comparison.Less &&
      //        _compare(y, node.MinY) == Comparison.Less &&
      //        _compare(z, node.MinZ) == Comparison.Less &&
      //        _compare(x, node.MaxX) == Comparison.Greater &&
      //        _compare(y, node.MaxY) == Comparison.Greater &&
      //        _compare(z, node.MaxZ) == Comparison.Greater)
      //        if (function(ref items[i]) == ForeachStatus.Break)
      //          return ForeachStatus.Break;
      //    }
      //  }
      //  else
      //  {
      //    Branch branch = (Branch)node;
      //    for (int i = 0; i < 8; i++)
      //      if (Foreach(function, branch.Children[i], xMin, yMin, zMin, xMax, yMax, zMax) == ForeachStatus.Break)
      //        return ForeachStatus.Break;
      //    return ForeachStatus.Continue;
      //  }
      //}
      //return ForeachStatus.Continue;
    }

    public void Foreach(Foreach<T> function, M[] min, M[] max)
    {
      Foreach(function, _top, min, max);
    }
    private void Foreach(Foreach<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        for (int j = 0; j < this._dimensions; j++)
          if (this._compare(node.Max[j], min[j]) == Comparison.Less ||
            this._compare(node.Min[j], max[j]) == Comparison.Greater)
            return;

        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms;
            this._locate(items[i], out ms);
            for (int j = 0; j < this._dimensions; j++)
              if (this._compare(ms[j], min[j]) == Comparison.Less ||
                this._compare(ms[j], max[j]) == Comparison.Greater)
                goto Continue;

            function(items[i]);

          Continue:

            continue;
          }
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < branch.Children.Length; i++)
          {
            //for (int j = 0; j < this._dimensions; j++)
            //  if (this._compare(branch.Children[i].Max[j], min[j]) == Comparison.Less ||
            //    this._compare(branch.Children[i].Min[j], max[j]) == Comparison.Greater)
            //    goto Continue;

            Foreach(function, branch.Children[i], min, max);

            //Continue:

            //  continue;
          }
        }
      }
    }

    public void Foreach(ForeachRef<T> function, M[] min, M[] max)
    {
      Foreach(function, _top, min, max);
    }
    private void Foreach(ForeachRef<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        for (int j = 0; j < this._dimensions; j++)
          if (this._compare(node.Max[j], min[j]) == Comparison.Less ||
            this._compare(node.Min[j], max[j]) == Comparison.Greater)
            return;

        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms;
            this._locate(items[i], out ms);
            for (int j = 0; j < this._dimensions; j++)
              if (this._compare(ms[j], min[j]) == Comparison.Less ||
                this._compare(ms[j], max[j]) == Comparison.Greater)
                goto Continue;

            function(ref items[i]);

          Continue:

            continue;
          }
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < branch.Children.Length; i++)
          {
            //for (int j = 0; j < this._dimensions; j++)
            //  if (this._compare(branch.Children[i].Max[j], min[j]) == Comparison.Less ||
            //    this._compare(branch.Children[i].Min[j], max[j]) == Comparison.Greater)
            //    goto Continue;

            Foreach(function, branch.Children[i], min, max);

            //Continue:

            //  continue;
          }
        }
      }
    }

    public T[] ToArray()
    {
      int index = 0;
      T[] array = new T[this._count];
      this.Foreach((T entry) => { array[index++] = entry; });
      return array;

      //int finalIndex;
      //T[] array = new T[_count];
      //ToArray(_top, array, 0, out finalIndex);
      //if (array.Length != finalIndex)
      //  throw new Error("There is a glitch in my octree, sorry...");
      //return array;
    }
    private void ToArray(Node octreeNode, T[] array, int entryIndex, out int returnIndex)
    {
      if (octreeNode != null)
      {
        if (octreeNode is Leaf)
        {
          returnIndex = entryIndex;
          foreach (T item in ((Leaf)octreeNode).Contents)
            array[returnIndex++] = item;
        }
        else
        {
          // The current node is a branch
          Branch branch = (Branch)octreeNode;
          for (int i = 0; i < 8; i++)
            ToArray(branch.Children[i], array, entryIndex, out entryIndex);
          returnIndex = entryIndex;
        }
      }
      else
        returnIndex = entryIndex;
    }

    #region Structure<Type>

    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      T[] array = this.ToArray();
      return array.GetEnumerator();
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.Generic.IEnumerator<T>
      System.Collections.Generic.IEnumerable<T>.GetEnumerator()
    {
      T[] array = this.ToArray();
      return ((System.Collections.Generic.IEnumerable<T>)array).GetEnumerator();
    }

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { throw new NotImplementedException(); } }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<T> Clone()
    {
      throw new NotImplementedException();
    }

    #endregion

    /// <summary>This is used for throwing OcTree exceptions only to make debugging faster.</summary>
    private class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}
