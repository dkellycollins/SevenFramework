// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Structures
{
	/// <summary>Sorts items along N dimensions. The one data structure to rule them all. Made by Zachary Patten.</summary>
  /// <typeparam name="T">The generice type of items to be stored in this omnitree.</typeparam>
  /// <typeparam name="M">The type of the axis dimensions to sort the "T" values upon.</typeparam>
  public interface Omnitree<T, M> : Structure<T>
  {
		/// <summary>Event for handling items that go outside the bounds of the Omnitree.</summary>
		event Foreach<T> HandleOutOfBounds;

		/// <summary>Gets the dimensions of the center point of the Omnitree.</summary>
		M[] Origin { get; }

    /// <summary>The minimum dimensions of the Omnitree.</summary>
    M[] Min { get; }

    /// <summary>The maximum dimensions of the Omnitree.</summary>
    M[] Max { get; }

    /// <summary>The compare function the Omnitree is using.</summary>
    Compare<M> Compare { get; }

    /// <summary>The location function the Omnitree is using.</summary>
    Omnitree.Locate<T, M> Locate { get; }

    /// <summary>The average function the Omnitree is using.</summary>
    Omnitree.Average<M> Average { get; }

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

  /// <summary>Extension class for the Omnitree interface.</summary>
  public static class Omnitree
  {
    /// <summary>Locates an item along the given dimensions.</summary>
    /// <typeparam name="T">The type of the item to locate.</typeparam>
    /// <typeparam name="M">The type of axis type of the Omnitree.</typeparam>
    /// <param name="item">The item to be located.</param>
    /// <returns>The computed locations of the item.</returns>
    public delegate M[] Locate_NonOut<T, M>(T item);

    /// <summary>Locates an item along the given dimensions.</summary>
    /// <typeparam name="T">The type of the item to locate.</typeparam>
    /// <typeparam name="M">The type of axis type of the Omnitree.</typeparam>
    /// <param name="item">The item to be located.</param>
    /// <param name="ms">The computed locations of the item.</param>
    public delegate void Locate<T, M>(T item, out M[] ms);

    /// <summary>Computes the average between two items.</summary>
    /// <typeparam name="M">The type of items to average.</typeparam>
    /// <param name="left">The first item of the average.</param>
    /// <param name="right">The second item of the average.</param>
    /// <returns>The computed average between the two items.</returns>
    public delegate M Average<M>(M left, M right);

		/// <summary>Polymorphism base for Omnitree exceptions.</summary>
		public class Error : Structure.Error
		{
			/// <summary>Constructs an Omnitree exception.</summary>
			/// <param name="message">The message of the exception.</param>
			public Error(string message) : base(message) { }
		}
  }

	/// <summary>Sorts items along N dimensions. The one data structure to rule them all. Made by Zachary Patten.</summary>
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

      public Node Head { get { return this._head; } set { this._head = value; } }
      public int Count { get { return this._count; } set { this._count = value; } }

      public Leaf(M[] min, M[] max, Branch parent, int load)
        : base(min, max, parent) { }
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
          // Needs an AVL-Tree

          Branch.Node list = _head;
          while (list != null)
            if (list.Index == index)
              return list.Value;
            else
              list = list.Next;
          return null;
        }
      }

      public Branch(M[] min, M[] max, Branch parent)
        : base(min, max, parent) { }
    }

    // Constants
    private const int _defaultLoad = 7;

    // Immutable Fields
    private Omnitree.Locate<T, M> _locate;
    private Omnitree.Average<M> _average;
    private Compare<M> _compare;
    private int _dimensions;
    private int _children;
    private M[] _origin;

    // Mutable Fields
    private int _depth;
    private Node _top;
    //private Node _previousAddition;
    //private int _previousAdditionDepth;
    private int _count;
    private int _load;
    private int _loadPlusOneSquared;
    private int _loadSquared;

    /// <summary>Event for handling items that go outside the bounds of the Omnitree.</summary>
    public event Foreach<T> HandleOutOfBounds;

    /// <summary>Gets the dimensions of the center point of the Omnitree.</summary>
    public M[] Origin { get { return (M[])this._origin.Clone(); } }

    /// <summary>The minimum dimensions of the Omnitree.</summary>
    public M[] Min { get { return (M[])this._top.Min.Clone(); } }

    /// <summary>The maximum dimensions of the Omnitree.</summary>
    public M[] Max { get { return (M[])this._top.Min.Clone(); } }

    /// <summary>The number of dimensions in this tree.</summary>
    public int Dimensions { get { return this._dimensions; } }

    /// <summary>The current number of items in the tree.</summary>
    public int Count { get { return this._count; } }

    /// <summary>True if (Count == 0).</summary>
    public bool IsEmpty { get { return this._count == 0; } }

    /// <summary>Gets the current memory allocation size of this structure.</summary>
    public int SizeOf { get { throw new System.NotImplementedException("Sorry, I'm working on it."); } }

    /// <summary>The current max depth in the Omnitree (only updated on additions).</summary>
    public int Depth { get { return this._depth; } }

    /// <summary>The compare function the Omnitree is using.</summary>
    public Compare<M> Compare { get { return this._compare; } }

    /// <summary>The location function the Omnitree is using.</summary>
    public Omnitree.Locate<T, M> Locate { get { return this._locate; } }

    /// <summary>The average function the Omnitree is using.</summary>
    public Omnitree.Average<M> Average { get { return this._average; } }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    public Omnitree_Linked(
      M[] min, M[] max,
      Omnitree.Locate<T, M> locate,
      Compare<M> compare,
      Omnitree.Average<M> average)
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
      Omnitree.Locate_NonOut<T, M> locate,
      Compare<M> compare,
      Omnitree.Average<M> average)
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
      Omnitree.Locate<T, M> locate,
      Compare<M> compare,
      Omnitree.Average<M> average,
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
      Omnitree.Locate_NonOut<T, M> locate,
      Compare<M> compare,
      Omnitree.Average<M> average,
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

      //if (this._previousAddition != null && InclusionCheck(this._previousAddition, ms))
      //  Add(addition, this._previousAddition, ms, this._previousAdditionDepth);
      //else
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
          leaf.Head = new Leaf.Node(addition, leaf.Head);
          leaf.Count++;
          //leaf.Add(addition);

          //_previousAddition = leaf;
          //_previousAdditionDepth = depth;
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
          leaf.Head = new Leaf.Node(addition, leaf.Head);
          leaf.Count++;
          //leaf.Add(addition);

          //_previousAddition = leaf;
          //_previousAdditionDepth = depth;
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
      for (int i = 0; i < this._dimensions; i++)
        if (!(this._compare(ms[i], this._average(node.Min[i], node.Max[i])) == Comparison.Less))
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
      for (int i = this._dimensions - 1; i >= 0; i--)
      {
        int temp = 1 << i;
        if (child >= temp)
        {
          min[i] = this._average(node.Min[i], node.Max[i]);
          max[i] = node.Max[i];
          child -= temp;
        }
        else
        {
          min[i] = node.Min[i];
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
      for (int i = 0; i < this._dimensions; i++)
        if (this._compare(node.Max[i], min[i]) == Comparison.Less ||
          this._compare(node.Min[i], max[i]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Checks a node for inclusion of specific coordinates.</summary>
    /// <param name="node">The node to check for inclusion with the coordinates.</param>
    /// <param name="ms">The coordinates to check for inclusion with the node.</param>
    /// <returns>True if the node includes the coordinates; False if not.</returns>
    private bool InclusionCheck(Node node, M[] ms)
    {
      for (int i = 0; i < this._dimensions; i++)
        if (this._compare(ms[i], node.Min[i]) == Comparison.Less ||
          this._compare(ms[i], node.Max[i]) == Comparison.Greater)
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
      for (int i = 0; i < this._dimensions; i++)
        if (this._compare(ms[i], min[i]) == Comparison.Less ||
          this._compare(ms[i], max[i]) == Comparison.Greater)
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

    /// <summary>Omnitree_Linked Exception.</summary>
    private class Error : Omnitree.Error
    {
      public Error(string message) : base(message) { }
    }

    ///// <summary>Can be a leaf or a branch.</summary>
    //private abstract class Node
    //{
    //  private M[] _min;
    //  private M[] _max;
    //  private Branch _parent;
    //  private int _index;
    //  private int _count;

    //  internal M[] Min { get { return this._min; } }
    //  internal M[] Max { get { return this._max; } }
    //  internal Branch Parent { get { return this._parent; } }
    //  internal int Index { get { return this._index; } }
    //  internal int Count { get { return this._count; } set { this._count = value; } }

    //  internal Node(M[] min, M[] max, Branch parent, int index)
    //  {
    //    this._min = min;
    //    this._max = max;
    //    this._parent = parent;
    //    this._index = index;
    //  }
    //}

    ///// <summary>A branch in the tree. Only contains items.</summary>
    //private class Leaf : Node
    //{
    //  internal class Node
    //  {
    //    private T _value;
    //    private Node _next;

    //    public T Value { get { return _value; } set { _value = value; } }
    //    public Node Next { get { return _next; } set { _next = value; } }

    //    public Node(T value, Node next)
    //    {
    //      _value = value;
    //      _next = next;
    //    }
    //  }

    //  private Node _head;
    //  private int _count;

    //  public Node Head { get { return this._head; } set { this._head = value; } }
    //  public int Count { get { return this._count; } set { this._count = value; } }

    //  public Leaf(M[] min, M[] max, Branch parent, int index)
    //    : base(min, max, parent, index) { }
    //}

    ///// <summary>A branch in the tree. Only contains nodes.</summary>
    //private class Branch : Node
    //{
    //  internal class Node
    //  {
    //    private Omnitree_Linked<T, M>.Node _value;
    //    private Branch.Node _next;

    //    public Omnitree_Linked<T, M>.Node Value { get { return this._value; } }
    //    public Branch.Node Next { get { return this._next; } set { this._next = value; } }

    //    public Node(Omnitree_Linked<T, M>.Node value, Branch.Node next)
    //    {
    //      this._value = value;
    //      this._next = next;
    //    }
    //  }

    //  private Branch.Node _head;

    //  public Branch.Node Head { get { return this._head; } set { this._head = value; } }
			
    //  public Branch(M[] min, M[] max, Branch parent, int index)
    //    : base(min, max, parent, index) { }
    //}

    //private const int _defaultLoad = 2;

    //private Omnitree.Locate<T, M> _locate;
    //private Omnitree.Average<M> _average;
    //private Compare<M> _compare;
    //private int _dimensions;
    //private int _children;
    //private M[] _origin;

    //private Node _top;
    //private int _count;
    ///// <summary>count ^ (1 / dimensions)</summary>
    //private int _load;
    ///// <summary>load ^ dimensions</summary>
    //private int _loadPowered;
    ///// <summary>(load + 1) ^ dimensions</summary>
    //private int _loadPlusOnePowered;
    ///// <summary>leaf depth of previous addition (sequencial addition optimization)</summary>
    //private Node _previousAddition;
    ///// <summary>tree depth of previous addition (sequencial addition optimization)</summary>
    //private int _previousAdditionDepth;

    ///// <summary>Event for handling items that go outside the bounds of the Omnitree.</summary>
    //public event Foreach<T> HandleOutOfBounds;

    ///// <summary>Gets the dimensions of the center point of the Omnitree.</summary>
    //public M[] Origin { get { return (M[])this._origin.Clone(); } }

    ///// <summary>The minimum dimensions of the Omnitree.</summary>
    //public M[] Min { get { return (M[])this._top.Min.Clone(); } }

    ///// <summary>The maximum dimensions of the Omnitree.</summary>
    //public M[] Max { get { return (M[])this._top.Max.Clone(); } }

    ///// <summary>The number of dimensions in this tree.</summary>
    //public int Dimensions { get { return this._dimensions; } }

    ///// <summary>The current number of items in the tree.</summary>
    //public int Count { get { return this._count; } }

    ///// <summary>True if (Count == 0).</summary>
    //public bool IsEmpty { get { return this._count == 0; } }

    ///// <summary>Gets the current memory allocation size of this structure.</summary>
    //public int SizeOf { get { throw new System.NotImplementedException("Sorry, I'm working on it."); } }

    ///// <summary>The compare function the Omnitree is using.</summary>
    //public Compare<M> Compare { get { return this._compare; } }

    ///// <summary>The location function the Omnitree is using.</summary>
    //public Omnitree.Locate<T, M> Locate { get { return this._locate; } }

    ///// <summary>The average function the Omnitree is using.</summary>
    //public Omnitree.Average<M> Average { get { return this._average; } }

    ///// <summary>Constructor for an Omnitree_Linked.</summary>
    ///// <param name="min">The minimum values of the tree.</param>
    ///// <param name="max">The maximum values of the tree.</param>
    ///// <param name="locate">A function for locating an item along the provided dimensions.</param>
    ///// <param name="compare">A function for comparing two items of the types of the axis.</param>
    ///// <param name="average">A function for computing the average between two items of the axis types.</param>
    //public Omnitree_Linked(
    //  M[] min, M[] max,
    //  Omnitree.Locate<T, M> locate,
    //  Compare<M> compare,
    //  Omnitree.Average<M> average)
    //{
    //  // check the locate and compare delegates
    //  if (locate == null)
    //    throw new Error("null reference on location delegate during Omnitree construction");
    //  if (compare == null)
    //    throw new Error("null reference on compare delegate during Omnitree construction");

    //  // Check the min/max values
    //  if (min == null)
    //    throw new Error("null reference on min dimensions during Omnitree construction");
    //  if (max == null)
    //    throw new Error("null reference on max dimensions during Omnitree construction");
    //  if (min.Length != max.Length)
    //    throw new Error("min/max values for omnitree mismatch dimensions");
    //  for (int i = 0; i < min.Length; i++)
    //    if (compare(min[i], max[i]) != Comparison.Less)
    //      throw new Error("invalid min/max values. not all min values are less than the max values");

    //  // Check the average delegate
    //  if (average == null)
    //    throw new Error("null reference on average delegate during Omnitree construction");
    //  M[] origin = new M[min.Length];
    //  for (int i = 0; i < min.Length; i++)
    //    origin[i] = average(min[i], max[i]);
    //  for (int i = 0; i < min.Length; i++)
    //    if (compare(min[i], origin[i]) != Comparison.Less || compare(origin[i], max[i]) != Comparison.Less)
    //      throw new Error("invalid average function. not all average values were computed to be between the min/max values.");
			
    //  this._locate = locate;
    //  this._average = average;
    //  this._compare = compare;
    //  this._dimensions = min.Length;
    //  this._children = 1 << this._dimensions;

    //  this._count = 0;
    //  this._top = new Leaf(min, max, null, -1);

    //  this._load = _defaultLoad;
    //  this._loadPlusOnePowered =
    //    Omnitree_Linked<T, M>.Int_Power(this._load + 1, this._dimensions);
    //  this._loadPowered =
    //    Omnitree_Linked<T, M>.Int_Power(_load, _dimensions);

    //  this._origin = origin;

    //  this._previousAddition = this._top;
    //  this._previousAdditionDepth = 0;
    //}

    ///// <summary>Constructor for an Omnitree_Linked.</summary>
    ///// <param name="min">The minimum values of the tree.</param>
    ///// <param name="max">The maximum values of the tree.</param>
    ///// <param name="locate">A function for locating an item along the provided dimensions.</param>
    ///// <param name="compare">A function for comparing two items of the types of the axis.</param>
    ///// <param name="average">A function for computing the average between two items of the axis types.</param>
    //public Omnitree_Linked(M[] min, M[] max, Omnitree.Locate_NonOut<T, M> locate, Compare<M> compare, Omnitree.Average<M> average)
    //  : this(min, max,
    //  // this is just an adapter - JIT should optimize
    //  (T item, out M[] ms) => { ms = locate(item); },
    //  compare, average)
    //{

    //}

    ///// <summary>Adds an item to the tree.</summary>
    ///// <param name="addition">The item to be added.</param>
    //public void Add(T addition)
    //{
    //  if (this._count == int.MaxValue)
    //    throw new Error("(Count == int.MaxValue) max Omnitree size reached (change ints to longs if you need to).");

    //  // dynamic tree sizes
    //  if (this._loadPlusOnePowered < this._count)
    //  {
    //    this._load++;
    //    this._loadPlusOnePowered =
    //      Omnitree_Linked<T, M>.Int_Power(this._load + 1, this._dimensions);
    //    this._loadPowered =
    //      Omnitree_Linked<T, M>.Int_Power(this._load, this._dimensions);
    //  }

    //  M[] ms;
    //  this._locate(addition, out ms);

    //  if (ms == null || ms.Length != this._dimensions)
    //    throw new Error("the location function for omnitree is invalid.");

    //  if (!EncapsulationCheck(this._top.Min, this._top.Max, ms))
    //    throw new Error("out of bounds during addition");

    //  if (this._top is Leaf && (this._top as Leaf).Count >= this._load)
    //  {
    //    Leaf leaf = this._top as Leaf;
    //    this._top = new Branch(this._top.Min, this._top.Max, null, -1);

    //    Leaf.Node list = leaf.Head;
    //    while (list != null)
    //    {
    //      M[] child_ms;
    //      this._locate(list.Value, out child_ms);

    //      if (child_ms == null || child_ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");

    //      if (!EncapsulationCheck(this._top.Min, this._top.Max, child_ms))
    //      {
    //        this._count--;
    //        if (HandleOutOfBounds == null)
    //          throw new Error("a node was updated to be out of bounds (found in an addition)");
    //        else
    //          this.HandleOutOfBounds(list.Value);
    //      }
    //      else
    //        Add(list.Value, this._top, child_ms, 0);
    //    }
    //  }

    //  if (this.EncapsulationCheck(this._previousAddition.Min, this._previousAddition.Max, ms))
    //    this.Add(addition, this._previousAddition, ms, this._previousAdditionDepth);
    //  else
    //    this.Add(addition, _top, ms, 0);
    //  this._count++;
    //}

    ///// <summary>Recursive version of the add function.</summary>
    ///// <param name="addition">The item to be added.</param>
    ///// <param name="node">The current location of the tree.</param>
    ///// <param name="ms">The location of the addition.</param>
    ///// <param name="depth">The current depth of iteration.</param>
    //private void Add(T addition, Node node, M[] ms, int depth)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    if (depth >= this._load || !(leaf.Count >= this._load))
    //    {
    //      Omnitree_Linked<T, M>.Leaf_Add(leaf, addition);

    //      this._previousAddition = leaf;
    //      this._previousAdditionDepth = depth;

    //      return;
    //    }
    //    else
    //    {
    //      Branch parent = node.Parent;
    //      Branch growth = GrowBranch(parent, leaf.Min, leaf.Max, this.DetermineChild(parent, ms));

    //      Leaf.Node list = leaf.Head;
    //      while (list != null)
    //      {
    //        M[] child_ms;
    //        this._locate(list.Value, out child_ms);

    //        if (child_ms == null || child_ms.Length != this._dimensions)
    //          throw new Error("the location function for omnitree is invalid.");

    //        if (EncapsulationCheck(growth.Min, growth.Max, child_ms))
    //          Add(list.Value, growth, child_ms, depth);
    //        else
    //        {
    //          if (EncapsulationCheck(this._top.Min, this._top.Max, child_ms))
    //          {
    //            this._count--;
    //            if (HandleOutOfBounds == null)
    //              throw new Error("a node was updated to be out of bounds (found in an addition)");
    //            else
    //              this.HandleOutOfBounds(list.Value);
    //          }
    //          else
    //            Add(list.Value, this._top, child_ms, depth);
    //        }
    //      }

    //      Add(addition, growth, ms, depth);
    //      return;
    //    }
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    int child = this.DetermineChild(branch, ms);
    //    Node child_node = this.Branch_GetChild(branch, child);

    //    // null children in branches are just empty leaves
    //    if (child_node == null)
    //    {
    //      Leaf leaf = GrowLeaf(branch, child);
    //      Omnitree_Linked<T, M>.Leaf_Add(leaf, addition);

    //      this._previousAddition = leaf;
    //      this._previousAdditionDepth = depth + 1;

    //      branch.Count++;
    //      return;
    //    }

    //    Add(addition, child_node, ms, depth + 1);
    //    branch.Count++;
    //    return;
    //  }
    //}

    ///// <summary>Adds an item to a leaf.</summary>
    ///// <param name="leaf">The leaf to add to.</param>
    ///// <param name="addition">The item to add to the leaf.</param>
    //private static void Leaf_Add(Leaf leaf, T addition)
    //{
    //  leaf.Head = new Leaf.Node(addition, leaf.Head);
    //  leaf.Count++;
    //}

    ///// <summary>Gets a child at of a given index in a branch.</summary>
    ///// <param name="branch">The branch to get the child of.</param>
    ///// <param name="index">The index of the child to get.</param>
    ///// <returns>The value of the child.</returns>
    //private Node Branch_GetChild(Branch branch, int index)
    //{
    //  Branch.Node child = branch.Head;
    //  while (child != null && child.Value.Index != index)
    //    child = child.Next;
    //  if (child == null)
    //    return null;
    //  else
    //    return child.Value;
    //}

    ///// <summary>Sets a child at of a given index in a branch.</summary>
    ///// <param name="branch">The branch to set the child of.</param>
    ///// <param name="index">The index of the child to set.</param>
    ///// <param name="value">The value to be set.</param>
    //private Node Branch_SetChild(Branch branch, int index, Node value)
    //{
    //  // leaves are completely new nodes (as oposed to branches replacing leaves)
    //  if (value is Leaf)
    //    branch.Head = new Branch.Node(value, branch.Head);
    //  else
    //  {
    //    if (branch.Head.Value.Index == index)
    //      branch.Head = branch.Head.Next;
    //    else
    //    {
    //      Branch.Node list = branch.Head;
    //      while (list.Next.Value.Index != index)
    //        list = list.Next;
    //      list.Next = list.Next.Next;
    //    }
    //  }
    //  return value;
    //}

    ///// <summary>Takes an integer to the power of the exponent.</summary>
    ///// <param name="_base">The base operand of the power function.</param>
    ///// <param name="exponent">The exponent operand of the power operand.</param>
    ///// <returns>The computed (bease ^ exponent) value.</returns>
    //private static int Int_Power(int _base, int exponent)
    //{
    //  int result = 1;
    //  while (exponent > 0)
    //  {
    //    if ((exponent & 1) > 0)
    //      result *= _base;
    //    exponent >>= 1;
    //    _base *= _base;
    //  }
    //  return result;
    //}

    ///// <summary>Removes all the items qualified by the delegate.</summary>
    ///// <param name="where">The predicate to qualify removals.</param>
    //public void Remove(Predicate<T> where)
    //{
    //  this._count -= this.Remove(this._top, where);

    //  // dynamic tree sizes
    //  while (this._loadPowered > this._count && this._load > _defaultLoad)
    //  {
    //    this._load--;
    //    this._loadPlusOnePowered =
    //      Omnitree_Linked<T, M>.Int_Power(_load + 1, _dimensions);
    //    this._loadPowered =
    //      Omnitree_Linked<T, M>.Int_Power(_load, _dimensions);
    //  }

    //  this._previousAddition = this._top;
    //  this._previousAdditionDepth = 0;
    //}

    ///// <summary>Recursive version of the remove method.</summary>
    ///// <param name="node">The current node of traversal.</param>
    ///// <param name="where">The predicate to qualify removals.</param>
    //private int Remove(Node node, Predicate<T> where)
    //{
    //  int removals = 0;
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    while (leaf.Head != null && where(leaf.Head.Value))
    //    {
    //      leaf.Head = leaf.Head.Next;
    //      removals++;
    //    }
    //    if (leaf.Head != null)
    //    {
    //      Leaf.Node list = leaf.Head;
    //      while (list.Next != null)
    //      {
    //        if (where(list.Next.Value))
    //        {
    //          list.Next = list.Next.Next;
    //          removals++;
    //        }
    //      }
    //    }

    //    leaf.Count -= removals;
    //    return removals;
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    Branch.Node list = branch.Head;

    //    while (list != null)
    //    {
    //      removals += this.Remove(list.Value, where);
    //      if (list.Value.Count == 0)
    //        ChopChild(branch, list.Value.Index);
    //      list = list.Next;
    //    }

    //    branch.Count -= removals;

    //    if (branch.Count < this._load && branch.Count != 0)
    //      ShrinkChild(branch.Parent, branch.Index);

    //    return removals;
    //  }
    //}

    ///// <summary>Removes all the items in a given space.</summary>
    ///// <param name="min">The minimum values of the space.</param>
    ///// <param name="max">The maximum values of the space.</param>
    ///// <returns>The number of items that were removed.</returns>
    //public void Remove(M[] min, M[] max)
    //{
    //  this._count -= this.Remove(this._top, min, max);

    //  // dynamic tree sizes
    //  while (this._loadPowered > this._count && this._load > _defaultLoad)
    //  {
    //    this._load--;
    //    this._loadPlusOnePowered =
    //      Omnitree_Linked<T, M>.Int_Power(_load + 1, _dimensions);
    //    this._loadPowered =
    //      Omnitree_Linked<T, M>.Int_Power(_load, _dimensions);
    //  }

    //  this._previousAddition = this._top;
    //  this._previousAdditionDepth = 0;
    //}

    ///// <summary>Recursive version of the remove method.</summary>
    ///// <param name="node">The current node of traversal.</param>
    ///// <param name="min">The min dimensions of the removal space.</param>
    ///// <param name="max">The max dimensions of the removal space.</param>
    //private int Remove(Node node, M[] min, M[] max)
    //{
    //  int removals = 0;
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    while (leaf.Head != null)
    //    {
    //      M[] ms;
    //      this._locate(leaf.Head.Value, out ms);

    //      if (ms == null || ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");

    //      if (EncapsulationCheck(min, max, ms))
    //      {
    //        leaf.Head = leaf.Head.Next;
    //        removals++;
    //      }
    //    }
    //    if (leaf.Head != null)
    //    {
    //      Leaf.Node list = leaf.Head;
    //      while (list.Next != null)
    //      {
    //        M[] ms;
    //        this._locate(list.Next.Value, out ms);

    //        if (ms == null || ms.Length != this._dimensions)
    //          throw new Error("the location function for omnitree is invalid.");

    //        if (EncapsulationCheck(min, max, ms))
    //        {
    //          list.Next = list.Next.Next;
    //          removals++;
    //        }
    //      }
    //    }

    //    leaf.Count -= removals;
    //    return removals;
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    while (branch.Head != null && EncapsulationCheck(min, max, branch.Head.Value.Min, branch.Head.Value.Max))
    //    {
    //      removals += branch.Head.Value.Count;
    //      branch.Head = branch.Head.Next;
    //    }
    //    if (branch.Head != null)
    //    {
    //      Branch.Node list = branch.Head;
    //      while (list.Next != null)
    //      {
    //        if (EncapsulationCheck(min, max, list.Next.Value.Min, list.Next.Value.Max))
    //        {
    //          removals += list.Next.Value.Count;
    //          list.Next = list.Next.Next;
    //        }
    //        else
    //        {
    //          removals += Remove(list.Next.Value, min, max);
    //          if (list.Next.Value.Count == 0)
    //            ChopChild(branch, list.Next.Value.Index);
    //        }
    //      }
    //    }

    //    branch.Count -= removals;

    //    if (branch.Count < this._load && branch.Count != 0)
    //      ShrinkChild(branch.Parent, branch.Index);

    //    return removals;
    //  }
    //}

    ///// <summary>Converts a branch back into a leaf when the count is reduced.</summary>
    ///// <param name="parent">The parent to shrink a child of.</param>
    ///// <param name="child">The index of the child to shrink.</param>
    //private void ShrinkChild(Branch parent, int child)
    //{
    //  if (parent == null)
    //  {
    //    Node oldChild = this._top;
    //    this._top = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
    //    this.Foreach((T i) => { this.Add(i); }, oldChild);
    //  }
    //  else if (parent.Children.Length == this._children)
    //  {
    //    Node oldChild = parent.Children[child];
    //    parent.Children[child] = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
    //    this.Foreach((T i) => { this.Add(i); }, oldChild);
    //  }
    //  else
    //  {
    //    for (int i = 0; i < parent.ChildCount; i++)
    //    {
    //      if (parent.Children[i].Index == child)
    //      {
    //        Node oldChild = parent.Children[i];
    //        parent.Children[i] = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
    //        this.Foreach((T j) => { this.Add(j); }, oldChild);
    //        break;
    //      }
    //    }
    //  }
    //}

    ///// <summary>Removes all the items in a given space validated by a predicate.</summary>
    ///// <param name="min">The minimum values of the space.</param>
    ///// <param name="max">The maximum values of the space.</param>
    ///// <param name="where">The equality constraint of the removal.</param>
    //public void Remove(M[] min, M[] max, Predicate<T> where)
    //{
    //  this._count -= this.Remove(this._top, min, max, where);

    //  // dynamic tree sizes
    //  while (this._loadPowered > this._count && this._load > _defaultLoad)
    //  {
    //    this._load--;
    //    this._loadPlusOnePowered =
    //      Omnitree_Linked<T, M>.Int_Power(_load + 1, _dimensions);
    //    this._loadPowered =
    //      Omnitree_Linked<T, M>.Int_Power(_load, _dimensions);
    //  }

    //  this._previousAddition = this._top;
    //  this._previousAdditionDepth = 0;
    //}

    ///// <summary>Recursive version of the remove method.</summary>
    ///// <param name="node">The current node of traversal.</param>
    ///// <param name="min">The min dimensions of the removal space.</param>
    ///// <param name="max">The max dimensions of the removal space.</param>
    ///// <param name="where">The equality constraint of the removal.</param>
    //private int Remove(Node node, M[] min, M[] max, Predicate<T> where)
    //{
    //  int removals = 0;
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //    {
    //      M[] ms;
    //      this._locate(leaf.Contents[i], out ms);

    //      if (ms == null || ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");

    //      if (this.EncapsulationCheck(min, max, ms) && where(leaf.Contents[i]))
    //        removals++;
    //      else
    //        leaf.Contents[i - removals] = leaf.Contents[i];
    //    }
    //    leaf.Count -= removals;
    //    this._count -= removals;

    //    return removals;
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (branch.Children[i] != null)
    //        {
    //          removals += this.Remove(branch.Children[i], min, max, where);
    //          if (branch.Children[i].Count == 0)
    //            ChopChild(branch, branch.Children[i--].Index);
    //        }
    //    }
    //    else
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //      {
    //        removals += this.Remove(branch.Children[i], min, max, where);
    //        if (branch.Children[i].Count == 0)
    //          this.ChopChild(branch, branch.Children[i--].Index);
    //      }
    //    }
    //    branch.Count -= removals;

    //    if (branch.Count < this._load && branch.Count != 0)
    //      ShrinkChild(branch.Parent, branch.Index);

    //    return removals;
    //  }
    //}

    ///// <summary>Grows a branch on the tree at the desired location</summary>
    ///// <param name="branch">The branch to grow a branch on.</param>
    ///// <param name="min">The minimum dimensions of the new branch.</param>
    ///// <param name="max">The maximum dimensions of the new branch.</param>
    ///// <param name="child">The child index to grow the branch on.</param>
    ///// <returns>The newly constructed branch.</returns>
    //private Branch GrowBranch(Branch branch, M[] min, M[] max, int child)
    //{
    //  return this.Branch_SetChild(branch, child, 
    //    new Branch(min, max, branch, child)) as Branch;
    //}

    ///// <summary>Grows a leaf on the tree at the desired location.</summary>
    ///// <param name="branch">The branch to grow a leaf on.</param>
    ///// <param name="child">The index to grow a leaf on.</param>
    ///// <returns>The constructed leaf.</returns>
    //private Leaf GrowLeaf(Branch branch, int child)
    //{
    //  M[] min, max;
    //  this.DetermineChildBounds(branch, child, out min, out max);
    //  return this.Branch_SetChild(branch, child, 
    //    new Leaf(min, max, branch, child)) as Leaf;
    //}

    ///// <summary>Computes the child index that contains the desired dimensions.</summary>
    ///// <param name="node">The node to compute the child index of.</param>
    ///// <param name="ms">The coordinates to find the child index of.</param>
    ///// <returns>The computed child index based on the coordinates relative to the center of the node.</returns>
    //private int DetermineChild(Node node, M[] ms)
    //{
    //  int child = 0;
    //  for (int i = 0; i < this._dimensions; i++)
    //    if (!(_compare(ms[i], _average(node.Min[i], node.Max[i])) == Comparison.Less))
    //      child += 1 << i;
    //  return child;
    //}

    ///// <summary>Determins the dimensions of the child at the given index.</summary>
    ///// <param name="node">The parent of the node to compute dimensions for.</param>
    ///// <param name="child">The index of the child to compute dimensions for.</param>
    ///// <param name="min">The computed minimum dimensions of the child node.</param>
    ///// <param name="max">The computed maximum dimensions of the child node.</param>
    //private void DetermineChildBounds(Node node, int child, out M[] min, out M[] max)
    //{
    //  min = new M[this._dimensions];
    //  max = new M[this._dimensions];
    //  for (int i = _dimensions - 1; i >= 0; i--)
    //  {
    //    int temp = 1 << i;
    //    if (child >= temp)
    //    {
    //      min[i] = this._average(node.Min[i], node.Max[i]);
    //      max[i] = node.Max[i];
    //      child -= temp;
    //    }
    //    else
    //    {
    //      min[i] = node.Min[i];
    //      max[i] = this._average(node.Min[i], node.Max[i]);
    //    }
    //  }
    //}

    ///// <summary>Checks a node for inclusion (overlap) between two spaces.</summary>
    ///// <param name="left_min">The minimum values of the left space.</param>
    ///// <param name="left_max">The maximum values of the left space.</param>
    ///// <param name="right_min">The minimum values of the right space.</param>
    ///// <param name="right_max">The maximum values of the right space.</param>
    ///// <returns>True if the spaces overlap; False if not.</returns>
    //private bool InclusionCheck(M[] left_min, M[] left_max, M[] right_min, M[] right_max)
    //{
    //  // if the right space is not outside the left space, they must overlap
    //  for (int j = 0; j < this._dimensions; j++)
    //    if (this._compare(left_max[j], right_min[j]) == Comparison.Less ||
    //      this._compare(left_min[j], right_max[j]) == Comparison.Greater)
    //      return false;
    //  return true;
    //}

    ///// <summary>Checks if a space encapsulates a point.</summary>
    ///// <param name="min">The minimum values of the space.</param>
    ///// <param name="max">The maximum values of the space.</param>
    ///// <param name="ms">The point.</param>
    ///// <returns>True if the space encapsulates the point; False if not.</returns>
    //private bool EncapsulationCheck(M[] min, M[] max, M[] ms)
    //{
    //  // if the space is not outside the bounds, it must be inside
    //  for (int j = 0; j < this._dimensions; j++)
    //    if (this._compare(ms[j], min[j]) == Comparison.Less ||
    //      this._compare(ms[j], max[j]) == Comparison.Greater)
    //      return false;
    //  return true;
    //}

    ///// <summary>Checks if a space (left) encapsulates another space (right).</summary>
    ///// <param name="left_min">The minimum values of the left space.</param>
    ///// <param name="left_max">The maximum values of the left space.</param>
    ///// <param name="right_min">The minimum values of the right space.</param>
    ///// <param name="right_max">The maximum values of the right space.</param>
    ///// <returns>True if the left space encapsulates the right; False if not.</returns>
    //private bool EncapsulationCheck(M[] left_min, M[] left_max, M[] right_min, M[] right_max)
    //{
    //  // if all the dimensions of the left space are 
    //  // beyond that of the right, the right is encapsulated
    //  for (int j = 0; j < this._dimensions; j++)
    //    if (this._compare(left_min[j], right_min[j]) != Comparison.Less ||
    //      this._compare(left_max[j], right_max[j]) != Comparison.Greater)
    //      return false;
    //  return true;
    //}

    ///// <summary>Chops (removes) a child from a branch.</summary>
    ///// <param name="branch">The parent of the child to chop.</param>
    ///// <param name="child">The index of the child to chop.</param>
    //private void ChopChild(Branch branch, int child)
    //{
    //  if (branch.Children.Length == this._children)
    //    branch.Children[child] = null;
    //  else
    //  {
    //    for (int i = 0; i < branch.ChildCount; i++)
    //      if (branch.Children[i].Index == child)
    //      {
    //        branch.Children[i] = branch.Children[branch.ChildCount - 1];
    //        branch.ChildCount--;
    //        break;
    //      }
    //  }
    //}

    ///// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
    //public void Update()
    //{
    //  this.Update(this._top, 0);

    //  this._previousAddition = this._top;
    //  this._previousAdditionDepth = 0;
    //}

    ///// <summary>Recursive version of the Update method.</summary>
    ///// <param name="node">The current node of iteration.</param>
    ///// <param name="depth">The current depth of iteration.</param>
    //private void Update(Node node, int depth)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //    {
    //      M[] ms;
    //      this._locate(leaf.Contents[i], out ms);

    //      if (ms == null || ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");

    //      if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
    //      {
    //        T item = leaf.Contents[i];
    //        leaf.Contents[i] = leaf.Contents[--leaf.Count];
    //        Node temp = leaf.Parent;
    //        while (temp != null)
    //        {
    //          if (EncapsulationCheck(temp.Min, temp.Max, ms))
    //            break;
    //          temp.Count--;
    //          temp = temp.Parent;
    //        }

    //        if (temp == null)
    //        {
    //          this._count--;
    //          if (HandleOutOfBounds == null)
    //            throw new Error("a node was updated to be out of bounds (found in an update)");
    //          else
    //            this.HandleOutOfBounds(leaf.Contents[i]);
    //        }
    //        else
    //          this.Add(item, temp, ms, depth);
    //      }
    //    }
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (branch.Children[i] != null)
    //        {
    //          this.Update(branch.Children[i], depth + 1);
    //          if (branch.Children[i].Count == 0)
    //            ChopChild(branch, branch.Children[i].Index);
    //        }
    //    }
    //    else
    //      for (int i = 0; i < branch.ChildCount; i++)
    //      {
    //        this.Update(branch.Children[i], depth + 1);
    //        if (branch.Children[i].Count == 0)
    //          ChopChild(branch, branch.Children[i].Index);
    //      }

    //    if (branch.Count < this._load && branch.Count != 0)
    //      ShrinkChild(branch.Parent, branch.Index);
    //  }
    //}

    ///// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
    ///// <param name="min">The minimum dimensions of the space to update.</param>
    ///// <param name="max">The maximum dimensions of the space to update.</param>
    //public void Update(M[] min, M[] max)
    //{
    //  this.Update(min, max, this._top, 0);

    //  this._previousAddition = this._top;
    //  this._previousAdditionDepth = 0;
    //}

    ///// <summary>Recursive version of the Update method.</summary>
    ///// <param name="min">The minimum dimensions of the space to update.</param>
    ///// <param name="max">The maximum dimensions of the space to update.</param>
    ///// <param name="node">The current node of iteration.</param>
    ///// <param name="depth">The current depth in the tree.</param>
    //private void Update(M[] min, M[] max, Node node, int depth)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //    {
    //      M[] ms;
    //      this._locate(leaf.Contents[i], out ms);

    //      if (ms == null || ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");


    //      if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
    //      {
    //        T item = leaf.Contents[i];
    //        leaf.Contents[i] = leaf.Contents[--leaf.Count];
    //        Node temp = leaf.Parent;
    //        while (temp != null)
    //        {
    //          if (EncapsulationCheck(temp.Min, temp.Max, ms))
    //            break;
    //          temp.Count--;
    //          temp = temp.Parent;
    //        }

    //        if (temp == null)
    //        {
    //          this._count--;
    //          if (HandleOutOfBounds == null)
    //            throw new Error("a node was updated to be out of bounds (found in an update)");
    //          else
    //            this.HandleOutOfBounds(leaf.Contents[i]);
    //        }
    //        else
    //          this.Add(item, temp, ms, depth);
    //      }
    //    }
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (branch.Children[i] != null)
    //        {
    //          this.Update(branch.Children[i], depth + 1);
    //          if (branch.Children[i].Count == 0)
    //            ChopChild(branch, branch.Children[i].Index);
    //        }
    //    }
    //    else
    //      for (int i = 0; i < branch.ChildCount; i++)
    //      {
    //        this.Update(branch.Children[i], depth + 1);
    //        if (branch.Children[i].Count == 0)
    //          ChopChild(branch, branch.Children[i].Index);
    //      }

    //    if (branch.Count < this._load && branch.Count != 0)
    //      ShrinkChild(branch.Parent, branch.Index);
    //  }
    //}

    ///// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    ///// <param name="function">The delegate to perform on every item in the tree.</param>
    //public void Foreach(Foreach<T> function)
    //{
    //  this.Foreach(function, this._top);
    //}
    //private void Foreach(Foreach<T> function, Node node)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //      function(leaf.Contents[i]);
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      foreach (Node child in branch.Children)
    //        if (child != null)
    //          this.Foreach(function, child);
    //    }
    //    else
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        this.Foreach(function, branch.Children[i]);
    //  }
    //}

    ///// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    ///// <param name="function">The delegate to perform on every item in the tree.</param>
    //public void Foreach(ForeachRef<T> function)
    //{
    //  Foreach(function, this._top);
    //}
    //private void Foreach(ForeachRef<T> function, Node node)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //      function(ref leaf.Contents[i]);
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      foreach (Node child in branch.Children)
    //        if (child != null)
    //          this.Foreach(function, child);
    //    }
    //    else
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        this.Foreach(function, branch.Children[i]);
    //  }
    //}

    ///// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    ///// <param name="function">The delegate to perform on every item in the tree.</param>
    //public ForeachStatus Foreach(ForeachBreak<T> function)
    //{
    //  return Foreach(function, this._top);
    //}
    //private ForeachStatus Foreach(ForeachBreak<T> function, Node node)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //      if (function(leaf.Contents[i]) == ForeachStatus.Break)
    //        return ForeachStatus.Break;
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.ChildCount == this._children)
    //    {
    //      foreach (Node child in branch.Children)
    //        if (child != null)
    //          if (this.Foreach(function, child) == ForeachStatus.Break)
    //            return ForeachStatus.Break;
    //    }
    //    else
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (this.Foreach(function, branch.Children[i]) == ForeachStatus.Break)
    //          return ForeachStatus.Break;
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    ///// <param name="function">The delegate to perform on every item in the tree.</param>
    //public ForeachStatus Foreach(ForeachRefBreak<T> function)
    //{
    //  return Foreach(function, this._top);
    //}
    //private ForeachStatus Foreach(ForeachRefBreak<T> function, Node node)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //      if (function(ref leaf.Contents[i]) == ForeachStatus.Break)
    //        return ForeachStatus.Break;
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      foreach (Node child in branch.Children)
    //        if (child != null)
    //          if (this.Foreach(function, child) == ForeachStatus.Break)
    //            return ForeachStatus.Break;
    //    }
    //    else
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (this.Foreach(function, branch.Children[i]) == ForeachStatus.Break)
    //          return ForeachStatus.Break;
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    ///// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    ///// <param name="min">The minimum dimensions of the traversal.</param>
    ///// <param name="max">The maximum dimensions of the traversal.</param>
    //public void Foreach(Foreach<T> function, M[] min, M[] max)
    //{
    //  Foreach(function, _top, min, max);
    //}
    //private void Foreach(Foreach<T> function, Node node, M[] min, M[] max)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //    {
    //      M[] ms;
    //      this._locate(leaf.Contents[i], out ms);

    //      if (ms == null || ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");

    //      if (EncapsulationCheck(min, max, ms))
    //        function(leaf.Contents[i]);
    //    }
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
    //          this.Foreach(function, branch.Children[i], min, max);
    //    }
    //    else
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
    //          this.Foreach(function, branch.Children[i], min, max);
    //    }
    //  }
    //}

    ///// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    ///// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    ///// <param name="min">The minimum dimensions of the traversal.</param>
    ///// <param name="max">The maximum dimensions of the traversal.</param>
    //public void Foreach(ForeachRef<T> function, M[] min, M[] max)
    //{
    //  Foreach(function, _top, min, max);
    //}
    //private void Foreach(ForeachRef<T> function, Node node, M[] min, M[] max)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //    {
    //      M[] ms;
    //      this._locate(leaf.Contents[i], out ms);

    //      if (ms == null || ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");

    //      if (EncapsulationCheck(min, max, ms))
    //        function(ref leaf.Contents[i]);
    //    }
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      for (int i = 0; i < this._children; i++)
    //        if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
    //          this.Foreach(function, branch.Children[i], min, max);
    //    }
    //    else
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
    //          this.Foreach(function, branch.Children[i], min, max);
    //    }
    //  }
    //}

    ///// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    ///// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    ///// <param name="min">The minimum dimensions of the traversal.</param>
    ///// <param name="max">The maximum dimensions of the traversal.</param>
    //public ForeachStatus Foreach(ForeachBreak<T> function, M[] min, M[] max)
    //{
    //  return Foreach(function, _top, min, max);
    //}
    //private ForeachStatus Foreach(ForeachBreak<T> function, Node node, M[] min, M[] max)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //    {
    //      M[] ms;
    //      this._locate(leaf.Contents[i], out ms);

    //      if (ms == null || ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");

    //      if (EncapsulationCheck(min, max, ms))
    //        if (function(leaf.Contents[i]) == ForeachStatus.Break)
    //          return ForeachStatus.Break;
    //    }
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      for (int i = 0; i < this._children; i++)
    //        if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
    //          if (this.Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
    //            return ForeachStatus.Break;
    //    }
    //    else
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
    //          if (this.Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
    //            return ForeachStatus.Break;
    //    }
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
    ///// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
    ///// <param name="min">The minimum dimensions of the traversal.</param>
    ///// <param name="max">The maximum dimensions of the traversal.</param>
    //public ForeachStatus Foreach(ForeachRefBreak<T> function, M[] min, M[] max)
    //{
    //  return Foreach(function, _top, min, max);
    //}
    //private ForeachStatus Foreach(ForeachRefBreak<T> function, Node node, M[] min, M[] max)
    //{
    //  if (node is Leaf)
    //  {
    //    Leaf leaf = node as Leaf;
    //    for (int i = 0; i < leaf.Count; i++)
    //    {
    //      M[] ms;
    //      this._locate(leaf.Contents[i], out ms);

    //      if (ms == null || ms.Length != this._dimensions)
    //        throw new Error("the location function for omnitree is invalid.");

    //      if (EncapsulationCheck(min, max, ms))
    //        if (function(ref leaf.Contents[i]) == ForeachStatus.Break)
    //          return ForeachStatus.Break;
    //    }
    //  }
    //  else
    //  {
    //    Branch branch = node as Branch;
    //    if (branch.Children.Length == this._children)
    //    {
    //      for (int i = 0; i < this._children; i++)
    //        if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
    //          if (this.Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
    //            return ForeachStatus.Break;
    //    }
    //    else
    //    {
    //      for (int i = 0; i < branch.ChildCount; i++)
    //        if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
    //          if (this.Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
    //            return ForeachStatus.Break;
    //    }
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //System.Collections.IEnumerator
    //  System.Collections.IEnumerable.GetEnumerator()
    //{
    //  T[] array = this.ToArray();
    //  return array.GetEnumerator();
    //}

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //System.Collections.Generic.IEnumerator<T>
    //  System.Collections.Generic.IEnumerable<T>.GetEnumerator()
    //{
    //  T[] array = this.ToArray();
    //  return ((System.Collections.Generic.IEnumerable<T>)array).GetEnumerator();
    //}

    ///// <summary>Puts all the items on this tree into an array.</summary>
    ///// <returns>The array containing all the items within the tree.</returns>
    //public T[] ToArray()
    //{
    //  int index = 0;
    //  T[] array = new T[this._count];
    //  this.Foreach((T entry) => { array[index++] = entry; });
    //  return array;
    //}

    ///// <summary>Creates a shallow clone of this data structure.</summary>
    ///// <returns>A shallow clone of this data structure.</returns>
    //public Structure<T> Clone()
    //{
    //  // OPTIMIZATION NEEDED
    //  Omnitree_Array<T, M> clone = new Omnitree_Array<T, M>(
    //    this._top.Min, this._top.Max,
    //    this._locate,
    //    this._compare,
    //    this._average);
    //  this.Foreach((T current) => { clone.Add(current); });
    //  return clone;
    //}

    ///// <summary>Returns the tree to an empty state.</summary>
    //public void Clear()
    //{
    //  this._top = new Leaf(this._top.Min, this._top.Max, null, -1);
    //  this._count = 0;

    //  this._load = _defaultLoad;
    //  this._loadPlusOnePowered =
    //    Omnitree_Linked<T, M>.Int_Power(this._load + 1, this._dimensions);
    //  this._loadPowered =
    //    Omnitree_Linked<T, M>.Int_Power(_load, _dimensions);
    //}

    ///// <summary>Omnitree_Array Exception.</summary>
    //private class Error : Omnitree.Error
    //{
    //  public Error(string message) : base(message) { }
    //}
  }

	/// <summary>Sorts items along N dimensions. The one data structure to rule them all. Made by Zachary Patten.</summary>
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
			private int _index;
			private int _count;

      internal M[] Min { get { return this._min; } }
      internal M[] Max { get { return this._max; } }
      internal Branch Parent { get { return this._parent; } }
			internal int Index { get { return this._index; } }
			internal int Count { get { return this._count; } set { this._count = value; } }

      internal Node(M[] min, M[] max, Branch parent, int index)
      {
        this._min = min;
        this._max = max;
        this._parent = parent;
				this._index = index;
      }
    }

    /// <summary>A leaf in the tree. Only contains items.</summary>
    private class Leaf : Node
    {
      private T[] _contents;

      public T[] Contents { get { return this._contents; } set { this._contents = value; } }

      public Leaf(M[] min, M[] max, Branch parent, int index)
        : base(min, max, parent, index)
      {
        this._contents = new T[_defaultLoad];
      }
    }

    /// <summary>A branch in the tree. Only contains nodes.</summary>
    private class Branch : Node
    {
      private Node[] _children;
      private int _childCount;

      internal int ChildCount { get { return this._childCount; } set { this._childCount = value; } }
      internal Node[] Children { get { return this._children; } set { this._children = value; } }

      public Branch(M[] min, M[] max, Branch parent, int index, int initialSize)
        : base(min, max, parent, index)
      {
        this._children = new Node[initialSize];
        this._childCount = 0;
      }
    }

		private const int _defaultLoad = 2;

    private Omnitree.Locate<T, M> _locate;
    private Omnitree.Average<M> _average;
    private Compare<M> _compare;
    private int _dimensions;
    private int _children;
		private M[] _origin;

    private Node _top;
    private int _count;
    /// <summary>count ^ (1 / dimensions)</summary>
    private int _load;
		/// <summary>load ^ dimensions</summary>
		private int _loadPowered;
    /// <summary>(load + 1) ^ dimensions</summary>
    private int _loadPlusOnePowered;
		/// <summary>leaf depth of previous addition (sequencial addition optimization)</summary>
    private Node _previousAddition;
		/// <summary>tree depth of previous addition (sequencial addition optimization)</summary>
    private int _previousAdditionDepth;

		/// <summary>Event for handling items that go outside the bounds of the Omnitree.</summary>
		public event Foreach<T> HandleOutOfBounds;

		/// <summary>Gets the dimensions of the center point of the Omnitree.</summary>
		public M[] Origin { get { return (M[])this._origin.Clone(); } }

    /// <summary>The minimum dimensions of the Omnitree.</summary>
    public M[] Min { get { return (M[])this._top.Min.Clone(); } }

    /// <summary>The maximum dimensions of the Omnitree.</summary>
    public M[] Max { get { return (M[])this._top.Max.Clone(); } }

    /// <summary>The number of dimensions in this tree.</summary>
    public int Dimensions { get { return this._dimensions; } }

    /// <summary>The current number of items in the tree.</summary>
    public int Count { get { return this._count; } }

    /// <summary>True if (Count == 0).</summary>
    public bool IsEmpty { get { return this._count == 0; } }

    /// <summary>Gets the current memory allocation size of this structure.</summary>
    public int SizeOf { get { throw new System.NotImplementedException("Sorry, I'm working on it."); } }

    /// <summary>The compare function the Omnitree is using.</summary>
    public Compare<M> Compare { get { return this._compare; } }

    /// <summary>The location function the Omnitree is using.</summary>
    public Omnitree.Locate<T, M> Locate { get { return this._locate; } }

    /// <summary>The average function the Omnitree is using.</summary>
    public Omnitree.Average<M> Average { get { return this._average; } }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    public Omnitree_Array(
      M[] min, M[] max,
      Omnitree.Locate<T, M> locate,
      Compare<M> compare,
      Omnitree.Average<M> average)
    {
			// check the locate and compare delegates
			if (locate == null)
				throw new Error("null reference on location delegate during Omnitree construction");
			if (compare == null)
				throw new Error("null reference on compare delegate during Omnitree construction");

			// Check the min/max values
			if (min == null)
				throw new Error("null reference on min dimensions during Omnitree construction");
			if (max == null)
				throw new Error("null reference on max dimensions during Omnitree construction");
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");
			for (int i = 0; i < min.Length; i++)
				if (compare(min[i], max[i]) != Comparison.Less)
					throw new Error("invalid min/max values. not all min values are less than the max values");

			// Check the average delegate
			if (average == null)
				throw new Error("null reference on average delegate during Omnitree construction");
			M[] origin = new M[min.Length];
			for (int i = 0; i < min.Length; i++)
				origin[i] = average(min[i], max[i]);
			for (int i = 0; i < min.Length; i++)
				if (compare(min[i], origin[i]) != Comparison.Less || compare(origin[i], max[i]) != Comparison.Less)
					throw new Error("invalid average function. not all average values were computed to be between the min/max values.");
			
		  this._locate = locate;
      this._average = average;
      this._compare = compare;
      this._dimensions = min.Length;
      this._children = 1 << this._dimensions;

      this._count = 0;
      this._top = new Leaf(min, max, null, -1);

      this._load = _defaultLoad;
      this._loadPlusOnePowered = 
				Omnitree_Array<T, M>.Int_Power(this._load + 1, this._dimensions);
			this._loadPowered = 
				Omnitree_Array<T, M>.Int_Power(_load, _dimensions);

			this._origin = origin;

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
    }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    public Omnitree_Array(M[] min, M[] max, Omnitree.Locate_NonOut<T, M> locate, Compare<M> compare, Omnitree.Average<M> average)
      : this(min, max,
      // this is just an adapter - JIT should optimize
      (T item, out M[] ms) => { ms = locate(item); },
      compare, average)
    {

    }

    /// <summary>Adds an item to the tree.</summary>
    /// <param name="addition">The item to be added.</param>
    public void Add(T addition)
    {
			if (this._count == int.MaxValue)
				throw new Error("(Count == int.MaxValue) max Omnitree size reached (change ints to longs if you need to).");

      // dynamic tree sizes
      if (this._loadPlusOnePowered < this._count)
      {
        this._load++;
        this._loadPlusOnePowered =
          Omnitree_Array<T, M>.Int_Power(this._load + 1, this._dimensions);
        this._loadPowered =
          Omnitree_Array<T, M>.Int_Power(this._load, this._dimensions);
      }

      M[] ms;
      this._locate(addition, out ms);

			if (ms == null || ms.Length != this._dimensions)
				throw new Error("the location function for omnitree is invalid.");

			if (!EncapsulationCheck(this._top.Min, this._top.Max, ms))
				throw new Error("out of bounds during addition");

      if (this._top is Leaf && (this._top as Leaf).Count >= this._load)
      {
				Leaf leaf = this._top as Leaf;
        this._top = new Branch(this._top.Min, this._top.Max, null, -1, _defaultLoad);

        for (int i = 0; i < leaf.Count; i++)
        {
          M[] child_ms;
          this._locate(leaf.Contents[i], out child_ms);

					if (child_ms == null || child_ms.Length != this._dimensions)
						throw new Error("the location function for omnitree is invalid.");

					if (!EncapsulationCheck(this._top.Min, this._top.Max, child_ms))
					{
						this._count--;
						if (HandleOutOfBounds == null)
							throw new Error("a node was updated to be out of bounds (found in an addition)");
						else
							this.HandleOutOfBounds(leaf.Contents[i]);
					}
					else
						Add(leaf.Contents[i], this._top, child_ms, 0);
        }
      }

			if (this.EncapsulationCheck(this._previousAddition.Min, this._previousAddition.Max, ms))
				this.Add(addition, this._previousAddition, ms, this._previousAdditionDepth);
			else
				this.Add(addition, _top, ms, 0);
      this._count++;
    }

    /// <summary>Recursive version of the add function.</summary>
    /// <param name="addition">The item to be added.</param>
    /// <param name="node">The current location of the tree.</param>
    /// <param name="ms">The location of the addition.</param>
    /// <param name="depth">The current depth of iteration.</param>
    private void Add(T addition, Node node, M[] ms, int depth)
    {
      if (node is Leaf)
      {
        Leaf leaf = node as Leaf;
        if (depth >= this._load || !(leaf.Count >= this._load))
        {
          Omnitree_Array<T, M>.Leaf_Add(leaf, addition);

					this._previousAddition = leaf;
					this._previousAdditionDepth = depth;

          return;
        }
        else
        {
          Branch parent = node.Parent;
          Branch growth = GrowBranch(parent, leaf.Min, leaf.Max, this.DetermineChild(parent, ms));

          for (int i = 0; i < leaf.Count; i++)
          {
            M[] child_ms;
						this._locate(leaf.Contents[i], out child_ms);

            if (child_ms == null || child_ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (EncapsulationCheck(growth.Min, growth.Max, child_ms))
							Add(leaf.Contents[i], growth, child_ms, depth);
            else
            {
							if (EncapsulationCheck(this._top.Min, this._top.Max, child_ms))
							{
								this._count--;
								if (HandleOutOfBounds == null)
									throw new Error("a node was updated to be out of bounds (found in an addition)");
								else
									this.HandleOutOfBounds(leaf.Contents[i]);
							}
							else
								Add(leaf.Contents[i], this._top, child_ms, depth);
            }
          }

          Add(addition, growth, ms, depth);
          return;
        }
      }
      else
      {
        Branch branch = node as Branch;
        int child = this.DetermineChild(branch, ms);
        Node child_node = this.Branch_GetChild(branch, child);

				// null children in branches are just empty leaves
        if (child_node == null)
        {
          Leaf leaf = GrowLeaf(branch, child);
          Omnitree_Array<T, M>.Leaf_Add(leaf, addition);

					this._previousAddition = leaf;
					this._previousAdditionDepth = depth + 1;

					branch.Count++;
          return;
        }

        Add(addition, child_node, ms, depth + 1);
				branch.Count++;
        return;
      }
    }

    /// <summary>Adds an item to a leaf.</summary>
    /// <param name="leaf">The leaf to add to.</param>
    /// <param name="addition">The item to add to the leaf.</param>
    private static void Leaf_Add(Leaf leaf, T addition)
    {
      if (leaf.Count == leaf.Contents.Length)
      {
        T[] newAllocaiton = new T[leaf.Contents.Length * 2];
        for (int i = 0; i < leaf.Contents.Length; i++)
          newAllocaiton[i] = leaf.Contents[i];
        leaf.Contents = newAllocaiton;
      }
      leaf.Contents[leaf.Count++] = addition;
    }

    /// <summary>Gets a child at of a given index in a branch.</summary>
    /// <param name="branch">The branch to get the child of.</param>
    /// <param name="index">The index of the child to get.</param>
    /// <returns>The value of the child.</returns>
    private Node Branch_GetChild(Branch branch, int index)
    {
      // fully allocated branch - use true indeces
      if (branch.Children.Length == this._children)
        return branch.Children[index];
      // partially allocated branch - use fake indeces
			for (int i = 0; i < branch.ChildCount; i++)
				if (branch.Children[i].Index == index)
					return branch.Children[i];
      return null;
    }

    /// <summary>Sets a child at of a given index in a branch.</summary>
    /// <param name="branch">The branch to set the child of.</param>
    /// <param name="index">The index of the child to set.</param>
    /// <param name="value">The value to be set.</param>
    private Node Branch_SetChild(Branch branch, int index, Node value)
    {
      // leaves are completely new nodes (as oposed to branches replacing leaves)
      if (value is Leaf)
      {
        // if the branch is fully allocated, treat the index as the
        // actual index we want to set
				if (branch.Children.Length == this._children)
          branch.Children[index] = value;
        // if the branch is not yet fully allocated - the index must
        // first be found in the partial allocation
        else
        {
          // the list array has reached capacity and needs to grow
          if (branch.ChildCount == branch.Children.Length)
          {
            Node[] newAllocation =
							new Node[branch.Children.Length * 2 > this._children ? 
								branch.Children.Length * 2 : this._children];

            // if the growth will result in a fully allocated branch -
            // make the indeces true indeces
						if (newAllocation.Length == this._children)
            {
              foreach (Node node in branch.Children)
                newAllocation[node.Index] = node;
              newAllocation[index] = value;
              branch.Children = newAllocation;
							branch.ChildCount++;
            }
            // if the branch will not yet be fully allocated - just
            // keep using fake indeces
            else
            {
              for (int i = 0; i < branch.Children.Length; i++)
                newAllocation[i] = branch.Children[i];
              newAllocation[branch.ChildCount++] = value;
              branch.Children = newAllocation;
            }
          }
          // now growth required - just add using fake indeces
          else
            branch.Children[branch.ChildCount++] = value;
        }
      }
      else
      {
        // value is a branch; we need to overwrite a leaf
        for (int i = 0; i < branch.Children.Length; i++)
					if (branch.Children[i].Index == index)
					{
						branch.Children[i] = value;
						break;
					}
      }
      return value;
    }

    /// <summary>Takes an integer to the power of the exponent.</summary>
    /// <param name="_base">The base operand of the power function.</param>
    /// <param name="exponent">The exponent operand of the power operand.</param>
    /// <returns>The computed (bease ^ exponent) value.</returns>
    private static int Int_Power(int _base, int exponent)
    {
      int result = 1;
      while (exponent > 0)
      {
        if ((exponent & 1) > 0)
          result *= _base;
        exponent >>= 1;
        _base *= _base;
      }
      return result;
    }

		/// <summary>Removes all the items qualified by the delegate.</summary>
		/// <param name="where">The predicate to qualify removals.</param>
		public void Remove(Predicate<T> where)
		{
			this._count -= this.Remove(this._top, where);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_Array<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_Array<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="where">The predicate to qualify removals.</param>
		private int Remove(Node node, Predicate<T> where)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					if (where(leaf.Contents[i]))
						removals++;
					else
						leaf.Contents[i - removals] = leaf.Contents[i];
				}
				leaf.Count -= removals;
				this._count -= removals;

				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null)
						{
							removals += this.Remove(branch.Children[i], where);
							if (branch.Children[i].Count == 0)
								ChopChild(branch, branch.Children[i--].Index);
						}
				}
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
					{
						removals += this.Remove(branch.Children[i], where);
						if (branch.Children[i].Count == 0)
							this.ChopChild(branch, branch.Children[i--].Index);
					}
				}
				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
			}
		}

    /// <summary>Removes all the items in a given space.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
		/// <returns>The number of items that were removed.</returns>
    public void Remove(M[] min, M[] max)
    {
			this._count -= this.Remove(this._top, min, max);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_Array<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_Array<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
    }

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="min">The min dimensions of the removal space.</param>
		/// <param name="max">The max dimensions of the removal space.</param>
		private int Remove(Node node, M[] min, M[] max)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					M[] ms;
					this._locate(leaf.Contents[i], out ms);

					if (ms == null || ms.Length != this._dimensions)
						throw new Error("the location function for omnitree is invalid.");

					if (this.EncapsulationCheck(min, max, ms))
						removals++;
					else
						leaf.Contents[i - removals] = leaf.Contents[i];
			  }
				leaf.Count -= removals;
				this._count -= removals;

			  return removals;
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
						{
							if (this.EncapsulationCheck(min, max, node.Min, node.Max))
							{
								removals += node.Count;
								ChopChild(branch, branch.Children[i--].Index);
							}
							else
							{
								removals += this.Remove(branch.Children[i], min, max);
								if (branch.Children[i].Count == 0)
									ChopChild(branch, branch.Children[i--].Index);
							}
						}
				}
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
						{
							if (this.EncapsulationCheck(min, max, node.Min, node.Max))
							{
								removals += node.Count;
								this.ChopChild(branch, branch.Children[i--].Index);
							}
							else
							{
								removals += this.Remove(branch.Children[i], min, max);
								if (branch.Children[i].Count == 0)
									this.ChopChild(branch, branch.Children[i--].Index);
							}
						}
				}
				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
			}
		}

		/// <summary>Converts a branch back into a leaf when the count is reduced.</summary>
		/// <param name="parent">The parent to shrink a child of.</param>
		/// <param name="child">The index of the child to shrink.</param>
		private void ShrinkChild(Branch parent, int child)
		{
			if (parent == null)
			{
				Node oldChild = this._top;
				this._top = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
				this.Foreach((T i) => { this.Add(i); }, oldChild);
			}
			else if (parent.Children.Length == this._children)
			{
				Node oldChild = parent.Children[child];
				parent.Children[child] = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
				this.Foreach((T i) => { this.Add(i); }, oldChild);
			}
			else
			{
				for (int i = 0; i < parent.ChildCount; i++)
				{
					if (parent.Children[i].Index == child)
					{
						Node oldChild = parent.Children[i];
						parent.Children[i] = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
						this.Foreach((T j) => { this.Add(j); }, oldChild);
						break;
					}
				}
			}
		}

    /// <summary>Removes all the items in a given space validated by a predicate.</summary>
    /// <param name="min">The minimum values of the space.</param>
    /// <param name="max">The maximum values of the space.</param>
    /// <param name="where">The equality constraint of the removal.</param>
    public void Remove(M[] min, M[] max, Predicate<T> where)
    {
			this._count -= this.Remove(this._top, min, max, where);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_Array<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_Array<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
    }

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="min">The min dimensions of the removal space.</param>
		/// <param name="max">The max dimensions of the removal space.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		private int Remove(Node node, M[] min, M[] max, Predicate<T> where)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					M[] ms;
					this._locate(leaf.Contents[i], out ms);

					if (ms == null || ms.Length != this._dimensions)
						throw new Error("the location function for omnitree is invalid.");

					if (this.EncapsulationCheck(min, max, ms) && where(leaf.Contents[i]))
						removals++;
					else
						leaf.Contents[i - removals] = leaf.Contents[i];
				}
				leaf.Count -= removals;
				this._count -= removals;

				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null)
						{
							removals += this.Remove(branch.Children[i], min, max, where);
							if (branch.Children[i].Count == 0)
								ChopChild(branch, branch.Children[i--].Index);
						}
				}
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
					{
						removals += this.Remove(branch.Children[i], min, max, where);
						if (branch.Children[i].Count == 0)
							this.ChopChild(branch, branch.Children[i--].Index);
					}
				}
				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
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
      return this.Branch_SetChild(branch, child, 
				new Branch(min, max, branch, child, _defaultLoad)) as Branch;
    }

    /// <summary>Grows a leaf on the tree at the desired location.</summary>
    /// <param name="branch">The branch to grow a leaf on.</param>
    /// <param name="child">The index to grow a leaf on.</param>
    /// <returns>The constructed leaf.</returns>
    private Leaf GrowLeaf(Branch branch, int child)
    {
      M[] min, max;
      this.DetermineChildBounds(branch, child, out min, out max);
      return this.Branch_SetChild(branch, child, 
				new Leaf(min, max, branch, child)) as Leaf;
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
      for (int i = _dimensions - 1; i >= 0; i--)
      {
        int temp = 1 << i;
        if (child >= temp)
        {
          min[i] = this._average(node.Min[i], node.Max[i]);
          max[i] = node.Max[i];
          child -= temp;
        }
        else
        {
          min[i] = node.Min[i];
          max[i] = this._average(node.Min[i], node.Max[i]);
        }
      }
    }

		/// <summary>Checks a node for inclusion (overlap) between two spaces.</summary>
		/// <param name="left_min">The minimum values of the left space.</param>
		/// <param name="left_max">The maximum values of the left space.</param>
		/// <param name="right_min">The minimum values of the right space.</param>
		/// <param name="right_max">The maximum values of the right space.</param>
		/// <returns>True if the spaces overlap; False if not.</returns>
		private bool InclusionCheck(M[] left_min, M[] left_max, M[] right_min, M[] right_max)
		{
			// if the right space is not outside the left space, they must overlap
			for (int j = 0; j < this._dimensions; j++)
				if (this._compare(left_max[j], right_min[j]) == Comparison.Less ||
					this._compare(left_min[j], right_max[j]) == Comparison.Greater)
					return false;
			return true;
		}

		/// <summary>Checks if a space encapsulates a point.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <param name="ms">The point.</param>
		/// <returns>True if the space encapsulates the point; False if not.</returns>
    private bool EncapsulationCheck(M[] min, M[] max, M[] ms)
    {
			// if the space is not outside the bounds, it must be inside
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(ms[j], min[j]) == Comparison.Less ||
          this._compare(ms[j], max[j]) == Comparison.Greater)
          return false;
      return true;
    }

		/// <summary>Checks if a space (left) encapsulates another space (right).</summary>
		/// <param name="left_min">The minimum values of the left space.</param>
		/// <param name="left_max">The maximum values of the left space.</param>
		/// <param name="right_min">The minimum values of the right space.</param>
		/// <param name="right_max">The maximum values of the right space.</param>
		/// <returns>True if the left space encapsulates the right; False if not.</returns>
		private bool EncapsulationCheck(M[] left_min, M[] left_max, M[] right_min, M[] right_max)
		{
			// if all the dimensions of the left space are 
			// beyond that of the right, the right is encapsulated
			for (int j = 0; j < this._dimensions; j++)
				if (this._compare(left_min[j], right_min[j]) != Comparison.Less ||
					this._compare(left_max[j], right_max[j]) != Comparison.Greater)
					return false;
			return true;
		}

    /// <summary>Chops (removes) a child from a branch.</summary>
    /// <param name="branch">The parent of the child to chop.</param>
    /// <param name="child">The index of the child to chop.</param>
    private void ChopChild(Branch branch, int child)
    {
			if (branch.Children.Length == this._children)
				branch.Children[child] = null;
			else
			{
				for (int i = 0; i < branch.ChildCount; i++)
					if (branch.Children[i].Index == child)
					{
						branch.Children[i] = branch.Children[branch.ChildCount - 1];
						branch.ChildCount--;
						break;
					}
			}
    }

    /// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
    public void Update()
    {
			this.Update(this._top, 0);

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
    }

		/// <summary>Recursive version of the Update method.</summary>
		/// <param name="node">The current node of iteration.</param>
		/// <param name="depth">The current depth of iteration.</param>
		private void Update(Node node, int depth)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					M[] ms;
          this._locate(leaf.Contents[i], out ms);

          if (ms == null || ms.Length != this._dimensions)
            throw new Error("the location function for omnitree is invalid.");

					if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
					{
						T item = leaf.Contents[i];
						leaf.Contents[i] = leaf.Contents[--leaf.Count];
						Node temp = leaf.Parent;
						while (temp != null)
						{
							if (EncapsulationCheck(temp.Min, temp.Max, ms))
								break;
							temp.Count--;
							temp = temp.Parent;
						}

						if (temp == null)
						{
							this._count--;
							if (HandleOutOfBounds == null)
								throw new Error("a node was updated to be out of bounds (found in an update)");
							else
								this.HandleOutOfBounds(leaf.Contents[i]);
						}
						else
							this.Add(item, temp, ms, depth);
					}
				}
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null)
						{
							this.Update(branch.Children[i], depth + 1);
							if (branch.Children[i].Count == 0)
								ChopChild(branch, branch.Children[i].Index);
						}
				}
				else
					for (int i = 0; i < branch.ChildCount; i++)
					{
						this.Update(branch.Children[i], depth + 1);
						if (branch.Children[i].Count == 0)
							ChopChild(branch, branch.Children[i].Index);
					}

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);
			}
		}

    /// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
    /// <param name="min">The minimum dimensions of the space to update.</param>
    /// <param name="max">The maximum dimensions of the space to update.</param>
    public void Update(M[] min, M[] max)
    {
			this.Update(min, max, this._top, 0);

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
    }

		/// <summary>Recursive version of the Update method.</summary>
		/// <param name="min">The minimum dimensions of the space to update.</param>
		/// <param name="max">The maximum dimensions of the space to update.</param>
		/// <param name="node">The current node of iteration.</param>
		/// <param name="depth">The current depth in the tree.</param>
		private void Update(M[] min, M[] max, Node node, int depth)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					M[] ms;
					this._locate(leaf.Contents[i], out ms);

					if (ms == null || ms.Length != this._dimensions)
						throw new Error("the location function for omnitree is invalid.");


					if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
					{
						T item = leaf.Contents[i];
						leaf.Contents[i] = leaf.Contents[--leaf.Count];
						Node temp = leaf.Parent;
						while (temp != null)
						{
							if (EncapsulationCheck(temp.Min, temp.Max, ms))
								break;
							temp.Count--;
							temp = temp.Parent;
						}

						if (temp == null)
						{
							this._count--;
							if (HandleOutOfBounds == null)
								throw new Error("a node was updated to be out of bounds (found in an update)");
							else
								this.HandleOutOfBounds(leaf.Contents[i]);
						}
						else
							this.Add(item, temp, ms, depth);
					}
				}
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null)
						{
							this.Update(branch.Children[i], depth + 1);
							if (branch.Children[i].Count == 0)
								ChopChild(branch, branch.Children[i].Index);
						}
				}
				else
					for (int i = 0; i < branch.ChildCount; i++)
					{
						this.Update(branch.Children[i], depth + 1);
						if (branch.Children[i].Count == 0)
							ChopChild(branch, branch.Children[i].Index);
					}

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);
			}
		}

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
    public void Foreach(Foreach<T> function)
    {
      this.Foreach(function, this._top);
    }
    private void Foreach(Foreach<T> function, Node node)
    {
      if (node is Leaf)
      {
				Leaf leaf = node as Leaf;
        for (int i = 0; i < leaf.Count; i++)
          function(leaf.Contents[i]);
      }
      else
      {
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
        {
          foreach (Node child in branch.Children)
            if (child != null)
              this.Foreach(function, child);
        }
        else
          for (int i = 0; i < branch.ChildCount; i++)
            this.Foreach(function, branch.Children[i]);
      }
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
    public void Foreach(ForeachRef<T> function)
    {
      Foreach(function, this._top);
    }
    private void Foreach(ForeachRef<T> function, Node node)
    {
      if (node is Leaf)
      {
				Leaf leaf = node as Leaf;
        for (int i = 0; i < leaf.Count; i++)
          function(ref leaf.Contents[i]);
      }
      else
      {
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
        {
					foreach (Node child in branch.Children)
            if (child != null)
              this.Foreach(function, child);
        }
        else
					for (int i = 0; i < branch.ChildCount; i++)
						this.Foreach(function, branch.Children[i]);
      }
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
    public ForeachStatus Foreach(ForeachBreak<T> function)
    {
      return Foreach(function, this._top);
    }
    private ForeachStatus Foreach(ForeachBreak<T> function, Node node)
    {
      if (node is Leaf)
      {
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
					if (function(leaf.Contents[i]) == ForeachStatus.Break)
            return ForeachStatus.Break;
      }
      else
      {
				Branch branch = node as Branch;
        if (branch.ChildCount == this._children)
        {
          foreach (Node child in branch.Children)
            if (child != null)
              if (this.Foreach(function, child) == ForeachStatus.Break)
                return ForeachStatus.Break;
        }
        else
          for (int i = 0; i < branch.ChildCount; i++)
            if (this.Foreach(function, branch.Children[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
    /// <param name="function">The delegate to perform on every item in the tree.</param>
    public ForeachStatus Foreach(ForeachRefBreak<T> function)
    {
      return Foreach(function, this._top);
    }
    private ForeachStatus Foreach(ForeachRefBreak<T> function, Node node)
    {
      if (node is Leaf)
      {
				Leaf leaf = node as Leaf;
        for (int i = 0; i < leaf.Count; i++)
          if (function(ref leaf.Contents[i]) == ForeachStatus.Break)
            return ForeachStatus.Break;
      }
      else
      {
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
        {
          foreach (Node child in branch.Children)
            if (child != null)
              if (this.Foreach(function, child) == ForeachStatus.Break)
                return ForeachStatus.Break;
        }
        else
          for (int i = 0; i < branch.ChildCount; i++)
            if (this.Foreach(function, branch.Children[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
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
      if (node is Leaf)
      {
				Leaf leaf = node as Leaf;
        for (int i = 0; i < leaf.Count; i++)
        {
					M[] ms;
          this._locate(leaf.Contents[i], out ms);

          if (ms == null || ms.Length != this._dimensions)
            throw new Error("the location function for omnitree is invalid.");

          if (EncapsulationCheck(min, max, ms))
            function(leaf.Contents[i]);
        }
      }
      else
      {
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
        {
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							this.Foreach(function, branch.Children[i], min, max);
        }
        else
        {
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							this.Foreach(function, branch.Children[i], min, max);
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
      if (node is Leaf)
      {
				Leaf leaf = node as Leaf;
        for (int i = 0; i < leaf.Count; i++)
        {
					M[] ms;
          this._locate(leaf.Contents[i], out ms);

          if (ms == null || ms.Length != this._dimensions)
            throw new Error("the location function for omnitree is invalid.");

          if (EncapsulationCheck(min, max, ms))
            function(ref leaf.Contents[i]);
        }
      }
      else
      {
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
        {
					for (int i = 0; i < this._children; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							this.Foreach(function, branch.Children[i], min, max);
        }
        else
        {
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							this.Foreach(function, branch.Children[i], min, max);
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
      if (node is Leaf)
      {
				Leaf leaf = node as Leaf;
        for (int i = 0; i < leaf.Count; i++)
        {
					M[] ms;
          this._locate(leaf.Contents[i], out ms);

          if (ms == null || ms.Length != this._dimensions)
            throw new Error("the location function for omnitree is invalid.");

          if (EncapsulationCheck(min, max, ms))
            if (function(leaf.Contents[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
        }
      }
      else
      {
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
        {
					for (int i = 0; i < this._children; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							if (this.Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
                return ForeachStatus.Break;
        }
        else
        {
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							if (this.Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
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
      if (node is Leaf)
      {
				Leaf leaf = node as Leaf;
        for (int i = 0; i < leaf.Count; i++)
        {
					M[] ms;
          this._locate(leaf.Contents[i], out ms);

          if (ms == null || ms.Length != this._dimensions)
            throw new Error("the location function for omnitree is invalid.");

          if (EncapsulationCheck(min, max, ms))
            if (function(ref leaf.Contents[i]) == ForeachStatus.Break)
              return ForeachStatus.Break;
        }
      }
      else
      {
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
        {
					for (int i = 0; i < this._children; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							if (this.Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
                return ForeachStatus.Break;
        }
        else
        {
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							if (this.Foreach(function, branch.Children[i], min, max) == ForeachStatus.Break)
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
      this._top = new Leaf(this._top.Min, this._top.Max, null, -1);
      this._count = 0;

			this._load = _defaultLoad;
			this._loadPlusOnePowered =
				Omnitree_Array<T, M>.Int_Power(this._load + 1, this._dimensions);
			this._loadPowered =
				Omnitree_Array<T, M>.Int_Power(_load, _dimensions);
    }

    /// <summary>Omnitree_Array Exception.</summary>
    private class Error : Omnitree.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}
