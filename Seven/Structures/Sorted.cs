namespace Seven.Structures
{
  public interface ComparisonSorted<Type>
  {
    bool Contains(Type item);

    Compare<Type> SortingTechnique { get; }

  }
}
