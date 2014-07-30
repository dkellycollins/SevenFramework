// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seven.Structures
{
  public interface Graph<T> : Structure<T>
  {

  }

  public class Graph_Array<T> : Graph<T>
  {
    private class Node
    {
      private T _value;
      private T[] _adjacents;

      public T Value { get { return _value; } set { _value = value; } }
      public T[] Adjacents { get { return _adjacents; } set { _adjacents = value; } }

      public Node(T value)
      {
        _value = value;
      }
    }

    private Equate<T> _equate;
    private Node[] _nodes;
    private int _edgeCount;

    public int NodeCount { get { return this._nodes.Length; } }
    public int EdgeCount { get { return this._edgeCount; } }

    public Graph_Array(Equate<T> equate)
    {
      _equate = equate;
    }

    /// <summary>Adds a node to the graph.</summary>
    /// <param name="node">The node to be added.</param>
    public void Add(T node)
    {
      if (_nodes == null)
        this._nodes = new Node[] { new Node(node) };
      else
      {
        Node[] nodes = new Node[this._nodes.Length + 1];
        Array.Copy(this._nodes, nodes, this._nodes.Length);
        nodes[nodes.Length - 1] = new Node(node);
        this._nodes = nodes;
      }
    }

    /// <summary>Adds an edge to this graph.</summary>
    /// <param name="from">The starting point of the edge.</param>
    /// <param name="to">The ending point of the edge.</param>
    public void Add(T from, T to)
    {
      int index_from = Index(from);
      int index_to = Index(from);

      if (index_from == -1 && index_to == -1)
      {
        Graph_Array<T>.Grow(ref this._nodes, 2);
        index_from = this._nodes.Length;
        index_to = this._nodes.Length + 1;
        this._nodes[index_from] = new Node(from);
        this._nodes[index_to] = new Node(to);
      }
      else if (index_from == -1)
      {
        Graph_Array<T>.Grow(ref this._nodes, 1);
        index_from = this._nodes.Length;
        this._nodes[index_from] = new Node(from);
      }
      else if (index_to == -1)
      {
        Graph_Array<T>.Grow(ref this._nodes, 1);
        index_to = this._nodes.Length;
        this._nodes[index_to] = new Node(to);
      }

      T[] temp = this._nodes[index_from].Adjacents;
      Graph_Array<T>.Grow(ref temp, 1);
      this._nodes[index_from].Adjacents = temp;

      Node node_from = this._nodes[index_from];
      node_from.Adjacents[node_from.Adjacents.Length - 1] = to;
    }

    private static void Grow(ref T[] input, int amount)
    {
      if (input == null)
        input = new T[amount];
      else
      {
        T[] temp = input;
        input = new T[input.Length + amount];
        for (int i = 0; i < input.Length; i++)
          input[i] = temp[i];
      }
    }

    private static void Grow(ref Node[] array, int amount)
    {
      if (array == null)
        array = new Node[amount];
      else
      {
        Node[] temp = array;
        array = new Node[array.Length + amount];
        for (int i = 0; i < array.Length; i++)
          array[i] = temp[i];
      }
    }

    private int Index(T node)
    {
      if (this._nodes == null)
        return -1;

      for (int i = 0; i < this._nodes.Length; i++)
        if (this._equate(this._nodes[i].Value, node))
          return i;

      return -1;
    }

    System.Collections.IEnumerator
      System.Collections.IEnumerable.GetEnumerator()
    {
      for (int i = 0; i < this._nodes.Length; i++)
        yield return this._nodes[i].Value;
    }

    System.Collections.Generic.IEnumerator<T>
      System.Collections.Generic.IEnumerable<T>.GetEnumerator()
    {
      for (int i = 0; i < this._nodes.Length; i++)
        yield return this._nodes[i].Value;
    }

    /// <summary>The current allocation size of the structure.</summary>
    /// <remarks>Returns long.MaxValue on overflow.</remarks>
    public int SizeOf { get { return this._edgeCount + this._nodes.Length; } }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(Foreach<T> function)
    {
      for (int i = 0; i < this._nodes.Length; i++)
        function(this._nodes[i].Value);
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    public void Foreach(ForeachRef<T> function)
    {
      for (int i = 0; i < this._nodes.Length; i++)
      {
        T temp = this._nodes[i].Value;
        function(ref temp);
        this._nodes[i].Value = temp;
      }
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachBreak<T> function)
    {
      for (int i = 0; i < this._nodes.Length; i++)
        if (function(this._nodes[i].Value) == ForeachStatus.Break)
          return ForeachStatus.Break;
      return ForeachStatus.Continue;
    }

    /// <summary>Invokes a delegate for each entry in the data structure.</summary>
    /// <param name="function">The delegate to invoke on each item in the structure.</param>
    /// <returns>The resulting status of the iteration.</returns>
    public ForeachStatus Foreach(ForeachRefBreak<T> function)
    {
      for (int i = 0; i < this._nodes.Length; i++)
      {
        T temp = this._nodes[i].Value;
        ForeachStatus status = function(ref temp);
        this._nodes[i].Value = temp;
        if (status == ForeachStatus.Break)
          return ForeachStatus.Break;
      }
      return ForeachStatus.Continue;
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
