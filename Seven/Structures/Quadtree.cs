 // Seven
 // https://github.com/53V3N1X/SevenFramework
 // LISCENSE: See "LISCENSE.txt" in th root project directory.
 // SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Structures
{
  public interface Quadtree<T> : Structure<T>
  {
    int Count { get; }
    bool IsEmpty { get; }
    void Add(T addition);
    //void Move(KeyType moving);
    //void Update();
  }

  /// <summary>Axis-Aligned rectangular prism generic octree for storing items along three axis.</summary>
  /// <typeparam name="T">The generice type of items to be stored in this octree.</typeparam>
  [System.Serializable]
  public class Quadtree_Linked<T, M> : Quadtree<T>
  {
    /// <summary>Represents a single node of the octree. Includes references both upwards and
    /// downwards the tree.</summary>
    private abstract class Node
    {
      private M _minX;
      private M _minY;
      private M _maxX;
      private M _maxY;
      private Branch _parent;

      internal M MinX { get { return _minX; } }
      internal M MinY { get { return _minY; } }
      internal M MaxX { get { return _maxX; } }
      internal M MaxY { get { return _maxY; } }
      internal Branch Parent { get { return _parent; } }

      internal Node(M minX, M minY, M maxX, M maxY, Branch parent)
      {
        _minX = minX;
        _minY = minY;
        _maxX = maxX;
        _maxY = maxY;
        _parent = parent;
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

      internal Leaf(M minX, M minY, M maxX, M maxY, Branch parent, int loadFactor)
        : base(minX, minY, maxX, maxY, parent)
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

      internal Branch(M minX, M minY, M maxX, M maxY, Branch parent)
        : base(minX, minY, maxX, maxY, parent)
      {
        this._children = new Node[8];
      }
    }

    public delegate void Locate<Type>(Type item, out M x, out M y);
    private Locate<T> _locate;
    public Binary<M> _average;
    public Compare<M> _compare;
    private int _loadFactor;
    private int _count;
    private Node _top;

    public int Count { get { return _count; } }
    public bool IsEmpty { get { return _count == 0; } }

    public Quadtree_Linked(
      M minX, M minY,
      M maxX, M maxY,
      int loadFactor,
      Locate<T> locate,
      Compare<M> compare,
      Binary<M> average)
    {
      this._loadFactor = loadFactor;
      this._top = new Leaf(minX, minY, maxX, maxY, null, _loadFactor);
      this._count = 0;
      this._locate = locate;
      this._average = average;
      this._compare = compare;
    }

    /// <summary>Adds an item to the Octree.</summary>
    /// <param name="id">The id associated with the addition.</param>
    /// <param name="addition">The addition.</param>
    public void Add(T addition)
    {
      M x, y;
      this._locate(addition, out x, out y);

      if (
        _compare(x, _top.MinX) == Comparison.Less ||
        _compare(y, _top.MinY) == Comparison.Less ||
        _compare(x, _top.MaxX) == Comparison.Greater ||
        _compare(y, _top.MaxY) == Comparison.Greater)
        throw new Error("out of bounds during addition");

      this.Add(addition, _top, x, y);
      this._count++;
    }

    /// <summary>Recursively adds an item to the octree and returns the node where the addition was placed
    /// and adjusts the octree structure as needed.</summary>
    private Leaf Add(T addition, Node octreeNode, M x, M y)
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
            growth = (Branch)(_top = new Branch(_top.MinX, _top.MinY, _top.MaxX, _top.MaxY, null));
          else
            growth = GrowBranch(parent, this.DetermineChild(parent, x, y));
          return Add(addition, growth, x, y);
        }
      }
      // We are still traversing the tree, determine the next move
      else
      {
        Branch branch = (Branch)octreeNode;
        int child = this.DetermineChild(branch, x, y);
        // If the leaf is null, we must grow one before attempting to add to it
        if (branch.Children[child] == null)
          return GrowLeaf(branch, child).Add(addition);
        return Add(addition, branch.Children[child], x, y);
      }
    }

    // Grows a branch on the tree at the desired location
    private Branch GrowBranch(Branch branch, int child)
    {
      // values for the new node
      M minX, minY, maxX, maxY;
      this.DetermineChildBounds(branch, child, out minX, out minY, out maxX, out maxY);
      branch.Children[child] = new Branch(minX, minY, maxX, maxY, branch);
      //Console.WriteLine("Growing branch " + x + ", " + y + ", " + z);
      return (Branch)branch.Children[child];
    }

    // Grows a leaf on the tree at the desired location
    private Leaf GrowLeaf(Branch branch, int child)
    {
      if (branch.Children[child] != null)
        throw new Error("My octree has a glitched, sorry.");
      // values for new node
      M minX, minY, minZ, maxX, maxY, maxZ;
      this.DetermineChildBounds(branch, child, out minX, out minY, out maxX, out maxY);
      branch.Children[child] = new Leaf(minX, minY, maxX, maxY, branch, _loadFactor);
      //Console.WriteLine("Growing leaf " + x + ", " + y + ", " + z);
      return (Leaf)branch.Children[child];
    }

    /// <summary>
    /// Determines relative child index.
    /// 0: (-x, -y)
    /// 1: (-x, +y)
    /// 2: (+x, -y)
    /// 3: (+x, +y)
    /// </summary>
    private int DetermineChild(Node node, M x, M y)
    {
      Comparison x_comp = _compare(x, _average(node.MinX, node.MaxX));
      Comparison y_comp = _compare(y, _average(node.MinY, node.MaxY));

      if (y_comp == Comparison.Less)
        if (x_comp == Comparison.Less)
          return 0;
        else // (x >= node.X)
          return 2;
      else // (y >= node.Y)
        if (x_comp == Comparison.Less)
          return 1;
        else // (x >= node.X)
          return 3;
    }

    /// <summary>
    /// Determins the bounds of a child node.
    /// 0: (-x, -y)
    /// 1: (-x, +y)
    /// 2: (+x, -y)
    /// 3: (+x, +y)
    /// </summary>
    private void DetermineChildBounds(Node node, int child,
      out M minX, out M minY, out M maxX, out M maxY)
    {
      switch (child)
      {
        case 0: // 0: (-x, -y)
          minX = node.MinX;
          minY = node.MinY;
          maxX = _average(node.MinX, node.MaxX);
          maxY = _average(node.MinY, node.MaxY);
          return;
        case 1: // 1: (-x, +y)
          minX = node.MinX;
          minY = _average(node.MinY, node.MaxY);
          maxX = _average(node.MinX, node.MaxX);
          maxY = node.MaxY;
          return;
        case 2: // 2: (+x, -y)
          minX = _average(node.MinX, node.MaxX);
          minY = node.MinY;
          maxX = node.MaxX;
          maxY = _average(node.MinY, node.MaxY);
          return;
        case 3: // 3: (+x, +y)
          minX = _average(node.MinX, node.MaxX);
          minY = _average(node.MinY, node.MaxY);
          maxX = node.MaxX;
          maxY = node.MaxY;
          return;
        default:
          throw new Error("There is a glitch in my octree, sorry...");
      }
    }

    private bool ContainsBounds(Node node, M xMin, M yMin, M zMin, M xMax, M yMax, M zMax)
    {
      return !(node == null || (
        _compare(xMax, node.MinX) == Comparison.Less &&
        _compare(yMax, node.MinY) == Comparison.Less &&
        _compare(xMin, node.MaxX) == Comparison.Greater &&
        _compare(yMin, node.MaxY) == Comparison.Greater));
    }

    private bool ContainsCoordinate(Node node, M x, M y, M z)
    {
      return !(node == null || (
        _compare(x, node.MinX) == Comparison.Less &&
        _compare(y, node.MinY) == Comparison.Less &&
        _compare(x, node.MaxX) == Comparison.Greater &&
        _compare(y, node.MaxY) == Comparison.Greater));
    }

    private void PluckLeaf(Branch branch, int child)
    {
      if (!(branch.Children[child] is Leaf) || ((Leaf)branch.Children[child]).Count > 1)
        throw new Error("There is a glitch in my octree, sorry.");
      branch.Children[child] = null;
      while (branch.IsEmpty)
      {
        ChopBranch(branch.Parent, this.DetermineChild(branch.Parent, branch.MinX, branch.MinY));
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
      throw new System.NotImplementedException("Sorry, I'm still working on the update function.");
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
      return Foreach(function, _top);
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
    public ForeachStatus Foreach(ForeachBreak<T> function, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      return Foreach(function, _top, xMin, yMin, zMin, xMax, yMax, zMax);
    }
    private ForeachStatus Foreach(ForeachBreak<T> function, Node node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M x, y;
            this._locate(items[i], out x, out y);
            if (
              _compare(x, node.MinX) == Comparison.Less &&
              _compare(y, node.MinY) == Comparison.Less &&
              _compare(x, node.MaxX) == Comparison.Greater &&
              _compare(y, node.MaxY) == Comparison.Greater)
              if (function(items[i]) == ForeachStatus.Break)
                return ForeachStatus.Break;
          }
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < 8; i++)
            if (Foreach(function, branch.Children[i], xMin, yMin, zMin, xMax, yMax, zMax) == ForeachStatus.Break)
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
    public ForeachStatus Foreach(ForeachRefBreak<T> function, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      return Foreach(function, _top, xMin, yMin, zMin, xMax, yMax, zMax);
    }
    private ForeachStatus Foreach(ForeachRefBreak<T> function, Node node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M x, y;
            this._locate(items[i], out x, out y);
            if (
              _compare(x, node.MinX) == Comparison.Less &&
              _compare(y, node.MinY) == Comparison.Less &&
              _compare(x, node.MaxX) == Comparison.Greater &&
              _compare(y, node.MaxY) == Comparison.Greater)
              if (function(ref items[i]) == ForeachStatus.Break)
                return ForeachStatus.Break;
          }
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < 8; i++)
            if (Foreach(function, branch.Children[i], xMin, yMin, zMin, xMax, yMax, zMax) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
      }
      return ForeachStatus.Continue;
    }

    public void Foreach(Foreach<T> function, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      Foreach(function, _top, xMin, yMin, zMin, xMax, yMax, zMax);
    }
    private void Foreach(Foreach<T> function, Node node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M x, y, z;
            this._locate(items[i], out x, out y);
            if (
              _compare(x, node.MinX) == Comparison.Less &&
              _compare(y, node.MinY) == Comparison.Less &&
              _compare(x, node.MaxX) == Comparison.Greater &&
              _compare(y, node.MaxY) == Comparison.Greater)
              function(items[i]);
          }
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < 8; i++)
            Foreach(function, branch.Children[i], xMin, yMin, zMin, xMax, yMax, zMax);
        }
      }
    }

    public void Foreach(ForeachRef<T> function, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      Foreach(function, _top, xMin, yMin, zMin, xMax, yMax, zMax);
    }
    private void Foreach(ForeachRef<T> function, Node node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M x, y;
            this._locate(items[i], out x, out y);
            if (
              _compare(x, node.MinX) == Comparison.Less &&
              _compare(y, node.MinY) == Comparison.Less &&
              _compare(x, node.MaxX) == Comparison.Greater &&
              _compare(y, node.MaxY) == Comparison.Greater)
              function(ref items[i]);
          }
        }
        else
        {
          Branch branch = (Branch)node;
          for (int i = 0; i < 8; i++)
            Foreach(function, branch.Children[i], xMin, yMin, zMin, xMax, yMax, zMax);
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
    public int SizeOf { get { throw new System.NotImplementedException(); } }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<T> Clone()
    {
      throw new System.NotImplementedException();
    }

    #endregion

    /// <summary>This is used for throwing OcTree exceptions only to make debugging faster.</summary>
    private class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}
