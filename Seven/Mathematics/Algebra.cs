// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using Seven.Structures;

namespace Seven.Mathematics
{
  /// <summary>Supplies algebra mathematics for generic types.</summary>
  /// <typeparam name="T">The type this algebra library can perform on.</typeparam>
  public interface Algebra<T>
  {
    /// <summary>Computes the natural log of the operand.</summary>
    T ln(T value);
    /// <summary>Computes the log of an operand using the base of the other operand.</summary>
    T log(T value, T _base);
		/// <summary>Computes the square root of a given value.</summary>
		T sqrt(T value);
		/// <summary>Takes one operand to the root of the other.</summary>
		T root(T _base, T root);
    /// <summary>Computes the exponential of the eperand.</summary>
    T exp(T value);
    /// <summary>Computes the prime factors of a given value.</summary>
    T[] factorPrimes(T value);
		/// <summary>Computes the reciprocal of the operand.</summary>
		T invert_mult(T value);
		/// <summary>Computes the additive inverse of the operand.</summary>
		T invert_add(T value);
  }

  /// <summary>Provides extensions for the Algebra interface.</summary>
  public static class Algebra
  {
		public delegate T _ln<T>(T value);
		public delegate T _log<T>(T value, T _base);
		public delegate T _sqrt<T>(T value);
		public delegate T _root<T>(T _base, T root);
		public delegate T _exp<T>(T value);
		public delegate T[] _factorPrimes<T>(T value);
		public delegate T _invert_mult<T>(T value);
		public delegate T _invert_add<T>(T value);

		private static Map<object, System.Type> _algebras =
			new Map_Linked<object, System.Type>(
				(System.Type left, System.Type right) => { return left == right; },
				(System.Type type) => { return type.GetHashCode(); })
				{
					{ typeof(double), Algebra_double.Get },
          { typeof(float), Algebra_float.Get },
				};

    public static void Add<T>(Algebra<T> _algebra)
    {
      if (_algebras.Contains(typeof(T)))
        throw new Error("algebra already exists for " + typeof(T));
      else
        _algebras.Add(typeof(T), _algebra);
    }

		public static Algebra<T> Get<T>()
		{
			try { return (Algebra<T>)_algebras[typeof(T)]; }
			catch { throw new Error("Could not load algebra for " + typeof(T)); }
		}

    #region Libraries

    private class Algebra_double : Algebra<double>
    {
      private Algebra_double() { _instance = this; }
      private static Algebra_double _instance;
      private static Algebra_double Instance
      {
        get
        {
          if (_instance == null)
            return _instance = new Algebra_double();
          else
            return _instance;
        }
      }

      public static Algebra_double Get { get { return Instance; } }

      public double ln(double value) { throw new System.NotImplementedException(); }
      public double log(double value, double _base) { return System.Math.Log(value, _base); }
      public double sqrt(double value) { return System.Math.Sqrt(value); }
      public double root(double _base, double root) { throw new System.NotImplementedException(); }
      public double exp(double value) { throw new System.NotImplementedException(); }
      public double[] factorPrimes(double value) { throw new System.NotImplementedException(); }
      public double invert_mult(double value) { return 1.0d / value; }
      public double invert_add(double value) { return -value; }
    }

    private class Algebra_float : Algebra<float>
    {
      private Algebra_float() { _instance = this; }
      private static Algebra_float _instance;
      private static Algebra_float Instance
      {
        get
        {
          if (_instance == null)
            return _instance = new Algebra_float();
          else
            return _instance;
        }
      }

      public static Algebra_float Get { get { return Instance; } }

      public float ln(float value) { throw new System.NotImplementedException(); }
      public float log(float value, float _base) { return (float)System.Math.Log(value, _base); }
      public float sqrt(float value) { return (float)System.Math.Sqrt(value); }
      public float root(float _base, float root) { throw new System.NotImplementedException(); }
      public float exp(float value) { throw new System.NotImplementedException(); }
      public float[] factorPrimes(float value) { throw new System.NotImplementedException(); }
      public float invert_mult(float value) { return 1.0f / value; }
      public float invert_add(float value) { return -value; }
    }

    private class Algebra_int //: Algebra<int>
    {
      private Algebra_int() { _instance = this; }
      private static Algebra_int _instance;
      private static Algebra_int Instance
      {
        get
        {
          if (_instance == null)
            return _instance = new Algebra_int();
          else
            return _instance;
        }
      }
    }

    #endregion

    #region Implementations

    #region decimal

    public static bool IsPrime(decimal candidate)
    {
      if (candidate % 1 == 0)
      {
        if (candidate == 2) return true;
        decimal squareRoot = Algebra.SquareRoot(candidate);
        for (int divisor = 3; divisor <= squareRoot; divisor += 2)
          if ((candidate % divisor) == 0)
            return false;
        return true;
      }
      else
        return false;
    }

