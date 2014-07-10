// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

// THIS FILE CONTAINS EXTERNAL CITATIONS

namespace Seven
{
  /// <summary>A fraction represented as two ints (numerator / denomnator).</summary>
  /// <citation>
  /// This fraction imlpementation was originally developed by 
  /// Syed Mehroz Alam and posted as an open source project on 
  /// CodeProject.com. However, it has been modified since its
  /// addition into the Seven framework.
  /// http://www.codeproject.com/Articles/9078/Fraction-class-in-C
  /// 
  /// Original Author:
  /// Author: Syed Mehroz Alam
  /// Email: smehrozalam@yahoo.com
  /// URL: Programming Home http://www.geocities.com/smehrozalam/
  /// Date: 6/15/2004
  /// Time: 10:54 AM
  /// </citation>
  public struct fraction64
  {
    private int m_iNumerator;
    private int m_iDenominator;

    //public fraction64()
    //{
    //  m_iNumerator = 0;
    //  m_iDenominator = 1;
    //  ReduceFraction(this);
    //}

    public fraction64(int iWholeNumber)
    {
      m_iNumerator = iWholeNumber;
      m_iDenominator = 1;
      ReduceFraction(this);
    }

    public fraction64(double dDecimalValue)
    {
      fraction64 temp = ToFraction(dDecimalValue);
      m_iNumerator = temp.m_iNumerator;
      m_iDenominator = temp.m_iDenominator;
      ReduceFraction(this);
    }

    public fraction64(int numerator, int deniminator)
    {
      m_iNumerator = numerator;
      m_iDenominator = deniminator;
      ReduceFraction(this);
    }

    private void Initialize(int iNumerator, int iDenominator)
    {
      Numerator = iNumerator;
      Denominator = iDenominator;
      ReduceFraction(this);
    }

    public int Denominator
    {
      get
      { return m_iDenominator; }
      set
      {
        if (value != 0)
          m_iDenominator = value;
        else
          throw new Error("Denominator cannot be assigned a ZERO Value");
      }
    }

    public int Numerator
    {
      get
      { return m_iNumerator; }
      set
      { m_iNumerator = value; }
    }

    public int Value
    {
      set
      {
        m_iNumerator = value;
        m_iDenominator = 1;
      }
    }

    public double ToDouble()
    {
      return ((double)this.Numerator / this.Denominator);
    }

    public override string ToString()
    {
      string str;
      if (this.Denominator == 1)
        str = this.Numerator.ToString();
      else
        str = this.Numerator + "/" + this.Denominator;
      return str;
    }

    /// <summary>
    /// The function takes an string as an argument and returns its corresponding reduced fraction64
    /// the string can be an in the form of and integer, double or fraction64.
    /// e.g it can be like "123" or "123.321" or "123/456"
    /// </summary>
    public static fraction64 Parse(string strValue)
    {
      int i;
      for (i = 0; i < strValue.Length; i++)
        if (strValue[i] == '/')
          break;

      if (i == strValue.Length)		// if string is not in the form of a fraction64
        // then it is double or integer
        return (System.Convert.ToDouble(strValue));
      //return ( ToFraction( Convert.ToDouble(strValue) ) );

      // else string is in the form of Numerator/Denominator
      int iNumerator = System.Convert.ToInt32(strValue.Substring(0, i));
      int iDenominator = System.Convert.ToInt32(strValue.Substring(i + 1));
      return new fraction64(iNumerator, iDenominator);
    }


    /// <summary>
    /// The function takes a floating point number as an argument 
    /// and returns its corresponding reduced fraction64
    /// </summary>
    public static fraction64 ToFraction(double dValue)
    {
      try
      {
        checked
        {
          fraction64 frac;
          if (dValue % 1 == 0)	// if whole number
          {
            frac = new fraction64((int)dValue);
          }
          else
          {
            double dTemp = dValue;
            int iMultiple = 1;
            string strTemp = dValue.ToString();
            while (strTemp.IndexOf("E") > 0)	// if in the form like 12E-9
            {
              dTemp *= 10;
              iMultiple *= 10;
              strTemp = dTemp.ToString();
            }
            int i = 0;
            while (strTemp[i] != '.')
              i++;
            int iDigitsAfterDecimal = strTemp.Length - i - 1;
            while (iDigitsAfterDecimal > 0)
            {
              dTemp *= 10;
              iMultiple *= 10;
              iDigitsAfterDecimal--;
            }
            frac = new fraction64((int)System.Math.Round(dTemp), iMultiple);
          }
          return frac;
        }
      }
      catch (System.OverflowException)
      {
        throw new Error("Conversion not possible due to overflow");
      }
      catch (System.Exception)
      {
        throw new Error("Conversion not possible");
      }
    }

