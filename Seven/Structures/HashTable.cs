// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using System;
using System.Threading;
using Seven.Structures;

using System.Collections;
using System.Collections.Generic;

namespace Seven.Structures
{
  public interface HashTable<ValueType, KeyType> : Structure<ValueType>
  {
    ValueType this[KeyType key] { get; set; }
    ValueType Get(KeyType get);
    bool TryGet(KeyType get, out ValueType returnValue);
    bool Contains(KeyType containsCheck);
    void Add(KeyType key, ValueType value);
    void Remove(KeyType removalKey);
    int Count { get; }
    bool IsEmpty { get; }
    void Clear();
  }

  #region HashTableLinked<ValueType, KeyType>

  [Serializable]
  public class HashTableLinked<ValueType, KeyType> : HashTable<ValueType, KeyType>
  {
    /// <summary>A set of allowable table sizes, all of which are prime.</summary>
    private static readonly int[] _tableSizes = new int[]
    {
        107, 223, 449, 907, 1823, 3659, 7321, 14653, 29311, 58631, 117269, 234539, 
        469099, 938207, 1876417, 3752839, 7505681, 15011389, 30022781, 60045577, 
        120091177, 240182359, 480364727, 960729461, 1921458943
    };

    #region HashTableLinkedNode

    private class HashTableLinkedNode
    {
      private KeyType _key;
      private ValueType _value;
      private HashTableLinkedNode _next;

      internal KeyType Key { get { return _key; } set { _key = value; } }
      internal ValueType Value { get { return _value; } set { _value = value; } }
      internal HashTableLinkedNode Next { get { return _next; } set { _next = value; } }

      internal HashTableLinkedNode(KeyType key, ValueType value, HashTableLinkedNode next)
      {
        _key = key;
        _value = value;
        _next = next;
      }
    }

    #endregion

    private const float _maxLoadFactor = 1.0f;

    private HashTableLinkedNode[] _table;
    private int _count;
    private int _sizeIndex;

    /// <summary>Returns the current number of items in the structure.</summary>
    /// <remarks>Runetime: O(1).</remarks>
    public int Count { get { return _count; } }

    /// <summary>Returns true if the structure is empty.</summary>
    /// <remarks>Runetime: O(1).</remarks>
    public bool IsEmpty { get { return _count == 0; } }

    /// <summary>Returns the current size of the actual table. You will want this if you 
    /// wish to multithread structure traversals.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int TableSize { get { return _table.Length; } }

    /// <summary>Allows indexed look-up of the structure. (Set does not replace the Add() method)</summary>
    /// <param name="key">The "index" to access of the structure.</param>
    /// <returns>The value at the index of the requested key.</returns>
    /// <remarks>Runtime: N/A.</remarks>
    public ValueType this[KeyType key]
    {
      get
      {
        ValueType temp;
        if (TryGet(key, out temp))
          return temp;
        else
          throw new HashTableListException("Attempting to look up a non-existing key.");
      }
      set
      {
        HashTableLinkedNode cell = Find(key, Hash(key));
        if (cell == null)
        {
          value = default(ValueType);
          throw new HashTableListException("Index out of range (key not found). This does not replace the add method.");
        }
        else
        {
          cell.Value = value;
        }
      }
    }

    /// <summary>Constructs a new hash table instance.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public HashTableLinked()
    {
      _table = new HashTableLinkedNode[107];
      _count = 0;
      _sizeIndex = 0;
    }

    public bool Contains(KeyType key)
    {
      HashTableLinkedNode cell = Find(key, Hash(key));
      if (cell == null)
        return false;
      else
        return true;
    }

    public ValueType Get(KeyType key)
    {
      HashTableLinkedNode cell = Find(key, Hash(key));
      if (cell == null)
        throw new HashTableListException("attempting to get a non-existing key value.");
      else
      {
        ValueType returnValue = cell.Value;
        return returnValue;
      }
    }

    /// <summary>Typical try-get functionality for data structures.</summary>
    /// <param name="key">The key to look up the value for.</param>
    /// <param name="value">The return value if the value is found (returns default if not).</param>
    /// <returns>True if the requested key look up found a value.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    public bool TryGet(KeyType key, out ValueType value)
    {
      HashTableLinkedNode cell = Find(key, Hash(key));
      if (cell == null)
      {
        value = default(ValueType);
        return false;
      }
      else
      {
        value = cell.Value;
        return true;
      }
    }

