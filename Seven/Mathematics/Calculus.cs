using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven.Mathematics
{
  /// <summary>Branch of mathematics study of change.</summary>
  /// <typeparam name="T"></typeparam>
  public interface Calculus<T>
  {
  }

  /// <summary>Extendions for the Calculus interface.</summary>
  public static class Calculus
  {
  }

  public class Calculus_int : Calculus<int>
  {
  }
}