    /// <summary>
    /// The function replicates current Fraction object
    /// </summary>
    public fraction64 Duplicate()
    {
      fraction64 frac = new fraction64();
      frac.Numerator = Numerator;
      frac.Denominator = Denominator;
      return frac;
    }

    /// <summary>
    /// The function returns the inverse of a Fraction object
    /// </summary>
    public static fraction64 Inverse(fraction64 frac1)
    {
      if (frac1.Numerator == 0)
        throw new Error("Operation not possible (Denominator cannot be assigned a ZERO Value)");

      int iNumerator = frac1.Denominator;
      int iDenominator = frac1.Numerator;
      return (new fraction64(iNumerator, iDenominator));
    }


    /// <summary>
    /// Operators for the Fraction object
    /// includes -(unary), and binary opertors such as +,-,*,/
    /// also includes relational and logical operators such as ==,!=,<,>,<=,>=
    /// </summary>
    public static fraction64 operator -(fraction64 frac1)
    { return (Negate(frac1)); }

    public static fraction64 operator +(fraction64 frac1, fraction64 frac2)
    { return (Add(frac1, frac2)); }

    public static fraction64 operator +(int iNo, fraction64 frac1)
    { return (Add(frac1, new fraction64(iNo))); }

    public static fraction64 operator +(fraction64 frac1, int iNo)
    { return (Add(frac1, new fraction64(iNo))); }

    public static fraction64 operator +(double dbl, fraction64 frac1)
    { return (Add(frac1, fraction64.ToFraction(dbl))); }

    public static fraction64 operator +(fraction64 frac1, double dbl)
    { return (Add(frac1, fraction64.ToFraction(dbl))); }

    public static fraction64 operator -(fraction64 frac1, fraction64 frac2)
    { return (Add(frac1, -frac2)); }

    public static fraction64 operator -(int iNo, fraction64 frac1)
    { return (Add(-frac1, new fraction64(iNo))); }

    public static fraction64 operator -(fraction64 frac1, int iNo)
    { return (Add(frac1, -(new fraction64(iNo)))); }

    public static fraction64 operator -(double dbl, fraction64 frac1)
    { return (Add(-frac1, fraction64.ToFraction(dbl))); }

    public static fraction64 operator -(fraction64 frac1, double dbl)
    { return (Add(frac1, -fraction64.ToFraction(dbl))); }

    public static fraction64 operator *(fraction64 frac1, fraction64 frac2)
    { return (Multiply(frac1, frac2)); }

    public static fraction64 operator *(int iNo, fraction64 frac1)
    { return (Multiply(frac1, new fraction64(iNo))); }

    public static fraction64 operator *(fraction64 frac1, int iNo)
    { return (Multiply(frac1, new fraction64(iNo))); }

    public static fraction64 operator *(double dbl, fraction64 frac1)
    { return (Multiply(frac1, fraction64.ToFraction(dbl))); }

    public static fraction64 operator *(fraction64 frac1, double dbl)
    { return (Multiply(frac1, fraction64.ToFraction(dbl))); }

    public static fraction64 operator /(fraction64 frac1, fraction64 frac2)
    { return (Multiply(frac1, Inverse(frac2))); }

    public static fraction64 operator /(int iNo, fraction64 frac1)
    { return (Multiply(Inverse(frac1), new fraction64(iNo))); }

    public static fraction64 operator /(fraction64 frac1, int iNo)
    { return (Multiply(frac1, Inverse(new fraction64(iNo)))); }

    public static fraction64 operator /(double dbl, fraction64 frac1)
    { return (Multiply(Inverse(frac1), fraction64.ToFraction(dbl))); }

    public static fraction64 operator /(fraction64 frac1, double dbl)
    { return (Multiply(frac1, fraction64.Inverse(fraction64.ToFraction(dbl)))); }

    public static bool operator ==(fraction64 frac1, fraction64 frac2)
    { return frac1.Equals(frac2); }

    public static bool operator !=(fraction64 frac1, fraction64 frac2)
    { return (!frac1.Equals(frac2)); }

    public static bool operator ==(fraction64 frac1, int iNo)
    { return frac1.Equals(new fraction64(iNo)); }

    public static bool operator !=(fraction64 frac1, int iNo)
    { return (!frac1.Equals(new fraction64(iNo))); }

