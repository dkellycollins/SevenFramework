// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>Supplies combinatoric mathematics for generic types.</summary>
  /// <typeparam name="T">The type this combinatoric library can perform on.</typeparam>
  public interface Combinatorics<T>
  {
    T Factorial(T integer);
    T Combinations(T set, params T[] subsets);
    T Choose(T top, T bottom);
  }

  /// <summary>Extendions for the Calculus interface.</summary>
  public static class Combinatorics
  {
    public static Seven.Structures.Map<object, System.Type> _combinatorics =
      new Seven.Structures.Map_Linked<object, System.Type>(
        (System.Type left, System.Type right) => { return left == right; },
        (System.Type type) => { return type.GetHashCode(); })
				{
					{ typeof(int), Combinatorics_int.Get },
				};

    public static Combinatorics<T> Get<T>()
    {
      try { return (Combinatorics<T>)_combinatorics[typeof(T)]; }
      catch { throw new Error("LinearAlgebra does not yet exist for " + typeof(T).ToString()); }
    }

    #region Implementations

    #region int

    /// <summary>calculates the factorial of a given number.</summary>
    /// <param name="integer">The number of the factorial to calculate.</param>
    /// <returns>The calculated factorial value.</returns>
    public static int Factorial(int integer)
    {
      try
      {
        checked
        {
          int result = 1;
          for (; integer > 1; integer--)
            result *= integer;
          return result;
        }
      }
      catch { throw new Error("overflow occured in factorial."); }
    }

    /// <summary></summary>
    /// <param name="set"></param>
    /// <param name="subsets"></param>
    /// <returns></returns>
    public static int Combinations(int set, params int[] subsets)
    {
      int result = Combinatorics.Factorial(set);
      int sum = 0;
      for (int i = 0; i < subsets.Length; i++)
      {
        result /= Combinatorics.Factorial(subsets[i]);
        sum += subsets[i];
      }
      if (sum > set)
        throw new Error("invalid combination (set < Count(subsets)).");
      return result;
    }

    /// <summary>Does a combinotorics choose operation.</summary>
    /// <param name="top">The number of items choosing from a set.</param>
    /// <param name="bottom">The set to be chosen from.</param>
    /// <returns>The result of the choose.</returns>
    public static int Choose(int top, int bottom)
    {
      if (!(top <= bottom || top >= 0))
        throw new Error("invalid choose values !(top <= bottom || top >= 0)");
      return Combinatorics.Factorial(top) / (Combinatorics.Factorial(top) * Combinatorics.Factorial(bottom - top));
    }

    #endregion

    #endregion
  }

  public class Combinatorics_int : Combinatorics<int>
  {
    private Combinatorics_int() { _instance = this; }
    private static Combinatorics_int _instance;
    private static Combinatorics_int Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Combinatorics_int();
        else
          return _instance;
      }
    }

    public static Combinatorics_int Get { get { return Instance; } }

    public int Factorial(int integer) { return Combinatorics.Factorial(integer); }
    public int Combinations(int set, params int[] subsets) { return Combinatorics.Combinations(set, subsets); }
    public int Choose(int top, int bottom) { return Combinatorics.Choose(top, bottom); }
  }
}
