using System;
using System.Collections;
using System.Collections.Generic;

namespace Seven.Structures
{
  /// <summary>Represents a link between objects.</summary>
  public interface Link : Structure<object>
  {
    /// <summary>Gets an array with all the types contained in this link in respective order.</summary>
    /// <returns>An array of all the types in this link in respective order.</returns>
    Type[] Types();

    /// <summary>The number of objects in the tuple.</summary>
    int Size { get; }
  }

  /// <summary>Represents a link between objects.</summary>
  /// <typeparam name="TypeOne">The type of the left item to be linked.</typeparam>
  [Serializable]
  public class Link<TypeOne>
    : Link
  {
    protected dynamic _one;

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public long SizeOf { get { return 1; } }

    /// <summary>The number of objects in the tuple.</summary>
    public int Size { get { return 1; } }

    /// <summary>The left item in the link.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public TypeOne One { get { return this._one; } set { this._one = value; } }

    #region .NET Framework Compatibility

    /// <summary>explicitly casts a System.Tuple to a Seven.Structures.Link.</summary>
    /// <param name="tuple">The System.Tuple to be casted.</param>
    /// <returns>A Seven.Structures.Link casted from the System.Tuple.</returns>
    public static explicit operator Link<TypeOne>(Tuple<TypeOne> tuple)
    {
      return new Link<TypeOne>(tuple.Item1);
    }

    /// <summary>explicitly casts a Seven.Structures.Link to a System.Tuple.</summary>
    /// <param name="tuple">The Seven.Structures.Link to be casted.</param>
    /// <returns>The System.Tuple casted Seven.Structures.Link.</returns>
    public static explicit operator Tuple<TypeOne>(Link<TypeOne> link)
    {
      return new Tuple<TypeOne>(link._one);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      yield return this._one;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
      yield return this._one;
    }

    #endregion

    /// <summary>Creates a link between objects.</summary>
    /// <param name="one">The first item to be linked.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public Link(TypeOne one)
    {
      this._one = one;
    }

    /// <summary>Gets an array with all the types contained in this link in respective order.</summary>
    /// <returns>An array of all the types in this link in respective order.</returns>
    public Type[] Types()
    {
      return new Type[]
      {
        typeof(TypeOne)
      };
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains(dynamic item, Compare<dynamic> compare)
    {
      if (compare(this._one, item) == Comparison.Equal)
        return true;
      return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<dynamic, Key> compare)
    {
      if (compare(this._one, key) == Comparison.Equal)
        return true;
      return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<dynamic> function)
    {
      function(this._one);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<dynamic> function)
    {
      function(ref this._one);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<dynamic> function)
    {
      return function(this._one);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<dynamic> function)
    {
      return function(ref this._one);
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<object> Clone()
    {
      return new Link<TypeOne>(this._one);
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public virtual dynamic[] ToArray()
    {
      return new dynamic[]
      {
        this._one
      };
    }
  }

  /// <summary>Represents a link between objects.</summary>
  /// <typeparam name="TypeOne">The type of the left item to be linked.</typeparam>
  /// <typeparam name="TypeTwo">The type of the right item to be linked.</typeparam>
  [Serializable]
  public class Link<TypeOne, TypeTwo> : Link
  {
    protected dynamic _one;
    protected dynamic _two;

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public  long SizeOf { get { return 2; } }

    /// <summary>The number of objects in the tuple.</summary>
    public  int Size { get { return 2; } }

    /// <summary>The right item in the link.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public TypeTwo Two { get { return this._two; } set { this._two = value; } }

    #region .NET Framework Compatibility

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    public static explicit operator Link<TypeOne, TypeTwo>(Tuple<TypeOne, TypeTwo> tuple)
    {
      return new Link<TypeOne, TypeTwo>(tuple.Item1, tuple.Item2);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    public static explicit operator Tuple<TypeOne, TypeTwo>(Link<TypeOne, TypeTwo> link)
    {
      return new Tuple<TypeOne, TypeTwo>(link._one, link._two);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
    }

    #endregion

    /// <summary>Creates a link between objects.</summary>
    /// <param name="one">The first item to be linked.</param>
    /// <param name="two">The second item to be linked.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public Link(TypeOne one, TypeTwo two)
    {
      this._one = one;
      this._two = two;
    }

    /// <summary>Gets an array with all the types contained in this link in respective order.</summary>
    /// <returns>An array of all the types in this link in respective order.</returns>
    public  Type[] Types()
    {
      return new Type[]
      {
        typeof(TypeOne),
        typeof(TypeTwo)
      };
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains(dynamic item, Compare<dynamic> compare)
    {
      if (compare(this._one, item) == Comparison.Equal)
        return true;
      if (compare(this._two, item) == Comparison.Equal)
        return true;
      return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public bool Contains<Key>(Key key, Compare<dynamic, Key> compare)
    {
      if (compare(this._one, key) == Comparison.Equal)
        return true;
      if (compare(this._two, key) == Comparison.Equal)
        return true;
      return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<dynamic> function)
    {
      function(this._one);
      function(this._two);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<dynamic> function)
    {
      function(ref this._one);
      function(ref this._two);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachBreak<dynamic> function)
    {
      if (function(this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(this._two);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachRefBreak<dynamic> function)
    {
      if (function(ref this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(ref this._two);
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public  Structure<object> Clone()
    {
      return new Link<TypeOne, TypeTwo>(this._one, this._two);
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public  dynamic[] ToArray()
    {
      return new dynamic[]
      {
        this._one,
        this._two
      };
    }
  }

  /// <summary>Represents a link between objects.</summary>
  /// <typeparam name="TypeOne">The type of the first item to be linked.</typeparam>
  /// <typeparam name="TypeTwo">The type of the second item to be linked.</typeparam>
  /// <typeparam name="TypeThree">The type of the third item to be linked.</typeparam>
  [Serializable]
  public class Link<TypeOne, TypeTwo, TypeThree> : Link
  {
    protected dynamic _one;
    protected dynamic _two;
    protected dynamic _three;

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public  long SizeOf { get { return 3; } }

    /// <summary>The number of objects in the tuple.</summary>
    public  int Size { get { return 3; } }

    /// <summary>The third item in the link.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public TypeThree Three { get { return this._three; } set { this._three = value; } }

    #region .NET Framework Compatibility

    /// <summary>explicitly casts a System.Tuple to a Seven.Structures.Link.</summary>
    /// <param name="tuple">The System.Tuple to be casted.</param>
    /// <returns>A Seven.Structures.Link casted from the System.Tuple.</returns>
    public static explicit operator Link<TypeOne, TypeTwo, TypeThree>(Tuple<TypeOne, TypeTwo, TypeThree> tuple)
    {
      return new Link<TypeOne, TypeTwo, TypeThree>(tuple.Item1, tuple.Item2, tuple.Item3);
    }

    /// <summary>explicitly casts a Seven.Structures.Link to a System.Tuple.</summary>
    /// <param name="tuple">The Seven.Structures.Link to be casted.</param>
    /// <returns>The System.Tuple casted Seven.Structures.Link.</returns>
    public static explicit operator Tuple<TypeOne, TypeTwo, TypeThree>(Link<TypeOne, TypeTwo, TypeThree> link)
    {
      return new Tuple<TypeOne, TypeTwo, TypeThree>(link._one, link._two, link._three);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
    }

    #endregion

    /// <summary>Creates a link between objects.</summary>
    /// <param name="one">The first item to be linked.</param>
    /// <param name="two">The second item to be linked.</param>
    /// <param name="three">The third item to be linked.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public Link(TypeOne one, TypeTwo two, TypeThree three)
    {
      this._one = one;
      this._two = two;
      this._three = three;
    }

    /// <summary>Gets an array with all the types contained in this link in respective order.</summary>
    /// <returns>An array of all the types in this link in respective order.</returns>
    public  Type[] Types()
    {
      return new Type[]
      {
        typeof(TypeOne),
        typeof(TypeTwo),
        typeof(TypeThree)
      };
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains(dynamic item, Compare<dynamic> compare)
    {
      if (compare(this._one, item) == Comparison.Equal)
        return true;
      else if (compare(this._two, item) == Comparison.Equal)
        return true;
      else if (compare(this._three, item) == Comparison.Equal)
        return true;
      return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains<Key>(Key key, Compare<dynamic, Key> compare)
    {
      if (compare(this._one, key) == Comparison.Equal)
        return true;
      else if (compare(this._two, key) == Comparison.Equal)
        return true;
      else if (compare(this._three, key) == Comparison.Equal)
        return true;
      return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(Foreach<dynamic> function)
    {
      function(this._one);
      function(this._two);
      function(this._three);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(ForeachRef<dynamic> function)
    {
      function(ref this._one);
      function(ref this._two);
      function(ref this._three);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachBreak<dynamic> function)
    {
      if (function(this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(this._three);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachRefBreak<dynamic> function)
    {
      if (function(ref this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(ref this._three);
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public  Structure<object> Clone()
    {
      return new Link<TypeOne, TypeTwo, TypeThree>
        (this._one, this._two, this._three);
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public  dynamic[] ToArray()
    {
      return new dynamic[]
      {
        this._one,
        this._two,
        this._three
      };
    }
  }

  /// <summary>Represents a link between objects.</summary>
  /// <typeparam name="TypeOne">The type of the first item to be linked.</typeparam>
  /// <typeparam name="TypeTwo">The type of the second item to be linked.</typeparam>
  /// <typeparam name="TypeThree">The type of the third item to be linked.</typeparam>
  /// <typeparam name="TypeFour">The type of the fourth item to be linked.</typeparam>
  [Serializable]
  public class Link<TypeOne, TypeTwo, TypeThree, TypeFour>
    : Link
  {
    protected dynamic _one;
    protected dynamic _two;
    protected dynamic _three;
    protected dynamic _four;

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public  long SizeOf { get { return 4; } }

    /// <summary>The number of objects in the tuple.</summary>
    public  int Size { get { return 4; } }

    /// <summary>The fourth item in the link.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public TypeFour Four { get { return this._four; } set { this._four = value; } }

    #region .NET Framework Compatibility

    /// <summary>explicitly casts a System.Tuple to a Seven.Structures.Link.</summary>
    /// <param name="tuple">The System.Tuple to be casted.</param>
    /// <returns>A Seven.Structures.Link casted from the System.Tuple.</returns>
    public static explicit operator Link<TypeOne, TypeTwo, TypeThree, TypeFour>
      (Tuple<TypeOne, TypeTwo, TypeThree, TypeFour> tuple)
    {
      return new Link<TypeOne, TypeTwo, TypeThree, TypeFour>
        (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
    }

    /// <summary>explicitly casts a Seven.Structures.Link to a System.Tuple.</summary>
    /// <param name="tuple">The Seven.Structures.Link to be casted.</param>
    /// <returns>The System.Tuple casted Seven.Structures.Link.</returns>
    public static explicit operator Tuple<TypeOne, TypeTwo, TypeThree, TypeFour>
      (Link<TypeOne, TypeTwo, TypeThree, TypeFour> link)
    {
      return new Tuple<TypeOne, TypeTwo, TypeThree, TypeFour>(link._one, link._two, link._three, link._four);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
      yield return this._four;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
      yield return this._four;
    }

    #endregion

    /// <summary>Creates a link between objects.</summary>
    /// <param name="one">The left item to be linked.</param>
    /// <param name="two">The second item to be linked.</param>
    /// <param name="three">The third item to be linked.</param>
    /// <param name="four">The fourth item to be linked.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public Link(TypeOne one, TypeTwo two, TypeThree three, TypeFour four)
    {
      this._one = one;
      this._two = two;
      this._three = three;
      this._four = four;
    }

    /// <summary>Gets an array with all the types contained in this link in respective order.</summary>
    /// <returns>An array of all the types in this link in respective order.</returns>
    public  Type[] Types()
    {
      return new Type[]
      {
        typeof(TypeOne),
        typeof(TypeTwo),
        typeof(TypeThree),
        typeof(TypeFour)
      };
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains(dynamic item, Compare<dynamic> compare)
    {
      if (compare(this._one, item) == Comparison.Equal)
        return true;
      else if (compare(this._two, item) == Comparison.Equal)
        return true;
      else if (compare(this._three, item) == Comparison.Equal)
        return true;
      else if (compare(this._four, item) == Comparison.Equal)
        return true;
      else
        return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains<Key>(Key key, Compare<dynamic, Key> compare)
    {
      if (compare(this._one, key) == Comparison.Equal)
        return true;
      else if (compare(this._two, key) == Comparison.Equal)
        return true;
      else if (compare(this._three, key) == Comparison.Equal)
        return true;
      else if (compare(this._four, key) == Comparison.Equal)
        return true;
      else
        return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(Foreach<dynamic> function)
    {
      function(this._one);
      function(this._two);
      function(this._three);
      function(this._four);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(ForeachRef<dynamic> function)
    {
      function(ref this._one);
      function(ref this._two);
      function(ref this._three);
      function(ref this._four);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachBreak<dynamic> function)
    {
      if (function(this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._three) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(this._four);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachRefBreak<dynamic> function)
    {
      if (function(ref this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._three) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(ref this._four);
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public  Structure<object> Clone()
    {
      return new Link<TypeOne, TypeTwo, TypeThree, TypeFour>
        (this._one, this._two, this._three, this._four);
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public  dynamic[] ToArray()
    {
      return new dynamic[]
      {
        this._one,
        this._two,
        this._three,
        this._four
      };
    }
  }

  /// <summary>Represents a link between objects.</summary>
  /// <typeparam name="TypeOne">The type of the first item to be linked.</typeparam>
  /// <typeparam name="TypeTwo">The type of the second item to be linked.</typeparam>
  /// <typeparam name="TypeThree">The type of the third item to be linked.</typeparam>
  /// <typeparam name="TypeFour">The type of the fourth item to be linked.</typeparam>
  /// <typeparam name="TypeFive">The type of the fifth item to be linked.</typeparam>
  [Serializable]
  public class Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive>
    : Link
  {
    protected dynamic _one;
    protected dynamic _two;
    protected dynamic _three;
    protected dynamic _four;
    protected dynamic _five;

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public  long SizeOf { get { return 5; } }

    /// <summary>The number of objects in the tuple.</summary>
    public  int Size { get { return 5; } }

    /// <summary>The fifth item in the link.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public TypeFive Five { get { return this._five; } set { this._five = value; } }

    #region .NET Framework Compatibility

    /// <summary>explicitly casts a System.Tuple to a Seven.Structures.Link.</summary>
    /// <param name="tuple">The System.Tuple to be casted.</param>
    /// <returns>A Seven.Structures.Link casted from the System.Tuple.</returns>
    public static explicit operator Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive>
      (Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive> tuple)
    {
      return new Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive>
        (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
    }

    /// <summary>explicitly casts a Seven.Structures.Link to a System.Tuple.</summary>
    /// <param name="tuple">The Seven.Structures.Link to be casted.</param>
    /// <returns>The System.Tuple casted Seven.Structures.Link.</returns>
    public static explicit operator Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive>
      (Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive> link)
    {
      return new Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive>
        (link._one, link._two, link._three, link._four, link._five);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
      yield return this._four;
      yield return this._five;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
      yield return this._four;
      yield return this._five;
    }

    #endregion

    /// <summary>Creates a link between objects.</summary>
    /// <param name="one">The first item to be linked.</param>
    /// <param name="two">The second item to be linked.</param>
    /// <param name="three">The third item to be linked.</param>
    /// <param name="four">The fourth item to be linked.</param>
    /// <param name="five">The fourth item to be linked.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public Link(TypeOne one, TypeTwo two, TypeThree three, TypeFour four, TypeFive five)
    {
      this._one = one;
      this._two = two;
      this._three = three;
      this._four = four;
      this._five = five;
    }

    /// <summary>Gets an array with all the types contained in this link in respective order.</summary>
    /// <returns>An array of all the types in this link in respective order.</returns>
    public  Type[] Types()
    {
      return new Type[]
      {
        typeof(TypeOne),
        typeof(TypeTwo),
        typeof(TypeThree),
        typeof(TypeFour),
        typeof(TypeFive)
      };
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains(dynamic item, Compare<dynamic> compare)
    {
      if (compare(this._one, item) == Comparison.Equal)
        return true;
      else if (compare(this._two, item) == Comparison.Equal)
        return true;
      else if (compare(this._three, item) == Comparison.Equal)
        return true;
      else if (compare(this._four, item) == Comparison.Equal)
        return true;
      else if (compare(this._five, item) == Comparison.Equal)
        return true;
      else
        return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains<Key>(Key key, Compare<dynamic, Key> compare)
    {
      if (compare(this._one, key) == Comparison.Equal)
        return true;
      else if (compare(this._two, key) == Comparison.Equal)
        return true;
      else if (compare(this._three, key) == Comparison.Equal)
        return true;
      else if (compare(this._four, key) == Comparison.Equal)
        return true;
      else if (compare(this._five, key) == Comparison.Equal)
        return true;
      else
        return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(Foreach<dynamic> function)
    {
      function(this._one);
      function(this._two);
      function(this._three);
      function(this._four);
      function(this._five);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(ForeachRef<dynamic> function)
    {
      function(ref this._one);
      function(ref this._two);
      function(ref this._three);
      function(ref this._four);
      function(ref this._five);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachBreak<dynamic> function)
    {
      if (function(this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._three) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._four) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(this._five);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachRefBreak<dynamic> function)
    {
      if (function(ref this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._three) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._four) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(ref this._five);
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public  Structure<object> Clone()
    {
      return new Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive>
        (this._one, this._two, this._three, this._four, this._five);
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public  dynamic[] ToArray()
    {
      return new dynamic[]
      {
        this._one,
        this._two,
        this._three,
        this._four,
        this._five
      };
    }
  }

  /// <summary>Represents a link between objects.</summary>
  /// <typeparam name="TypeOne">The type of the first item to be linked.</typeparam>
  /// <typeparam name="TypeTwo">The type of the second item to be linked.</typeparam>
  /// <typeparam name="TypeThree">The type of the third item to be linked.</typeparam>
  /// <typeparam name="TypeFour">The type of the fourth item to be linked.</typeparam>
  /// <typeparam name="TypeFive">The type of the fifth item to be linked.</typeparam>
  /// <typeparam name="TypeSix">The type of the sixth item to be linked.</typeparam>
  [Serializable]
  public class Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix>
    : Link
  {
    protected dynamic _one;
    protected dynamic _two;
    protected dynamic _three;
    protected dynamic _four;
    protected dynamic _five;
    protected dynamic _six;

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public  long SizeOf { get { return 6; } }

    /// <summary>The number of objects in the tuple.</summary>
    public  int Size { get { return 6; } }

    /// <summary>The sixth item in the link.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public TypeSix Six { get { return this._six; } set { this._six = value; } }

    #region .NET Framework Compatibility

    /// <summary>explicitly casts a System.Tuple to a Seven.Structures.Link.</summary>
    /// <param name="tuple">The System.Tuple to be casted.</param>
    /// <returns>A Seven.Structures.Link casted from the System.Tuple.</returns>
    public static explicit operator
      Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix>
      (Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix> tuple)
    {
      return new Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix>
        (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
    }

    /// <summary>explicitly casts a Seven.Structures.Link to a System.Tuple.</summary>
    /// <param name="tuple">The Seven.Structures.Link to be casted.</param>
    /// <returns>The System.Tuple casted Seven.Structures.Link.</returns>
    public static explicit operator
      Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix>
      (Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix> link)
    {
      return new Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix>
        (link._one, link._two, link._three, link._four, link._five, link._six);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
      yield return this._four;
      yield return this._five;
      yield return this._six;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
      yield return this._four;
      yield return this._five;
      yield return this._six;
    }

    #endregion

    /// <summary>Creates a link between objects.</summary>
    /// <param name="one">The first item to be linked.</param>
    /// <param name="two">The second item to be linked.</param>
    /// <param name="three">The third item to be linked.</param>
    /// <param name="four">The fourth item to be linked.</param>
    /// <param name="five">The fourth item to be linked.</param>
    /// <param name="six">The fourth item to be linked.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public Link(TypeOne one, TypeTwo two, TypeThree three, TypeFour four, TypeFive five, TypeSix six)
    {
      this._one = one;
      this._two = two;
      this._three = three;
      this._four = four;
      this._five = five;
      this._six = six;
    }

    /// <summary>Gets an array with all the types contained in this link in respective order.</summary>
    /// <returns>An array of all the types in this link in respective order.</returns>
    public  Type[] Types()
    {
      return new Type[]
      {
        typeof(TypeOne),
        typeof(TypeTwo),
        typeof(TypeThree),
        typeof(TypeFour),
        typeof(TypeFive),
        typeof(TypeSix)
      };
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains(dynamic item, Compare<dynamic> compare)
    {
      if (compare(this._one, item) == Comparison.Equal)
        return true;
      else if (compare(this._two, item) == Comparison.Equal)
        return true;
      else if (compare(this._three, item) == Comparison.Equal)
        return true;
      else if (compare(this._four, item) == Comparison.Equal)
        return true;
      else if (compare(this._five, item) == Comparison.Equal)
        return true;
      else if (compare(this._six, item) == Comparison.Equal)
        return true;
      else
        return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains<Key>(Key key, Compare<dynamic, Key> compare)
    {
      if (compare(this._one, key) == Comparison.Equal)
        return true;
      else if (compare(this._two, key) == Comparison.Equal)
        return true;
      else if (compare(this._three, key) == Comparison.Equal)
        return true;
      else if (compare(this._four, key) == Comparison.Equal)
        return true;
      else if (compare(this._five, key) == Comparison.Equal)
        return true;
      else if (compare(this._six, key) == Comparison.Equal)
        return true;
      else
        return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(Foreach<dynamic> function)
    {
      function(this._one);
      function(this._two);
      function(this._three);
      function(this._four);
      function(this._five);
      function(this._six);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(ForeachRef<dynamic> function)
    {
      function(ref this._one);
      function(ref this._two);
      function(ref this._three);
      function(ref this._four);
      function(ref this._five);
      function(ref this._six);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachBreak<dynamic> function)
    {
      if (function(this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._three) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._four) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._five) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(this._six);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachRefBreak<dynamic> function)
    {
      if (function(ref this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._three) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._four) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._five) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(ref this._six);
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public  Structure<object> Clone()
    {
      return new Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix>
        (this._one, this._two, this._three, this._four, this._five, this._six);
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public  dynamic[] ToArray()
    {
      return new dynamic[]
      {
        this._one,
        this._two,
        this._three,
        this._four,
        this._five,
        this._six
      };
    }
  }

  /// <summary>Represents a link between objects.</summary>
  /// <typeparam name="TypeOne">The type of the first item to be linked.</typeparam>
  /// <typeparam name="TypeTwo">The type of the second item to be linked.</typeparam>
  /// <typeparam name="TypeThree">The type of the third item to be linked.</typeparam>
  /// <typeparam name="TypeFour">The type of the fourth item to be linked.</typeparam>
  /// <typeparam name="TypeFive">The type of the fifth item to be linked.</typeparam>
  /// <typeparam name="TypeSix">The type of the sixth item to be linked.</typeparam>
  [Serializable]
  public class Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix, TypeSeven>
    : Link
  {
    protected dynamic _one;
    protected dynamic _two;
    protected dynamic _three;
    protected dynamic _four;
    protected dynamic _five;
    protected dynamic _six;
    protected dynamic _seven;

    /// <summary>Gets the current memory imprint of this structure in bytes.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public  long SizeOf { get { return 7; } }

    /// <summary>The number of objects in the tuple.</summary>
    public  int Size { get { return 7; } }

    /// <summary>The sixth item in the link.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public TypeSeven Seven { get { return this._seven; } set { this._seven = value; } }

    #region .NET Framework Compatibility

    /// <summary>explicitly casts a System.Tuple to a Seven.Structures.Link.</summary>
    /// <param name="tuple">The System.Tuple to be casted.</param>
    /// <returns>A Seven.Structures.Link casted from the System.Tuple.</returns>
    public static explicit operator
      Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix, TypeSeven>
      (Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix, TypeSeven> tuple)
    {
      return new Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix, TypeSeven>
        (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
    }

    /// <summary>explicitly casts a Seven.Structures.Link to a System.Tuple.</summary>
    /// <param name="tuple">The Seven.Structures.Link to be casted.</param>
    /// <returns>The System.Tuple casted Seven.Structures.Link.</returns>
    public static explicit operator
      Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix, TypeSeven>
      (Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix, TypeSeven> link)
    {
      return new Tuple<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix, TypeSeven>
        (link._one, link._two, link._three, link._four, link._five, link._six, link._seven);
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
      yield return this._four;
      yield return this._five;
      yield return this._six;
      yield return this._seven;
    }

    /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
      yield return this._one;
      yield return this._two;
      yield return this._three;
      yield return this._four;
      yield return this._five;
      yield return this._six;
      yield return this._seven;
    }

    #endregion

    /// <summary>Creates a link between objects.</summary>
    /// <param name="one">The first item to be linked.</param>
    /// <param name="two">The second item to be linked.</param>
    /// <param name="three">The third item to be linked.</param>
    /// <param name="four">The fourth item to be linked.</param>
    /// <param name="five">The fourth item to be linked.</param>
    /// <param name="six">The fourth item to be linked.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public Link(TypeOne one, TypeTwo two, TypeThree three, TypeFour four, TypeFive five, TypeSix six, TypeSeven seven)
    {
      this._one = one;
      this._two = two;
      this._three = three;
      this._four = four;
      this._five = five;
      this._six = six;
      this._seven = seven;
    }

    /// <summary>Gets an array with all the types contained in this link in respective order.</summary>
    /// <returns>An array of all the types in this link in respective order.</returns>
    public  Type[] Types()
    {
      return new Type[]
      {
        typeof(TypeOne),
        typeof(TypeTwo),
        typeof(TypeThree),
        typeof(TypeFour),
        typeof(TypeFive),
        typeof(TypeSix),
        typeof(TypeSeven)
      };
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains(dynamic item, Compare<dynamic> compare)
    {
      if (compare(this._one, item) == Comparison.Equal)
        return true;
      if (compare(this._two, item) == Comparison.Equal)
        return true;
      if (compare(this._three, item) == Comparison.Equal)
        return true;
      if (compare(this._four, item) == Comparison.Equal)
        return true;
      if (compare(this._five, item) == Comparison.Equal)
        return true;
      if (compare(this._six, item) == Comparison.Equal)
        return true;
      if (compare(this._seven, item) == Comparison.Equal)
        return true;
      return false;
    }

    /// <summary>Checks to see if a given object is in this data structure.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    public  bool Contains<Key>(Key key, Compare<dynamic, Key> compare)
    {
      if (compare(this._one, key) == Comparison.Equal)
        return true;
      else if (compare(this._two, key) == Comparison.Equal)
        return true;
      else if (compare(this._three, key) == Comparison.Equal)
        return true;
      else if (compare(this._four, key) == Comparison.Equal)
        return true;
      else if (compare(this._five, key) == Comparison.Equal)
        return true;
      else if (compare(this._six, key) == Comparison.Equal)
        return true;
      else if (compare(this._seven, key) == Comparison.Equal)
        return true;
      else
        return false;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(Foreach<dynamic> function)
    {
      function(this._one);
      function(this._two);
      function(this._three);
      function(this._four);
      function(this._five);
      function(this._six);
      function(this._seven);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public  void Foreach(ForeachRef<dynamic> function)
    {
      function(ref this._one);
      function(ref this._two);
      function(ref this._three);
      function(ref this._four);
      function(ref this._five);
      function(ref this._six);
      function(ref this._seven);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachBreak<dynamic> function)
    {
      if (function(this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._three) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._four) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._five) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(this._six) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(this._seven);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public  ForeachStatus Foreach(ForeachRefBreak<dynamic> function)
    {
      if (function(ref this._one) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._two) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._three) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._four) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._five) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else if (function(ref this._six) == ForeachStatus.Break)
        return ForeachStatus.Break;
      else
        return function(ref this._seven);
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public  Structure<object> Clone()
    {
      return new Link<TypeOne, TypeTwo, TypeThree, TypeFour, TypeFive, TypeSix, TypeSeven>
        (this._one, this._two, this._three, this._four, this._five, this._six, this._seven);
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public  dynamic[] ToArray()
    {
      return new dynamic[]
      {
        this._one,
        this._two,
        this._three,
        this._four,
        this._five,
        this._six,
        this._seven
      };
    }
  }
}