    public static bool operator ==(fraction64 frac1, double dbl)
    { return frac1.Equals(new fraction64(dbl)); }

    public static bool operator !=(fraction64 frac1, double dbl)
    { return (!frac1.Equals(new fraction64(dbl))); }

    public static bool operator <(fraction64 frac1, fraction64 frac2)
    { return frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator; }

    public static bool operator >(fraction64 frac1, fraction64 frac2)
    { return frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator; }

    public static bool operator <=(fraction64 frac1, fraction64 frac2)
    { return frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator; }

    public static bool operator >=(fraction64 frac1, fraction64 frac2)
    { return frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator; }


    /// <summary>Casts a int to a fraction64 (implicit).</summary>
    /// <param name="l">The int to be casted.</param>
    /// <returns></returns>
    public static implicit operator fraction64(int l)
    { return new fraction64(l); }
    public static implicit operator fraction64(double dNo)
    { return new fraction64(dNo); }

    /// <summary>
    /// overloaed user defined conversions: from fraction64s to double and string
    /// </summary>
    public static explicit operator double(fraction64 frac)
    { return frac.ToDouble(); }


    /// <summary>
    /// checks whether two fraction64s are equal
    /// </summary>
    public override bool Equals(object obj)
    {
      fraction64 frac = (fraction64)obj;
      return (Numerator == frac.Numerator && Denominator == frac.Denominator);
    }

    /// <summary>
    /// returns a hash code for this fraction64
    /// </summary>
    public override int GetHashCode()
    {
      return (System.Convert.ToInt32((Numerator ^ Denominator) & 0xFFFFFFFF));
    }

    /// <summary>
    /// internal function for negation
    /// </summary>
    private static fraction64 Negate(fraction64 frac1)
    {
      int iNumerator = -frac1.Numerator;
      int iDenominator = frac1.Denominator;
      return (new fraction64(iNumerator, iDenominator));

    }

    /// <summary>
    /// internal functions for binary operations
    /// </summary>
    private static fraction64 Add(fraction64 frac1, fraction64 frac2)
    {
      try
      {
        checked
        {
          int iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
          int iDenominator = frac1.Denominator * frac2.Denominator;
          return (new fraction64(iNumerator, iDenominator));
        }
      }
      catch (System.OverflowException)
      {
        throw new Error("Overflow occurred while performing arithemetic operation");
      }
      catch (System.Exception)
      {
        throw new Error("An error occurred while performing arithemetic operation");
      }
    }

    private static fraction64 Multiply(fraction64 frac1, fraction64 frac2)
    {
      try
      {
        checked
        {
          int iNumerator = frac1.Numerator * frac2.Numerator;
          int iDenominator = frac1.Denominator * frac2.Denominator;
          return (new fraction64(iNumerator, iDenominator));
        }
      }
      catch (System.OverflowException)
      {
        throw new Error("Overflow occurred while performing arithemetic operation");
      }
      catch (System.Exception)
      {
        throw new Error("An error occurred while performing arithemetic operation");
      }
    }

    /// <summary>
    /// The function returns GCD of two numbers (used for reducing a Fraction)
    /// </summary>
    private static int GCD(int iNo1, int iNo2)
    {
      // take absolute values
      if (iNo1 < 0) iNo1 = -iNo1;
      if (iNo2 < 0) iNo2 = -iNo2;

      do
      {
        if (iNo1 < iNo2)
        {
          int tmp = iNo1;  // swap the two operands
          iNo1 = iNo2;
          iNo2 = tmp;
        }
        iNo1 = iNo1 % iNo2;
      } while (iNo1 != 0);
      return iNo2;
    }

    /// <summary>
    /// The function reduces(simplifies) a Fraction object by dividing both its numerator 
    /// and denominator by their GCD
    /// </summary>
    public static void ReduceFraction(fraction64 frac)
    {
      try
      {
        if (frac.Numerator == 0)
        {
          frac.Denominator = 1;
          return;
        }

        int iGCD = GCD(frac.Numerator, frac.Denominator);
        frac.Numerator /= iGCD;
        frac.Denominator /= iGCD;

        if (frac.Denominator < 0)	// if -ve sign in denominator
        {
          //pass -ve sign to numerator
          frac.Numerator *= -1;
          frac.Denominator *= -1;
        }
      } // end try
      catch (System.Exception exp)
      {
        throw new Error("Cannot reduce Fraction: " + exp.Message);
      }
    }

    public class Error : Seven.Error
    {
      public Error(string Message) : base(Message) { }
    }

  }	//end class Fraction


