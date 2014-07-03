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
  public interface Map<V, K> : Structure<V>
  {
    V this[K key] { get; set; }
    V Get(K get);
    bool TryGet(K get, out V returnValue);
    bool Contains(K containsCheck);
    void Add(K key, V value);
    void Remove(K removalKey);
    int Count { get; }
    bool IsEmpty { get; }
    void Clear();
    Map.Hash<K> Hash { get; }
    Equate<K> Equate { get; }
  }

  /// <summary>Contains extensions for the Map interface.</summary>
  public class Map
  {
    public delegate int Hash<K>(K key);
  }

  [Serializable]
  public class Map_Linked<T, K> : Map<T, K>
  {
    /// <summary>A set of allowable table sizes, all of which are prime.</summary>
    private static readonly int[] _tableSizes = new int[]
    {
        1, 2, 5, 11, 23, 47, 97, 197, 397, 797, 1597, 3203, 6421, 12853, 25717, 51437,
        102877, 205759, 411527, 823117, 1646237, 3292489, 6584983, 13169977, 26339969,
        52679969, 105359939, 210719881, 421439783, 842879579, 1685759167
    };

    private class Node
    {
      private K _key;
      private T _value;
      private Node _next;

      internal K Key { get { return _key; } set { _key = value; } }
      internal T Value { get { return _value; } set { _value = value; } }
      internal Node Next { get { return _next; } set { _next = value; } }

      internal Node(K key, T value, Node next)
      {
        _key = key;
        _value = value;
        _next = next;
      }
    }

    private const double _maxLoadFactor = 1.0d;

    private Equate<K> _equate;
    private Map.Hash<K> _hash;
    private Node[] _table;
    private int _count;
    private int _sizeIndex;

    /// <summary>The function for calculating hash codes for this table.</summary>
    public Map.Hash<K> Hash { get { return _hash; } }

    /// <summary>The function for equating keys in this table.</summary>
    public Equate<K> Equate { get { return _equate; } }

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
    public T this[K key]
    {
      get
      {
        T temp;
        if (TryGet(key, out temp))
          return temp;
        else
          throw new Error("Attempting to look up a non-existing key.");
      }
      set
      {
        Node cell = Find(key, ComputeHash(key));
        if (cell == null)
          throw new Error("Index out of range (key not found). This does not replace the add method.");
        else
          cell.Value = value;
      }
    }

    /// <summary>Constructs a new hash table instance.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public Map_Linked(Equate<K> equate, Map.Hash<K> hash)
    {
      this._equate = equate;
      this._hash = hash;
      _table = new Node[_tableSizes[0]];
      _count = 0;
      _sizeIndex = 0;
    }

    public bool Contains(K key)
    {
      Node cell = Find(key, ComputeHash(key));
      if (cell == null)
        return false;
      else
        return true;
    }

    public T Get(K key)
    {
      Node cell = Find(key, ComputeHash(key));
      if (cell == null)
        throw new Error("attempting to get a non-existing key value.");
      else
      {
        T returnValue = cell.Value;
        return returnValue;
      }
    }

    /// <summary>Typical try-get functionality for data structures.</summary>
    /// <param name="key">The key to look up the value for.</param>
    /// <param name="value">The return value if the value is found (returns default if not).</param>
    /// <returns>True if the requested key look up found a value.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    public bool TryGet(K key, out T value)
    {
      Node cell = Find(key, ComputeHash(key));
      if (cell == null)
      {
        value = default(T);
        return false;
      }
      else
      {
        value = cell.Value;
        return true;
      }
    }

    private Node Find(K key, int loc)
    {
      for (Node bucket = _table[loc]; bucket != null; bucket = bucket.Next)
        if (_equate(bucket.Key, key))
          return bucket;
      return null;
    }

    private int ComputeHash(K key) { return (_hash(key) & 0x7fffffff) % _table.Length; }

    /// <summary>Adds a value to the hash table.</summary>
    /// <param name="key">The key value to use as the look-up reference in the hash table.</param>
    /// <param name="value">The value to store in the hash table.</param>
    /// <remarks>Runtime: O(n), Omega(1).</remarks>
    public void Add(K key, T value)
    {
      if (key == null)
        throw new Error("attempting to add a null key to the structure.");
      int location = ComputeHash(key);
      if (Find(key, location) == null)
      {
        if (++_count > _table.Length * _maxLoadFactor)
        {
          if (_sizeIndex + 1 == _tableSizes.Length)
            throw new Error("maximum size " + _tableSizes[_tableSizes.Length - 1] + " of hash table reached.");

          Node[] t = _table;
          _table = new Node[_tableSizes[++_sizeIndex]];
          for (int i = 0; i < t.Length; i++)
          {
            while (t[i] != null)
            {
              Node cell = RemoveFirst(t, i);
              Add(cell, ComputeHash(cell.Key));
            }
          }
          location = ComputeHash(key);
        }
        Node p = new Node(key, value, null);
        Add(p, location);
      }
      else
        throw new Error("\nMember: \"Add(TKey key, TValue value)\"\nThe key is already in the table.");
    }

    private Node RemoveFirst(Node[] t, int i)
    {
      Node first = t[i];
      t[i] = first.Next;
      return first;
    }

    private void Add(Node cell, int location)
    {
      cell.Next = _table[location];
      _table[location] = cell;
    }

    /// <summary>Removes a value from the hash table.</summary>
    /// <param name="key">The key of the value to remove.</param>
    /// <remarks>Runtime: N/A. (I'm still editing this structure)</remarks>
    public void Remove(K key)
    {
      if (key == null)
        throw new Error("attempting to remove \"null\" from the structure.");
      int location = ComputeHash(key);
      if (_table[location].Key.Equals(key))
        _table[location] = _table[location].Next;
      for (Node bucket = _table[location]; bucket != null; bucket = bucket.Next)
      {
        if (bucket.Next == null)
          throw new Error("attempting to remove a non-existing value.");
        else if (bucket.Next.Key.Equals(key))
          bucket.Next = bucket.Next.Next;
      }
      _count--;
    }

    public void Clear()
    {
      _table = new Node[107];
      _count = 0;
      _sizeIndex = 0;
    }
    
    public T[] ToArray()
    {
      T[] array = new T[_count];
      int index = 0;
      Node node;
      for (int i = 0; i < _table.Length; i++)
        if ((node = _table[i]) != null)
          do
          {
            array[index++] = node.Value;
          } while ((node = node.Next) != null);
      return array;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      Node node;
      for (int i = 0; i < _table.Length; i++)
        if ((node = _table[i]) != null)
          do
          {
            yield return node.Value;
          } while ((node = node.Next) != null);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      Node node;
      for (int i = 0; i < _table.Length; i++)
        if ((node = _table[i]) != null)
          do
          {
            yield return node.Value;
          } while ((node = node.Next) != null);
    }

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { return _count + _table.Length; } }
    
    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<T> function)
    {
      Node node;
      for (int i = 0; i < _table.Length; i++)
        if ((node = _table[i]) != null)
          do {
            function(node.Value);
          } while ((node = node.Next) != null);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<T> function)
    {
      Node node;
      for (int i = 0; i < _table.Length; i++)
        if ((node = _table[i]) != null)
          do
          {
            T temp = node.Value;
            function(ref temp);
            node.Value = temp;
          } while ((node = node.Next) != null);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<T> function)
    {
      Node node;
      for (int i = 0; i < _table.Length; i++)
        if ((node = _table[i]) != null)
          do
          {
            if (function(node.Value) == ForeachStatus.Break)
              return ForeachStatus.Break;
          } while ((node = node.Next) != null);
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<T> function)
    {
      Node node;
      for (int i = 0; i < _table.Length; i++)
        if ((node = _table[i]) != null)
          do
          {
            T temp = node.Value;
            ForeachStatus status = function(ref temp);
            node.Value = temp;
            if (status == ForeachStatus.Break)
              return ForeachStatus.Break;
          } while ((node = node.Next) != null);
      return ForeachStatus.Continue;
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<T> Clone()
    {
      throw new NotImplementedException();
    }

    /// <summary>This is used for throwing hash table exceptions only to make debugging faster.</summary>
    private class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}