// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using Seven;
using Seven.Parallels;

// using System; // Exception, Serializable
// using System.Collections; // IEnumerable
// using System.Collections.Generic; // IEnumerable<Type>

namespace Seven.Structures
{
  public interface Stack<Type> : Structure<Type>
  {
    int Count { get; }
    bool IsEmpty { get; }
    void Push(Type push);
    Type Peek();
    Type Pop();
  }

  /// <summary>Implements a First-In-Last-Out stack data structure that inherits InterfaceTraversable.</summary>
  /// <typeparam name="Type">The generic type within the structure.</typeparam>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.
  /// Seven (Zachary Patten) 10-12-13.</remarks>
  [System.Serializable]
  public class Stack_Linked<Type> : Stack<Type>
  {
    #region StackLinkedNode

    /// <summary>This class just holds the data for each individual node of the stack.</summary>
    private class Node
    {
      private Type _value;
      private Node _down;

      internal Type Value { get { return _value; } set { _value = value; } }
      internal Node Down { get { return _down; } set { _down = value; } }

      internal Node(Type data, Node down) 
      {
        _value = data;
        _down = down;
      }
    }

    #endregion

    private Node _top;
    protected int _count;

    /// <summary>Returns the number of items in the stack.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int Count { get { return this._count; } }
    /// <summary>Returns true if this stack currenlty has no entries.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public bool IsEmpty { get { return this._count == 0; } }

    /// <summary>Creates an instance of a stack.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public Stack_Linked()
    {
      _top = null;
      _count = 0;
    }

    /// <summary>Adds an item to the top of the stack.</summary>
    /// <param name="addition">The item to add to the stack.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public void Push(Type addition)
    {
      _top = new Node(addition, _top);
      _count++;
    }

    /// <summary>Returns the most recent addition to the stack.</summary>
    /// <returns>The most recent addition to the stack.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    public Type Peek()
    {
      if (_top == null)
        throw new StackLinkedException("Attempting to remove from an empty queue.");
      Type peek = _top.Value;
      return peek;
    }

    /// <summary>Removes and returns the most recent addition to the stack.</summary>
    /// <returns>The most recent addition to the stack.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    public Type Pop()
    {
      Type x = _top.Value;
      _top = _top.Down;
      _count--;
      return x;
    }

    /// <summary>Clears the stack to an empty state.</summary>
    /// <remarks>Runtime: O(1). Note: causes considerable garbage collection.</remarks>
    public void Clear()
    {
      _top = null;
      _count = 0;
    }
    
    #region .Net Framework Compatibility

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //public static explicit operator Array_Array<Type>(Type[] array)
    //{
    //  return new Array_Array<Type>(array);
    //}

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //public static explicit operator Type[](Array_Array<Type> array)
    //{
    //  return array.ToArray();
    //}

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      for (Node looper = this._top; looper != null; looper = looper.Down)
        yield return looper.Value;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.Generic.IEnumerator<Type>
      System.Collections.Generic.IEnumerable<Type>.GetEnumerator()
    {
      for (Node looper = this._top; looper != null; looper = looper.Down)
        yield return looper.Value;
    }

