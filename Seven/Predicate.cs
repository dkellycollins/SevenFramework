// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven
{
  /// <summary>Unary operator for criteria testing.</summary>
  /// <typeparam name="T">The type of item for the predicate.</typeparam>
  /// <param name="item">The item of the predicate.</param>
  /// <returns>True if the item passes the criteria test. False if not.</returns>
  public delegate bool Predicate<T>(T item);
}