  /// <summary>A fraction represented as two longs (numerator / denomnator).</summary>
  /// <citation>
  /// This fraction imlpementation was originally developed by 
  /// Syed Mehroz Alam and hosted as an open source project on 
  /// CodeProject.com. However, it has been modified since its
  /// addition into the Seven framework.
  /// http://www.codeproject.com/Articles/9078/Fraction-class-in-C
  /// 
  /// Original Author:
  /// Author: Syed Mehroz Alam
  /// Email: smehrozalam@yahoo.com
  /// URL: Programming Home http://www.geocities.com/smehrozalam/
  /// Date: 6/15/2004
  /// Time: 10:54 AM
  /// </citation>
  public struct fraction128
  {
    private long m_iNumerator;
    private long m_iDenominator;

    //public fraction128()
    //{
    //  m_iNumerator = 0;
    //  m_iDenominator = 1;
    //  ReduceFraction(this);
    //}

    public fraction128(long iWholeNumber)
    {
      m_iNumerator = iWholeNumber;
      m_iDenominator = 1;
      ReduceFraction(this);
    }

    public fraction128(double dDecimalValue)
    {
      fraction128 temp = ToFraction(dDecimalValue);
      m_iNumerator = temp.m_iNumerator;
      m_iDenominator = temp.m_iDenominator;
      ReduceFraction(this);
    }

    public fraction128(long numerator, long deniminator)
    {
      m_iNumerator = numerator;
      m_iDenominator = deniminator;
      ReduceFraction(this);
    }

    private void Initialize(long iNumerator, long iDenominator)
    {
      Numerator = iNumerator;
      Denominator = iDenominator;
      ReduceFraction(this);
    }

    public long Denominator
    {
      get
      { return m_iDenominator; }
      set
      {
        if (value != 0)
          m_iDenominator = value;
        else
          throw new Error("Denominator cannot be assigned a ZERO Value");
      }
    }

    public long Numerator
    {
      get
      { return m_iNumerator; }
      set
      { m_iNumerator = value; }
    }

    public long Value
    {
      set
      {
        m_iNumerator = value;
        m_iDenominator = 1;
      }
    }

    public double ToDouble()
    {
      return ((double)this.Numerator / this.Denominator);
    }

    public override string ToString()
    {
      string str;
      if (this.Denominator == 1)
        str = this.Numerator.ToString();
      else
        str = this.Numerator + "/" + this.Denominator;
      return str;
    }

    /// <summary>
    /// The function takes an string as an argument and returns its corresponding reduced fraction
    /// the string can be an in the form of and integer, double or fraction.
    /// e.g it can be like "123" or "123.321" or "123/456"
    /// </summary>
    public static fraction128 Parse(string strValue)
    {
      int i;
      for (i = 0; i < strValue.Length; i++)
        if (strValue[i] == '/')
          break;

      if (i == strValue.Length)		// if string is not in the form of a fraction
        // then it is double or integer
        return (System.Convert.ToDouble(strValue));
      //return ( ToFraction( Convert.ToDouble(strValue) ) );

      // else string is in the form of Numerator/Denominator
      long iNumerator = System.Convert.ToInt64(strValue.Substring(0, i));
      long iDenominator = System.Convert.ToInt64(strValue.Substring(i + 1));
      return new fraction128(iNumerator, iDenominator);
    }


    /// <summary>
    /// The function takes a floating point number as an argument 
    /// and returns its corresponding reduced fraction
    /// </summary>
    public static fraction128 ToFraction(double dValue)
    {
      try
      {
        checked
        {
          fraction128 frac;
          if (dValue % 1 == 0)	// if whole number
          {
            frac = new fraction128((long)dValue);
          }
          else
          {
            double dTemp = dValue;
            long iMultiple = 1;
            string strTemp = dValue.ToString();
            while (strTemp.IndexOf("E") > 0)	// if in the form like 12E-9
            {
              dTemp *= 10;
              iMultiple *= 10;
              strTemp = dTemp.ToString();
            }
            int i = 0;
            while (strTemp[i] != '.')
              i++;
            int iDigitsAfterDecimal = strTemp.Length - i - 1;
            while (iDigitsAfterDecimal > 0)
            {
              dTemp *= 10;
              iMultiple *= 10;
              iDigitsAfterDecimal--;
            }
            frac = new fraction128((int)System.Math.Round(dTemp), iMultiple);
          }
          return frac;
        }
      }
      catch (System.OverflowException)
      {
        throw new Error("Conversion not possible due to overflow");
      }
      catch (System.Exception)
      {
        throw new Error("Conversion not possible");
      }
    }

