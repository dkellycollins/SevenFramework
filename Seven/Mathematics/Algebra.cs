// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>Provides algebra operations for mathematical computation.</summary>
  /// <typeparam name="T">The type this algebra library can perform on.</typeparam>
  public interface Algebra<T>
  {
    /// <summary>Computes the natural log of the operand.</summary>
    Unary<T> Reciprocal { get; }
    /// <summary>Takes one operand to the power of the other.</summary>
    Binary<T> Power { get; }
    /// <summary>Roots one operand to the degree of the other.</summary>
    Binary<T> Square { get; }
    /// <summary>Computes the natural log of the operand.</summary>
    Unary<T> ln { get; }
    /// <summary>Computes the log of an operand using the base of the other operand.</summary>
    Binary<T> log { get; }
    /// <summary>Computes the exponential of the eperand.</summary>
    Unary<T> e { get; }
  }
}