    public static AvlTree<decimal> PrimeFactors(decimal a)
    {
      AvlTree<decimal> factors =
        new AvlTree_Linked<decimal>(Logic.Compare);
      if (IsPrime(a))
        factors.Add(a);
      else
        for (decimal b = 2; a > 1; b++)
          while (a % b == 0)
          {
            a /= b;
            factors.Add(b);
          }
      return factors;
    }

    public static decimal SquareRoot(decimal number)
    {
      return (decimal)System.Math.Sqrt((double)number);
    }

    public static decimal Power(decimal number, decimal power)
    {
      return (decimal)System.Math.Pow((double)number, (double)power);
    }

    private static decimal GreatestCommonDenominator(decimal first, decimal second)
    {
      if (first % 1 == 0 && second % 1 == 0)
      {
        if (first < 0) first = -first;
        if (second < 0) second = -second;
        decimal temp = first;
        do
        {
          if (first < second)
          {
            temp = first;
            first = second;
            second = temp;
          }
          first = first % second;
        } while (first != 0);
        return second;
      }
      throw new Error("GCD cannot be performed on non-natural numbers");
    }

    static public decimal Lerp(decimal x, decimal x0, decimal x1, decimal y0, decimal y1)
    {
      if ((x1 - x0) == 0M)
        return (y0 + y1) / 2M;
      return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }

    public static decimal Lerp(decimal x, decimal x0, decimal x1)
    {
      if (x < 0M || x > 1M)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      return x0 + x * (x1 - x0);
    }

    #endregion

    #region double

    public static bool IsPrime(double candidate)
    {
      if (candidate % 1 == 0)
      {
        if (candidate == 2) return true;
        double squareRoot = (double)Algebra.SquareRoot(candidate);
        for (int divisor = 3; divisor <= squareRoot; divisor += 2)
          if ((candidate % divisor) == 0)
            return false;
        return true;
      }
      else
        return false;
    }

    public static AvlTree<double> PrimeFactors(double a)
    {
      AvlTree<double> factors =
        new AvlTree_Linked<double>(Logic.Compare);
      if (a % 1 == 0)
      {
        if (IsPrime(a))
          factors.Add(a);
        else
          for (int b = 2; a > 1; b++)
            while (a % b == 0)
            {
              a /= b;
              factors.Add(b);
            }
      }
      return factors;
    }

    public static double SquareRoot(double number)
    {
      return System.Math.Sqrt(number);
    }

    public static double Power(double number, double power)
    {
      return System.Math.Pow(number, power);
      // I have not written my own version of this function yet, just use the System for now...
    }

    private static double GreatestCommonDenominator(double first, double second)
    {
      if (first % 1 != 0 || second % 1 != 0)
        throw new Error("GCD can only be performed on whole numbers");
      if (first < 0) first = -first;
      if (second < 0) second = -second;
      double temp = first;
      do
      {
        if (first < second)
        {
          temp = first;
          first = second;
          second = temp;
        }
        first = first % second;
      } while (first != 0);
      return second;
    }

    static public double LinearInterpolation(double x, double x0, double x1, double y0, double y1)
    {
      if ((x1 - x0) == 0)
        return (y0 + y1) / 2;
      return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }

    public static double Lerp(double left, double right, double blend)
    {
      //if (left > right)
      //  throw new CalcException("invalid lerp values: (left > right).");
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      return left + blend * (right - left);
    }

    #endregion

    #region float

    public static bool IsPrime(float candidate)
    {
      if (candidate % 1 == 0)
      {
        if (candidate == 2) return true;
        float squareRoot = (float)Algebra.SquareRoot(candidate);
        for (int divisor = 3; divisor <= squareRoot; divisor += 2)
          if ((candidate % divisor) == 0)
            return false;
        return true;
      }
      else
        return false;
    }

    public static AvlTree<float> PrimeFactors(float a)
    {
      AvlTree<float> factors =
        new AvlTree_Linked<float>(Logic.Compare);
      if (a % 1 == 0)
      {
        if (IsPrime(a))
          factors.Add(a);
        else
          for (int b = 2; a > 1; b++)
            while (a % b == 0)
            {
              a /= b;
              factors.Add(b);
            }
      }
      return factors;
    }

    public static float SquareRoot(float number)
    {
      return (float)System.Math.Sqrt(number);
    }

    public static float Power(float number, float power)
    {
      return (float)System.Math.Pow(number, power);
      // I have not written my own version of this function yet, just use the System for now...
    }

