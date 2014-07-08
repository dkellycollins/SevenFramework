// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

namespace Seven.Mathematics
{
  /// <summary>A mathematical function that takes 0 parameters.</summary>
  /// <typeparam name="T">The type this function operates on.</typeparam>
  /// <returns>The value of the function given the parameters.</returns>
  public delegate T function_0<T>();

  /// <summary>A mathematical function that takes 1 parameters.</summary>
  /// <typeparam name="T">The type this function operates on.</typeparam>
  /// <param name="x">The first parameter of the function.</param>
  /// <returns>The value of the function given the parameters.</returns>
  public delegate T function_1<T>(T x);

  /// <summary>A mathematical function that takes 2 parameters.</summary>
  /// <typeparam name="T">The type this function operates on.</typeparam>
  /// <param name="x">The first parameter of the function.</param>
  /// <param name="y">The second parameter of the function.</param>
  /// <returns>The value of the function given the parameters.</returns>
  public delegate T function_2<T>(T x, T y);

  /// <summary>A mathematical function that takes 3 parameters.</summary>
  /// <typeparam name="T">The type this function operates on.</typeparam>
  /// <param name="x">The first parameter of the function.</param>
  /// <param name="y">The second parameter of the function.</param>
  /// <param name="z">The third parameter of the function.</param>
  /// <returns>The value of the function given the parameters.</returns>
  public delegate T function_3<T>(T x, T y, T z);

  /// <summary>A mathematical function that takes 4 parameters.</summary>
  /// <typeparam name="T">The type this function operates on.</typeparam>
  /// <param name="a">The first parameter of the function.</param>
  /// <param name="b">The second parameter of the function.</param>
  /// <param name="c">The third parameter of the function.</param>
  /// <param name="c">The fourth parameter of the function.</param>
  /// <returns>The value of the function given the parameters.</returns>
  public delegate T function_4<T>(T a, T b, T c, T d);

  public interface Function_0<T>
  {
    T this[T a] { get; }
  }


  
  ////public interface Function_xyz
  ////{
  ////  double this[double x, double y] { get; }
  ////}

  //public class Function_Delegate_xy : Function_xy
  //{
  //  private function_xy function;

  //  public double this[double arg] { get { return function(arg); } }

  //  public Function_Delegate_xy(function_xy func)
  //  {
  //    this.function = func;
  //  }

  //  /// <summary>Constructs functions from standard equation patterns.</summary>
  //  public static class Factory
  //  {
  //    /// <summary>Constructs a linear equation "y = a * x + b".</summary>
  //    /// <param name="a">The coeficient of x.</param>
  //    /// <param name="b">The constant.</param>
  //    /// <returns>The constructed linear equation.</returns>
  //    public static Function_Delegate_xy Linear(double a, double b)
  //    {
  //      return new Function_Delegate_xy((double x) => { return a * x + b; });
  //    }

  //    /// <summary>Constructs a quadratic equation "y = a * x ^ 2 + b * x + c".</summary>
  //    /// <param name="a">The coefficient of x ^ 2.</param>
  //    /// <param name="b">The coefficient of x.</param>
  //    /// <param name="c">The constant.</param>
  //    /// <returns>The constructed quadratic equation.</returns>
  //    public static Function_Delegate_xy Quadratic(double a, double b, double c)
  //    {
  //      return new Function_Delegate_xy((double x) => { return a * (x * x) + x * b + c; });
  //    }
  //  }

  //  /// <summary>Estimates the derives this function.</summary>
  //  /// <returns>The dirivation of this function.</returns>
  //  /// <remarks>WARNING. Calling this function often can cause memory overflow.</remarks>
  //  public Function_Delegate_xy Derive()
  //  {
  //    const double h = 10e-6;

  //    return new Function_Delegate_xy(
  //      (double x) =>
  //        {
  //          double h2 = h * 2;
  //          return
  //            (function(x - h2) - function(x + h2) +
  //            (function(x + h) - function(x - h)) * 8) /
  //            (h2 * 6);
  //        });
  //  }

  //  /// <summary>Derives this function at a given point.</summary>
  //  /// <param name="x">The point to calculate the derivitive of.</param>
  //  /// <returns>The dirivation of this function.</returns>
  //  public double Derive(double x)
  //  {
  //    const double h = 10e-6;

  //    double h2 = h * 2;

  //    return 
  //      (function(x - h2) - function(x + h2) +
  //      (function(x + h) - function(x - h)) * 8) /
  //      (h2 * 6);
          
  //  }
    
  //  //public Function_Delegate_xy calculate_definite_integral_of_f(double initial_step_size)
  //  //{
  //  //  // This algorithm calculates the definite integral of a function
  //  //  // from 0 to 1, adaptively, by choosing smaller steps near
  //  //  // problematic points.
  //  //  double x = 0.0;
  //  //  double h = initial_step_size;
  //  //  double accumulator = 0.0;
  //  //  while (x < 1.0)
  //  //  {
  //  //      if (x + h > 1.0)
  //  //          h = 1.0 - x;
  //  //      //quad_this_step =
  //  //      if (error_too_big_in_quadrature_of_over_range(f, [x,x+h]))
  //  //          h = make_h_smaller(h)
  //  //      else:
  //  //          accumulator += quadrature_of_f_over_range(f, [x,x+h])
  //  //          x += h
  //  //          if error_too_small_in_quadrature_of_over_range(f, [x,x+h]):
  //  //              h = make_h_larger(h) # Avoid wasting time on tiny steps.
  //  //  }
  //  //  return accumulator
  //  //}
  //}
}
