// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Structures
{
  public enum Structures
  {
    Array,
    AvlTree,
    BTree,
    Graph,
    Map,
    Heap,
    Link,
    List,
    Octree,
    QuadTree,
    ReadBlackTree,
    Ring,
    Stack
  }

  /// <summary>Polymorphism base for all data structures in the Seven framework.</summary>
  /// <typeparam name="Type">The type of the instances to store in this data structure.</typeparam>
  public interface Structure<Type> : 
    System.Collections.Generic.IEnumerable<Type>
  {
    #region .Net Framework Compatibility

    /// <summary>FOR COMPATIBILITY ONLY. DO NO USE. AVOID IF POSSIBLE.</summary>
    //System.Collections.IEnumerator GetEnumerator();

    /// <summary>FOR COMPATIBILITY ONLY. DO NO USE. AVOID IF POSSIBLE.</summary>
    //System.Collections.Generic.IEnumerator<Type> GetEnumerator();

    #endregion

    /// <summary>The current allocation size of the structure.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    int SizeOf { get; }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    void Foreach(Foreach<Type> function);

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    void Foreach(ForeachRef<Type> function);

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    ForeachStatus Foreach(ForeachBreak<Type> function);

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    ForeachStatus Foreach(ForeachRefBreak<Type> function);

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    Structure<Type> Clone();

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    Type[] ToArray();
  }

  /// <summary>Contains the implementations of the methods in the Structure interface.</summary>
  internal static class Structure
  {
    public enum Selection { Left, Right };
    public delegate Selection Select<T>(T left, T right);
    public delegate Selection Select<L, R>(L left, R right);

    #region Unions

    public static void Union<T>(this Structure<T> left, Structure<T> right, Equate<T> equate, Foreach<T> function, Selection selection)
    {

      left.Foreach(
      (T l) =>
      {
        
      });

      right.Foreach(
      (T r) =>
      {
          
      });
    }

    public static void Union<L, R>(this Structure<L> left, Structure<R> right, Equate<L, R> equate, Foreach<L> function_left, Foreach<R> function_right, Selection selection)
    {
      if (selection == Selection.Left)
      {
        left.Foreach(
        (L l) =>
        {
          right.Foreach(
          (R r) =>
          {
            if (equate(l, r))
              function_left(l);
          });
        });
      }
      else
      {
        left.Foreach(
        (L l) =>
        {
          right.Foreach(
          (R r) =>
          {
            if (equate(l, r))
              function_right(r);
          });
        });
      }
    }

    public static void Union<T>(this Structure<T> left, Structure<T> right, Equate<T> equate, Foreach<T> function, Select<T> select)
    {
      left.Foreach(
        (T l) =>
        {
          right.Foreach(
          (T r) =>
          {
            if (equate(l, r))
              if (select(l, r) == Selection.Left)
                function(l);
              else
                function(r);
          });
        });
    }

    public static void Union<L, R>(this Structure<L> left, Structure<R> right, Equate<L, R> equate, Foreach<L> function_left, Foreach<R> function_right, Select<L, R> select)
    {
      left.Foreach(
        (L l) =>
        {
          right.Foreach(
          (R r) =>
          {
            if (equate(l, r))
              if (select(l, r) == Selection.Left)
                function_left(l);
              else
                function_right(r);
          });
        });
    }

    #endregion

    #region Intersects

    public static void Intersect<T>(this Structure<T> left, Structure<T> right, Equate<T> equate, Foreach<T> function, Selection selection)
    {
      if (selection == Selection.Left)
      {
        left.Foreach(
        (T l) =>
        {
          right.Foreach(
          (T r) =>
          {
            if (equate(l, r))
              function(l);
          });
        });
      }
      else
      {
        left.Foreach(
        (T l) =>
        {
          right.Foreach(
          (T r) =>
          {
            if (equate(l, r))
              function(l);
          });
        });
      }
    }

    public static void Intersect<L, R>(this Structure<L> left, Structure<R> right, Equate<L, R> equate, Foreach<L> function_left, Foreach<R> function_right, Selection selection)
    {
      if (selection == Selection.Left)
      {
        left.Foreach(
        (L l) =>
        {
          right.Foreach(
          (R r) =>
          {
            if (equate(l, r))
              function_left(l);
          });
        });
      }
      else
      {
        left.Foreach(
        (L l) =>
        {
          right.Foreach(
          (R r) =>
          {
            if (equate(l, r))
              function_right(r);
          });
        });
      }
    }

    public static void Intersect<T>(this Structure<T> left, Structure<T> right, Equate<T> equate, Foreach<T> function, Select<T> select)
    {
      left.Foreach(
        (T l) =>
        {
          right.Foreach(
          (T r) =>
          {
            if (equate(l, r))
              if (select(l, r) == Selection.Left)
                function(l);
              else
                function(r);
          });
        });
    }

    public static void Intersect<L, R>(this Structure<L> left, Structure<R> right, Equate<L, R> equate, Foreach<L> function_left, Foreach<R> function_right, Select<L, R> select)
    {
      left.Foreach(
        (L l) =>
        {
          right.Foreach(
          (R r) =>
          {
            if (equate(l, r))
              if (select(l, r) == Selection.Left)
                function_left(l);
              else
                function_right(r);
          });
        });
    }

    #endregion
  }
}
