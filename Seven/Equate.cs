// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven
{
  /// <summary>Delegate for equating two instances of the same type.</summary>
  /// <typeparam name="Type">The types of the istances to compare.</typeparam>
  /// <param name="left">The left operand of the comparison.</param>
  /// <param name="right">The right operand of the comparison.</param>
  /// <returns>Whether the equate is valid (true) or not (false).</returns>
  public delegate bool Equate<Type>(Type left, Type right);

  /// <summary>Delegate for equating two instances of different types.</summary>
  /// <typeparam name="Left">The type of the left istance to compare.</typeparam>
  /// <typeparam name="Right">The type of the right instance to compare.</typeparam>
  /// <param name="left">The left operand of the equating.</param>
  /// <param name="right">The right operand of the equating.</param>
  /// <returns>Whether the equate is valid (true) or not (false).</returns>
  public delegate bool Equate<Left, Right>(Left left, Right right);
}
