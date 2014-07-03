// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>Provides logic operations for mathematical computation.</summary>
  /// <typeparam name="T">The type this logic library can perform on.</typeparam>
  public interface Logic<T>
  {
    /// <summary>Determines and returns the larger operand.</summary>
    Binary<T> Max { get; }
    /// <summary>Determines and returns the smaller operand.</summary>
    Binary<T> Min { get; }
    /// <summary>Determines equality between two operands.</summary>
    Equate<T> Equate { get; }
    /// <summary>Compares two operands.</summary>
    Compare<T> Compare { get; }
  }
}
