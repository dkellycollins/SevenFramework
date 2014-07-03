// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven
{
  /// <summary>Delegate for equating two instances of the same type.</summary>
  /// <typeparam name="T">The types of the istances to compare.</typeparam>
  /// <param name="left">The left operand of the comparison.</param>
  /// <param name="right">The right operand of the comparison.</param>
  /// <returns>Whether the equate is valid (true) or not (false).</returns>
  public delegate bool Equate<T>(T left, T right);

  /// <summary>Delegate for equating two instances of different types.</summary>
  /// <typeparam name="L">The type of the left istance to compare.</typeparam>
  /// <typeparam name="R">The type of the right instance to compare.</typeparam>
  /// <param name="left">The left operand of the equating.</param>
  /// <param name="right">The right operand of the equating.</param>
  /// <returns>Whether the equate is valid (true) or not (false).</returns>
  public delegate bool Equate<L, R>(L left, R right);

  /// <summary>Comparison operator between two operands in a logical expression.</summary>
  public enum Comparison
  {
    /// <summary>The left operand is less than the right operand.</summary>
    Less = -1,
    /// <summary>The left operand is equal to the right operand.</summary>
    Equal = 0,
    /// <summary>The left operand is greater than the right operand.</summary>
    Greater = 1
  };

  /// <summary>Delegate for comparing two instances of the same type.</summary>
  /// <typeparam name="Type">The type of the istances to compare.</typeparam>
  /// <param name="left">The left operand of the comparison.</param>
  /// <param name="right">The right operand of the comparison.</param>
  /// <returns>The Comparison operator between the operands to form a true logic statement.</returns>
  public delegate Comparison Compare<Type>(Type left, Type right);

  /// <summary>Delegate for comparing two instances of different types.</summary>
  /// <typeparam name="Left">The type of the left istance to compare.</typeparam>
  /// <typeparam name="Right">The type of the right instance to compare.</typeparam>
  /// <param name="left">The left operand of the comparison.</param>
  /// <param name="right">The right operand of the comparison.</param>
  /// <returns>The Comparison operator between the operands to form a true logic statement.</returns>
  public delegate Comparison Compare<Left, Right>(Left left, Right right);

  /// <summary>A unary computational operator.</summary>
  /// <typeparam name="T">The type this operator can perform on.</typeparam>
  /// <param name="value">The operand of this operator.</param>
  /// <returns>The result of the operator.</returns>
  public delegate T Unary<T>(T value);

  /// <summary>A binary computational operator.</summary>
  /// <typeparam name="T">The type this operator can perform on.</typeparam>
  /// <param name="left">The left operand of the operation.</param>
  /// <param name="right">The right operand of the operation.</param>
  /// <returns>The result of the operation.</returns>
  public delegate T Binary<T>(T left, T right);
}
