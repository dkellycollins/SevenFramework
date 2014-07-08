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
    /// <summary>Determines the largest value of the operands.</summary>
    //Logic.Max<T> Max { get; }
    ///// <summary>Determines and returns the larger operand.</summary>
    //Binary<T> Max { get; }
    ///// <summary>Determines the smallest value of the operands.</summary>
    //Multinary<T> Min { get; }
    ///// <summary>Determines and returns the smaller operand.</summary>
    //Binary<T> Min { get; }
    ///// <summary>Determines equality between two operands.</summary>
    //Equate<T> Equate { get; }
    ///// <summary>Determines equality between two operands with a leniency.</summary>
    //Ternary<T> Equate { get; }
    ///// <summary>Determines equality between multiple operands.</summary>
    //Ternary<T> Equate { get; }
    ///// <summary>Compares two operands.</summary>
    //Compare<T> Compare { get; }
  }

  public class Logic
  {
    #region Compare

    public static Comparison Compare(decimal left, decimal right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    public static Comparison Compare(double left, double right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    public static Comparison Compare(float left, float right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    public static Comparison Compare(long left, long right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    public static Comparison Compare(int left, int right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    public static Comparison Compare(short left, short right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    public static Comparison Compare(byte left, byte right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    #endregion

    #region Equate

    public static bool Equate(decimal left, decimal right)
    {
      return left == right;
    }

    public static bool Equate(double left, double right)
    {
      return left == right;
    }

    public static bool Equate(float left, float right)
    {
      return left == right;
    }

    public static bool Equate(long left, long right)
    {
      return left == right;
    }

    public static bool Equate(int left, int right)
    {
      return left == right;
    }

    public static bool Equate(short left, short right)
    {
      return left == right;
    }

    public static bool Equate(byte left, byte right)
    {
      return left == right;
    }

    public static bool Equate(decimal left, decimal right, decimal leniency)
    {
      return Logic.Abs(left - right) < leniency;
    }

    public static bool Equate(double left, double right, double leniency)
    {
      return Logic.Abs(left - right) < leniency;
    }

    public static bool Equate(float left, float right, float leniency)
    {
      return Logic.Abs(left - right) < leniency;
    }

    public static bool Equate(long left, long right, long leniency)
    {
      return Logic.Abs(left - right) < leniency;
    }

    public static bool Equate(int left, int right, int leniency)
    {
      return Logic.Abs(left - right) < leniency;
    }

    public static bool Equate(short left, short right, short leniency)
    {
      return Logic.Abs(left - right) < leniency;
    }

    public static bool Equate(byte left, byte right, byte leniency)
    {
      return Logic.Abs(left - right) < leniency;
    }

    #endregion

    #region Max

    public static string Max(params string[] values)
    {
      string max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i].CompareTo(max) > 0)
          max = values[i];
      return max;
    }

    public static decimal Max(params decimal[] values)
    {
      decimal max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    public static double Max(params double[] values)
    {
      double max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    public static float Max(params float[] values)
    {
      float max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    public static long Max(params long[] values)
    {
      long max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    public static int Max(params int[] values)
    {
      int max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    public static short Max(params short[] values)
    {
      short max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    public static byte Max(params byte[] values)
    {
      byte max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    public static string Max(string first, string second)
    {
      if (second.CompareTo(first) > 0)
        return second;
      return first;
    }

    public static decimal Max(decimal first, decimal second)
    {
      if (second > first)
        return second;
      return first;
    }

    public static double Max(double first, double second)
    {
      if (second > first)
        return second;
      return first;
    }

    public static float Max(float first, float second)
    {
      if (second > first)
        return second;
      return first;
    }

    public static long Max(long first, long second)
    {
      if (second > first)
        return second;
      return first;
    }

    public static int Max(int first, int second)
    {
      if (second > first)
        return second;
      return first;
    }

    public static short Max(short first, short second)
    {
      if (second > first)
        return second;
      return first;
    }

    public static short Max(byte first, byte second)
    {
      if (second > first)
        return second;
      return first;
    }

    #endregion

    #region Min

    public static string Min(params string[] values)
    {
      string min = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i].CompareTo(min) < 0)
          min = values[i];
      return min;
    }

    public static decimal Min(params decimal[] values)
    {
      decimal min = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < min)
          min = values[i];
      return min;
    }

    public static double Min(params double[] values)
    {
      double min = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < min)
          min = values[i];
      return min;
    }

    public static float Min(params float[] values)
    {
      float min = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < min)
          min = values[i];
      return min;
    }

    public static long Min(params long[] values)
    {
      long min = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < min)
          min = values[i];
      return min;
    }

    public static int Min(params int[] values)
    {
      int min = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < min)
          min = values[i];
      return min;
    }

    public static short Min(params short[] values)
    {
      short min = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < min)
          min = values[i];
      return min;
    }

    public static byte Min(params byte[] values)
    {
      byte min = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < min)
          min = values[i];
      return min;
    }

    public static string Min(string first, string second)
    {
      if (second.CompareTo(first) < 0)
        return second;
      return first;
    }

    public static decimal Min(decimal first, decimal second)
    {
      if (second < first)
        return second;
      return first;
    }

    public static double Min(double first, double second)
    {
      if (second < first)
        return second;
      return first;
    }

    public static float Min(float first, float second)
    {
      if (second < first)
        return second;
      return first;
    }

    public static long Min(long first, long second)
    {
      if (second < first)
        return second;
      return first;
    }

    public static int Min(int first, int second)
    {
      if (second < first)
        return second;
      return first;
    }

    public static short Min(short first, short second)
    {
      if (second < first)
        return second;
      return first;
    }

    public static short Min(byte first, byte second)
    {
      if (second < first)
        return second;
      return first;
    }

    #endregion

    #region Abs

    public static decimal Abs(decimal number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    public static double Abs(double number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    public static float Abs(float number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    public static long Abs(long number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    public static int Abs(int number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    public static short Abs(short number)
    {
      if (number < 0)
        return (short)-number;
      return number;
    }

    #endregion

    #region LessThan

    public bool LessThan(decimal left, decimal right)
    {
      return left < right;
    }

    public bool LessThan(double left, double right)
    {
      return left < right;
    }

    public bool LessThan(float left, float right)
    {
      return left < right;
    }

    public bool LessThan(long left, long right)
    {
      return left < right;
    }

    public bool LessThan(int left, int right)
    {
      return left < right;
    }

    public bool LessThan(short left, short right)
    {
      return left < right;
    }

    public bool LessThan(byte left, byte right)
    {
      return left < right;
    }

    #endregion

    #region GreaterThan

    public bool GreaterThan(decimal left, decimal right)
    {
      return left > right;
    }

    public bool GreaterThan(double left, double right)
    {
      return left > right;
    }

    public bool GreaterThan(float left, float right)
    {
      return left > right;
    }

    public bool GreaterThan(long left, long right)
    {
      return left > right;
    }

    public bool GreaterThan(int left, int right)
    {
      return left > right;
    }

    public bool GreaterThan(short left, short right)
    {
      return left > right;
    }

    public bool GreaterThan(byte left, byte right)
    {
      return left > right;
    }

    #endregion

    #region Clamp

    public static decimal Clamp(decimal value, decimal minimum, decimal maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }

    public static double Clamp(double value, double minimum, double maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }

    public static float Clamp(float value, float minimum, float maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }

    public static long Clamp(long value, long minimum, long maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }

    public static int Clamp(int value, int minimum, int maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }

    public static short Clamp(short value, short minimum, short maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }

    public static byte Clamp(byte value, byte minimum, byte maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }

    #endregion
  }

  public class Logic_int
  {

  }


}
