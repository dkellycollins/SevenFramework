// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>Supplies algebra mathematics for generic types.</summary>
  /// <typeparam name="T">The type this algebra library can perform on.</typeparam>
  public interface Algebra<T>
  {
    /// <summary>Computes the natural log of the operand.</summary>
    T ln(T value);
    /// <summary>Computes the log of an operand using the base of the other operand.</summary>
    T log(T value, T _base);
		/// <summary>Takes one operand to the root of the other.</summary>
		T Root(T _base, T root);
    /// <summary>Computes the exponential of the eperand.</summary>
    T exp(T value);
    /// <summary>Computes the prime factors of a given value.</summary>
    T PrimeFactors(T value);
		/// <summary>Computes the reciprocal of the operand.</summary>
		T Invert_Multiplicative(T value);
		/// <summary>Computes the reciprocal of the operand.</summary>
		T Invert_Additive(T value);
  }

  /// <summary>Provides extensions for the Algebra interface.</summary>
  public static class Algebra
  {
		private static Seven.Structures.Map<object, System.Type> _algebras = 
			new Seven.Structures.Map_Linked<object, System.Type>(
				(System.Type left, System.Type right) => { return left == right; }, 
				(System.Type type) => { return type.GetHashCode(); });

		public static Algebra<T> Get<T>()
		{
			try { return (Algebra<T>)_algebras[typeof(T)]; }
			catch { throw new Error("Could not load algebra for " + typeof(T).ToString()); }
		}

    /// <summary>Error type for all algebra computations.</summary>
    public class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Algebra_int //: Algebra<int>
  {
    private Algebra_int() { _instance = this; }
    private static Algebra_int _instance;
    private static Algebra_int Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Algebra_int();
        else
          return _instance;
      }
    }
  }

}
