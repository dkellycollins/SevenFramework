// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>Supplies arithmetic mathematics for generic types.</summary>
  /// <typeparam name="T">The type this arithmetic library can perform on.</typeparam>
  public interface Arithmetic<T>
  {
    /// <summary>Negates a value.</summary>
    T Negate(T value);
    /// <summary>Returns the value ignoring the sign.</summary>
    T Absolute(T value);
    /// <summary>Divides two operands.</summary>
    T Divide(T left, T right);
    /// <summary>Multiplies two operands.</summary>
    T Multiply(T left, T right);
    /// <summary>Adds two operands.</summary>
    T Add(T left, T right);
    /// <summary>Subtracts two operands.</summary>
    T Subtract(T left, T right);
    /// <summary>Takes one operand to the power of the other.</summary>
    T Power(T left, T right);
  }

  public static class Arithmetic
  {
    public static Arithmetic<int> _int
    { get { return (Arithmetic<int>)Arithmetic_int.Get; } }
    public static Arithmetic<int> GetArithmetic(this int integer)
    { return (Arithmetic<int>)Arithmetic_int.Get; }

    public static Arithmetic<double> _double
    { get { return (Arithmetic<double>)Arithmetic_double.Get; } }
    public static Arithmetic<double> GetArithmetic(this double integer)
    { return (Arithmetic<double>)Arithmetic_double.Get; }

    public static Arithmetic<float> _float
    { get { return (Arithmetic<float>)Arithmetic_float.Get; } }
    public static Arithmetic<float> GetArithmetic(this float integer)
    { return (Arithmetic<float>)Arithmetic_float.Get; }

    public static Arithmetic<short> _short
    { get { return (Arithmetic<short>)Arithmetic_short.Get; } }
    public static Arithmetic<short> GetArithmetic(this short integer)
    { return (Arithmetic<short>)Arithmetic_short.Get; }

    public static Arithmetic<long> _long
    { get { return (Arithmetic<long>)Arithmetic_long.Get; } }
    public static Arithmetic<long> GetArithmetic(this long integer)
    { return (Arithmetic<long>)Arithmetic_long.Get; }

    public static Arithmetic<decimal> _decimal
    { get { return (Arithmetic<decimal>)Arithmetic_decimal.Get; } }
    public static Arithmetic<decimal> GetArithmetic(this decimal integer)
    { return (Arithmetic<decimal>)Arithmetic_decimal.Get; }

    public static Arithmetic<byte> _byte
    { get { return (Arithmetic<byte>)Arithmetic_byte.Get; } }
    public static Arithmetic<byte> GetArithmetic(this byte byteeger)
    { return (Arithmetic<byte>)Arithmetic_byte.Get; }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Arithmetic_int : Arithmetic<int>
  {
    private Arithmetic_int() { _instance = this; }
    private static Arithmetic_int _instance;
    private static Arithmetic_int Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Arithmetic_int();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "int" types.</summary>
    public static Arithmetic_int Get { get { return Instance; } }

    /// <summary>Negates a value.</summary>
    public int Negate(int value) { return -value; }
    /// <summary>Returns the value ignoring the sign.</summary>
    public int Absolute(int value) { return System.Math.Abs(value); }
    /// <summary>Divides two operands.</summary>
    public int Divide(int left, int right) { return left / right; }
    /// <summary>Multiplies two operands.</summary>
    public int Multiply(int left, int right) { return left * right; }
    /// <summary>Adds two operands.</summary>
    public int Add(int left, int right) { return left + right; }
    /// <summary>Subtracts two operands.</summary>
    public int Subtract(int left, int right) { return left - right; }
    /// <summary>Takes one operand to the power of the other.</summary>
    public int Power(int left, int right) { return (int)System.Math.Pow(left, right); }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Arithmetic.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Arithmetic_float : Arithmetic<float>
  {
    private Arithmetic_float() { _instance = this; }
    private static Arithmetic_float _instance;
    private static Arithmetic_float Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Arithmetic_float();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "float" types.</summary>
    public static Arithmetic_float Get { get { return Instance; } }

    /// <summary>Negates a value.</summary>
    public float Negate(float value) { return -value; }
    /// <summary>Returns the value ignoring the sign.</summary>
    public float Absolute(float value) { return System.Math.Abs(value); }
    /// <summary>Divides two operands.</summary>
    public float Divide(float left, float right) { return left / right; }
    /// <summary>Multiplies two operands.</summary>
    public float Multiply(float left, float right) { return left * right; }
    /// <summary>Adds two operands.</summary>
    public float Add(float left, float right) { return left + right; }
    /// <summary>Subtracts two operands.</summary>
    public float Subtract(float left, float right) { return left - right; }
    /// <summary>Takes one operand to the power of the other.</summary>
    public float Power(float left, float right) { return (float)System.Math.Pow(left, right); }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Arithmetic.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Arithmetic_double : Arithmetic<double>
  {
    private Arithmetic_double() { _instance = this; }
    private static Arithmetic_double _instance;
    private static Arithmetic_double Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Arithmetic_double();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "double" types.</summary>
    public static Arithmetic_double Get { get { return Instance; } }

    /// <summary>Negates a value.</summary>
    public double Negate(double value) { return -value; }
    /// <summary>Returns the value ignoring the sign.</summary>
    public double Absolute(double value) { return System.Math.Abs(value); }
    /// <summary>Divides two operands.</summary>
    public double Divide(double left, double right) { return left / right; }
    /// <summary>Multiplies two operands.</summary>
    public double Multiply(double left, double right) { return left * right; }
    /// <summary>Adds two operands.</summary>
    public double Add(double left, double right) { return left + right; }
    /// <summary>Subtracts two operands.</summary>
    public double Subtract(double left, double right) { return left - right; }
    /// <summary>Takes one operand to the power of the other.</summary>
    public double Power(double left, double right) { return System.Math.Pow(left, right); }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Arithmetic.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Arithmetic_decimal : Arithmetic<decimal>
  {
    private Arithmetic_decimal() { _instance = this; }
    private static Arithmetic_decimal _instance;
    private static Arithmetic_decimal Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Arithmetic_decimal();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "decimal" types.</summary>
    public static Arithmetic_decimal Get { get { return Instance; } }

    /// <summary>Negates a value.</summary>
    public decimal Negate(decimal value) { return -value; }
    /// <summary>Returns the value ignoring the sign.</summary>
    public decimal Absolute(decimal value) { return System.Math.Abs(value); }
    /// <summary>Divides two operands.</summary>
    public decimal Divide(decimal left, decimal right) { return left / right; }
    /// <summary>Multiplies two operands.</summary>
    public decimal Multiply(decimal left, decimal right) { return left * right; }
    /// <summary>Adds two operands.</summary>
    public decimal Add(decimal left, decimal right) { return left + right; }
    /// <summary>Subtracts two operands.</summary>
    public decimal Subtract(decimal left, decimal right) { return left - right; }
    /// <summary>Takes one operand to the power of the other.</summary>
    public decimal Power(decimal left, decimal right) { return (decimal)System.Math.Pow((double)left, (double)right); }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Arithmetic.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Arithmetic_long : Arithmetic<long>
  {
    private Arithmetic_long() { _instance = this; }
    private static Arithmetic_long _instance;
    private static Arithmetic_long Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Arithmetic_long();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "long" types.</summary>
    public static Arithmetic_long Get { get { return Instance; } }

    /// <summary>Negates a value.</summary>
    public long Negate(long value) { return -value; }
    /// <summary>Returns the value ignoring the sign.</summary>
    public long Absolute(long value) { return System.Math.Abs(value); }
    /// <summary>Divides two operands.</summary>
    public long Divide(long left, long right) { return left / right; }
    /// <summary>Multiplies two operands.</summary>
    public long Multiply(long left, long right) { return left * right; }
    /// <summary>Adds two operands.</summary>
    public long Add(long left, long right) { return left + right; }
    /// <summary>Subtracts two operands.</summary>
    public long Subtract(long left, long right) { return left - right; }
    /// <summary>Takes one operand to the power of the other.</summary>
    public long Power(long left, long right) { return (long)System.Math.Pow(left, right); }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Arithmetic.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Arithmetic_short : Arithmetic<short>
  {
    private Arithmetic_short() { _instance = this; }
    private static Arithmetic_short _instance;
    private static Arithmetic_short Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Arithmetic_short();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "short" types.</summary>
    public static Arithmetic_short Get { get { return Instance; } }

    /// <summary>Negates a value.</summary>
    public short Negate(short value) { return (short)-value; }
    /// <summary>Returns the value ignoring the sign.</summary>
    public short Absolute(short value) { return System.Math.Abs(value); }
    /// <summary>Divides two operands.</summary>
    public short Divide(short left, short right) { return (short)(left / right); }
    /// <summary>Multiplies two operands.</summary>
    public short Multiply(short left, short right) { return (short)(left * right); }
    /// <summary>Adds two operands.</summary>
    public short Add(short left, short right) { return (short)(left + right); }
    /// <summary>Subtracts two operands.</summary>
    public short Subtract(short left, short right) { return (short)(left - right); }
    /// <summary>Takes one operand to the power of the other.</summary>
    public short Power(short left, short right) { return (short)System.Math.Pow(left, right); }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Arithmetic.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Arithmetic_byte : Arithmetic<byte>
  {
    private Arithmetic_byte() { _instance = this; }
    private static Arithmetic_byte _instance;
    private static Arithmetic_byte Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Arithmetic_byte();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "byte" types.</summary>
    public static Arithmetic_byte Get { get { return Instance; } }

    /// <summary>Negates a value.</summary>
    public byte Negate(byte b) { throw new Arithmetic.Error("attempting to negate a byte."); }
    /// <summary>Returns the value ignoring the sign.</summary>
    public byte Absolute(byte b) { return b; }
    /// <summary>Divides two operands.</summary>
    public byte Divide(byte left, byte right) { return (byte)(left / right); }
    /// <summary>Multiplies two operands.</summary>
    public byte Multiply(byte left, byte right) { return (byte)(left * right); }
    /// <summary>Adds two operands.</summary>
    public byte Add(byte left, byte right) { return (byte)(left + right); }
    /// <summary>Subtracts two operands.</summary>
    public byte Subtract(byte left, byte right) { return (byte)(left - right); }
    /// <summary>Takes one operand to the power of the other.</summary>
    public byte Power(byte left, byte right) { return (byte)System.Math.Pow(left, right); }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Arithmetic.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}
