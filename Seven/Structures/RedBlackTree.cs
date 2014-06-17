using Seven;
using Seven.Parallels;

// using System; // Exception, Serializable
// using System.Collections; // IEnumerable
// using System.Collections.Generic; // IEnumerable<Type>

namespace Seven.Structures
{
  public interface RedBlackTree<Type> : Structure<Type>
  {
    void Add(Type addition);
    //bool TryAdd(Type addition);
    void Remove(Type removal);
    //bool TryRemove(Type removal);
    int Count { get; }
    bool IsEmpty { get; }
    void Clear();

    //bool Contains<Key>(Key key, Func<Type, Key, int> comparison);
    //Type Get<Key>(Key get, Func<Type, Key, int> comparison);
    //bool TryGet<Key>(Key get, Func<Type, Key, int> comparison, out Type item);
    //void Remove<Key>(Key removal, Func<Type, Key, int> comparison);
    //bool TryRemove<Key>(Key removal, Func<Type, Key, int> comparison);
  }


  [System.Serializable]
  public class RedBlackTree_Linked<Type> : RedBlackTree<Type>
  {
    protected const bool Red = true;
    protected const bool Black = false;

    protected class Node
    {
      private bool _color;
      private Type _value;
      private Node _leftChild;
      private Node _rightChild;
      private Node _parent;

      internal bool Color { get { return _color; } set { _color = value; } }
      internal Type Value { get { return _value; } set { _value = value; } }
      internal Node LeftChild { get { return _leftChild; } set { _leftChild = value; } }
      internal Node RightChild { get { return _rightChild; } set { _rightChild = value; } }
      internal Node Parent { get { return _parent; } set { _parent = value; } }

      internal Node()
      {
        _color = Red;
      }
    }

    protected Compare<Type> _compare;

    protected int _count;
    protected Node _redBlackTree;
    protected static Node _sentinelNode;

    public int Count { get { return _count; } }

    public bool IsEmpty { get { return _redBlackTree == null; } }

    public RedBlackTree_Linked(Compare<Type> valueComparisonFunction)
    {
      _sentinelNode = new Node();
      _sentinelNode.Color = Black;
      _redBlackTree = _sentinelNode;
      _compare = valueComparisonFunction;
    }

    public Type Get<Key>(Key key, Compare<Type, Key> compare)
    {
      //int compareResult;
      Node treeNode = _redBlackTree;
      while (treeNode != _sentinelNode)
      {
        Comparison compareResult = compare(treeNode.Value, key);
        if (compareResult == Comparison.Equal)
          return treeNode.Value;
        else if (compareResult == Comparison.Greater)
          treeNode = treeNode.LeftChild;
        else // (compareResult == Comparison.Less)
          treeNode = treeNode.RightChild;
      }
      throw new RedBlackLinkedException("attempting to get a non-existing value.");
    }

    public bool TryGet<Key>(Key key, Compare<Type, Key> comparison, out Type returnValue)
    {
      Node treeNode = _redBlackTree;
      while (treeNode != _sentinelNode)
      {
        Comparison compareResult = comparison(treeNode.Value, key);
        if (compareResult == Comparison.Equal)
        {
          returnValue = treeNode.Value;
          return true;
        }
        if (compareResult == Comparison.Greater)
          treeNode = treeNode.LeftChild;
        else // (compareResult == Comparison.Less)
          treeNode = treeNode.RightChild;
      }
      returnValue = default(Type);
      return false;
    }

    public bool Contains(Type item)
    {
      Node treeNode = _redBlackTree;
      while (treeNode != _sentinelNode)
      {
        Comparison compareResult = _compare(treeNode.Value, item);
        if (compareResult == Comparison.Equal)
          return true;
        if (compareResult == Comparison.Greater)
          treeNode = treeNode.LeftChild;
        else // (compareResult == Comparison.Less)
          treeNode = treeNode.RightChild;
      }
      return false;
    }

