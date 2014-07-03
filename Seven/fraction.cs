// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

// Citations:
// This fraction imlpementation was originally developed by 
// Syed Mehroz Alam and hosted as an open source project on 
// CodeProject.com. However, it has been modified since its
// addition into the Seven framework.
// http://www.codeproject.com/Articles/9078/Fraction-class-in-C

using System;

namespace Mehroz
{
  public class fraction
  {
    private long m_iNumerator;
    private long m_iDenominator;

    public fraction()
    {
      Initialize(0, 1);
    }

    public fraction(long iWholeNumber)
    {
      Initialize(iWholeNumber, 1);
    }

    public fraction(double dDecimalValue)
    {
      fraction temp = ToFraction(dDecimalValue);
      Initialize(temp.Numerator, temp.Denominator);
    }

    public fraction(string strValue)
    {
      fraction temp = ToFraction(strValue);
      Initialize(temp.Numerator, temp.Denominator);
    }

    public fraction(long iNumerator, long iDenominator)
    {
      Initialize(iNumerator, iDenominator);
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
          throw new FractionException("Denominator cannot be assigned a ZERO Value");
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
    public static fraction ToFraction(string strValue)
    {
      int i;
      for (i = 0; i < strValue.Length; i++)
        if (strValue[i] == '/')
          break;

      if (i == strValue.Length)		// if string is not in the form of a fraction
        // then it is double or integer
        return (Convert.ToDouble(strValue));
      //return ( ToFraction( Convert.ToDouble(strValue) ) );

      // else string is in the form of Numerator/Denominator
      long iNumerator = Convert.ToInt64(strValue.Substring(0, i));
      long iDenominator = Convert.ToInt64(strValue.Substring(i + 1));
      return new fraction(iNumerator, iDenominator);
    }


    /// <summary>
    /// The function takes a floating point number as an argument 
    /// and returns its corresponding reduced fraction
    /// </summary>
    public static fraction ToFraction(double dValue)
    {
      try
      {
        checked
        {
          fraction frac;
          if (dValue % 1 == 0)	// if whole number
          {
            frac = new fraction((long)dValue);
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
            frac = new fraction((int)Math.Round(dTemp), iMultiple);
          }
          return frac;
        }
      }
      catch (OverflowException)
      {
        throw new FractionException("Conversion not possible due to overflow");
      }
      catch (Exception)
      {
        throw new FractionException("Conversion not possible");
      }
    }

    /// <summary>
    /// The function replicates current Fraction object
    /// </summary>
    public fraction Duplicate()
    {
      fraction frac = new fraction();
      frac.Numerator = Numerator;
      frac.Denominator = Denominator;
      return frac;
    }

    /// <summary>
    /// The function returns the inverse of a Fraction object
    /// </summary>
    public static fraction Inverse(fraction frac1)
    {
      if (frac1.Numerator == 0)
        throw new FractionException("Operation not possible (Denominator cannot be assigned a ZERO Value)");

      long iNumerator = frac1.Denominator;
      long iDenominator = frac1.Numerator;
      return (new fraction(iNumerator, iDenominator));
    }


    /// <summary>
    /// Operators for the Fraction object
    /// includes -(unary), and binary opertors such as +,-,*,/
    /// also includes relational and logical operators such as ==,!=,<,>,<=,>=
    /// </summary>
    public static fraction operator -(fraction frac1)
    { return (Negate(frac1)); }

    public static fraction operator +(fraction frac1, fraction frac2)
    { return (Add(frac1, frac2)); }

    public static fraction operator +(int iNo, fraction frac1)
    { return (Add(frac1, new fraction(iNo))); }

    public static fraction operator +(fraction frac1, int iNo)
    { return (Add(frac1, new fraction(iNo))); }

    public static fraction operator +(double dbl, fraction frac1)
    { return (Add(frac1, fraction.ToFraction(dbl))); }

    public static fraction operator +(fraction frac1, double dbl)
    { return (Add(frac1, fraction.ToFraction(dbl))); }

    public static fraction operator -(fraction frac1, fraction frac2)
    { return (Add(frac1, -frac2)); }

    public static fraction operator -(int iNo, fraction frac1)
    { return (Add(-frac1, new fraction(iNo))); }

    public static fraction operator -(fraction frac1, int iNo)
    { return (Add(frac1, -(new fraction(iNo)))); }

    public static fraction operator -(double dbl, fraction frac1)
    { return (Add(-frac1, fraction.ToFraction(dbl))); }

    public static fraction operator -(fraction frac1, double dbl)
    { return (Add(frac1, -fraction.ToFraction(dbl))); }

    public static fraction operator *(fraction frac1, fraction frac2)
    { return (Multiply(frac1, frac2)); }

    public static fraction operator *(int iNo, fraction frac1)
    { return (Multiply(frac1, new fraction(iNo))); }

    public static fraction operator *(fraction frac1, int iNo)
    { return (Multiply(frac1, new fraction(iNo))); }

    public static fraction operator *(double dbl, fraction frac1)
    { return (Multiply(frac1, fraction.ToFraction(dbl))); }

    public static fraction operator *(fraction frac1, double dbl)
    { return (Multiply(frac1, fraction.ToFraction(dbl))); }

    public static fraction operator /(fraction frac1, fraction frac2)
    { return (Multiply(frac1, Inverse(frac2))); }

    public static fraction operator /(int iNo, fraction frac1)
    { return (Multiply(Inverse(frac1), new fraction(iNo))); }

    public static fraction operator /(fraction frac1, int iNo)
    { return (Multiply(frac1, Inverse(new fraction(iNo)))); }

    public static fraction operator /(double dbl, fraction frac1)
    { return (Multiply(Inverse(frac1), fraction.ToFraction(dbl))); }

    public static fraction operator /(fraction frac1, double dbl)
    { return (Multiply(frac1, fraction.Inverse(fraction.ToFraction(dbl)))); }

    public static bool operator ==(fraction frac1, fraction frac2)
    { return frac1.Equals(frac2); }

    public static bool operator !=(fraction frac1, fraction frac2)
    { return (!frac1.Equals(frac2)); }

    public static bool operator ==(fraction frac1, int iNo)
    { return frac1.Equals(new fraction(iNo)); }

    public static bool operator !=(fraction frac1, int iNo)
    { return (!frac1.Equals(new fraction(iNo))); }

    public static bool operator ==(fraction frac1, double dbl)
    { return frac1.Equals(new fraction(dbl)); }

    public static bool operator !=(fraction frac1, double dbl)
    { return (!frac1.Equals(new fraction(dbl))); }

    public static bool operator <(fraction frac1, fraction frac2)
    { return frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator; }

    public static bool operator >(fraction frac1, fraction frac2)
    { return frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator; }

    public static bool operator <=(fraction frac1, fraction frac2)
    { return frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator; }

    public static bool operator >=(fraction frac1, fraction frac2)
    { return frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator; }


    /// <summary>
    /// overloaed user defined conversions: from numeric data types to Fractions
    /// </summary>
    public static implicit operator fraction(long lNo)
    { return new fraction(lNo); }
    public static implicit operator fraction(double dNo)
    { return new fraction(dNo); }
    public static implicit operator fraction(string strNo)
    { return new fraction(strNo); }

    /// <summary>
    /// overloaed user defined conversions: from fractions to double and string
    /// </summary>
    public static explicit operator double(fraction frac)
    { return frac.ToDouble(); }

    public static implicit operator string(fraction frac)
    { return frac.ToString(); }

    /// <summary>
    /// checks whether two fractions are equal
    /// </summary>
    public override bool Equals(object obj)
    {
      fraction frac = (fraction)obj;
      return (Numerator == frac.Numerator && Denominator == frac.Denominator);
    }

    /// <summary>
    /// returns a hash code for this fraction
    /// </summary>
    public override int GetHashCode()
    {
      return (Convert.ToInt32((Numerator ^ Denominator) & 0xFFFFFFFF));
    }

    /// <summary>
    /// internal function for negation
    /// </summary>
    private static fraction Negate(fraction frac1)
    {
      long iNumerator = -frac1.Numerator;
      long iDenominator = frac1.Denominator;
      return (new fraction(iNumerator, iDenominator));

    }

    /// <summary>
    /// internal functions for binary operations
    /// </summary>
    private static fraction Add(fraction frac1, fraction frac2)
    {
      try
      {
        checked
        {
          long iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
          long iDenominator = frac1.Denominator * frac2.Denominator;
          return (new fraction(iNumerator, iDenominator));
        }
      }
      catch (OverflowException)
      {
        throw new FractionException("Overflow occurred while performing arithemetic operation");
      }
      catch (Exception)
      {
        throw new FractionException("An error occurred while performing arithemetic operation");
      }
    }

    private static fraction Multiply(fraction frac1, fraction frac2)
    {
      try
      {
        checked
        {
          long iNumerator = frac1.Numerator * frac2.Numerator;
          long iDenominator = frac1.Denominator * frac2.Denominator;
          return (new fraction(iNumerator, iDenominator));
        }
      }
      catch (OverflowException)
      {
        throw new FractionException("Overflow occurred while performing arithemetic operation");
      }
      catch (Exception)
      {
        throw new FractionException("An error occurred while performing arithemetic operation");
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
    public static void ReduceFraction(fraction frac)
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
      catch (Exception exp)
      {
        throw new FractionException("Cannot reduce Fraction: " + exp.Message);
      }
    }

  }	//end class Fraction


  /// <summary>
  /// Exception class for Fraction, derived from System.Exception
  /// </summary>
  public class FractionException : Exception
  {
    public FractionException()
      : base()
    { }

    public FractionException(string Message)
      : base(Message)
    { }

    public FractionException(string Message, Exception InnerException)
      : base(Message, InnerException)
    { }
  }	//end class FractionException


}	//end namespace Mehroz
