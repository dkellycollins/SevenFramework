using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven.Mathematics
{
  public interface Calculator<T>
  {
    T Divide(T left, T right);
    T Multiply(T left, T right);
    T Add(T left, T right);
    T Negate(T left);
    T Subtract(T left, T right);
    T Power(T value, T power);
    T Square(T value, T square);
    T Absolute(T Value);
    T sin(T angle);
    T cos(T angle);
    T tan(T angle);
    T sec(T angle);
    T csc(T angle);
    T cot(T angle);
    T arcsin(T sinRatio);
    T arccos(T cosRatio);
    T arctan(T tanRatio);
    T arccsc(T cscRatio);
    T arcsec(T secRatio);
    T arccot(T cotRatio);
    T ln(T value);
    T log_10(T value);
    T e(T power);
    T Max(T left, T right);
    T Min(T left, T right);
    T Equate(T left, T right);
    T Equate(T left, T right, T leniency);
  }

  public static class Calculator
  {
    public static Calculator<int> _int(this int integer)
    {
      return (Calculator<int>)Calculator_int.Get;
    }
  }

  public class Calculator_int //: Calculator<int>
  {
    private Calculator_int() { _instance = this; }
    private static Calculator_int _instance;
    private static Calculator_int Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new Calculator_int();
        else
          return _instance;
      }
    }

    public static Calculator_int Get { get { return Instance; } }

    public int Divide(int left, int right) { return left / right; }
    public int Multiply(int left, int right) { return left * right; }
    public int Add(int left, int right) { return left + right; }
    public int Negate(int value) { return -value; }
    public int Subtract(int left, int right) { return left - right; }
    public int Power(int value, int power) { return (int)Math.Pow(value, power); }
    public int Square(int value, int square) { return (int)Math.Pow(value, 1 / square); }
    public int Absolute(int value) { return Math.Abs(value); }
    public int sin(int angle) { return (int)Math.Sin(angle); }
    public int cos(int angle) { return (int)Math.Cos(angle); }
    //public int tan(int angle);
    //public int sec(int angle);
    //public int csc(int angle);
    //public int cot(int angle);
    //public int arcsin(int sinRatio);
    //public int arccos(int cosRatio);
    //public int arctan(int tanRatio);
    //public int arccsc(int cscRatio);
    //public int arcsec(int secRatio);
    //public int arccot(int cotRatio);
    //public int ln(int value);
    //public int log_10(int value);
    //public int e(int power);
    //public int Max(int left, int right);
    //public int Min(int left, int right);
    //public int Equate(int left, int right);
    //public int Equate(int left, int right, int leniency);
  }
}