    public void Add(Type data)
    {
      if (data == null)
        throw (new RedBlackLinkedException("RedBlackNode key and data must not be null"));
      Node addition = new Node();
      Node temp = _redBlackTree;
      while (temp != _sentinelNode)
      {
        addition.Parent = temp;
        Comparison result = _compare(data, temp.Value);
        if (result == Comparison.Equal)
          throw (new RedBlackLinkedException("A Node with the same key already exists"));
        else if (result == Comparison.Greater)
          temp = temp.RightChild;
        else // (result == Comparison.Less)
          temp = temp.LeftChild;
      }
      addition.Value = data;
      addition.LeftChild = _sentinelNode;
      addition.RightChild = _sentinelNode;
      if (addition.Parent != null)
      {
        Comparison result = _compare(addition.Value, addition.Parent.Value);
        if (result == Comparison.Greater)
          addition.Parent.RightChild = addition;
        else // (result == Comparison.Less)
          addition.Parent.LeftChild = addition;
      }
      else
        _redBlackTree = addition;
      BalanceAddition(addition);
      _count = _count + 1;
    }

    protected void BalanceAddition(Node balancing)
    {
      Node temp;
      while (balancing != _redBlackTree && balancing.Parent.Color == Red)
      {
        if (balancing.Parent == balancing.Parent.Parent.LeftChild)
        {
          temp = balancing.Parent.Parent.RightChild;
          if (temp != null && temp.Color == Red)
          {
            balancing.Parent.Color = Black;
            temp.Color = Black;
            balancing.Parent.Parent.Color = Red;
            balancing = balancing.Parent.Parent;
          }
          else
          {
            if (balancing == balancing.Parent.RightChild)
            {
              balancing = balancing.Parent;
              RotateLeft(balancing);
            }
            balancing.Parent.Color = Black;
            balancing.Parent.Parent.Color = Red;
            RotateRight(balancing.Parent.Parent);
          }
        }
        else
        {
          temp = balancing.Parent.Parent.LeftChild;
          if (temp != null && temp.Color == Red)
          {
            balancing.Parent.Color = Black;
            temp.Color = Black;
            balancing.Parent.Parent.Color = Red;
            balancing = balancing.Parent.Parent;
          }
          else
          {
            if (balancing == balancing.Parent.LeftChild)
            {
              balancing = balancing.Parent;
              RotateRight(balancing);
            }
            balancing.Parent.Color = Black;
            balancing.Parent.Parent.Color = Red;
            RotateLeft(balancing.Parent.Parent);
          }
        }
      }
      _redBlackTree.Color = Black;
    }

    protected void RotateLeft(Node redBlackTree)
    {
      Node temp = redBlackTree.RightChild;
      redBlackTree.RightChild = temp.LeftChild;
      if (temp.LeftChild != _sentinelNode)
        temp.LeftChild.Parent = redBlackTree;
      if (temp != _sentinelNode)
        temp.Parent = redBlackTree.Parent;
      if (redBlackTree.Parent != null)
      {
        if (redBlackTree == redBlackTree.Parent.LeftChild)
          redBlackTree.Parent.LeftChild = temp;
        else
          redBlackTree.Parent.RightChild = temp;
      }
      else
        _redBlackTree = temp;
      temp.LeftChild = redBlackTree;
      if (redBlackTree != _sentinelNode)
        redBlackTree.Parent = temp;
    }

    protected void RotateRight(Node redBlacktree)
    {
      Node temp = redBlacktree.LeftChild;
      redBlacktree.LeftChild = temp.RightChild;
      if (temp.RightChild != _sentinelNode)
        temp.RightChild.Parent = redBlacktree;
      if (temp != _sentinelNode)
        temp.Parent = redBlacktree.Parent;
      if (redBlacktree.Parent != null)
      {
        if (redBlacktree == redBlacktree.Parent.RightChild)
          redBlacktree.Parent.RightChild = temp;
        else
          redBlacktree.Parent.LeftChild = temp;
      }
      else
        _redBlackTree = temp;
      temp.RightChild = redBlacktree;
      if (redBlacktree != _sentinelNode)
        redBlacktree.Parent = temp;
    }

    public Type GetMin()
    {
      Node treeNode = _redBlackTree;
      if (treeNode == null || treeNode == _sentinelNode)
        throw new RedBlackLinkedException("attempting to get the minimum value from an empty tree.");
      while (treeNode.LeftChild != _sentinelNode)
        treeNode = treeNode.LeftChild;
      Type returnValue = treeNode.Value;
      return returnValue;
    }

    public Type GetMax()
    {
      Node treeNode = _redBlackTree;
      if (treeNode == null || treeNode == _sentinelNode)
      {
        throw (new RedBlackLinkedException("attempting to get the maximum value from an empty tree."));
      }
      while (treeNode.RightChild != _sentinelNode)
        treeNode = treeNode.RightChild;
      Type returnValue = treeNode.Value;
      return returnValue;
    }

