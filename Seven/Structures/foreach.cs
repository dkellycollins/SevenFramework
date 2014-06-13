namespace Seven.Structures
{
  /// <summary>Status of data structure iteration.</summary>
  public enum ForeachStatus
  {
    /// <summary>Iteration has/was not broken.</summary>
    Continue = 0,
    /// <summary>Iteration has/was broken.</summary>
    Break = 1
  };

  /// <summary>Delegate for data structure iteration.</summary>
  /// <typeparam name="Type">The type of the instances within the data structure.</typeparam>
  /// <param name="current">The current instance of iteration through the data structure.</param>
  public delegate void Foreach<Type>(Type current);

  /// <summary>Delegate for data structure iteration.</summary>
  /// <typeparam name="Type">The type of the instances within the data structure.</typeparam>
  /// <param name="current">The current instance of iteration through the data structure.</param>
  public delegate void ForeachRef<Type>(ref Type current);

  /// <summary>Delegate for data structure iteration.</summary>
  /// <typeparam name="Type">The type of the instances within the data structure.</typeparam>
  /// <param name="current">The current instance of iteration through the data structure.</param>
  /// <returns>The status of the iteration. Allows breaking functionality.</returns>
  public delegate ForeachStatus ForeachBreak<Type>(Type current);

  /// <summary>Delegate for data structure iteration.</summary>
  /// <typeparam name="Type">The type of the instances within the data structure.</typeparam>
  /// <param name="current">The current instance of iteration through the data structure.</param>
  /// <returns>The status of the iteration. Allows breaking functionality.</returns>
  public delegate ForeachStatus ForeachRefBreak<Type>(ref Type current);
}