    private HashTableLinkedNode Find(KeyType key, int loc)
    {
      for (HashTableLinkedNode bucket = _table[loc]; bucket != null; bucket = bucket.Next)
        if (bucket.Key.Equals(key))
          return bucket;
      return null;
    }

    private int Hash(KeyType key) { return (key.GetHashCode() & 0x7fffffff) % _table.Length; }

    /// <summary>Adds a value to the hash table.</summary>
    /// <param name="key">The key value to use as the look-up reference in the hash table.</param>
    /// <param name="value">The value to store in the hash table.</param>
    /// <remarks>Runtime: O(n), Omega(1).</remarks>
    public void Add(KeyType key, ValueType value)
    {
      if (key == null)
        throw new HashTableListException("attempting to add a null key to the structure.");
      int location = Hash(key);
      if (Find(key, location) == null)
      {
        if (++_count > _table.Length * _maxLoadFactor && _sizeIndex < _tableSizes.Length - 1)
        {
          HashTableLinkedNode[] t = _table;
          _table = new HashTableLinkedNode[_tableSizes[++_sizeIndex]];
          for (int i = 0; i < t.Length; i++)
          {
            while (t[i] != null)
            {
              HashTableLinkedNode cell = RemoveFirst(t, i);
              Add(cell, Hash(cell.Key));
            }
          }
          location = Hash(key);
        }
        HashTableLinkedNode p = new HashTableLinkedNode(key, value, null);
        Add(p, location);
      }
      else
        throw new HashTableListException("\nMember: \"Add(TKey key, TValue value)\"\nThe key is already in the table.");
    }

    private HashTableLinkedNode RemoveFirst(HashTableLinkedNode[] t, int i)
    {
      HashTableLinkedNode first = t[i];
      t[i] = first.Next;
      return first;
    }

    private void Add(HashTableLinkedNode cell, int location)
    {
      cell.Next = _table[location];
      _table[location] = cell;
    }

    /// <summary>Removes a value from the hash table.</summary>
    /// <param name="key">The key of the value to remove.</param>
    /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
    public void Remove(KeyType key)
    {
      if (key == null)
        throw new HashTableListException("attempting to remove \"null\" from the structure.");
      int location = Hash(key);
      if (_table[location].Key.Equals(key))
        _table[location] = _table[location].Next;
      for (HashTableLinkedNode bucket = _table[location]; bucket != null; bucket = bucket.Next)
      {
        if (bucket.Next == null)
          throw new HashTableListException("attempting to remove a non-existing value.");
        else if (bucket.Next.Key.Equals(key))
          bucket.Next = bucket.Next.Next;
      }
      _count--;
    }

    public void Clear()
    {
      _table = new HashTableLinkedNode[107];
      _count = 0;
      _sizeIndex = 0;
    }

    /// <summary>Does an imperative traversal of the structure.</summary>
    /// <param name="traversalAction">The action to perform during traversal.</param>
    /// <returns>Whether or not the traversal was broken.</returns>
    /// <remarks>Runtime: O(n * traversalAction).</remarks>
    public bool TraverseBreakable(Func<ValueType, bool> traversalAction)
    {
      for (int i = 0; i < _table.Length; i++)
      {
        if (_table[i] == null) continue;
        HashTableLinkedNode looper = _table[i];
        while (looper != null)
        {
          if (!traversalAction(looper.Value))
            return false;
          looper = looper.Next;
        }
      }
      return true;
    }

    /// <summary>Does an imperative traversal of the structure.</summary>
    /// <param name="traversalAction">The action to perform during the traversal.</param>
    /// <remarks>Runtime: O(n * traversalAction).</remarks>
    public void Traverse(Action<ValueType> traversalAction)
    {
      for (int i = 0; i < _table.Length; i++)
      {
        if (_table[i] == null) continue;
        HashTableLinkedNode looper = _table[i];
        while (looper != null)
        {
          traversalAction(looper.Value);
          looper = looper.Next;
        }
      }
    }

    public ValueType[] ToArray()
    {
      ValueType[] array = new ValueType[_count];
      int index = 0;
      for (int i = 0; i < _table.Length; i++)
      {
        if (_table[i] == null) continue;
        HashTableLinkedNode looper = _table[i];
        while (looper != null)
        {
          array[index++] = looper.Value;
          looper = looper.Next;
        }
      }
      return array;
    }

