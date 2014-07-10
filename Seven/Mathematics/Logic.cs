// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>Supplies logic mathematics for generic types.</summary>
  /// <typeparam name="T">The type this logic library can perform on.</typeparam>
  public interface Logic<T>
  {
    /// <summary>Determines the largest value of the operands.</summary>
    T Max(params T[] values);
    ///// <summary>Determines and returns the larger operand.</summary>
    T Max(T left, T right);
    ///// <summary>Determines the smallest value of the operands.</summary>
    T Min(params T[] values);
    ///// <summary>Determines and returns the smaller operand.</summary>
    T Min(T left, T right);
    ///// <summary>Determines equality between two operands.</summary>
    bool Equate(T left, T right);
    ///// <summary>Determines equality between two operands with a leniency.</summary>
    bool Equate(T left, T right, T leniency);
    ///// <summary>Determines equality between multiple operands.</summary>
    bool Equate(params T[] values);
    ///// <summary>Compares two operands.</summary>
    Comparison Compare(T left, T right);
  }

  public static class Logic
  {
    public static Logic<int> _int
    { get { return (Logic<int>)Logic_int.Get; } }
    public static Logic<int> GetLogic(this int integer)
    { return (Logic<int>)Logic_int.Get; }

    public static Logic<double> _double
    { get { return (Logic<double>)Logic_double.Get; } }
    public static Logic<double> GetLogic(this double integer)
    { return (Logic<double>)Logic_double.Get; }

    public static Logic<float> _float
    { get { return (Logic<float>)Logic_float.Get; } }
    public static Logic<float> GetLogic(this float integer)
    { return (Logic<float>)Logic_float.Get; }

    public static Logic<short> _short
    { get { return (Logic<short>)Logic_short.Get; } }
    public static Logic<short> GetLogic(this short integer)
    { return (Logic<short>)Logic_short.Get; }

    public static Logic<long> _long
    { get { return (Logic<long>)Logic_long.Get; } }
    public static Logic<long> GetLogic(this long integer)
    { return (Logic<long>)Logic_long.Get; }

    public static Logic<decimal> _decimal
    { get { return (Logic<decimal>)Logic_decimal.Get; } }
    public static Logic<decimal> GetLogic(this decimal integer)
    { return (Logic<decimal>)Logic_decimal.Get; }

    public static Logic<byte> _byte
    { get { return (Logic<byte>)Logic_byte.Get; } }
    public static Logic<byte> GetLogic(this byte byteeger)
    { return (Logic<byte>)Logic_byte.Get; }

    public class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Logic_decimal : Logic<decimal>
  {
    private Logic_decimal() { _instance = this; }
    private static Logic_decimal _instance;
    private static Logic_decimal Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Logic_decimal();
        else
          return _instance;
      }
    }

    public static Logic_decimal Get { get { return Instance; } }

    /// <summary>Returns a less/greater/equal comparison.</summary>
    public Comparison Compare(decimal left, decimal right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    /// <summary>Returns true if all values are equal.</summary>
    public bool Equate(params decimal[] values)
    {
      for (int i = 0; i < values.Length - 1; i++)
        if (values[i] != values[i + 1])
          return false;
      return true;
    }

    /// <summary>Returns left == right.</summary>
    public bool Equate(decimal left, decimal right)
    {
      return left == right;
    }

    /// <summary>Returns Abs(left - right) < leniency.</summary>
    public bool Equate(decimal left, decimal right, decimal leniency)
    {
      return Abs(left - right) < leniency;
    }

    /// <summary>Returns the maximum value.</summary>
    public decimal Max(params decimal[] values)
    {
      decimal max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the maximum value.</summary>
    public decimal Max(decimal first, decimal second)
    {
      if (second > first)
        return second;
      return first;
    }

    /// <summary>Returns the minimum value.</summary>
    public decimal Min(params decimal[] values)
    {
      decimal max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the minimum value.</summary>
    public decimal Min(decimal first, decimal second)
    {
      if (second < first)
        return second;
      return first;
    }

    /// <summary>Returns the absolute value of the provided value.</summary>
    public decimal Abs(decimal number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    /// <summary>Returns left < right.</summary>
    public bool LessThan(decimal left, decimal right)
    {
      return left < right;
    }

    /// <summary>Returns left > right.</summary>
    public bool GreaterThan(decimal left, decimal right)
    {
      return left > right;
    }

    /// <summary>Clamps a value to be within a given minimum and maximum range.</summary>
    public decimal Clamp(decimal value, decimal minimum, decimal maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }
  }

  public class Logic_double : Logic<double>
  {
    private Logic_double() { _instance = this; }
    private static Logic_double _instance;
    private static Logic_double Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Logic_double();
        else
          return _instance;
      }
    }

    public static Logic_double Get { get { return Instance; } }

    /// <summary>Returns a less/greater/equal comparison.</summary>
    public Comparison Compare(double left, double right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    /// <summary>Returns true if all values are equal.</summary>
    public bool Equate(params double[] values)
    {
      for (int i = 0; i < values.Length - 1; i++)
        if (values[i] != values[i + 1])
          return false;
      return true;
    }

    /// <summary>Returns left == right.</summary>
    public bool Equate(double left, double right)
    {
      return left == right;
    }

    /// <summary>Returns Abs(left - right) < leniency.</summary>
    public bool Equate(double left, double right, double leniency)
    {
      return Abs(left - right) < leniency;
    }

    /// <summary>Returns the maximum value.</summary>
    public double Max(params double[] values)
    {
      double max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the maximum value.</summary>
    public double Max(double first, double second)
    {
      if (second > first)
        return second;
      return first;
    }

    /// <summary>Returns the minimum value.</summary>
    public double Min(params double[] values)
    {
      double max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the minimum value.</summary>
    public double Min(double first, double second)
    {
      if (second < first)
        return second;
      return first;
    }

    /// <summary>Returns the absolute value of the provided value.</summary>
    public double Abs(double number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    /// <summary>Returns left < right.</summary>
    public bool LessThan(double left, double right)
    {
      return left < right;
    }

    /// <summary>Returns left > right.</summary>
    public bool GreaterThan(double left, double right)
    {
      return left > right;
    }

    /// <summary>Clamps a value to be within a given minimum and maximum range.</summary>
    public double Clamp(double value, double minimum, double maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }
  }

  public class Logic_float : Logic<float>
  {
    private Logic_float() { _instance = this; }
    private static Logic_float _instance;
    private static Logic_float Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Logic_float();
        else
          return _instance;
      }
    }

    public static Logic_float Get { get { return Instance; } }

    /// <summary>Returns a less/greater/equal comparison.</summary>
    public Comparison Compare(float left, float right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    /// <summary>Returns true if all values are equal.</summary>
    public bool Equate(params float[] values)
    {
      for (int i = 0; i < values.Length - 1; i++)
        if (values[i] != values[i + 1])
          return false;
      return true;
    }

    /// <summary>Returns left == right.</summary>
    public bool Equate(float left, float right)
    {
      return left == right;
    }

    /// <summary>Returns Abs(left - right) < leniency.</summary>
    public bool Equate(float left, float right, float leniency)
    {
      return Abs(left - right) < leniency;
    }

    /// <summary>Returns the maximum value.</summary>
    public float Max(params float[] values)
    {
      float max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the maximum value.</summary>
    public float Max(float first, float second)
    {
      if (second > first)
        return second;
      return first;
    }

    /// <summary>Returns the minimum value.</summary>
    public float Min(params float[] values)
    {
      float max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the minimum value.</summary>
    public float Min(float first, float second)
    {
      if (second < first)
        return second;
      return first;
    }

    /// <summary>Returns the absolute value of the provided value.</summary>
    public float Abs(float number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    /// <summary>Returns left < right.</summary>
    public bool LessThan(float left, float right)
    {
      return left < right;
    }

    /// <summary>Returns left > right.</summary>
    public bool GreaterThan(float left, float right)
    {
      return left > right;
    }

    /// <summary>Clamps a value to be within a given minimum and maximum range.</summary>
    public float Clamp(float value, float minimum, float maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }
  }

  public class Logic_long : Logic<long>
  {
    private Logic_long() { _instance = this; }
    private static Logic_long _instance;
    private static Logic_long Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Logic_long();
        else
          return _instance;
      }
    }

    public static Logic_long Get { get { return Instance; } }

    /// <summary>Returns a less/greater/equal comparison.</summary>
    public Comparison Compare(long left, long right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    /// <summary>Returns true if all values are equal.</summary>
    public bool Equate(params long[] values)
    {
      for (int i = 0; i < values.Length - 1; i++)
        if (values[i] != values[i + 1])
          return false;
      return true;
    }

    /// <summary>Returns left == right.</summary>
    public bool Equate(long left, long right)
    {
      return left == right;
    }

    /// <summary>Returns Abs(left - right) < leniency.</summary>
    public bool Equate(long left, long right, long leniency)
    {
      return Abs(left - right) < leniency;
    }

    /// <summary>Returns the maximum value.</summary>
    public long Max(params long[] values)
    {
      long max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the maximum value.</summary>
    public long Max(long first, long second)
    {
      if (second > first)
        return second;
      return first;
    }

    /// <summary>Returns the minimum value.</summary>
    public long Min(params long[] values)
    {
      long max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the minimum value.</summary>
    public long Min(long first, long second)
    {
      if (second < first)
        return second;
      return first;
    }

    /// <summary>Returns the absolute value of the provided value.</summary>
    public long Abs(long number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    /// <summary>Returns left < right.</summary>
    public bool LessThan(long left, long right)
    {
      return left < right;
    }

    /// <summary>Returns left > right.</summary>
    public bool GreaterThan(long left, long right)
    {
      return left > right;
    }

    /// <summary>Clamps a value to be within a given minimum and maximum range.</summary>
    public long Clamp(long value, long minimum, long maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }
  }

  public class Logic_int : Logic<int>
  {
    private Logic_int() { _instance = this; }
    private static Logic_int _instance;
    private static Logic_int Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Logic_int();
        else
          return _instance;
      }
    }

    public static Logic_int Get { get { return Instance; } }

    /// <summary>Returns a less/greater/equal comparison.</summary>
    public Comparison Compare(int left, int right)
    {
      int result = left.CompareTo(right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    /// <summary>Returns true if all values are equal.</summary>
    public bool Equate(params int[] values)
    {
      for (int i = 0; i < values.Length - 1; i++)
        if (values[i] != values[i + 1])
          return false;
      return true;
    }

    /// <summary>Returns left == right.</summary>
    public bool Equate(int left, int right)
    {
      return left == right;
    }

    /// <summary>Returns Abs(left - right) < leniency.</summary>
    public bool Equate(int left, int right, int leniency)
    {
      return Abs(left - right) < leniency;
    }

    /// <summary>Returns the maximum value.</summary>
    public int Max(params int[] values)
    {
      int max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the maximum value.</summary>
    public int Max(int first, int second)
    {
      if (second > first)
        return second;
      return first;
    }

    /// <summary>Returns the minimum value.</summary>
    public int Min(params int[] values)
    {
      int max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the minimum value.</summary>
    public int Min(int first, int second)
    {
      if (second < first)
        return second;
      return first;
    }

    /// <summary>Returns the absolute value of the provided value.</summary>
    public int Abs(int number)
    {
      if (number < 0)
        return -number;
      return number;
    }

    /// <summary>Returns left < right.</summary>
    public bool LessThan(int left, int right)
    {
      return left < right;
    }

    /// <summary>Returns left > right.</summary>
    public bool GreaterThan(int left, int right)
    {
      return left > right;
    }

    /// <summary>Clamps a value to be within a given minimum and maximum range.</summary>
    public int Clamp(int value, int minimum, int maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }
  }

  public class Logic_short : Logic<short>
  {
    private Logic_short() { _instance = this; }
    private static Logic_short _instance;
    private static Logic_short Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Logic_short();
        else
          return _instance;
      }
    }

    public static Logic_short Get { get { return Instance; } }

    /// <summary>Returns a less/greater/equal comparison.</summary>
    public Comparison Compare(short left, short right)
    {
      int result = ((int)left).CompareTo((int)right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    /// <summary>Returns true if all values are equal.</summary>
    public bool Equate(params short[] values)
    {
      for (int i = 0; i < values.Length - 1; i++)
        if (values[i] != values[i + 1])
          return false;
      return true;
    }

    /// <summary>Returns left == right.</summary>
    public bool Equate(short left, short right)
    {
      return left == right;
    }

    /// <summary>Returns Abs(left - right) < leniency.</summary>
    public bool Equate(short left, short right, short leniency)
    {
      return Abs((short)(left - right)) < leniency;
    }

    /// <summary>Returns the maximum value.</summary>
    public short Max(params short[] values)
    {
      short max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the maximum value.</summary>
    public short Max(short first, short second)
    {
      if (second > first)
        return second;
      return first;
    }

    /// <summary>Returns the minimum value.</summary>
    public short Min(params short[] values)
    {
      short max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the minimum value.</summary>
    public short Min(short first, short second)
    {
      if (second < first)
        return second;
      return first;
    }

    /// <summary>Returns the absolute value of the provided value.</summary>
    public short Abs(short number)
    {
      if (number < 0)
        return (short)-number;
      return number;
    }

    /// <summary>Returns left < right.</summary>
    public bool LessThan(short left, short right)
    {
      return left < right;
    }

    /// <summary>Returns left > right.</summary>
    public bool GreaterThan(short left, short right)
    {
      return left > right;
    }

    /// <summary>Clamps a value to be within a given minimum and maximum range.</summary>
    public short Clamp(short value, short minimum, short maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }
  }

  public class Logic_byte : Logic<byte>
  {
    private Logic_byte() { _instance = this; }
    private static Logic_byte _instance;
    private static Logic_byte Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Logic_byte();
        else
          return _instance;
      }
    }

    public static Logic_byte Get { get { return Instance; } }

    /// <summary>Returns a less/greater/equal comparison.</summary>
    public Comparison Compare(byte left, byte right)
    {
      int result = ((int)left).CompareTo((int)right);
      if (result > 0)
        return Comparison.Greater;
      else if (result < 0)
        return Comparison.Less;
      else
        return Comparison.Equal;
    }

    /// <summary>Returns true if all values are equal.</summary>
    public bool Equate(params byte[] values)
    {
      for (int i = 0; i < values.Length - 1; i++)
        if (values[i] != values[i + 1])
          return false;
      return true;
    }

    /// <summary>Returns left == right.</summary>
    public bool Equate(byte left, byte right)
    {
      return left == right;
    }

    /// <summary>Returns Abs(left - right) < leniency.</summary>
    public bool Equate(byte left, byte right, byte leniency)
    {
      return Abs((byte)(left - right)) < leniency;
    }

    /// <summary>Returns the maximum value.</summary>
    public byte Max(params byte[] values)
    {
      byte max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] > max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the maximum value.</summary>
    public byte Max(byte first, byte second)
    {
      if (second > first)
        return second;
      return first;
    }

    /// <summary>Returns the minimum value.</summary>
    public byte Min(params byte[] values)
    {
      byte max = values[0];
      for (int i = 0; i < values.Length; i++)
        if (values[i] < max)
          max = values[i];
      return max;
    }

    /// <summary>Returns the minimum value.</summary>
    public byte Min(byte first, byte second)
    {
      if (second < first)
        return second;
      return first;
    }

    /// <summary>Returns the absolute value of the provided value.</summary>
    public byte Abs(byte number)
    {
      if (number < 0)
        return (byte)-number;
      return number;
    }

    /// <summary>Returns left < right.</summary>
    public bool LessThan(byte left, byte right)
    {
      return left < right;
    }

    /// <summary>Returns left > right.</summary>
    public bool GreaterThan(byte left, byte right)
    {
      return left > right;
    }

    /// <summary>Clamps a value to be within a given minimum and maximum range.</summary>
    public byte Clamp(byte value, byte minimum, byte maximum)
    {
      if (value < minimum)
        return minimum;
      if (value > maximum)
        return maximum;
      return value;
    }
  }
}
