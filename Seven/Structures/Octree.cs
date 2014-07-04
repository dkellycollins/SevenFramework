// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Structures
{
  public interface Octree<T> : Structure<T>
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
  public class Octree_Linked<T, M> : Octree<T>
  {
    /// <summary>Represents a single node of the octree. Includes references both upwards and
    /// downwards the tree.</summary>
    private abstract class Node
    {
      private M _minX;
      private M _minY;
      private M _minZ;
      private M _maxX;
      private M _maxY;
      private M _maxZ;
      private Branch _parent;

      internal M MinX { get { return _minX; } }
      internal M MinY { get { return _minY; } }
      internal M MinZ { get { return _minZ; } }
      internal M MaxX { get { return _maxX; } }
      internal M MaxY { get { return _maxY; } }
      internal M MaxZ { get { return _maxZ; } }
      internal Branch Parent { get { return _parent; } }

      internal Node(M minX, M minY, M minZ, M maxX, M maxY, M maxZ, Branch parent)
      {
        _minX = minX;
        _minY = minY;
        _minZ = minZ;
        _maxX = maxX;
        _maxY = maxY;
        _maxZ = maxZ;
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

      internal Leaf(M minX, M minY, M minZ, M maxX, M maxY, M maxZ, Branch parent, int loadFactor)
        : base(minX, minY, minZ, maxX, maxY, maxZ, parent)
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

      internal Branch(M minX, M minY, M minZ, M maxX, M maxY, M maxZ, Branch parent)
        : base(minX, minY, minZ, maxX, maxY, maxZ, parent)
      {
        this._children = new Node[8];
      }
    }

    public delegate void Locate<Type>(Type item, out M x, out M y, out M z);
    private Locate<T> _locate;
    public Binary<M> _average;
    public Compare<M> _compare;
    private int _loadFactor;
    private int _count;
    private Node _top;

    public int Count { get { return _count; } }
    public bool IsEmpty { get { return _count == 0; } }

    public Octree_Linked(
      M minX, M minY, M minZ,
      M maxX, M maxY, M maxZ,
      int loadFactor,
      Locate<T> locate,
      Compare<M> compare,
      Binary<M> average)
    {
      this._loadFactor = loadFactor;
      this._top = new Leaf(minX, minY, minZ, maxX, maxY, maxZ, null, _loadFactor);
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
      M x, y, z;
      this._locate(addition, out x, out y, out z);

      if (
        _compare(x, _top.MinX) == Comparison.Less ||
        _compare(y, _top.MinY) == Comparison.Less ||
        _compare(z, _top.MinZ) == Comparison.Less ||
        _compare(x, _top.MaxX) == Comparison.Greater ||
        _compare(y, _top.MaxY) == Comparison.Greater ||
        _compare(z, _top.MaxZ) == Comparison.Greater)
        throw new Error("out of bounds during addition");

      this.Add(addition, _top, x, y, z);
      this._count++;
    }

    /// <summary>Recursively adds an item to the octree and returns the node where the addition was placed
    /// and adjusts the octree structure as needed.</summary>
    private Leaf Add(T addition, Node octreeNode, M x, M y, M z)
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
            growth = (Branch)(_top = new Branch(_top.MinX, _top.MinY, _top.MinZ, _top.MaxX, _top.MaxY, _top.MaxZ, null));
          else
            growth = GrowBranch(parent, this.DetermineChild(parent, x, y, z));
          return Add(addition, growth, x, y, z);
        }
      }
      // We are still traversing the tree, determine the next move
      else
      {
        Branch branch = (Branch)octreeNode;
        int child = this.DetermineChild(branch, x, y, z);
        // If the leaf is null, we must grow one before attempting to add to it
        if (branch.Children[child] == null)
          return GrowLeaf(branch, child).Add(addition);
        return Add(addition, branch.Children[child], x, y, z);
      }
    }

    // Grows a branch on the tree at the desired location
    private Branch GrowBranch(Branch branch, int child)
    {
      // values for the new node
      M minX, minY, minZ, maxX, maxY, maxZ;
      this.DetermineChildBounds(branch, child, out minX, out minY, out minZ, out maxX, out maxY, out maxZ);
      branch.Children[child] = new Branch(minX, minY, minZ, maxX, maxY, maxZ, branch);
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
      this.DetermineChildBounds(branch, child, out minX, out minY, out minZ, out maxX, out maxY, out maxZ);
      branch.Children[child] = new Leaf(minX, minY, minZ, maxX, maxY, maxZ, branch, _loadFactor);
      //Console.WriteLine("Growing leaf " + x + ", " + y + ", " + z);
      return (Leaf)branch.Children[child];
    }

    /// <summary>
    /// Determines relative child index.
    /// 0: (-x, -y, -z)
    /// 1: (-x, -y, +z)
    /// 2: (-x, +y, -z)
    /// 3: (-x, +y, +z)
    /// 4: (+x, -y, -z)
    /// 5: (+x, -y, +z)
    /// 6: (+x, +y, -z)
    /// 7: (+x, +y, +z)
    /// </summary>
    private int DetermineChild(Node node, M x, M y, M z)
    {
      Comparison x_comp = _compare(x, _average(node.MinX, node.MaxX));
      Comparison y_comp = _compare(y, _average(node.MinY, node.MaxY));
      Comparison z_comp = _compare(z, _average(node.MinZ, node.MaxZ));

      if (z_comp == Comparison.Less)
        if (y_comp == Comparison.Less)
          if (x_comp == Comparison.Less)
            return 0;
          else // (x >= node.X)
            return 4;
        else // (y >= node.Y)
          if (x_comp == Comparison.Less)
            return 2;
          else // (x >= node.X)
            return 6;
      else // (z >= node.Z)
        if (y_comp == Comparison.Less)
          if (x_comp == Comparison.Less)
            return 1;
          else // (x >= node.X)
            return 5;
        else // (y >= node.Y)
          if (x_comp == Comparison.Less)
            return 3;
          else  // (x >= node.X)
            return 7;
    }

    /// <summary>
    /// Determins the bounds of a child node.
    /// 0: (-x, -y, -z)
    /// 1: (-x, -y, +z)
    /// 2: (-x, +y, -z)
    /// 3: (-x, +y, +z)
    /// 4: (+x, -y, -z)
    /// 5: (+x, -y, +z)
    /// 6: (+x, +y, -z)
    /// 7: (+x, +y, +z)
    /// </summary>
    private void DetermineChildBounds(Node node, int child,
      out M minX, out M minY, out M minZ, out M maxX, out M maxY, out M maxZ)
    {
      switch (child)
      {
        case 0: // 0: (-x, -y, -z)
          minX = node.MinX;
          minY = node.MinY;
          minZ = node.MinZ;
          maxX = _average(node.MinX, node.MaxX);
          maxY = _average(node.MinY, node.MaxY);
          maxZ = _average(node.MinZ, node.MaxZ);
          return;
        case 1: // 1: (-x, -y, +z)
          minX = node.MinX;
          minY = node.MinY;
          minZ = _average(node.MinZ, node.MaxZ);
          maxX = _average(node.MinX, node.MaxX);
          maxY = _average(node.MinY, node.MaxY);
          maxZ = node.MaxZ;
          return;
        case 2: // 2: (-x, +y, -z)
          minX = node.MinX;
          minY = _average(node.MinY, node.MaxY);
          minZ = node.MinZ;
          maxX = _average(node.MinX, node.MaxX);
          maxY = node.MaxY;
          maxZ = _average(node.MinZ, node.MaxZ);
          return;
        case 3: // 3: (-x, +y, +z)
          minX = node.MinX;
          minY = _average(node.MinY, node.MaxY);
          minZ = _average(node.MinZ, node.MaxZ);
          maxX = _average(node.MinX, node.MaxX);
          maxY = node.MaxY;
          maxZ = node.MaxZ;
          return;
        case 4: // 4: (+x, -y, -z)
          minX = _average(node.MinX, node.MaxX);
          minY = node.MinY;
          minZ = node.MinZ;
          maxX = node.MaxX;
          maxY = _average(node.MinY, node.MaxY);
          maxZ = _average(node.MinZ, node.MaxZ);
          return;
        case 5: // 5: (+x, -y, +z)
          minX = _average(node.MinX, node.MaxX);
          minY = node.MinY;
          minZ = _average(node.MinZ, node.MaxZ);
          maxX = node.MaxX;
          maxY = _average(node.MinY, node.MaxY);
          maxZ = node.MaxZ;
          return;
        case 6: // 6: (+x, +y, -z)
          minX = _average(node.MinX, node.MaxX);
          minY = _average(node.MinY, node.MaxY);
          minZ = node.MinZ;
          maxX = node.MaxX;
          maxY = node.MaxY;
          maxZ = _average(node.MinZ, node.MaxZ);
          return;
        case 7: // 7: (+x, +y, +z)
          minX = _average(node.MinX, node.MaxX);
          minY = _average(node.MinY, node.MaxY);
          minZ = _average(node.MinZ, node.MaxZ);
          maxX = node.MaxX;
          maxY = node.MaxY;
          maxZ = node.MaxZ;
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
        _compare(zMax, node.MinZ) == Comparison.Less &&
        _compare(xMin, node.MaxX) == Comparison.Greater &&
        _compare(yMin, node.MaxY) == Comparison.Greater &&
        _compare(zMin, node.MaxZ) == Comparison.Greater));
    }

    private bool ContainsCoordinate(Node node, M x, M y, M z)
    {
      return !(node == null || (
        _compare(x, node.MinX) == Comparison.Less &&
        _compare(y, node.MinY) == Comparison.Less &&
        _compare(z, node.MinZ) == Comparison.Less &&
        _compare(x, node.MaxX) == Comparison.Greater &&
        _compare(y, node.MaxY) == Comparison.Greater &&
        _compare(z, node.MaxZ) == Comparison.Greater));
    }

    private void PluckLeaf(Branch branch, int child)
    {
      if (!(branch.Children[child] is Leaf) || ((Leaf)branch.Children[child]).Count > 1)
        throw new Error("There is a glitch in my octree, sorry.");
      branch.Children[child] = null;
      while (branch.IsEmpty)
      {
        ChopBranch(branch.Parent, this.DetermineChild(branch.Parent, branch.MinX, branch.MinY, branch.MinZ));
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
            M x, y, z;
            this._locate(items[i], out x, out y, out z);
            if (
              _compare(x, node.MinX) == Comparison.Less &&
              _compare(y, node.MinY) == Comparison.Less &&
              _compare(z, node.MinZ) == Comparison.Less &&
              _compare(x, node.MaxX) == Comparison.Greater &&
              _compare(y, node.MaxY) == Comparison.Greater &&
              _compare(z, node.MaxZ) == Comparison.Greater)
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
            M x, y, z;
            this._locate(items[i], out x, out y, out z);
            if (
              _compare(x, node.MinX) == Comparison.Less &&
              _compare(y, node.MinY) == Comparison.Less &&
              _compare(z, node.MinZ) == Comparison.Less &&
              _compare(x, node.MaxX) == Comparison.Greater &&
              _compare(y, node.MaxY) == Comparison.Greater &&
              _compare(z, node.MaxZ) == Comparison.Greater)
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
            this._locate(items[i], out x, out y, out z);
            if (
              _compare(x, node.MinX) == Comparison.Less &&
              _compare(y, node.MinY) == Comparison.Less &&
              _compare(z, node.MinZ) == Comparison.Less &&
              _compare(x, node.MaxX) == Comparison.Greater &&
              _compare(y, node.MaxY) == Comparison.Greater &&
              _compare(z, node.MaxZ) == Comparison.Greater)
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
            M x, y, z;
            this._locate(items[i], out x, out y, out z);
            if (
              _compare(x, node.MinX) == Comparison.Less &&
              _compare(y, node.MinY) == Comparison.Less &&
              _compare(z, node.MinZ) == Comparison.Less &&
              _compare(x, node.MaxX) == Comparison.Greater &&
              _compare(y, node.MaxY) == Comparison.Greater &&
              _compare(z, node.MaxZ) == Comparison.Greater)
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

  /// <summary>Stores objects efficiently in 3-Dimensional space by x, y, and z coordinates.</summary>
  /// <typeparam name="T">The generice type of items to be stored in this octree.</typeparam>
  [System.Serializable]
  public class Octree_Linked_Center<T> : Octree<T>
  {
    /// <summary>Represents a single node of the octree. Includes references both upwards and
    /// downwards the tree.</summary>
    private abstract class Node
    {
      private float _x, _y, _z, _scale;
      private Branch _parent;

      internal float X { get { return _x; } }
      internal float Y { get { return _y; } }
      internal float Z { get { return _z; } }
      internal float Scale { get { return _scale; } }
      internal Branch Parent { get { return _parent; } }

      internal Node(float x, float y, float z, float scale, Branch parent)
      {
        _x = x;
        _y = y;
        _z = z;
        _scale = scale;
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

      internal Leaf(float x, float y, float z, float scale, Branch parent, int branchFactor)
        : base(x, y, z, scale, parent)
      { _contents = new T[branchFactor]; }

      internal Leaf Add(T addition)
      {
        System.Console.WriteLine("Placing " + addition + ", in " + this.X + ", " + this.Y + ", " + this.Z);
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

      internal Branch(float x, float y, float z, float scale, Branch parent)
        : base(x, y, z, scale, parent)
      {
        this._children = new Node[8];
      }
    }

    public delegate void Locate<Type>(Type item, out double x, out double y, out double z);
    private Locate<T> _locate;
    private int _loadFactor;
    private int _count;
    private Node _top;

    public int Count { get { return _count; } }
    public bool IsEmpty { get { return _count == 0; } }

    /// <summary>Creates an octree for three dimensional space partitioning.</summary>
    /// <param name="x">The x coordinate of the center of the octree.</param>
    /// <param name="y">The y coordinate of the center of the octree.</param>
    /// <param name="z">The z coordinate of the center of the octree.</param>
    /// <param name="scale">How far the tree expands along each dimension.</param>
    /// <param name="loadFactor">The maximum items per octree node before expansion.</param>
    public Octree_Linked_Center(float x, float y, float z, float scale, int loadFactor, Locate<T> locate)
    {
      this._loadFactor = loadFactor;
      this._top = new Leaf(x, y, z, scale, null, _loadFactor);
      this._count = 0;
      this._locate = locate;
    }

    /// <summary>Adds an item to the Octree.</summary>
    /// <param name="id">The id associated with the addition.</param>
    /// <param name="addition">The addition.</param>
    public void Add(T addition)
    {
      this.Add(addition, _top);
      this._count++;
    }

    /// <summary>Recursively adds an item to the octree and returns the node where the addition was placed
    /// and adjusts the octree structure as needed.</summary>
    private Leaf Add(T addition, Node octreeNode)
    {
      System.Console.WriteLine("Adding " + addition + " to " + octreeNode.X + ", " + octreeNode.Y + ", " + octreeNode.Z);

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
            growth = (Branch)(_top = new Branch(_top.X, _top.Y, _top.Z, _top.Scale, null));
          else
          {
            double x, y, z;
            this._locate(addition, out x, out y, out z);
            growth = GrowBranch(parent, this.DetermineChild(parent, x, y, z));
          }
          return Add(addition, growth);
        }
      }
      // We are still traversing the tree, determine the next move
      else
      {
        Branch branch = (Branch)octreeNode;
        double x, y, z;
        this._locate(addition, out x, out y, out z);
        int child = this.DetermineChild(branch, x, y, z);
        // If the leaf is null, we must grow one before attempting to add to it
        if (branch.Children[child] == null)
          return GrowLeaf(branch, child).Add(addition);
        return Add(addition, branch.Children[child]);
      }
    }

    // Grows a branch on the tree at the desired location
    private Branch GrowBranch(Branch branch, int child)
    {
      // values for the new node
      float x, y, z, scale;
      this.DetermineChildBounds(branch, child, out x, out y, out z, out scale);
      branch.Children[child] = new Branch(x, y, z, scale, branch);
      System.Console.WriteLine("Growing branch " + x + ", " + y + ", " + z);
      return (Branch)branch.Children[child];
    }

    // Grows a leaf on the tree at the desired location
    private Leaf GrowLeaf(Branch branch, int child)
    {
      if (branch.Children[child] != null)
        throw new Error("My octree has a glitched, sorry.");
      // values for new node
      float x, y, z, scale;
      this.DetermineChildBounds(branch, child, out x, out y, out z, out scale);
      branch.Children[child] = new Leaf(x, y, z, scale, branch, _loadFactor);
      System.Console.WriteLine("Growing leaf " + x + ", " + y + ", " + z);
      return (Leaf)branch.Children[child];
    }

    /// <summary>
    /// Determines relative child index.
    /// 0: (-x, -y, -z)
    /// 1: (-x, -y, +z)
    /// 2: (-x, +y, -z)
    /// 3: (-x, +y, +z)
    /// 4: (+x, -y, -z)
    /// 5: (+x, -y, +z)
    /// 6: (+x, +y, -z)
    /// 7: (+x, +y, +z)
    /// </summary>
    private int DetermineChild(Node node, double x, double y, double z)
    {
      if (z < node.Z)
        if (y < node.Y)
          if (x < node.X)
            return 0;
          else // (x >= node.X)
            return 4;
        else // (y >= node.Y)
          if (x < node.X)
            return 2;
          else // (x >= node.X)
            return 6;
      else // (z >= node.Z)
        if (y < node.Y)
          if (x < node.X)
            return 1;
          else // (x >= node.X)
            return 5;
        else // (y >= node.Y)
          if (x < node.X)
            return 3;
          else  // (x >= node.X)
            return 7;
    }

    /// <summary>Determins the bounds of a child node.</summary>
    private void DetermineChildBounds(Node node, int child, out float x, out float y, out float z, out float scale)
    {
      float halfScale = node.Scale * 0.5f;
      switch (child)
      {
        case 0:
          x = node.X - halfScale;
          y = node.Y - halfScale;
          z = node.Z - halfScale;
          scale = halfScale;
          return;
        case 1:
          x = node.X - halfScale;
          y = node.Y - halfScale;
          z = node.Z + halfScale;
          scale = halfScale;
          return;
        case 2:
          x = node.X - halfScale;
          y = node.Y + halfScale;
          z = node.Z - halfScale;
          scale = halfScale;
          return;
        case 3:
          x = node.X - halfScale;
          y = node.Y + halfScale;
          z = node.Z + halfScale;
          scale = halfScale;
          return;
        case 4:
          x = node.X + halfScale;
          y = node.Y - halfScale;
          z = node.Z - halfScale;
          scale = halfScale;
          return;
        case 5:
          x = node.X + halfScale;
          y = node.Y - halfScale;
          z = node.Z + halfScale;
          scale = halfScale;
          return;
        case 6:
          x = node.X + halfScale;
          y = node.Y + halfScale;
          z = node.Z - halfScale;
          scale = halfScale;
          return;
        case 7:
          x = node.X + halfScale;
          y = node.Y + halfScale;
          z = node.Z + halfScale;
          scale = halfScale;
          return;
        default:
          throw new Error("There is a glitch in my octree, sorry...");
      }
    }

    private bool ContainsBounds(Node node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      return !(node == null ||
          xMax < node.X - node.Scale || xMin > node.X + node.Scale ||
          yMax < node.Y - node.Scale || yMin > node.Y + node.Scale ||
          zMax < node.Z - node.Scale || zMin > node.Z + node.Scale);
    }

    private bool ContainsCoordinate(Node node, float x, float y, float z)
    {
      return !(node == null ||
          x < node.X - node.Scale || x > node.X + node.Scale ||
          y < node.Y - node.Scale || y > node.Y + node.Scale ||
          z < node.Z - node.Scale || z > node.Z + node.Scale);
    }

    private void PluckLeaf(Branch branch, int child)
    {
      if (!(branch.Children[child] is Leaf) || ((Leaf)branch.Children[child]).Count > 1)
        throw new Error("There is a glitch in my octree, sorry.");
      branch.Children[child] = null;
      while (branch.IsEmpty)
      {
        ChopBranch(branch.Parent, this.DetermineChild(branch.Parent, branch.X, branch.Y, branch.Z));
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
    private ForeachStatus Foreach(ForeachBreak<T> function, Node octreeNode, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      if (octreeNode != null)
      {
        if (octreeNode is Leaf)
        {
          int count = ((Leaf)octreeNode).Count;
          T[] items = ((Leaf)octreeNode).Contents;
          for (int i = 0; i < count; i++)
          {
            double x, y, z;
            this._locate(items[i], out x, out y, out z);
            if (items[i] != null &&
              x > xMin && x < xMax &&
              y > yMin && y < yMax &&
              z > zMin && z < zMax)
                if (function(items[i]) == ForeachStatus.Break)
                  return ForeachStatus.Break;
          }
        }
        else
        {
          Branch branch = (Branch)octreeNode;
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
    private ForeachStatus Foreach(ForeachRefBreak<T> function, Node octreeNode, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      if (octreeNode != null)
      {
        if (octreeNode is Leaf)
        {
          int count = ((Leaf)octreeNode).Count;
          T[] items = ((Leaf)octreeNode).Contents;
          for (int i = 0; i < count; i++)
          {
            double x, y, z;
            this._locate(items[i], out x, out y, out z);
            if (items[i] != null &&
              x > xMin && x < xMax &&
              y > yMin && y < yMax &&
              z > zMin && z < zMax)
              if (function(ref items[i]) == ForeachStatus.Break)
                return ForeachStatus.Break;
          }
        }
        else
        {
          Branch branch = (Branch)octreeNode;
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
    private void Foreach(Foreach<T> function, Node octreeNode, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
    {
      if (octreeNode != null)
      {
        if (octreeNode is Leaf)
        {
          int count = ((Leaf)octreeNode).Count;
          T[] items = ((Leaf)octreeNode).Contents;
          for (int i = 0; i < count; i++)
          {
            double x, y, z;
            this._locate(items[i], out x, out y, out z);
            if (items[i] != null &&
                x > xMin && x < xMax &&
                y > yMin && y < yMax &&
                z > zMin && z < zMax)
              function(items[i]);
          }
        }
        else
        {
          Branch branch = (Branch)octreeNode;
          for (int i = 0; i < 8; i++)
            Foreach(function, branch.Children[i], xMin, yMin, zMin, xMax, yMax, zMax);
        }
      }
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
    public int SizeOf { get { throw new System.NotImplementedException(); } }
    
    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains(T item, Compare<T> compare)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<T, Key> compare)
    {
      throw new System.NotImplementedException();
    }

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

  #region OctreeLinked

  //[Serializable]
  //public class OctreeLinked<Type> : Octree<Type>
  //  where Type : IOctreeEntry
  //{
  //  private Func<Type, Type, int> _comparisonFunction;
  //  private Func<OctreeLinkedReference, Type, int> _referenceComparison;

  //  #region OctreeLinkedNode

  //  /// <summary>Represents a single node of the octree. Includes references both upwards and
  //  /// downwards the tree.</summary>
  //  private abstract class OctreeLinkedNode
  //  {
  //    private float _x, _y, _z, _scale;
  //    private OctreeLinkedBranch _parent;

  //    internal float X { get { return _x; } }
  //    internal float Y { get { return _y; } }
  //    internal float Z { get { return _z; } }
  //    internal float Scale { get { return _scale; } }
  //    internal OctreeLinkedBranch Parent { get { return _parent; } }

  //    internal OctreeLinkedNode(float x, float y, float z, float scale, OctreeLinkedBranch parent)
  //    {
  //      _x = x;
  //      _y = y;
  //      _z = z;
  //      _scale = scale;
  //      _parent = parent;
  //    }

  //    /// <summary>Finds the child index relative to "this" node given x, y, and z coordinates.</summary>
  //    static internal int DetermineChild(OctreeLinkedNode node, float x, float y, float z)
  //    {
  //      // Finds the child given an x, y, and z
  //      // Possible child (all): 0, 1, 2, 3, 4, 5, 6, 7
  //      if (z < node.Z)
  //      {
  //        // Possible child: 0, 2, 4, 6
  //        if (y < node.Y)
  //          // Possible child: 0, 4
  //          if (x < node.X) return 0;
  //          else return 4;
  //        else
  //          // Possible child: 2, 6, 
  //          if (x < node.X) return 2;
  //          else return 6;
  //      }
  //      else
  //      {
  //        // Possible child: 1, 3, 5, 7
  //        if (y < node.Y)
  //          // Possible child: 1, 5
  //          if (x < node.X) return 1;
  //          else return 5;
  //        else
  //          // Possible child: 3, 7 
  //          if (x < node.X) return 3;
  //          else return 7;
  //      }
  //    }

  //    /// <summary>Determins the bounds of a child node.</summary>
  //    static internal void DetermineChildBounds(OctreeLinkedNode node, int child, out float x, out float y, out float z, out float scale)
  //    {
  //      float halfScale = node.Scale * .5f;
  //      switch (child)
  //      {
  //        case 0: x = node.X - halfScale; y = node.Y - halfScale; z = node.Z - halfScale; scale = halfScale; return;
  //        case 1: x = node.X - halfScale; y = node.Y - halfScale; z = node.Z + halfScale; scale = halfScale; return;
  //        case 2: x = node.X - halfScale; y = node.Y + halfScale; z = node.Z - halfScale; scale = halfScale; return;
  //        case 3: x = node.X - halfScale; y = node.Y + halfScale; z = node.Z + halfScale; scale = halfScale; return;
  //        case 4: x = node.X + halfScale; y = node.Y - halfScale; z = node.Z - halfScale; scale = halfScale; return;
  //        case 5: x = node.X + halfScale; y = node.Y - halfScale; z = node.Z + halfScale; scale = halfScale; return;
  //        case 6: x = node.X + halfScale; y = node.Y + halfScale; z = node.Z - halfScale; scale = halfScale; return;
  //        case 7: x = node.X + halfScale; y = node.Y + halfScale; z = node.Z + halfScale; scale = halfScale; return;
  //        default: throw new OctreeLinkedException("There is a glitch in my octree, sorry...");
  //      }
  //    }

  //    static internal bool ContainsBounds(OctreeLinkedNode node, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
  //    {
  //      return !(node == null ||
  //          xMax < node.X - node.Scale || xMin > node.X + node.Scale ||
  //          yMax < node.Y - node.Scale || yMin > node.Y + node.Scale ||
  //          zMax < node.Z - node.Scale || zMin > node.Z + node.Scale);
  //    }

  //    static internal bool ContainsCoordinate(OctreeLinkedNode node, float x, float y, float z)
  //    {
  //      return !(node == null ||
  //          x < node.X - node.Scale || x > node.X + node.Scale ||
  //          y < node.Y - node.Scale || y > node.Y + node.Scale ||
  //          z < node.Z - node.Scale || z > node.Z + node.Scale);
  //    }
  //  }

  //  #endregion

  //  #region OctreeLinkedLeaf

  //  /// <summary>Represents a single node of the octree. Includes references both upwards and
  //  /// downwards the tree.</summary>
  //  private class OctreeLinkedLeaf : OctreeLinkedNode
  //  {
  //    //private OctreeEntry[] _contents;
  //    private Type[] _contents;
  //    private int _count;

  //    //internal OctreeEntry[] Contents { get { return _contents; } }
  //    internal Type[] Contents { get { return _contents; } }
  //    internal int Count { get { return _count; } set { _count = value; } }
  //    internal bool IsFull { get { return _count == _contents.Length; } }

  //    internal OctreeLinkedLeaf(float x, float y, float z, float scale, OctreeLinkedBranch parent, int branchFactor)
  //      : base(x, y, z, scale, parent)
  //    { _contents = new Type[branchFactor]; }

  //    internal OctreeLinkedLeaf Add(Type addition)
  //    {
  //      if (_count == _contents.Length)
  //        throw new OctreeLinkedException("There is a glitch in my octree, sorry...");
  //      _contents[_count++] = addition;
  //      return this;
  //    }
  //  }

  //  #endregion

  //  #region OctreelinkedBranch

  //  /// <summary>Represents a single node of the octree. Includes references both upwards and
  //  /// downwards the tree.</summary>
  //  private class OctreeLinkedBranch : OctreeLinkedNode
  //  {
  //    // The children are indexed as follows (relative to this node's center):
  //    // 0: (-x, -y, -z)   1: (-x, -y, z)   2: (-x, y, -z)   3: (-x, y, z)
  //    // 4: (x, -y, -z)   5: (x, -y, z)   6: (x, y, -z)   7: (x, y, z)
  //    //private OctreeLinkedNode[] _children;

  //    private OctreeLinkedNode _child0;
  //    private OctreeLinkedNode _child1;
  //    private OctreeLinkedNode _child2;
  //    private OctreeLinkedNode _child3;
  //    private OctreeLinkedNode _child4;
  //    private OctreeLinkedNode _child5;
  //    private OctreeLinkedNode _child6;
  //    private OctreeLinkedNode _child7;

  //    public OctreeLinkedNode Child0 { get { return _child0; } }
  //    public OctreeLinkedNode Child1 { get { return _child1; } }
  //    public OctreeLinkedNode Child2 { get { return _child2; } }
  //    public OctreeLinkedNode Child3 { get { return _child3; } }
  //    public OctreeLinkedNode Child4 { get { return _child4; } }
  //    public OctreeLinkedNode Child5 { get { return _child5; } }
  //    public OctreeLinkedNode Child6 { get { return _child6; } }
  //    public OctreeLinkedNode Child7 { get { return _child7; } }

  //    public OctreeLinkedNode this[int index]
  //    {
  //      get
  //      {
  //        switch (index)
  //        {
  //          case 0: return _child0;
  //          case 1: return _child1;
  //          case 2: return _child2;
  //          case 3: return _child3;
  //          case 4: return _child4;
  //          case 5: return _child5;
  //          case 6: return _child6;
  //          case 7: return _child7;
  //          default: throw new OctreeLinkedException("index out of bounds.");
  //        }
  //      }
  //      set
  //      {
  //        switch (index)
  //        {
  //          case 0: _child0 = value; break;
  //          case 1: _child1 = value; break;
  //          case 2: _child2 = value; break;
  //          case 3: _child3 = value; break;
  //          case 4: _child4 = value; break;
  //          case 5: _child5 = value; break;
  //          case 6: _child6 = value; break;
  //          case 7: _child7 = value; break;
  //          default: throw new OctreeLinkedException("index out of bounds.");
  //        }
  //      }
  //    }

  //    //internal OctreeLinkedNode[] Children { get { return _children; } }

  //    internal bool IsEmpty
  //    {
  //      get
  //      {
  //        //return _children[0] == null && _children[1] == null && _children[2] == null
  //        //  && _children[3] == null && _children[4] == null && _children[5] == null
  //        //  && _children[6] == null && _children[7] == null;
  //        return _child0 == null && _child1 == null && _child2 == null
  //          && _child3 == null && _child4 == null && _child5== null
  //          && _child6 == null && _child7 == null;
  //      }
  //    }

  //    internal OctreeLinkedBranch(float x, float y, float z, float scale, OctreeLinkedBranch parent)
  //      : base(x, y, z, scale, parent)
  //    {
  //      //_children = new OctreeLinkedNode[8];
  //    }
  //  }

  //  #endregion

  //  #region OctreeLinkedReference

  //  private class OctreeLinkedReference
  //  {
  //    private Type _value;
  //    private OctreeLinkedLeaf _leaf;

  //    internal Type Value { get { return _value; } set { _value = value; } }
  //    internal OctreeLinkedLeaf Leaf { get { return _leaf; } set { _leaf = value; } }

  //    internal OctreeLinkedReference(Type value, OctreeLinkedLeaf leaf) { _value = value; _leaf = leaf; }
  //  }

  //  #endregion

  //  private int _branchFactor;
  //  private int _count;
  //  private AvlTree<OctreeLinkedReference> _referenceDatabase;
  //  private OctreeLinkedNode _top;

  //  private object _lock;
  //  private int _readers;
  //  private int _writers;

  //  public int Count { get { return _count; } }
  //  public bool IsEmpty { get { return _count == 0; } }

  //  /// <summary>Creates an octree for three dimensional space partitioning.</summary>
  //  /// <param name="x">The x coordinate of the center of the octree.</param>
  //  /// <param name="y">The y coordinate of the center of the octree.</param>
  //  /// <param name="z">The z coordinate of the center of the octree.</param>
  //  /// <param name="scale">How far the tree expands along each dimension.</param>
  //  /// <param name="branchFactor">The maximum items per octree node before expansion.</param>
  //  public OctreeLinked(float x, float y, float z, float scale, int branchFactor,
  //    Func<Type, Type, int> comparisonFunction)
  //  {
  //    _branchFactor = branchFactor;
  //    _top = new OctreeLinkedLeaf(x, y, z, scale, null, _branchFactor);
  //    _count = 0;

  //    _comparisonFunction = comparisonFunction;

  //    _referenceComparison =
  //      (OctreeLinkedReference left, Type right) =>
  //      { return comparisonFunction(left.Value, right); };

  //    Func<OctreeLinkedReference, OctreeLinkedReference, int> octreeReferenceComparison =
  //      (OctreeLinkedReference left, OctreeLinkedReference right) =>
  //      { return comparisonFunction(left.Value, right.Value); };
  //    _referenceDatabase = null;// new AvlTreeLinked<OctreeLinkedReference>(octreeReferenceComparison);
  //  }

  //  /// <summary>Adds an item to the Octree.</summary>
  //  /// <param name="id">The id associated with the addition.</param>
  //  /// <param name="addition">The addition.</param>
  //  /// <param name="x">The x coordinate of the addition's location.</param>
  //  /// <param name="y">The y coordinate of the addition's location.</param>
  //  /// <param name="z">The z coordinate of the addition's location.</param>
  //  public void Add(Type addition)
  //  {
  //    _referenceDatabase.Add(new OctreeLinkedReference(addition, Add(addition, _top)));
  //    _count++;
  //  }

  //  /// <summary>Recursively adds an item to the octree and returns the node where the addition was placed
  //  /// and adjusts the octree structure as needed.</summary>
  //  private OctreeLinkedLeaf Add(Type addition, OctreeLinkedNode octreeNode)
  //  {
  //    // If the node is a leaf we have reached the bottom of the tree
  //    if (octreeNode is OctreeLinkedLeaf)
  //    {
  //      OctreeLinkedLeaf leaf = (OctreeLinkedLeaf)octreeNode;
  //      if (!leaf.IsFull)
  //      {
  //        // We found a proper leaf, and the leaf has room, just add it
  //        leaf.Add(addition);
  //        return leaf;
  //      }
  //      else
  //      {
  //        // The leaf is full so we need to grow out the tree
  //        OctreeLinkedBranch parent = octreeNode.Parent;
  //        OctreeLinkedBranch growth;
  //        if (parent == null)
  //          growth = (OctreeLinkedBranch)(_top = new OctreeLinkedBranch(_top.X, _top.Y, _top.Z, _top.Scale, null));
  //        else
  //          growth = GrowBranch(parent, OctreeLinkedNode.DetermineChild(parent, addition.Position.X, addition.Position.Y, addition.Position.Z));
  //        //foreach (Type entry in leaf.Contents)
  //        //  _referenceDatabase.Get<Type>(entry, _referenceComparison).Leaf = Add(entry, growth);
  //        return Add(addition, growth);
  //      }
  //    }
  //    // We are still traversing the tree, determine the next move
  //    else
  //    {
  //      OctreeLinkedBranch branch = (OctreeLinkedBranch)octreeNode;
  //      int child = OctreeLinkedNode.DetermineChild(branch, addition.Position.X, addition.Position.Y, addition.Position.Z);
  //      // If the leaf is null, we must grow one before attempting to add to it
  //      if (branch[child] == null)
  //        return GrowLeaf(branch, child).Add(addition);
  //      return Add(addition, branch[child]);
  //    }
  //  }

  //  // Grows a branch on the tree at the desired location
  //  private OctreeLinkedBranch GrowBranch(OctreeLinkedBranch branch, int child)
  //  {
  //    // values for the new node
  //    float x, y, z, scale;
  //    OctreeLinkedNode.DetermineChildBounds(branch, child, out x, out y, out z, out scale);
  //    branch[child] = new OctreeLinkedBranch(x, y, z, scale, branch);
  //    return (OctreeLinkedBranch)branch[child];
  //  }

  //  // Grows a leaf on the tree at the desired location
  //  private OctreeLinkedLeaf GrowLeaf(OctreeLinkedBranch branch, int child)
  //  {
  //    if (branch[child] != null)
  //      throw new OctreeLinkedException("My octree has a glitched, sorry.");
  //    // values for new node
  //    float x, y, z, scale;
  //    OctreeLinkedNode.DetermineChildBounds(branch, child, out x, out y, out z, out scale);
  //    branch[child] = new OctreeLinkedLeaf(x, y, z, scale, branch, _branchFactor);
  //    return (OctreeLinkedLeaf)branch[child];
  //  }

  //  /// <summary>Removes an item from the octree by the id that was assigned to it.</summary>
  //  /// <param name="id">The string id of the removal that was given to the item when it was added.</param>
  //  public void Remove(Type key)
  //  {
  //    throw new NotImplementedException();
  //    //Remove(key, _referenceDatabase.Get<Type>(key, _referenceComparison).Leaf);
  //    //_referenceDatabase.Remove<Type>(key, _referenceComparison);
  //    _count--;
  //  }

  //  private void Remove(Type key, OctreeLinkedLeaf leaf)
  //  {
  //    if (leaf.Count > 1)
  //    {
  //      Type[] contents = leaf.Contents;
  //      for (int i = 0; i < leaf.Count; i++)
  //        if (_comparisonFunction(contents[i], key) == 0)
  //        {
  //          Type temp = contents[_count - 1];
  //          contents[_count - 1] = contents[i];
  //          contents[i] = temp;
  //          break;
  //        }
  //    }
  //    else PluckLeaf(leaf.Parent, OctreeLinkedNode.DetermineChild(leaf.Parent, leaf.X, leaf.Y, leaf.Z));
  //  }

  //  private void PluckLeaf(OctreeLinkedBranch branch, int child)
  //  {
  //    if (!(branch[child] is OctreeLinkedLeaf) || ((OctreeLinkedLeaf)branch[child]).Count > 1)
  //      throw new OctreeLinkedException("There is a glitch in my octree, sorry.");
  //    branch[child] = null;
  //    while (branch.IsEmpty)
  //    {
  //      ChopBranch(branch.Parent, OctreeLinkedNode.DetermineChild(branch.Parent, branch.X, branch.Y, branch.Z));
  //      branch = branch.Parent;
  //    }
  //  }

  //  private void ChopBranch(OctreeLinkedBranch branch, int child)
  //  {
  //    if (branch[child] == null)
  //      throw new OctreeLinkedException("There is a glitch in my octree, sorry...");
  //    branch[child] = null;
  //  }

  //  /// <summary>Moves an existing item from one position to another.</summary>
  //  /// <param name="key">The key of the item to be moved.</param>
  //  /// <param name="x">The x coordinate of the new position of the item.</param>
  //  /// <param name="y">The y coordinate of the new position of the item.</param>
  //  /// <param name="z">The z coordinate of the new position of the item.</param>
  //  public void Move(Type key, float x, float y, float z)
  //  {
  //    OctreeLinkedLeaf leaf = null;// _referenceDatabase.Get<Type>(key, _referenceComparison).Leaf;
  //    Type entry = default(Type);
  //    bool found = false;
  //    foreach (Type value in leaf.Contents)
  //      if (_comparisonFunction(value, key) == 0)
  //      {
  //        entry = value;
  //        found = true;
  //        break;
  //      }
  //    if (found == false)
  //      throw new OctreeLinkedException("attempting to move a non-existing value.");
  //    entry.Position.X = x;
  //    entry.Position.Y = y;
  //    entry.Position.Z = z;
  //    if ((x > leaf.X - leaf.Scale && x < leaf.X + leaf.Scale)
  //      && (y > leaf.Y - leaf.Scale && y < leaf.Y + leaf.Scale)
  //      && (z > leaf.Z - leaf.Scale && z < leaf.Z + leaf.Scale))
  //      return;
  //    else
  //    {
  //      Remove(key, leaf);
  //      Add(entry, _top);
  //    }
  //  }

  //  /// <summary>Iterates through the entire tree and ensures each item is in the proper node.</summary>
  //  public void Update()
  //  {
  //    throw new NotImplementedException("Sorry, I'm still working on the update function.");
  //  }

  //  /// <summary>Performs a functional paradigm traversal of the octree.</summary>
  //  /// <param name="traversalFunction"></param>
  //  public bool TraverseBreakable(Func<Type, bool> traversalFunction)
  //  {
  //    if (!TraverseBreakable(traversalFunction, _top))
  //      return false;
  //    return true;
  //  }
  //  private bool TraverseBreakable(Func<Type, bool> traversalFunctionBreakable, OctreeLinkedNode octreeNode)
  //  {
  //    if (octreeNode != null)
  //    {
  //      if (octreeNode is OctreeLinkedLeaf)
  //      {
  //        foreach (Type item in ((OctreeLinkedLeaf)octreeNode).Contents)
  //          if (!traversalFunctionBreakable(item)) return false;
  //      }
  //      else
  //      {
  //        // The current node is a branch
  //        OctreeLinkedBranch branch = (OctreeLinkedBranch)octreeNode;
  //        if (!TraverseBreakable(traversalFunctionBreakable, branch.Child0)) return false;
  //        if (!TraverseBreakable(traversalFunctionBreakable, branch.Child1)) return false;
  //        if (!TraverseBreakable(traversalFunctionBreakable, branch.Child2)) return false;
  //        if (!TraverseBreakable(traversalFunctionBreakable, branch.Child3)) return false;
  //        if (!TraverseBreakable(traversalFunctionBreakable, branch.Child4)) return false;
  //        if (!TraverseBreakable(traversalFunctionBreakable, branch.Child5)) return false;
  //        if (!TraverseBreakable(traversalFunctionBreakable, branch.Child6)) return false;
  //        if (!TraverseBreakable(traversalFunctionBreakable, branch.Child7)) return false;
  //      }
  //    }
  //    return true;
  //  }

  //  public void Traverse(Action<Type> traversalFunction)
  //  {
  //    Traverse(traversalFunction, _top);
  //  }
  //  private void Traverse(Action<Type> traversalFunction, OctreeLinkedNode octreeNode)
  //  {
  //    if (octreeNode != null)
  //    {
  //      if (octreeNode is OctreeLinkedLeaf)
  //      {
  //        foreach (Type item in ((OctreeLinkedLeaf)octreeNode).Contents)
  //          traversalFunction(item);
  //      }
  //      else
  //      {
  //        // The current node is a branch
  //        OctreeLinkedBranch branch = (OctreeLinkedBranch)octreeNode;
  //        Traverse(traversalFunction, branch.Child0);
  //        Traverse(traversalFunction, branch.Child1);
  //        Traverse(traversalFunction, branch.Child2);
  //        Traverse(traversalFunction, branch.Child3);
  //        Traverse(traversalFunction, branch.Child4);
  //        Traverse(traversalFunction, branch.Child5);
  //        Traverse(traversalFunction, branch.Child6);
  //        Traverse(traversalFunction, branch.Child7);
  //      }
  //    }
  //  }

  //  /// <summary>Performs a functional paradigm traversal of the octree with data structure optimization.</summary>
  //  /// <param name="traversalFunction">The function to perform per iteration.</param>
  //  /// <param name="xMin">The minimum x of a rectangular prism to query the octree.</param>
  //  /// <param name="yMin">The minimum y of a rectangular prism to query the octree.</param>
  //  /// <param name="zMin">The minimum z of a rectangular prism to query the octree.</param>
  //  /// <param name="xMax">The maximum x of a rectangular prism to query the octree.</param>
  //  /// <param name="yMax">The maximum y of a rectangular prism to query the octree.</param>
  //  /// <param name="zMax">The maximum z of a rectangular prism to query the octree.</param>
  //  public bool TraverseBreakable(Func<Type, bool> traversalFunction, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
  //  {
  //    return TraverseBreakable(traversalFunction, _top, xMin, yMin, zMin, xMax, yMax, zMax);
  //  }
  //  private bool TraverseBreakable(Func<Type, bool> traversalFunction, OctreeLinkedNode octreeNode, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
  //  {
  //    if (octreeNode != null)
  //    {
  //      if (octreeNode is OctreeLinkedLeaf)
  //      {
  //        foreach (Type entry in ((OctreeLinkedLeaf)octreeNode).Contents)
  //          //if (!traversalFunction(item)) return false;
  //          if (entry != null &&
  //          entry.Position.X > xMin && entry.Position.X < xMax
  //          && entry.Position.Y > yMin && entry.Position.Y < yMax
  //          && entry.Position.Z > zMin && entry.Position.Z < zMax)
  //            if (!traversalFunction(entry)) return false;
  //      }
  //      else
  //      {
  //        OctreeLinkedBranch branch = (OctreeLinkedBranch)octreeNode;
  //        OctreeLinkedNode node = branch.Child0;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          if (!TraverseBreakable(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax))
  //            return false;
  //        node = branch.Child1;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          if (!TraverseBreakable(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax))
  //            return false;
  //        node = branch.Child2;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          if (!TraverseBreakable(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax))
  //            return false;
  //        node = branch.Child3;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          if (!TraverseBreakable(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax))
  //            return false;
  //        node = branch.Child4;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          if (!TraverseBreakable(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax))
  //            return false;
  //        node = branch.Child5;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          if (!TraverseBreakable(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax))
  //            return false;
  //        node = branch.Child6;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          if (!TraverseBreakable(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax))
  //            return false;
  //        node = branch.Child7;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          if (!TraverseBreakable(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax))
  //            return false;
  //      }
  //    }
  //    return true;
  //  }

  //  public void Traverse(Action<Type> traversalFunction, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
  //  {
  //    Traverse(traversalFunction, _top, xMin, yMin, zMin, xMax, yMax, zMax);
  //  }
  //  private void Traverse(Action<Type> traversalFunction, OctreeLinkedNode octreeNode, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
  //  {
  //    if (octreeNode != null)
  //    {
  //      if (octreeNode is OctreeLinkedLeaf)
  //      {
  //        foreach (Type entry in ((OctreeLinkedLeaf)octreeNode).Contents)
  //          if (entry != null &&
  //          entry.Position.X > xMin && entry.Position.X < xMax
  //          && entry.Position.Y > yMin && entry.Position.Y < yMax
  //          && entry.Position.Z > zMin && entry.Position.Z < zMax)
  //            traversalFunction(entry);
  //      }
  //      else
  //      {
  //        OctreeLinkedBranch branch = (OctreeLinkedBranch)octreeNode;
  //        OctreeLinkedNode node = branch.Child0;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          Traverse(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax);
  //        node = branch.Child1;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          Traverse(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax);
  //        node = branch.Child2;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          Traverse(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax);
  //        node = branch.Child3;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          Traverse(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax);
  //        node = branch.Child4;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          Traverse(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax);
  //        node = branch.Child5;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          Traverse(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax);
  //        node = branch.Child6;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          Traverse(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax);
  //        node = branch.Child7;
  //        if (OctreeLinkedNode.ContainsBounds(node, xMin, yMin, zMin, xMax, yMax, zMax))
  //          Traverse(traversalFunction, node, xMin, yMin, zMin, xMax, yMax, zMax);
  //      }
  //    }
  //  }

  //  public Type[] ToArray()
  //  {
  //    int finalIndex;
  //    Type[] array = new Type[_count];
  //    ToArray(_top, array, 0, out finalIndex);
  //    if (array.Length != finalIndex)
  //      throw new OctreeLinkedException("There is a glitch in my octree, sorry...");
  //    return array;
  //  }
  //  private void ToArray(OctreeLinkedNode octreeNode, Type[] array, int entryIndex, out int returnIndex)
  //  {
  //    if (octreeNode != null)
  //    {
  //      if (octreeNode is OctreeLinkedLeaf)
  //      {
  //        returnIndex = entryIndex;
  //        foreach (Type item in ((OctreeLinkedLeaf)octreeNode).Contents)
  //          array[returnIndex++] = item;
  //      }
  //      else
  //      {
  //        // The current node is a branch
  //        OctreeLinkedBranch branch = (OctreeLinkedBranch)octreeNode;
  //        ToArray(branch.Child0, array, entryIndex, out entryIndex);
  //        ToArray(branch.Child1, array, entryIndex, out entryIndex);
  //        ToArray(branch.Child2, array, entryIndex, out entryIndex);
  //        ToArray(branch.Child3, array, entryIndex, out entryIndex);
  //        ToArray(branch.Child4, array, entryIndex, out entryIndex);
  //        ToArray(branch.Child5, array, entryIndex, out entryIndex);
  //        ToArray(branch.Child6, array, entryIndex, out entryIndex);
  //        ToArray(branch.Child7, array, entryIndex, out entryIndex);
  //        returnIndex = entryIndex;
  //      }
  //    }
  //    else
  //      returnIndex = entryIndex;
  //  }

  //  #region Structure<Type>

  //  #region .Net Framework Compatibility

  //  /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
  //  IEnumerator IEnumerable.GetEnumerator()
  //  {
  //    throw new NotImplementedException();
  //  }

  //  /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
  //  IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
  //  {
  //    throw new NotImplementedException();
  //  }

  //  #endregion

  //  /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
  //  /// <remarks>Returns long.MaxValue on overflow.</remarks>
  //  public int SizeOf { get { throw new NotImplementedException(); } }

  //  /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
  //  /// <typeparam name="Key">The type of the key to check for.</typeparam>
  //  /// <param name="key">The key to check for.</param>
  //  /// <param name="compare">Delegate representing comparison technique.</param>
  //  /// <returns>An array containing all the values matching the key or null if non were found.</returns>
  //  //Type[] GetValues<Key>(Key key, Compare<Type, Key> compare);

  //  /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
  //  /// <typeparam name="Key">The type of the key to check for.</typeparam>
  //  /// <param name="key">The key to check for.</param>
  //  /// <param name="compare">Delegate representing comparison technique.</param>
  //  /// <returns>An array containing all the values matching the key or null if non were found.</returns>
  //  /// <param name="values">The values that matched the given key.</param>
  //  /// <returns>true if 1 or more values were found; false if no values were found.</returns>
  //  //bool TryGetValues<Key>(Key key, Compare<Type, Key> compare, out Type[] values);

  //  /// <summary>Checks to see if a given object is in this data structure.</summary>
  //  /// <param name="item">The item to check for.</param>
  //  /// <param name="compare">Delegate representing comparison technique.</param>
  //  /// <returns>true if the item is in this structure; false if not.</returns>
  //  public bool Contains(Type item, Compare<Type> compare)
  //  {
  //    throw new NotImplementedException();
  //  }

  //  /// <summary>Checks to see if a given object is in this data structure.</summary>
  //  /// <typeparam name="Key">The type of the key to check for.</typeparam>
  //  /// <param name="key">The key to check for.</param>
  //  /// <param name="compare">Delegate representing comparison technique.</param>
  //  /// <returns>true if the item is in this structure; false if not.</returns>
  //  public bool Contains<Key>(Key key, Compare<Type, Key> compare)
  //  {
  //    throw new NotImplementedException();
  //  }

  //  /// <summary>Invokes a delegate for each entry in the data structure.</summary>
  //  /// <param name="function">The delegate to invoke on each item in the structure.</param>
  //  public void Foreach(Foreach<Type> function)
  //  {
  //    throw new NotImplementedException();
  //  }

  //  /// <summary>Invokes a delegate for each entry in the data structure.</summary>
  //  /// <param name="function">The delegate to invoke on each item in the structure.</param>
  //  public void Foreach(ForeachRef<Type> function)
  //  {
  //    throw new NotImplementedException();
  //  }

  //  /// <summary>Invokes a delegate for each entry in the data structure.</summary>
  //  /// <param name="function">The delegate to invoke on each item in the structure.</param>
  //  /// <returns>The resulting status of the iteration.</returns>
  //  public ForeachStatus Foreach(ForeachBreak<Type> function)
  //  {
  //    throw new NotImplementedException();
  //  }

  //  /// <summary>Invokes a delegate for each entry in the data structure.</summary>
  //  /// <param name="function">The delegate to invoke on each item in the structure.</param>
  //  /// <returns>The resulting status of the iteration.</returns>
  //  public ForeachStatus Foreach(ForeachRefBreak<Type> function)
  //  {
  //    throw new NotImplementedException();
  //  }

  //  /// <summary>Creates a shallow clone of this data structure.</summary>
  //  /// <returns>A shallow clone of this data structure.</returns>
  //  public Structure<Type> Clone()
  //  {
  //    throw new NotImplementedException();
  //  }

  //  ///// <summary>Converts the structure into an array.</summary>
  //  ///// <returns>An array containing all the item in the structure.</returns>
  //  //public Type[] ToArray()
  //  //{
  //  //  throw new NotImplementedException();
  //  //}

  //  #endregion
    
  //  /// <summary>This is used for throwing OcTree exceptions only to make debugging faster.</summary>
  //  private class OctreeLinkedException : Error
  //  {
  //    public OctreeLinkedException(string message) : base(message) { }
  //  }
  //}

  #endregion
}