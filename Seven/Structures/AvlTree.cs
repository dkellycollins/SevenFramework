// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

// THIS FILE CONTAINS ETERNAL CITATIONS

using System;
using Seven;
using Seven.Parallels;

namespace Seven.Structures
{
  /// <summary>Polymorphism base for all AVL trees in the Seven framework.</summary>
  /// <typeparam name="Type"></typeparam>
  public interface AvlTree<Type> : Structure<Type>
  {
    void Add(Type addition);
    //bool TryAdd(Type addition);
    //void Remove(Type removal);
    //bool TryRemove(Type removal);
    int Count { get; }
    bool IsEmpty { get; }
    void Clear();

    bool Contains<Key>(Key key, Compare<Type, Key> comparison);
    Type Get<Key>(Key get, Compare<Type, Key> comparison);
    bool TryGet<Key>(Key get, Compare<Type, Key> comparison, out Type item);
    void Remove<Key>(Key removal, Compare<Type, Key> comparison);
    bool TryRemove<Key>(Key removal, Compare<Type, Key> comparison);

    /// <summary>Foreach loop that takes advantage of the AvlTree structure.</summary>
    /// <param name="function"></param>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    void Foreach(Foreach<Type> function, Type minimum, Type maximum);
    /// <summary>Foreach loop that takes advantage of the AvlTree structure.</summary>
    /// <param name="function"></param>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    void Foreach(ForeachRef<Type> function, Type minimum, Type maximum);
    /// <summary>Foreach loop that takes advantage of the AvlTree structure.</summary>
    /// <param name="function"></param>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    /// <returns></returns>
    ForeachStatus Foreach(ForeachBreak<Type> function, Type minimum, Type maximum);
    /// <summary>Foreach loop that takes advantage of the AvlTree structure.</summary>
    /// <param name="function"></param>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    /// <returns></returns>
    ForeachStatus Foreach(ForeachRefBreak<Type> function, Type minimum, Type maximum);
  }

  /// <summary>Contains extensions for generic AvlTrees.</summary>
  public static class AvlTree
  {
    // currently none
  }

  /// <summary>Implements an AVL Tree where the items are sorted by string id values.</summary>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  /// <citation>
  /// This AVL tree imlpementation was originally developed by 
  /// Rodney Howell of Kansas State University. However, it has 
  /// been modified since its addition into the Seven framework.
  /// </citation>
  [Serializable]
  public class AvlTree_Linked<Type> : AvlTree<Type>
  {
    #region Node

    /// <summary>This class just holds the data for each individual node of the tree.</summary>
    public class Node
    {
      private Type _value;
      private Node _leftChild;
      private Node _rightChild;
      private int _height;

      public Type Value { get { return _value; } set { _value = value; } }
      public Node LeftChild { get { return _leftChild; } set { _leftChild = value; } }
      public Node RightChild { get { return _rightChild; } set { _rightChild = value; } }
      public int Height { get { return _height; } set { _height = value; } }

      public Node(Type value)
      {
        _value = value;
        _leftChild = null;
        _rightChild = null;
        _height = 0;
      }
    }

    #endregion

    internal Node _avlTree;
    protected int _count;

    protected Compare<Type> _compare;

    public int SizeOf { get { return _count; } }

    /// <summary>Gets the number of elements in the collection.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int Count { get { return _count; } }
    /// <summary>Gets whether the binary search tree is empty.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public bool IsEmpty { get { return _avlTree == null; } }

    /// <summary>Constructs an AVL Tree.</summary>
    /// <param name="compare">The comparison function for sorting the items.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public AvlTree_Linked(Compare<Type> compare)
    {
      _avlTree = null;
      _count = 0;
      _compare = compare;
    }

    #region Recursive Versions

    // THE FOLLOWING FUNCTIONS ARE RECURSIVE VERSIONS OF THE EXISTING 
    // MEMBERS WITHIN THIS CLASS. THEY HAVE THE SAME FUNCTIONALITY.

    //public Type Get(string id)
    //{
    //  ReaderLock();
    //   Type returnValue = Get(id, _avlTree);
    //   ReaderUnlock();
    //   return returnValue;
    //}

    //private Type Get(string id, AvlTreeNode avlTree)
    //{
    //  if (avlTree == null)
    //    throw new AvlTreeException("Attempting to get a non-existing value: " + id + ".");
    //  int compResult = id.CompareTo(avlTree.Value.Id);
    //  if (compResult == 0)
    //    return avlTree.Value;
    //  else if (compResult < 0)
    //    return Get(id, avlTree.LeftChild);
    //  else
    //    return Get(id, avlTree.RightChild);
    //}

    //public bool Contains(string id)
    //{
    //   ReaderLock();
    //   bool returnValue = Contains(id, _avlTree);
    //   ReaderUnlock();
    //   return returnValue;
    //}

    //private bool Contains(string id, AvlTreeNode avlTree)
    //{
    //  if (avlTree == null) return false;
    //  int compareResult = id.CompareTo(avlTree.Value.Id);
    //  if (compareResult == 0) return true;
    //  else if (compareResult < 0) return Contains(id, avlTree.LeftChild);
    //  else return Contains(id, avlTree.RightChild);
    //}

    #endregion

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      Stack<Node> forks = new Stack_Linked<Node>();
      Node current = _avlTree;
      while (current != null || forks.Count > 0)
      {
        if (current != null)
        {
          forks.Push(current);
          current = current.LeftChild;
        }
        else if (forks.Count > 0)
        {
          current = forks.Pop();
          yield return current.Value;
          current = current.RightChild;
        }
      }
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.Generic.IEnumerator<Type>
      System.Collections.Generic.IEnumerable<Type>.GetEnumerator()
    {
      Stack<Node> forks = new Stack_Linked<Node>();
      Node current = _avlTree;
      while (current != null || forks.Count > 0)
      {
        if (current != null)
        {
          forks.Push(current);
          current = current.LeftChild;
        }
        else if (forks.Count > 0)
        {
          current = forks.Pop();
          yield return current.Value;
          current = current.RightChild;
        }
      }
    }

    public virtual bool Contains<Key>(Key key, Compare<Type, Key> comparison)
    {
      // THIS THIS THE ITERATIVE VERSION OF THIS FUNCTION. THERE IS A RECURSIVE
      // VERSION IN THE "RECURSIVE" REGION SHOULD YOU WISH TO SEE IT.
      Node _current = _avlTree;
      while (_current != null)
      {
        Comparison compareResult = comparison(_current.Value, key);
        if (compareResult == Comparison.Equal)
          return true;
        else if (compareResult == Comparison.Greater)
          _current = _current.LeftChild;
        else // (compareResult == Copmarison.Less)
          _current = _current.RightChild;
      }
      return false;
    }

    /// <summary>Gets the item with the designated by the string.</summary>
    /// <param name="id">The string ID to look for.</param>
    /// <returns>The object with the desired string ID if it exists.</returns>
    /// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    public virtual Type Get<Key>(Key get, Compare<Type, Key> comparison)
    {
      // THIS THIS THE ITERATIVE VERSION OF THIS FUNCTION. THERE IS A RECURSIVE
      // VERSION IN THE "RECURSIVE" REGION SHOULD YOU WISH TO SEE IT.
      Node _current = _avlTree;
      while (_current != null)
      {
        Comparison compareResult = comparison(_current.Value, get);
        if (compareResult == Comparison.Equal)
          return _current.Value;
        else if (compareResult == Comparison.Greater)
          _current = _current.LeftChild;
        else // (compareResult == Copmarison.Less)
          _current = _current.RightChild;
      }
      throw new Exception("Attempting to get a non-existing value: " + get.ToString() + ".");
    }

