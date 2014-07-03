using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven.Structures
{
  public interface Trie<T> : Structure<T>
  {
  }

  public class Trie_Linked<T> : Trie<T>
  {
    System.Collections.IEnumerator
     System.Collections.IEnumerable.GetEnumerator()
    {
      throw new System.NotImplementedException();
    }

    System.Collections.Generic.IEnumerator<T>
      System.Collections.Generic.IEnumerable<T>.GetEnumerator()
    {
      throw new System.NotImplementedException();
    }

    /// <summary>The current allocation size of the structure.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { throw new System.NotImplementedException(); } }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<T> function)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<T> function)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<T> function)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<T> function)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Creates a shallow clone of this data structure.</summary>
    /// <returns>A shallow clone of this data structure.</returns>
    public Structure<T> Clone()
    {
      throw new System.NotImplementedException();
    }

    /// <summary>Converts the structure into an array.</summary>
    /// <returns>An array containing all the item in the structure.</returns>
    public T[] ToArray()
    {
      throw new System.NotImplementedException();
    }
  }
}
