using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven.Mathematics
{
  /// <summary>Provides arithmetical operations for mathematical computation.</summary>
  /// <typeparam name="T">The type this arithmetic library can perform on.</typeparam>
  public interface Arithmetic<T>
  {
    /// <summary>Negates a value.</summary>
    Unary<T> Negate { get; }
    /// <summary>Returns the value ignoring the sign.</summary>
    Unary<T> Absolute { get; }
    /// <summary>Divides two operands.</summary>
    Binary<T> Divide { get; }
    /// <summary>Multiplies two operands.</summary>
    Binary<T> Multiply { get; }
    /// <summary>Adds two operands.</summary>
    Binary<T> Add { get; }
    /// <summary>Subtracts two operands.</summary>
    Binary<T> Subtract { get; }
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
    public Unary<int> Negate { get { return (int value) => { return -value; }; } }
    /// <summary>Returns the value ignoring the sign.</summary>
    public Unary<int> Absolute { get { return Math.Abs; } }
    /// <summary>Divides two operands.</summary>
    public Binary<int> Divide { get { return (int left, int right) => { return left / right; }; } }
    /// <summary>Multiplies two operands.</summary>
    public Binary<int> Multiply { get { return (int left, int right) => { return left * right; }; } }
    /// <summary>Adds two operands.</summary>
    public Binary<int> Add { get { return (int left, int right) => { return left + right; }; } }
    /// <summary>Subtracts two operands.</summary>
    public Binary<int> Subtract { get { return (int left, int right) => { return left - right; }; } }

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
    public Unary<float> Negate { get { return (float value) => { return -value; }; } }
    /// <summary>Returns the value ignoring the sign.</summary>
    public Unary<float> Absolute { get { return Math.Abs; } }
    /// <summary>Divides two operands.</summary>
    public Binary<float> Divide { get { return (float left, float right) => { return left / right; }; } }
    /// <summary>Multiplies two operands.</summary>
    public Binary<float> Multiply { get { return (float left, float right) => { return left * right; }; } }
    /// <summary>Adds two operands.</summary>
    public Binary<float> Add { get { return (float left, float right) => { return left + right; }; } }
    /// <summary>Subtracts two operands.</summary>
    public Binary<float> Subtract { get { return (float left, float right) => { return left - right; }; } }

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
    public Unary<double> Negate { get { return (double value) => { return -value; }; } }
    /// <summary>Returns the value ignoring the sign.</summary>
    public Unary<double> Absolute { get { return Math.Abs; } }
    /// <summary>Divides two operands.</summary>
    public Binary<double> Divide { get { return (double left, double right) => { return left / right; }; } }
    /// <summary>Multiplies two operands.</summary>
    public Binary<double> Multiply { get { return (double left, double right) => { return left * right; }; } }
    /// <summary>Adds two operands.</summary>
    public Binary<double> Add { get { return (double left, double right) => { return left + right; }; } }
    /// <summary>Subtracts two operands.</summary>
    public Binary<double> Subtract { get { return (double left, double right) => { return left - right; }; } }

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
    public Unary<decimal> Negate { get { return (decimal value) => { return -value; }; } }
    /// <summary>Returns the value ignoring the sign.</summary>
    public Unary<decimal> Absolute { get { return Math.Abs; } }
    /// <summary>Divides two operands.</summary>
    public Binary<decimal> Divide { get { return (decimal left, decimal right) => { return left / right; }; } }
    /// <summary>Multiplies two operands.</summary>
    public Binary<decimal> Multiply { get { return (decimal left, decimal right) => { return left * right; }; } }
    /// <summary>Adds two operands.</summary>
    public Binary<decimal> Add { get { return (decimal left, decimal right) => { return left + right; }; } }
    /// <summary>Subtracts two operands.</summary>
    public Binary<decimal> Subtract { get { return (decimal left, decimal right) => { return left - right; }; } }

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
    public Unary<long> Negate { get { return (long value) => { return -value; }; } }
    /// <summary>Returns the value ignoring the sign.</summary>
    public Unary<long> Absolute { get { return Math.Abs; } }
    /// <summary>Divides two operands.</summary>
    public Binary<long> Divide { get { return (long left, long right) => { return left / right; }; } }
    /// <summary>Multiplies two operands.</summary>
    public Binary<long> Multiply { get { return (long left, long right) => { return left * right; }; } }
    /// <summary>Adds two operands.</summary>
    public Binary<long> Add { get { return (long left, long right) => { return left + right; }; } }
    /// <summary>Subtracts two operands.</summary>
    public Binary<long> Subtract { get { return (long left, long right) => { return left - right; }; } }

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
    public Unary<short> Negate { get { return (short value) => { return (short)-value; }; } }
    /// <summary>Returns the value ignoring the sign.</summary>
    public Unary<short> Absolute { get { return Math.Abs; } }
    /// <summary>Divides two operands.</summary>
    public Binary<short> Divide { get { return (short left, short right) => { return (short)(left / right); }; } }
    /// <summary>Multiplies two operands.</summary>
    public Binary<short> Multiply { get { return (short left, short right) => { return (short)(left * right); }; } }
    /// <summary>Adds two operands.</summary>
    public Binary<short> Add { get { return (short left, short right) => { return (short)(left + right); }; } }
    /// <summary>Subtracts two operands.</summary>
    public Binary<short> Subtract { get { return (short left, short right) => { return (short)(left - right); }; } }

    /// <summary>Error type for all arithmetic computations.</summary>
    public class Error : Arithmetic.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}