    /// <summary>Gets the item with the designated by the string.</summary>
    /// <param name="id">The string ID to look for.</param>
    /// <returns>The object with the desired string ID if it exists.</returns>
    /// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    public virtual bool TryGet<Key>(Key get, Compare<Type, Key> comparison, out Type result)
    {
      // THIS THIS THE ITERATIVE VERSION OF THIS FUNCTION. THERE IS A RECURSIVE
      // VERSION IN THE "RECURSIVE" REGION SHOULD YOU WISH TO SEE IT.
      Node _current = _avlTree;
      while (_current != null)
      {
        Comparison compareResult = comparison(_current.Value, get);
        if (compareResult == Comparison.Equal)
        {
          result = _current.Value;
          return true;
        }
        else if (compareResult == Comparison.Greater)
          _current = _current.LeftChild;
        else // (compareResult == Comparison.Less)
          _current = _current.RightChild;
      }
      result = default(Type);
      return false;
    }

    /// <summary>Adds an object to the AVL Tree.</summary>
    /// <param name="addition">The object to add.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public virtual void Add(Type addition)
    {
      this._avlTree = Add(addition, this._avlTree);
      this._count++;
    }

    protected virtual Node Add(Type addition, Node avlTree)
    {
      if (avlTree == null) return new Node(addition);
      Comparison compareResult = this._compare(avlTree.Value, addition);
      if (compareResult == Comparison.Equal)
        throw new Exception("Attempting to add an already existing id exists.");
      else if (compareResult == Comparison.Greater) 
       avlTree.LeftChild = Add(addition, avlTree.LeftChild);
      else // (compareResult == Comparison.Less)
        avlTree.RightChild = Add(addition, avlTree.RightChild);
      return Balance(avlTree);
    }

    /// <summary>Adds an object to the AVL Tree.</summary>
    /// <param name="addition">The object to add.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public virtual bool TryAdd(Type addition)
    {
      bool added;
      this._avlTree = TryAdd(addition, _avlTree, out added);
      this._count++;
      return added;
    }

