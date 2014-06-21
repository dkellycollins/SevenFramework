using System;

namespace Seven.Structures
{
  public enum Structures
  {
    Array,
    AvlTree,
    BTree,
    Graph,
    HashTable,
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

    /// <summary>Gets the current memory imprint of this structure.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    long SizeOf { get; }

    /// <summary>Gets all the equalities found within a structure.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>A structure containing all the equalities found.</returns>
    //Structure<Type> GetAll<Key>(Key key, Compare<Type, Key> compare);
    
    /// <summary>Searches the entire structure for the given item.</summary>
    /// <param name="item">The item to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    //bool Contains(Type item, Compare<Type> compare);

    /// <summary>Searches the entire structure for the given key.</summary>
    /// <typeparam name="Key">The type of the key to check for.</typeparam>
    /// <param name="key">The key to check for.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>true if the item is in this structure; false if not.</returns>
    //bool Contains<Key>(Key key, Compare<Type, Key> compare);

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

    /// <summary>Set theory union between two sets (structures).</summary>
    /// <param name="other">The other structure to union with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the union operation.</returns>
    //Structure<Type> Union(Structure<Type> other, Compare<Type> compare);

    /// <summary>Set theory intersection between two sets (structures).</summary>
    /// <param name="other">The other structure to intersect with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the intersection operation.</returns>
    //Structure<Type> Intersection(Structure<Type> other, Compare<Type> compare);

    /// <summary>Set theory set difference between two sets (structures).</summary>
    /// <param name="right">The other structure to set diference with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the set difference operation.</returns>
    //Structure<Type> SetDifference(Structure<Type> right, Compare<Type> compare);

    /// <summary>Set theory symmetric difference between two sets (structures).</summary>
    /// <param name="right">The other structure to symmetric diference with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the symmetric difference operation.</returns>
    //Structure<Type> SymmetricDifference(Structure<Type> right, Compare<Type> compare);

    /// <summary>Set theory cartesian product between two sets (structures).</summary>
    /// <param name="other">The other structure to cartesian product with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the cartesian product operation.</returns>
    //Structure<Type> CartesianProduct(Structure<Type> other, Compare<Type> compare);

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
    internal static Structure<Type> Union<Type>(Structure<Type> left, Structure<Type> right, Compare<Type> compare)
    {
      List<Type> union = new List_Linked<Type>();
      foreach (Type leftLoop in left)
        union.Add(leftLoop);
      //foreach (Type rightLoop in right)
      //  if (!left.Contains(rightLoop, compare))
      //    union.Add(rightLoop);
      throw new NotImplementedException();
      //return union;
    }

    //internal static Structure<Type> Union<Type>(this Structure<Type> left, Structure<Type> right, Compare<Type> compare)
    //{
    //  List<Type> union = new List_Linked<Type>();
    //  foreach (Type leftLoop in left)
    //    union.Add(leftLoop);
    //  //foreach (Type rightLoop in right)
    //  //  if (!left.Contains(rightLoop, compare))
    //  //    union.Add(rightLoop);
    //  throw new NotImplementedException();
    //  return union;
    //}

    internal static Structure<Type> Intersection<Type>(Structure<Type> left, Structure<Type> right, Compare<Type> compare)
    {
      //Structure.Union<Type>(null, null, (Type left1, Type right1) => { return Comparison.Less; });

      List<Type> intersection = new List_Linked<Type>();
      foreach (Type leftLoop in left)
        foreach (Type rightLoop in right)
          if (compare(leftLoop, rightLoop) == Comparison.Equal)
            intersection.Add(leftLoop);
      return intersection;
    }



  }
}
