// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>A collection of all the necessary knowledge to perform algebra on a type.</summary>
  /// <typeparam name="T">The type this algebra library can perform on.</typeparam>
  public interface Algebra<T>
  {
    /// <summary>Computes the natural log of the operand.</summary>
    Unary<T> Reciprocal { get; }
    /// <summary>Roots one operand to the degree of the other.</summary>
    Binary<T> Square { get; }
    /// <summary>Computes the natural log of the operand.</summary>
    Unary<T> ln { get; }
    /// <summary>Computes the log of an operand using the base of the other operand.</summary>
    Binary<T> log { get; }
    /// <summary>Computes the exponential of the eperand.</summary>
    Unary<T> exp { get; }
    /// <summary>Gets the value of e.</summary>
    Unary<T> e { get; }
  }

  /// <summary>Provides extensions for the Algebra interface.</summary>
  public static class Algebra
  {
    /// <summary>Error type for all arithmetic computations.</summary>
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

    /// <summary>Gets Arithmetic for "int" types.</summary>
    public static Algebra_int Get { get { return Instance; } }

    ///// <summary>Computes the natural log of the operand.</summary>
    //Unary<int> Reciprocal { get { return  } }
    ///// <summary>Roots one operand to the degree of the other.</summary>
    //Binary<int> Square { get; }
    ///// <summary>Computes the natural log of the operand.</summary>
    //Unary<int> ln { get; }
    ///// <summary>Computes the log of an operand using the base of the other operand.</summary>
    //Binary<int> log { get; }
    ///// <summary>Computes the exponential of the eperand.</summary>
    //Unary<int> exp { get; }
    ///// <summary>Gets the value of e.</summary>
    //Unary<int> e { get; }
  }

}
