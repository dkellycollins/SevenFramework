// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Structures
{
  /// <summary></summary>
  /// <typeparam name="Type"></typeparam>
  public interface ComparisonSorted<Type>
  {
    /// <summary></summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool Contains(Type item);

    /// <summary></summary>
    Compare<Type> SortingTechnique { get; }
  }
}