    public void Remove(Type value)
    {
      if (value is object)
        if (((object)value) == null)
          throw new RedBlackLinkedException("Attempting to remove a null value from the tree.");
      //int result;
      Node node;
      node = _redBlackTree;
      while (node != _sentinelNode)
      {
        Comparison result = _compare(node.Value, value);
        if (result == Comparison.Equal)
          break;
        if (result == Comparison.Greater)
          node = node.LeftChild;
        else // (result == Comparison.Less)
          node = node.RightChild;
      }
      if (node == _sentinelNode) return;
      Remove(node);
      _count = _count - 1;
    }

    protected void Remove(Node removal)
    {
      Node x = new Node();
      Node temp;
      if (removal.LeftChild == _sentinelNode || removal.RightChild == _sentinelNode)
        temp = removal;
      else
      {
        temp = removal.RightChild;
        while (temp.LeftChild != _sentinelNode)
          temp = temp.LeftChild;
      }
      if (temp.LeftChild != _sentinelNode)
        x = temp.LeftChild;
      else
        x = temp.RightChild;
      x.Parent = temp.Parent;
      if (temp.Parent != null)
        if (temp == temp.Parent.LeftChild)
          temp.Parent.LeftChild = x;
        else
          temp.Parent.RightChild = x;
      else
        _redBlackTree = x;
      if (temp != removal)
        removal.Value = temp.Value;
      if (temp.Color == Black) BalanceRemoval(x);
    }

    protected void BalanceRemoval(Node balancing)
    {
      Node temp;
      while (balancing != _redBlackTree && balancing.Color == Black)
      {
        if (balancing == balancing.Parent.LeftChild)
        {
          temp = balancing.Parent.RightChild;
          if (temp.Color == Red)
          {
            temp.Color = Black;
            balancing.Parent.Color = Red;
            RotateLeft(balancing.Parent);
            temp = balancing.Parent.RightChild;
          }
          if (temp.LeftChild.Color == Black && temp.RightChild.Color == Black)
          {
            temp.Color = Red;
            balancing = balancing.Parent;
          }
          else
          {
            if (temp.RightChild.Color == Black)
            {
              temp.LeftChild.Color = Black;
              temp.Color = Red;
              RotateRight(temp);
              temp = balancing.Parent.RightChild;
            }
            temp.Color = balancing.Parent.Color;
            balancing.Parent.Color = Black;
            temp.RightChild.Color = Black;
            RotateLeft(balancing.Parent);
            balancing = _redBlackTree;
          }
        }
        else
        {
          temp = balancing.Parent.LeftChild;
          if (temp.Color == Red)
          {
            temp.Color = Black;
            balancing.Parent.Color = Red;
            RotateRight(balancing.Parent);
            temp = balancing.Parent.LeftChild;
          }
          if (temp.RightChild.Color == Black && temp.LeftChild.Color == Black)
          {
            temp.Color = Red;
            balancing = balancing.Parent;
          }
          else
          {
            if (temp.LeftChild.Color == Black)
            {
              temp.RightChild.Color = Black;
              temp.Color = Red;
              RotateLeft(temp);
              temp = balancing.Parent.LeftChild;
            }
            temp.Color = balancing.Parent.Color;
            balancing.Parent.Color = Black;
            temp.LeftChild.Color = Black;
            RotateRight(balancing.Parent);
            balancing = _redBlackTree;
          }
        }
      }
      balancing.Color = Black;
    }

    public void Clear()
    {
      _redBlackTree = _sentinelNode;
      _count = 0;
    }

    /// <summary>Creates an array out of the values in this structure.</summary>
    /// <returns>An array containing the values in this structure.</returns>
    /// <remarks>Runtime: Theta(n),</remarks>
    public Type[] ToArray() { return ToArrayInOrder(); }

    /// <summary>Puts all the items in the tree into an array in order.</summary>
    /// <returns>The alphabetized list of items.</returns>
    /// <remarks>Runtime: Theta(n).</remarks>
    public Type[] ToArrayInOrder()
    {
      Type[] array = new Type[_count];
      ToArrayInOrder(array, _redBlackTree, 0);
      return array;
    }
    protected void ToArrayInOrder(Type[] array, Node avltreeNode, int position)
    {
      if (avltreeNode != null)
      {
        ToArrayInOrder(array, avltreeNode.LeftChild, position);
        array[position++] = avltreeNode.Value;
        ToArrayInOrder(array, avltreeNode.RightChild, position);
      }
    }