    /// <summary>
    /// The function replicates current Fraction object
    /// </summary>
    public fraction128 Duplicate()
    {
      fraction128 frac = new fraction128();
      frac.Numerator = Numerator;
      frac.Denominator = Denominator;
      return frac;
    }

    /// <summary>
    /// The function returns the inverse of a Fraction object
    /// </summary>
    public static fraction128 Inverse(fraction128 frac1)
    {
      if (frac1.Numerator == 0)
        throw new Error("Operation not possible (Denominator cannot be assigned a ZERO Value)");

      long iNumerator = frac1.Denominator;
      long iDenominator = frac1.Numerator;
      return (new fraction128(iNumerator, iDenominator));
    }


    /// <summary>
    /// Operators for the Fraction object
    /// includes -(unary), and binary opertors such as +,-,*,/
    /// also includes relational and logical operators such as ==,!=,<,>,<=,>=
    /// </summary>
    public static fraction128 operator -(fraction128 frac1)
    { return (Negate(frac1)); }

    public static fraction128 operator +(fraction128 frac1, fraction128 frac2)
    { return (Add(frac1, frac2)); }

    public static fraction128 operator +(int iNo, fraction128 frac1)
    { return (Add(frac1, new fraction128(iNo))); }

    public static fraction128 operator +(fraction128 frac1, int iNo)
    { return (Add(frac1, new fraction128(iNo))); }

    public static fraction128 operator +(double dbl, fraction128 frac1)
    { return (Add(frac1, fraction128.ToFraction(dbl))); }

    public static fraction128 operator +(fraction128 frac1, double dbl)
    { return (Add(frac1, fraction128.ToFraction(dbl))); }

    public static fraction128 operator -(fraction128 frac1, fraction128 frac2)
    { return (Add(frac1, -frac2)); }

    public static fraction128 operator -(int iNo, fraction128 frac1)
    { return (Add(-frac1, new fraction128(iNo))); }

    public static fraction128 operator -(fraction128 frac1, int iNo)
    { return (Add(frac1, -(new fraction128(iNo)))); }

    public static fraction128 operator -(double dbl, fraction128 frac1)
    { return (Add(-frac1, fraction128.ToFraction(dbl))); }

    public static fraction128 operator -(fraction128 frac1, double dbl)
    { return (Add(frac1, -fraction128.ToFraction(dbl))); }

    public static fraction128 operator *(fraction128 frac1, fraction128 frac2)
    { return (Multiply(frac1, frac2)); }

    public static fraction128 operator *(int iNo, fraction128 frac1)
    { return (Multiply(frac1, new fraction128(iNo))); }

    public static fraction128 operator *(fraction128 frac1, int iNo)
    { return (Multiply(frac1, new fraction128(iNo))); }

    public static fraction128 operator *(double dbl, fraction128 frac1)
    { return (Multiply(frac1, fraction128.ToFraction(dbl))); }

    public static fraction128 operator *(fraction128 frac1, double dbl)
    { return (Multiply(frac1, fraction128.ToFraction(dbl))); }

    public static fraction128 operator /(fraction128 frac1, fraction128 frac2)
    { return (Multiply(frac1, Inverse(frac2))); }

    public static fraction128 operator /(int iNo, fraction128 frac1)
    { return (Multiply(Inverse(frac1), new fraction128(iNo))); }

    public static fraction128 operator /(fraction128 frac1, int iNo)
    { return (Multiply(frac1, Inverse(new fraction128(iNo)))); }

    public static fraction128 operator /(double dbl, fraction128 frac1)
    { return (Multiply(Inverse(frac1), fraction128.ToFraction(dbl))); }

    public static fraction128 operator /(fraction128 frac1, double dbl)
    { return (Multiply(frac1, fraction128.Inverse(fraction128.ToFraction(dbl)))); }

    public static bool operator ==(fraction128 frac1, fraction128 frac2)
    { return frac1.Equals(frac2); }

    public static bool operator !=(fraction128 frac1, fraction128 frac2)
    { return (!frac1.Equals(frac2)); }

    public static bool operator ==(fraction128 frac1, int iNo)
    { return frac1.Equals(new fraction128(iNo)); }

    public static bool operator !=(fraction128 frac1, int iNo)
    { return (!frac1.Equals(new fraction128(iNo))); }

    public static bool operator ==(fraction128 frac1, double dbl)
    { return frac1.Equals(new fraction128(dbl)); }

