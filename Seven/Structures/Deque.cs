// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Structures
{
  public interface Deque<T> : Structure<T>
  {

  }

  /// <summary>Implements First-In-First-Out queue data structure.</summary>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [System.Serializable]
  public class Deque_Linked<Type> : Deque<Type>
  {
    /// <summary>This class just holds the data for each individual node of the list.</summary>
    private class Node
    {
      private Type _value;
      private Node _next;

      internal Type Value { get { return _value; } set { _value = value; } }
      internal Node Next { get { return _next; } set { _next = value; } }

      internal Node(Type data) { _value = data; }
    }

    private Node _head;
    private Node _tail;
    private int _count;

    /// <summary>Returns the number of items in the queue.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int Count { get { return _count; } }

    /// <summary>Returns true if this structure is in an empty state.</summary>
    public bool IsEmpty { get { return _head == null; } }

    /// <summary>Creates an instance of a queue.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public Deque_Linked()
    {
      _head = _tail = null;
      _count = 0;
    }

    /// <summary>Adds an item to the back of the queue.</summary>
    /// <param name="enqueue">The item to add to the queue.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public void EnqueueBack(Type enqueue)
    {
      if (_tail == null)
        _head = _tail = new Node(enqueue);
      else
        _tail = _tail.Next = new Node(enqueue);
      _count++;
    }

    /// <summary>Removes the oldest item in the queue.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public Type DequeueFront()
    {
      if (_head == null)
        throw new Exception("Attempting to remove a non-existing id value.");
      Type value = _head.Value;
      if (_head == _tail)
        _tail = null;
      _head = null;
      _count--;
      return value;
    }

    /// <summary>Looks at the front-most value.</summary>
    /// <returns>The front-most value.</returns>
    public Type PeekFront()
    {
      if (_head == null)
        throw new Exception("Attempting to remove a non-existing id value.");
      Type returnValue = _head.Value;
      return returnValue;
    }

    /// <summary>Resets the queue to an empty state.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public void Clear()
    {
      _head = _tail = null;
      _count = 0;
    }


    /// <summary>Converts the list into a standard array.</summary>
    /// <returns>A standard array of all the items.</returns>
    /// /// <remarks>Runtime: Theta(n).</remarks>
    public Type[] ToArray()
    {
      if (_count == 0)
        return null;
      Type[] array = new Type[_count];
      Node looper = _head;
      for (int i = 0; i < _count; i++)
      {
        array[i] = looper.Value;
        looper = looper.Next;
      }
      return array;
    }

    #region .Net Framework Compatibility

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      Node current = this._head;
      while (current != null)
      {
        yield return current.Value;
        current = current.Next;
      }
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.Generic.IEnumerator<Type>
      System.Collections.Generic.IEnumerable<Type>.GetEnumerator()
    {
      Node current = this._head;
      while (current != null)
      {
        yield return current.Value;
        current = current.Next;
      }
    }

    #endregion

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { throw new System.NotImplementedException(); } }

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
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<Type> function)
    {
      Node current = this._head;
      while (current != null)
      {
        function(current.Value);
        current = current.Next;
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

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private class Exception : Error
    {
      public Exception(string message) : base(message) { }
    }
  }

}