    #endregion

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { return this._count; } }

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
      for (Node looper = this._top; looper != null; looper = looper.Down)
        if (compare(looper.Value, item) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    {
      for (Node looper = this._top; looper != null; looper = looper.Down)
        if (compare(looper.Value, key) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<Type> function)
    {
      for (Node looper = this._top; looper != null; looper = looper.Down)
        function(looper.Value);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<Type> function)
    {
      for (Node looper = this._top; looper != null; looper = looper.Down)
      {
        Type temp = looper.Value;
        function(ref temp);
        looper.Value = temp;
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<Type> function)
    {
      for (Node looper = this._top; looper != null; looper = looper.Down)
        if (function(looper.Value) == ForeachStatus.Break)
          return ForeachStatus.Break;
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      for (Node looper = this._top; looper != null; looper = looper.Down)
      {
        Type temp = looper.Value;
        if (function(ref temp) == ForeachStatus.Break)
        {
          looper.Value = temp;
          return ForeachStatus.Break;
        }
        looper.Value = temp;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<Type> Clone()
    {
      Stack_Linked<Type> clone = new Stack_Linked<Type>();
      if (this._count == 0)
        return clone;
      Node copying = this._top;
      Node cloneTop = new Node(this._top.Value, null);
      Node cloning = cloneTop;
      while (copying != null)
      {
        copying = copying.Down;
        cloning.Down = new Node(copying.Value, null);
        cloning = cloning.Down;
      }
      clone._top = cloneTop;
      return clone;
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    /// <remarks>Runtime: Theta(n).</remarks>
    public Type[] ToArray()
    {
      if (_count == 0)
        return null;
      Type[] array = new Type[_count];
      Node looper = _top;
      for (int i = 0; i < _count; i++)
      {
        array[i] = looper.Value;
        looper = looper.Down;
      }
      return array;
    }

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private class StackLinkedException : Error
    {
      public StackLinkedException(string message) : base(message) { }
    }
  }

  /// <summary>Implements a First-In-Last-Out stack data structure that inherits InterfaceTraversable.</summary>
  /// <typeparam name="Type">The generic type within the structure.</typeparam>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.
  /// Seven (Zachary Patten) 10-12-13.</remarks>
  [System.Serializable]
  public class Stack_Linked_ThreadSafe<Type> : Stack_Linked<Type>
  {
    ReaderWriterLock _readerWriterLock;

    /// <summary>Returns the number of items in the stack.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public new int Count
    {
      get
      {
        try
        {
          this._readerWriterLock.ReaderLock();
          return this._count;
        }
        finally
        {
          this._readerWriterLock.ReaderUnlock();
        }
      }
    }

    /// <summary>Creates an instance of a stack.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public Stack_Linked_ThreadSafe() : base()
    {
      _readerWriterLock = new ReaderWriterLock();
    }

    /// <summary>Adds an item to the top of the stack.</summary>
    /// <param name="addition">The item to add to the stack.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public new void Push(Type addition)
    {
      try
      {
        this._readerWriterLock.WriterLock();
        base.Push(addition);
      }
      finally
      {
        this._readerWriterLock.WriterLock();
      }
    }

    /// <summary>Returns the most recent addition to the stack.</summary>
    /// <returns>The most recent addition to the stack.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    public new Type Peek()
    {
      try
      {
        this._readerWriterLock.ReaderLock();
        return base.Peek();
      }
      finally
      {
        this._readerWriterLock.ReaderUnlock();
      }
    }

    /// <summary>Removes and returns the most recent addition to the stack.</summary>
    /// <returns>The most recent addition to the stack.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    public new Type Pop()
    {
      try
      {
        this._readerWriterLock.WriterLock();
        return base.Pop();
      }
      finally
      {
        this._readerWriterLock.WriterUnlock();
      }
    }

    /// <summary>Clears the stack to an empty state.</summary>
    /// <remarks>Runtime: O(1). Note: causes considerable garbage collection.</remarks>
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


    /// <summary>Converts the list into a standard array.</summary>
    /// <returns>A standard array of all the items.</returns>
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
    
    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private class Error : Structure.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  /// <summary>Implements a growing stack as an array (with expansions/contractions) data structure.</summary>
  /// <typeparam name="Type">The type of objects to be placed in the list.</typeparam>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [System.Serializable]
  public class StackArray<Type> : Stack<Type>
  {
    private Type[] _stack;
    private int _count;
    private int _minimumCapacity;

    /// <summary>Gets the number of items in the list.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int Count
    {
      get
      {
        int returnValue = _count;
        return returnValue;
      }
    }

    /// <summary>Returns true if the structure is empty.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public bool IsEmpty { get { return _count == 0; } }

    /// <summary>Gets the current capacity of the list.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int CurrentCapacity
    {
      get
      {
        int returnValue = _stack.Length;
        return returnValue;
      }
    }

    /// <summary>Allows you to adjust the minimum capacity of this list.</summary>
    /// <remarks>Runtime: O(n), Omega(1).</remarks>
    public int MinimumCapacity
    {
      get
      {
        int returnValue = _minimumCapacity;
        return returnValue;
      }
      set
      {
        if (value < 1)
          throw new Error("Attempting to set a minimum capacity to a negative or zero value.");
        else if (value > _stack.Length)
        {
          Type[] newList = new Type[value];
          _stack.CopyTo(newList, 0);
          _stack = newList;
        }
        else
          _minimumCapacity = value;
      }
    }

    /// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
    /// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public StackArray(int minimumCapacity)
    {
      _stack = new Type[minimumCapacity];
      _count = 0;
      _minimumCapacity = minimumCapacity;
    }

    /// <summary>Adds an item to the end of the list.</summary>
    /// <param name="addition">The item to be added.</param>
    /// <remarks>Runtime: O(n), EstAvg(1). </remarks>
    public void Push(Type addition)
    {
      if (_count == _stack.Length)
      {
        if (_stack.Length > System.Int32.MaxValue / 2)
          throw new Error("your queue is so large that it can no longer double itself (Int32.MaxValue barrier reached).");
        Type[] newStack = new Type[_stack.Length * 2];
        for (int i = 0; i < _count; i++)
          newStack[i] = _stack[i];
        _stack = newStack;
      }
      _stack[_count++] = addition;
    }

    /// <summary>Removes the item at a specific index.</summary>
    /// <remarks>Runtime: Theta(n - index).</remarks>
    public Type Pop()
    {
      if (_count == 0)
        throw new Error("attempting to dequeue from an empty queue.");
      if (_count < _stack.Length / 4 && _stack.Length / 2 > _minimumCapacity)
      {
        Type[] newQueue = new Type[_stack.Length / 2];
        for (int i = 0; i < _count; i++)
          newQueue[i] = _stack[i];
        _stack = newQueue;
      }
      Type returnValue = _stack[--_count];
      return returnValue;
    }

    public Type Peek()
    {
      Type returnValue = _stack[_count - 1];
      return returnValue;
    }

    /// <summary>Empties the list back and reduces it back to its original capacity.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public void Clear()
    {
      _stack = new Type[_minimumCapacity];
      _count = 0;
    }

    #region .Net Framework Compatibility
    
    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      for (int i = 0; i < this._stack.Length; i++)
        yield return this._stack[i];
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.Generic.IEnumerator<Type>
      System.Collections.Generic.IEnumerable<Type>.GetEnumerator()
    {
      for (int i = 0; i < this._stack.Length; i++)
        yield return this._stack[i];
    }

    #endregion

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { return this._count; } }

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
      for (int i = 0; i < this._stack.Length; i++)
        if (compare(this._stack[i], item) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    {
      for (int i = 0; i < this._stack.Length; i++)
        if (compare(this._stack[i], key) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<Type> function)
    {
      for (int i = 0; i < this._stack.Length; i++)
        function(this._stack[i]);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<Type> function)
    {
      for (int i = 0; i < this._stack.Length; i++)
        function(ref this._stack[i]);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<Type> function)
    {
      for (int i = 0; i < this._stack.Length; i++)
        if (function(this._stack[i]) == ForeachStatus.Break)
          return ForeachStatus.Break;
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      for (int i = 0; i < this._stack.Length; i++)
        if (function(ref this._stack[i]) == ForeachStatus.Break)
          return ForeachStatus.Break;
      return ForeachStatus.Continue;
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<Type> Clone()
    {
      StackArray<Type> clone = new StackArray<Type>(this._minimumCapacity);
      for (int i = 0; i < this._stack.Length; i++)
        clone.Push(this._stack[this._stack.Length - i]);
      return clone;
    }

    /// <summary>Converts the list array into a standard array.</summary>
    /// <returns>A standard array of all the elements.</returns>
    public Type[] ToArray()
    {
      Type[] array = new Type[_count];
      for (int i = 0; i < _count; i++)
        array[i] = _stack[i];
      return array;
    }

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private class Error : Structure.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  //#endregion

  //#region StackArrayThreadSafe<Type>

  ///// <summary>Implements a growing stack as an array (with expansions/contractions) data structure.</summary>
  ///// <typeparam name="Type">The type of objects to be placed in the list.</typeparam>
  ///// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  //[Serializable]
  //public class StackArrayThreadSafe<Type> : Stack<Type>
  //{
  //  private Type[] _stack;
  //  private int _count;
  //  private int _minimumCapacity;

  //  // This value determines the starting data structure size
  //  // at which my traversal functions will begin dynamic multithreading
  //  private object _lock;
  //  private int _readers;
  //  private int _writers;

  //  /// <summary>Gets the number of items in the list.</summary>
  //  /// <remarks>Runtime: O(1).</remarks>
  //  public int Count
  //  {
  //    get
  //    {
  //      ReaderLock();
  //      int returnValue = _count;
  //      ReaderUnlock();
  //      return returnValue;
  //    }
  //  }

  //  /// <summary>Returns true if the structure is empty.</summary>
  //  /// <remarks>Runtime: O(1).</remarks>
  //  public bool IsEmpty { get { return _count == 0; } }

  //  /// <summary>Gets the current capacity of the list.</summary>
  //  /// <remarks>Runtime: O(1).</remarks>
  //  public int CurrentCapacity
  //  {
  //    get
  //    {
  //      ReaderLock();
  //      int returnValue = _stack.Length;
  //      ReaderUnlock();
  //      return returnValue;
  //    }
  //  }

  //  /// <summary>Allows you to adjust the minimum capacity of this list.</summary>
  //  /// <remarks>Runtime: O(n), Omega(1).</remarks>
  //  public int MinimumCapacity
  //  {
  //    get
  //    {
  //      ReaderLock();
  //      int returnValue = _minimumCapacity;
  //      ReaderUnlock();
  //      return returnValue;
  //    }
  //    set
  //    {
  //      WriterLock();
  //      if (value < 1)
  //        throw new ListArrayException("Attempting to set a minimum capacity to a negative or zero value.");
  //      else if (value > _stack.Length)
  //      {
  //        Type[] newList = new Type[value];
  //        _stack.CopyTo(newList, 0);
  //        _stack = newList;
  //      }
  //      else
  //        _minimumCapacity = value;
  //      WriterUnlock();
  //    }
  //  }

  //  /// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
  //  /// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
  //  /// <remarks>Runtime: O(1).</remarks>
  //  public StackArrayThreadSafe(int minimumCapacity)
  //  {
  //    _stack = new Type[minimumCapacity];
  //    _count = 0;
  //    _minimumCapacity = minimumCapacity;
  //    _lock = new object();
  //    _readers = 0;
  //    _writers = 0;
  //  }

  //  /// <summary>Adds an item to the end of the list.</summary>
  //  /// <param name="addition">The item to be added.</param>
  //  /// <remarks>Runtime: O(n), EstAvg(1). </remarks>
  //  public void Push(Type addition)
  //  {
  //    WriterLock();
  //    if (_count == _stack.Length)
  //    {
  //      if (_stack.Length > Int32.MaxValue / 2)
  //      {
  //        WriterUnlock();
  //        throw new ListArrayException("your queue is so large that it can no longer double itself (Int32.MaxValue barrier reached).");
  //      }
  //      Type[] newStack = new Type[_stack.Length * 2];
  //      for (int i = 0; i < _count; i++)
  //        newStack[i] = _stack[i];
  //      _stack = newStack;
  //    }
  //    _stack[_count++] = addition;
  //    WriterUnlock();
  //  }

  //  /// <summary>Removes the item at a specific index.</summary>
  //  /// <remarks>Runtime: Theta(n - index).</remarks>
  //  public Type Pop()
  //  {
  //    WriterLock();
  //    if (_count == 0)
  //      throw new ListArrayException("attempting to dequeue from an empty queue.");
  //    if (_count < _stack.Length / 4 && _stack.Length / 2 > _minimumCapacity)
  //    {
  //      Type[] newQueue = new Type[_stack.Length / 2];
  //      for (int i = 0; i < _count; i++)
  //        newQueue[i] = _stack[i];
  //      _stack = newQueue;
  //    }
  //    Type returnValue = _stack[--_count];
  //    WriterUnlock();
  //    return returnValue;
  //  }

  //  public Type Peek()
  //  {
  //    ReaderLock();
  //    Type returnValue = _stack[_count - 1];
  //    ReaderUnlock();
  //    return returnValue;
  //  }

  //  /// <summary>Empties the list back and reduces it back to its original capacity.</summary>
  //  /// <remarks>Runtime: O(1).</remarks>
  //  public void Clear()
  //  {
  //    WriterLock();
  //    _stack = new Type[_minimumCapacity];
  //    _count = 0;
  //    WriterUnlock();
  //  }

  //  /// <summary>Traverses the structure and performs a function on each entry.</summary>
  //  /// <param name="traversalFunction">The function within a foreach loop.</param>
  //  /// <remarks>Runtime: O(n * traversalFunction).</remarks>
  //  public bool TraverseBreakable(Func<Type, bool> traversalFunction)
  //  {
  //    ReaderLock();
  //    for (int i = 0; i < _count; i++)
  //      if (!traversalFunction(_stack[i]))
  //      {
  //        ReaderUnlock();
  //        return false;
  //      }
  //    ReaderUnlock();
  //    return true;
  //  }

  //  /// <summary>Traverses the structure and performs a function on each entry.</summary>
  //  /// <param name="traversalFunction">The function within a foreach loop.</param>
  //  /// <param name="start">The index to start the traversal from.</param>
  //  /// <param name="end">The index to end the traversal at.</param>
  //  /// <remarks>Runtime: O((end - start) * traversalFunction).</remarks>
  //  public bool TraverseBreakable(Func<Type, bool> traversalFunction, int start, int end)
  //  {
  //    if (start < 0 || start < end || end > _count)
  //      throw new ListArrayException("invalid index parameters on traversal");
  //    ReaderLock();
  //    for (int i = start; i < end; i++)
  //      if (!traversalFunction(_stack[i]))
  //      {
  //        ReaderUnlock();
  //        return false;
  //      }
  //    ReaderUnlock();
  //    return true;
  //  }

  //  /// <summary>Traverses the structure and performs an action on each entry.</summary>
  //  /// <param name="traversalAction">The action within a foreach loop.</param>
  //  /// <remarks>Runtime: O(n * traversalAction).</remarks>
  //  public void Traverse(Action<Type> traversalAction)
  //  {
  //    ReaderLock();
  //    for (int i = 0; i < _count; i++) traversalAction(_stack[i]);
  //    ReaderUnlock();
  //  }

  //  /// <summary>Traverses the structure and performs a function on each entry.</summary>
  //  /// <param name="traversalAction">The action within a foreach loop.</param>
  //  /// <param name="start">The index to start the traversal from.</param>
  //  /// <param name="end">The index to end the traversal at.</param>
  //  /// <remarks>Runtime: O((end - start) * traversalAction).</remarks>
  //  public void Traverse(Action<Type> traversalAction, int start, int end)
  //  {
  //    if (start < 0 || start < end || end > _count)
  //      throw new ListArrayException("invalid index parameters on traversal");
  //    ReaderLock();
  //    for (int i = start; i < end; i++) traversalAction(_stack[i]);
  //    ReaderUnlock();
  //  }

  //  /// <summary>Converts the list array into a standard array.</summary>
  //  /// <returns>A standard array of all the elements.</returns>
  //  public Type[] ToArray()
  //  {
  //    ReaderLock();
  //    Type[] array = new Type[_count];
  //    for (int i = 0; i < _count; i++) array[i] = _stack[i];
  //    ReaderUnlock();
  //    return array;
  //  }

  //  /// <summary>Thread safe enterance for readers.</summary>
  //  private void ReaderLock() { lock (_lock) { while (!(_writers == 0)) Monitor.Wait(_lock); _readers++; } }
  //  /// <summary>Thread safe exit for readers.</summary>
  //  private void ReaderUnlock() { lock (_lock) { _readers--; Monitor.Pulse(_lock); } }
  //  /// <summary>Thread safe enterance for writers.</summary>
  //  private void WriterLock() { lock (_lock) { while (!(_writers == 0) && !(_readers == 0)) Monitor.Wait(_lock); _writers++; } }
  //  /// <summary>Thread safe exit for readers.</summary>
  //  private void WriterUnlock() { lock (_lock) { _writers--; Monitor.PulseAll(_lock); } }

  //  /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
  //  private class ListArrayException : Exception { public ListArrayException(string message) : base(message) { } }
  //}

  //#endregion
}