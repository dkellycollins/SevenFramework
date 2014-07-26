// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
	/// <summary>Supplies mathematic constants for generic types.</summary>
	/// <typeparam name="T">The type this constants library can supply for.</typeparam>
	public interface Constants<T>
	{
		T e { get; }
		T pi { get; }

		T factory(int value);
		T factory(float value);
		T factory(double value);
		T factory(decimal value);
	}

	public static class Constants
	{
		public const decimal pi_decimal = 3.1415926535897932384626433832795028841971693993751M;
		public const double pi_double = (double)pi_decimal;
    public const float pi_float = (float)pi_decimal;

		public const decimal e_decimal = 2.7182818284590452353602874713527M;
		public const double e_double = (double)e_decimal;
		public const float e_float = (float)e_decimal;

		public static Seven.Structures.Map<object, System.Type> _constants =
			new Seven.Structures.Map_Linked<object, System.Type>(
				(System.Type left, System.Type right) => { return left == right; },
				(System.Type type) => { return type.GetHashCode(); })
				{
					{ typeof(int), Constants_int.Get },
					{ typeof(double), Constants_double.Get },
					{ typeof(float), Constants_float.Get },
					{ typeof(decimal), Constants_decimal.Get },
					//{ typeof(long), Constants_long.Get },
					//{ typeof(short), Constants_short.Get },
					//{ typeof(byte), Constants_byte.Get }
				};

		public static Constants<T> Get<T>()
		{
			try { return _constants[typeof(T)] as Constants<T>; }
			catch { throw new Error("Algebra does not yet exist for " + typeof(T).ToString()); }
		}

    /// <summary>Error type for all algebra computations.</summary>
    public class Error : Seven.Error
    {
      public Error(string message) : base(message) { }
    }
	}

	public class Constants_decimal : Constants<decimal>
	{
		private Constants_decimal() { _instance = this; }
    private static Constants_decimal _instance;
    private static Constants_decimal Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Constants_decimal();
        else
          return _instance;
      }
    }

    /// <summary>Gets Constants for "decimal" types.</summary>
		public static Constants_decimal Get { get { return Instance; } }

		public decimal e { get { return Constants.e_decimal; } }
		public decimal pi { get { return Constants.pi_decimal; } }

		public decimal factory(int value) { return value; }
		public decimal factory(float value) { return (decimal)value; }
		public decimal factory(double value) { return (decimal)value; }
		public decimal factory(decimal value) { return value; }
	}

	public class Constants_double : Constants<double>
	{
		private Constants_double() { _instance = this; }
		private static Constants_double _instance;
		private static Constants_double Instance
		{
			get
			{
				if (_instance == null)
					return _instance = new Constants_double();
				else
					return _instance;
			}
		}

		/// <summary>Gets Constants for "double" types.</summary>
		public static Constants_double Get { get { return Instance; } }

		public double e { get { return Constants.e_double; } }
		public double pi { get { return Constants.pi_double; } }

		public double factory(int value) { return value; }
		public double factory(float value) { return value; }
		public double factory(double value) { return value; }
		public double factory(decimal value) { return (double)value; }
	}

	public class Constants_float : Constants<float>
	{
		private Constants_float() { _instance = this; }
		private static Constants_float _instance;
		private static Constants_float Instance
		{
			get
			{
				if (_instance == null)
					return _instance = new Constants_float();
				else
					return _instance;
			}
		}

		/// <summary>Gets Arithmetic for "int" types.</summary>
		public static Constants_float Get { get { return Instance; } }

		public float e { get { return Constants.e_float; } }
		public float pi { get { return Constants.pi_float; } }

		public float factory(int value) { return value; }
		public float factory(float value) { return value; }
		public float factory(double value) { return (float)value; }
		public float factory(decimal value) { return (float)value; }
	}

  public class Constants_long : Constants<long>
  {
    private Constants_long() { _instance = this; }
    private static Constants_long _instance;
    private static Constants_long Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Constants_long();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "int" types.</summary>
    public static Constants_long Get { get { return Instance; } }

    public long e { get { throw new Constants.Error("constant e cannot be represented as a long"); } }
    public long pi { get { throw new Constants.Error("constant e cannot be represented as a long"); } }

    public long factory(int value) { return value; }
    public long factory(float value) { return (long)value; }
    public long factory(double value) { return (long)value; }
    public long factory(decimal value) { return (long)value; }
  }

  public class Constants_int : Constants<int>
  {
    private Constants_int() { _instance = this; }
    private static Constants_int _instance;
    private static Constants_int Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Constants_int();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "int" types.</summary>
    public static Constants_int Get { get { return Instance; } }

    public int e { get { throw new Constants.Error("constant e cannot be represented as an int"); } }
    public int pi { get { throw new Constants.Error("constant e cannot be represented as an int"); } }

    public int factory(int value) { return value; }
    public int factory(float value) { return (int)value; }
    public int factory(double value) { return (int)value; }
    public int factory(decimal value) { return (int)value; }
  }
}