    #region Structure<Type>

    #region .Net Framework Compatibility

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<ValueType> IEnumerable<ValueType>.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { throw new NotImplementedException(); } }

    /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    /// <typeparam name="KeyType">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>An array containing all the values matching the key or null if non were found.</returns>
    //Type[] GetValues<Key>(Key key, Compare<Type, Key> compare);

    /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    /// <typeparam name="KeyType">The type of the key to check for.</typeparam>
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
    public bool Contains(ValueType item, Compare<ValueType> compare)
    {
      throw new NotImplementedException();
    }

    ///// <summary>Checks to see if a given object is in this data structure.</summary>
    ///// <typeparam name="Key">The type of the key to check for.</typeparam>
    ///// <param name="key">The key to check for.</param>
    ///// <param name="compare">Delegate representing comparison technique.</param>
    ///// <returns>true if the item is in this structure; false if not.</returns>
    //public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    //{
    //  throw new NotImplementedException();
    //}

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<ValueType> function)
    {
      throw new NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<ValueType> function)
    {
      throw new NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<ValueType> function)
    {
      throw new NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<ValueType> function)
    {
      throw new NotImplementedException();
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<ValueType> Clone()
    {
      throw new NotImplementedException();
    }

    ///// <summary>Converts the structure into an array.</summary>
    ///// <returns>An array containing all the item in the structure.</returns>
    //public Type[] ToArray()
    //{
    //  throw new NotImplementedException();
    //}

    #endregion

    /// <summary>This is used for throwing hash table exceptions only to make debugging faster.</summary>
    private class HashTableListException : Error
    {
      public HashTableListException(string message) : base(message) { }
    }
  }

  #endregion

  #region HashTableLinkedThreadSafe<ValueType, KeyType>

  //[Serializable]
  //public class HashTableLinkedThreadSafe<Value, Key> : HashTable<Value, Key>
  //{
  //  /// <summary>A set of allowable table sizes, all of which are prime.</summary>
  //  private static readonly int[] _tableSizes = new int[]
  //  {
  //      107, 223, 449, 907, 1823, 3659, 7321, 14653, 29311, 58631, 117269, 234539, 
  //      469099, 938207, 1876417, 3752839, 7505681, 15011389, 30022781, 60045577, 
  //      120091177, 240182359, 480364727, 960729461, 1921458943
  //  };

  //  #region HashTableLinkedNode

  //  private class HashTableLinkedThreadSafeNode
  //  {
  //    private Key _key;
  //    private Value _value;
  //    private HashTableLinkedThreadSafeNode _next;

  //    internal Key Key { get { return _key; } set { _key = value; } }
  //    internal Value Value { get { return _value; } set { _value = value; } }
  //    internal HashTableLinkedThreadSafeNode Next { get { return _next; } set { _next = value; } }

  //    internal HashTableLinkedThreadSafeNode(Key key, Value value, HashTableLinkedThreadSafeNode next)
  //    {
  //      _key = key;
  //      _value = value;
  //      _next = next;
  //    }
  //  }

  //  #endregion

  //  private const float _maxLoadFactor = 1.0f;

  //  private HashTableLinkedThreadSafeNode[] _table;
  //  private int _count;
  //  private int _sizeIndex;

  //  private object _lock;
  //  private int _readers;
  //  private int _writers;

  //  /// <summary>Returns the current number of items in the structure.</summary>
  //  /// <remarks>Runetime: O(1).</remarks>
  //  public int Count { get { return _count; } }

  //  /// <summary>Returns true if the structure is empty.</summary>
  //  /// <remarks>Runetime: O(1).</remarks>
  //  public bool IsEmpty { get { return _count == 0; } }

  //  /// <summary>Returns the current size of the actual table. You will want this if you 
  //  /// wish to multithread structure traversals.</summary>
  //  /// <remarks>Runtime: O(1).</remarks>
  //  public int TableSize { get { return _table.Length; } }

  //  /// <summary>Allows indexed look-up of the structure. (Set does not replace the Add() method)</summary>
  //  /// <param name="key">The "index" to access of the structure.</param>
  //  /// <returns>The value at the index of the requested key.</returns>
  //  /// <remarks>Runtime: N/A.</remarks>
  //  public Value this[Key key]
  //  {
  //    get
  //    {
  //      ReaderLock();
  //      Value temp;
  //      if (TryGet(key, out temp))
  //      {
  //        ReaderUnlock();
  //        return temp;
  //      }
  //      else
  //      {
  //        ReaderUnlock();
  //        throw new HashTableListException("Attempting to look up a non-existing key.");
  //      }
  //    }
  //    set
  //    {
  //      WriterLock();
  //      HashTableLinkedThreadSafeNode cell = Find(key, Hash(key));
  //      if (cell == null)
  //      {
  //        value = default(Value);
  //        WriterUnlock();
  //        throw new HashTableListException("Index out of range (key not found). This does not replace the add method.");
  //      }
  //      else
  //      {
  //        cell.Value = value;
  //        WriterUnlock();
  //      }
  //    }
  //  }

  //  /// <summary>Constructs a new hash table instance.</summary>
  //  /// <remarks>Runtime: O(1).</remarks>
  //  public HashTableLinkedThreadSafe()
  //  {
  //    _table = new HashTableLinkedThreadSafeNode[107];
  //    _count = 0;
  //    _sizeIndex = 0;
  //    _lock = new object();
  //    _readers = 0;
  //    _writers = 0;
  //  }

  //  public bool Contains(Key key)
  //  {
  //    ReaderLock();
  //    HashTableLinkedThreadSafeNode cell = Find(key, Hash(key));
  //    if (cell == null)
  //    {
  //      ReaderUnlock();
  //      return false;
  //    }
  //    else
  //    {
  //      ReaderUnlock();
  //      return true;
  //    }
  //  }

  //  public Value Get(Key key)
  //  {
  //    ReaderLock();
  //    HashTableLinkedThreadSafeNode cell = Find(key, Hash(key));
  //    if (cell == null)
  //    {
  //      ReaderUnlock();
  //      throw new HashTableListException("attempting to get a non-existing key value.");
  //    }
  //    else
  //    {
  //      Value returnValue = cell.Value;
  //      ReaderUnlock();
  //      return returnValue;
  //    }
  //  }

  //  /// <summary>Typical try-get functionality for data structures.</summary>
  //  /// <param name="key">The key to look up the value for.</param>
  //  /// <param name="value">The return value if the value is found (returns default if not).</param>
  //  /// <returns>True if the requested key look up found a value.</returns>
  //  /// <remarks>Runtime: O(1).</remarks>
  //  public bool TryGet(Key key, out Value value)
  //  {
  //    ReaderLock();
  //    HashTableLinkedThreadSafeNode cell = Find(key, Hash(key));
  //    if (cell == null)
  //    {
  //      value = default(Value);
  //      ReaderUnlock();
  //      return false;
  //    }
  //    else
  //    {
  //      value = cell.Value;
  //      ReaderUnlock();
  //      return true;
  //    }
  //  }

  //  private HashTableLinkedThreadSafeNode Find(Key key, int loc)
  //  {
  //    for (HashTableLinkedThreadSafeNode bucket = _table[loc]; bucket != null; bucket = bucket.Next)
  //      if (bucket.Key.Equals(key))
  //        return bucket;
  //    return null;
  //  }

  //  private int Hash(Key key) { return (key.GetHashCode() & 0x7fffffff) % _table.Length; }

  //  /// <summary>Adds a value to the hash table.</summary>
  //  /// <param name="key">The key value to use as the look-up reference in the hash table.</param>
  //  /// <param name="value">The value to store in the hash table.</param>
  //  /// <remarks>Runtime: O(n), Omega(1).</remarks>
  //  public void Add(Key key, Value value)
  //  {
  //    WriterLock();
  //    if (key == null)
  //    {
  //      WriterUnlock();
  //      throw new HashTableListException("attempting to add a null key to the structure.");
  //    }
  //    int location = Hash(key);
  //    if (Find(key, location) == null)
  //    {
  //      if (++_count > _table.Length * _maxLoadFactor && _sizeIndex < _tableSizes.Length - 1)
  //      {
  //        HashTableLinkedThreadSafeNode[] t = _table;
  //        _table = new HashTableLinkedThreadSafeNode[_tableSizes[++_sizeIndex]];
  //        for (int i = 0; i < t.Length; i++)
  //        {
  //          while (t[i] != null)
  //          {
  //            HashTableLinkedThreadSafeNode cell = RemoveFirst(t, i);
  //            Add(cell, Hash(cell.Key));
  //          }
  //        }
  //        location = Hash(key);
  //      }
  //      HashTableLinkedThreadSafeNode p = new HashTableLinkedThreadSafeNode(key, value, null);
  //      Add(p, location);
  //      WriterUnlock();
  //    }
  //    else
  //    {
  //      WriterUnlock();
  //      throw new HashTableListException("\nMember: \"Add(TKey key, TValue value)\"\nThe key is already in the table.");
  //    }
  //  }

  //  private HashTableLinkedThreadSafeNode RemoveFirst(HashTableLinkedThreadSafeNode[] t, int i)
  //  {
  //    HashTableLinkedThreadSafeNode first = t[i];
  //    t[i] = first.Next;
  //    return first;
  //  }

  //  private void Add(HashTableLinkedThreadSafeNode cell, int location)
  //  {
  //    cell.Next = _table[location];
  //    _table[location] = cell;
  //  }

  //  /// <summary>Removes a value from the hash table.</summary>
  //  /// <param name="key">The key of the value to remove.</param>
  //  /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
  //  public void Remove(Key key)
  //  {
  //    WriterLock();
  //    if (key == null)
  //    {
  //      WriterUnlock();
  //      throw new HashTableListException("attempting to remove \"null\" from the structure.");
  //    }
  //    int location = Hash(key);
  //    if (_table[location].Key.Equals(key))
  //      _table[location] = _table[location].Next;
  //    for (HashTableLinkedThreadSafeNode bucket = _table[location]; bucket != null; bucket = bucket.Next)
  //    {
  //      if (bucket.Next == null)
  //      {
  //        WriterUnlock();
  //        throw new HashTableListException("attempting to remove a non-existing value.");
  //      }
  //      else if (bucket.Next.Key.Equals(key))
  //        bucket.Next = bucket.Next.Next;
  //    }
  //    _count--;
  //    WriterUnlock();
  //  }

  //  public void Clear()
  //  {
  //    WriterLock();
  //    _table = new HashTableLinkedThreadSafeNode[107];
  //    _count = 0;
  //    _sizeIndex = 0;
  //    WriterUnlock();
  //  }
    
  //  /// <summary>Does an imperative traversal of the structure.</summary>
  //  /// <param name="traversalAction">The action to perform during traversal.</param>
  //  /// <returns>Whether or not the traversal was broken.</returns>
  //  /// <remarks>Runtime: O(n * traversalAction).</remarks>
  //  public bool TraverseBreakable(Func<Value, bool> traversalAction)
  //  {
  //    ReaderLock();
  //    for (int i = 0; i < _table.Length; i++)
  //    {
  //      if (_table[i] == null) continue;
  //      HashTableLinkedThreadSafeNode looper = _table[i];
  //      while (looper != null)
  //      {
  //        if (!traversalAction(looper.Value)) { ReaderUnlock(); return false; }
  //        looper = looper.Next;
  //      }
  //    }
  //    ReaderUnlock();
  //    return true;
  //  }

  //  /// <summary>Does an imperative traversal of the structure.</summary>
  //  /// <param name="traversalAction">The action to perform during the traversal.</param>
  //  /// <remarks>Runtime: O(n * traversalAction).</remarks>
  //  public void Traverse(Action<Value> traversalAction)
  //  {
  //    ReaderLock();
  //    for (int i = 0; i < _table.Length; i++)
  //    {
  //      if (_table[i] == null) continue;
  //      HashTableLinkedThreadSafeNode looper = _table[i];
  //      while (looper != null)
  //      {
  //        traversalAction(looper.Value);
  //        looper = looper.Next;
  //      }
  //    }
  //    ReaderUnlock();
  //  }

  //  public Value[] ToArray()
  //  {
  //    ReaderLock();
  //    Value[] array = new Value[_count];
  //    int index = 0;
  //    for (int i = 0; i < _table.Length; i++)
  //    {
  //      if (_table[i] == null) continue;
  //      HashTableLinkedThreadSafeNode looper = _table[i];
  //      while (looper != null)
  //      {
  //        array[index++] = looper.Value;
  //        looper = looper.Next;
  //      }
  //    }
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

  //  /// <summary>This is used for throwing hash table exceptions only to make debugging faster.</summary>
  //  private class HashTableListException : Exception { public HashTableListException(string message) : base(message) { } }
  //}

  #endregion
}