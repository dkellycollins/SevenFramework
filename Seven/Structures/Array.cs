// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using System;
using Seven.Parallels;
using System.Collections;
using System.Collections.Generic;

namespace Seven.Structures
{
  /// <summary>A fixed sized data structure linear in memory.</summary>
  /// <typeparam name="Type">The type of the instances to store in this data structure.</typeparam>
  public interface Array<Type> : Structure<Type>
  {
    /// <summary>Allows indexed access of the array.</summary>
    /// <param name="index">The index of the array to get/set.</param>
    /// <returns>The value at the desired index.</returns>
    Type this[int index] { get; set; }

    /// <summary>The length of the array.</summary>
    int Length { get; }
  }

  /// <summary>Implements a standard array that inherits InterfaceTraversable.</summary>
  /// <typeparam name="Type">The generic type within the structure.</typeparam>
  [Serializable]
  public class Array_Array<Type> : Array<Type>
  {
    protected Type[] _array;

    #region .NET Framework Compatibility

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    public static implicit operator Array_Array<Type>(Type[] array)
    {
      return new Array_Array<Type>(array);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    public static implicit operator Type[](Array_Array<Type> array)
    {
      
      return array.ToArray();
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      for (int i = 0; i < _array.Length; i++)
        yield return _array[i];
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
    {
      for (int i = 0; i < _array.Length; i++)
        yield return _array[i];
    }

    #endregion

    /// <summary>The length of the array.</summary>
    public int Length { get { return _array.Length; } }

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public long SizeOf { get { return _array.Length; } }

    /// <summary>Allows indexed access of the array.</summary>
    /// <param name="index">The index of the array to get/set.</param>
    /// <returns>The value at the desired index.</returns>
    public Type this[int index]
    {
      get
      {
        if (index < 0 || index > _array.Length)
          throw new Exception("index out of bounds.");
        Type returnValue = _array[index];
        return returnValue;
      }
      set
      {
        if (index < 0 || index > _array.Length)
          throw new Exception("index out of bounds.");
        _array[index] = value;
      }
    }

    /// <summary>Constructs an array that implements a traversal delegate function 
    /// which is an optimized "foreach" implementation.</summary>
    /// <param name="size">The length of the array in memory.</param>
    public Array_Array(int size)
    {
      if (size < 1)
        throw new Exception("size of the array must be at least 1.");
      this._array = new Type[size];
    }

    /// <summary>Constructs by wrapping an existing array.</summary>
    /// <param name="array">The array to be wrapped.</param>
    public Array_Array(params Type[] array)
    {
      this._array = new Type[array.Length];
      for (int i = 0; i < array.Length; i++)
        this._array[i] = array[i];
    }

    /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>An array containing all the values matching the key or null if non were found.</returns>
    public Type[] GetValues<Key>(Key key, Compare<Type, Key> compare)
    {
      int count = 0;
      for (int i = 0; i < this._array.Length; i++)
        if (compare(this._array[i], key) == Comparison.Equal)
          count++;
      Type[] values = new Type[count];
      count = 0;
      for (int i = 0; i < this._array.Length; i++)
        if (compare(this._array[i], key) == Comparison.Equal)
          values[count++] = this._array[i];
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
      for (int i = 0; i < this._array.Length; i++)
        if (compare(this._array[i], key) == Comparison.Equal)
          count++;

      if (count == 0)
      {
        values = null;
        return false;
      }

      values = new Type[count];
      count = 0;
      for (int i = 0; i < this._array.Length; i++)
        if (compare(this._array[i], key) == Comparison.Equal)
          values[count++] = this._array[i];
      return true;
    }
    
    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains(Type check, Compare<Type> compare)
    {
      for (int i = 0; i < this._array.Length; i++)
        if (compare(_array[i], check) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    {
      for (int i = 0; i < _array.Length; i++)
        if (compare(_array[i], key) == Comparison.Equal)
          return true;
      return false;
    }

    /// <summary>Looks up an item this structure by a given key.</summary>
    /// <typeparam name="Key">The type of the key to look up.</typeparam>
    /// <param name="key">The key to look up.</param>
    /// <param name="compare">A delegate representing a comparison technique between a value and a key.</param>
    /// <returns>The item with the corresponding to the given key.</returns>
    Type Get<Key>(Key key, Compare<Type, Key> compare)
    {
      for (int i = 0; i < _array.Length; i++)
        if (compare(_array[i], key) == Comparison.Equal)
          return _array[i];
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
      for (int i = 0; i < _array.Length; i++)
        if (compare(_array[i], key) == Comparison.Equal)
        {
          item = _array[i];
          return true;
        }
      item = default(Type);
      return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<Type> function)
    {
      for (int i = 0; i < _array.Length; i++)
        function(_array[i]);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<Type> function)
    {
      for (int i = 0; i < _array.Length; i++)
        function(ref _array[i]);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<Type> function)
    {
      for (int i = 0; i < _array.Length; i++)
        if (function(_array[i]) == ForeachStatus.Break)
          return ForeachStatus.Break;
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<Type> function)
    {
      for (int i = 0; i < _array.Length; i++)
        if (function(ref _array[i]) == ForeachStatus.Break)
          return ForeachStatus.Break;
      return ForeachStatus.Continue;
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<Type> Clone()
    {
      Array_Array<Type> clone = new Array_Array<Type>(_array.Length);
      for (int i = 0; i < _array.Length; i++)
        clone[i] = _array[i];
      return clone;
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public Type[] ToArray()
    {
      Type[] array = new Type[_array.Length];
      for (int i = 0; i < _array.Length; i++)
        array[i] = _array[i];
      return array;
    }

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private class Exception : Error
    {
      public Exception(string message) : base(message) { }
    }
  }
}
