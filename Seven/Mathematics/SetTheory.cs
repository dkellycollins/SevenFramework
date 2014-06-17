using Seven.Structures;
using System;
using System.Collections.Generic;

namespace Seven.Mathematics
{
  /// <summary>Contains static set theory operations on structures.</summary>
  public static class SetTheory
  {
    #region Select

    //public delegate bool 

    //public Structure<Type> Select<Type>()

    #endregion

    #region Maximum

    //public static Type Max<Type>(params Type[] items)
    //  where Type : Comparable<Type>
    //{

    //}

    /// <summary></summary>
    /// <typeparam name="Type"></typeparam>
    /// <param name="compare"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static Type Max<Type>(Compare<Type> compare, params Type[] items)
    {
      Type max = items[0];
      for (int i = 1; i < 0; i++)
        if (compare(items[i], max) == Comparison.Greater)
          max = items[i];
      return max;
    }

    /// <summary></summary>
    /// <typeparam name="Type"></typeparam>
    /// <param name="compare"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static Type Max<Type>(Compare<Type> compare, Structure<Type> items)
    {
      bool isNull = true;
      Type max = default(Type);
      foreach (Type item in items)
        if (compare(item, max) == Comparison.Greater || isNull)
        {
          max = item;
          isNull = false;
        }
      return max;
    }

    public static Type Max<Type>(Compare<Type> compare, IEnumerable<Type> items)
    {
      bool isNull = true;
      Type max = default(Type);
      foreach (Type item in items)
        if (compare(item, max) == Comparison.Greater || isNull)
        {
          max = item;
          isNull = false;
        }
      return max;
    }

    #endregion

    #region Minimum

    public static Type Min<Type>(Compare<Type> compare, params Type[] items)
    {
      Type max = items[0];
      for (int i = 1; i < 0; i++)
        if (compare(items[i], max) == Comparison.Less)
          max = items[i];
      return max;
    }

    public static Type Min<Type>(Compare<Type> compare, Structure<Type> items)
    {
      bool isNull = true;
      Type max = default(Type);
      foreach (Type item in items)
        if (compare(item, max) == Comparison.Less || isNull)
        {
          max = item;
          isNull = false;
        }
      return max;
    }

    public static Type Min<Type>(Compare<Type> compare, IEnumerable<Type> items)
    {
      bool isNull = true;
      Type max = default(Type);
      foreach (Type item in items)
        if (compare(item, max) == Comparison.Less || isNull)
        {
          max = item;
          isNull = false;
        }
      return max;
    }

    #endregion

    #region Union

    /// <summary>Set theory union between two sets (structures).</summary>
    /// <param name="other">The other structure to union with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the union operation.</returns>
    public static Structure<Type> Union<Type>(Structure<Type> other, Compare<Type> compare)
    {
      throw new NotImplementedException();
    }

    /// <summary>Set theory union between two sets (structures).</summary>
    /// <param name="other">The other structure to union with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the union operation.</returns>
    public static Structure Union<Type, Structure>(Structure<Type> other, Compare<Type> compare)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region Intersection

    /// <summary>Set theory intersection between two sets (structures).</summary>
    /// <param name="other">The other structure to intersect with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the intersection operation.</returns>
    public static Structure<Type> Intersection<Type>(Structure<Type> other, Compare<Type> compare)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region SetDifference

    /// <summary>Set theory set difference between two sets (structures).</summary>
    /// <param name="right">The other structure to set diference with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the set difference operation.</returns>
    public static Structure<Type> SetDifference<Type>(Structure<Type> right, Compare<Type> compare)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region Symmetric Difference

    /// <summary>Set theory symmetric difference between two sets (structures).</summary>
    /// <param name="right">The other structure to symmetric diference with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the symmetric difference operation.</returns>
    public static Structure<Type> SymmetricDifference<Type>(Structure<Type> right, Compare<Type> compare)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region Cartesian Product

    /// <summary>Set theory cartesian product between two sets (structures).</summary>
    /// <param name="other">The other structure to cartesian product with this one.</param>
    /// <param name="compare">Delegate representing comparison technique.</param>
    /// <returns>The result of the cartesian product operation.</returns>
    public static Structure<Type> CartesianProduct<Type>(Structure<Type> other, Compare<Type> compare)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