    private static float GreatestCommonDenominator(float first, float second)
    {
      if (first % 1 != 0 || second % 1 != 0)
        throw new Error("GCD can only be performed on whole numbers");
      if (first < 0) first = -first;
      if (second < 0) second = -second;
      float temp = first;
      do
      {
        if (first < second)
        {
          temp = first;
          first = second;
          second = temp;
        }
        first = first % second;
      } while (first != 0);
      return second;
    }

    static public float LinearInterpolation(float x, float x0, float x1, float y0, float y1)
    {
      if ((x1 - x0) == 0)
        return (y0 + y1) / 2;
      return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }

    public static float Lerp(float left, float right, float blend)
    {
      //if (left > right)
      //  throw new CalcException("invalid lerp values: (left > right).");
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      return left + blend * (right - left);
    }

    #endregion

    #region long

    public static bool IsPrime(long candidate)
    {
      if (candidate % 1 == 0)
      {
        if (candidate == 2) return true;
        long squareRoot = (long)Algebra.SquareRoot(candidate);
        for (int divisor = 3; divisor <= squareRoot; divisor += 2)
          if ((candidate % divisor) == 0)
            return false;
        return true;
      }
      else
        return false;
    }

    public static AvlTree<long> PrimeFactors(long a)
    {
      AvlTree<long> factors =
        new AvlTree_Linked<long>(Logic.Compare);
      if (a % 1 == 0)
      {
        if (IsPrime(a))
          factors.Add(a);
        else
          for (int b = 2; a > 1; b++)
            while (a % b == 0)
            {
              a /= b;
              factors.Add(b);
            }
      }
      return factors;
    }

    public static long SquareRoot(long number)
    {
      return (long)System.Math.Sqrt(number);
    }

    public static long Power(long number, long power)
    {
      return (long)System.Math.Pow(number, power);
      // I have not written my own version of this function yet, just use the System for now...
    }

    private static long GreatestCommonDenominator(long first, long second)
    {
      if (first % 1 != 0 || second % 1 != 0)
        throw new Error("GCD can only be performed on whole numbers");
      if (first < 0) first = -first;
      if (second < 0) second = -second;
      long temp = first;
      do
      {
        if (first < second)
        {
          temp = first;
          first = second;
          second = temp;
        }
        first = first % second;
      } while (first != 0);
      return second;
    }

    static public long LinearInterpolation(long x, long x0, long x1, long y0, long y1)
    {
      if ((x1 - x0) == 0)
        return (y0 + y1) / 2;
      return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }

    public static long Lerp(long left, long right, long blend)
    {
      //if (left > right)
      //  throw new CalcException("invalid lerp values: (left > right).");
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      return left + blend * (right - left);
    }

    #endregion

    #region int

    public static bool IsPrime(int candidate)
    {
      if (candidate == 2) return true;
      int squareRoot = (int)Algebra.SquareRoot(candidate);
      for (int divisor = 3; divisor <= squareRoot; divisor += 2)
        if ((candidate % divisor) == 0)
          return false;
      return true;
    }

    public static Seven.Structures.List<int> PrimeFactors(int a)
    {
      Seven.Structures.List<int> factors = new Seven.Structures.List_Linked<int>();
      if (IsPrime(a))
        factors.Add(a);
      else
        for (int b = 2; a > 1; b++)
          while (a % b == 0)
          {
            a /= b;
            factors.Add(b);
          }
      return factors;
    }

    public static int SquareRoot(int number)
    {
      return (int)System.Math.Sqrt(number);
      // I have not written my own version of this function yet, just use the System for now...
    }

    public static int Power(int number, int power)
    {
      return (int)System.Math.Pow(number, power);
      // I have not written my own version of this function yet, just use the System for now...
    }

    private static int GreatestCommonDenominator(int first, int second)
    {
      if (first < 0) first = -first;
      if (second < 0) second = -second;
      int temp = first;
      do
      {
        if (first < second)
        {
          temp = first;
          first = second;
          second = temp;
        }
        first = first % second;
      } while (first != 0);
      return second;
    }

    static public int LinearInterpolation(int x, int x0, int x1, int y0, int y1)
    {
      if ((x1 - x0) == 0)
        return (y0 + y1) / 2;
      return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }

    public static int Lerp(int left, int right, int blend)
    {
      throw new Error("Lerp using blend requires rational types.");
    }

    #endregion
    
    #endregion

    #region Method Signatures

    internal static System.Reflection.MemberInfo[] _powerMethods =
      new System.Reflection.MemberInfo[]
    {
      ((function_2<decimal>)Algebra.Power).Method,
      ((function_2<double>)Algebra.Power).Method,
      ((function_2<float>)Algebra.Power).Method,
      ((function_2<long>)Algebra.Power).Method,
      ((function_2<int>)Algebra.Power).Method,
    };

    #endregion

    /// <summary>Error type for all algebra computations.</summary>
    public class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}
