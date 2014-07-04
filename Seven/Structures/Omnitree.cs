 // Seven
 // https://github.com/53V3N1X/SevenFramework
 // LISCENSE: See "LISCENSE.txt" in th root project directory.
 // SUPPORT: See "README.txt" in the root project directory.

using System;

namespace Seven.Structures
{
  /// <summary>The one data structure to rule them all. Made by Zachary Patten.</summary>
  public interface Omnitree<T> : Structure<T>
  {
    int Count { get; }
    bool IsEmpty { get; }
    void Add(T addition);
    //void Move(KeyType moving);
    //void Update();
  }

  /// <summary>The one data structure to rule them all. Made by Zachary Patten.</summary>
  /// <typeparam name="T">The generice type of items to be stored in this octree.</typeparam>
  [Serializable]
  public class Omnitree_Linked<T, M> : Octree<T>
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
    private Locate<T> _locate;
    private Binary<M> _average;
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
      int loadFactor,
      Locate<T> locate,
      Compare<M> compare,
      Binary<M> average,
      int dimenions)
    {
      this._loadFactor = loadFactor;
      this._top = new Leaf(min, max, null, _loadFactor);
      this._count = 0;
      this._locate = locate;
      this._average = average;
      this._compare = compare;
      this._dimensions = dimenions;
      this._children = (int)Math.Pow(2, dimenions);
    }

    /// <summary>Adds an item to the Octree.</summary>
    /// <param name="id">The id associated with the addition.</param>
    /// <param name="addition">The addition.</param>
    public void Add(T addition)
    {
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
    private Leaf Add(T addition, Node octreeNode, M[] ms)
    {
      //Console.WriteLine("Adding " + addition + " to " + octreeNode.MinX + ", " + octreeNode.MinY + ", " + octreeNode.MinZ);

      // If the node is a leaf we have reached the bottom of the tree
      if (octreeNode is Leaf)
      {
        Leaf leaf = (Leaf)octreeNode;
        if (!leaf.IsFull)
        {
          // We found a proper leaf, and the leaf has room, just add it
          leaf.Add(addition);
          return leaf;
        }
        else
        {
          // The leaf is full so we need to grow out the tree
          Branch parent = octreeNode.Parent;
          Branch growth;
          if (parent == null)
            growth = (Branch)(_top = new Branch(_top.Min, _top.Max, null, this._children));
          else
          {
            growth = GrowBranch(parent, this.DetermineChild(parent, ms));
          }
          return Add(addition, growth, ms);
        }
      }
      // We are still traversing the tree, determine the next move
      else
      {
        Branch branch = (Branch)octreeNode;
        int child = this.DetermineChild(branch, ms);
        // If the leaf is null, we must grow one before attempting to add to it
        if (branch.Children[child] == null)
          return GrowLeaf(branch, child).Add(addition);
        return Add(addition, branch.Children[child], ms);
      }
    }

    // Grows a branch on the tree at the desired location
    private Branch GrowBranch(Branch branch, int child)
    {
      // values for the new node
      M[] min, max;
      this.DetermineChildBounds(branch, child, out min, out max);
      branch.Children[child] = new Branch(min, max, branch, this._children);
      //Console.WriteLine("Growing branch " + x + ", " + y + ", " + z);
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
      //Console.WriteLine("Growing leaf " + x + ", " + y + ", " + z);
      return (Leaf)branch.Children[child];
    }

    private int DetermineChild(Node node, M[] ms)
    {
      int child = 0;
      for (int i = 0; i < this._dimensions; i++)
        if (!(_compare(ms[i], _average(node.Min[i], node.Max[i])) == Comparison.Less))
          child += (int)Math.Pow(2, i);
      return child;
    }

    private void DetermineChildBounds(Node node, int child, out M[] min, out M[] max)
    {
      min = new M[this._dimensions];
      max = new M[this._dimensions];
      for (int i = _dimensions - 1; i >= 0; i--)
      {
        int temp = (int)Math.Pow(2, i);
        if (child % temp == 0)
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
    private void Foreach(Foreach<T> function, Node octreeNode)
    {
      if (octreeNode != null)
      {
        if (octreeNode is Leaf)
        {
          int count = ((Leaf)octreeNode).Count;
          T[] items = ((Leaf)octreeNode).Contents;
          for (int i = 0; i < count; i++)
            function(items[i]);
        }
        else
        {
          Branch branch = (Branch)octreeNode;
          for (int i = 0; i < 8; i++)
            Foreach(function, branch.Children[i]);
        }
      }
    }

    public void Foreach(ForeachRef<T> function)
    {
      Foreach(function, _top);
    }
    private void Foreach(ForeachRef<T> function, Node octreeNode)
    {
      if (octreeNode != null)
      {
        if (octreeNode is Leaf)
        {
          int count = ((Leaf)octreeNode).Count;
          T[] items = ((Leaf)octreeNode).Contents;
          for (int i = 0; i < count; i++)
            function(ref items[i]);
        }
        else
        {
          Branch branch = (Branch)octreeNode;
          for (int i = 0; i < 8; i++)
            Foreach(function, branch.Children[i]);
        }
      }
    }

    public ForeachStatus Foreach(ForeachBreak<T> function)
    {
      return Foreach(function, _top);
    }
    private ForeachStatus Foreach(ForeachBreak<T> function, Node octreeNode)
    {
      if (octreeNode != null)
      {
        if (octreeNode is Leaf)
        {
          int count = ((Leaf)octreeNode).Count;
          T[] items = ((Leaf)octreeNode).Contents;
          for (int i = 0; i < count; i++)
            if (function(items[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
        else
        {
          Branch branch = (Branch)octreeNode;
          for (int i = 0; i < 8; i++)
            if (Foreach(function, branch.Children[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
      }
      return ForeachStatus.Continue;
    }

    public ForeachStatus Foreach(ForeachRefBreak<T> function)
    {
      throw new NotImplementedException();
      //return Foreach(function, _top);
    }
    private ForeachStatus Foreach(ForeachRefBreak<T> function, Node octreeNode)
    {
      if (octreeNode != null)
      {
        if (octreeNode is Leaf)
        {
          int count = ((Leaf)octreeNode).Count;
          T[] items = ((Leaf)octreeNode).Contents;
          for (int i = 0; i < count; i++)
            if (function(ref items[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
        else
        {
          Branch branch = (Branch)octreeNode;
          for (int i = 0; i < 8; i++)
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

    public void Foreach(Foreach<T> function, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      throw new NotImplementedException();
      //Foreach(function, _top, xMin, yMin, zMin, xMax, yMax, zMax);
    }
    private void Foreach(Foreach<T> function, Node node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
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
      //        function(items[i]);
      //    }
      //  }
      //  else
      //  {
      //    Branch branch = (Branch)node;
      //    for (int i = 0; i < 8; i++)
      //      Foreach(function, branch.Children[i], xMin, yMin, zMin, xMax, yMax, zMax);
      //  }
      //}
    }

    public void Foreach(ForeachRef<T> function, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      throw new NotImplementedException();
      //Foreach(function, _top, xMin, yMin, zMin, xMax, yMax, zMax);
    }
    private void Foreach(ForeachRef<T> function, Node node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
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
      //        function(ref items[i]);
      //    }
      //  }
      //  else
      //  {
      //    Branch branch = (Branch)node;
      //    for (int i = 0; i < 8; i++)
      //      Foreach(function, branch.Children[i], xMin, yMin, zMin, xMax, yMax, zMax);
      //  }
      //}
    }

    public T[] ToArray()
    {
      int finalIndex;
      T[] array = new T[_count];
      ToArray(_top, array, 0, out finalIndex);
      if (array.Length != finalIndex)
        throw new Error("There is a glitch in my octree, sorry...");
      return array;
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