    /// <summary>Puts all the items in the tree into an array in reverse order.</summary>
    /// <returns>The alphabetized list of items.</returns>
    /// <remarks>Runtime: Theta(n).</remarks>
    public Type[] ToArrayPostOrder()
    {
      Type[] array = new Type[_count];
      ToArrayPostOrder(array, _redBlackTree, 0);
      return array;
    }
    protected void ToArrayPostOrder(Type[] array, Node avltreeNode, int position)
    {
      if (avltreeNode != null)
      {
        ToArrayInOrder(array, avltreeNode.RightChild, position);
        array[position++] = avltreeNode.Value;
        ToArrayInOrder(array, avltreeNode.LeftChild, position);
      }
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.IEnumerator 
      System.Collections.IEnumerable.GetEnumerator()
    {
      Stack<Node> forks = new Stack_Linked<Node>();
      Node current = _redBlackTree;
      while (current != null && current.LeftChild != null && current.RightChild != null || forks.Count > 0)
      {
        if (current != null)
        {
          forks.Push(current);
          current = current.LeftChild;
        }
        else if (forks.Count > 0)
        {
          current = forks.Pop();
          if (current.LeftChild != null && current.RightChild != null)
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
      Node current = _redBlackTree;
      while (current != null && current.LeftChild != null && current.RightChild != null || forks.Count > 0)
      {
        if (current != null)
        {
          forks.Push(current);
          current = current.LeftChild;
        }
        else if (forks.Count > 0)
        {
          current = forks.Pop();
          if (current.LeftChild != null && current.RightChild != null)
            yield return current.Value;
          current = current.RightChild;
        }
      }
    }

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public long SizeOf { get { throw new System.NotImplementedException(); } }

    /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>An array containing all the values matching the key or null if non were found.</returns>
    //Type[] GetValues<Key>(Key key, Compare<Type, Key> compare);

    /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>An array containing all the values matching the key or null if non were found.</returns>
    /// <param name="values">The values that matched the given key.</param>
    /// <returns>true if 1 or more values were found; false if no values were found.</returns>
    //bool TryGetValues<Key>(Key key, Compare<Type, Key> compare, out Type[] values);

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains(Type item, Compare<Type> compare)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    {
      throw new System.NotImplementedException();
      //Node treeNode = _redBlackTree;
      //while (treeNode != _sentinelNode)
      //{
      //  Comparison compareResult = compare(treeNode.Value, key);
      //  if (compareResult == Comparison.Equal)
      //    return true;
      //  else if (compareResult == Comparison.Greater)
      //    treeNode = treeNode.LeftChild;
      //  else // (compareResult == Comparison.Less)
      //    treeNode = treeNode.RightChild;
      //}
      //return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<Type> function)
    {
      ForeachInOrder(function);
    }

    public void ForeachInOrder(Foreach<Type> function)
    {
      ForeachInOrder(function, _redBlackTree);
    }
    protected void ForeachInOrder(Foreach<Type> function, Node node)
    {
      if (node != null && node.LeftChild != null && node.RightChild != null)
      {
        ForeachInOrder(function, node.LeftChild);
        function(node.Value);
        ForeachInOrder(function, node.RightChild);
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<Type> function)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<Type> function)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<Type> Clone()
    {
      throw new System.NotImplementedException();
    }

    ///// <summary>Converts the structure into an array.</summary>
    ///// <returns>An array containing all the item in the structure.</returns>
    //public Type[] ToArray()
    //{
    //  throw new NotImplementedException();
    //}

    /// <summary>This is used for throwing RedBlackTree exceptions only to make debugging faster.</summary>
    protected class RedBlackLinkedException : System.Exception { public RedBlackLinkedException(string message) : base(message) { } }
  }


  [System.Serializable]
  public class RedBlackTree_Linked_ThreadSafe<Type> : RedBlackTree_Linked<Type>
  {
    ReaderWriterLock _readerWriterLock;

    public RedBlackTree_Linked_ThreadSafe(Compare<Type> compare) : base(compare)
    {
      this._readerWriterLock = new ReaderWriterLock();
    }

    ///// <summary>This is used for throwing RedBlackTree exceptions only to make debugging faster.</summary>
    //protected class Exception : System.Exception
    //{
    //  public Exception(string message) : base(message) { }
    //}
  }
}
