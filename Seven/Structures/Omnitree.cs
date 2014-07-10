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
    void Remove(M[] min, M[] max, Equate<T> where);

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
      private T[] _contents;
      private int _count;

      public T[] Contents { get { return _contents; } }
      public int Count { get { return _count; } set { _count = value; } }
      public bool IsFull { get { return _count == _contents.Length; } }

      public Leaf(M[] min, M[] max, Branch parent, int loadFactor)
        : base(min, max, parent)
      { _contents = new T[loadFactor]; }

      public Leaf Add(T addition)
      {
        if (_count == _contents.Length)
          throw new Error("There is a glitch in my octree, sorry...");
        _contents[_count++] = addition;
        return this;
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
    /// <typeparam name="G">The generic type of the type to locate.</typeparam>
    /// <param name="item">The item to be located.</param>
    /// <returns>The computed locations of the item.</returns>
    public delegate M[] Locate<G>(G item);

    /// <summary>Locates an item along the given dimensions.</summary>
    /// <typeparam name="T">The generic type of the type to locate.</typeparam>
    /// <param name="item">The item to be located.</param>
    /// <param name="ms">The computed locations of the item.</param>
    public delegate void LocateOut<G>(T item, out M[] ms);

    /// <summary>Computes the average between two items.</summary>
    /// <typeparam name="G">The generic type of the items to average.</typeparam>
    /// <param name="left">The first item of the average.</param>
    /// <param name="right">The second item of the average.</param>
    /// <returns>The computed average between the two items.</returns>
    public delegate G Average<G>(G left, G right);

    private const int _maxDimensions = 20;

    // Immutable Fields
    private Locate<T> _locate;
    private Average<M> _average;
    private Compare<M> _compare;
    private int _dimensions;
    private int _children;

    // Mutable Fields
    private Node _top;
    private int _count;
    private int _load;
    private int _loadPlusOneSquared;
    private int _loadSquared;

    /// <summary>The current number of items in the tree.</summary>
    public int Count { get { return _count; } }

    /// <summary>True if (Count == 0).</summary>
    public bool IsEmpty { get { return _count == 0; } }

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
      Locate<T> locate,
      Compare<M> compare,
      Average<M> average)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions.");

      if (min.Length > _maxDimensions)
        throw new Error("you are sorting on +" + _maxDimensions + 
          " dimensions. if wish to do this, remove this exception at your own risk.");

      for (int i = 0; i < min.Length; i++)
        if (min[i] == null)
          throw new Error("null reference during omni-tree construction.");

      for (int i = 0; i < max.Length; i++)
        if (max[i] == null)
          throw new Error("null reference during omni-tree construction.");

      this._load = 7; // can you tell this is my fav # yet?
      this._top = new Leaf(min, max, null, _load);
      this._count = 0;
      this._locate = locate;
      this._average = average;
      this._compare = compare;
      this._dimensions = min.Length;
      this._children = Omnitree_Linked<T, M>.TwoPower(this._dimensions);
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
    }

    /// <summary>Constructor for an Omnitree_Linked.</summary>
    /// <param name="min">The minimum values of the tree.</param>
    /// <param name="max">The maximum values of the tree.</param>
    /// <param name="locate">A function for locating an item along the provided dimensions.</param>
    /// <param name="compare">A function for comparing two items of the types of the axis.</param>
    /// <param name="average">A function for computing the average between two items of the axis types.</param>
    public Omnitree_Linked(
      M[] min, M[] max,
      LocateOut<T> locate,
      Compare<M> compare,
      Average<M> average)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");

      if (min.Length > _maxDimensions)
        throw new Error("you are sorting on +" + _maxDimensions +
          " dimensions. if wish to do this, remove this exception at your own risk.");

      for (int i = 0; i < min.Length; i++)
        if (min[i] == null)
          throw new Error("null reference during omni-tree construction.");

      for (int i = 0; i < max.Length; i++)
        if (max[i] == null)
          throw new Error("null reference during omni-tree construction.");

      this._load = 7; // can you tell this is my fav # yet?
      this._top = new Leaf(min, max, null, _load);
      this._count = 0;
      // This is just an adapter so my CodeProject article will still work...
      this._locate = (T item) => { M[] ms; locate(item, out ms); return ms; };
      this._average = average;
      this._compare = compare;
      this._dimensions = min.Length;
      this._children = Omnitree_Linked<T, M>.TwoPower(this._dimensions);
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
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
      Locate<T> locate,
      Compare<M> compare,
      Average<M> average,
      int load)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");

      if (min.Length > _maxDimensions)
        throw new Error("you are sorting on +" + _maxDimensions +
          " dimensions. if wish to do this, remove this exception at your own risk.");
      
      this._load = load;
      this._top = new Leaf(min, max, null, _load);
      this._count = 0;
      this._locate = locate;
      this._average = average;
      this._compare = compare;
      this._dimensions = min.Length;
      this._children = Omnitree_Linked<T, M>.TwoPower(this._dimensions);
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
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
      LocateOut<T> locate,
      Compare<M> compare,
      Average<M> average,
      int load)
    {
      if (min.Length != max.Length)
        throw new Error("min/max values for omnitree mismatch dimensions");

      if (min.Length > _maxDimensions)
        throw new Error("you are sorting on +" + _maxDimensions +
          " dimensions. if wish to do this, remove this exception at your own risk.");

      this._load = load;
      this._top = new Leaf(min, max, null, _load);
      this._count = 0;
      // This is just an adapter so my CodeProject article will still work...
      this._locate = (T item) => { M[] ms; locate(item, out ms); return ms; };
      this._average = average;
      this._compare = compare;
      this._dimensions = min.Length;
      this._children = Omnitree_Linked<T, M>.TwoPower(this._dimensions);
      this._loadPlusOneSquared = (this._load + 1) * (this._load + 1);
      this._loadSquared = this._load * this._load;
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

      M[] ms = this._locate(addition);

      if (ms.Length != this._dimensions)
        throw new Error("the location function for omnitree is invalid.");

      if (!InclusionCheck(this._top, ms))
        throw new Error("out of bounds during addition");

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
        Leaf leaf = (Leaf)node;
        if (!leaf.IsFull)
        {
          leaf.Add(addition);
          return;
        }
        else
        {
          Branch parent = node.Parent;
          Branch growth;
          T[] contents = ((Leaf)node).Contents;
          if (parent == null)
            growth = (Branch)(_top = new Branch(_top.Min, _top.Max, null, this._children));
          else
            growth = GrowBranch(parent, leaf.Min, leaf.Max, this.DetermineChild(parent, ms));

          foreach (T item in contents)
          {
            M[] child_ms = this._locate(item);

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
        Branch branch = (Branch)node;
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
    public void Remove(M[] min, M[] max, Equate<T> where)
    {
      throw new System.NotImplementedException("In Development...");

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
      return (Branch)branch.Children[child];
    }

    /// <summary>Grows a leaf on the tree at the desired location.</summary>
    /// <param name="branch">The branch to grow a leaf on.</param>
    /// <param name="child">The index to grow a leaf on.</param>
    /// <returns>The constructed leaf.</returns>
    private Leaf GrowLeaf(Branch branch, int child)
    {
      if (branch.Children[child] != null)
        throw new Error("My octree has a glitched, sorry.");
      M[] min, max;
      this.DetermineChildBounds(branch, child, out min, out max);
      branch.Children[child] = new Leaf(min, max, branch, _load);
      return (Leaf)branch.Children[child];
    }

    /// <summary>Computes 2 ^ power.</summary>
    /// <param name="power">The degree to power 2 to.</param>
    /// <returns>2 ^ power.</returns>
    public static int TwoPower(int power)
    {
      int result = 1;
      while (power-- > 0)
        result <<= 1;
      return result;
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
          child += Omnitree_Linked<T, M>.TwoPower(i);
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
        int temp = Omnitree_Linked<T, M>.TwoPower(i);
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
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(node.Max[j], min[j]) == Comparison.Less ||
          this._compare(node.Min[j], max[j]) == Comparison.Greater)
          return false;
      return true;
    }

    /// <summary>Checks a node for inclusion of specific coordinates.</summary>
    /// <param name="node">The node to check for inclusion with the coordinates.</param>
    /// <param name="ms">The coordinates to check for inclusion with the node.</param>
    /// <returns>True if the node includes the coordinates; False if not.</returns>
    private bool InclusionCheck(Node node, M[] ms)
    {
      for (int j = 0; j < this._dimensions; j++)
        if (this._compare(ms[j], node.Min[j]) == Comparison.Less ||
          this._compare(ms[j], node.Max[j]) == Comparison.Greater)
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
      if (!(branch.Children[child] is Leaf) || ((Leaf)branch.Children[child]).Count > 1)
        throw new Error("There is a glitch in my octree, sorry.");
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
      if (branch.Children[child] == null)
        throw new Error("There is a glitch in my octree, sorry...");
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
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms = this._locate(items[i]);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              function(items[i]);
          }
        }
        else
        {
          Node[] children = ((Branch)node).Children;
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
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms = this._locate(items[i]);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              function(ref items[i]);
          }
        }
        else
        {
          Node[] children = ((Branch)node).Children;
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
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms = this._locate(items[i]);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              if (function(items[i]) == ForeachStatus.Break)
                return ForeachStatus.Break;
          }
        }
        else
        {
          Node[] children = ((Branch)node).Children;
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
          int count = ((Leaf)node).Count;
          T[] items = ((Leaf)node).Contents;
          for (int i = 0; i < count; i++)
          {
            M[] ms = this._locate(items[i]);

            if (ms.Length != this._dimensions)
              throw new Error("the location function for omnitree is invalid.");

            if (InclusionCheck(min, max, ms))
              if (function(ref items[i]) == ForeachStatus.Break)
                return ForeachStatus.Break;
          }
        }
        else
        {
          Node[] children = ((Branch)node).Children;
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
      throw new System.NotImplementedException("Sorry, I'm working on it.");
    }

    /// <summary>Returns the tree to an empty state.</summary>
    public void Clear()
    {
      this._load = 7;
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
