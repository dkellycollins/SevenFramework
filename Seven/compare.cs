namespace Seven
{
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
}