    protected Node TryAdd(Type addition, Node avlTree, out bool added)
    {
      if (avlTree == null)
      {
        added = true;
        return new Node(addition);
      }
      Comparison compareResult = _compare(avlTree.Value, addition);
      if (compareResult == Comparison.Equal)
      {
        added = false;
        return avlTree;
      }
      else if (compareResult == Comparison.Greater)
        avlTree.LeftChild = TryAdd(addition, avlTree.LeftChild, out added);
      else // (compareResult == Comparison.Less)
        avlTree.RightChild = TryAdd(addition, avlTree.RightChild, out added);
      return Balance(avlTree);
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public virtual void Remove(Type removal)
    {
      _avlTree = Remove(removal, _avlTree);
      _count--;
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <param name="avlTree">The binary tree to remove from.</param>
    /// <returns>The resulting tree after the removal.</returns>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    protected Node Remove(Type removal, Node avlTree)
    {
      if (avlTree != null)
      {
        Comparison compareResult = _compare(avlTree.Value, removal);
        if (compareResult == Comparison.Equal)
        {
          if (avlTree.RightChild != null)
          {
            Node leftMostOfRight;
            avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out leftMostOfRight);
            leftMostOfRight.RightChild = avlTree.RightChild;
            leftMostOfRight.LeftChild = avlTree.LeftChild;
            avlTree = leftMostOfRight;
          }
          else if (avlTree.LeftChild != null)
          {
            Node rightMostOfLeft;
            avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out rightMostOfLeft);
            rightMostOfLeft.RightChild = avlTree.RightChild;
            rightMostOfLeft.LeftChild = avlTree.LeftChild;
            avlTree = rightMostOfLeft;
          }
          else return null;
          SetHeight(avlTree);
          return Balance(avlTree);
        }
        else if (compareResult == Comparison.Greater)
          avlTree.LeftChild = Remove(removal, avlTree.LeftChild);
        else // (compareResult == Comparison.Less)
          avlTree.RightChild = Remove(removal, avlTree.RightChild);
        SetHeight(avlTree);
        return Balance(avlTree);
      }
      throw new Exception("Attempting to remove a non-existing entry.");
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public virtual bool TryRemove(Type removal)
    {
      try
      {
        _avlTree = Remove(removal, _avlTree);
        _count--;
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public virtual void Remove<Key>(Key removal, Compare<Type, Key> comparison)
    {
      _avlTree = Remove(removal, comparison, _avlTree);
      _count--;
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <param name="avlTree">The binary tree to remove from.</param>
    /// <returns>The resulting tree after the removal.</returns>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    protected Node Remove<Key>(Key removal, Compare<Type, Key> comparison, Node avlTree)
    {
      if (avlTree != null)
      {
        Comparison compareResult = comparison(avlTree.Value, removal);
        if (compareResult == Comparison.Equal)
        {
          if (avlTree.RightChild != null)
          {
            Node leftMostOfRight;
            avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out leftMostOfRight);
            leftMostOfRight.RightChild = avlTree.RightChild;
            leftMostOfRight.LeftChild = avlTree.LeftChild;
            avlTree = leftMostOfRight;
          }
          else if (avlTree.LeftChild != null)
          {
            Node rightMostOfLeft;
            avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out rightMostOfLeft);
            rightMostOfLeft.RightChild = avlTree.RightChild;
            rightMostOfLeft.LeftChild = avlTree.LeftChild;
            avlTree = rightMostOfLeft;
          }
          else return null;
          SetHeight(avlTree);
          return Balance(avlTree);
        }
        else if (compareResult == Comparison.Greater)
          avlTree.LeftChild = Remove<Key>(removal, comparison, avlTree.LeftChild);
        else // (compareResult == Comparison.Less)
          avlTree.RightChild = Remove<Key>(removal, comparison, avlTree.RightChild);
        SetHeight(avlTree);
        return Balance(avlTree);
      }
      throw new Exception("Attempting to remove a non-existing entry.");
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public virtual bool TryRemove<Key>(Key removal, Compare<Type, Key> comparison)
    {
      try
      {
        _avlTree = Remove(removal, comparison, _avlTree);
        _count--;
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>Removes the left-most child of an AVL Tree node and returns it 
    /// through the out parameter.</summary>
    /// <param name="avlTree">The tree to remove the left-most child from.</param>
    /// <param name="leftMost">The left-most child of this AVL tree.</param>
    /// <returns>The updated tree with the removal.</returns>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    protected Node RemoveLeftMost(Node avlTree, out Node leftMost)
    {
      if (avlTree.LeftChild == null) { leftMost = avlTree; return null; }
      avlTree.LeftChild = RemoveLeftMost(avlTree.LeftChild, out leftMost);
      SetHeight(avlTree);
      return Balance(avlTree);
    }

    /// <summary>Removes the right-most child of an AVL Tree node and returns it 
    /// through the out parameter.</summary>
    /// <param name="avlTree">The tree to remove the right-most child from.</param>
    /// <param name="leftMost">The right-most child of this AVL tree.</param>
    /// <returns>The updated tree with the removal.</returns>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    protected Node RemoveRightMost(Node avlTree, out Node rightMost)
    {
      if (avlTree.RightChild == null) { rightMost = avlTree; return null; }
      avlTree.LeftChild = RemoveLeftMost(avlTree.RightChild, out rightMost);
      SetHeight(avlTree);
      return Balance(avlTree);
    }

    /// <summary>This is just a protection against the null valued leaf nodes, 
    /// which have a height of "-1".</summary>
    /// <param name="avlTree">The AVL Tree to find the hight of.</param>
    /// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    protected int Height(Node avlTree)
    {
      if (avlTree == null) return -1;
      else return avlTree.Height;
    }

    /// <summary>Sets the height of a tree based on its children's heights.</summary>
    /// <param name="avlTree">The tree to have its height adjusted.</param>
    /// <remarks>Runtime: O(1).</remarks>
    protected void SetHeight(Node avlTree)
    {
      if (Height(avlTree.LeftChild) < Height(avlTree.RightChild))
        avlTree.Height = Math.Max(Height(avlTree.LeftChild), Height(avlTree.RightChild)) + 1;
    }

    /// <summary>Standard balancing algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to check the balancing of.</param>
    /// <returns>The result of the possible balancing.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    protected Node Balance(Node avlTree)
    {
      if (Height(avlTree.LeftChild) == Height(avlTree.RightChild) + 2)
      {
        if (Height(avlTree.LeftChild.LeftChild) > Height(avlTree.RightChild))
          return SingleRotateRight(avlTree);
        else return DoubleRotateRight(avlTree);
      }
      else if (Height(avlTree.RightChild) == Height(avlTree.LeftChild) + 2)
      {
        if (Height(avlTree.RightChild.RightChild) > Height(avlTree.LeftChild))
          return SingleRotateLeft(avlTree);
        else return DoubleRotateLeft(avlTree);
      }
      SetHeight(avlTree);
      return avlTree;
    }

    /// <summary>Standard single rotation (to the right) algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to single rotate right.</param>
    /// <returns>The resulting tree.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    protected Node SingleRotateRight(Node avlTree)
    {
      Node temp = avlTree.LeftChild;
      avlTree.LeftChild = temp.RightChild;
      temp.RightChild = avlTree;
      SetHeight(avlTree);
      SetHeight(temp);
      return temp;
    }

    /// <summary>Standard single rotation (to the left) algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to single rotate left.</param>
    /// <returns>The resulting tree.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    protected Node SingleRotateLeft(Node avlTree)
    {
      Node temp = avlTree.RightChild;
      avlTree.RightChild = temp.LeftChild;
      temp.LeftChild = avlTree;
      SetHeight(avlTree);
      SetHeight(temp);
      return temp;
    }

    /// <summary>Standard double rotation (to the right) algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to float rotate right.</param>
    /// <returns>The resulting tree.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    protected Node DoubleRotateRight(Node avlTree)
    {
      Node temp = avlTree.LeftChild.RightChild;
      avlTree.LeftChild.RightChild = temp.LeftChild;
      temp.LeftChild = avlTree.LeftChild;
      avlTree.LeftChild = temp.RightChild;
      temp.RightChild = avlTree;
      SetHeight(temp.LeftChild);
      SetHeight(temp.RightChild);
      SetHeight(temp);
      return temp;
    }

    /// <summary>Standard double rotation (to the left) algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to float rotate left.</param>
    /// <returns>The resulting tree.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    protected Node DoubleRotateLeft(Node avlTree)
    {
      Node temp = avlTree.RightChild.LeftChild;
      avlTree.RightChild.LeftChild = temp.RightChild;
      temp.RightChild = avlTree.RightChild;
      avlTree.RightChild = temp.LeftChild;
      temp.LeftChild = avlTree;
      SetHeight(temp.LeftChild);
      SetHeight(temp.RightChild);
      SetHeight(temp);
      return temp;
    }

    /// <summary>Returns the tree to an iterative state.</summary>
    public virtual void Clear() { _avlTree = null; _count = 0; }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public void Foreach(Foreach<Type> function)
    {
      this.ForeachInOrder(function);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public void Foreach(ForeachRef<Type> function)
    {
      this.ForeachInOrder(function);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public ForeachStatus Foreach(ForeachBreak<Type> function)
    {
      return this.ForeachInOrder(function);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      return this.ForeachInOrder(function);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public void Foreach(Foreach<Type> function, Type minimum, Type maximum)
    {
      this.ForeachInOrder(function, minimum, maximum);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public void Foreach(ForeachRef<Type> function, Type minimum, Type maximum)
    {
      this.ForeachInOrder(function, minimum, maximum);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public ForeachStatus Foreach(ForeachBreak<Type> function, Type minimum, Type maximum)
    {
      return this.ForeachInOrder(function, minimum, maximum);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function, Type minimum, Type maximum)
    {
      return this.ForeachInOrder(function, minimum, maximum);
    }

    //public IEnumerator<Type> GetEnumerator()
    //{
    //  return AvlTree_Linked<Type>.GetEnumerator(this._avlTree);
    //}
    //private static IEnumerator<Type> GetEnumerator(Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    AvlTree_Linked<Type>.GetEnumerator(avltreeNode.LeftChild);
    //    yield return avltreeNode.Value;
    //    AvlTree_Linked<Type>.GetEnumerator(avltreeNode.RightChild);
    //  }
    //}

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual void ForeachInOrder(Foreach<Type> function)
    {
      AvlTree_Linked<Type>.TraversalInOrder(function, _avlTree);
    }
    protected static bool TraversalInOrder(Foreach<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.LeftChild);
        function(avltreeNode.Value);
        AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.RightChild);
      }
      return true;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual void ForeachInOrder(Foreach<Type> function, Type minimum, Type maximum)
    {
      if (this._compare(minimum, maximum) == Comparison.Greater)
        throw new Exception("invalid minimum and maximum values on Avl traversal.");
      AvlTree_Linked<Type>.TraversalInOrder(function, _avlTree);
    }
    protected void TraversalInOrder(Foreach<Type> function, Node node, Type minimum, Type maximum)
    {
      if (node != null)
      {
        if (this._compare(node.Value, minimum) == Comparison.Less)
          this.TraversalInOrder(function, node.RightChild, minimum, maximum);
        else if (this._compare(node.Value, maximum) == Comparison.Greater)
          this.TraversalInOrder(function, node.LeftChild, minimum, maximum);
        else
        {
          this.TraversalInOrder(function, node.LeftChild, minimum, maximum);
          function(node.Value);
          this.TraversalInOrder(function, node.RightChild, minimum, maximum);
        }
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual void ForeachInOrder(ForeachRef<Type> function)
    {
      AvlTree_Linked<Type>.TraversalInOrder(function, _avlTree);
    }
    protected static bool TraversalInOrder(ForeachRef<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.LeftChild);
        Type value = avltreeNode.Value;
        function(ref value);
        avltreeNode.Value = value;
        AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.RightChild);
      }
      return true;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual void ForeachInOrder(ForeachRef<Type> function, Type minimum, Type maximum)
    {
      if (this._compare(minimum, maximum) == Comparison.Greater)
        throw new Exception("invalid minimum and maximum values on Avl traversal.");
      this.TraversalInOrder(function, _avlTree, minimum, maximum);
    }
    protected void TraversalInOrder(ForeachRef<Type> function, Node node, Type minimum, Type maximum)
    {
      if (node != null)
      {
        if (this._compare(node.Value, minimum) == Comparison.Less)
          this.TraversalInOrder(function, node.RightChild, minimum, maximum);
        else if (this._compare(node.Value, maximum) == Comparison.Greater)
          this.TraversalInOrder(function, node.LeftChild, minimum, maximum);
        else
        {
          this.TraversalInOrder(function, node.LeftChild, minimum, maximum);
          Type temp = node.Value;
          function(ref temp);
          node.Value = temp;
          this.TraversalInOrder(function, node.RightChild, minimum, maximum);
        }
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual ForeachStatus ForeachInOrder(ForeachBreak<Type> function)
    {
      return AvlTree_Linked<Type>.ForeachInOrder(function, _avlTree);
    }
    protected static ForeachStatus ForeachInOrder(ForeachBreak<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        if (AvlTree_Linked<Type>.ForeachInOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
        Type value = avltreeNode.Value;
        ForeachStatus status = function(value);
        avltreeNode.Value = value;
        if (status == ForeachStatus.Break)
          return ForeachStatus.Break;
        if (AvlTree_Linked<Type>.ForeachInOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual ForeachStatus ForeachInOrder(ForeachBreak<Type> function, Type minimum, Type maximum)
    {
      if (this._compare(minimum, maximum) == Comparison.Greater)
        throw new Exception("invalid minimum and maximum values on Avl traversal.");
      return this.ForeachInOrder(function, _avlTree, minimum, maximum);
    }
    protected ForeachStatus ForeachInOrder(ForeachBreak<Type> function, Node node, Type minimum, Type maximum)
    {
      if (node != null)
      {
        if (this._compare(node.Value, minimum) == Comparison.Less)
          this.ForeachInOrder(function, node.RightChild, minimum, maximum);
        else if (this._compare(node.Value, maximum) == Comparison.Greater)
          this.ForeachInOrder(function, node.LeftChild, minimum, maximum);
        else
        {
          if (this.ForeachInOrder(function, node.LeftChild, minimum, maximum) == ForeachStatus.Break)
            return ForeachStatus.Break;
          Type value = node.Value;
          ForeachStatus status = function(value);
          node.Value = value;
          if (status == ForeachStatus.Break)
            return ForeachStatus.Break;
          if (this.ForeachInOrder(function, node.RightChild, minimum, maximum) == ForeachStatus.Break)
            return ForeachStatus.Break;
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual ForeachStatus ForeachInOrder(ForeachRefBreak<Type> function)
    {
      return AvlTree_Linked<Type>.ForeachInOrder(function, _avlTree);
    }
    protected static ForeachStatus ForeachInOrder(ForeachRefBreak<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        if (AvlTree_Linked<Type>.ForeachInOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
        Type value = avltreeNode.Value;
        ForeachStatus status = function(ref value);
        avltreeNode.Value = value;
        if (status == ForeachStatus.Break)
          return ForeachStatus.Break;
        if (AvlTree_Linked<Type>.ForeachInOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual ForeachStatus ForeachInOrder(ForeachRefBreak<Type> function, Type minimum, Type maximum)
    {
      if (this._compare(minimum, maximum) == Comparison.Greater)
        throw new Exception("invalid minimum and maximum values on Avl traversal.");
      return this.ForeachInOrder(function, _avlTree, minimum, maximum);
    }
    protected ForeachStatus ForeachInOrder(ForeachRefBreak<Type> function, Node node, Type minimum, Type maximum)
    {
      if (node != null)
      {
        if (this._compare(node.Value, minimum) == Comparison.Less)
          this.ForeachInOrder(function, node.RightChild, minimum, maximum);
        else if (this._compare(node.Value, maximum) == Comparison.Greater)
          this.ForeachInOrder(function, node.LeftChild, minimum, maximum);
        else
        {
          if (this.ForeachInOrder(function, node.LeftChild, minimum, maximum) == ForeachStatus.Break)
            return ForeachStatus.Break;
          Type value = node.Value;
          ForeachStatus status = function(ref value);
          node.Value = value;
          if (status == ForeachStatus.Break)
            return ForeachStatus.Break;
          if (this.ForeachInOrder(function, node.RightChild, minimum, maximum) == ForeachStatus.Break)
            return ForeachStatus.Break;
        }
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual void ForeachPreOrder(Foreach<Type> function)
    {
      AvlTree_Linked<Type>.TraversalPreOrder(function, _avlTree);
    }
    protected static bool TraversalPreOrder(Foreach<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        function(avltreeNode.Value);
        AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.LeftChild);
        AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.RightChild);
      }
      return true;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual void ForeachPreOrder(ForeachRef<Type> function)
    {
      AvlTree_Linked<Type>.TraversalPreOrder(function, _avlTree);
    }
    protected static bool TraversalPreOrder(ForeachRef<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        Type value = avltreeNode.Value;
        function(ref value);
        avltreeNode.Value = value;
        AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.LeftChild);
        AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.RightChild);
      }
      return true;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual ForeachStatus ForeachPreOrder(ForeachBreak<Type> function)
    {
      return AvlTree_Linked<Type>.TraversalPreOrder(function, _avlTree);
    }
    protected static ForeachStatus TraversalPreOrder(ForeachBreak<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        Type value = avltreeNode.Value;
        ForeachStatus status = function(value);
        avltreeNode.Value = value;
        if (status == ForeachStatus.Break)
          return ForeachStatus.Break;
        if (AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
        if (AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual ForeachStatus ForeachPreOrder(ForeachRefBreak<Type> function)
    {
      return AvlTree_Linked<Type>.TraversalPreOrder(function, _avlTree);
    }
    protected static ForeachStatus TraversalPreOrder(ForeachRefBreak<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        Type value = avltreeNode.Value;
        ForeachStatus status = function(ref value);
        avltreeNode.Value = value;
        if (status == ForeachStatus.Break)
          return ForeachStatus.Break;
        if (AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
        if (AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual void ForeachPostOrder(Foreach<Type> function)
    {
      AvlTree_Linked<Type>.TraversalPostOrder(function, _avlTree);
    }
    protected static bool TraversalPostOrder(Foreach<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.RightChild);
        function(avltreeNode.Value);
        AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.LeftChild);
      }
      return true;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual void ForeachPostOrder(ForeachRef<Type> function)
    {
      AvlTree_Linked<Type>.TraversalPostOrder(function, _avlTree);
    }
    protected static bool TraversalPostOrder(ForeachRef<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.RightChild);
        Type value = avltreeNode.Value;
        function(ref value);
        avltreeNode.Value = value;
        AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.LeftChild);
      }
      return true;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual ForeachStatus ForeachPostOrder(ForeachBreak<Type> function)
    {
      return AvlTree_Linked<Type>.TraversalPostOrder(function, _avlTree);
    }
    protected static ForeachStatus TraversalPostOrder(ForeachBreak<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        if (AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
        Type value = avltreeNode.Value;
        ForeachStatus status = function(value);
        avltreeNode.Value = value;
        if (status == ForeachStatus.Break)
          return ForeachStatus.Break;
        if (AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public virtual ForeachStatus ForeachPostOrder(ForeachRefBreak<Type> function)
    {
      return AvlTree_Linked<Type>.TraversalPostOrder(function, _avlTree);
    }
    protected static ForeachStatus TraversalPostOrder(ForeachRefBreak<Type> function, Node avltreeNode)
    {
      if (avltreeNode != null)
      {
        if (AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
        Type value = avltreeNode.Value;
        ForeachStatus status = function(ref value);
        avltreeNode.Value = value;
        if (status == ForeachStatus.Break)
          return ForeachStatus.Break;
        if (AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
          return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Creates an array out of the values in this structure.</summary>
    /// <returns>An array containing the values in this structure.</returns>
    /// <remarks>Runtime: Theta(n).</remarks>
    public virtual Type[] ToArray()
    {
      Type[] array = new Type[_count];
      ToArray(array, _avlTree, 0);
      return array;
    }
    protected void ToArray(Type[] array, Node avltreeNode, int position)
    {
      if (avltreeNode != null)
      {
        ToArray(array, avltreeNode.LeftChild, position);
        array[position++] = avltreeNode.Value;
        ToArray(array, avltreeNode.RightChild, position);
      }
    }

    //public void Traverse(Foreach<Type> function, Type minimum, Type maximum)
    //{
    //  this.Traverse
    //  (
    //    (Type current) =>
    //    {
    //      if (current == null)
    //        return AvlTree.Move.Break;
    //      else if (this._compare(current, minimum) == Comparison.Less)
    //        return AvlTree.Move.RightChild;
    //      else if (this._compare(current, maximum) == Comparison.Greater)
    //        return AvlTree.Move.LeftChild;
    //      else
    //      {

    //      }
    //    }
    //  );
    //}

    ///// <summary>Allows custom traversals of the structure.</summary>
    ///// <param name="function">The delegate denoting cutom traversal of the structure.</param>
    //public void Traverse(AvlTree.Traversal<Type> function)
    //{
    //  Node node = _avlTree;
    //  while (true)
    //  {
    //    switch (function(node.Value))
    //    {
    //      case AvlTree.Move.LeftChild:
    //        if (node == null)
    //          throw new Exception("invalid AvlTree traversal.");
    //        node = node.LeftChild;
    //        break;
    //      case AvlTree.Move.RightChild:
    //        if (node == null)
    //          throw new Exception("invalid AvlTree traversal.");
    //        node = node.RightChild;
    //        break;
    //      case AvlTree.Move.Parent:
    //        if (node == _avlTree)
    //          throw new Exception("invalid AvlTree traversal.");
    //        Node parent = _avlTree;
    //        while (parent.RightChild != node && parent.LeftChild != node)
    //          if (_compare(parent.Value, node.Value) == Comparison.Less)
    //            parent = parent.RightChild;
    //          else
    //            parent = parent.LeftChild;
    //        node = parent;
    //        break;
    //      case AvlTree.Move.Break:
    //        return;
    //    }
    //  }
    //}

    public Structure<Type> Clone()
    {
      AvlTree_Linked<Type> clone = new AvlTree_Linked<Type>(this._compare);
      foreach (Type current in this)
        clone.Add(current);
      return clone;
    }

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    protected class Exception : Error { public Exception(string message) : base(message) { } }
  }

  /// <summary>Implements an AVL Tree where the items are sorted by string id values.</summary>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [Serializable]
  public class AvlTreeLinkedThreadSafe<Type> : AvlTree_Linked<Type>
  {
    ReaderWriterLock _readerWriterLock;

    /// <summary>Constructs an AVL Tree.</summary>
    /// <param name="compare">The comparison function for sorting the items.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public AvlTreeLinkedThreadSafe(Compare<Type> compare) : base(compare)
    {
      this._readerWriterLock = new ReaderWriterLock();
    }

    /// <summary></summary>
    /// <typeparam name="Key"></typeparam>
    /// <param name="key"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public new bool Contains<Key>(Key key, Compare<Type, Key> compare)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.Contains<Key>(key, compare);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Gets the item with the designated by the string.</summary>
    /// <param name="id">The string ID to look for.</param>
    /// <returns>The object with the desired string ID if it exists.</returns>
    /// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    public new Type Get<Key>(Key get, Compare<Type, Key> comparison)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.Get<Key>(get, comparison);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Gets the item with the designated by the string.</summary>
    /// <param name="id">The string ID to look for.</param>
    /// <returns>The object with the desired string ID if it exists.</returns>
    /// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    public new bool TryGet<Key>(Key get, Compare<Type, Key> compare, out Type result)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.TryGet<Key>(get, compare, out result);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Adds an object to the AVL Tree.</summary>
    /// <param name="addition">The object to add.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public new void Add(Type addition)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.Add(addition);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Adds an object to the AVL Tree.</summary>
    /// <param name="addition">The object to add.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public new bool TryAdd(Type addition)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        return base.TryAdd(addition);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public new void Remove(Type removal)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.Remove(removal);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public new bool TryRemove(Type removal)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        return base.TryRemove(removal);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public new void Remove<Key>(Key removal, Compare<Type, Key> compare)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.Remove<Key>(removal, compare);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public new bool TryRemove<Key>(Key removal, Compare<Type, Key> compare)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        return base.TryRemove<Key>(removal, compare);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Returns the tree to an iterative state.</summary>
    public new void Clear()
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.Clear();
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }
    
    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new void Foreach(Foreach<Type> function)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        base.Foreach(function);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new void Foreach(ForeachRef<Type> function)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.Foreach(function);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new ForeachStatus Foreach(ForeachBreak<Type> function)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.Foreach(function);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        return base.Foreach(function);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new void ForeachInOrder(Foreach<Type> function)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        base.ForeachInOrder(function);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new void ForeachInOrder(ForeachRef<Type> function)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.ForeachInOrder(function);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new ForeachStatus ForeachInOrder(ForeachBreak<Type> function)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.ForeachInOrder(function);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new ForeachStatus ForeachInOrder(ForeachRefBreak<Type> function)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        return base.ForeachInOrder(function);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new void ForeachPreOrder(Foreach<Type> function)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        base.ForeachPreOrder(function);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new void ForeachPreOrder(ForeachRef<Type> function)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.ForeachPreOrder(function);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new ForeachStatus ForeachPreOrder(ForeachBreak<Type> function)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.ForeachPreOrder(function);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new ForeachStatus ForeachPreOrder(ForeachRefBreak<Type> function)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        return base.ForeachPreOrder(function);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new void ForeachPostOrder(Foreach<Type> function)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        base.ForeachPostOrder(function);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new void ForeachPostOrder(ForeachRef<Type> function)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.ForeachPostOrder(function);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new ForeachStatus ForeachPostOrder(ForeachBreak<Type> function)
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.ForeachPostOrder(function);
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public new ForeachStatus ForeachPostOrder(ForeachRefBreak<Type> function)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        return base.ForeachPostOrder(function);
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Creates an array out of the values in this structure.</summary>
    /// <returns>An array containing the values in this structure.</returns>
    /// <remarks>Runtime: Theta(n).</remarks>
    public new Type[] ToArray()
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.ToArray();
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }
  }

  /// <summary>Implements an AVL Tree where the items are sorted by string id values.</summary>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [Serializable]
  public class AvlTree_Array<Type>// : AvlTree<Type>
  {
    public struct Node
    {
      public bool _occupied;
      public Type _value;
    }

    internal Link<bool, Type>[] _avlTree;
    protected int _count;

    protected Compare<Type> _compare;

    /// <summary>Gets the number of elements in the collection.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int Count { get { return _count; } }
    /// <summary>Gets whether the binary search tree is empty.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public bool IsEmpty { get { return _avlTree == null; } }

    /// <summary>Constructs an AVL Tree.</summary>
    /// <param name="compare">The comparison function for sorting the items.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public AvlTree_Array(Compare<Type> compare, int maximumSize)
    {
      if (maximumSize < 1)
        throw new AvlTree_Array<Type>.Exception("");
      _avlTree = new Link<bool,Type>[maximumSize + 1];
      _count = 0;
      _compare = compare;
    }

    private int Height(int index)
    {
      return (index - 1) / 2;
    }

    public virtual bool Contains<Key>(Key key, Compare<Type, Key> comparison)
    {
      // THIS THIS THE ITERATIVE VERSION OF THIS FUNCTION. THERE IS A RECURSIVE
      // VERSION IN THE "RECURSIVE" REGION SHOULD YOU WISH TO SEE IT.
      int current = 1;
      while (_avlTree[current].One != false)
      {
        Comparison compareResult = comparison(_avlTree[current].Two, key);
        if (compareResult == Comparison.Equal)
          return true;
        else if (compareResult == Comparison.Greater)
          current = current * 2; // LeftChild
        else // (compareResult == Copmarison.Less)
          current = current * 2 + 1; // RightChild
      }
      return false;
    }

    ///// <summary>Gets the item with the designated by the string.</summary>
    ///// <param name="id">The string ID to look for.</param>
    ///// <returns>The object with the desired string ID if it exists.</returns>
    ///// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    //public virtual Type Get<Key>(Key get, Compare<Type, Key> comparison)
    //{
    //  int current = 1;
    //  while (_avlTree[current] != null)
    //  {
    //    Comparison compareResult = comparison(_avlTree[current], get);
    //    if (compareResult == Comparison.Equal)
    //      return _avlTree[current];
    //    else if (compareResult == Comparison.Greater)
    //      current = current * 2; // LeftChild
    //    else // (compareResult == Copmarison.Less)
    //      current = current * 2 + 1; // RightChild
    //  }
    //  throw new AvlTree_Array<Type>.Exception
    //    ("Attempting to get a non-existing value: " + get.ToString() + ".");
    //}

    ///// <summary>Gets the item with the designated by the string.</summary>
    ///// <param name="id">The string ID to look for.</param>
    ///// <returns>The object with the desired string ID if it exists.</returns>
    ///// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    //public virtual bool TryGet<Key>(Key get, Compare<Type, Key> comparison, out Type result)
    //{
    //  int current = 1;
    //  while (_avlTree[current] != null)
    //  {
    //    Comparison compareResult = comparison(_avlTree[current], get);
    //    if (compareResult == Comparison.Equal)
    //    {
    //      result = _avlTree[current];
    //      return true;
    //    }
    //    else if (compareResult == Comparison.Greater)
    //      current = current * 2; // LeftChild
    //    else // (compareResult == Comparison.Less)
    //      current = current * 2 + 1; // RightChild
    //  }
    //  result = default(Type);
    //  return false;
    //}

    ///// <summary>Adds an object to the AVL Tree.</summary>
    ///// <param name="addition">The object to add.</param>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //public virtual void Add(Type addition)
    //{
    //  _avlTree = Add(addition, 1);
    //  _count++;
    //}

    //protected virtual void Add(Type addition, int index)
    //{
    //  if (index > _avlTree.Length)
    //    throw new Exception("maximum tree size reached");

    //  if (_avlTree[index] == null)
    //  {
    //    _avlTree[index] = addition;
    //    return
    //  }
    //  Comparison compareResult = _compare(avlTree.Value, addition);
    //  if (compareResult == Comparison.Equal)
    //    throw new AvlTreeLinkedException("Attempting to add an already existing id exists.");
    //  else if (compareResult == Comparison.Greater)
    //    avlTree.LeftChild = Add(addition, avlTree.LeftChild);
    //  else // (compareResult == Comparison.Less)
    //    avlTree.RightChild = Add(addition, avlTree.RightChild);
    //  return Balance(avlTree);
    //}

    ///// <summary>Adds an object to the AVL Tree.</summary>
    ///// <param name="addition">The object to add.</param>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //public virtual bool TryAdd(Type addition)
    //{
    //  bool added;
    //  _avlTree = TryAdd(addition, _avlTree, out added);
    //  _count++;
    //  return added;
    //}

    //protected Node TryAdd(Type addition, Node avlTree, out bool added)
    //{
    //  if (avlTree == null)
    //  {
    //    added = true;
    //    return new Node(addition);
    //  }
    //  Comparison compareResult = _compare(avlTree.Value, addition);
    //  if (compareResult == Comparison.Equal)
    //  {
    //    added = false;
    //    return avlTree;
    //  }
    //  else if (compareResult == Comparison.Greater)
    //    avlTree.LeftChild = TryAdd(addition, avlTree.LeftChild, out added);
    //  else // (compareResult == Comparison.Less)
    //    avlTree.RightChild = TryAdd(addition, avlTree.RightChild, out added);
    //  return Balance(avlTree);
    //}

    ///// <summary>Removes an object from the AVL Tree.</summary>
    ///// <param name="removal">The string ID of the object to remove.</param>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //public virtual void Remove(Type removal)
    //{
    //  _avlTree = Remove(removal, _avlTree);
    //  _count--;
    //}

    ///// <summary>Removes an object from the AVL Tree.</summary>
    ///// <param name="removal">The string ID of the object to remove.</param>
    ///// <param name="avlTree">The binary tree to remove from.</param>
    ///// <returns>The resulting tree after the removal.</returns>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //protected Node Remove(Type removal, Node avlTree)
    //{
    //  if (avlTree != null)
    //  {
    //    Comparison compareResult = _compare(avlTree.Value, removal);
    //    if (compareResult == Comparison.Equal)
    //    {
    //      if (avlTree.RightChild != null)
    //      {
    //        Node leftMostOfRight;
    //        avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out leftMostOfRight);
    //        leftMostOfRight.RightChild = avlTree.RightChild;
    //        leftMostOfRight.LeftChild = avlTree.LeftChild;
    //        avlTree = leftMostOfRight;
    //      }
    //      else if (avlTree.LeftChild != null)
    //      {
    //        Node rightMostOfLeft;
    //        avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out rightMostOfLeft);
    //        rightMostOfLeft.RightChild = avlTree.RightChild;
    //        rightMostOfLeft.LeftChild = avlTree.LeftChild;
    //        avlTree = rightMostOfLeft;
    //      }
    //      else return null;
    //      SetHeight(avlTree);
    //      return Balance(avlTree);
    //    }
    //    else if (compareResult == Comparison.Greater)
    //      avlTree.LeftChild = Remove(removal, avlTree.LeftChild);
    //    else // (compareResult == Comparison.Less)
    //      avlTree.RightChild = Remove(removal, avlTree.RightChild);
    //    SetHeight(avlTree);
    //    return Balance(avlTree);
    //  }
    //  throw new AvlTreeLinkedException("Attempting to remove a non-existing entry.");
    //}

    ///// <summary>Removes an object from the AVL Tree.</summary>
    ///// <param name="removal">The string ID of the object to remove.</param>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //public virtual bool TryRemove(Type removal)
    //{
    //  try
    //  {
    //    _avlTree = Remove(removal, _avlTree);
    //    _count--;
    //    return true;
    //  }
    //  catch
    //  {
    //    return false;
    //  }
    //}

    ///// <summary>Removes an object from the AVL Tree.</summary>
    ///// <param name="removal">The string ID of the object to remove.</param>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //public virtual void Remove<Key>(Key removal, Compare<Type, Key> comparison)
    //{
    //  _avlTree = Remove(removal, comparison, _avlTree);
    //  _count--;
    //}

    ///// <summary>Removes an object from the AVL Tree.</summary>
    ///// <param name="removal">The string ID of the object to remove.</param>
    ///// <param name="avlTree">The binary tree to remove from.</param>
    ///// <returns>The resulting tree after the removal.</returns>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //protected Node Remove<Key>(Key removal, Compare<Type, Key> comparison, Node avlTree)
    //{
    //  if (avlTree != null)
    //  {
    //    Comparison compareResult = comparison(avlTree.Value, removal);
    //    if (compareResult == Comparison.Equal)
    //    {
    //      if (avlTree.RightChild != null)
    //      {
    //        Node leftMostOfRight;
    //        avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out leftMostOfRight);
    //        leftMostOfRight.RightChild = avlTree.RightChild;
    //        leftMostOfRight.LeftChild = avlTree.LeftChild;
    //        avlTree = leftMostOfRight;
    //      }
    //      else if (avlTree.LeftChild != null)
    //      {
    //        Node rightMostOfLeft;
    //        avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out rightMostOfLeft);
    //        rightMostOfLeft.RightChild = avlTree.RightChild;
    //        rightMostOfLeft.LeftChild = avlTree.LeftChild;
    //        avlTree = rightMostOfLeft;
    //      }
    //      else return null;
    //      SetHeight(avlTree);
    //      return Balance(avlTree);
    //    }
    //    else if (compareResult == Comparison.Greater)
    //      avlTree.LeftChild = Remove<Key>(removal, comparison, avlTree.LeftChild);
    //    else // (compareResult == Comparison.Less)
    //      avlTree.RightChild = Remove<Key>(removal, comparison, avlTree.RightChild);
    //    SetHeight(avlTree);
    //    return Balance(avlTree);
    //  }
    //  throw new AvlTreeLinkedException("Attempting to remove a non-existing entry.");
    //}

    ///// <summary>Removes an object from the AVL Tree.</summary>
    ///// <param name="removal">The string ID of the object to remove.</param>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //public virtual bool TryRemove<Key>(Key removal, Compare<Type, Key> comparison)
    //{
    //  try
    //  {
    //    _avlTree = Remove(removal, comparison, _avlTree);
    //    _count--;
    //    return true;
    //  }
    //  catch
    //  {
    //    return false;
    //  }
    //}

    ///// <summary>Removes the left-most child of an AVL Tree node and returns it 
    ///// through the out parameter.</summary>
    ///// <param name="avlTree">The tree to remove the left-most child from.</param>
    ///// <param name="leftMost">The left-most child of this AVL tree.</param>
    ///// <returns>The updated tree with the removal.</returns>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //protected Node RemoveLeftMost(Node avlTree, out Node leftMost)
    //{
    //  if (avlTree.LeftChild == null) { leftMost = avlTree; return null; }
    //  avlTree.LeftChild = RemoveLeftMost(avlTree.LeftChild, out leftMost);
    //  SetHeight(avlTree);
    //  return Balance(avlTree);
    //}

    ///// <summary>Removes the right-most child of an AVL Tree node and returns it 
    ///// through the out parameter.</summary>
    ///// <param name="avlTree">The tree to remove the right-most child from.</param>
    ///// <param name="leftMost">The right-most child of this AVL tree.</param>
    ///// <returns>The updated tree with the removal.</returns>
    ///// <remarks>Runtime: Theta(ln(n)).</remarks>
    //protected Node RemoveRightMost(Node avlTree, out Node rightMost)
    //{
    //  if (avlTree.RightChild == null) { rightMost = avlTree; return null; }
    //  avlTree.LeftChild = RemoveLeftMost(avlTree.RightChild, out rightMost);
    //  SetHeight(avlTree);
    //  return Balance(avlTree);
    //}

    ///// <summary>This is just a protection against the null valued leaf nodes, 
    ///// which have a height of "-1".</summary>
    ///// <param name="avlTree">The AVL Tree to find the hight of.</param>
    ///// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
    ///// <remarks>Runtime: O(1).</remarks>
    //protected int Height(Node avlTree)
    //{
    //  if (avlTree == null) return -1;
    //  else return avlTree.Height;
    //}

    ///// <summary>Sets the height of a tree based on its children's heights.</summary>
    ///// <param name="avlTree">The tree to have its height adjusted.</param>
    ///// <remarks>Runtime: O(1).</remarks>
    //protected void SetHeight(Node avlTree)
    //{
    //  if (Height(avlTree.LeftChild) < Height(avlTree.RightChild))
    //    avlTree.Height = Math.Max(Height(avlTree.LeftChild), Height(avlTree.RightChild)) + 1;
    //}

    ///// <summary>Standard balancing algorithm for an AVL Tree.</summary>
    ///// <param name="avlTree">The tree to check the balancing of.</param>
    ///// <returns>The result of the possible balancing.</returns>
    ///// <remarks>Runtime: O(1).</remarks>
    //protected Node Balance(Node avlTree)
    //{
    //  if (Height(avlTree.LeftChild) == Height(avlTree.RightChild) + 2)
    //  {
    //    if (Height(avlTree.LeftChild.LeftChild) > Height(avlTree.RightChild))
    //      return SingleRotateRight(avlTree);
    //    else return DoubleRotateRight(avlTree);
    //  }
    //  else if (Height(avlTree.RightChild) == Height(avlTree.LeftChild) + 2)
    //  {
    //    if (Height(avlTree.RightChild.RightChild) > Height(avlTree.LeftChild))
    //      return SingleRotateLeft(avlTree);
    //    else return DoubleRotateLeft(avlTree);
    //  }
    //  SetHeight(avlTree);
    //  return avlTree;
    //}

    ///// <summary>Standard single rotation (to the right) algorithm for an AVL Tree.</summary>
    ///// <param name="avlTree">The tree to single rotate right.</param>
    ///// <returns>The resulting tree.</returns>
    ///// <remarks>Runtime: O(1).</remarks>
    //protected Node SingleRotateRight(Node avlTree)
    //{
    //  Node temp = avlTree.LeftChild;
    //  avlTree.LeftChild = temp.RightChild;
    //  temp.RightChild = avlTree;
    //  SetHeight(avlTree);
    //  SetHeight(temp);
    //  return temp;
    //}

    ///// <summary>Standard single rotation (to the left) algorithm for an AVL Tree.</summary>
    ///// <param name="avlTree">The tree to single rotate left.</param>
    ///// <returns>The resulting tree.</returns>
    ///// <remarks>Runtime: O(1).</remarks>
    //protected Node SingleRotateLeft(Node avlTree)
    //{
    //  Node temp = avlTree.RightChild;
    //  avlTree.RightChild = temp.LeftChild;
    //  temp.LeftChild = avlTree;
    //  SetHeight(avlTree);
    //  SetHeight(temp);
    //  return temp;
    //}

    ///// <summary>Standard double rotation (to the right) algorithm for an AVL Tree.</summary>
    ///// <param name="avlTree">The tree to float rotate right.</param>
    ///// <returns>The resulting tree.</returns>
    ///// <remarks>Runtime: O(1).</remarks>
    //protected Node DoubleRotateRight(Node avlTree)
    //{
    //  Node temp = avlTree.LeftChild.RightChild;
    //  avlTree.LeftChild.RightChild = temp.LeftChild;
    //  temp.LeftChild = avlTree.LeftChild;
    //  avlTree.LeftChild = temp.RightChild;
    //  temp.RightChild = avlTree;
    //  SetHeight(temp.LeftChild);
    //  SetHeight(temp.RightChild);
    //  SetHeight(temp);
    //  return temp;
    //}

    ///// <summary>Standard double rotation (to the left) algorithm for an AVL Tree.</summary>
    ///// <param name="avlTree">The tree to float rotate left.</param>
    ///// <returns>The resulting tree.</returns>
    ///// <remarks>Runtime: O(1).</remarks>
    //protected Node DoubleRotateLeft(Node avlTree)
    //{
    //  Node temp = avlTree.RightChild.LeftChild;
    //  avlTree.RightChild.LeftChild = temp.RightChild;
    //  temp.RightChild = avlTree.RightChild;
    //  avlTree.RightChild = temp.LeftChild;
    //  temp.LeftChild = avlTree;
    //  SetHeight(temp.LeftChild);
    //  SetHeight(temp.RightChild);
    //  SetHeight(temp);
    //  return temp;
    //}

    ///// <summary>Returns the tree to an iterative state.</summary>
    //public virtual void Clear() { _avlTree = null; _count = 0; }

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public void Foreach(Foreach<Type> function)
    //{
    //  this.ForeachInOrder(function);
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public void Foreach(ForeachRef<Type> function)
    //{
    //  this.ForeachInOrder(function);
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <returns>The resulting status of the iteration.</returns>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public ForeachStatus Foreach(ForeachBreak<Type> function)
    //{
    //  return this.ForeachInOrder(function);
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <returns>The resulting status of the iteration.</returns>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    //{
    //  return this.ForeachInOrder(function);
    //}

    //public IEnumerator<Type> GetEnumerator()
    //{
    //  return AvlTree_Linked<Type>.GetEnumerator(this._avlTree);
    //}
    //private static IEnumerator<Type> GetEnumerator(Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    AvlTree_Linked<Type>.GetEnumerator(avltreeNode.LeftChild);
    //    yield return avltreeNode.Value;
    //    AvlTree_Linked<Type>.GetEnumerator(avltreeNode.RightChild);
    //  }
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual void ForeachInOrder(Foreach<Type> function)
    //{
    //  AvlTree_Linked<Type>.TraversalInOrder(function, _avlTree);
    //}
    //protected static bool TraversalInOrder(Foreach<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.LeftChild);
    //    function(avltreeNode.Value);
    //    AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.RightChild);
    //  }
    //  return true;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual void ForeachInOrder(ForeachRef<Type> function)
    //{
    //  AvlTree_Linked<Type>.TraversalInOrder(function, _avlTree);
    //}
    //protected static bool TraversalInOrder(ForeachRef<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.LeftChild);
    //    Type value = avltreeNode.Value;
    //    function(ref value);
    //    avltreeNode.Value = value;
    //    AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.RightChild);
    //  }
    //  return true;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual ForeachStatus ForeachInOrder(ForeachBreak<Type> function)
    //{
    //  return AvlTree_Linked<Type>.TraversalInOrder(function, _avlTree);
    //}
    //protected static ForeachStatus TraversalInOrder(ForeachBreak<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    if (AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    Type value = avltreeNode.Value;
    //    ForeachStatus status = function(value);
    //    avltreeNode.Value = value;
    //    if (status == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    if (AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual ForeachStatus ForeachInOrder(ForeachRefBreak<Type> function)
    //{
    //  return AvlTree_Linked<Type>.TraversalInOrder(function, _avlTree);
    //}
    //protected static ForeachStatus TraversalInOrder(ForeachRefBreak<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    if (AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    Type value = avltreeNode.Value;
    //    ForeachStatus status = function(ref value);
    //    avltreeNode.Value = value;
    //    if (status == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    if (AvlTree_Linked<Type>.TraversalInOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual void ForeachPreOrder(Foreach<Type> function)
    //{
    //  AvlTree_Linked<Type>.TraversalPreOrder(function, _avlTree);
    //}
    //protected static bool TraversalPreOrder(Foreach<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    function(avltreeNode.Value);
    //    AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.LeftChild);
    //    AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.RightChild);
    //  }
    //  return true;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual void ForeachPreOrder(ForeachRef<Type> function)
    //{
    //  AvlTree_Linked<Type>.TraversalPreOrder(function, _avlTree);
    //}
    //protected static bool TraversalPreOrder(ForeachRef<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    Type value = avltreeNode.Value;
    //    function(ref value);
    //    avltreeNode.Value = value;
    //    AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.LeftChild);
    //    AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.RightChild);
    //  }
    //  return true;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual ForeachStatus ForeachPreOrder(ForeachBreak<Type> function)
    //{
    //  return AvlTree_Linked<Type>.TraversalPreOrder(function, _avlTree);
    //}
    //protected static ForeachStatus TraversalPreOrder(ForeachBreak<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    Type value = avltreeNode.Value;
    //    ForeachStatus status = function(value);
    //    avltreeNode.Value = value;
    //    if (status == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    if (AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    if (AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual ForeachStatus ForeachPreOrder(ForeachRefBreak<Type> function)
    //{
    //  return AvlTree_Linked<Type>.TraversalPreOrder(function, _avlTree);
    //}
    //protected static ForeachStatus TraversalPreOrder(ForeachRefBreak<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    Type value = avltreeNode.Value;
    //    ForeachStatus status = function(ref value);
    //    avltreeNode.Value = value;
    //    if (status == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    if (AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    if (AvlTree_Linked<Type>.TraversalPreOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual void ForeachPostOrder(Foreach<Type> function)
    //{
    //  AvlTree_Linked<Type>.TraversalPostOrder(function, _avlTree);
    //}
    //protected static bool TraversalPostOrder(Foreach<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.RightChild);
    //    function(avltreeNode.Value);
    //    AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.LeftChild);
    //  }
    //  return true;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual void ForeachPostOrder(ForeachRef<Type> function)
    //{
    //  AvlTree_Linked<Type>.TraversalPostOrder(function, _avlTree);
    //}
    //protected static bool TraversalPostOrder(ForeachRef<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.RightChild);
    //    Type value = avltreeNode.Value;
    //    function(ref value);
    //    avltreeNode.Value = value;
    //    AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.LeftChild);
    //  }
    //  return true;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual ForeachStatus ForeachPostOrder(ForeachBreak<Type> function)
    //{
    //  return AvlTree_Linked<Type>.TraversalPostOrder(function, _avlTree);
    //}
    //protected static ForeachStatus TraversalPostOrder(ForeachBreak<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    if (AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    Type value = avltreeNode.Value;
    //    ForeachStatus status = function(value);
    //    avltreeNode.Value = value;
    //    if (status == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    if (AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    ///// <param name="function">The delegate to invoke on each item in the structure.</param>
    ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //public virtual ForeachStatus ForeachPostOrder(ForeachRefBreak<Type> function)
    //{
    //  return AvlTree_Linked<Type>.TraversalPostOrder(function, _avlTree);
    //}
    //protected static ForeachStatus TraversalPostOrder(ForeachRefBreak<Type> function, Node avltreeNode)
    //{
    //  if (avltreeNode != null)
    //  {
    //    if (AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.RightChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    Type value = avltreeNode.Value;
    //    ForeachStatus status = function(ref value);
    //    avltreeNode.Value = value;
    //    if (status == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //    if (AvlTree_Linked<Type>.TraversalPostOrder(function, avltreeNode.LeftChild) == ForeachStatus.Break)
    //      return ForeachStatus.Break;
    //  }
    //  return ForeachStatus.Continue;
    //}

    ///// <summary>Creates an array out of the values in this structure.</summary>
    ///// <returns>An array containing the values in this structure.</returns>
    ///// <remarks>Runtime: Theta(n).</remarks>
    //public virtual Type[] ToArray()
    //{
    //  Type[] array = new Type[_count];
    //  ToArray(array, _avlTree, 0);
    //  return array;
    //}
    //protected void ToArray(Type[] array, Node avltreeNode, int position)
    //{
    //  if (avltreeNode != null)
    //  {
    //    ToArray(array, avltreeNode.LeftChild, position);
    //    array[position++] = avltreeNode.Value;
    //    ToArray(array, avltreeNode.RightChild, position);
    //  }
    //}

    public class Exception : Error
    {
      public Exception(string message) : base(message) { }
    }
  }
}