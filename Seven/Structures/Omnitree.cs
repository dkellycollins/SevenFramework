// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Structures
{
  /// <summary>The one data structure to rule them all. Made by Zachary Patten.</summary>
  /// <typeparam name="T">The generice type of items to be stored in this omnitree.</typeparam>
  /// <typeparam name="M">The type of the axis dimensions to sort the "T" values upon.</typeparam>
  public interface Omnitree<T, M> : Structure<T>
  {
    /// <summary>The number of dimensions in this tree.</summary>
    int Dimensions { get; }

    /// <summary>The current number of items in the tree.</summary>
    int Count { get; }

    /// <summary>True (if Count == 0).</summary>
    bool IsEmpty { get; }

    /// <summary>Adds an item to the tree.</summary>
    /// <param name="addition">The item to be added.</param>
    void Add(T addition);

    /// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
    void Update();

    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
    /// <param name="min">The minimum dimensions of the space to update.</param>
    /// <param name="max">The maximum dimensions of the space to update.</param>
    void Update(M[] min, M[] max);

    /// <summary>Removes all the items in a given space.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    void Remove(M[] min, M[] max);

    /// <summary>Removes all the items in a given space where equality is met.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    /// <param name="where">The equality constraint of the removal.</param>
    void Remove(M[] min, M[] max, Predicate<T> where);

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    void Foreach(Foreach<T> function, M[] min, M[] max);

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    void Foreach(ForeachRef<T> function, M[] min, M[] max);

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    ForeachStatus Foreach(ForeachBreak<T> function, M[] min, M[] max);

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    ForeachStatus Foreach(ForeachRefBreak<T> function, M[] min, M[] max);

    /// <summary>Returns the tree to an empty state.</summary>
    void Clear();
  }

  /// <summary>The one data structure to rule them all. Made by Zachary Patten.</summary>
  /// <typeparam name="T">The generice type of items to be stored in this octree.</typeparam>
  /// <typeparam name="M">The type of the axis dimensions to sort the "T" values upon.</typeparam>
  [System.Serializable]
  public class Omnitree_Linked<T, M> : Omnitree<T, M>
  {
    /// <summary>Can be a leaf or a branch.</summary>
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

    /// <summary>A branch in the tree. Only contains items.</summary>
    private class Leaf : Node
    {
      internal class Node
      {
        private T _value;
        private Node _next;

        public T Value { get { return _value; } set { _value = value; } }
        public Node Next { get { return _next; } set { _next = value; } }

        public Node(T value, Node next)
        {
          _value = value;
          _next = next;
        }
      }

      private Node _head;
      private int _count;

      public Node Head { get { return this._head; } }
      public int Count { get { return this._count; } set { this._count = value; } }

      public Leaf(M[] min, M[] max, Branch parent, int load)
        : base(min, max, parent) { }

      public void Add(T addition)
      {
        this._head = new Node(addition, this._head);
        this._count++;
      }
    }

    /// <summary>A branch in the tree. Only contains nodes.</summary>
    private class Branch : Node
    {
      internal class Node
      {
        private int _index;
        private Omnitree_Linked<T, M>.Node _value;
        private Branch.Node _next;

        public int Index { get { return this._index; } }
        public Omnitree_Linked<T, M>.Node Value { get { return this._value; } }
        public Branch.Node Next { get { return this._next; } set { this._next = value; } }

        public Node(int index, Omnitree_Linked<T, M>.Node value, Branch.Node next)
        {
          this._index = index;
          this._value = value;
          this._next = next;
        }
      }

      private Branch.Node _head;

      public Branch.Node Head { get { return this._head; } set { this._head = value; } }

      internal Omnitree_Linked<T, M>.Node this[int index]
      {
        get
        {
          Branch.Node list = _head;
          while (list != null)
            if (list.Index == index)
              return list.Value;
            else
              list = list.Next;
          return null;
        }
      }

      public bool IsEmpty
      {
        get {
          throw new System.NotImplementedException();
          // dis wont quite work...
          return this._head == null; }
      }

      public Branch(M[] min, M[] max, Branch parent)
        : base(min, max, parent) { }
    }

    /// <summary>Locates an item along the given dimensions.</summary>
    /// <param name="item">The item to be located.</param>
    /// <returns>The computed locations of the item.</returns>
    public delegate M[] Locate_NonOut(T item);

    /// <summary>Locates an item along the given dimensions.</summary>
    /// <param name="item">The item to be located.</param>
    /// <param name="ms">The computed locations of the item.</param>
    public delegate void Locate(T item, out M[] ms);

    /// <summary>Computes the average between two items.</summary>
    /// <param name="left">The first item of the average.</param>
    /// <param name="right">The second item of the average.</param>
    /// <returns>The computed average between the two items.</returns>
    public delegate M Average(M left, M right);

    // Constants
    private const int _defaultLoad = 7;

    // Immutable Fields
    private Locate _locate;
    private Average _average;
    private Compare<M> _compare;
    private int _dimensions;
    private int _children;

    // Mutable Fields
    private Node _top;
    private int _count;
    private int _load;
    private int _loadPlusOneSquared;
    private int _loadSquared;

    /// <summary>The number of dimensions in this tree.</summary>
    public int Dimensions { get { return this._dimensions; } }

    /// <summary>The current number of items in the tree.</summary>
    public int Count { get { return this._count; } }

    /// <summary>True if (Count == 0).</summary>
    public bool IsEmpty { get { return this._count == 0; } }

    /// <summary>Gets the current memory allocation size of this structure.</summary>
    public int SizeOf { get { throw new System.NotImplementedException("Sorry, I'm working on it."); } }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    public Omnitree_Linked(
      M[] min, M[] max,
      Locate locate,
      Compare<M> compare,
      Average average)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions.");
      
      this._locate = locate;
      this._average = average;
      this._compare = compare;

      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;
      this._load = _defaultLoad;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
      this._count = 0;
      this._top = new Leaf(min, max, null, _load);
    }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    public Omnitree_Linked(
      M[] min, M[] max,
      Locate_NonOut locate,
      Compare<M> compare,
      Average average)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");
      
      // This is just an adapter, JIT will optimize
      this._locate = (T item, out M[] ms) => { ms = locate(item); };
      this._average = average;
      this._compare = compare;

      this._top = new Leaf(min, max, null, _load);
      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;
      this._load = _defaultLoad;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
      this._count = 0;
      this._top = new Leaf(min, max, null, _load);
    }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    /// <param name="load">The initial load (slight optimization for large populations).</param>
    public Omnitree_Linked(
      M[] min, M[] max,
      Locate locate,
      Compare<M> compare,
      Average average,
      int load)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");
      
      this._locate = locate;
      this._average = average;
      this._compare = compare;

      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;
      this._load = load;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
      this._count = 0;
      this._top = new Leaf(min, max, null, _load);
    }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    /// <param name="load">The initial load (slight optimization for large populations).</param>
    public Omnitree_Linked(
      M[] min, M[] max,
      Locate_NonOut locate,
      Compare<M> compare,
      Average average,
      int load)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");
      
      // This is just an adapter, JIT will optimize
      this._locate = (T item, out M[] ms) => { ms = locate(item); };
      this._average = average;
      this._compare = compare;

      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;
      this._load = load;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
      this._count = 0;
      this._top = new Leaf(min, max, null, _load);
    }

    /// <summary>Adds an item to the tree.</summary>
    /// <param name="addition">The item to be added.</param>
    public void Add(T addition)
    {
      if (this._count == int.MaxValue)
        throw new Error("(Count == int.MaxValue) switch ints to longs in source code.");

      if (this._loadPlusOneSquared < _count)
      {
        this._load++;
        this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
        this._loadSquared = this._load * this._load;
      }

      M[] ms;
      this._locate(addition, out ms);

      if (ms.Length != this._dimensions)
        throw new Error("the location function for omnitree is invalid.");

      if (!InclusionCheck(this._top, ms))
        throw new Error("out of bounds during addition");

      if (this._top is Leaf && (this._top as Leaf).Count >= this._load)
      {
        Leaf.Node list = (this._top as Leaf).Head;
        _top = new Branch(_top.Min, _top.Max, null);

        while (list != null)
        {
          M[] child_ms;
          this._locate(list.Value, out child_ms);

          if (child_ms.Length != this._dimensions)
            throw new Error("the location function for omnitree is invalid.");

          // NEED BOUNDS CHECKING HERE

          this.Add(list.Value, this._top, child_ms, 0);
          list = list.Next;
        }
      }

      this.Add(addition, _top, ms, 0);
      this._count++;
    }

    /// <summary>Recursive version of the add function.</summary>
    /// <param name="addition">The item to be added.</param>
    /// <param name="node">The current location of the tree.</param>
    /// <param name="ms">The location of the addition.</param>
    /// <param name="depth">The current depth to prevent long running </param>
    private void Add(T addition, Node node, M[] ms, int depth)
    {
      if (node is Leaf)
      {
        Leaf leaf = node as Leaf;
        if (depth >= this._load || !(leaf.Count >= this._load))
        {
          leaf.Add(addition);
          return;
        }
        else
        {
          Branch parent = node.Parent;
          Leaf.Node list = leaf.Head;
          int child = this.DetermineChild(parent, ms);
          this.PluckLeaf(parent, child);
          Branch growth = this.GrowBranch(parent, leaf.Min, leaf.Max, child);

          while (list != null)
          {
            M[] child_ms;
            this._locate(list.Value, out child_ms);

            if (child_ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            // NEED BOUNDS CHECKING HERE

            Add(list.Value, growth, child_ms, depth);
            list = list.Next;
          }

          Add(addition, growth, ms, depth);
          return;
        }
      }
      else
      {
        Branch branch = node as Branch;
        int child = this.DetermineChild(branch, ms);
        Node child_node = branch[child];
        if (child_node == null)
        {
          Leaf leaf = GrowLeaf(branch, child);
          leaf.Add(addition);
          return;
        }
        Add(addition, child_node, ms, depth + 1);
        return;
      }
    }

    /// <summary>Removes all the items in a given space.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    public void Remove(M[] min, M[] max)
    {
      throw new System.NotImplementedException("In Development...");

      // POSSIBLE MATH SLOWDOWN - SPEED TESTING REQUIRED FOR REMOVING LARGE QUANTITIES
      while (this._loadSquared > _count)
      {
        this._load--;
        this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
        this._loadSquared = this._load * this._load;
      }
    }

    /// <summary>Removes all the items in a given space where equality is met.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    /// <param name="where">The constraint of the removal.</param>
    public void Remove(M[] min, M[] max, Predicate<T> where)
    {
      throw new System.NotImplementedException("In Development...");

      // POSSIBLE MATH SLOWDOWN - SPEED TESTING REQUIRED FOR REMOVING LARGE QUANTITIES
      while (this._loadSquared > _count)
      {
        this._load--;
        this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
        this._loadSquared = this._load * this._load;
      }
    }

    /// <summary>Grows a branch on the tree at the desired location</summary>
    /// <param name="branch">The branch to grow a branch on.</param>
    /// <param name="min">The minimum dimensions of the new branch.</param>
    /// <param name="max">The maximum dimensions of the new branch.</param>
    /// <param name="child">The child index to grow the branch on.</param>
    /// <returns>The newly constructed branch.</returns>
    private Branch GrowBranch(Branch branch, M[] min, M[] max, int child)
    {
      return (branch.Head = 
        new Branch.Node(child, 
          new Branch(min, max, branch), branch.Head)).Value as Branch;
    }

    /// <summary>Grows a leaf on the tree at the desired location.</summary>
    /// <param name="branch">The branch to grow a leaf on.</param>
    /// <param name="child">The index to grow a leaf on.</param>
    /// <returns>The constructed leaf.</returns>
    private Leaf GrowLeaf(Branch branch, int child)
    {
      M[] min, max;
      this.DetermineChildBounds(branch, child, out min, out max);
      return (branch.Head =
        new Branch.Node(child,
          new Leaf(min, max, branch, this._load), branch.Head)).Value as Leaf;
    }

    /// <summary>Computes the child index that contains the desired dimensions.</summary>
    /// <param name="node">The node to compute the child index of.</param>
    /// <param name="ms">The coordinates to find the child index of.</param>
    /// <returns>The computed child index based on the coordinates relative to the center of the node.</returns>
    private int DetermineChild(Node node, M[] ms)
    {
      int child = 0;
      M[] min = node.Min;
      M[] max = node.Max;
      for (int i = 0; i < this._dimensions; i++)
        if (!(_compare(ms[i], _average(min[i], max[i])) == Comparison.Less))
          child += 1 << i;
      return child;
    }

    /// <summary>Determins the dimensions of the child at the given index.</summary>
    /// <param name="node">The parent of the node to compute dimensions for.</param>
    /// <param name="child">The index of the child to compute dimensions for.</param>
    /// <param name="min">The computed minimum dimensions of the child node.</param>
    /// <param name="max">The computed maximum dimensions of the child node.</param>
    private void DetermineChildBounds(Node node, int child, out M[] min, out M[] max)
    {
      min = new M[this._dimensions];
      max = new M[this._dimensions];
      for (int i = _dimensions - 1; i >= 0; i--)
      {
        int temp = 1 << i;
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
    }

    /// <summary>Checks a node for inclusion of a specific space.</summary>
    /// <param name="node">The node to check for inclusion with the space.</param>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    /// <returns>True if the node includes the space; False if not.</returns>
    private bool InclusionCheck(Node node, M[] min, M[] max)
    {
      M[] node_min = node.Min;
      M[] node_max = node.Max;
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(node_max[j], min[j]) == Comparison.Less ||
          this._compare(node_min[j], max[j]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Checks a node for inclusion of specific coordinates.</summary>
    /// <param name="node">The node to check for inclusion with the coordinates.</param>
    /// <param name="ms">The coordinates to check for inclusion with the node.</param>
    /// <returns>True if the node includes the coordinates; False if not.</returns>
    private bool InclusionCheck(Node node, M[] ms)
    {
      M[] node_min = node.Min;
      M[] node_max = node.Max;
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(ms[j], node_min[j]) == Comparison.Less ||
          this._compare(ms[j], node_max[j]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Checks a space for inclusion of specific coordinates.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    /// <param name="ms">The coordinates to check for inclusion with the node.</param>
    /// <returns>True if the space includes the coordinates; False if not.</returns>
    private bool InclusionCheck(M[] min, M[] max, M[] ms)
    {
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(ms[j], min[j]) == Comparison.Less ||
          this._compare(ms[j], max[j]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Plucks (removes) a leaf, and recursively chops empty branches.</summary>
    /// <param name="branch">The banch to pluck the leaf from.</param>
    /// <param name="child">The index of the leaf to pluck.</param>
    private void PluckLeaf(Branch branch, int child)
    {
      Branch.Node list = branch.Head;
      if (list.Index == child)
        branch.Head = list.Next;
      else
        while (list != null)
          if (list.Next.Index == child)
            list.Next = list.Next.Next;
    }

    /// <summary>Chops (removes) a branch.</summary>
    /// <param name="branch">The parent of the branch to chop.</param>
    /// <param name="child">The index of the branch to chop.</param>
    private void ChopBranch(Branch branch, int child)
    {
      throw new System.NotImplementedException();
      //branch.Children[child] = null;
    }

    /// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
    public void Update()
    {
      throw new System.NotImplementedException("Sorry, I'm still working on the update function.");
    }

    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
    /// <param name="min">The minimum dimensions of the space to update.</param>
    /// <param name="max">The maximum dimensions of the space to update.</param>
    public void Update(M[] min, M[] max)
    {
      throw new System.NotImplementedException("Sorry, I'm still working on the update function.");
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
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
          Leaf.Node list = (node as Leaf).Head;
          while (list != null)
          {
            function(list.Value);
            list = list.Next;
          }
        }
        else
        {
          Branch.Node list = (node as Branch).Head;
          while (list != null)
          {
            Foreach(function, list.Value);
            list = list.Next;
          }
        }
      }
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
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
          Leaf.Node list = (node as Leaf).Head;
          while (list != null)
          {
            T temp = list.Value;
            function(ref temp);
            list.Value = temp;
            list = list.Next;
          }
        }
        else
        {
          Branch.Node list = (node as Branch).Head;
          while (list != null)
          {
            Foreach(function, list.Value);
            list = list.Next;
          }
        }
      }
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
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
          Leaf.Node list = (node as Leaf).Head;
          while (list != null)
          {
            if (function(list.Value) == ForeachStatus.Break)
              return ForeachStatus.Break;
            list = list.Next;
          }
        }
        else
        {
          Branch.Node list = (node as Branch).Head;
          while (list != null)
          {
            if (Foreach(function, list.Value) == ForeachStatus.Break)
              return ForeachStatus.Break;
            list = list.Next;
          }
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
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
          Leaf.Node list = (node as Leaf).Head;
          while (list != null)
          {
            T temp = list.Value;
            ForeachStatus status;
            status = function(ref temp);
            list.Value = temp;
            if (status == ForeachStatus.Break)
              return ForeachStatus.Break;
            list = list.Next;
          }
        }
        else
        {
          Branch.Node list = (node as Branch).Head;
          while (list != null)
          {
            if (Foreach(function, list.Value) == ForeachStatus.Break)
              return ForeachStatus.Break;
            list = list.Next;
          }
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    public void Foreach(Foreach<T> function, M[] min, M[] max)
    {
      Foreach(function, _top, min, max);
    }
    private void Foreach(Foreach<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          Leaf.Node list = (node as Leaf).Head;
          while (list != null)
          {
            M[] ms;
            this._locate(list.Value, out ms);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              function(list.Value);

            list = list.Next;
          }
        }
        else
        {
          Branch.Node list = (node as Branch).Head;
          while (list != null)
          {
            if (InclusionCheck(list.Value, min, max))
              Foreach(function, list.Value, min, max);
            list = list.Next;
          }
        }
      }
    }

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    public void Foreach(ForeachRef<T> function, M[] min, M[] max)
    {
      Foreach(function, _top, min, max);
    }
    private void Foreach(ForeachRef<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          Leaf.Node list = (node as Leaf).Head;
          while (list != null)
          {
            M[] ms;
            this._locate(list.Value, out ms);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            T temp = list.Value;
            if (InclusionCheck(min, max, ms))
              function(ref temp);
            list.Value = temp;

            list = list.Next;
          }
        }
        else
        {
          Branch.Node list = (node as Branch).Head;
          while (list != null)
          {
            if (InclusionCheck(list.Value, min, max))
              Foreach(function, list.Value, min, max);
            list = list.Next;
          }
        }
      }
    }

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    public ForeachStatus Foreach(ForeachBreak<T> function, M[] min, M[] max)
    {
      return Foreach(function, _top, min, max);
    }
    private ForeachStatus Foreach(ForeachBreak<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          Leaf.Node list = (node as Leaf).Head;
          while (list != null)
          {
            M[] ms;
            this._locate(list.Value, out ms);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              if (function(list.Value) == ForeachStatus.Break)
                return ForeachStatus.Break;

            list = list.Next;
          }
        }
        else
        {
          Branch.Node list = (node as Branch).Head;
          while (list != null)
          {
            if (InclusionCheck(list.Value, min, max))
              if (Foreach(function, list.Value, min, max) == ForeachStatus.Break)
                return ForeachStatus.Break;
            list = list.Next;
          }
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    public ForeachStatus Foreach(ForeachRefBreak<T> function, M[] min, M[] max)
    {
      return Foreach(function, _top, min, max);
    }
    private ForeachStatus Foreach(ForeachRefBreak<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          Leaf.Node list = (node as Leaf).Head;
          while (list != null)
          {
            M[] ms;
            this._locate(list.Value, out ms);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
            {
              T temp = list.Value;
              ForeachStatus status;
              status = function(ref temp);
              list.Value = temp;
              if (status == ForeachStatus.Break)
                return ForeachStatus.Break;
            }

            list = list.Next;
          }
        }
        else
        {
          Branch.Node list = (node as Branch).Head;
          while (list != null)
          {
            if (InclusionCheck(list.Value, min, max))
              if (Foreach(function, list.Value, min, max) == ForeachStatus.Break)
                return ForeachStatus.Break;
            list = list.Next;
          }
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
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

    /// <summary>Puts all the items on this tree into an array.</summary>
    /// <returns>The array containing all the items within the tree.</returns>
    public T[] ToArray()
    {
      int index = 0;
      T[] array = new T[this._count];
      this.Foreach((T entry) => { array[index++] = entry; });
      return array;
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<T> Clone()
    {
      // OPTIMIZATION NEEDED
      Omnitree_Linked<T, M> clone = new Omnitree_Linked<T, M>(
        this._top.Min, this._top.Max,
        this._locate,
        this._compare,
        this._average);
      this.Foreach((T current) => { clone.Add(current); });
      return clone;
    }

    /// <summary>Returns the tree to an empty state.</summary>
    public void Clear()
    {
      this._load = _defaultLoad;
      this._top = new Leaf(this._top.Min, this._top.Max, null, _load);
      this._count = 0;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
    }

    /// <summary>This is used for throwing OcTree exceptions only to make debugging faster.</summary>
    private class Error : Structure.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  /// <summary>The one data structure to rule them all. Made by Zachary Patten.</summary>
  /// <typeparam name="T">The generice type of items to be stored in this octree.</typeparam>
  /// <typeparam name="M">The type of the axis dimensions to sort the "T" values upon.</typeparam>
  [System.Serializable]
  public class Omnitree_Array<T, M> : Omnitree<T, M>
  {
    /// <summary>Can be a leaf or a branch.</summary>
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

    /// <summary>A branch in the tree. Only contains items.</summary>
    private class Leaf : Node
    {
      private T[] _contents;
      private int _count;

      public T[] Contents { get { return this._contents; } }
      public int Count { get { return this._count; } set { this._count = value; } }
      public bool IsFull { get { return this._count == this._contents.Length; } }

      public Leaf(M[] min, M[] max, Branch parent, int loadFactor)
        : base(min, max, parent)
      { _contents = new T[loadFactor]; }

      public void Add(T addition)
      {
        this._contents[this._count++] = addition;
      }
    }

    /// <summary>A branch in the tree. Only contains nodes.</summary>
    private class Branch : Node
    {
      public Node[] _children;

      public Node[] Children { get { return this._children; } }

      public bool IsEmpty
      {
        get
        {
          foreach (Node child in this._children)
            if (child != null)
              return false;
          return true;
        }
      }

      public Branch(M[] min, M[] max, Branch parent, int children)
        : base(min, max, parent)
      {
        this._children = new Node[children];
      }
    }

    /// <summary>Locates an item along the given dimensions.</summary>
    /// <param name="item">The item to be located.</param>
    /// <returns>The computed locations of the item.</returns>
    public delegate M[] Locate_NonOut(T item);

    /// <summary>Locates an item along the given dimensions.</summary>
    /// <param name="item">The item to be located.</param>
    /// <param name="ms">The computed locations of the item.</param>
    public delegate void Locate(T item, out M[] ms);

    /// <summary>Computes the average between two items.</summary>
    /// <param name="left">The first item of the average.</param>
    /// <param name="right">The second item of the average.</param>
    /// <returns>The computed average between the two items.</returns>
    public delegate M Average(M left, M right);

    // Constants
    private const int _maxDimensions = 20;
    private const int _defaultLoad = 7;

    // Immutable Fields
    private Locate _locate;
    private Average _average;
    private Compare<M> _compare;
    private int _dimensions;
    private int _children;

    // Mutable Fields
    private Node _top;
    private int _count;
    private int _load;
    private int _loadPlusOneSquared;
    private int _loadSquared;

    /// <summary>The number of dimensions in this tree.</summary>
    public int Dimensions { get { return this._dimensions; } }

    /// <summary>The current number of items in the tree.</summary>
    public int Count { get { return this._count; } }

    /// <summary>True if (Count == 0).</summary>
    public bool IsEmpty { get { return this._count == 0; } }

    /// <summary>Gets the current memory allocation size of this structure.</summary>
    public int SizeOf { get { throw new System.NotImplementedException("Sorry, I'm working on it."); } }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    public Omnitree_Array(
      M[] min, M[] max,
      Locate locate,
      Compare<M> compare,
      Average average)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions.");

      if (min.Length > _maxDimensions)
        throw new Error("you are sorting on +" + _maxDimensions +
          " dimensions. if wish to do this, remove this exception at your own risk.");

      this._locate = locate;
      this._average = average;
      this._compare = compare;

      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;
      this._load = _defaultLoad;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
      this._count = 0;
      this._top = new Leaf(min, max, null, _load);
    }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    public Omnitree_Array(
      M[] min, M[] max,
      Locate_NonOut locate,
      Compare<M> compare,
      Average average)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");

      if (min.Length > _maxDimensions)
        throw new Error("you are sorting on +" + _maxDimensions +
          " dimensions. if wish to do this, remove this exception at your own risk.");

      // This is just an adapter, JIT will optimize
      this._locate = (T item, out M[] ms) => { ms = locate(item); };
      this._average = average;
      this._compare = compare;

      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;
      this._load = _defaultLoad;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
      this._count = 0;
      this._top = new Leaf(min, max, null, _load);
    }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    /// <param name="load">The initial load (slight optimization for large populations).</param>
    public Omnitree_Array(
      M[] min, M[] max,
      Locate locate,
      Compare<M> compare,
      Average average,
      int load)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");

      if (min.Length > _maxDimensions)
        throw new Error("you are sorting on +" + _maxDimensions +
          " dimensions. if wish to do this, remove this exception at your own risk.");

      // This is just an adapter, JIT will optimize
      this._locate = locate;
      this._average = average;
      this._compare = compare;

      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;
      this._load = load;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
      this._count = 0;
      this._top = new Leaf(min, max, null, _load);
    }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    /// <param name="load">The initial load (slight optimization for large populations).</param>
    public Omnitree_Array(
      M[] min, M[] max,
      Locate_NonOut locate,
      Compare<M> compare,
      Average average,
      int load)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");

      if (min.Length > _maxDimensions)
        throw new Error("you are sorting on +" + _maxDimensions +
          " dimensions. if wish to do this, remove this exception at your own risk.");

      // This is just an adapter, JIT will optimize
      this._locate = (T item, out M[] ms) => { ms = locate(item); };
      this._average = average;
      this._compare = compare;

      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;
      this._load = load;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
      this._count = 0;
      this._top = new Leaf(min, max, null, _load);
    }

    /// <summary>Adds an item to the tree.</summary>
    /// <param name="addition">The item to be added.</param>
    public void Add(T addition)
    {
      if (this._count == int.MaxValue)
        throw new Error("(Count == int.MaxValue) switch ints to longs in source code.");

      if (this._loadPlusOneSquared < _count)
      {
        this._load++;
        this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
        this._loadSquared = this._load * this._load;
      }

      M[] ms;
      this._locate(addition, out ms);

      if (ms.Length != this._dimensions)
        throw new Error("the location function for omnitree is invalid.");

      if (!InclusionCheck(this._top, ms))
        throw new Error("out of bounds during addition");

      if (this._top is Leaf && (this._top as Leaf).IsFull)
      {
        int count = (this._top as Leaf).Count;
        T[] contents = (this._top as Leaf).Contents;
        _top = new Branch(_top.Min, _top.Max, null, this._children);

        for (int i = 0; i < count; i++)
        {
          M[] child_ms;
          this._locate(contents[i], out child_ms);

          if (child_ms.Length != this._dimensions)
            throw new Error("the location function for omnitree is invalid.");

          // NEED BOUNDS CHECKING HERE

          Add(contents[i], this._top, child_ms);
        }
      }

      this.Add(addition, _top, ms);
      this._count++;
    }

    /// <summary>Recursive version of the add function.</summary>
    /// <param name="addition">The item to be added.</param>
    /// <param name="node">The current location of the tree.</param>
    /// <param name="ms">The location of the addition.</param>
    private void Add(T addition, Node node, M[] ms)
    {
      if (node is Leaf)
      {
        Leaf leaf = node as Leaf;
        if (!leaf.IsFull)
        {
          leaf.Add(addition);
          return;
        }
        else
        {
          Branch parent = node.Parent;
          T[] contents = (node as Leaf).Contents;
          Branch growth = GrowBranch(parent, leaf.Min, leaf.Max, this.DetermineChild(parent, ms));

          foreach (T item in contents)
          {
            M[] child_ms;
            this._locate(item, out child_ms);

            if (child_ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            // NEED BOUNDS CHECKING HERE

            Add(item, growth, child_ms);
          }

          Add(addition, growth, ms);
          return;
        }
      }
      else
      {
        Branch branch = node as Branch;
        int child = this.DetermineChild(branch, ms);
        if (branch.Children[child] == null)
        {
          Leaf leaf = GrowLeaf(branch, child);
          leaf.Add(addition);
          return;
        }
        Add(addition, branch.Children[child], ms);
        return;
      }
    }

    /// <summary>Removes all the items in a given space.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    public void Remove(M[] min, M[] max)
    {
      throw new System.NotImplementedException("In Development...");

      // POSSIBLE MATH SLOWDOWN - SPEED TESTING REQUIRED FOR REMOVING LARGE QUANTITIES
      while (this._loadSquared > _count)
      {
        this._load--;
        this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
        this._loadSquared = this._load * this._load;
      }
    }

    /// <summary>Removes all the items in a given space where equality is met.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    /// <param name="where">The equality constraint of the removal.</param>
    public void Remove(M[] min, M[] max, Predicate<T> where)
    {
      throw new System.NotImplementedException("In Development...");

      // POSSIBLE MATH SLOWDOWN - SPEED TESTING REQUIRED FOR REMOVING LARGE QUANTITIES
      while (this._loadSquared > _count)
      {
        this._load--;
        this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
        this._loadSquared = this._load * this._load;
      }
    }

    /// <summary>Grows a branch on the tree at the desired location</summary>
    /// <param name="branch">The branch to grow a branch on.</param>
    /// <param name="min">The minimum dimensions of the new branch.</param>
    /// <param name="max">The maximum dimensions of the new branch.</param>
    /// <param name="child">The child index to grow the branch on.</param>
    /// <returns>The newly constructed branch.</returns>
    private Branch GrowBranch(Branch branch, M[] min, M[] max, int child)
    {
      branch.Children[child] = new Branch(min, max, branch, this._children);
      return branch.Children[child] as Branch;
    }

    /// <summary>Grows a leaf on the tree at the desired location.</summary>
    /// <param name="branch">The branch to grow a leaf on.</param>
    /// <param name="child">The index to grow a leaf on.</param>
    /// <returns>The constructed leaf.</returns>
    private Leaf GrowLeaf(Branch branch, int child)
    {
      M[] min, max;
      this.DetermineChildBounds(branch, child, out min, out max);
      branch.Children[child] = new Leaf(min, max, branch, _load);
      return branch.Children[child] as Leaf;
    }

    /// <summary>Computes the child index that contains the desired dimensions.</summary>
    /// <param name="node">The node to compute the child index of.</param>
    /// <param name="ms">The coordinates to find the child index of.</param>
    /// <returns>The computed child index based on the coordinates relative to the center of the node.</returns>
    private int DetermineChild(Node node, M[] ms)
    {
      int child = 0;
      for (int i = 0; i < this._dimensions; i++)
        if (!(_compare(ms[i], _average(node.Min[i], node.Max[i])) == Comparison.Less))
          child += 1 << i;
      return child;
    }

    /// <summary>Determins the dimensions of the child at the given index.</summary>
    /// <param name="node">The parent of the node to compute dimensions for.</param>
    /// <param name="child">The index of the child to compute dimensions for.</param>
    /// <param name="min">The computed minimum dimensions of the child node.</param>
    /// <param name="max">The computed maximum dimensions of the child node.</param>
    private void DetermineChildBounds(Node node, int child, out M[] min, out M[] max)
    {
      min = new M[this._dimensions];
      max = new M[this._dimensions];
      M[] node_min = node.Min;
      M[] node_max = node.Max;
      for (int i = _dimensions - 1; i >= 0; i--)
      {
        int temp = 1 << i;
        if (child >= temp)
        {
          min[i] = this._average(node.Min[i], node.Max[i]);
          max[i] = node_max[i];
          child -= temp;
        }
        else
        {
          min[i] = node_min[i];
          max[i] = this._average(node.Min[i], node.Max[i]);
        }
      }
    }

    /// <summary>Checks a node for inclusion of a specific space.</summary>
    /// <param name="node">The node to check for inclusion with the space.</param>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    /// <returns>True if the node includes the space; False if not.</returns>
    private bool InclusionCheck(Node node, M[] min, M[] max)
    {
      M[] node_min = node.Min; M[] node_max = node.Max;
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(node_max[j], min[j]) == Comparison.Less ||
          this._compare(node_min[j], max[j]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Checks a node for inclusion of specific coordinates.</summary>
    /// <param name="node">The node to check for inclusion with the coordinates.</param>
    /// <param name="ms">The coordinates to check for inclusion with the node.</param>
    /// <returns>True if the node includes the coordinates; False if not.</returns>
    private bool InclusionCheck(Node node, M[] ms)
    {
      M[] node_min = node.Min; M[] node_max = node.Max;
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(ms[j], node_min[j]) == Comparison.Less ||
          this._compare(ms[j], node_max[j]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Checks a space for inclusion of specific coordinates.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    /// <param name="ms">The coordinates to check for inclusion with the node.</param>
    /// <returns>True if the space includes the coordinates; False if not.</returns>
    private bool InclusionCheck(M[] min, M[] max, M[] ms)
    {
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(ms[j], min[j]) == Comparison.Less ||
          this._compare(ms[j], max[j]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Plucks (removes) a leaf, and recursively chops empty branches.</summary>
    /// <param name="branch">The banch to pluck the leaf from.</param>
    /// <param name="child">The index of the leaf to pluck.</param>
    private void PluckLeaf(Branch branch, int child)
    {
      branch.Children[child] = null;
      while (branch.IsEmpty)
      {
        ChopBranch(branch.Parent, this.DetermineChild(branch.Parent, branch.Min));
        branch = branch.Parent;
      }
    }

    /// <summary>Chops (removes) a branch.</summary>
    /// <param name="branch">The parent of the branch to chop.</param>
    /// <param name="child">The index of the branch to chop.</param>
    private void ChopBranch(Branch branch, int child)
    {
      branch.Children[child] = null;
    }

    /// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
    public void Update()
    {
      throw new System.NotImplementedException("Sorry, I'm still working on the update function.");
    }

    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
    /// <param name="min">The minimum dimensions of the space to update.</param>
    /// <param name="max">The maximum dimensions of the space to update.</param>
    public void Update(M[] min, M[] max)
    {
      throw new System.NotImplementedException("Sorry, I'm still working on the update function.");
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
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
          int count = (node as Leaf).Count;
          T[] items = (node as Leaf).Contents;
          for (int i = 0; i < count; i++)
            function(items[i]);
        }
        else
        {
          Branch branch = node as Branch;
          for (int i = 0; i < branch.Children.Length; i++)
            Foreach(function, branch.Children[i]);
        }
      }
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
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
          int count = (node as Leaf).Count;
          T[] items = (node as Leaf).Contents;
          for (int i = 0; i < count; i++)
            function(ref items[i]);
        }
        else
        {
          Branch branch = node as Branch;
          for (int i = 0; i < branch.Children.Length; i++)
            Foreach(function, branch.Children[i]);
        }
      }
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
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
          int count = (node as Leaf).Count;
          T[] items = (node as Leaf).Contents;
          for (int i = 0; i < count; i++)
            if (function(items[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
        else
        {
          Branch branch = node as Branch;
          for (int i = 0; i < branch.Children.Length; i++)
            if (Foreach(function, branch.Children[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
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
          int count = (node as Leaf).Count;
          T[] items = (node as Leaf).Contents;
          for (int i = 0; i < count; i++)
            if (function(ref items[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
        else
        {
          Branch branch = node as Branch;
          for (int i = 0; i < branch.Children.Length; i++)
            if (Foreach(function, branch.Children[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
          return ForeachStatus.Continue;
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    public void Foreach(Foreach<T> function, M[] min, M[] max)
    {
      Foreach(function, _top, min, max);
    }
    private void Foreach(Foreach<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = (node as Leaf).Count;
          T[] items = (node as Leaf).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms;
            this._locate(items[i], out ms);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              function(items[i]);
          }
        }
        else
        {
          Node[] children = (node as Branch).Children;
          for (int i = 0; i < children.Length; i++)
            if (children[i] != null && InclusionCheck(children[i], min, max))
              Foreach(function, children[i], min, max);
        }
      }
    }

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    public void Foreach(ForeachRef<T> function, M[] min, M[] max)
    {
      Foreach(function, _top, min, max);
    }
    private void Foreach(ForeachRef<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = (node as Leaf).Count;
          T[] items = (node as Leaf).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms;
            this._locate(items[i], out ms);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              function(ref items[i]);
          }
        }
        else
        {
          Node[] children = (node as Branch).Children;
          for (int i = 0; i < children.Length; i++)
            if (children[i] != null && InclusionCheck(children[i], min, max))
              Foreach(function, children[i], min, max);
        }
      }
    }

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    public ForeachStatus Foreach(ForeachBreak<T> function, M[] min, M[] max)
    {
      return Foreach(function, _top, min, max);
    }
    private ForeachStatus Foreach(ForeachBreak<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = (node as Leaf).Count;
          T[] items = (node as Leaf).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms;
            this._locate(items[i], out ms);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              if (function(items[i]) == ForeachStatus.Break)
                return ForeachStatus.Break;
          }
        }
        else
        {
          Node[] children = (node as Branch).Children;
          for (int i = 0; i < children.Length; i++)
            if (children[i] != null && InclusionCheck(children[i], min, max))
              if (Foreach(function, children[i], min, max) == ForeachStatus.Break)
                return ForeachStatus.Break;
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    /// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    /// <param name="min">The minimum dimensions of the traversal.</param>
    /// <param name="max">The maximum dimensions of the traversal.</param>
    public ForeachStatus Foreach(ForeachRefBreak<T> function, M[] min, M[] max)
    {
      return Foreach(function, _top, min, max);
    }
    private ForeachStatus Foreach(ForeachRefBreak<T> function, Node node, M[] min, M[] max)
    {
      if (node != null)
      {
        if (node is Leaf)
        {
          int count = (node as Leaf).Count;
          T[] items = (node as Leaf).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms;
            this._locate(items[i], out ms);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              if (function(ref items[i]) == ForeachStatus.Break)
                return ForeachStatus.Break;
          }
        }
        else
        {
          Node[] children = (node as Branch).Children;
          for (int i = 0; i < children.Length; i++)
            if (children[i] != null && InclusionCheck(children[i], min, max))
              if (Foreach(function, children[i], min, max) == ForeachStatus.Break)
                return ForeachStatus.Break;
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
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

    /// <summary>Puts all the items on this tree into an array.</summary>
    /// <returns>The array containing all the items within the tree.</returns>
    public T[] ToArray()
    {
      int index = 0;
      T[] array = new T[this._count];
      this.Foreach((T entry) => { array[index++] = entry; });
      return array;
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<T> Clone()
    {
      // OPTIMIZATION NEEDED
      Omnitree_Array<T, M> clone = new Omnitree_Array<T, M>(
        this._top.Min, this._top.Max,
        this._locate,
        this._compare,
        this._average);
      this.Foreach((T current) => { clone.Add(current); });
      return clone;
    }

    /// <summary>Returns the tree to an empty state.</summary>
    public void Clear()
    {
      this._load = _defaultLoad;
      this._top = new Leaf(this._top.Min, this._top.Max, null, _load);
      this._count = 0;
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
    }

    /// <summary>This is used for throwing OcTree exceptions only to make debugging faster.</summary>
    private class Error : Structure.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}
