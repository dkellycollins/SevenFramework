// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Seven.Structures
{
  /// <summary>A circular structure with position tracking. aka CircularBuffer.</summary>
  /// <typeparam name="T">The type of the instances to store in this data structure.</typeparam>
  public interface Ring<T> : Structure<T>
  {
    //int Count { get; }
    //void Write(Type value);
    //Type Current { get; }
    //void Move(int ammount);
  }

  public class Ring_Array<T> : Ring<T>
  {
    T[] _ring; 
    int _nextFree;
    int _location;
    int _count;

    public Ring_Array(int length)
    {
      _ring = new T[length];
      _nextFree = 0;
      _location = 0;
      _count = 0;
    }

    public void Add(T o)
    {
      _ring[_nextFree] = o;
      _nextFree = (_nextFree+1) % _ring.Length;
      _count++;
    }

    #region .Net Framework Compatibility

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      for (int i = 0; i < _count; i++)
        yield return _ring[(i + _location) % _ring.Length];
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      for (int i = 0; i < _count; i++)
        yield return _ring[(i + _location) % _ring.Length];
    }

    #endregion

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { throw new NotImplementedException(); } }

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
    public bool Contains(T item, Compare<T> compare)
    {
      throw new NotImplementedException(); 
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<T, Key> compare)
    {
      throw new NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<T> function)
    {
      throw new NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<T> function)
    {
      throw new NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<T> function)
    {
      throw new NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<T> function)
    {
      throw new NotImplementedException();
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<T> Clone()
    {
      throw new NotImplementedException();
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public T[] ToArray()
    {
      throw new NotImplementedException();
    }
  }
}
