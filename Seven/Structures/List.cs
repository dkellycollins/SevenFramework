// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using System;
using Seven;
using Seven.Parallels;

namespace Seven.Structures
{
  public interface List<Type> : Structure<Type>
  {
    /// <summary>Adds an item to the list.</summary>
    /// <param name="addition">The item to add to the list.</param>
    void Add(Type addition);

    //void RemoveFirst(Type removal, Compare<Type> compare);
    
    //bool TryRemoveFirst(Type removal, Compare<Type> compare);
    
    //Type GetFirst<Key>(Key key, Compare<Type, Key> compare);
    
    //bool TryGetFirst<Key>(Key key, Compare<Type, Key> compare, out Type item);

    /// <summary>Returns the number of items in the list.</summary>
    int Count { get; }

    /// <summary>Returns true if the structure is empty.</summary>
    bool IsEmpty { get; }

    /// <summary>Resets the list to an empty state. WARNING could cause excessive garbage collection.</summary>
    void Clear();
  }

  /// <summary>Implements a growing, singularly-linked list data structure that inherits InterfaceTraversable.</summary>
  /// <typeparam name="InterfaceStringId">The type of objects to be placed in the list.</typeparam>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [Serializable]
  public class List_Linked<Type> : List<Type>
  {
    /// <summary>This class just holds the data for each individual node of the list.</summary>
    protected class Node
    {
      protected Type _value;
      protected Node _next;

      internal Type Value { get { return _value; } set { _value = value; } }
      internal Node Next { get { return _next; } set { _next = value; } }

      internal Node(Type data) { _value = data; }
    }

    protected Node _head;
    protected Node _tail;
    protected int _count;
    
    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { return _count; } }

    /// <summary>Returns the number of items in the list.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int Count { get { return _count; } }

    /// <summary>Returns true if the structure is empty.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public bool IsEmpty { get { return _count == 0; } }

