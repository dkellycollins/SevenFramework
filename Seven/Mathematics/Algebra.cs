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
    Algebra._ln<T> ln { get; }
    /// <summary>Computes the log of an operand using the base of the other operand.</summary>
    Algebra._log<T> log { get; }
		/// <summary>Computes the square root of a given value.</summary>
    Algebra._sqrt<T> sqrt { get; }
		/// <summary>Takes one operand to the root of the other.</summary>
    Algebra._root<T> root { get; }
    /// <summary>Computes the exponential of the eperand.</summary>
    Algebra._exp<T> exp { get; }
    /// <summary>Computes the prime factors of a given value.</summary>
    Algebra._factorPrimes<T> factorPrimes { get; }
		/// <summary>Computes the reciprocal of the operand.</summary>
    Algebra._invert_mult<T> invert_mult { get; }
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

    public static void Set<T>(Algebra<T> _algebra)
    {
      _algebras[typeof(T)] = _algebra;
    }

    public static bool Contains<T>()
    {
      return _algebras.Contains(typeof(T));
    }

		public static Algebra<T> Get<T>()
		{
      object temp;
      if (_algebras.TryGet(typeof(T), out temp))
        return temp as Algebra<T>;
      else
        return Algebra_unsupported<T>.Get;
      //return (Algebra<T>)_algebras[typeof(T)];
      //catch { throw new Error("Could not load algebra for " + typeof(T)); }
		}

    #region Libraries

    private class Algebra_decimal : Algebra<decimal>
    {
      private Algebra_decimal() { _instance = this; }
      private static Algebra_decimal _instance;
      private static Algebra_decimal Instance
      {
        get
        {
          if (_instance == null)
            return _instance = new Algebra_decimal();
          else
            return _instance;
        }
      }

      public static Algebra_decimal Get { get { return Instance; } }

      public Algebra._ln<decimal> ln
      { get { return Algebra.ln; } }
      public Algebra._log<decimal> log
      { get { return Algebra.log; } }
      public Algebra._sqrt<decimal> sqrt
      { get { return Algebra.sqrt; } }
      public Algebra._root<decimal> root
      { get { return Algebra.root; } }
      public Algebra._exp<decimal> exp
      { get { return Algebra.exp; } }
      public Algebra._factorPrimes<decimal> factorPrimes
      { get { return Algebra.factorPrimes; } }
      public Algebra._invert_mult<decimal> invert_mult
      { get { return Algebra.invert_mult; } }
      public Algebra._invert_add<decimal> invert_add
      { get { return Algebra.invert_add; } }
    }

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

      public Algebra._ln<double> ln
      { get { return Algebra.ln; } }
      public Algebra._log<double> log
      { get { return Algebra.log; } }
      public Algebra._sqrt<double> sqrt
      { get { return Algebra.sqrt; } }
      public Algebra._root<double> root
      { get { return Algebra.root; } }
      public Algebra._exp<double> exp
      { get { return Algebra.exp; } }
      public Algebra._factorPrimes<double> factorPrimes
      { get { return Algebra.factorPrimes; } }
      public Algebra._invert_mult<double> invert_mult
      { get { return Algebra.invert_mult; } }
      public Algebra._invert_add<double> invert_add
      { get { return Algebra.invert_add; } }
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

      public Algebra._ln<float> ln
      { get { return Algebra.ln; } }
      public Algebra._log<float> log
      { get { return Algebra.log; } }
      public Algebra._sqrt<float> sqrt
      { get { return Algebra.sqrt; } }
      public Algebra._root<float> root
      { get { return Algebra.root; } }
      public Algebra._exp<float> exp
      { get { return Algebra.exp; } }
      public Algebra._factorPrimes<float> factorPrimes
      { get { return Algebra.factorPrimes; } }
      public Algebra._invert_mult<float> invert_mult
      { get { return Algebra.invert_mult; } }
      public Algebra._invert_add<float> invert_add
      { get { return Algebra.invert_add; } }
    }

    private class Algebra_long : Algebra<long>
    {
      private Algebra_long() { _instance = this; }
      private static Algebra_long _instance;
      private static Algebra_long Instance
      {
        get
        {
          if (_instance == null)
            return _instance = new Algebra_long();
          else
            return _instance;
        }
      }

      public static Algebra_long Get { get { return Instance; } }

      public Algebra._ln<long> ln
      { get { return Algebra.ln; } }
      public Algebra._log<long> log
      { get { return Algebra.log; } }
      public Algebra._sqrt<long> sqrt
      { get { return Algebra.sqrt; } }
      public Algebra._root<long> root
      { get { return Algebra.root; } }
      public Algebra._exp<long> exp
      { get { return Algebra.exp; } }
      public Algebra._factorPrimes<long> factorPrimes
      { get { return Algebra.factorPrimes; } }
      public Algebra._invert_mult<long> invert_mult
      { get { return Algebra.invert_mult; } }
      public Algebra._invert_add<long> invert_add
      { get { return Algebra.invert_add; } }
    }

    private class Algebra_int : Algebra<int>
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

      public static Algebra_int Get { get { return Instance; } }

      public Algebra._ln<int> ln
      { get { return Algebra.ln; } }
      public Algebra._log<int> log
      { get { return Algebra.log; } }
      public Algebra._sqrt<int> sqrt
      { get { return Algebra.sqrt; } }
      public Algebra._root<int> root
      { get { return Algebra.root; } }
      public Algebra._exp<int> exp
      { get { return Algebra.exp; } }
      public Algebra._factorPrimes<int> factorPrimes
      { get { return Algebra.factorPrimes; } }
      public Algebra._invert_mult<int> invert_mult
      { get { return Algebra.invert_mult; } }
      public Algebra._invert_add<int> invert_add
      { get { return Algebra.invert_add; } }
    }

    private class Algebra_unsupported<T> : Algebra<T>
    {
      private Algebra_unsupported() { _instance = this; }
      private static Algebra_unsupported<T> _instance;
      private static Algebra_unsupported<T> Instance
      {
        get
        {
          if (_instance == null)
            return _instance = new Algebra_unsupported<T>();
          else
            return _instance;
        }
      }

      public static Algebra_unsupported<T> Get { get { return Instance; } }

      public Algebra._ln<T> ln
      { get { throw new Error("there is no implementation of algebra for " + typeof(T)); } }
      public Algebra._log<T> log
      { get { throw new Error("there is no implementation of algebra for " + typeof(T)); } }
      public Algebra._sqrt<T> sqrt
      { get { throw new Error("there is no implementation of algebra for " + typeof(T)); } }
      public Algebra._root<T> root
      { get { throw new Error("there is no implementation of algebra for " + typeof(T)); } }
      public Algebra._exp<T> exp
      { get { throw new Error("there is no implementation of algebra for " + typeof(T)); } }
      public Algebra._factorPrimes<T> factorPrimes
      { get { throw new Error("there is no implementation of algebra for " + typeof(T)); } }
      public Algebra._invert_mult<T> invert_mult
      { get { throw new Error("there is no implementation of algebra for " + typeof(T)); } }
      public Algebra._invert_add<T> invert_add
      { get { throw new Error("there is no implementation of algebra for " + typeof(T)); } }
    }

    #endregion

    #region Implementations

    #region decimal

    public static decimal ln(decimal value)
    {
      throw new System.NotImplementedException();
    }

    public static decimal log(decimal value, decimal _base)
    {
      return (decimal)System.Math.Log((double)value, (double)_base);
    }

    public static decimal root(decimal _base, decimal root)
    {
      throw new System.NotImplementedException();
    }

    public static decimal exp(decimal value)
    {
      throw new System.NotImplementedException();
    }

    public static decimal[] factorPrimes(decimal value)
    {
      throw new System.NotImplementedException();
    }

    public static decimal invert_mult(decimal value)
    {
      return 1.0M / value;
    }

    public static decimal invert_add(decimal value)
    {
      return -value;
    }

    public static bool IsPrime(decimal candidate)
    {
      if (candidate % 1 == 0)
      {
        if (candidate == 2) return true;
        decimal squareRoot = Algebra.sqrt(candidate);
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

    public static decimal sqrt(decimal number)
    {
      return (decimal)System.Math.Sqrt((double)number);
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

    public static double ln(double value)
    {
      throw new System.NotImplementedException();
    }

    public static double log(double value, double _base)
    {
      return (double)System.Math.Log((double)value, (double)_base);
    }

    public static double root(double _base, double root)
    {
      throw new System.NotImplementedException();
    }

    public static double exp(double value)
    {
      throw new System.NotImplementedException();
    }

    public static double[] factorPrimes(double value)
    {
      throw new System.NotImplementedException();
    }

    public static double invert_mult(double value)
    {
      return 1.0d / value;
    }

    public static double invert_add(double value)
    {
      return -value;
    }

    public static bool IsPrime(double candidate)
    {
      if (candidate % 1 == 0)
      {
        if (candidate == 2) return true;
        double squareRoot = (double)Algebra.sqrt(candidate);
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

    public static double sqrt(double number)
    {
      return System.Math.Sqrt(number);
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

    public static float ln(float value)
    {
      throw new System.NotImplementedException();
    }

    public static float log(float value, float _base)
    {
      return (float)System.Math.Log((float)value, (float)_base);
    }

    public static float root(float _base, float root)
    {
      throw new System.NotImplementedException();
    }

    public static float exp(float value)
    {
      throw new System.NotImplementedException();
    }

    public static float[] factorPrimes(float value)
    {
      throw new System.NotImplementedException();
    }

    public static float invert_mult(float value)
    {
      return 1.0f / value;
    }

    public static float invert_add(float value)
    {
      return -value;
    }

    public static bool IsPrime(float candidate)
    {
      if (candidate % 1 == 0)
      {
        if (candidate == 2) return true;
        float squareRoot = (float)Algebra.sqrt(candidate);
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

    public static float sqrt(float number)
    {
      return (float)System.Math.Sqrt(number);
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

    public static long ln(long value)
    {
      throw new System.NotImplementedException();
    }

    public static long log(long value, long _base)
    {
      return (long)System.Math.Log((long)value, (long)_base);
    }

    public static long root(long _base, long root)
    {
      throw new System.NotImplementedException();
    }

    public static long exp(long value)
    {
      throw new System.NotImplementedException();
    }

    public static long[] factorPrimes(long value)
    {
      throw new System.NotImplementedException();
    }

    public static long invert_mult(long value)
    {
      throw new Error("rational mathematics required");
    }

    public static long invert_add(long value)
    {
      return -value;
    }

    public static bool IsPrime(long candidate)
    {
      if (candidate % 1 == 0)
      {
        if (candidate == 2) return true;
        long squareRoot = (long)Algebra.sqrt(candidate);
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

    public static long sqrt(long number)
    {
      return (long)System.Math.Sqrt(number);
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

    public static int ln(int value)
    {
      throw new System.NotImplementedException();
    }

    public static int log(int value, int _base)
    {
      return (int)System.Math.Log((int)value, (int)_base);
    }

    public static int root(int _base, int root)
    {
      throw new System.NotImplementedException();
    }

    public static int exp(int value)
    {
      throw new System.NotImplementedException();
    }

    public static int[] factorPrimes(int value)
    {
      throw new System.NotImplementedException();
    }

    public static int invert_mult(int value)
    {
      throw new Error("rational mathematics required");
    }

    public static int invert_add(int value)
    {
      return -value;
    }


    public static bool IsPrime(int candidate)
    {
      if (candidate == 2) return true;
      int squareRoot = (int)Algebra.sqrt(candidate);
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

    public static int sqrt(int number)
    {
      return (int)System.Math.Sqrt(number);
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

    // This region is for storing reflection properties

    internal static System.Reflection.MemberInfo[] _lnMethods =
      new System.Reflection.MemberInfo[]
    {
      ((_ln<decimal>)Algebra.ln).Method,
      ((_ln<double>)Algebra.ln).Method,
      ((_ln<float>)Algebra.ln).Method,
      ((_ln<long>)Algebra.ln).Method,
      ((_ln<int>)Algebra.ln).Method,
    };

    internal static System.Reflection.MemberInfo[] _logMethods =
      new System.Reflection.MemberInfo[]
    {
      ((_log<decimal>)Algebra.log).Method,
      ((_log<double>)Algebra.log).Method,
      ((_log<float>)Algebra.log).Method,
      ((_log<long>)Algebra.log).Method,
      ((_log<int>)Algebra.log).Method,
    };

    internal static System.Reflection.MemberInfo[] _sqrtMethods =
      new System.Reflection.MemberInfo[]
    {
      ((_sqrt<decimal>)Algebra.sqrt).Method,
      ((_sqrt<double>)Algebra.sqrt).Method,
      ((_sqrt<float>)Algebra.sqrt).Method,
      ((_sqrt<long>)Algebra.sqrt).Method,
      ((_sqrt<int>)Algebra.sqrt).Method,
    };

    internal static System.Reflection.MemberInfo[] _rootMethods =
      new System.Reflection.MemberInfo[]
    {
      ((_root<decimal>)Algebra.root).Method,
      ((_root<double>)Algebra.root).Method,
      ((_root<float>)Algebra.root).Method,
      ((_root<long>)Algebra.root).Method,
      ((_root<int>)Algebra.root).Method,
    };

    internal static System.Reflection.MemberInfo[] _expMethods =
      new System.Reflection.MemberInfo[]
    {
      ((_exp<decimal>)Algebra.exp).Method,
      ((_exp<double>)Algebra.exp).Method,
      ((_exp<float>)Algebra.exp).Method,
      ((_exp<long>)Algebra.exp).Method,
      ((_exp<int>)Algebra.exp).Method,
    };

    internal static System.Reflection.MemberInfo[] _factorPrimesMethods =
      new System.Reflection.MemberInfo[]
    {
      ((_factorPrimes<decimal>)Algebra.factorPrimes).Method,
      ((_factorPrimes<double>)Algebra.factorPrimes).Method,
      ((_factorPrimes<float>)Algebra.factorPrimes).Method,
      ((_factorPrimes<long>)Algebra.factorPrimes).Method,
      ((_factorPrimes<int>)Algebra.factorPrimes).Method,
    };

    internal static System.Reflection.MemberInfo[] _invertMethods =
      new System.Reflection.MemberInfo[]
    {
      ((_invert_mult<decimal>)Algebra.invert_mult).Method,
      ((_invert_mult<double>)Algebra.invert_mult).Method,
      ((_invert_mult<float>)Algebra.invert_mult).Method,
      ((_invert_mult<long>)Algebra.invert_mult).Method,
      ((_invert_mult<int>)Algebra.invert_mult).Method,
    };

    //internal static System.Reflection.MemberInfo[] _factorPrimesMethods =
    //  new System.Reflection.MemberInfo[]
    //{
    //  ((_invert_mult<decimal>)Algebra.invert_mult).Method,
    //  ((_invert_mult<double>)Algebra.invert_mult).Method,
    //  ((_invert_mult<float>)Algebra.invert_mult).Method,
    //  ((_invert_mult<long>)Algebra.invert_mult).Method,
    //  ((_invert_mult<int>)Algebra.invert_mult).Method,
    //};

    #endregion

    /// <summary>Error type for all algebra computations.</summary>
    public class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }
}
