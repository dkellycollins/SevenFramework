// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

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
		T Invert_Multiplicative(T value);
		/// <summary>Computes the reciprocal of the operand.</summary>
		T Invert_Additive(T value);
  }

  /// <summary>Provides extensions for the Algebra interface.</summary>
  public static class Algebra
  {
		public delegate T _ln<T>(T value);
		public delegate T _log<T>(T value, T _base);
		public delegate T _sqrt<T>(T value);
		public delegate T _root<T>(T _base, T root);
		public delegate T _exp<T>(T value);
		public delegate T[] _PrimeFactors<T>(T value);
		public delegate T _Invert_Multiplicative<T>(T value);
		public delegate T _Invert_Additive<T>(T value);

		private static Seven.Structures.Map<object, System.Type> _algebras =
			new Seven.Structures.Map_Linked<object, System.Type>(
				(System.Type left, System.Type right) => { return left == right; },
				(System.Type type) => { return type.GetHashCode(); })
				{
					{ typeof(double), Algebra_double.Get },
          { typeof(float), Algebra_float.Get },
				};

		public static Algebra<T> Get<T>()
		{
			try { return (Algebra<T>)_algebras[typeof(T)]; }
			catch { throw new Error("Could not load algebra for " + typeof(T).ToString()); }
		}

    #region Implementations

    #region int

    public static bool IsPrime(int candidate)
    {
      if (candidate == 2) return true;
      int squareRoot = (int)SquareRoot(candidate);
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

    public static float SquareRoot(float number)
    {
      return (float)System.Math.Sqrt(number);
      // I have not written my own version of this function yet, just use the System for now...
    }

    public static float Power(float number, float power)
    {
      return (float)System.Math.Pow(number, power);
      // I have not written my own version of this function yet, just use the System for now...
    }

    private static long GreatestCommonDenominator(int first, int second)
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

    #endregion

    /// <summary>Error type for all algebra computations.</summary>
    public class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
  }

  public class Algebra_double : Algebra<double>
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
		public double Invert_Multiplicative(double value) { return 1.0d / value; }
		public double Invert_Additive(double value) { return -value; }
  }

  public class Algebra_float : Algebra<float>
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
    public float Invert_Multiplicative(float value) { return 1.0f / value; }
    public float Invert_Additive(float value) { return -value; }
  }

  public class Algebra_int //: Algebra<int>
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
}