    #region .NET Framework Compatibility

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //public static explicit operator System.Collections.Generic.L<Type>(Type[] array)
    //{
    //  return new Array_Array<Type>(array);
    //}

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //public static explicit operator Type[](Array_Array<Type> array)
    //{
    //  return array._array;
    //}

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
        yield return looper.Value;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.Generic.IEnumerator<Type>
      System.Collections.Generic.IEnumerable<Type>.GetEnumerator()
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
        yield return looper.Value;
    }

    #endregion

    /// <summary>Creates an instance of a stalistck.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public List_Linked()
    {
      _head = _tail = null;
      _count = 0;
    }

    /// <summary>Checks to see if an object reference exists.</summary>
    /// <param name="itemReference">The reference to the object.</param>
    /// <returns>Whether or not the object reference was found.</returns>
    public bool Contains(Type itemReference)
    {
      for (Node looper = _head; looper != null; looper = looper.Next)
        if (looper.Value.Equals(itemReference))
          return true;
      return false;
    }

    /// <summary>Adds an item to the list.</summary>
    /// <param name="id">The string id of the item to add to the list.</param>
    /// <param name="addition">The item to add to the list.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public void Add(Type addition)
    {
      if (_tail == null)
        _head = _tail = new Node(addition);
      else
        _tail = _tail.Next = new Node(addition);
      _count++;
    }

    /// <summary>Removes the first equality by object reference.</summary>
    /// <param name="removal">The reference to the item to remove.</param>
    public void RemoveFirst(Type removal)
    {
      if (_head == null)
        throw new Exception("Attempting to remove a non-existing id value.");
      if (_head.Value.Equals(removal))
      {
        _head = _head.Next;
        _count--;
        return;
      }
      Node listNode = _head;
      while (listNode != null)
      {
        if (listNode.Next == null)
          throw new Exception("Attempting to remove a non-existing id value.");
        else if (_head.Value.Equals(removal))
        {
          if (listNode.Next.Equals(_tail))
            _tail = listNode;
          listNode.Next = listNode.Next.Next;
          return;
        }
        else
          listNode = listNode.Next;
      }
      throw new Exception("Attempting to remove a non-existing id value.");
    }

    /// <summary>Resets the list to an empty state. WARNING could cause excessive garbage collection.</summary>
    public void Clear()
    {
      _head = _tail = null;
      _count = 0;
    }

    /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>An array containing all the values matching the key or null if non were found.</returns>
    public Type[] GetValues<Key>(Key key, Compare<Type, Key> compare)
    {
      int count = 0;
      for (Node looper = this._head; looper != null; looper = looper.Next)
        if (compare(looper.Value, key) == Comparison.Equal)
          count++;
      Type[] values = new Type[count];
      count = 0;
      for (Node looper = this._head; looper != null; looper = looper.Next)
        if (compare(looper.Value, key) == Comparison.Equal)
          values[count++] = looper.Value;
      return values;
    }

    /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>An array containing all the values matching the key or null if non were found.</returns>
    /// <param name="values">The values that matched the given key.</param>
    /// <returns>true if 1 or more values were found; false if no values were found.</returns>
    public bool TryGetValues<Key>(Key key, Compare<Type, Key> compare, out Type[] values)
    {
      int count = 0;
      for (Node looper = this._head; looper != null; looper = looper.Next)
        if (compare(looper.Value, key) == Comparison.Equal)
          count++;

      if (count == 0)
      {
        values = null;
        return false;
      }

      values = new Type[count];
      count = 0;
      for (Node looper = this._head; looper != null; looper = looper.Next)
        if (compare(looper.Value, key) == Comparison.Equal)
          values[count++] = looper.Value;
      return true;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains(Type item, Compare<Type> compare)
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
        if (compare(looper.Value, item) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
        if (compare(looper.Value, key) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Looks up an item this structure by a given key.</summary>
    /// <typeparam name="Key">The type of the key to look up.</typeparam>
    /// <param name="key">The key to look up.</param>
    /// <param name="compare">A delegate representing a comparison technique between a value and a key.</param>
    /// <returns>The item with the corresponding to the given key.</returns>
    /// <exception cref="List_Linked.Exception">key not found.</exception>
    Type Get<Key>(Key key, Compare<Type, Key> compare)
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
        if (compare(looper.Value, key) == Comparison.Equal)
          return looper.Value;
      throw new Exception("key not found.");
    }

    /// <summary>Trys to look up an item this structure by a given key.</summary>
    /// <typeparam name="Key">The type of the key to look up.</typeparam>
    /// <param name="key">The key to look up.</param>
    /// <param name="compare">A delegate representing a comparison technique between a value and a key.</param>
    /// <param name="item">The item if it was found or null if not the default(Type) value.</param>
    /// <returns>true if the key was found; false if the key was not found.</returns>
    bool TryGet<Key>(Key key, Compare<Type, Key> compare, out Type item)
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
        if (compare(looper.Value, key) == Comparison.Equal)
        {
          item = looper.Value;
          return true;
        }
      item = default(Type);
      return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<Type> function)
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
        function(looper.Value);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<Type> function)
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
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
      for (Node looper = this._head; looper != null; looper = looper.Next)
      {
        if (function(looper.Value) == ForeachStatus.Break)
          return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      for (Node looper = this._head; looper != null; looper = looper.Next)
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
      List_Linked<Type> clone = new List_Linked<Type>();
      for (Node looper = this._head; looper != null; looper = looper.Next)
        clone.Add(looper.Value);
      return clone;
    }

    /// <summary>Converts the list into a standard array.</summary>
    /// <returns>A standard array of all the items.</returns>
    /// <remarks>Runtime: Theta(n).</remarks>
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

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private class Exception : Error { public Exception(string message) : base(message) { } }
  }

  /// <summary>Implements a growing, singularly-linked list data structure that inherits InterfaceTraversable.</summary>
  /// <typeparam name="InterfaceStringId">The type of objects to be placed in the list.</typeparam>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [Serializable]
  public class List_Linked_ThreadSafe<Type> : List_Linked<Type>
  {
    ReaderWriterLock _readerWriterLock;
    
    /// <summary>Creates an instance of a stalistck.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public List_Linked_ThreadSafe() : base()
    {
      this._readerWriterLock = new ReaderWriterLock();

      throw new NotImplementedException();
    }

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private class Exception : Error { public Exception(string message) : base(message) { } }
  }

  /// <summary>Implements a growing list as an array (with expansions/contractions) 
  /// data structure that inherits InterfaceTraversable.</summary>
  /// <typeparam name="Type">The type of objects to be placed in the list.</typeparam>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [Serializable]
  public class List_Array<Type> : List<Type>
  {
    protected Type[] _list;
    protected int _count;
    protected int _minimumCapacity;

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
        int returnValue = _list.Length;
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
          throw new Exception("Attempting to set a minimum capacity to a negative or zero value.");
        else if (value > _list.Length)
        {
          Type[] newList = new Type[value];
          _list.CopyTo(newList, 0);
          _list = newList;
        }
        else
          _minimumCapacity = value;
      }
    }

    /// <summary>Look-up and set an indexed item in the list.</summary>
    /// <param name="index">The index of the item to get or set.</param>
    /// <returns>The value at the given index.</returns>
    public Type this[int index]
    {
      get
      {
        if (index < 0 || index > _count)
        {
          throw new Exception("Attempting an index look-up outside the ListArray's current size.");
        }
        Type returnValue = _list[index];
        return returnValue;
      }
      set
      {
        if (index < 0 || index > _count)
        {
          throw new Exception("Attempting an index assignment outside the ListArray's current size.");
        }
        _list[index] = value;
      }
    }

    /// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
    /// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public List_Array(int minimumCapacity)
    {
      _list = new Type[minimumCapacity];
      _count = 0;
      _minimumCapacity = minimumCapacity;
    }

    /// <summary>Determines if an object reference exists in the array.</summary>
    /// <param name="reference">The reference to the object.</param>
    /// <returns>Whether or not the object reference exists.</returns>
    public bool Contains(Type reference)
    {
      for (int i = 0; i < _count; i++)
        if (_list[i].Equals(reference))
          return true;
      return false;
    }

    /// <summary>Adds an item to the end of the list.</summary>
    /// <param name="addition">The item to be added.</param>
    /// <remarks>Runtime: O(n), EstAvg(1). </remarks>
    public void Add(Type addition)
    {
      if (_count == _list.Length)
      {
        if (_list.Length > int.MaxValue / 2)
          throw new Exception("Your list is so large that it can no longer double itself (int.MaxValue barrier reached).");
        Type[] newList = new Type[_list.Length * 2];
        _list.CopyTo(newList, 0);
        _list = newList;
      }
      _list[_count++] = addition;
    }

    /// <summary>Removes the item at a specific index.</summary>
    /// <param name="index">The index of the item to be removed.</param>
    /// <remarks>Runtime: Theta(n - index).</remarks>
    public void Remove(int index)
    {
      if (index < 0 || index > _count)
        throw new Exception("Attempting to remove an index outside the ListArray's current size.");
      if (_count < _list.Length / 4 && _list.Length / 2 > _minimumCapacity)
      {
        Type[] newList = new Type[_list.Length / 2];
        for (int i = 0; i < _count; i++)
          newList[i] = _list[i];
        _list = newList;
      }
      for (int i = index; i < _count; i++)
        _list[i] = _list[i + 1];
      _count--;
    }

    /// <summary>Removes the first equality by object reference.</summary>
    /// <param name="removal">The reference to the item to remove.</param>
    public void RemoveFirst(Type removal)
    {
      for (int index = 0; index < _count; index++)
        if (_list[index].Equals(removal))
        {
          if (_count < _list.Length / 4 && _list.Length / 2 > _minimumCapacity)
          {
            Type[] newList = new Type[_list.Length / 2];
            for (int i = 0; i < _count; i++)
              newList[i] = _list[i];
            _list = newList;
          }
          for (int i = index; i < _count - 1; i++)
            _list[i] = _list[i + 1];
          _count--;
          return;
        }
      throw new Exception("attempting to remove a non-existing value.");
    }

    /// <summary>Empties the list back and reduces it back to its original capacity.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public void Clear()
    {
      _list = new Type[_minimumCapacity];
      _count = 0;
    }

    #region .Net Framework Compatibility

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //public static explicit operator System.Collections.Generic.L<Type>(Type[] array)
    //{
    //  return new Array_Array<Type>(array);
    //}

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //public static explicit operator Type[](Array_Array<Type> array)
    //{
    //  return array._array;
    //}

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      for (int i = 0; i < this._count; i++)
        yield return this._list[i];
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.Generic.IEnumerator<Type>
      System.Collections.Generic.IEnumerable<Type>.GetEnumerator()
    {
      for (int i = 0; i < this._count; i++)
        yield return this._list[i];
    }
    #endregion

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { return this._list.Length; } }

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
      for (int i = 0; i < this._count; i++)
        if (compare(this._list[i], item) == Comparison.Equal)
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
      for (int i = 0; i < this._count; i++)
        if (compare(this._list[i], key) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<Type> function)
    {
      for (int i = 0; i < this._count; i++)
        function(this._list[i]);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<Type> function)
    {
      for (int i = 0; i < this._count; i++)
        function(ref this._list[i]);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<Type> function)
    {
      for (int i = 0; i < this._count; i++)
        if (function(this._list[i]) == ForeachStatus.Break)
          return ForeachStatus.Break;
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      for (int i = 0; i < this._count; i++)
        if (function(ref this._list[i]) == ForeachStatus.Break)
          return ForeachStatus.Break;
      return ForeachStatus.Continue;
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<Type> Clone()
    {
      List_Array<Type> clone = new List_Array<Type>(this._minimumCapacity);
      for (int i = 0; i < this._count; i++)
        clone.Add(this._list[i]);
      return clone;
    }

    /// <summary>Converts the list array into a standard array.</summary>
    /// <returns>A standard array of all the elements.</returns>
    public Type[] ToArray()
    {
      Type[] array = new Type[_count];
      for (int i = 0; i < _count; i++) array[i] = _list[i];
      return array;
    }
    
    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    protected class Exception : Error { public Exception(string message) : base(message) { } }
  }

  /// <summary>Implements a growing, singularly-linked list data structure that inherits InterfaceTraversable.</summary>
  /// <typeparam name="InterfaceStringId">The type of objects to be placed in the list.</typeparam>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [Serializable]
  public class List_Array_ThreadSafe<Type> : List_Array<Type>
  {
    ReaderWriterLock _readerWriterLock;

    /// <summary>Creates an instance of a stalistck.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public List_Array_ThreadSafe(int minimumCapacity)
      : base(minimumCapacity)
    {
      this._readerWriterLock = new ReaderWriterLock();

      throw new NotImplementedException();
    }

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private new class Exception : Error { public Exception(string message) : base(message) { } }
  }

  /// <summary>WARNING: THIS IMPLEMENTATION IS INTENDED FOR EDUCATIONAL PURPOSES VS USAGE.</summary>
  /// <typeparam name="Type">The type of objects to be placed in the list.</typeparam>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  [Serializable]
  public class List_Delegate<Type> : List<Type>
  {
    protected delegate void List();
    protected List _list;
    protected Foreach<Type> _operation;
    protected int _count;

    /// <summary>Gets the number of items in the list.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int Count { get { return _count; } }

    /// <summary>Returns true if the structure is empty.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public bool IsEmpty { get { return _count == 0; } }
    
    /// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
    /// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public List_Delegate()
    {
      _count = 0;
    }

    /// <summary>Determines if an object reference exists in the array.</summary>
    /// <param name="reference">The reference to the object.</param>
    /// <returns>Whether or not the object reference exists.</returns>
    public bool Contains(Type reference)
    {
      throw new NotSupportedException("List_Delegate does not support contains checking");
    }

    /// <summary>Adds an item to the end of the list.</summary>
    /// <param name="addition">The item to be added.</param>
    /// <remarks>Runtime: O(n), EstAvg(1). </remarks>
    public void Add(Type addition)
    {
      this._list += () => { this._operation(addition); };
      this._count++;
    }

    /// <summary>Removes the first equality by object reference.</summary>
    /// <param name="removal">The reference to the item to remove.</param>
    public void RemoveFirst(Type removal)
    {
      throw new NotSupportedException("List_Delegate does not support contains checking");
      //this._list -= () => { this._operation(addition); };
      //this._count--;
    }

    /// <summary>Empties the list back and reduces it back to its original capacity.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public void Clear()
    {
      _list = null;
      _count = 0;
    }

    #region .Net Framework Compatibility

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //public static explicit operator System.Collections.Generic.L<Type>(Type[] array)
    //{
    //  return new Array_Array<Type>(array);
    //}

    ///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //public static explicit operator Type[](Array_Array<Type> array)
    //{
    //  return array._array;
    //}

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      //_operation = (Type current) => { yield return current; };

      throw new NotSupportedException("List_Delegate does not support contains checking");
      //yield return this._list();
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    System.Collections.Generic.IEnumerator<Type>
      System.Collections.Generic.IEnumerable<Type>.GetEnumerator()
    {
      throw new NotSupportedException("List_Delegate does not support contains checking");
      //_operation = () => yield return 
      //yield return this._list();
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
      throw new NotSupportedException("List_Delegate does not support contains checking");
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    {
      throw new NotSupportedException("List_Delegate does not support contains checking");
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<Type> function)
    {
      this._operation = function;
      this._list();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<Type> function)
    {
      throw new NotSupportedException("List_Delegate does not support contains checking");
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<Type> function)
    {
      throw new NotSupportedException("List_Delegate does not support contains checking");
      //this._operation = function;
      //this._list();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      throw new NotSupportedException("List_Delegate does not support contains checking");
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<Type> Clone()
    {
      List_Delegate<Type> clone = new List_Delegate<Type>();
      clone._list = (List)this._list.Clone();
      clone._count = this._count;
      return clone;
    }

    /// <summary>Converts the list array into a standard array.</summary>
    /// <returns>A standard array of all the elements.</returns>
    public Type[] ToArray()
    {
      throw new NotSupportedException("List_Delegate does not support contains checking");
    }

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    protected class Exception : Error { public Exception(string message) : base(message) { } }
  }
}
