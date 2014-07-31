// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>Supplies trigonometry mathematics for generic types.</summary>
  /// <typeparam name="T">The type this trigonometry library can perform on.</typeparam>
  public interface Trigonometry<T>
  {
		/// <summary>Converts degrees to radians.</summary>
		T toRadians(T angle);
		/// <summary>Converts radians to degrees.</summary>
		T toDegrees(T angle);
    /// <summary>Computes the sin of an angle.</summary>
    T sin(T angle);
    /// <summary>Computes the cos of an angle.</summary>
    T cos(T angle);
    /// <summary>Computes the tan of an angle.</summary>
    T tan(T angle);
    /// <summary>Computes the sec of an angle.</summary>
    T sec(T angle);
    /// <summary>Computes the csc of an angle.</summary>
    T csc(T angle);
    /// <summary>Computes the cot of an angle.</summary>
    T cot(T angle);
    /// <summary>Computes the arcsin of a ratio.</summary>
    T arcsin(T ratio);
    /// <summary>Computes the arccos of a ratio.</summary>
    T arccos(T ratio);
    /// <summary>Computes the arctan of a ratio.</summary>
    T arctan(T ratio);
    /// <summary>Computes the arccsc of a ratio.</summary>
    T arccsc(T ratio);
    /// <summary>Computes the arcsec of a ratio.</summary>
    T arcsec(T ratio);
    /// <summary>Computes the arccot of a ratio.</summary>
    T arccot(T ratio);
  }

  public static class Trigonometry
  {
    #region Delegates

    public delegate T _toRadians<T>(T angle);
		public delegate T _toDegrees<T>(T angle);
		public delegate T _sin<T>(T angle);
		public delegate T _cos<T>(T angle);
		public delegate T _tan<T>(T angle);
		public delegate T _sec<T>(T angle);
		public delegate T _csc<T>(T angle);
		public delegate T _cot<T>(T angle);
		public delegate T _arcsin<T>(T ratio);
		public delegate T _arccos<T>(T ratio);
		public delegate T _arctan<T>(T ratio);
		public delegate T _arccsc<T>(T ratio);
		public delegate T _arcsec<T>(T ratio);
		public delegate T _arccot<T>(T ratio);

    #endregion

    #region Libraries

    public static Seven.Structures.Map<object, System.Type> _trigonometries =
			new Seven.Structures.Map_Linked<object, System.Type>(
				(System.Type left, System.Type right) => { return left == right; },
				(System.Type type) => { return type.GetHashCode(); })
				{
					{ typeof(float), Trigonometry_float.Get },
          { typeof(double), Trigonometry_double.Get },
				};

		public static Trigonometry<T> Get<T>()
		{
			try { return _trigonometries[typeof(T)] as Trigonometry<T>; }
			catch { throw new Error("Arithmetic does not yet exist for " + typeof(T).ToString()); }
		}

    #region Built-In

    private class Trigonometry_float : Trigonometry<float>
    {
      private Trigonometry_float() { _instance = this; }
      private static Trigonometry_float _instance;
      private static Trigonometry_float Instance
      {
        get
        {
          if (_instance == null)
            return _instance = new Trigonometry_float();
          else
            return _instance;
        }
      }

      public static Trigonometry_float Get { get { return Instance; } }

      public float toRadians(float angle)
      {
        return angle * Constants.pi_float / 180f;
      }

      public float toDegrees(float angle)
      {
        return angle * 180f / Constants.pi_float;
      }

      public float sin(float angle)
      {
        return (float)System.Math.Sin(angle);

        // THE FOLLOWING IS PERSONAL SIN FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Sin Function
        //// get the angle to be within the unit circle
        //angle = angle % (TwoPi);

        //// if the angle is negative, inverse it against the full unit circle
        //if (angle < 0)
        //  angle = TwoPi + angle;

        //// adjust for quadrants
        //// NOTE: if you want more accuracy, you can follow this pattern
        //// sin(x) = x - x^3/3! + x^5/5! - x^7/7! ...
        //// the more terms you include the more accurate it is
        //float angleCubed;
        //float angleToTheFifth;
        //// quadrant 1
        //if (angle <= HalfPi)
        //{
        //  angleCubed = angle * angle * angle;
        //  angleToTheFifth = angleCubed * angle * angle;
        //  return angle
        //    - ((angleCubed) / 6)
        //    + ((angleToTheFifth) / 120);
        //}
        //// quadrant 2
        //else if (angle <= Pi)
        //{
        //  angle = HalfPi - (angle % HalfPi);
        //  angleCubed = angle * angle * angle;
        //  angleToTheFifth = angleCubed * angle * angle;
        //  return angle
        //    - ((angleCubed) / 6)
        //    + ((angleToTheFifth) / 120);
        //}
        //// quadrant 3
        //else if (angle <= ThreeHalvesPi)
        //{
        //  angle = angle % Pi;
        //  angleCubed = angle * angle * angle;
        //  angleToTheFifth = angleCubed * angle * angle;
        //  return -(angle
        //      - ((angleCubed) / 6)
        //      + ((angleToTheFifth) / 120));
        //}
        //// quadrant 4  
        //else
        //{
        //  angle = HalfPi - (angle % HalfPi);
        //  angleCubed = angle * angle * angle;
        //  angleToTheFifth = angleCubed * angle * angle;
        //  return -(angle
        //      - ((angleCubed) / 6)
        //      + ((angleToTheFifth) / 120));
        //}
        #endregion
      }

      public float cos(float angle)
      {
        return (float)System.Math.Cos(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Cos Function
        //// If you wanted to be cheap, you could just use the following commented line...
        //// return Sin(angle + (Pi / 2));

        //// get the angle to be within the unit circle
        //angle = angle % (TwoPi);

        //// if the angle is negative, inverse it against the full unit circle
        //if (angle < 0)
        //  angle = TwoPi + angle;

        //// adjust for quadrants
        //// NOTE: if you want more accuracy, you can follow this pattern
        //// cos(x) = 1 - x^2/2! + x^4/4! - x^6/6! ...
        //// the terms you include the more accuracy it is
        //float angleSquared;
        //float angleToTheFourth;
        //float angleToTheSixth;
        //// quadrant 1
        //if (angle <= HalfPi)
        //{
        //  angleSquared = angle * angle;
        //  angleToTheFourth = angleSquared * angle * angle;
        //  angleToTheSixth = angleToTheFourth * angle * angle;
        //  return 1
        //    - (angleSquared / 2)
        //    + (angleToTheFourth / 24)
        //    - (angleToTheSixth / 720);
        //}
        //// quadrant 2
        //else if (angle <= Pi)
        //{
        //  angle = HalfPi - (angle % HalfPi);
        //  angleSquared = angle * angle;
        //  angleToTheFourth = angleSquared * angle * angle;
        //  angleToTheSixth = angleToTheFourth * angle * angle;
        //  return -(1
        //    - (angleSquared / 2)
        //    + (angleToTheFourth / 24)
        //    - (angleToTheSixth / 720));
        //}
        //// quadrant 3
        //else if (angle <= ThreeHalvesPi)
        //{
        //  angle = angle % Pi;
        //  angleSquared = angle * angle;
        //  angleToTheFourth = angleSquared * angle * angle;
        //  angleToTheSixth = angleToTheFourth * angle * angle;
        //  return -(1
        //    - (angleSquared / 2)
        //    + (angleToTheFourth / 24)
        //    - (angleToTheSixth / 720));
        //}
        //// quadrant 4  
        //else
        //{
        //  angle = HalfPi - (angle % HalfPi);
        //  angleSquared = angle * angle;
        //  angleToTheFourth = angleSquared * angle * angle;
        //  angleToTheSixth = angleToTheFourth * angle * angle;
        //  return 1
        //    - (angleSquared / 2)
        //    + (angleToTheFourth / 24)
        //    - (angleToTheSixth / 720);
        //}
        #endregion
      }

      public float tan(float angle)
      {
        return (float)System.Math.Tan(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Tan Function
        //// "sin / cos" results in "opposite side / adjacent side", which is equal to tangent
        //return Sin(angle) / Cos(angle);
        #endregion
      }

      public float sec(float angle)
      {
        return 1.0f / (float)System.Math.Cos(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Sec Function
        //// by definition, sec is the reciprical of cos
        //return 1 / Cos(angle);
        #endregion
      }

      public float csc(float angle)
      {
        return 1.0f / (float)System.Math.Sin(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Csc Function
        //// by definition, csc is the reciprical of sin
        //return 1 / Sin(angle);
        #endregion
      }

      public float cot(float angle)
      {
        return 1.0f / (float)System.Math.Tan(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Cot Function
        //// by definition, cot is the reciprical of tan
        //return 1 / Tan(angle);
        #endregion
      }

      public float arcsin(float sinRatio)
      {
        return (float)System.Math.Asin(sinRatio);
        //I haven't made a custom ArcSin function yet...
      }

      public float arccos(float cosRatio)
      {
        return (float)System.Math.Acos(cosRatio);
        //I haven't made a custom ArcCos function yet...
      }

      public float arctan(float tanRatio)
      {
        return (float)System.Math.Atan(tanRatio);
        //I haven't made a custom ArcTan function yet...
      }

      public float arccsc(float cscRatio)
      {
        return (float)System.Math.Asin(1.0f / cscRatio);
        //I haven't made a custom ArcCsc function yet...
      }

      public float arcsec(float secRatio)
      {
        return (float)System.Math.Acos(1.0f / secRatio);
        //I haven't made a custom ArcSec function yet...
      }

      public float arccot(float cotRatio)
      {
        return (float)System.Math.Atan(1.0f / cotRatio);
        //I haven't made a custom ArcCot function yet...
      }
    }

    private class Trigonometry_double : Trigonometry<double>
    {
      private Trigonometry_double() { _instance = this; }
      private static Trigonometry_double _instance;
      private static Trigonometry_double Instance
      {
        get
        {
          if (_instance == null)
            return _instance = new Trigonometry_double();
          else
            return _instance;
        }
      }

      public static Trigonometry_double Get { get { return Instance; } }

      public double toRadians(double angle)
      {
        return angle * Constants.pi_double / 180d;
      }

      public double toDegrees(double angle)
      {
        return angle * 180d / Constants.pi_double;
      }

      public double sin(double angle)
      {
        return System.Math.Sin(angle);

        // THE FOLLOWING IS PERSONAL SIN FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Sin Function
        //// get the angle to be within the unit circle
        //angle = angle % (TwoPi);

        //// if the angle is negative, inverse it against the full unit circle
        //if (angle < 0)
        //  angle = TwoPi + angle;

        //// adjust for quadrants
        //// NOTE: if you want more accuracy, you can follow this pattern
        //// sin(x) = x - x^3/3! + x^5/5! - x^7/7! ...
        //// the more terms you include the more accurate it is
        //float angleCubed;
        //float angleToTheFifth;
        //// quadrant 1
        //if (angle <= HalfPi)
        //{
        //  angleCubed = angle * angle * angle;
        //  angleToTheFifth = angleCubed * angle * angle;
        //  return angle
        //    - ((angleCubed) / 6)
        //    + ((angleToTheFifth) / 120);
        //}
        //// quadrant 2
        //else if (angle <= Pi)
        //{
        //  angle = HalfPi - (angle % HalfPi);
        //  angleCubed = angle * angle * angle;
        //  angleToTheFifth = angleCubed * angle * angle;
        //  return angle
        //    - ((angleCubed) / 6)
        //    + ((angleToTheFifth) / 120);
        //}
        //// quadrant 3
        //else if (angle <= ThreeHalvesPi)
        //{
        //  angle = angle % Pi;
        //  angleCubed = angle * angle * angle;
        //  angleToTheFifth = angleCubed * angle * angle;
        //  return -(angle
        //      - ((angleCubed) / 6)
        //      + ((angleToTheFifth) / 120));
        //}
        //// quadrant 4  
        //else
        //{
        //  angle = HalfPi - (angle % HalfPi);
        //  angleCubed = angle * angle * angle;
        //  angleToTheFifth = angleCubed * angle * angle;
        //  return -(angle
        //      - ((angleCubed) / 6)
        //      + ((angleToTheFifth) / 120));
        //}
        #endregion
      }

      public double cos(double angle)
      {
        return System.Math.Cos(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Cos Function
        //// If you wanted to be cheap, you could just use the following commented line...
        //// return Sin(angle + (Pi / 2));

        //// get the angle to be within the unit circle
        //angle = angle % (TwoPi);

        //// if the angle is negative, inverse it against the full unit circle
        //if (angle < 0)
        //  angle = TwoPi + angle;

        //// adjust for quadrants
        //// NOTE: if you want more accuracy, you can follow this pattern
        //// cos(x) = 1 - x^2/2! + x^4/4! - x^6/6! ...
        //// the terms you include the more accuracy it is
        //float angleSquared;
        //float angleToTheFourth;
        //float angleToTheSixth;
        //// quadrant 1
        //if (angle <= HalfPi)
        //{
        //  angleSquared = angle * angle;
        //  angleToTheFourth = angleSquared * angle * angle;
        //  angleToTheSixth = angleToTheFourth * angle * angle;
        //  return 1
        //    - (angleSquared / 2)
        //    + (angleToTheFourth / 24)
        //    - (angleToTheSixth / 720);
        //}
        //// quadrant 2
        //else if (angle <= Pi)
        //{
        //  angle = HalfPi - (angle % HalfPi);
        //  angleSquared = angle * angle;
        //  angleToTheFourth = angleSquared * angle * angle;
        //  angleToTheSixth = angleToTheFourth * angle * angle;
        //  return -(1
        //    - (angleSquared / 2)
        //    + (angleToTheFourth / 24)
        //    - (angleToTheSixth / 720));
        //}
        //// quadrant 3
        //else if (angle <= ThreeHalvesPi)
        //{
        //  angle = angle % Pi;
        //  angleSquared = angle * angle;
        //  angleToTheFourth = angleSquared * angle * angle;
        //  angleToTheSixth = angleToTheFourth * angle * angle;
        //  return -(1
        //    - (angleSquared / 2)
        //    + (angleToTheFourth / 24)
        //    - (angleToTheSixth / 720));
        //}
        //// quadrant 4  
        //else
        //{
        //  angle = HalfPi - (angle % HalfPi);
        //  angleSquared = angle * angle;
        //  angleToTheFourth = angleSquared * angle * angle;
        //  angleToTheSixth = angleToTheFourth * angle * angle;
        //  return 1
        //    - (angleSquared / 2)
        //    + (angleToTheFourth / 24)
        //    - (angleToTheSixth / 720);
        //}
        #endregion
      }

      public double tan(double angle)
      {
        return System.Math.Tan(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Tan Function
        //// "sin / cos" results in "opposite side / adjacent side", which is equal to tangent
        //return Sin(angle) / Cos(angle);
        #endregion
      }

      public double sec(double angle)
      {
        return 1.0f / (double)System.Math.Cos(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Sec Function
        //// by definition, sec is the reciprical of cos
        //return 1 / Cos(angle);
        #endregion
      }

      public double csc(double angle)
      {
        return 1.0d / System.Math.Sin(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Csc Function
        //// by definition, csc is the reciprical of sin
        //return 1 / Sin(angle);
        #endregion
      }

      public double cot(double angle)
      {
        return 1.0d / System.Math.Tan(angle);

        // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
        // THE SYSTEM FUNCTION IN ITS CURRENT STATE
        #region Custom Cot Function
        //// by definition, cot is the reciprical of tan
        //return 1 / Tan(angle);
        #endregion
      }

      public double arcsin(double sinRatio)
      {
        return System.Math.Asin(sinRatio);
        //I haven't made a custom ArcSin function yet...
      }

      public double arccos(double cosRatio)
      {
        return System.Math.Acos(cosRatio);
        //I haven't made a custom ArcCos function yet...
      }

      public double arctan(double tanRatio)
      {
        return System.Math.Atan(tanRatio);
        //I haven't made a custom ArcTan function yet...
      }

      public double arccsc(double cscRatio)
      {
        return System.Math.Asin(1.0d / cscRatio);
        //I haven't made a custom ArcCsc function yet...
      }

      public double arcsec(double secRatio)
      {
        return System.Math.Acos(1.0d / secRatio);
        //I haven't made a custom ArcSec function yet...
      }

      public double arccot(double cotRatio)
      {
        return System.Math.Atan(1.0d / cotRatio);
        //I haven't made a custom ArcCot function yet...
      }
    }

    #endregion

    #endregion

    #region Implementations

    #region decimal

    public static decimal toRadians(decimal angle)
    {
      return (decimal)(angle * Constants.pi_decimal / 180M);
    }

    public static decimal toDegrees(decimal angle)
    {
      return (decimal)(angle * 180M / Constants.pi_decimal);
    }

    public static decimal sin(decimal angle)
    {
      return (decimal)System.Math.Sin((double)angle);

      // THE FOLLOWING IS PERSONAL SIN FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Sin Function
      //// get the angle to be within the unit circle
      //angle = angle % (TwoPi);

      //// if the angle is negative, inverse it against the full unit circle
      //if (angle < 0)
      //  angle = TwoPi + angle;

      //// adjust for quadrants
      //// NOTE: if you want more accuracy, you can follow this pattern
      //// sin(x) = x - x^3/3! + x^5/5! - x^7/7! ...
      //// the more terms you include the more accurate it is
      //float angleCubed;
      //float angleToTheFifth;
      //// quadrant 1
      //if (angle <= HalfPi)
      //{
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return angle
      //    - ((angleCubed) / 6)
      //    + ((angleToTheFifth) / 120);
      //}
      //// quadrant 2
      //else if (angle <= Pi)
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return angle
      //    - ((angleCubed) / 6)
      //    + ((angleToTheFifth) / 120);
      //}
      //// quadrant 3
      //else if (angle <= ThreeHalvesPi)
      //{
      //  angle = angle % Pi;
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return -(angle
      //      - ((angleCubed) / 6)
      //      + ((angleToTheFifth) / 120));
      //}
      //// quadrant 4  
      //else
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return -(angle
      //      - ((angleCubed) / 6)
      //      + ((angleToTheFifth) / 120));
      //}
      #endregion
    }

    public static decimal cos(decimal angle)
    {
      return (decimal)System.Math.Cos((double)angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Cos Function
      //// If you wanted to be cheap, you could just use the following commented line...
      //// return Sin(angle + (Pi / 2));

      //// get the angle to be within the unit circle
      //angle = angle % (TwoPi);

      //// if the angle is negative, inverse it against the full unit circle
      //if (angle < 0)
      //  angle = TwoPi + angle;

      //// adjust for quadrants
      //// NOTE: if you want more accuracy, you can follow this pattern
      //// cos(x) = 1 - x^2/2! + x^4/4! - x^6/6! ...
      //// the terms you include the more accuracy it is
      //float angleSquared;
      //float angleToTheFourth;
      //float angleToTheSixth;
      //// quadrant 1
      //if (angle <= HalfPi)
      //{
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return 1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720);
      //}
      //// quadrant 2
      //else if (angle <= Pi)
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return -(1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720));
      //}
      //// quadrant 3
      //else if (angle <= ThreeHalvesPi)
      //{
      //  angle = angle % Pi;
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return -(1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720));
      //}
      //// quadrant 4  
      //else
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return 1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720);
      //}
      #endregion
    }

    public static decimal tan(decimal angle)
    {
      return (decimal)System.Math.Tan((double)angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Tan Function
      //// "sin / cos" results in "opposite side / adjacent side", which is equal to tangent
      //return Sin(angle) / Cos(angle);
      #endregion
    }

    public static decimal sec(decimal angle)
    {
      return (decimal)(1d / System.Math.Cos((double)angle));

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Sec Function
      //// by definition, sec is the reciprical of cos
      //return 1 / Cos(angle);
      #endregion
    }

    public static decimal csc(decimal angle)
    {
      return (decimal)(1d / System.Math.Sin((double)angle));

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Csc Function
      //// by definition, csc is the reciprical of sin
      //return 1 / Sin(angle);
      #endregion
    }

    public static decimal cot(decimal angle)
    {
      return (decimal)(1.0d / System.Math.Tan((double)angle));

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Cot Function
      //// by definition, cot is the reciprical of tan
      //return 1 / Tan(angle);
      #endregion
    }

    public static decimal arcsin(decimal sinRatio)
    {
      return (decimal)System.Math.Asin((double)sinRatio);
      //I haven't made a custom ArcSin function yet...
    }

    public static decimal arccos(decimal cosRatio)
    {
      return (decimal)System.Math.Acos((double)cosRatio);
      //I haven't made a custom ArcCos function yet...
    }

    public static decimal arctan(decimal tanRatio)
    {
      return (decimal)System.Math.Atan((double)tanRatio);
      //I haven't made a custom ArcTan function yet...
    }

    public static decimal arccsc(decimal cscRatio)
    {
      return (decimal)System.Math.Asin(1.0d / (double)cscRatio);
      //I haven't made a custom ArcCsc function yet...
    }

    public static decimal arcsec(decimal secRatio)
    {
      return (decimal)System.Math.Acos(1.0d / (double)secRatio);
      //I haven't made a custom ArcSec function yet...
    }

    public static decimal arccot(decimal cotRatio)
    {
      return (decimal)System.Math.Atan(1.0d / (double)cotRatio);
      //I haven't made a custom ArcCot function yet...
    }

    #endregion

    #region double

    public static double toRadians(double angle)
    {
      return angle * Constants.pi_double / 180d;
    }

    public static double toDegrees(double angle)
    {
      return angle * 180d / Constants.pi_double;
    }

    public static double sin(double angle)
    {
      return System.Math.Sin(angle);

      // THE FOLLOWING IS PERSONAL SIN FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Sin Function
      //// get the angle to be within the unit circle
      //angle = angle % (TwoPi);

      //// if the angle is negative, inverse it against the full unit circle
      //if (angle < 0)
      //  angle = TwoPi + angle;

      //// adjust for quadrants
      //// NOTE: if you want more accuracy, you can follow this pattern
      //// sin(x) = x - x^3/3! + x^5/5! - x^7/7! ...
      //// the more terms you include the more accurate it is
      //float angleCubed;
      //float angleToTheFifth;
      //// quadrant 1
      //if (angle <= HalfPi)
      //{
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return angle
      //    - ((angleCubed) / 6)
      //    + ((angleToTheFifth) / 120);
      //}
      //// quadrant 2
      //else if (angle <= Pi)
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return angle
      //    - ((angleCubed) / 6)
      //    + ((angleToTheFifth) / 120);
      //}
      //// quadrant 3
      //else if (angle <= ThreeHalvesPi)
      //{
      //  angle = angle % Pi;
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return -(angle
      //      - ((angleCubed) / 6)
      //      + ((angleToTheFifth) / 120));
      //}
      //// quadrant 4  
      //else
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return -(angle
      //      - ((angleCubed) / 6)
      //      + ((angleToTheFifth) / 120));
      //}
      #endregion
    }

    public static double cos(double angle)
    {
      return System.Math.Cos(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Cos Function
      //// If you wanted to be cheap, you could just use the following commented line...
      //// return Sin(angle + (Pi / 2));

      //// get the angle to be within the unit circle
      //angle = angle % (TwoPi);

      //// if the angle is negative, inverse it against the full unit circle
      //if (angle < 0)
      //  angle = TwoPi + angle;

      //// adjust for quadrants
      //// NOTE: if you want more accuracy, you can follow this pattern
      //// cos(x) = 1 - x^2/2! + x^4/4! - x^6/6! ...
      //// the terms you include the more accuracy it is
      //float angleSquared;
      //float angleToTheFourth;
      //float angleToTheSixth;
      //// quadrant 1
      //if (angle <= HalfPi)
      //{
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return 1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720);
      //}
      //// quadrant 2
      //else if (angle <= Pi)
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return -(1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720));
      //}
      //// quadrant 3
      //else if (angle <= ThreeHalvesPi)
      //{
      //  angle = angle % Pi;
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return -(1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720));
      //}
      //// quadrant 4  
      //else
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return 1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720);
      //}
      #endregion
    }

    public static double tan(double angle)
    {
      return System.Math.Tan(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Tan Function
      //// "sin / cos" results in "opposite side / adjacent side", which is equal to tangent
      //return Sin(angle) / Cos(angle);
      #endregion
    }

    public static double sec(double angle)
    {
      return 1.0f / (double)System.Math.Cos(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Sec Function
      //// by definition, sec is the reciprical of cos
      //return 1 / Cos(angle);
      #endregion
    }

    public static double csc(double angle)
    {
      return 1.0d / System.Math.Sin(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Csc Function
      //// by definition, csc is the reciprical of sin
      //return 1 / Sin(angle);
      #endregion
    }

    public static double cot(double angle)
    {
      return 1.0d / System.Math.Tan(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Cot Function
      //// by definition, cot is the reciprical of tan
      //return 1 / Tan(angle);
      #endregion
    }

    public static double arcsin(double sinRatio)
    {
      return System.Math.Asin(sinRatio);
      //I haven't made a custom ArcSin function yet...
    }

    public static double arccos(double cosRatio)
    {
      return System.Math.Acos(cosRatio);
      //I haven't made a custom ArcCos function yet...
    }

    public static double arctan(double tanRatio)
    {
      return System.Math.Atan(tanRatio);
      //I haven't made a custom ArcTan function yet...
    }

    public static double arccsc(double cscRatio)
    {
      return System.Math.Asin(1.0d / cscRatio);
      //I haven't made a custom ArcCsc function yet...
    }

    public static double arcsec(double secRatio)
    {
      return System.Math.Acos(1.0d / secRatio);
      //I haven't made a custom ArcSec function yet...
    }

    public static double arccot(double cotRatio)
    {
      return System.Math.Atan(1.0d / cotRatio);
      //I haven't made a custom ArcCot function yet...
    }

    #endregion

    #region float

    public static float toRadians(float angle)
    {
      return angle * Constants.pi_float / 180f;
    }

    public static float toDegrees(float angle)
    {
      return angle * 180f / Constants.pi_float;
    }

    public static float sin(float angle)
    {
      return (float)System.Math.Sin(angle);

      // THE FOLLOWING IS PERSONAL SIN FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Sin Function
      //// get the angle to be within the unit circle
      //angle = angle % (TwoPi);

      //// if the angle is negative, inverse it against the full unit circle
      //if (angle < 0)
      //  angle = TwoPi + angle;

      //// adjust for quadrants
      //// NOTE: if you want more accuracy, you can follow this pattern
      //// sin(x) = x - x^3/3! + x^5/5! - x^7/7! ...
      //// the more terms you include the more accurate it is
      //float angleCubed;
      //float angleToTheFifth;
      //// quadrant 1
      //if (angle <= HalfPi)
      //{
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return angle
      //    - ((angleCubed) / 6)
      //    + ((angleToTheFifth) / 120);
      //}
      //// quadrant 2
      //else if (angle <= Pi)
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return angle
      //    - ((angleCubed) / 6)
      //    + ((angleToTheFifth) / 120);
      //}
      //// quadrant 3
      //else if (angle <= ThreeHalvesPi)
      //{
      //  angle = angle % Pi;
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return -(angle
      //      - ((angleCubed) / 6)
      //      + ((angleToTheFifth) / 120));
      //}
      //// quadrant 4  
      //else
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleCubed = angle * angle * angle;
      //  angleToTheFifth = angleCubed * angle * angle;
      //  return -(angle
      //      - ((angleCubed) / 6)
      //      + ((angleToTheFifth) / 120));
      //}
      #endregion
    }

    public static float cos(float angle)
    {
      return (float)System.Math.Cos(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Cos Function
      //// If you wanted to be cheap, you could just use the following commented line...
      //// return Sin(angle + (Pi / 2));

      //// get the angle to be within the unit circle
      //angle = angle % (TwoPi);

      //// if the angle is negative, inverse it against the full unit circle
      //if (angle < 0)
      //  angle = TwoPi + angle;

      //// adjust for quadrants
      //// NOTE: if you want more accuracy, you can follow this pattern
      //// cos(x) = 1 - x^2/2! + x^4/4! - x^6/6! ...
      //// the terms you include the more accuracy it is
      //float angleSquared;
      //float angleToTheFourth;
      //float angleToTheSixth;
      //// quadrant 1
      //if (angle <= HalfPi)
      //{
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return 1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720);
      //}
      //// quadrant 2
      //else if (angle <= Pi)
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return -(1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720));
      //}
      //// quadrant 3
      //else if (angle <= ThreeHalvesPi)
      //{
      //  angle = angle % Pi;
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return -(1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720));
      //}
      //// quadrant 4  
      //else
      //{
      //  angle = HalfPi - (angle % HalfPi);
      //  angleSquared = angle * angle;
      //  angleToTheFourth = angleSquared * angle * angle;
      //  angleToTheSixth = angleToTheFourth * angle * angle;
      //  return 1
      //    - (angleSquared / 2)
      //    + (angleToTheFourth / 24)
      //    - (angleToTheSixth / 720);
      //}
      #endregion
    }

    public static float tan(float angle)
    {
      return (float)System.Math.Tan(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Tan Function
      //// "sin / cos" results in "opposite side / adjacent side", which is equal to tangent
      //return Sin(angle) / Cos(angle);
      #endregion
    }

    public static float sec(float angle)
    {
      return 1.0f / (float)System.Math.Cos(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Sec Function
      //// by definition, sec is the reciprical of cos
      //return 1 / Cos(angle);
      #endregion
    }

    public static float csc(float angle)
    {
      return 1.0f / (float)System.Math.Sin(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Csc Function
      //// by definition, csc is the reciprical of sin
      //return 1 / Sin(angle);
      #endregion
    }

    public static float cot(float angle)
    {
      return 1.0f / (float)System.Math.Tan(angle);

      // THE FOLLOWING IS MY PERSONAL FUNCTION. IT WORKS BUT IT IS NOT AS FAST AS
      // THE SYSTEM FUNCTION IN ITS CURRENT STATE
      #region Custom Cot Function
      //// by definition, cot is the reciprical of tan
      //return 1 / Tan(angle);
      #endregion
    }

    public static float arcsin(float sinRatio)
    {
      return (float)System.Math.Asin(sinRatio);
      //I haven't made a custom ArcSin function yet...
    }

    public static float arccos(float cosRatio)
    {
      return (float)System.Math.Acos(cosRatio);
      //I haven't made a custom ArcCos function yet...
    }

    public static float arctan(float tanRatio)
    {
      return (float)System.Math.Atan(tanRatio);
      //I haven't made a custom ArcTan function yet...
    }

    public static float arccsc(float cscRatio)
    {
      return (float)System.Math.Asin(1.0f / cscRatio);
      //I haven't made a custom ArcCsc function yet...
    }

    public static float arcsec(float secRatio)
    {
      return (float)System.Math.Acos(1.0f / secRatio);
      //I haven't made a custom ArcSec function yet...
    }

    public static float arccot(float cotRatio)
    {
      return (float)System.Math.Atan(1.0f / cotRatio);
      //I haven't made a custom ArcCot function yet...
    }

    #endregion

    #endregion

    /// <summary>Error type for all arithmetic computations.</summary>
		public class Error : Seven.Error
		{
			public Error(string message) : base(message) { }
		}
  }
}