    public static bool operator !=(fraction128 frac1, double dbl)
    { return (!frac1.Equals(new fraction128(dbl))); }

    public static bool operator <(fraction128 frac1, fraction128 frac2)
    { return frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator; }

    public static bool operator >(fraction128 frac1, fraction128 frac2)
    { return frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator; }

    public static bool operator <=(fraction128 frac1, fraction128 frac2)
    { return frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator; }

    public static bool operator >=(fraction128 frac1, fraction128 frac2)
    { return frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator; }


    /// <summary>Casts a long to a fraction128 (implicit).</summary>
    /// <param name="l">The long to be casted.</param>
    /// <returns></returns>
    public static implicit operator fraction128(long l)
    { return new fraction128(l); }
    public static implicit operator fraction128(double dNo)
    { return new fraction128(dNo); }

    /// <summary>
    /// overloaed user defined conversions: from fractions to double and string
    /// </summary>
    public static explicit operator double(fraction128 frac)
    { return frac.ToDouble(); }


    /// <summary>
    /// checks whether two fractions are equal
    /// </summary>
    public override bool Equals(object obj)
    {
      fraction128 frac = (fraction128)obj;
      return (Numerator == frac.Numerator && Denominator == frac.Denominator);
    }

    /// <summary>
    /// returns a hash code for this fraction
    /// </summary>
    public override int GetHashCode()
    {
      return (System.Convert.ToInt32((Numerator ^ Denominator) & 0xFFFFFFFF));
    }

    /// <summary>
    /// internal function for negation
    /// </summary>
    private static fraction128 Negate(fraction128 frac1)
    {
      long iNumerator = -frac1.Numerator;
      long iDenominator = frac1.Denominator;
      return (new fraction128(iNumerator, iDenominator));

    }

    /// <summary>
    /// internal functions for binary operations
    /// </summary>
    private static fraction128 Add(fraction128 frac1, fraction128 frac2)
    {
      try
      {
        checked
        {
          long iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
          long iDenominator = frac1.Denominator * frac2.Denominator;
          return (new fraction128(iNumerator, iDenominator));
        }
      }
      catch (System.OverflowException)
      {
        throw new Error("Overflow occurred while performing arithemetic operation");
      }
      catch (System.Exception)
      {
        throw new Error("An error occurred while performing arithemetic operation");
      }
    }

    private static fraction128 Multiply(fraction128 frac1, fraction128 frac2)
    {
      try
      {
        checked
        {
          long iNumerator = frac1.Numerator * frac2.Numerator;
          long iDenominator = frac1.Denominator * frac2.Denominator;
          return (new fraction128(iNumerator, iDenominator));
        }
      }
      catch (System.OverflowException)
      {
        throw new Error("Overflow occurred while performing arithemetic operation");
      }
      catch (System.Exception)
      {
        throw new Error("An error occurred while performing arithemetic operation");
      }
    }

    /// <summary>
    /// The function returns GCD of two numbers (used for reducing a Fraction)
    /// </summary>
    private static long GCD(long iNo1, long iNo2)
    {
      // take absolute values
      if (iNo1 < 0) iNo1 = -iNo1;
      if (iNo2 < 0) iNo2 = -iNo2;

      do
      {
        if (iNo1 < iNo2)
        {
          long tmp = iNo1;  // swap the two operands
          iNo1 = iNo2;
          iNo2 = tmp;
        }
        iNo1 = iNo1 % iNo2;
      } while (iNo1 != 0);
      return iNo2;
    }

    /// <summary>
    /// The function reduces(simplifies) a Fraction object by dividing both its numerator 
    /// and denominator by their GCD
    /// </summary>
    public static void ReduceFraction(fraction128 frac)
    {
      try
      {
        if (frac.Numerator == 0)
        {
          frac.Denominator = 1;
          return;
        }

        long iGCD = GCD(frac.Numerator, frac.Denominator);
        frac.Numerator /= iGCD;
        frac.Denominator /= iGCD;

        if (frac.Denominator < 0)	// if -ve sign in denominator
        {
          //pass -ve sign to numerator
          frac.Numerator *= -1;
          frac.Denominator *= -1;
        }
      } // end try
      catch (System.Exception exp)
      {
        throw new Error("Cannot reduce Fraction: " + exp.Message);
      }
    }

    public class Error : Seven.Error
    {
      public Error(string Message) : base(Message) { }
    }

  }	//end class Fraction

}	//end namespace Mehroz
