using System.Collections;
using System.Collections.Generic;

namespace Seven.Structures
{
  /// <summary>Polymorphism base for data structures in the Seven framework.</summary>
  /// <typeparam name="Type">The type of the instances to store in this data structure.</typeparam>
  public interface Structure<Type> : IEnumerable<Type>
  {
    #region .Net Framework Compatibility

    /// <summary>FOR COMPATIBILITY ONLY. DO NO USE. AVOID IF POSSIBLE.</summary>
    //IEnumerator IEnumerable.GetEnumerator();

    /// <summary>FOR COMPATIBILITY ONLY. DO NO USE. AVOID IF POSSIBLE.</summary>
    //IEnumerator<Type> IEnumerable<Type>.GetEnumerator();

    #endregion

    /// <summary>Gets the current memory imprint of this structure.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    long SizeOf { get; }
    
    ///// <summary>Checks to see if a given object is in this data structure.</summary>
    ///// <param name="item">The item to check for.</param>
    ///// <param name="compare">Delegate representing comparison technique.</param>
    ///// <returns>true if the item is in this structure; false if not.</returns>
    //bool Contains(Type item, Compare<Type> compare);

    ///// <summary>Checks to see if a given object is in this data structure.</summary>
    ///// <typeparam name="Key">The type of the key to check for.</typeparam>
    ///// <param name="key">The key to check for.</param>
    ///// <param name="compare">Delegate representing comparison technique.</param>
    ///// <returns>true if the item is in this structure; false if not.</returns>
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

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    Structure<Type> Clone();

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    Type[] ToArray();
  }
}
