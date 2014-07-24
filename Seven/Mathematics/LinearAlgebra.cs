// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.txt" in th root project directory.
// SUPPORT: See "README.txt" in the root project directory.

using System;

namespace Seven.Mathematics
{
	/// <summary>Supplies linear algebra mathematics for generic types.</summary>
	/// <typeparam name="T">The type this linear algebra library can perform on.</typeparam>
	public interface LinearAlgebra<T>
	{
    /// <summary>Adds two vectors together.</summary>
    T[] Add(T[] left, T[] right);
    /// <summary>Negates all the values in a vector.</summary>
    T[] Negate(T[] vector);
    /// <summary>Subtracts two vectors.</summary>
    T[] Subtract(T[] left, T[] right);
    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    T[] Multiply(T[] left, T right);
    /// <summary>Divides all the components of a vector by a scalar.</summary>
    T[] Divide(T[] vector, T right);
    /// <summary>Computes the dot product between two vectors.</summary>
    T DotProduct(T[] left, T[] right);
    /// <summary>Computes teh cross product of two vectors.</summary>
    T[] CrossProduct(T[] left, T[] right);
    /// <summary>Normalizes a vector.</summary>
    T[] Normalize(T[] vector);
    /// <summary>Computes the length of a vector.</summary>
    T Magnitude(T[] vector);
    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    T MagnitudeSquared(T[] vector);
    /// <summary>Computes the angle between two vectors.</summary>
    T Angle(T[] first, T[] second);
    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    T[] RotateBy(T[] vector, T angle, T x, T y, T z);
    /// <summary>Computes the linear interpolation between two vectors.</summary>
    T[] Lerp(T[] left, T[] right, T blend);
    /// <summary>Sphereically interpolates between two vectors.</summary>
    T[] Slerp(T[] left, T[] right, T blend);
    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    T[] Blerp(T[] a, T[] b, T[] c, T u, T v);

		/// <summary>Negates all the values in this matrix.</summary>
		T[,] Negate(T[,] matrix);
		/// <summary>Does a standard matrix addition.</summary>
		T[,] Add(T[,] left, T[,] right);
		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
		T[,] Multiply(T[,] left, T[,] right);
		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
		T[,] Multiply(T[,] matrix, T right);
		/// <summary>Divides all the values in this matrix by a scalar.</summary>
		T[,] Divide(T[,] left, T right);
		/// <summary>Gets the minor of a matrix.</summary>
		T[,] Minor(T[,] matrix, int row, int column);
		/// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		T[,] ConcatenateRowWise(T[,] left, T[,] right);
		/// <summary>Computes the determinent if this matrix is square.</summary>
		T Determinent(T[,] matrix);
		/// <summary>Computes the echelon form of this matrix (aka REF).</summary>
		T[,] Echelon(T[,] matrix);
		/// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
		T[,] ReducedEchelon(T[,] matrix);
		/// <summary>Computes the inverse of this matrix.</summary>
		T[,] Inverse(T[,] matrix);
		/// <summary>Gets the adjoint of this matrix.</summary>
		T[,] Adjoint(T[,] matrix);
		/// <summary>Transposes this matrix.</summary>
		T[,] Transpose(T[,] matrix);
		/// <summary>Copies this matrix.</summary>
		T[,] Clone(T[,] matrix);
	}

	public static class LinearAlgebra
	{
    // Vector operations
    public delegate T[] _Vector_Add<T>(T[] left, T[] right);
    public delegate T[] _Vector_Negate<T>(T[] vector);
    public delegate T[] _Vector_Subtract<T>(T[] left, T[] right);
    public delegate T[] _Vector_Multiply<T>(T[] left, T right);
    public delegate T[] _Vector_Divide<T>(T[] vector, T right);
    public delegate T _Vector_DotProduct<T>(T[] left, T[] right);
    public delegate T[] _Vector_CrossProduct<T>(T[] left, T[] right);
    public delegate T[] _Vector_Normalize<T>(T[] vector);
    public delegate T _Vector_Magnitude<T>(T[] vector);
    public delegate T _Vector_MagnitudeSquared<T>(T[] vector);
    public delegate T _Vector_Angle<T>(T[] first, T[] second);
    public delegate T[] _Vector_RotateBy<T>(T[] vector, T angle, T x, T y, T z);
    public delegate T[] _Vector_Lerp<T>(T[] left, T[] right, T blend);
    public delegate T[] _Vector_Slerp<T>(T[] left, T[] right, T blend);
    public delegate T[] _Vector_Blerp<T>(T[] a, T[] b, T[] c, T u, T v);

    // Matrix operations
		public delegate T[,] _Matrix_Negate<T>(T[,] matrix);
		public delegate T[,] _Matrix_Add<T>(T[,] left, T[,] right);
		public delegate T[,] _Matrix_Multiply<T>(T[,] left, T[,] right);
		public delegate T[,] _Matrix_Multiply_Matrix_scalar<T>(T[,] matrix, T right);
		public delegate T[,] _Matrix_Divide<T>(T[,] left, T right);
		public delegate T[,] _Matrix_Minor<T>(T[,] matrix, int row, int column);
		public delegate T[,] _Matrix_ConcatenateRowWise<T>(T[,] left, T[,] right);
		public delegate T _Matrix_Determinent<T>(T[,] matrix);
		public delegate T[,] _Matrix_Echelon<T>(T[,] matrix);
		public delegate T[,] _Matrix_ReducedEchelon<T>(T[,] matrix);
		public delegate T[,] _Matrix_Inverse<T>(T[,] matrix);
		public delegate T[,] _Matrix_Adjoint<T>(T[,] matrix);
		public delegate T[,] _Matrix_Transpose<T>(T[,] matrix);
		public delegate T[,] _Matrix_Clone<T>(T[,] matrix);
    
		public static Seven.Structures.Map<object, System.Type> _linearAlgebras =
			new Seven.Structures.Map_Linked<object, System.Type>(
				(System.Type left, System.Type right) => { return left == right; },
				(System.Type type) => { return type.GetHashCode(); })
				{
					//{ typeof(int), LinearAlgebra_int.Get },
					{ typeof(double), LinearAlgebra_double.Get },
					{ typeof(float), LinearAlgebra_float.Get },
					{ typeof(decimal), LinearAlgebra_decimal.Get },
					//{ typeof(long), LinearAlgebra_long.Get }
				};

		public static LinearAlgebra<T> Get<T>()
		{
			try { return (LinearAlgebra<T>)_linearAlgebras[typeof(T)]; }
			catch { throw new Error("LinearAlgebra does not yet exist for " + typeof(T).ToString()); }
		}
    
		#region Implementations
    
    #region decimal

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static decimal[] Add(decimal[] left, decimal[] right)
    {
      if (left.Length != right.Length)
        throw new Error("invalid dimensions on vector addition.");
      decimal[] result = new decimal[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + right[i];
      return result;
    }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static decimal[] Negate(decimal[] vector)
    {
      decimal[] result = new decimal[vector.Length];
      for (int i = 0; i < vector.Length; i++)
        result[i] = -vector[i];
      return result;
    }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static decimal[] Subtract(decimal[] left, decimal[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector subtraction.");
      decimal[] result = new decimal[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] - right[i];
      return result;
    }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static decimal[] Multiply(decimal[] left, decimal right)
    {
      decimal[] result = new decimal[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] * right;
      return result;
    }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static decimal[] Divide(decimal[] vector, decimal right)
    {
      decimal[] result = new decimal[vector.Length];
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result[i] = vector[i] / right;
      return result;
    }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static decimal DotProduct(decimal[] left, decimal[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector dot product.");
      decimal result = 0;
      for (int i = 0; i < left.Length; i++)
        result += (left[i] * right[i]);
      return result;
    }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static decimal[] CrossProduct(decimal[] left, decimal[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid cross product !(left.Dimensions == right.Dimensions)");
      if (left.Length == 3 || left.Length == 4)
      {
        return new decimal[] {
          left[1] * right[2] - left[2] * right[1],
          left[2] * right[0] - left[0] * right[2],
          left[0] * right[1] - left[1] * right[0] };
      }
      throw new Exception("my cross product function is only defined for 3-component vectors.");
    }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static decimal[] Normalize(decimal[] vector)
    {
      decimal length = LinearAlgebra.Magnitude(vector);
      if (length != 0.0M)
      {
        decimal[] result = new decimal[vector.Length];
        for (int i = 0; i < vector.Length; i++)
          result[i] = vector[i] / length;
        return result;
      }
      else
        return new decimal[vector.Length];
    }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static decimal Magnitude(decimal[] vector)
    {
      decimal result = 0;
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result += (vector[i] * vector[i]);
      return (decimal)System.Math.Sqrt((double)result);
    }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static decimal MagnitudeSquared(decimal[] vector)
    {
      decimal result = 0;
      for (int i = 0; i < vector.Length; i++)
        result += (vector[i] * vector[i]);
      return result;
    }

    /// <summary>Computes the angle between two vectors.</summary>
    /// <param name="first">The first vector to determine the angle between.</param>
    /// <param name="second">The second vector to determine the angle between.</param>
    /// <returns>The angle between the two vectors in radians.</returns>
    public static decimal Angle(decimal[] first, decimal[] second)
    {
      return (decimal)System.Math.Acos((double)(LinearAlgebra.DotProduct(first, second) / (LinearAlgebra.Magnitude(first) * LinearAlgebra.Magnitude(second))));
    }

    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle of the rotation.</param>
    /// <param name="x">The x component of the axis vector to rotate about.</param>
    /// <param name="y">The y component of the axis vector to rotate about.</param>
    /// <param name="z">The z component of the axis vector to rotate about.</param>
    /// <returns>The result of the rotation.</returns>
    public static decimal[] RotateBy(decimal[] vector, decimal angle, decimal x, decimal y, decimal z)
    {
      if (vector.Length == 3)
      {
        // Note: the angle is in radians
        decimal sinHalfAngle = (decimal)System.Math.Sin((double)angle / 2d);
        decimal cosHalfAngle = (decimal)System.Math.Cos((double)angle / 2d);
        x *= sinHalfAngle;
        y *= sinHalfAngle;
        z *= sinHalfAngle;
        decimal x2 = cosHalfAngle * vector[0] + y * vector[2] - z * vector[1];
        decimal y2 = cosHalfAngle * vector[1] + z * vector[0] - x * vector[2];
        decimal z2 = cosHalfAngle * vector[2] + x * vector[1] - y * vector[0];
        decimal w2 = -x * vector[0] - y * vector[1] - z * vector[2];
        return new decimal[] {
          x * w2 + cosHalfAngle * x2 + y * z2 - z * y2,
          y * w2 + cosHalfAngle * y2 + z * x2 - x * z2,
          z * w2 + cosHalfAngle * z2 + x * y2 - y * x2 };
      }
      throw new Error("my RotateBy() function is only defined for 3-component vectors.");
    }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static decimal[] Lerp(decimal[] left, decimal[] right, decimal blend)
    {
      if (blend < 0 || blend > 1.0M)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      if (left.Length != right.Length)
        throw new Error("invalid lerp matrix length: (left.Dimensions != right.Dimensions)");
      decimal[] result = new decimal[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + blend * (right[i] - left[i]);
      return result;
    }

    /// <summary>Sphereically interpolates between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    /// <returns>The result of the slerp operation.</returns>
    public static decimal[] Slerp(decimal[] left, decimal[] right, decimal blend)
    {
      if (blend < 0 || blend > 1.0M)
        throw new Error("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
      decimal dot = LinearAlgebra.DotProduct(left, right);
      dot = dot < -1.0M ? -1.0M : dot;
      dot = dot > 1.0M ? 1.0M : dot;
      decimal theta = (decimal)System.Math.Acos((double)dot) * blend;
      return LinearAlgebra.Multiply(LinearAlgebra.Add(LinearAlgebra.Multiply(left, (decimal)System.Math.Cos((double)theta)),
        LinearAlgebra.Normalize(LinearAlgebra.Subtract(right, LinearAlgebra.Multiply(left, dot)))), (decimal)System.Math.Sin((double)theta));
    }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static decimal[] Blerp(decimal[] a, decimal[] b, decimal[] c, decimal u, decimal v)
    {
      return LinearAlgebra.Add(LinearAlgebra.Add(LinearAlgebra.Multiply(LinearAlgebra.Subtract(b, a), u),
        LinearAlgebra.Multiply(LinearAlgebra.Subtract(c, a), v)), a);
    }

    /// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed identity-matrix.</returns>
    public static decimal[,] MatrixFactoryIdentity_decimal(int rows, int columns)
    {
      decimal[,] matrix;
      try { matrix = new decimal[rows, columns]; }
      catch { throw new Error("invalid dimensions."); }
      if (rows <= columns)
        for (int i = 0; i < rows; i++)
          matrix[i, i] = 1;
      else
        for (int i = 0; i < columns; i++)
          matrix[i, i] = 1;
      return matrix;
    }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static decimal[,] Negate(decimal[,] matrix)
    {
      decimal[,] result = new decimal[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = -matrix[i, j];
      return result;
    }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static decimal[,] Add(decimal[,] left, decimal[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid addition (size miss-match).");
      decimal[,] result = new decimal[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          result[i, j] = left[i, j] + right[i, j];
      return result;
    }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static decimal[,] Subtract(decimal[,] left, decimal[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid subtraction (size miss-match).");
      decimal[,] result = new decimal[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          result[i, j] = left[i, j] - right[i, j];
      return result;
    }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static decimal[,] Multiply(decimal[,] left, decimal[,] right)
    {
      if (left.GetLength(1) != right.GetLength(0))
        throw new LinearAlgebra.Error("invalid multiplication (size miss-match).");
      decimal[,] result = new decimal[left.GetLength(0), right.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          for (int k = 0; k < left.GetLength(1); k++)
            result[i, j] += left[i, k] * right[k, j];
      return result;
    }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static decimal[,] Multiply(decimal[,] left, decimal right)
    {
      decimal[,] result = new decimal[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          result[i, j] = left[i, j] * right;
      return result;
    }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static decimal[,] Power(decimal[,] matrix, int power)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid power (!matrix.IsSquare).");
      if (!(power >= 0))
        throw new Error("invalid power !(power > -1)");
      if (power == 0)
        return LinearAlgebra.MatrixFactoryIdentity_decimal(matrix.GetLength(0), matrix.GetLength(1));
      decimal[,] result = matrix.Clone() as decimal[,];
      for (int i = 0; i < power; i++)
        result = LinearAlgebra.Multiply(result, result);
      return result;
    }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static decimal[,] Divide(decimal[,] matrix, decimal right)
    {
      decimal[,] result = new decimal[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j] / right;
      return result;
    }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static decimal[,] Minor(decimal[,] matrix, int row, int column)
    {
      decimal[,] minor = new decimal[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
      int m = 0, n = 0;
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        if (i == row) continue;
        n = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          if (j == column) continue;
          minor[m, n] = matrix[i, j];
          n++;
        }
        m++;
      }
      return minor;
    }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static decimal[,] ConcatenateRowWise(decimal[,] left, decimal[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0))
        throw new Error("invalid row-wise concatenation !(left.Rows == right.Rows).");
      decimal[,] result = new decimal[left.GetLength(0), left.GetLength(1) + right.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
        {
          if (j < left.GetLength(1)) result[i, j] = left[i, j];
          else result[i, j] = right[i, j - left.GetLength(1)];
        }
      return result;
    }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static decimal[,] Echelon(decimal[,] matrix)
    {
      try
      {
        decimal[,] result = matrix.Clone() as decimal[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] != 0)
                LinearAlgebra.SwapRows(result, i, j);
          if (result[i, i] == 0)
            continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] == 1)
                LinearAlgebra.SwapRows(result, i, j);
          LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.GetLength(0); j++)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new Error("echelon computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static decimal[,] ReducedEchelon(decimal[,] matrix)
    {
      try
      {
        decimal[,] result = matrix.Clone() as decimal[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] != 0)
                LinearAlgebra.SwapRows(result, i, j);
          if (result[i, i] == 0) continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] == 1)
                LinearAlgebra.SwapRows(result, i, j);
          LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.GetLength(0); j++)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
          for (int j = i - 1; j >= 0; j--)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new Error("reduced echelon calculation failed."); }
    }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static decimal Determinent(decimal[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid determinent !(matrix.IsSquare).");
      try
      {
        decimal det = 1.0M;
        decimal[,] rref = matrix.Clone() as decimal[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (rref[j, i] != 0)
              {
                LinearAlgebra.SwapRows(rref, i, j);
                det *= -1;
              }
          det *= rref[i, i];
          LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.GetLength(0); j++)
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          for (int j = i - 1; j >= 0; j--)
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
        }
        return det;
      }
      catch { throw new Error("determinent computation failed."); }
    }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static decimal[,] Inverse(decimal[,] matrix)
    {
      if (LinearAlgebra.Determinent(matrix) == 0)
        throw new Error("inverse calculation failed.");
      try
      {
        decimal[,] identity = LinearAlgebra.MatrixFactoryIdentity_decimal(matrix.GetLength(0), matrix.GetLength(1));
        decimal[,] rref = matrix.Clone() as decimal[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (rref[j, i] != 0)
              {
                LinearAlgebra.SwapRows(rref, i, j);
                LinearAlgebra.SwapRows(identity, i, j);
              }
          LinearAlgebra.RowMultiplication(identity, i, 1 / rref[i, i]);
          LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.GetLength(0); j++)
          {
            LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          }
          for (int j = i - 1; j >= 0; j--)
          {
            LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          }
        }
        return identity;
      }
      catch { throw new Error("inverse calculation failed."); }
    }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static decimal[,] Adjoint(decimal[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("Adjoint of a non-square matrix does not exists");
      decimal[,] AdjointMatrix = new decimal[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          if ((i + j) % 2 == 0)
            AdjointMatrix[i, j] = LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
          else
            AdjointMatrix[i, j] = -LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
      return LinearAlgebra.Transpose(AdjointMatrix);
    }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static decimal[,] Transpose(decimal[,] matrix)
    {
      decimal[,] transpose = new decimal[matrix.GetLength(1), matrix.GetLength(0)];
      for (int i = 0; i < transpose.GetLength(0); i++)
        for (int j = 0; j < transpose.GetLength(1); j++)
          transpose[i, j] = matrix[j, i];
      return transpose;
    }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="Lower">The computed lower triangular matrix.</param>
    /// <param name="Upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(decimal[,] matrix, out decimal[,] Lower, out decimal[,] Upper)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("The matrix is not square!");
      Lower = LinearAlgebra.MatrixFactoryIdentity_decimal(matrix.GetLength(0), matrix.GetLength(1));
      Upper = matrix.Clone() as decimal[,];
      int[] permutation = new int[matrix.GetLength(0)];
      for (int i = 0; i < matrix.GetLength(0); i++) permutation[i] = i;
      decimal p = 0, pom2, detOfP = 1;
      int k0 = 0, pom1 = 0;
      for (int k = 0; k < matrix.GetLength(1) - 1; k++)
      {
        p = 0;
        for (int i = k; i < matrix.GetLength(0); i++)
          if ((Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k]) > p)
          {
            p = Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k];
            k0 = i;
          }
        if (p == 0)
          throw new Error("The matrix is singular!");
        pom1 = permutation[k];
        permutation[k] = permutation[k0];
        permutation[k0] = pom1;
        for (int i = 0; i < k; i++)
        {
          pom2 = Lower[k, i];
          Lower[k, i] = Lower[k0, i];
          Lower[k0, i] = pom2;
        }
        if (k != k0)
          detOfP *= -1;
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
          pom2 = Upper[k, i];
          Upper[k, i] = Upper[k0, i];
          Upper[k0, i] = pom2;
        }
        for (int i = k + 1; i < matrix.GetLength(0); i++)
        {
          Lower[i, k] = Upper[i, k] / Upper[k, k];
          for (int j = k; j < matrix.GetLength(1); j++)
            Upper[i, j] = Upper[i, j] - Lower[i, k] * Upper[k, j];
        }
      }
    }

    /// <summary>Creates a copy of a matrix.</summary>
    /// <param name="matrix">The matrix to copy.</param>
    /// <returns>A copy of the matrix.</returns>
    public static decimal[,] Clone(decimal[,] matrix)
    {
      decimal[,] result = new decimal[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j];
      return result;
    }

    private static void RowMultiplication(decimal[,] matrix, int row, decimal scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[row, j] *= scalar;
    }

    private static void RowAddition(decimal[,] matrix, int target, int second, decimal scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[target, j] += (matrix[second, j] * scalar);
    }

    private static void SwapRows(decimal[,] matrix, int row1, int row2)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
      {
        decimal temp = matrix[row1, j];
        matrix[row1, j] = matrix[row2, j];
        matrix[row2, j] = temp;
      }
    }

    #endregion

		#region double

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static double[] Add(double[] left, double[] right)
    {
      if (left.Length != right.Length)
        throw new Error("invalid dimensions on vector addition.");
      double[] result = new double[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + right[i];
      return result;
    }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static double[] Negate(double[] vector)
    {
      double[] result = new double[vector.Length];
      for (int i = 0; i < vector.Length; i++)
        result[i] = -vector[i];
      return result;
    }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static double[] Subtract(double[] left, double[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector subtraction.");
      double[] result = new double[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] - right[i];
      return result;
    }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static double[] Multiply(double[] left, double right)
    {
      double[] result = new double[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] * right;
      return result;
    }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static double[] Divide(double[] vector, double right)
    {
      double[] result = new double[vector.Length];
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result[i] = vector[i] / right;
      return result;
    }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static double DotProduct(double[] left, double[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector dot product.");
      double result = 0;
      for (int i = 0; i < left.Length; i++)
        result += (left[i] * right[i]);
      return result;
    }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static double[] CrossProduct(double[] left, double[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid cross product !(left.Dimensions == right.Dimensions)");
      if (left.Length == 3 || left.Length == 4)
      {
        return new double[] {
          left[1] * right[2] - left[2] * right[1],
          left[2] * right[0] - left[0] * right[2],
          left[0] * right[1] - left[1] * right[0] };
      }
      throw new Exception("my cross product function is only defined for 3-component vectors.");
    }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static double[] Normalize(double[] vector)
    {
      double length = LinearAlgebra.Magnitude(vector);
      if (length != 0.0)
      {
        double[] result = new double[vector.Length];
        for (int i = 0; i < vector.Length; i++)
          result[i] = vector[i] / length;
        return result;
      }
      else
        return new double[vector.Length];
    }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static double Magnitude(double[] vector)
    {
      double result = 0;
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result += (vector[i] * vector[i]);
      return System.Math.Sqrt(result);
    }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static double MagnitudeSquared(double[] vector)
    {
      double result = 0;
      for (int i = 0; i < vector.Length; i++)
        result += (vector[i] * vector[i]);
      return result;
    }

    /// <summary>Computes the angle between two vectors.</summary>
    /// <param name="first">The first vector to determine the angle between.</param>
    /// <param name="second">The second vector to determine the angle between.</param>
    /// <returns>The angle between the two vectors in radians.</returns>
    public static double Angle(double[] first, double[] second)
    {
      return System.Math.Acos(LinearAlgebra.DotProduct(first, second) / (LinearAlgebra.Magnitude(first) * LinearAlgebra.Magnitude(second)));
    }

    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle of the rotation.</param>
    /// <param name="x">The x component of the axis vector to rotate about.</param>
    /// <param name="y">The y component of the axis vector to rotate about.</param>
    /// <param name="z">The z component of the axis vector to rotate about.</param>
    /// <returns>The result of the rotation.</returns>
    public static double[] RotateBy(double[] vector, double angle, double x, double y, double z)
    {
      if (vector.Length == 3)
      {
        // Note: the angle is in radians
        double sinHalfAngle = System.Math.Sin(angle / 2);
        double cosHalfAngle = System.Math.Cos(angle / 2);
        x *= sinHalfAngle;
        y *= sinHalfAngle;
        z *= sinHalfAngle;
        double x2 = cosHalfAngle * vector[0] + y * vector[2] - z * vector[1];
        double y2 = cosHalfAngle * vector[1] + z * vector[0] - x * vector[2];
        double z2 = cosHalfAngle * vector[2] + x * vector[1] - y * vector[0];
        double w2 = -x * vector[0] - y * vector[1] - z * vector[2];
        return new double[] {
          x * w2 + cosHalfAngle * x2 + y * z2 - z * y2,
          y * w2 + cosHalfAngle * y2 + z * x2 - x * z2,
          z * w2 + cosHalfAngle * z2 + x * y2 - y * x2 };
      }
      throw new Error("my RotateBy() function is only defined for 3-component vectors.");
    }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static double[] Lerp(double[] left, double[] right, double blend)
    {
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      if (left.Length != right.Length)
        throw new Error("invalid lerp matrix length: (left.Dimensions != right.Dimensions)");
      double[] result = new double[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + blend * (right[i] - left[i]);
      return result;
    }

    /// <summary>Sphereically interpolates between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    /// <returns>The result of the slerp operation.</returns>
    public static double[] Slerp(double[] left, double[] right, double blend)
    {
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
      double dot = LinearAlgebra.DotProduct(left, right);
      dot = dot < -1.0d ? -1.0d : dot;
      dot = dot > 1.0d ? 1.0 : dot;
      double theta = System.Math.Acos(dot) * blend;
      return LinearAlgebra.Multiply(LinearAlgebra.Add(LinearAlgebra.Multiply(left, System.Math.Cos(theta)), 
        LinearAlgebra.Normalize(LinearAlgebra.Subtract(right, LinearAlgebra.Multiply(left, dot)))), System.Math.Sin(theta));
    }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static double[] Blerp(double[] a, double[] b, double[] c, double u, double v)
    {
      return LinearAlgebra.Add(LinearAlgebra.Add(LinearAlgebra.Multiply(LinearAlgebra.Subtract(b, a), u), 
        LinearAlgebra.Multiply(LinearAlgebra.Subtract(c, a), v)), a);
    }

    /// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static double[,] MatrixFactoryIdentity_double(int rows, int columns)
		{
			double[,] matrix;
			try { matrix = new double[rows, columns]; }
			catch { throw new Error("invalid dimensions."); }
			if (rows <= columns)
				for (int i = 0; i < rows; i++)
					matrix[i, i] = 1;
			else
				for (int i = 0; i < columns; i++)
					matrix[i, i] = 1;
			return matrix;
		}

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="matrix">The matrix to have its values negated.</param>
		/// <returns>The resulting matrix after the negations.</returns>
		public static double[,] Negate(double[,] matrix)
		{
			double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
			for (int i = 0; i < matrix.GetLength(0); i++)
				for (int j = 0; j < matrix.GetLength(1); j++)
					result[i, j] = -matrix[i, j];
			return result;
		}

		/// <summary>Does standard addition of two matrices.</summary>
		/// <param name="left">The left matrix of the addition.</param>
		/// <param name="right">The right matrix of the addition.</param>
		/// <returns>The resulting matrix after the addition.</returns>
		public static double[,] Add(double[,] left, double[,] right)
		{
			if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
				throw new Error("invalid addition (size miss-match).");
			double[,] result = new double[left.GetLength(0), left.GetLength(1)];
			for (int i = 0; i < result.GetLength(0); i++)
				for (int j = 0; j < result.GetLength(1); j++)
					result[i, j] = left[i, j] + right[i, j];
			return result;
		}

		/// <summary>Subtracts a scalar from all the values in a matrix.</summary>
		/// <param name="left">The matrix to have the values subtracted from.</param>
		/// <param name="right">The scalar to subtract from all the matrix values.</param>
		/// <returns>The resulting matrix after the subtractions.</returns>
		public static double[,] Subtract(double[,] left, double[,] right)
		{
			if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
				throw new Error("invalid subtraction (size miss-match).");
			double[,] result = new double[left.GetLength(0), left.GetLength(1)];
			for (int i = 0; i < result.GetLength(0); i++)
				for (int j = 0; j < result.GetLength(1); j++)
					result[i, j] = left[i, j] - right[i, j];
			return result;
		}

		/// <summary>Performs multiplication on two matrices.</summary>
		/// <param name="left">The left matrix of the multiplication.</param>
		/// <param name="right">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix of the multiplication.</returns>
		public static double[,] Multiply(double[,] left, double[,] right)
		{
			if (left.GetLength(1) != right.GetLength(0))
				throw new LinearAlgebra.Error("invalid multiplication (size miss-match).");
			double[,] result = new double[left.GetLength(0), right.GetLength(1)];
			for (int i = 0; i < result.GetLength(0); i++)
				for (int j = 0; j < result.GetLength(1); j++)
					for (int k = 0; k < left.GetLength(1); k++)
						result[i, j] += left[i, k] * right[k, j];
			return result;
		}

		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="left">The matrix to have the values multiplied.</param>
		/// <param name="right">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static double[,] Multiply(double[,] left, double right)
		{
			double[,] result = new double[left.GetLength(0), left.GetLength(1)];
			for (int i = 0; i < left.GetLength(0); i++)
				for (int j = 0; j < left.GetLength(1); j++)
					result[i, j] = left[i, j] * right;
			return result;
		}

		/// <summary>Applies a power to a square matrix.</summary>
		/// <param name="matrix">The matrix to be powered by.</param>
		/// <param name="power">The power to apply to the matrix.</param>
		/// <returns>The resulting matrix of the power operation.</returns>
		public static double[,] Power(double[,] matrix, int power)
		{
			if (!(matrix.GetLength(0) == matrix.GetLength(1)))
				throw new Error("invalid power (!matrix.IsSquare).");
			if (!(power >= 0))
				throw new Error("invalid power !(power > -1)");
			if (power == 0)
				return LinearAlgebra.MatrixFactoryIdentity_double(matrix.GetLength(0), matrix.GetLength(1));
			double[,] result = matrix.Clone() as double[,];
			for (int i = 0; i < power; i++)
				result = LinearAlgebra.Multiply(result, result);
			return result;
		}

		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		/// <param name="matrix">The matrix to divide the values of.</param>
		/// <param name="right">The scalar to divide all the matrix values by.</param>
		/// <returns>The resulting matrix with the divided values.</returns>
		public static double[,] Divide(double[,] matrix, double right)
		{
			double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
			for (int i = 0; i < matrix.GetLength(0); i++)
				for (int j = 0; j < matrix.GetLength(1); j++)
					result[i, j] = matrix[i, j] / right;
			return result;
		}

		/// <summary>Gets the minor of a matrix.</summary>
		/// <param name="matrix">The matrix to get the minor of.</param>
		/// <param name="row">The restricted row to form the minor.</param>
		/// <param name="column">The restricted column to form the minor.</param>
		/// <returns>The minor of the matrix.</returns>
		public static double[,] Minor(double[,] matrix, int row, int column)
		{
			double[,] minor = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
			int m = 0, n = 0;
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (i == row) continue;
				n = 0;
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (j == column) continue;
					minor[m, n] = matrix[i, j];
					n++;
				}
				m++;
			}
			return minor;
		}

		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="left">The left matrix of the concatenation.</param>
		/// <param name="right">The right matrix of the concatenation.</param>
		/// <returns>The resulting matrix of the concatenation.</returns>
		public static double[,] ConcatenateRowWise(double[,] left, double[,] right)
		{
			if (left.GetLength(0) != right.GetLength(0))
				throw new Error("invalid row-wise concatenation !(left.Rows == right.Rows).");
			double[,] result = new double[left.GetLength(0), left.GetLength(1) + right.GetLength(1)];
			for (int i = 0; i < result.GetLength(0); i++)
				for (int j = 0; j < result.GetLength(1); j++)
				{
					if (j < left.GetLength(1)) result[i, j] = left[i, j];
					else result[i, j] = right[i, j - left.GetLength(1)];
				}
			return result;
		}

		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
		/// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
		/// <returns>The echelon of the matrix (aka REF).</returns>
		public static double[,] Echelon(double[,] matrix)
		{
			try
			{
				double[,] result = matrix.Clone() as double[,];
				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					if (result[i, i] == 0)
						for (int j = i + 1; j < result.GetLength(0); j++)
							if (result[j, i] != 0)
								LinearAlgebra.SwapRows(result, i, j);
					if (result[i, i] == 0)
						continue;
					if (result[i, i] != 1)
						for (int j = i + 1; j < result.GetLength(0); j++)
							if (result[j, i] == 1)
								LinearAlgebra.SwapRows(result, i, j);
					LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
					for (int j = i + 1; j < result.GetLength(0); j++)
						LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
				}
				return result;
			}
			catch { throw new Error("echelon computation failed."); }
		}

		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
		/// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
		/// <returns>The reduced echelon of the matrix (aka RREF).</returns>
		public static double[,] ReducedEchelon(double[,] matrix)
		{
			try
			{
				double[,] result = matrix.Clone() as double[,];
				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					if (result[i, i] == 0)
						for (int j = i + 1; j < result.GetLength(0); j++)
							if (result[j, i] != 0)
								LinearAlgebra.SwapRows(result, i, j);
					if (result[i, i] == 0) continue;
					if (result[i, i] != 1)
						for (int j = i + 1; j < result.GetLength(0); j++)
							if (result[j, i] == 1)
								LinearAlgebra.SwapRows(result, i, j);
					LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
					for (int j = i + 1; j < result.GetLength(0); j++)
						LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
					for (int j = i - 1; j >= 0; j--)
						LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
				}
				return result;
			}
			catch { throw new Error("reduced echelon calculation failed."); }
		}

		/// <summary>Calculates the determinent of a square matrix.</summary>
		/// <param name="matrix">The matrix to calculate the determinent of.</param>
		/// <returns>The determinent of the matrix.</returns>
		public static double Determinent(double[,] matrix)
		{
			if (!(matrix.GetLength(0) == matrix.GetLength(1)))
				throw new Error("invalid determinent !(matrix.IsSquare).");
			try
			{
				double det = 1.0f;
				double[,] rref = matrix.Clone() as double[,];
				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					if (rref[i, i] == 0)
						for (int j = i + 1; j < rref.GetLength(0); j++)
							if (rref[j, i] != 0)
							{
								LinearAlgebra.SwapRows(rref, i, j);
								det *= -1;
							}
					det *= rref[i, i];
					LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
					for (int j = i + 1; j < rref.GetLength(0); j++)
						LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
					for (int j = i - 1; j >= 0; j--)
						LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
				}
				return det;
			}
			catch { throw new Error("determinent computation failed."); }
		}

		/// <summary>Calculates the inverse of a matrix.</summary>
		/// <param name="matrix">The matrix to calculate the inverse of.</param>
		/// <returns>The inverse of the matrix.</returns>
		public static double[,] Inverse(double[,] matrix)
		{
			if (LinearAlgebra.Determinent(matrix) == 0)
				throw new Error("inverse calculation failed.");
			try
			{
				double[,] identity = LinearAlgebra.MatrixFactoryIdentity_double(matrix.GetLength(0), matrix.GetLength(1));
				double[,] rref = matrix.Clone() as double[,];
				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					if (rref[i, i] == 0)
						for (int j = i + 1; j < rref.GetLength(0); j++)
							if (rref[j, i] != 0)
							{
								LinearAlgebra.SwapRows(rref, i, j);
								LinearAlgebra.SwapRows(identity, i, j);
							}
					LinearAlgebra.RowMultiplication(identity, i, 1 / rref[i, i]);
					LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
					for (int j = i + 1; j < rref.GetLength(0); j++)
					{
						LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
						LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
					}
					for (int j = i - 1; j >= 0; j--)
					{
						LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
						LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
					}
				}
				return identity;
			}
			catch { throw new Error("inverse calculation failed."); }
		}

		/// <summary>Calculates the adjoint of a matrix.</summary>
		/// <param name="matrix">The matrix to calculate the adjoint of.</param>
		/// <returns>The adjoint of the matrix.</returns>
		public static double[,] Adjoint(double[,] matrix)
		{
			if (!(matrix.GetLength(0) == matrix.GetLength(1)))
				throw new Error("Adjoint of a non-square matrix does not exists");
			double[,] AdjointMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
			for (int i = 0; i < matrix.GetLength(0); i++)
				for (int j = 0; j < matrix.GetLength(1); j++)
					if ((i + j) % 2 == 0)
						AdjointMatrix[i, j] = LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
					else
						AdjointMatrix[i, j] = -LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
			return LinearAlgebra.Transpose(AdjointMatrix);
		}

		/// <summary>Returns the transpose of a matrix.</summary>
		/// <param name="matrix">The matrix to transpose.</param>
		/// <returns>The transpose of the matrix.</returns>
		public static double[,] Transpose(double[,] matrix)
		{
			double[,] transpose = new double[matrix.GetLength(1), matrix.GetLength(0)];
			for (int i = 0; i < transpose.GetLength(0); i++)
				for (int j = 0; j < transpose.GetLength(1); j++)
					transpose[i, j] = matrix[j, i];
			return transpose;
		}

		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
		/// <param name="matrix">The matrix to decompose.</param>
		/// <param name="Lower">The computed lower triangular matrix.</param>
		/// <param name="Upper">The computed upper triangular matrix.</param>
		public static void DecomposeLU(double[,] matrix, out double[,] Lower, out double[,] Upper)
		{
			if (!(matrix.GetLength(0) == matrix.GetLength(1)))
				throw new Error("The matrix is not square!");
			Lower = LinearAlgebra.MatrixFactoryIdentity_double(matrix.GetLength(0), matrix.GetLength(1));
			Upper = matrix.Clone() as double[,];
			int[] permutation = new int[matrix.GetLength(0)];
			for (int i = 0; i < matrix.GetLength(0); i++) permutation[i] = i;
			double p = 0, pom2, detOfP = 1;
			int k0 = 0, pom1 = 0;
			for (int k = 0; k < matrix.GetLength(1) - 1; k++)
			{
				p = 0;
				for (int i = k; i < matrix.GetLength(0); i++)
					if ((Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k]) > p)
					{
						p = Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k];
						k0 = i;
					}
				if (p == 0)
					throw new Error("The matrix is singular!");
				pom1 = permutation[k];
				permutation[k] = permutation[k0];
				permutation[k0] = pom1;
				for (int i = 0; i < k; i++)
				{
					pom2 = Lower[k, i];
					Lower[k, i] = Lower[k0, i];
					Lower[k0, i] = pom2;
				}
				if (k != k0)
					detOfP *= -1;
				for (int i = 0; i < matrix.GetLength(1); i++)
				{
					pom2 = Upper[k, i];
					Upper[k, i] = Upper[k0, i];
					Upper[k0, i] = pom2;
				}
				for (int i = k + 1; i < matrix.GetLength(0); i++)
				{
					Lower[i, k] = Upper[i, k] / Upper[k, k];
					for (int j = k; j < matrix.GetLength(1); j++)
						Upper[i, j] = Upper[i, j] - Lower[i, k] * Upper[k, j];
				}
			}
		}

		/// <summary>Creates a copy of a matrix.</summary>
		/// <param name="matrix">The matrix to copy.</param>
		/// <returns>A copy of the matrix.</returns>
		public static double[,] Clone(double[,] matrix)
		{
			double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
			for (int i = 0; i < matrix.GetLength(0); i++)
				for (int j = 0; j < matrix.GetLength(1); j++)
					result[i, j] = matrix[i, j];
			return result;
		}

		private static void RowMultiplication(double[,] matrix, int row, double scalar)
		{
			for (int j = 0; j < matrix.GetLength(1); j++)
				matrix[row, j] *= scalar;
		}

		private static void RowAddition(double[,] matrix, int target, int second, double scalar)
		{
			for (int j = 0; j < matrix.GetLength(1); j++)
				matrix[target, j] += (matrix[second, j] * scalar);
		}

		private static void SwapRows(double[,] matrix, int row1, int row2)
		{
			for (int j = 0; j < matrix.GetLength(1); j++)
			{
				double temp = matrix[row1, j];
				matrix[row1, j] = matrix[row2, j];
				matrix[row2, j] = temp;
			}
		}

		#endregion

    #region float

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static float[] Add(float[] left, float[] right)
    {
      if (left.Length != right.Length)
        throw new Error("invalid dimensions on vector addition.");
      float[] result = new float[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + right[i];
      return result;
    }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static float[] Negate(float[] vector)
    {
      float[] result = new float[vector.Length];
      for (int i = 0; i < vector.Length; i++)
        result[i] = -vector[i];
      return result;
    }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static float[] Subtract(float[] left, float[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector subtraction.");
      float[] result = new float[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] - right[i];
      return result;
    }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static float[] Multiply(float[] left, float right)
    {
      float[] result = new float[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] * right;
      return result;
    }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static float[] Divide(float[] vector, float right)
    {
      float[] result = new float[vector.Length];
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result[i] = vector[i] / right;
      return result;
    }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static float DotProduct(float[] left, float[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector dot product.");
      float result = 0;
      for (int i = 0; i < left.Length; i++)
        result += (left[i] * right[i]);
      return result;
    }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static float[] CrossProduct(float[] left, float[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid cross product !(left.Dimensions == right.Dimensions)");
      if (left.Length == 3 || left.Length == 4)
      {
        return new float[] {
          left[1] * right[2] - left[2] * right[1],
          left[2] * right[0] - left[0] * right[2],
          left[0] * right[1] - left[1] * right[0] };
      }
      throw new Exception("my cross product function is only defined for 3-component vectors.");
    }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static float[] Normalize(float[] vector)
    {
      float length = LinearAlgebra.Magnitude(vector);
      if (length != 0.0)
      {
        float[] result = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
          result[i] = vector[i] / length;
        return result;
      }
      else
        return new float[vector.Length];
    }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static float Magnitude(float[] vector)
    {
      float result = 0;
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result += (vector[i] * vector[i]);
      return (float)System.Math.Sqrt(result);
    }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static float MagnitudeSquared(float[] vector)
    {
      float result = 0;
      for (int i = 0; i < vector.Length; i++)
        result += (vector[i] * vector[i]);
      return result;
    }

    /// <summary>Computes the angle between two vectors.</summary>
    /// <param name="first">The first vector to determine the angle between.</param>
    /// <param name="second">The second vector to determine the angle between.</param>
    /// <returns>The angle between the two vectors in radians.</returns>
    public static float Angle(float[] first, float[] second)
    {
      return (float)System.Math.Acos(LinearAlgebra.DotProduct(first, second) / (LinearAlgebra.Magnitude(first) * LinearAlgebra.Magnitude(second)));
    }

    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle of the rotation.</param>
    /// <param name="x">The x component of the axis vector to rotate about.</param>
    /// <param name="y">The y component of the axis vector to rotate about.</param>
    /// <param name="z">The z component of the axis vector to rotate about.</param>
    /// <returns>The result of the rotation.</returns>
    public static float[] RotateBy(float[] vector, float angle, float x, float y, float z)
    {
      if (vector.Length == 3)
      {
        // Note: the angle is in radians
        float sinHalfAngle = (float)System.Math.Sin(angle / 2);
        float cosHalfAngle = (float)System.Math.Cos(angle / 2);
        x *= sinHalfAngle;
        y *= sinHalfAngle;
        z *= sinHalfAngle;
        float x2 = cosHalfAngle * vector[0] + y * vector[2] - z * vector[1];
        float y2 = cosHalfAngle * vector[1] + z * vector[0] - x * vector[2];
        float z2 = cosHalfAngle * vector[2] + x * vector[1] - y * vector[0];
        float w2 = -x * vector[0] - y * vector[1] - z * vector[2];
        return new float[] {
          x * w2 + cosHalfAngle * x2 + y * z2 - z * y2,
          y * w2 + cosHalfAngle * y2 + z * x2 - x * z2,
          z * w2 + cosHalfAngle * z2 + x * y2 - y * x2 };
      }
      throw new Error("my RotateBy() function is only defined for 3-component vectors.");
    }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static float[] Lerp(float[] left, float[] right, float blend)
    {
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      if (left.Length != right.Length)
        throw new Error("invalid lerp matrix length: (left.Dimensions != right.Dimensions)");
      float[] result = new float[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + blend * (right[i] - left[i]);
      return result;
    }

    /// <summary>Sphereically interpolates between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    /// <returns>The result of the slerp operation.</returns>
    public static float[] Slerp(float[] left, float[] right, float blend)
    {
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
      float dot = LinearAlgebra.DotProduct(left, right);
      dot = dot < -1.0f ? -1.0f : dot;
      dot = dot > 1.0f ? 1.0f : dot;
      float theta = (float)System.Math.Acos(dot) * blend;
      return LinearAlgebra.Multiply(LinearAlgebra.Add(LinearAlgebra.Multiply(left, (float)System.Math.Cos(theta)),
        LinearAlgebra.Normalize(LinearAlgebra.Subtract(right, LinearAlgebra.Multiply(left, dot)))), (float)System.Math.Sin(theta));
    }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static float[] Blerp(float[] a, float[] b, float[] c, float u, float v)
    {
      return LinearAlgebra.Add(LinearAlgebra.Add(LinearAlgebra.Multiply(LinearAlgebra.Subtract(b, a), u),
        LinearAlgebra.Multiply(LinearAlgebra.Subtract(c, a), v)), a);
    }

    /// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed identity-matrix.</returns>
    public static float[,] MatrixFactoryIdentity_float(int rows, int columns)
    {
      float[,] matrix;
      try { matrix = new float[rows, columns]; }
      catch { throw new Error("invalid dimensions."); }
      if (rows <= columns)
        for (int i = 0; i < rows; i++)
          matrix[i, i] = 1;
      else
        for (int i = 0; i < columns; i++)
          matrix[i, i] = 1;
      return matrix;
    }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static float[,] Negate(float[,] matrix)
    {
      float[,] result = new float[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = -matrix[i, j];
      return result;
    }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static float[,] Add(float[,] left, float[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid addition (size miss-match).");
      float[,] result = new float[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          result[i, j] = left[i, j] + right[i, j];
      return result;
    }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static float[,] Subtract(float[,] left, float[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid subtraction (size miss-match).");
      float[,] result = new float[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          result[i, j] = left[i, j] - right[i, j];
      return result;
    }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static float[,] Multiply(float[,] left, float[,] right)
    {
      if (left.GetLength(1) != right.GetLength(0))
        throw new LinearAlgebra.Error("invalid multiplication (size miss-match).");
      float[,] result = new float[left.GetLength(0), right.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          for (int k = 0; k < left.GetLength(1); k++)
            result[i, j] += left[i, k] * right[k, j];
      return result;
    }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static float[,] Multiply(float[,] left, float right)
    {
      float[,] result = new float[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          result[i, j] = left[i, j] * right;
      return result;
    }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static float[,] Power(float[,] matrix, int power)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid power (!matrix.IsSquare).");
      if (!(power >= 0))
        throw new Error("invalid power !(power > -1)");
      if (power == 0)
        return LinearAlgebra.MatrixFactoryIdentity_float(matrix.GetLength(0), matrix.GetLength(1));
      float[,] result = matrix.Clone() as float[,];
      for (int i = 0; i < power; i++)
        result = LinearAlgebra.Multiply(result, result);
      return result;
    }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static float[,] Divide(float[,] matrix, float right)
    {
      float[,] result = new float[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j] / right;
      return result;
    }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static float[,] Minor(float[,] matrix, int row, int column)
    {
      float[,] minor = new float[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
      int m = 0, n = 0;
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        if (i == row) continue;
        n = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          if (j == column) continue;
          minor[m, n] = matrix[i, j];
          n++;
        }
        m++;
      }
      return minor;
    }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static float[,] ConcatenateRowWise(float[,] left, float[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0))
        throw new Error("invalid row-wise concatenation !(left.Rows == right.Rows).");
      float[,] result = new float[left.GetLength(0), left.GetLength(1) + right.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
        {
          if (j < left.GetLength(1)) result[i, j] = left[i, j];
          else result[i, j] = right[i, j - left.GetLength(1)];
        }
      return result;
    }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static float[,] Echelon(float[,] matrix)
    {
      try
      {
        float[,] result = matrix.Clone() as float[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] != 0)
                LinearAlgebra.SwapRows(result, i, j);
          if (result[i, i] == 0)
            continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] == 1)
                LinearAlgebra.SwapRows(result, i, j);
          LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.GetLength(0); j++)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new Error("echelon computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static float[,] ReducedEchelon(float[,] matrix)
    {
      try
      {
        float[,] result = matrix.Clone() as float[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] != 0)
                LinearAlgebra.SwapRows(result, i, j);
          if (result[i, i] == 0) continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] == 1)
                LinearAlgebra.SwapRows(result, i, j);
          LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.GetLength(0); j++)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
          for (int j = i - 1; j >= 0; j--)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new Error("reduced echelon calculation failed."); }
    }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static float Determinent(float[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid determinent !(matrix.IsSquare).");
      try
      {
        float det = 1.0f;
        float[,] rref = matrix.Clone() as float[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (rref[j, i] != 0)
              {
                LinearAlgebra.SwapRows(rref, i, j);
                det *= -1;
              }
          det *= rref[i, i];
          LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.GetLength(0); j++)
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          for (int j = i - 1; j >= 0; j--)
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
        }
        return det;
      }
      catch { throw new Error("determinent computation failed."); }
    }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static float[,] Inverse(float[,] matrix)
    {
      if (LinearAlgebra.Determinent(matrix) == 0)
        throw new Error("inverse calculation failed.");
      try
      {
        float[,] identity = LinearAlgebra.MatrixFactoryIdentity_float(matrix.GetLength(0), matrix.GetLength(1));
        float[,] rref = matrix.Clone() as float[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (rref[j, i] != 0)
              {
                LinearAlgebra.SwapRows(rref, i, j);
                LinearAlgebra.SwapRows(identity, i, j);
              }
          LinearAlgebra.RowMultiplication(identity, i, 1 / rref[i, i]);
          LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.GetLength(0); j++)
          {
            LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          }
          for (int j = i - 1; j >= 0; j--)
          {
            LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          }
        }
        return identity;
      }
      catch { throw new Error("inverse calculation failed."); }
    }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static float[,] Adjoint(float[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("Adjoint of a non-square matrix does not exists");
      float[,] AdjointMatrix = new float[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          if ((i + j) % 2 == 0)
            AdjointMatrix[i, j] = LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
          else
            AdjointMatrix[i, j] = -LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
      return LinearAlgebra.Transpose(AdjointMatrix);
    }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static float[,] Transpose(float[,] matrix)
    {
      float[,] transpose = new float[matrix.GetLength(1), matrix.GetLength(0)];
      for (int i = 0; i < transpose.GetLength(0); i++)
        for (int j = 0; j < transpose.GetLength(1); j++)
          transpose[i, j] = matrix[j, i];
      return transpose;
    }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="Lower">The computed lower triangular matrix.</param>
    /// <param name="Upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(float[,] matrix, out float[,] Lower, out float[,] Upper)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("The matrix is not square!");
      Lower = LinearAlgebra.MatrixFactoryIdentity_float(matrix.GetLength(0), matrix.GetLength(1));
      Upper = matrix.Clone() as float[,];
      int[] permutation = new int[matrix.GetLength(0)];
      for (int i = 0; i < matrix.GetLength(0); i++) permutation[i] = i;
      float p = 0, pom2, detOfP = 1;
      int k0 = 0, pom1 = 0;
      for (int k = 0; k < matrix.GetLength(1) - 1; k++)
      {
        p = 0;
        for (int i = k; i < matrix.GetLength(0); i++)
          if ((Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k]) > p)
          {
            p = Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k];
            k0 = i;
          }
        if (p == 0)
          throw new Error("The matrix is singular!");
        pom1 = permutation[k];
        permutation[k] = permutation[k0];
        permutation[k0] = pom1;
        for (int i = 0; i < k; i++)
        {
          pom2 = Lower[k, i];
          Lower[k, i] = Lower[k0, i];
          Lower[k0, i] = pom2;
        }
        if (k != k0)
          detOfP *= -1;
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
          pom2 = Upper[k, i];
          Upper[k, i] = Upper[k0, i];
          Upper[k0, i] = pom2;
        }
        for (int i = k + 1; i < matrix.GetLength(0); i++)
        {
          Lower[i, k] = Upper[i, k] / Upper[k, k];
          for (int j = k; j < matrix.GetLength(1); j++)
            Upper[i, j] = Upper[i, j] - Lower[i, k] * Upper[k, j];
        }
      }
    }

    /// <summary>Creates a copy of a matrix.</summary>
    /// <param name="matrix">The matrix to copy.</param>
    /// <returns>A copy of the matrix.</returns>
    public static float[,] Clone(float[,] matrix)
    {
      float[,] result = new float[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j];
      return result;
    }

    private static void RowMultiplication(float[,] matrix, int row, float scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[row, j] *= scalar;
    }

    private static void RowAddition(float[,] matrix, int target, int second, float scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[target, j] += (matrix[second, j] * scalar);
    }

    private static void SwapRows(float[,] matrix, int row1, int row2)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
      {
        float temp = matrix[row1, j];
        matrix[row1, j] = matrix[row2, j];
        matrix[row2, j] = temp;
      }
    }

    #endregion

    #region long

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static long[] Add(long[] left, long[] right)
    {
      if (left.Length != right.Length)
        throw new Error("invalid dimensions on vector addition.");
      long[] result = new long[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + right[i];
      return result;
    }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static long[] Negate(long[] vector)
    {
      long[] result = new long[vector.Length];
      for (int i = 0; i < vector.Length; i++)
        result[i] = -vector[i];
      return result;
    }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static long[] Subtract(long[] left, long[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector subtraction.");
      long[] result = new long[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] - right[i];
      return result;
    }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static long[] Multiply(long[] left, long right)
    {
      long[] result = new long[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] * right;
      return result;
    }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static long[] Divide(long[] vector, long right)
    {
      long[] result = new long[vector.Length];
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result[i] = vector[i] / right;
      return result;
    }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static long DotProduct(long[] left, long[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector dot product.");
      long result = 0;
      for (int i = 0; i < left.Length; i++)
        result += (left[i] * right[i]);
      return result;
    }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static long[] CrossProduct(long[] left, long[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid cross product !(left.Dimensions == right.Dimensions)");
      if (left.Length == 3 || left.Length == 4)
      {
        return new long[] {
          left[1] * right[2] - left[2] * right[1],
          left[2] * right[0] - left[0] * right[2],
          left[0] * right[1] - left[1] * right[0] };
      }
      throw new Exception("my cross product function is only defined for 3-component vectors.");
    }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static long[] Normalize(long[] vector)
    {
      long length = LinearAlgebra.Magnitude(vector);
      if (length != 0.0)
      {
        long[] result = new long[vector.Length];
        for (int i = 0; i < vector.Length; i++)
          result[i] = vector[i] / length;
        return result;
      }
      else
        return new long[vector.Length];
    }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static long Magnitude(long[] vector)
    {
      long result = 0;
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result += (vector[i] * vector[i]);
      return (long)System.Math.Sqrt(result);
    }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static long MagnitudeSquared(long[] vector)
    {
      long result = 0;
      for (int i = 0; i < vector.Length; i++)
        result += (vector[i] * vector[i]);
      return result;
    }

    /// <summary>Computes the angle between two vectors.</summary>
    /// <param name="first">The first vector to determine the angle between.</param>
    /// <param name="second">The second vector to determine the angle between.</param>
    /// <returns>The angle between the two vectors in radians.</returns>
    public static long Angle(long[] first, long[] second)
    {
      throw new Error("Angle is a rational operation that cannot be performed on long[,]");
      //return System.Math.Acos(LinearAlgebra.DotProduct(first, second) / (LinearAlgebra.Magnitude(first) * LinearAlgebra.Magnitude(second)));
    }

    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle of the rotation.</param>
    /// <param name="x">The x component of the axis vector to rotate about.</param>
    /// <param name="y">The y component of the axis vector to rotate about.</param>
    /// <param name="z">The z component of the axis vector to rotate about.</param>
    /// <returns>The result of the rotation.</returns>
    public static long[] RotateBy(long[] vector, long angle, long x, long y, long z)
    {
      throw new Error("RotateBy is a rational operation that cannot be performed on long[,]");
      //if (vector.Length == 3)
      //{
      //  // Note: the angle is in radians
      //  long sinHalfAngle = System.Math.Sin(angle / 2);
      //  long cosHalfAngle = System.Math.Cos(angle / 2);
      //  x *= sinHalfAngle;
      //  y *= sinHalfAngle;
      //  z *= sinHalfAngle;
      //  long x2 = cosHalfAngle * vector[0] + y * vector[2] - z * vector[1];
      //  long y2 = cosHalfAngle * vector[1] + z * vector[0] - x * vector[2];
      //  long z2 = cosHalfAngle * vector[2] + x * vector[1] - y * vector[0];
      //  long w2 = -x * vector[0] - y * vector[1] - z * vector[2];
      //  return new long[] {
      //    x * w2 + cosHalfAngle * x2 + y * z2 - z * y2,
      //    y * w2 + cosHalfAngle * y2 + z * x2 - x * z2,
      //    z * w2 + cosHalfAngle * z2 + x * y2 - y * x2 };
      //}
      //throw new Error("my RotateBy() function is only defined for 3-component vectors.");
    }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static long[] Lerp(long[] left, long[] right, long blend)
    {
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      if (left.Length != right.Length)
        throw new Error("invalid lerp matrix length: (left.Dimensions != right.Dimensions)");
      long[] result = new long[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + blend * (right[i] - left[i]);
      return result;
    }

    /// <summary>Sphereically interpolates between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    /// <returns>The result of the slerp operation.</returns>
    public static long[] Slerp(long[] left, long[] right, long blend)
    {
      throw new Error("Slerp is a rational operation that cannot be performed on long[,]");
      //if (blend < 0 || blend > 1.0f)
      //  throw new Error("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
      //long dot = LinearAlgebra.DotProduct(left, right);
      //dot = dot < -1.0d ? -1.0d : dot;
      //dot = dot > 1.0d ? 1.0 : dot;
      //long theta = System.Math.Acos(dot) * blend;
      //return LinearAlgebra.Multiply(LinearAlgebra.Add(LinearAlgebra.Multiply(left, System.Math.Cos(theta)),
      //  LinearAlgebra.Normalize(LinearAlgebra.Subtract(right, LinearAlgebra.Multiply(left, dot)))), System.Math.Sin(theta));
    }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static long[] Blerp(long[] a, long[] b, long[] c, long u, long v)
    {
      return LinearAlgebra.Add(LinearAlgebra.Add(LinearAlgebra.Multiply(LinearAlgebra.Subtract(b, a), u),
        LinearAlgebra.Multiply(LinearAlgebra.Subtract(c, a), v)), a);
    }

    /// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed identity-matrix.</returns>
    public static long[,] MatrixFactoryIdentity_long(int rows, int columns)
    {
      long[,] matrix;
      try { matrix = new long[rows, columns]; }
      catch { throw new Error("invalid dimensions."); }
      if (rows <= columns)
        for (int i = 0; i < rows; i++)
          matrix[i, i] = 1;
      else
        for (int i = 0; i < columns; i++)
          matrix[i, i] = 1;
      return matrix;
    }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static long[,] Negate(long[,] matrix)
    {
      long[,] result = new long[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = -matrix[i, j];
      return result;
    }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static long[,] Add(long[,] left, long[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid addition (size miss-match).");
      long[,] result = new long[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          result[i, j] = left[i, j] + right[i, j];
      return result;
    }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static long[,] Subtract(long[,] left, long[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid subtraction (size miss-match).");
      long[,] result = new long[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          result[i, j] = left[i, j] - right[i, j];
      return result;
    }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static long[,] Multiply(long[,] left, long[,] right)
    {
      if (left.GetLength(1) != right.GetLength(0))
        throw new LinearAlgebra.Error("invalid multiplication (size miss-match).");
      long[,] result = new long[left.GetLength(0), right.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          for (int k = 0; k < left.GetLength(1); k++)
            result[i, j] += left[i, k] * right[k, j];
      return result;
    }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static long[,] Multiply(long[,] left, long right)
    {
      long[,] result = new long[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          result[i, j] = left[i, j] * right;
      return result;
    }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static long[,] Power(long[,] matrix, int power)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid power (!matrix.IsSquare).");
      if (!(power >= 0))
        throw new Error("invalid power !(power > -1)");
      if (power == 0)
        return LinearAlgebra.MatrixFactoryIdentity_long(matrix.GetLength(0), matrix.GetLength(1));
      long[,] result = matrix.Clone() as long[,];
      for (int i = 0; i < power; i++)
        result = LinearAlgebra.Multiply(result, result);
      return result;
    }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static long[,] Divide(long[,] matrix, long right)
    {
      long[,] result = new long[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j] / right;
      return result;
    }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static long[,] Minor(long[,] matrix, int row, int column)
    {
      long[,] minor = new long[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
      int m = 0, n = 0;
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        if (i == row) continue;
        n = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          if (j == column) continue;
          minor[m, n] = matrix[i, j];
          n++;
        }
        m++;
      }
      return minor;
    }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static long[,] ConcatenateRowWise(long[,] left, long[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0))
        throw new Error("invalid row-wise concatenation !(left.Rows == right.Rows).");
      long[,] result = new long[left.GetLength(0), left.GetLength(1) + right.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
        {
          if (j < left.GetLength(1)) result[i, j] = left[i, j];
          else result[i, j] = right[i, j - left.GetLength(1)];
        }
      return result;
    }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static long[,] Echelon(long[,] matrix)
    {
      try
      {
        long[,] result = matrix.Clone() as long[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] != 0)
                LinearAlgebra.SwapRows(result, i, j);
          if (result[i, i] == 0)
            continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] == 1)
                LinearAlgebra.SwapRows(result, i, j);
          LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.GetLength(0); j++)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new Error("echelon computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static long[,] ReducedEchelon(long[,] matrix)
    {
      try
      {
        long[,] result = matrix.Clone() as long[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] != 0)
                LinearAlgebra.SwapRows(result, i, j);
          if (result[i, i] == 0) continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] == 1)
                LinearAlgebra.SwapRows(result, i, j);
          LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.GetLength(0); j++)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
          for (int j = i - 1; j >= 0; j--)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new Error("reduced echelon calculation failed."); }
    }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static long Determinent(long[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid determinent !(matrix.IsSquare).");
      try
      {
        long det = 1;
        long[,] rref = matrix.Clone() as long[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (rref[j, i] != 0)
              {
                LinearAlgebra.SwapRows(rref, i, j);
                det *= -1;
              }
          det *= rref[i, i];
          LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.GetLength(0); j++)
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          for (int j = i - 1; j >= 0; j--)
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
        }
        return det;
      }
      catch { throw new Error("determinent computation failed."); }
    }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static long[,] Inverse(long[,] matrix)
    {
      if (LinearAlgebra.Determinent(matrix) == 0)
        throw new Error("inverse calculation failed.");
      try
      {
        long[,] identity = LinearAlgebra.MatrixFactoryIdentity_long(matrix.GetLength(0), matrix.GetLength(1));
        long[,] rref = matrix.Clone() as long[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (rref[j, i] != 0)
              {
                LinearAlgebra.SwapRows(rref, i, j);
                LinearAlgebra.SwapRows(identity, i, j);
              }
          LinearAlgebra.RowMultiplication(identity, i, 1 / rref[i, i]);
          LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.GetLength(0); j++)
          {
            LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          }
          for (int j = i - 1; j >= 0; j--)
          {
            LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          }
        }
        return identity;
      }
      catch { throw new Error("inverse calculation failed."); }
    }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static long[,] Adjoint(long[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("Adjoint of a non-square matrix does not exists");
      long[,] AdjointMatrix = new long[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          if ((i + j) % 2 == 0)
            AdjointMatrix[i, j] = LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
          else
            AdjointMatrix[i, j] = -LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
      return LinearAlgebra.Transpose(AdjointMatrix);
    }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static long[,] Transpose(long[,] matrix)
    {
      long[,] transpose = new long[matrix.GetLength(1), matrix.GetLength(0)];
      for (int i = 0; i < transpose.GetLength(0); i++)
        for (int j = 0; j < transpose.GetLength(1); j++)
          transpose[i, j] = matrix[j, i];
      return transpose;
    }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="Lower">The computed lower triangular matrix.</param>
    /// <param name="Upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(long[,] matrix, out long[,] Lower, out long[,] Upper)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("The matrix is not square!");
      Lower = LinearAlgebra.MatrixFactoryIdentity_long(matrix.GetLength(0), matrix.GetLength(1));
      Upper = matrix.Clone() as long[,];
      int[] permutation = new int[matrix.GetLength(0)];
      for (int i = 0; i < matrix.GetLength(0); i++) permutation[i] = i;
      long p = 0, pom2, detOfP = 1;
      int k0 = 0, pom1 = 0;
      for (int k = 0; k < matrix.GetLength(1) - 1; k++)
      {
        p = 0;
        for (int i = k; i < matrix.GetLength(0); i++)
          if ((Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k]) > p)
          {
            p = Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k];
            k0 = i;
          }
        if (p == 0)
          throw new Error("The matrix is singular!");
        pom1 = permutation[k];
        permutation[k] = permutation[k0];
        permutation[k0] = pom1;
        for (int i = 0; i < k; i++)
        {
          pom2 = Lower[k, i];
          Lower[k, i] = Lower[k0, i];
          Lower[k0, i] = pom2;
        }
        if (k != k0)
          detOfP *= -1;
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
          pom2 = Upper[k, i];
          Upper[k, i] = Upper[k0, i];
          Upper[k0, i] = pom2;
        }
        for (int i = k + 1; i < matrix.GetLength(0); i++)
        {
          Lower[i, k] = Upper[i, k] / Upper[k, k];
          for (int j = k; j < matrix.GetLength(1); j++)
            Upper[i, j] = Upper[i, j] - Lower[i, k] * Upper[k, j];
        }
      }
    }

    /// <summary>Creates a copy of a matrix.</summary>
    /// <param name="matrix">The matrix to copy.</param>
    /// <returns>A copy of the matrix.</returns>
    public static long[,] Clone(long[,] matrix)
    {
      long[,] result = new long[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j];
      return result;
    }

    private static void RowMultiplication(long[,] matrix, int row, long scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[row, j] *= scalar;
    }

    private static void RowAddition(long[,] matrix, int target, int second, long scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[target, j] += (matrix[second, j] * scalar);
    }

    private static void SwapRows(long[,] matrix, int row1, int row2)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
      {
        long temp = matrix[row1, j];
        matrix[row1, j] = matrix[row2, j];
        matrix[row2, j] = temp;
      }
    }

    #endregion

    #region int

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static int[] Add(int[] left, int[] right)
    {
      if (left.Length != right.Length)
        throw new Error("invalid dimensions on vector addition.");
      int[] result = new int[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + right[i];
      return result;
    }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static int[] Negate(int[] vector)
    {
      int[] result = new int[vector.Length];
      for (int i = 0; i < vector.Length; i++)
        result[i] = -vector[i];
      return result;
    }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static int[] Subtract(int[] left, int[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector subtraction.");
      int[] result = new int[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] - right[i];
      return result;
    }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static int[] Multiply(int[] left, int right)
    {
      int[] result = new int[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] * right;
      return result;
    }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static int[] Divide(int[] vector, int right)
    {
      int[] result = new int[vector.Length];
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result[i] = vector[i] / right;
      return result;
    }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static int DotProduct(int[] left, int[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid dimensions on vector dot product.");
      int result = 0;
      for (int i = 0; i < left.Length; i++)
        result += (left[i] * right[i]);
      return result;
    }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static int[] CrossProduct(int[] left, int[] right)
    {
      if (left.Length != right.Length)
        throw new Exception("invalid cross product !(left.Dimensions == right.Dimensions)");
      if (left.Length == 3 || left.Length == 4)
      {
        return new int[] {
          left[1] * right[2] - left[2] * right[1],
          left[2] * right[0] - left[0] * right[2],
          left[0] * right[1] - left[1] * right[0] };
      }
      throw new Exception("my cross product function is only defined for 3-component vectors.");
    }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static int[] Normalize(int[] vector)
    {
      int length = LinearAlgebra.Magnitude(vector);
      if (length != 0.0)
      {
        int[] result = new int[vector.Length];
        for (int i = 0; i < vector.Length; i++)
          result[i] = vector[i] / length;
        return result;
      }
      else
        return new int[vector.Length];
    }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static int Magnitude(int[] vector)
    {
      int result = 0;
      int arrayLength = vector.Length;
      for (int i = 0; i < arrayLength; i++)
        result += (vector[i] * vector[i]);
      return (int)System.Math.Sqrt(result);
    }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static int MagnitudeSquared(int[] vector)
    {
      int result = 0;
      for (int i = 0; i < vector.Length; i++)
        result += (vector[i] * vector[i]);
      return result;
    }

    /// <summary>Computes the angle between two vectors.</summary>
    /// <param name="first">The first vector to determine the angle between.</param>
    /// <param name="second">The second vector to determine the angle between.</param>
    /// <returns>The angle between the two vectors in radians.</returns>
    public static int Angle(int[] first, int[] second)
    {
      throw new Error("Angle is a rational operation that cannot be performed on int[,]");
      //return System.Math.Acos(LinearAlgebra.DotProduct(first, second) / (LinearAlgebra.Magnitude(first) * LinearAlgebra.Magnitude(second)));
    }

    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle of the rotation.</param>
    /// <param name="x">The x component of the axis vector to rotate about.</param>
    /// <param name="y">The y component of the axis vector to rotate about.</param>
    /// <param name="z">The z component of the axis vector to rotate about.</param>
    /// <returns>The result of the rotation.</returns>
    public static int[] RotateBy(int[] vector, int angle, int x, int y, int z)
    {
      throw new Error("Angle is a rational operation that cannot be performed on int[,]");
      //if (vector.Length == 3)
      //{
      //  // Note: the angle is in radians
      //  int sinHalfAngle = System.Math.Sin(angle / 2);
      //  int cosHalfAngle = System.Math.Cos(angle / 2);
      //  x *= sinHalfAngle;
      //  y *= sinHalfAngle;
      //  z *= sinHalfAngle;
      //  int x2 = cosHalfAngle * vector[0] + y * vector[2] - z * vector[1];
      //  int y2 = cosHalfAngle * vector[1] + z * vector[0] - x * vector[2];
      //  int z2 = cosHalfAngle * vector[2] + x * vector[1] - y * vector[0];
      //  int w2 = -x * vector[0] - y * vector[1] - z * vector[2];
      //  return new int[] {
      //    x * w2 + cosHalfAngle * x2 + y * z2 - z * y2,
      //    y * w2 + cosHalfAngle * y2 + z * x2 - x * z2,
      //    z * w2 + cosHalfAngle * z2 + x * y2 - y * x2 };
      //}
      //throw new Error("my RotateBy() function is only defined for 3-component vectors.");
    }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static int[] Lerp(int[] left, int[] right, int blend)
    {
      if (blend < 0 || blend > 1.0f)
        throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
      if (left.Length != right.Length)
        throw new Error("invalid lerp matrix length: (left.Dimensions != right.Dimensions)");
      int[] result = new int[left.Length];
      for (int i = 0; i < left.Length; i++)
        result[i] = left[i] + blend * (right[i] - left[i]);
      return result;
    }

    /// <summary>Sphereically interpolates between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    /// <returns>The result of the slerp operation.</returns>
    public static int[] Slerp(int[] left, int[] right, int blend)
    {
      throw new Error("Slerp is a rational operation that cannot be performed on int[,]");
      //if (blend < 0 || blend > 1.0f)
      //  throw new Error("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
      //int dot = LinearAlgebra.DotProduct(left, right);
      //dot = dot < -1.0d ? -1.0d : dot;
      //dot = dot > 1.0d ? 1.0 : dot;
      //int theta = System.Math.Acos(dot) * blend;
      //return LinearAlgebra.Multiply(LinearAlgebra.Add(LinearAlgebra.Multiply(left, System.Math.Cos(theta)),
      //  LinearAlgebra.Normalize(LinearAlgebra.Subtract(right, LinearAlgebra.Multiply(left, dot)))), System.Math.Sin(theta));
    }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static int[] Blerp(int[] a, int[] b, int[] c, int u, int v)
    {
      return LinearAlgebra.Add(LinearAlgebra.Add(LinearAlgebra.Multiply(LinearAlgebra.Subtract(b, a), u),
        LinearAlgebra.Multiply(LinearAlgebra.Subtract(c, a), v)), a);
    }

    /// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed identity-matrix.</returns>
    public static int[,] MatrixFactoryIdentity_int(int rows, int columns)
    {
      int[,] matrix;
      try { matrix = new int[rows, columns]; }
      catch { throw new Error("invalid dimensions."); }
      if (rows <= columns)
        for (int i = 0; i < rows; i++)
          matrix[i, i] = 1;
      else
        for (int i = 0; i < columns; i++)
          matrix[i, i] = 1;
      return matrix;
    }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static int[,] Negate(int[,] matrix)
    {
      int[,] result = new int[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = -matrix[i, j];
      return result;
    }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static int[,] Add(int[,] left, int[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid addition (size miss-match).");
      int[,] result = new int[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          result[i, j] = left[i, j] + right[i, j];
      return result;
    }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static int[,] Subtract(int[,] left, int[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid subtraction (size miss-match).");
      int[,] result = new int[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          result[i, j] = left[i, j] - right[i, j];
      return result;
    }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static int[,] Multiply(int[,] left, int[,] right)
    {
      if (left.GetLength(1) != right.GetLength(0))
        throw new LinearAlgebra.Error("invalid multiplication (size miss-match).");
      int[,] result = new int[left.GetLength(0), right.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
          for (int k = 0; k < left.GetLength(1); k++)
            result[i, j] += left[i, k] * right[k, j];
      return result;
    }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static int[,] Multiply(int[,] left, int right)
    {
      int[,] result = new int[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          result[i, j] = left[i, j] * right;
      return result;
    }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static int[,] Power(int[,] matrix, int power)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid power (!matrix.IsSquare).");
      if (!(power >= 0))
        throw new Error("invalid power !(power > -1)");
      if (power == 0)
        return LinearAlgebra.MatrixFactoryIdentity_int(matrix.GetLength(0), matrix.GetLength(1));
      int[,] result = matrix.Clone() as int[,];
      for (int i = 0; i < power; i++)
        result = LinearAlgebra.Multiply(result, result);
      return result;
    }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static int[,] Divide(int[,] matrix, int right)
    {
      int[,] result = new int[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j] / right;
      return result;
    }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static int[,] Minor(int[,] matrix, int row, int column)
    {
      int[,] minor = new int[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
      int m = 0, n = 0;
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        if (i == row) continue;
        n = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          if (j == column) continue;
          minor[m, n] = matrix[i, j];
          n++;
        }
        m++;
      }
      return minor;
    }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static int[,] ConcatenateRowWise(int[,] left, int[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0))
        throw new Error("invalid row-wise concatenation !(left.Rows == right.Rows).");
      int[,] result = new int[left.GetLength(0), left.GetLength(1) + right.GetLength(1)];
      for (int i = 0; i < result.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
        {
          if (j < left.GetLength(1)) result[i, j] = left[i, j];
          else result[i, j] = right[i, j - left.GetLength(1)];
        }
      return result;
    }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static int[,] Echelon(int[,] matrix)
    {
      try
      {
        int[,] result = matrix.Clone() as int[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] != 0)
                LinearAlgebra.SwapRows(result, i, j);
          if (result[i, i] == 0)
            continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] == 1)
                LinearAlgebra.SwapRows(result, i, j);
          LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.GetLength(0); j++)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new Error("echelon computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static int[,] ReducedEchelon(int[,] matrix)
    {
      try
      {
        int[,] result = matrix.Clone() as int[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] != 0)
                LinearAlgebra.SwapRows(result, i, j);
          if (result[i, i] == 0) continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (result[j, i] == 1)
                LinearAlgebra.SwapRows(result, i, j);
          LinearAlgebra.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.GetLength(0); j++)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
          for (int j = i - 1; j >= 0; j--)
            LinearAlgebra.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new Error("reduced echelon calculation failed."); }
    }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static int Determinent(int[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid determinent !(matrix.IsSquare).");
      try
      {
        int det = 1;
        int[,] rref = matrix.Clone() as int[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (rref[j, i] != 0)
              {
                LinearAlgebra.SwapRows(rref, i, j);
                det *= -1;
              }
          det *= rref[i, i];
          LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.GetLength(0); j++)
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          for (int j = i - 1; j >= 0; j--)
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
        }
        return det;
      }
      catch { throw new Error("determinent computation failed."); }
    }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static int[,] Inverse(int[,] matrix)
    {
      if (LinearAlgebra.Determinent(matrix) == 0)
        throw new Error("inverse calculation failed.");
      try
      {
        int[,] identity = LinearAlgebra.MatrixFactoryIdentity_int(matrix.GetLength(0), matrix.GetLength(1));
        int[,] rref = matrix.Clone() as int[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (rref[j, i] != 0)
              {
                LinearAlgebra.SwapRows(rref, i, j);
                LinearAlgebra.SwapRows(identity, i, j);
              }
          LinearAlgebra.RowMultiplication(identity, i, 1 / rref[i, i]);
          LinearAlgebra.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.GetLength(0); j++)
          {
            LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          }
          for (int j = i - 1; j >= 0; j--)
          {
            LinearAlgebra.RowAddition(identity, j, i, -rref[j, i]);
            LinearAlgebra.RowAddition(rref, j, i, -rref[j, i]);
          }
        }
        return identity;
      }
      catch { throw new Error("inverse calculation failed."); }
    }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static int[,] Adjoint(int[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("Adjoint of a non-square matrix does not exists");
      int[,] AdjointMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          if ((i + j) % 2 == 0)
            AdjointMatrix[i, j] = LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
          else
            AdjointMatrix[i, j] = -LinearAlgebra.Determinent(LinearAlgebra.Minor(matrix, i, j));
      return LinearAlgebra.Transpose(AdjointMatrix);
    }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static int[,] Transpose(int[,] matrix)
    {
      int[,] transpose = new int[matrix.GetLength(1), matrix.GetLength(0)];
      for (int i = 0; i < transpose.GetLength(0); i++)
        for (int j = 0; j < transpose.GetLength(1); j++)
          transpose[i, j] = matrix[j, i];
      return transpose;
    }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="Lower">The computed lower triangular matrix.</param>
    /// <param name="Upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(int[,] matrix, out int[,] Lower, out int[,] Upper)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("The matrix is not square!");
      Lower = LinearAlgebra.MatrixFactoryIdentity_int(matrix.GetLength(0), matrix.GetLength(1));
      Upper = matrix.Clone() as int[,];
      int[] permutation = new int[matrix.GetLength(0)];
      for (int i = 0; i < matrix.GetLength(0); i++) permutation[i] = i;
      int p = 0, pom2, detOfP = 1;
      int k0 = 0, pom1 = 0;
      for (int k = 0; k < matrix.GetLength(1) - 1; k++)
      {
        p = 0;
        for (int i = k; i < matrix.GetLength(0); i++)
          if ((Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k]) > p)
          {
            p = Upper[i, k] > 0 ? Upper[i, k] : -Upper[i, k];
            k0 = i;
          }
        if (p == 0)
          throw new Error("The matrix is singular!");
        pom1 = permutation[k];
        permutation[k] = permutation[k0];
        permutation[k0] = pom1;
        for (int i = 0; i < k; i++)
        {
          pom2 = Lower[k, i];
          Lower[k, i] = Lower[k0, i];
          Lower[k0, i] = pom2;
        }
        if (k != k0)
          detOfP *= -1;
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
          pom2 = Upper[k, i];
          Upper[k, i] = Upper[k0, i];
          Upper[k0, i] = pom2;
        }
        for (int i = k + 1; i < matrix.GetLength(0); i++)
        {
          Lower[i, k] = Upper[i, k] / Upper[k, k];
          for (int j = k; j < matrix.GetLength(1); j++)
            Upper[i, j] = Upper[i, j] - Lower[i, k] * Upper[k, j];
        }
      }
    }

    /// <summary>Creates a copy of a matrix.</summary>
    /// <param name="matrix">The matrix to copy.</param>
    /// <returns>A copy of the matrix.</returns>
    public static int[,] Clone(int[,] matrix)
    {
      int[,] result = new int[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j];
      return result;
    }

    private static void RowMultiplication(int[,] matrix, int row, int scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[row, j] *= scalar;
    }

    private static void RowAddition(int[,] matrix, int target, int second, int scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[target, j] += (matrix[second, j] * scalar);
    }

    private static void SwapRows(int[,] matrix, int row1, int row2)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
      {
        int temp = matrix[row1, j];
        matrix[row1, j] = matrix[row2, j];
        matrix[row2, j] = temp;
      }
    }

    #endregion

		#endregion

		/// <summary>Error type for all algebra computations.</summary>
		public class Error : Seven.Error
		{
			public Error(string message) : base(message) { }
		}
	}

  /// <summary>High performance non-generic linear algrbra extension methods.</summary>
  public static class LinearAlgebra_Extensions
  {
    // The following are high performance (non-generic) extension
    // methods for some primitive types

    #region decimal

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static decimal[] Add(this decimal[] left, decimal[] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static decimal[] Negate(this decimal[] vector)
    { return LinearAlgebra.Negate(vector); }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static decimal[] Subtract(this decimal[] left, decimal[] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static decimal[] Multiply(this decimal[] left, decimal right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static decimal[] Divide(this decimal[] vector, decimal right)
    { return LinearAlgebra.Divide(vector, right); }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static decimal DotProduct(this decimal[] left, decimal[] right)
    { return LinearAlgebra.DotProduct(left, right); }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static decimal[] CrossProduct(this decimal[] left, decimal[] right)
    { return LinearAlgebra.CrossProduct(left, right); }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static decimal[] Normalize(this decimal[] vector)
    { return LinearAlgebra.Normalize(vector); }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static decimal Magnitude(this decimal[] vector)
    { return LinearAlgebra.Magnitude(vector); }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static decimal MagnitudeSquared(this decimal[] vector)
    { return LinearAlgebra.MagnitudeSquared(vector); }

    /// <summary>Computes the angle between two vectors.</summary>
    /// <param name="first">The first vector to determine the angle between.</param>
    /// <param name="second">The second vector to determine the angle between.</param>
    /// <returns>The angle between the two vectors in radians.</returns>
    public static decimal Angle(this decimal[] first, decimal[] second)
    { return LinearAlgebra.Angle(first, second); }

    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle of the rotation.</param>
    /// <param name="x">The x component of the axis vector to rotate about.</param>
    /// <param name="y">The y component of the axis vector to rotate about.</param>
    /// <param name="z">The z component of the axis vector to rotate about.</param>
    /// <returns>The result of the rotation.</returns>
    public static decimal[] RotateBy(this decimal[] vector, decimal angle, decimal x, decimal y, decimal z)
    { return LinearAlgebra.RotateBy(vector, angle, x, y, z); }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static decimal[] Lerp(this decimal[] left, decimal[] right, decimal blend)
    { return LinearAlgebra.Lerp(left, right, blend); }

    /// <summary>Sphereically interpolates between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    /// <returns>The result of the slerp operation.</returns>
    public static decimal[] Slerp(this decimal[] left, decimal[] right, decimal blend)
    { return LinearAlgebra.Slerp(left, right, blend); }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static decimal[] Blerp(this decimal[] a, decimal[] b, decimal[] c, decimal u, decimal v)
    { return LinearAlgebra.Blerp(a, b, c, u, v); }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static decimal[,] Negate(this decimal[,] matrix)
    { return LinearAlgebra.Negate(matrix); }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static decimal[,] Add(this decimal[,] left, decimal[,] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static decimal[,] Subtract(this decimal[,] left, decimal[,] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static decimal[,] Multiply(this decimal[,] left, decimal[,] right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static decimal[,] Multiply(this decimal[,] left, decimal right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static decimal[,] Power(this decimal[,] matrix, int power)
    { return LinearAlgebra.Power(matrix, power); }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static decimal[,] Divide(this decimal[,] matrix, decimal right)
    { return LinearAlgebra.Divide(matrix, right); }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static decimal[,] Minor(this decimal[,] matrix, int row, int column)
    { return LinearAlgebra.Minor(matrix, row, column); }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static decimal[,] ConcatenateRowWise(this decimal[,] left, decimal[,] right)
    { return LinearAlgebra.ConcatenateRowWise(left, right); }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static decimal[,] Echelon(this decimal[,] matrix)
    { return LinearAlgebra.Echelon(matrix); }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static decimal[,] ReducedEchelon(this decimal[,] matrix)
    { return LinearAlgebra.ReducedEchelon(matrix); }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static decimal Determinent(this decimal[,] matrix)
    { return LinearAlgebra.Determinent(matrix); }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static decimal[,] Inverse(this decimal[,] matrix)
    { return LinearAlgebra.Inverse(matrix); }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static decimal[,] Adjoint(this decimal[,] matrix)
    { return LinearAlgebra.Adjoint(matrix); }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static decimal[,] Transpose(this decimal[,] matrix)
    { return LinearAlgebra.Transpose(matrix); }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="lower">The computed lower triangular matrix.</param>
    /// <param name="upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(this decimal[,] matrix, out decimal[,] lower, out decimal[,] upper)
    { LinearAlgebra.DecomposeLU(matrix, out lower, out upper); }

    #endregion

    #region double

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static double[] Add(this double[] left, double[] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static double[] Negate(this double[] vector)
    { return LinearAlgebra.Negate(vector); }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static double[] Subtract(this double[] left, double[] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static double[] Multiply(this double[] left, double right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static double[] Divide(this double[] vector, double right)
    { return LinearAlgebra.Divide(vector, right); }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static double DotProduct(this double[] left, double[] right)
    { return LinearAlgebra.DotProduct(left, right); }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static double[] CrossProduct(this double[] left, double[] right)
    { return LinearAlgebra.CrossProduct(left, right); }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static double[] Normalize(this double[] vector)
    { return LinearAlgebra.Normalize(vector); }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static double Magnitude(this double[] vector)
    { return LinearAlgebra.Magnitude(vector); }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static double MagnitudeSquared(this double[] vector)
    { return LinearAlgebra.MagnitudeSquared(vector); }

    /// <summary>Computes the angle between two vectors.</summary>
    /// <param name="first">The first vector to determine the angle between.</param>
    /// <param name="second">The second vector to determine the angle between.</param>
    /// <returns>The angle between the two vectors in radians.</returns>
    public static double Angle(this double[] first, double[] second)
    { return LinearAlgebra.Angle(first, second); }

    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle of the rotation.</param>
    /// <param name="x">The x component of the axis vector to rotate about.</param>
    /// <param name="y">The y component of the axis vector to rotate about.</param>
    /// <param name="z">The z component of the axis vector to rotate about.</param>
    /// <returns>The result of the rotation.</returns>
    public static double[] RotateBy(this double[] vector, double angle, double x, double y, double z)
    { return LinearAlgebra.RotateBy(vector, angle, x, y, z); }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static double[] Lerp(this double[] left, double[] right, double blend)
    { return LinearAlgebra.Lerp(left, right, blend); }

    /// <summary>Sphereically interpolates between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    /// <returns>The result of the slerp operation.</returns>
    public static double[] Slerp(this double[] left, double[] right, double blend)
    { return LinearAlgebra.Slerp(left, right, blend); }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static double[] Blerp(this double[] a, double[] b, double[] c, double u, double v)
    { return LinearAlgebra.Blerp(a, b, c, u, v); }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static double[,] Negate(this double[,] matrix)
    { return LinearAlgebra.Negate(matrix); }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static double[,] Add(this double[,] left, double[,] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static double[,] Subtract(this double[,] left, double[,] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static double[,] Multiply(this double[,] left, double[,] right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static double[,] Multiply(this double[,] left, double right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static double[,] Power(this double[,] matrix, int power)
    { return LinearAlgebra.Power(matrix, power); }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static double[,] Divide(this double[,] matrix, double right)
    { return LinearAlgebra.Divide(matrix, right); }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static double[,] Minor(this double[,] matrix, int row, int column)
    { return LinearAlgebra.Minor(matrix, row, column); }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static double[,] ConcatenateRowWise(this double[,] left, double[,] right)
    { return LinearAlgebra.ConcatenateRowWise(left, right); }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static double[,] Echelon(this double[,] matrix)
    { return LinearAlgebra.Echelon(matrix); }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static double[,] ReducedEchelon(this double[,] matrix)
    { return LinearAlgebra.ReducedEchelon(matrix); }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static double Determinent(this double[,] matrix)
    { return LinearAlgebra.Determinent(matrix); }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static double[,] Inverse(this double[,] matrix)
    { return LinearAlgebra.Inverse(matrix); }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static double[,] Adjoint(this double[,] matrix)
    { return LinearAlgebra.Adjoint(matrix); }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static double[,] Transpose(this double[,] matrix)
    { return LinearAlgebra.Transpose(matrix); }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="lower">The computed lower triangular matrix.</param>
    /// <param name="upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(this double[,] matrix, out double[,] lower, out double[,] upper) 
    { LinearAlgebra.DecomposeLU(matrix, out lower, out upper); }

    #endregion

    #region float

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static float[] Add(this float[] left, float[] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static float[] Negate(this float[] vector)
    { return LinearAlgebra.Negate(vector); }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static float[] Subtract(this float[] left, float[] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static float[] Multiply(this float[] left, float right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static float[] Divide(this float[] vector, float right)
    { return LinearAlgebra.Divide(vector, right); }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static float DotProduct(this float[] left, float[] right)
    { return LinearAlgebra.DotProduct(left, right); }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static float[] CrossProduct(this float[] left, float[] right)
    { return LinearAlgebra.CrossProduct(left, right); }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static float[] Normalize(this float[] vector)
    { return LinearAlgebra.Normalize(vector); }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static float Magnitude(this float[] vector)
    { return LinearAlgebra.Magnitude(vector); }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static float MagnitudeSquared(this float[] vector)
    { return LinearAlgebra.MagnitudeSquared(vector); }

    /// <summary>Computes the angle between two vectors.</summary>
    /// <param name="first">The first vector to determine the angle between.</param>
    /// <param name="second">The second vector to determine the angle between.</param>
    /// <returns>The angle between the two vectors in radians.</returns>
    public static float Angle(this float[] first, float[] second)
    { return LinearAlgebra.Angle(first, second); }

    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle of the rotation.</param>
    /// <param name="x">The x component of the axis vector to rotate about.</param>
    /// <param name="y">The y component of the axis vector to rotate about.</param>
    /// <param name="z">The z component of the axis vector to rotate about.</param>
    /// <returns>The result of the rotation.</returns>
    public static float[] RotateBy(this float[] vector, float angle, float x, float y, float z)
    { return LinearAlgebra.RotateBy(vector, angle, x, y, z); }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static float[] Lerp(this float[] left, float[] right, float blend)
    { return LinearAlgebra.Lerp(left, right, blend); }

    /// <summary>Sphereically interpolates between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    /// <returns>The result of the slerp operation.</returns>
    public static float[] Slerp(this float[] left, float[] right, float blend)
    { return LinearAlgebra.Slerp(left, right, blend); }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static float[] Blerp(this float[] a, float[] b, float[] c, float u, float v)
    { return LinearAlgebra.Blerp(a, b, c, u, v); }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static float[,] Negate(this float[,] matrix)
    { return LinearAlgebra.Negate(matrix); }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static float[,] Add(this float[,] left, float[,] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static float[,] Subtract(this float[,] left, float[,] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static float[,] Multiply(this float[,] left, float[,] right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static float[,] Multiply(this float[,] left, float right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static float[,] Power(this float[,] matrix, int power)
    { return LinearAlgebra.Power(matrix, power); }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static float[,] Divide(this float[,] matrix, float right)
    { return LinearAlgebra.Divide(matrix, right); }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static float[,] Minor(this float[,] matrix, int row, int column)
    { return LinearAlgebra.Minor(matrix, row, column); }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static float[,] ConcatenateRowWise(this float[,] left, float[,] right)
    { return LinearAlgebra.ConcatenateRowWise(left, right); }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static float[,] Echelon(this float[,] matrix)
    { return LinearAlgebra.Echelon(matrix); }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static float[,] ReducedEchelon(this float[,] matrix)
    { return LinearAlgebra.ReducedEchelon(matrix); }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static float Determinent(this float[,] matrix)
    { return LinearAlgebra.Determinent(matrix); }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static float[,] Inverse(this float[,] matrix)
    { return LinearAlgebra.Inverse(matrix); }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static float[,] Adjoint(this float[,] matrix)
    { return LinearAlgebra.Adjoint(matrix); }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static float[,] Transpose(this float[,] matrix)
    { return LinearAlgebra.Transpose(matrix); }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="lower">The computed lower triangular matrix.</param>
    /// <param name="upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(this float[,] matrix, out float[,] lower, out float[,] upper)
    { LinearAlgebra.DecomposeLU(matrix, out lower, out upper); }

    #endregion

    #region long

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static long[] Add(this long[] left, long[] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static long[] Negate(this long[] vector)
    { return LinearAlgebra.Negate(vector); }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static long[] Subtract(this long[] left, long[] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static long[] Multiply(this long[] left, long right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static long[] Divide(this long[] vector, long right)
    { return LinearAlgebra.Divide(vector, right); }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static long DotProduct(this long[] left, long[] right)
    { return LinearAlgebra.DotProduct(left, right); }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static long[] CrossProduct(this long[] left, long[] right)
    { return LinearAlgebra.CrossProduct(left, right); }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static long[] Normalize(this long[] vector)
    { return LinearAlgebra.Normalize(vector); }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static long Magnitude(this long[] vector)
    { return LinearAlgebra.Magnitude(vector); }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static long MagnitudeSquared(this long[] vector)
    { return LinearAlgebra.MagnitudeSquared(vector); }

    ///// <summary>Computes the angle between two vectors.</summary>
    ///// <param name="first">The first vector to determine the angle between.</param>
    ///// <param name="second">The second vector to determine the angle between.</param>
    ///// <returns>The angle between the two vectors in radians.</returns>
    //public static long Angle(this long[] first, long[] second)
    //{ return LinearAlgebra.Angle(first, second); }

    ///// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    ///// <param name="vector">The vector to rotate.</param>
    ///// <param name="angle">The angle of the rotation.</param>
    ///// <param name="x">The x component of the axis vector to rotate about.</param>
    ///// <param name="y">The y component of the axis vector to rotate about.</param>
    ///// <param name="z">The z component of the axis vector to rotate about.</param>
    ///// <returns>The result of the rotation.</returns>
    //public static long[] RotateBy(this long[] vector, long angle, long x, long y, long z)
    //{ return LinearAlgebra.RotateBy(vector, angle, x, y, z); }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static long[] Lerp(this long[] left, long[] right, long blend)
    { return LinearAlgebra.Lerp(left, right, blend); }

    ///// <summary>Sphereically interpolates between two vectors.</summary>
    ///// <param name="left">The starting vector of the interpolation.</param>
    ///// <param name="right">The ending vector of the interpolation.</param>
    ///// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    ///// <returns>The result of the slerp operation.</returns>
    //public static long[] Slerp(this long[] left, long[] right, long blend)
    //{ return LinearAlgebra.Slerp(left, right, blend); }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static long[] Blerp(this long[] a, long[] b, long[] c, long u, long v)
    { return LinearAlgebra.Blerp(a, b, c, u, v); }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static long[,] Negate(this long[,] matrix)
    { return LinearAlgebra.Negate(matrix); }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static long[,] Add(this long[,] left, long[,] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static long[,] Subtract(this long[,] left, long[,] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static long[,] Multiply(this long[,] left, long[,] right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static long[,] Multiply(this long[,] left, long right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static long[,] Power(this long[,] matrix, int power)
    { return LinearAlgebra.Power(matrix, power); }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static long[,] Divide(this long[,] matrix, long right)
    { return LinearAlgebra.Divide(matrix, right); }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static long[,] Minor(this long[,] matrix, int row, int column)
    { return LinearAlgebra.Minor(matrix, row, column); }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static long[,] ConcatenateRowWise(this long[,] left, long[,] right)
    { return LinearAlgebra.ConcatenateRowWise(left, right); }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static long[,] Echelon(this long[,] matrix)
    { return LinearAlgebra.Echelon(matrix); }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static long[,] ReducedEchelon(this long[,] matrix)
    { return LinearAlgebra.ReducedEchelon(matrix); }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static long Determinent(this long[,] matrix)
    { return LinearAlgebra.Determinent(matrix); }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static long[,] Inverse(this long[,] matrix)
    { return LinearAlgebra.Inverse(matrix); }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static long[,] Adjoint(this long[,] matrix)
    { return LinearAlgebra.Adjoint(matrix); }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static long[,] Transpose(this long[,] matrix)
    { return LinearAlgebra.Transpose(matrix); }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="lower">The computed lower triangular matrix.</param>
    /// <param name="upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(this long[,] matrix, out long[,] lower, out long[,] upper)
    { LinearAlgebra.DecomposeLU(matrix, out lower, out upper); }

    #endregion

    #region int

    /// <summary>Adds two vectors together.</summary>
    /// <param name="leftFloats">The first vector of the addition.</param>
    /// <param name="rightFloats">The second vector of the addiiton.</param>
    /// <returns>The result of the addiion.</returns>
    public static int[] Add(this int[] left, int[] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Negates all the values in a vector.</summary>
    /// <param name="vector">The vector to have its values negated.</param>
    /// <returns>The result of the negations.</returns>
    public static int[] Negate(this int[] vector)
    { return LinearAlgebra.Negate(vector); }

    /// <summary>Subtracts two vectors.</summary>
    /// <param name="left">The left vector of the subtraction.</param>
    /// <param name="right">The right vector of the subtraction.</param>
    /// <returns>The result of the vector subtracton.</returns>
    public static int[] Subtract(this int[] left, int[] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    /// <param name="left">The vector to have the components multiplied by.</param>
    /// <param name="right">The scalars to multiply the vector components by.</param>
    /// <returns>The result of the multiplications.</returns>
    public static int[] Multiply(this int[] left, int right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Divides all the components of a vector by a scalar.</summary>
    /// <param name="vector">The vector to have the components divided by.</param>
    /// <param name="right">The scalar to divide the vector components by.</param>
    /// <returns>The resulting vector after teh divisions.</returns>
    public static int[] Divide(this int[] vector, int right)
    { return LinearAlgebra.Divide(vector, right); }

    /// <summary>Computes the dot product between two vectors.</summary>
    /// <param name="left">The first vector of the dot product operation.</param>
    /// <param name="right">The second vector of the dot product operation.</param>
    /// <returns>The result of the dot product operation.</returns>
    public static int DotProduct(this int[] left, int[] right)
    { return LinearAlgebra.DotProduct(left, right); }

    /// <summary>Computes teh cross product of two vectors.</summary>
    /// <param name="left">The first vector of the cross product operation.</param>
    /// <param name="right">The second vector of the cross product operation.</param>
    /// <returns>The result of the cross product operation.</returns>
    public static int[] CrossProduct(this int[] left, int[] right)
    { return LinearAlgebra.CrossProduct(left, right); }

    /// <summary>Normalizes a vector.</summary>
    /// <param name="vector">The vector to normalize.</param>
    /// <returns>The result of the normalization.</returns>
    public static int[] Normalize(this int[] vector)
    { return LinearAlgebra.Normalize(vector); }

    /// <summary>Computes the length of a vector.</summary>
    /// <param name="vector">The vector to calculate the length of.</param>
    /// <returns>The computed length of the vector.</returns>
    public static int Magnitude(this int[] vector)
    { return LinearAlgebra.Magnitude(vector); }

    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    /// <param name="vector">The vector to compute the length squared of.</param>
    /// <returns>The computed length squared of the vector.</returns>
    public static int MagnitudeSquared(this int[] vector)
    { return LinearAlgebra.MagnitudeSquared(vector); }

    ///// <summary>Computes the angle between two vectors.</summary>
    ///// <param name="first">The first vector to determine the angle between.</param>
    ///// <param name="second">The second vector to determine the angle between.</param>
    ///// <returns>The angle between the two vectors in radians.</returns>
    //public static int Angle(this int[] first, int[] second)
    //{ return LinearAlgebra.Angle(first, second); }

    ///// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    ///// <param name="vector">The vector to rotate.</param>
    ///// <param name="angle">The angle of the rotation.</param>
    ///// <param name="x">The x component of the axis vector to rotate about.</param>
    ///// <param name="y">The y component of the axis vector to rotate about.</param>
    ///// <param name="z">The z component of the axis vector to rotate about.</param>
    ///// <returns>The result of the rotation.</returns>
    //public static int[] RotateBy(this int[] vector, int angle, int x, int y, int z)
    //{ return LinearAlgebra.RotateBy(vector, angle, x, y, z); }

    /// <summary>Computes the linear interpolation between two vectors.</summary>
    /// <param name="left">The starting vector of the interpolation.</param>
    /// <param name="right">The ending vector of the interpolation.</param>
    /// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
    /// <returns>The result of the interpolation.</returns>
    public static int[] Lerp(this int[] left, int[] right, int blend)
    { return LinearAlgebra.Lerp(left, right, blend); }

    ///// <summary>Sphereically interpolates between two vectors.</summary>
    ///// <param name="left">The starting vector of the interpolation.</param>
    ///// <param name="right">The ending vector of the interpolation.</param>
    ///// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
    ///// <returns>The result of the slerp operation.</returns>
    //public static int[] Slerp(this int[] left, int[] right, int blend)
    //{ return LinearAlgebra.Slerp(left, right, blend); }

    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    /// <param name="a">The first vector of the interpolation.</param>
    /// <param name="b">The second vector of the interpolation.</param>
    /// <param name="c">The thrid vector of the interpolation.</param>
    /// <param name="u">The "U" value of the barycentric interpolation equation.</param>
    /// <param name="v">The "V" value of the barycentric interpolation equation.</param>
    /// <returns>The resulting vector of the barycentric interpolation.</returns>
    public static int[] Blerp(this int[] a, int[] b, int[] c, int u, int v)
    { return LinearAlgebra.Blerp(a, b, c, u, v); }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static int[,] Negate(this int[,] matrix)
    { return LinearAlgebra.Negate(matrix); }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static int[,] Add(this int[,] left, int[,] right)
    { return LinearAlgebra.Add(left, right); }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static int[,] Subtract(this int[,] left, int[,] right)
    { return LinearAlgebra.Subtract(left, right); }

    /// <summary>Performs multiplication on two matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static int[,] Multiply(this int[,] left, int[,] right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static int[,] Multiply(this int[,] left, int right)
    { return LinearAlgebra.Multiply(left, right); }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static int[,] Power(this int[,] matrix, int power)
    { return LinearAlgebra.Power(matrix, power); }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static int[,] Divide(this int[,] matrix, int right)
    { return LinearAlgebra.Divide(matrix, right); }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static int[,] Minor(this int[,] matrix, int row, int column)
    { return LinearAlgebra.Minor(matrix, row, column); }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static int[,] ConcatenateRowWise(this int[,] left, int[,] right)
    { return LinearAlgebra.ConcatenateRowWise(left, right); }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static int[,] Echelon(this int[,] matrix)
    { return LinearAlgebra.Echelon(matrix); }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static int[,] ReducedEchelon(this int[,] matrix)
    { return LinearAlgebra.ReducedEchelon(matrix); }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static int Determinent(this int[,] matrix)
    { return LinearAlgebra.Determinent(matrix); }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static int[,] Inverse(this int[,] matrix)
    { return LinearAlgebra.Inverse(matrix); }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static int[,] Adjoint(this int[,] matrix)
    { return LinearAlgebra.Adjoint(matrix); }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static int[,] Transpose(this int[,] matrix)
    { return LinearAlgebra.Transpose(matrix); }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="lower">The computed lower triangular matrix.</param>
    /// <param name="upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(this int[,] matrix, out int[,] lower, out int[,] upper)
    { LinearAlgebra.DecomposeLU(matrix, out lower, out upper); }

    #endregion
  }

	#region LinearAlgebra providers

	public class LinearAlgebra_decimal : LinearAlgebra<decimal>
  {
    private LinearAlgebra_decimal() { _instance = this; }
    private static LinearAlgebra_decimal _instance;
    private static LinearAlgebra_decimal Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new LinearAlgebra_decimal();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "byte" types.</summary>
    public static LinearAlgebra_decimal Get { get { return Instance; } }

    /// <summary>Adds two vectors together.</summary>
    public decimal[] Add(decimal[] left, decimal[] right) { return LinearAlgebra.Add(left, right); }
    /// <summary>Negates all the values in a vector.</summary>
    public decimal[] Negate(decimal[] vector) { return LinearAlgebra.Negate(vector); }
    /// <summary>Subtracts two vectors.</summary>
    public decimal[] Subtract(decimal[] left, decimal[] right) { return LinearAlgebra.Subtract(left, right); }
    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    public decimal[] Multiply(decimal[] left, decimal right) { return LinearAlgebra.Multiply(left, right); }
    /// <summary>Divides all the components of a vector by a scalar.</summary>
    public decimal[] Divide(decimal[] vector, decimal right) { return LinearAlgebra.Divide(vector, right); }
    /// <summary>Computes the dot product between two vectors.</summary>
    public decimal DotProduct(decimal[] left, decimal[] right) { return LinearAlgebra.DotProduct(left, right); }
    /// <summary>Computes teh cross product of two vectors.</summary>
    public decimal[] CrossProduct(decimal[] left, decimal[] right) { return LinearAlgebra.CrossProduct(left, right); }
    /// <summary>Normalizes a vector.</summary>
    public decimal[] Normalize(decimal[] vector) { return LinearAlgebra.Normalize(vector); }
    /// <summary>Computes the length of a vector.</summary>
    public decimal Magnitude(decimal[] vector) { return LinearAlgebra.Magnitude(vector); }
    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    public decimal MagnitudeSquared(decimal[] vector) { return LinearAlgebra.MagnitudeSquared(vector); }
    /// <summary>Computes the angle between two vectors.</summary>
    public decimal Angle(decimal[] first, decimal[] second) { return LinearAlgebra.Angle(first, second); }
    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    public decimal[] RotateBy(decimal[] vector, decimal angle, decimal x, decimal y, decimal z) { return LinearAlgebra.RotateBy(vector, angle, x, y, z); }
    /// <summary>Computes the linear interpolation between two vectors.</summary>
    public decimal[] Lerp(decimal[] left, decimal[] right, decimal blend) { return LinearAlgebra.Lerp(left, right, blend); }
    /// <summary>Sphereically interpolates between two vectors.</summary>
    public decimal[] Slerp(decimal[] left, decimal[] right, decimal blend) { return LinearAlgebra.Slerp(left, right, blend); }
    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    public decimal[] Blerp(decimal[] a, decimal[] b, decimal[] c, decimal u, decimal v) { return LinearAlgebra.Blerp(a, b, c, u, v); }

    /// <summary>Negates all the values in this matrix.</summary>
    public decimal[,] Negate(decimal[,] matrix) { return LinearAlgebra.Negate(matrix); }
    /// <summary>Does a standard matrix addition.</summary>
    public decimal[,] Add(decimal[,] left, decimal[,] right) { return LinearAlgebra.Add(left, right); }
    /// <summary>Does a standard matrix multiplication (triple for loop).</summary>
    public decimal[,] Multiply(decimal[,] left, decimal[,] right) { return LinearAlgebra.Multiply(left, right); }
    /// <summary>Multiplies all the values in this matrix by a scalar.</summary>
    public decimal[,] Multiply(decimal[,] matrix, decimal scalar) { return LinearAlgebra.Multiply(matrix, scalar); }
    /// <summary>Divides all the values in this matrix by a scalar.</summary>
    public decimal[,] Divide(decimal[,] left, decimal right) { return LinearAlgebra.Divide(left, right); }
    /// <summary>Gets the minor of a matrix.</summary>
    public decimal[,] Minor(decimal[,] matrix, int row, int column) { return LinearAlgebra.Minor(matrix, row, column); }
    /// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    public decimal[,] ConcatenateRowWise(decimal[,] left, decimal[,] right) { return LinearAlgebra.ConcatenateRowWise(left, right); }
    /// <summary>Computes the determinent if this matrix is square.</summary>
    public decimal Determinent(decimal[,] matrix) { return LinearAlgebra.Determinent(matrix); }
    /// <summary>Computes the echelon form of this matrix (aka REF).</summary>
    public decimal[,] Echelon(decimal[,] matrix) { return LinearAlgebra.Echelon(matrix); }
    /// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
    public decimal[,] ReducedEchelon(decimal[,] matrix) { return LinearAlgebra.ReducedEchelon(matrix); }
    /// <summary>Computes the inverse of this matrix.</summary>
    public decimal[,] Inverse(decimal[,] matrix) { return LinearAlgebra.Inverse(matrix); }
    /// <summary>Gets the adjoint of this matrix.</summary>
    public decimal[,] Adjoint(decimal[,] matrix) { return LinearAlgebra.Adjoint(matrix); }
    /// <summary>Transposes this matrix.</summary>
    public decimal[,] Transpose(decimal[,] matrix) { return LinearAlgebra.Transpose(matrix); }
    /// <summary>Copies this matrix.</summary>
    public decimal[,] Clone(decimal[,] matrix) { return LinearAlgebra.Clone(matrix); }
  }

	public class LinearAlgebra_double : LinearAlgebra<double>
	{
		private LinearAlgebra_double() { _instance = this; }
		private static LinearAlgebra_double _instance;
		private static LinearAlgebra_double Instance
		{
			get
			{
				if (_instance == null)
					return _instance = new LinearAlgebra_double();
				else
					return _instance;
			}
		}

		/// <summary>Gets Arithmetic for "byte" types.</summary>
		public static LinearAlgebra_double Get { get { return Instance; } }

    /// <summary>Adds two vectors together.</summary>
    public double[] Add(double[] left, double[] right) { return LinearAlgebra.Add(left, right); }
    /// <summary>Negates all the values in a vector.</summary>
    public double[] Negate(double[] vector) { return LinearAlgebra.Negate(vector); }
    /// <summary>Subtracts two vectors.</summary>
    public double[] Subtract(double[] left, double[] right) { return LinearAlgebra.Subtract(left, right); }
    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    public double[] Multiply(double[] left, double right) { return LinearAlgebra.Multiply(left, right); }
    /// <summary>Divides all the components of a vector by a scalar.</summary>
    public double[] Divide(double[] vector, double right) { return LinearAlgebra.Divide(vector, right); }
    /// <summary>Computes the dot product between two vectors.</summary>
    public double DotProduct(double[] left, double[] right) { return LinearAlgebra.DotProduct(left, right); }
    /// <summary>Computes teh cross product of two vectors.</summary>
    public double[] CrossProduct(double[] left, double[] right) { return LinearAlgebra.CrossProduct(left, right); }
    /// <summary>Normalizes a vector.</summary>
    public double[] Normalize(double[] vector) { return LinearAlgebra.Normalize(vector); }
    /// <summary>Computes the length of a vector.</summary>
    public double Magnitude(double[] vector) { return LinearAlgebra.Magnitude(vector); }
    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    public double MagnitudeSquared(double[] vector) { return LinearAlgebra.MagnitudeSquared(vector); }
    /// <summary>Computes the angle between two vectors.</summary>
    public double Angle(double[] first, double[] second) { return LinearAlgebra.Angle(first, second); }
    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    public double[] RotateBy(double[] vector, double angle, double x, double y, double z) { return LinearAlgebra.RotateBy(vector, angle, x, y, z); }
    /// <summary>Computes the linear interpolation between two vectors.</summary>
    public double[] Lerp(double[] left, double[] right, double blend) { return LinearAlgebra.Lerp(left, right, blend); }
    /// <summary>Sphereically interpolates between two vectors.</summary>
    public double[] Slerp(double[] left, double[] right, double blend) { return LinearAlgebra.Slerp(left, right, blend); }
    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    public double[] Blerp(double[] a, double[] b, double[] c, double u, double v) { return LinearAlgebra.Blerp(a, b, c, u, v); }

		/// <summary>Negates all the values in this matrix.</summary>
		public double[,] Negate(double[,] matrix) { return LinearAlgebra.Negate(matrix); }
		/// <summary>Does a standard matrix addition.</summary>
		public double[,] Add(double[,] left, double[,] right) { return LinearAlgebra.Add(left, right); }
		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
		public double[,] Multiply(double[,] left, double[,] right) { return LinearAlgebra.Multiply(left, right); }
		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
		public double[,] Multiply(double[,] matrix, double scalar) { return LinearAlgebra.Multiply(matrix, scalar); }
		/// <summary>Divides all the values in this matrix by a scalar.</summary>
		public double[,] Divide(double[,] left, double right) { return LinearAlgebra.Divide(left, right); }
		/// <summary>Gets the minor of a matrix.</summary>
		public double[,] Minor(double[,] matrix, int row, int column) { return LinearAlgebra.Minor(matrix, row, column); }
		/// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		public double[,] ConcatenateRowWise(double[,] left, double[,] right) { return LinearAlgebra.ConcatenateRowWise(left, right); }
		/// <summary>Computes the determinent if this matrix is square.</summary>
		public double Determinent(double[,] matrix) { return LinearAlgebra.Determinent(matrix); }
		/// <summary>Computes the echelon form of this matrix (aka REF).</summary>
		public double[,] Echelon(double[,] matrix) { return LinearAlgebra.Echelon(matrix); }
		/// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
		public double[,] ReducedEchelon(double[,] matrix) { return LinearAlgebra.ReducedEchelon(matrix); }
		/// <summary>Computes the inverse of this matrix.</summary>
		public double[,] Inverse(double[,] matrix) { return LinearAlgebra.Inverse(matrix); }
		/// <summary>Gets the adjoint of this matrix.</summary>
		public double[,] Adjoint(double[,] matrix) { return LinearAlgebra.Adjoint(matrix); }
		/// <summary>Transposes this matrix.</summary>
		public double[,] Transpose(double[,] matrix) { return LinearAlgebra.Transpose(matrix); }
		/// <summary>Copies this matrix.</summary>
		public double[,] Clone(double[,] matrix) { return LinearAlgebra.Clone(matrix); }
	}

  public class LinearAlgebra_float : LinearAlgebra<float>
  {
    private LinearAlgebra_float() { _instance = this; }
    private static LinearAlgebra_float _instance;
    private static LinearAlgebra_float Instance
    {
      get
      {
        if (_instance == null)
          return _instance = new LinearAlgebra_float();
        else
          return _instance;
      }
    }

    /// <summary>Gets Arithmetic for "byte" types.</summary>
    public static LinearAlgebra_float Get { get { return Instance; } }

    /// <summary>Adds two vectors together.</summary>
    public float[] Add(float[] left, float[] right) { return LinearAlgebra.Add(left, right); }
    /// <summary>Negates all the values in a vector.</summary>
    public float[] Negate(float[] vector) { return LinearAlgebra.Negate(vector); }
    /// <summary>Subtracts two vectors.</summary>
    public float[] Subtract(float[] left, float[] right) { return LinearAlgebra.Subtract(left, right); }
    /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
    public float[] Multiply(float[] left, float right) { return LinearAlgebra.Multiply(left, right); }
    /// <summary>Divides all the components of a vector by a scalar.</summary>
    public float[] Divide(float[] vector, float right) { return LinearAlgebra.Divide(vector, right); }
    /// <summary>Computes the dot product between two vectors.</summary>
    public float DotProduct(float[] left, float[] right) { return LinearAlgebra.DotProduct(left, right); }
    /// <summary>Computes teh cross product of two vectors.</summary>
    public float[] CrossProduct(float[] left, float[] right) { return LinearAlgebra.CrossProduct(left, right); }
    /// <summary>Normalizes a vector.</summary>
    public float[] Normalize(float[] vector) { return LinearAlgebra.Normalize(vector); }
    /// <summary>Computes the length of a vector.</summary>
    public float Magnitude(float[] vector) { return LinearAlgebra.Magnitude(vector); }
    /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
    public float MagnitudeSquared(float[] vector) { return LinearAlgebra.MagnitudeSquared(vector); }
    /// <summary>Computes the angle between two vectors.</summary>
    public float Angle(float[] first, float[] second) { return LinearAlgebra.Angle(first, second); }
    /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
    public float[] RotateBy(float[] vector, float angle, float x, float y, float z) { return LinearAlgebra.RotateBy(vector, angle, x, y, z); }
    /// <summary>Computes the linear interpolation between two vectors.</summary>
    public float[] Lerp(float[] left, float[] right, float blend) { return LinearAlgebra.Lerp(left, right, blend); }
    /// <summary>Sphereically interpolates between two vectors.</summary>
    public float[] Slerp(float[] left, float[] right, float blend) { return LinearAlgebra.Slerp(left, right, blend); }
    /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
    public float[] Blerp(float[] a, float[] b, float[] c, float u, float v) { return LinearAlgebra.Blerp(a, b, c, u, v); }

    /// <summary>Negates all the values in this matrix.</summary>
    public float[,] Negate(float[,] matrix) { return LinearAlgebra.Negate(matrix); }
    /// <summary>Does a standard matrix addition.</summary>
    public float[,] Add(float[,] left, float[,] right) { return LinearAlgebra.Add(left, right); }
    /// <summary>Does a standard matrix multiplication (triple for loop).</summary>
    public float[,] Multiply(float[,] left, float[,] right) { return LinearAlgebra.Multiply(left, right); }
    /// <summary>Multiplies all the values in this matrix by a scalar.</summary>
    public float[,] Multiply(float[,] matrix, float scalar) { return LinearAlgebra.Multiply(matrix, scalar); }
    /// <summary>Divides all the values in this matrix by a scalar.</summary>
    public float[,] Divide(float[,] left, float right) { return LinearAlgebra.Divide(left, right); }
    /// <summary>Gets the minor of a matrix.</summary>
    public float[,] Minor(float[,] matrix, int row, int column) { return LinearAlgebra.Minor(matrix, row, column); }
    /// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    public float[,] ConcatenateRowWise(float[,] left, float[,] right) { return LinearAlgebra.ConcatenateRowWise(left, right); }
    /// <summary>Computes the determinent if this matrix is square.</summary>
    public float Determinent(float[,] matrix) { return LinearAlgebra.Determinent(matrix); }
    /// <summary>Computes the echelon form of this matrix (aka REF).</summary>
    public float[,] Echelon(float[,] matrix) { return LinearAlgebra.Echelon(matrix); }
    /// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
    public float[,] ReducedEchelon(float[,] matrix) { return LinearAlgebra.ReducedEchelon(matrix); }
    /// <summary>Computes the inverse of this matrix.</summary>
    public float[,] Inverse(float[,] matrix) { return LinearAlgebra.Inverse(matrix); }
    /// <summary>Gets the adjoint of this matrix.</summary>
    public float[,] Adjoint(float[,] matrix) { return LinearAlgebra.Adjoint(matrix); }
    /// <summary>Transposes this matrix.</summary>
    public float[,] Transpose(float[,] matrix) { return LinearAlgebra.Transpose(matrix); }
    /// <summary>Copies this matrix.</summary>
    public float[,] Clone(float[,] matrix) { return LinearAlgebra.Clone(matrix); }
  }

  #region Unfinished long

  //public class LinearAlgebra_long : LinearAlgebra<long>
  //{
  //  private LinearAlgebra_long() { _instance = this; }
  //  private static LinearAlgebra_long _instance;
  //  private static LinearAlgebra_long Instance
  //  {
  //    get
  //    {
  //      if (_instance == null)
  //        return _instance = new LinearAlgebra_long();
  //      else
  //        return _instance;
  //    }
  //  }

  //  /// <summary>Gets Arithmetic for "byte" types.</summary>
  //  public static LinearAlgebra_long Get { get { return Instance; } }

  //  /// <summary>Adds two vectors together.</summary>
  //  public long[] Add(long[] left, long[] right) { return LinearAlgebra.Add(left, right); }
  //  /// <summary>Negates all the values in a vector.</summary>
  //  public long[] Negate(long[] vector) { return LinearAlgebra.Negate(vector); }
  //  /// <summary>Subtracts two vectors.</summary>
  //  public long[] Subtract(long[] left, long[] right) { return LinearAlgebra.Subtract(left, right); }
  //  /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
  //  public long[] Multiply(long[] left, long right) { return LinearAlgebra.Multiply(left, right); }
  //  /// <summary>Divides all the components of a vector by a scalar.</summary>
  //  public long[] Divide(long[] vector, long right) { return LinearAlgebra.Divide(vector, right); }
  //  /// <summary>Computes the dot product between two vectors.</summary>
  //  public long DotProduct(long[] left, long[] right) { return LinearAlgebra.DotProduct(left, right); }
  //  /// <summary>Computes teh cross product of two vectors.</summary>
  //  public long[] CrossProduct(long[] left, long[] right) { return LinearAlgebra.CrossProduct(left, right); }
  //  /// <summary>Normalizes a vector.</summary>
  //  public long[] Normalize(long[] vector) { return LinearAlgebra.Normalize(vector); }
  //  /// <summary>Computes the length of a vector.</summary>
  //  public long Magnitude(long[] vector) { return LinearAlgebra.Magnitude(vector); }
  //  /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
  //  public long MagnitudeSquared(long[] vector) { return LinearAlgebra.MagnitudeSquared(vector); }
  //  /// <summary>Computes the angle between two vectors.</summary>
  //  public long Angle(long[] first, long[] second) { return LinearAlgebra.Angle(first, second); }
  //  /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
  //  public long[] RotateBy(long[] vector, long angle, long x, long y, long z) { return LinearAlgebra.RotateBy(vector, angle, x, y, z); }
  //  /// <summary>Computes the linear interpolation between two vectors.</summary>
  //  public long[] Lerp(long[] left, long[] right, long blend) { return LinearAlgebra.Lerp(left, right, blend); }
  //  /// <summary>Sphereically interpolates between two vectors.</summary>
  //  public long[] Slerp(long[] left, long[] right, long blend) { return LinearAlgebra.Slerp(left, right, blend); }
  //  /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
  //  public long[] Blerp(long[] a, long[] b, long[] c, long u, long v) { return LinearAlgebra.Blerp(a, b, c, u, v); }

  //  /// <summary>Negates all the values in this matrix.</summary>
  //  public long[,] Negate(long[,] matrix) { return LinearAlgebra.Negate(matrix); }
  //  /// <summary>Does a standard matrix addition.</summary>
  //  public long[,] Add(long[,] left, long[,] right) { return LinearAlgebra.Add(left, right); }
  //  /// <summary>Does a standard matrix multiplication (triple for loop).</summary>
  //  public long[,] Multiply(long[,] left, long[,] right) { return LinearAlgebra.Multiply(left, right); }
  //  /// <summary>Multiplies all the values in this matrix by a scalar.</summary>
  //  public long[,] Multiply(long[,] matrix, long scalar) { return LinearAlgebra.Multiply(matrix, scalar); }
  //  /// <summary>Divides all the values in this matrix by a scalar.</summary>
  //  public long[,] Divide(long[,] left, long right) { return LinearAlgebra.Divide(left, right); }
  //  /// <summary>Gets the minor of a matrix.</summary>
  //  public long[,] Minor(long[,] matrix, int row, int column) { return LinearAlgebra.Minor(matrix, row, column); }
  //  /// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
  //  public long[,] ConcatenateRowWise(long[,] left, long[,] right) { return LinearAlgebra.ConcatenateRowWise(left, right); }
  //  /// <summary>Computes the determinent if this matrix is square.</summary>
  //  public long Determinent(long[,] matrix) { return LinearAlgebra.Determinent(matrix); }
  //  /// <summary>Computes the echelon form of this matrix (aka REF).</summary>
  //  public long[,] Echelon(long[,] matrix) { return LinearAlgebra.Echelon(matrix); }
  //  /// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
  //  public long[,] ReducedEchelon(long[,] matrix) { return LinearAlgebra.ReducedEchelon(matrix); }
  //  /// <summary>Computes the inverse of this matrix.</summary>
  //  public long[,] Inverse(long[,] matrix) { return LinearAlgebra.Inverse(matrix); }
  //  /// <summary>Gets the adjoint of this matrix.</summary>
  //  public long[,] Adjoint(long[,] matrix) { return LinearAlgebra.Adjoint(matrix); }
  //  /// <summary>Transposes this matrix.</summary>
  //  public long[,] Transpose(long[,] matrix) { return LinearAlgebra.Transpose(matrix); }
  //  /// <summary>Copies this matrix.</summary>
  //  public long[,] Clone(long[,] matrix) { return LinearAlgebra.Clone(matrix); }
  //}

  #endregion

  #region Unfinishhed int

  //public class LinearAlgebra_int : LinearAlgebra<int>
  //{
  //  private LinearAlgebra_int() { _instance = this; }
  //  private static LinearAlgebra_int _instance;
  //  private static LinearAlgebra_int Instance
  //  {
  //    get
  //    {
  //      if (_instance == null)
  //        return _instance = new LinearAlgebra_int();
  //      else
  //        return _instance;
  //    }
  //  }

  //  /// <summary>Gets Arithmetic for "byte" types.</summary>
  //  public static LinearAlgebra_int Get { get { return Instance; } }

  //  /// <summary>Adds two vectors together.</summary>
  //  public int[] Add(int[] left, int[] right) { return LinearAlgebra.Add(left, right); }
  //  /// <summary>Negates all the values in a vector.</summary>
  //  public int[] Negate(int[] vector) { return LinearAlgebra.Negate(vector); }
  //  /// <summary>Subtracts two vectors.</summary>
  //  public int[] Subtract(int[] left, int[] right) { return LinearAlgebra.Subtract(left, right); }
  //  /// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
  //  public int[] Multiply(int[] left, int right) { return LinearAlgebra.Multiply(left, right); }
  //  /// <summary>Divides all the components of a vector by a scalar.</summary>
  //  public int[] Divide(int[] vector, int right) { return LinearAlgebra.Divide(vector, right); }
  //  /// <summary>Computes the dot product between two vectors.</summary>
  //  public int DotProduct(int[] left, int[] right) { return LinearAlgebra.DotProduct(left, right); }
  //  /// <summary>Computes teh cross product of two vectors.</summary>
  //  public int[] CrossProduct(int[] left, int[] right) { return LinearAlgebra.CrossProduct(left, right); }
  //  /// <summary>Normalizes a vector.</summary>
  //  public int[] Normalize(int[] vector) { return LinearAlgebra.Normalize(vector); }
  //  /// <summary>Computes the length of a vector.</summary>
  //  public int Magnitude(int[] vector) { return LinearAlgebra.Magnitude(vector); }
  //  /// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
  //  public int MagnitudeSquared(int[] vector) { return LinearAlgebra.MagnitudeSquared(vector); }
  //  /// <summary>Computes the angle between two vectors.</summary>
  //  public int Angle(int[] first, int[] second) { return LinearAlgebra.Angle(first, second); }
  //  /// <summary>Rotates a vector by the specified axis and rotation values.</summary>
  //  public int[] RotateBy(int[] vector, int angle, int x, int y, int z) { return LinearAlgebra.RotateBy(vector, angle, x, y, z); }
  //  /// <summary>Computes the linear interpolation between two vectors.</summary>
  //  public int[] Lerp(int[] left, int[] right, int blend) { return LinearAlgebra.Lerp(left, right, blend); }
  //  /// <summary>Sphereically interpolates between two vectors.</summary>
  //  public int[] Slerp(int[] left, int[] right, int blend) { return LinearAlgebra.Slerp(left, right, blend); }
  //  /// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
  //  public int[] Blerp(int[] a, int[] b, int[] c, int u, int v) { return LinearAlgebra.Blerp(a, b, c, u, v); }

  //  /// <summary>Negates all the values in this matrix.</summary>
  //  public int[,] Negate(int[,] matrix) { return LinearAlgebra.Negate(matrix); }
  //  /// <summary>Does a standard matrix addition.</summary>
  //  public int[,] Add(int[,] left, int[,] right) { return LinearAlgebra.Add(left, right); }
  //  /// <summary>Does a standard matrix multiplication (triple for loop).</summary>
  //  public int[,] Multiply(int[,] left, int[,] right) { return LinearAlgebra.Multiply(left, right); }
  //  /// <summary>Multiplies all the values in this matrix by a scalar.</summary>
  //  public int[,] Multiply(int[,] matrix, int scalar) { return LinearAlgebra.Multiply(matrix, scalar); }
  //  /// <summary>Divides all the values in this matrix by a scalar.</summary>
  //  public int[,] Divide(int[,] left, int right) { return LinearAlgebra.Divide(left, right); }
  //  /// <summary>Gets the minor of a matrix.</summary>
  //  public int[,] Minor(int[,] matrix, int row, int column) { return LinearAlgebra.Minor(matrix, row, column); }
  //  /// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
  //  public int[,] ConcatenateRowWise(int[,] left, int[,] right) { return LinearAlgebra.ConcatenateRowWise(left, right); }
  //  /// <summary>Computes the determinent if this matrix is square.</summary>
  //  public int Determinent(int[,] matrix) { return LinearAlgebra.Determinent(matrix); }
  //  /// <summary>Computes the echelon form of this matrix (aka REF).</summary>
  //  public int[,] Echelon(int[,] matrix) { return LinearAlgebra.Echelon(matrix); }
  //  /// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
  //  public int[,] ReducedEchelon(int[,] matrix) { return LinearAlgebra.ReducedEchelon(matrix); }
  //  /// <summary>Computes the inverse of this matrix.</summary>
  //  public int[,] Inverse(int[,] matrix) { return LinearAlgebra.Inverse(matrix); }
  //  /// <summary>Gets the adjoint of this matrix.</summary>
  //  public int[,] Adjoint(int[,] matrix) { return LinearAlgebra.Adjoint(matrix); }
  //  /// <summary>Transposes this matrix.</summary>
  //  public int[,] Transpose(int[,] matrix) { return LinearAlgebra.Transpose(matrix); }
  //  /// <summary>Copies this matrix.</summary>
  //  public int[,] Clone(int[,] matrix) { return LinearAlgebra.Clone(matrix); }
  //}

  #endregion

	#endregion

	/// <summary>Represents a vector with an arbitrary number of components.</summary>
	/// <typeparam name="T">The numeric type of this Vector.</typeparam>
	public class Vector<T>
	{
		// Constants
    private static T _zero;
    private static T _one;
    private static T _two;

		// Logic
    private static Logic._Abs<T> _abs;
    private static Equate<T> _equate;
    private static Compare<T> _compare;

		// Arithmetic Operations
    private static Arithmetic._Negate<T> _negate;
    private static Arithmetic._Add<T> _add;
    private static Arithmetic._Subtract<T> _subtract;
    private static Arithmetic._Multiply<T> _multiply;
    private static Arithmetic._Divide<T> _divide;

		// Algebra Operations
    private static Algebra._sqrt<T> _sqrt;
    private static Algebra._Invert_Multiplicative<T> _Invert_Multiplicative;

		// Trigonometry
    private static Trigonometry._sin<T> _sin;
    private static Trigonometry._cos<T> _cos;
    private static Trigonometry._arccos<T> _acos;

    static Vector()
    {
      // Constants
      _zero = Constants.Get<T>().factory(0);
      _one = Constants.Get<T>().factory(1);
      _two = Constants.Get<T>().factory(2);
      
      // Logic
      _abs = Logic.Get<T>().Abs;
      _equate = Logic.Get<T>().Equate;
      _compare = Logic.Get<T>().Compare;
      
      // Arithmetic
      _negate = Arithmetic.Get<T>().Negate;
      _add = Arithmetic.Get<T>().Add;
      _subtract = Arithmetic.Get<T>().Subtract;
      _multiply = Arithmetic.Get<T>().Multiply;
      _divide = Arithmetic.Get<T>().Divide;
      
      // Algebra
      _sqrt = Algebra.Get<T>().sqrt;
      _Invert_Multiplicative = Algebra.Get<T>().Invert_Multiplicative;
   
      // Trigonometry
      _sin = Trigonometry.Get<T>().sin;
      _cos = Trigonometry.Get<T>().cos;
      _acos = Trigonometry.Get<T>().arccos;
    }

		public readonly T[] _vector;

		/// <summary>Sane as accessing index 0.</summary>
		public T X
		{
			get { return _vector[0]; }
			set { _vector[0] = value; }
		}

		/// <summary>Same as accessing index 1.</summary>
		public T Y
		{
			get { try { return _vector[1]; } catch { throw new Error("vector does not contains a y component."); } }
			set { try { _vector[1] = value; } catch { throw new Error("vector does not contains a y component."); } }
		}

		/// <summary>Same as accessing index 2.</summary>
		public T Z
		{
			get { try { return _vector[2]; } catch { throw new Error("vector does not contains a z component."); } }
			set { try { _vector[2] = value; } catch { throw new Error("vector does not contains a z component."); } }
		}

		/// <summary>Same as accessing index 3.</summary>
		public T W
		{
			get { try { return _vector[3]; } catch { throw new Error("vector does not contains a w component."); } }
			set { try { _vector[3] = value; } catch { throw new Error("vector does not contains a w component."); } }
		}
		
		/// <summary>The number of components in this vector.</summary>
		public int Dimensions { get { return _vector.Length; } }

		/// <summary>Allows indexed access to this vector.</summary>
		/// <param name="index">The index to access.</param>
		/// <returns>The value of the given index.</returns>
		public T this[int index]
		{
			get { try { return _vector[index]; } catch { throw new Error("index out of bounds."); } }
			set { try { _vector[index] = value; } catch { throw new Error("index out of bounds."); } }
		}

		/// <summary>Creates a new vector with the given number of components.</summary>
		/// <param name="dimensions">The number of dimensions this vector will have.</param>
		public Vector(int dimensions) { try { _vector = new T[dimensions]; } catch { throw new Error("invalid dimensions on vector contruction."); } }

		/// <summary>Creates a vector out of the given values.</summary>
		/// <param name="vector">The values to initialize the vector to.</param>
		public Vector(params T[] vector)
		{
			_vector = vector;
		}

		/// <summary>Creates a vector with the given number of components with the values initialized to zeroes.</summary>
		/// <param name="dimensions">The number of components in the vector.</param>
		/// <returns>The newly constructed vector.</returns>
		public static Vector<T> FactoryZero(int dimensions) { return new Vector<T>(dimensions); }

		/// <summary>Creates a vector with the given number of components with the values initialized to ones.</summary>
		/// <param name="dimensions">The number of components in the vector.</param>
		/// <returns>The newly constructed vector.</returns>
		public static Vector<T> FactoryOne(int dimensions) { return new Vector<T>(new T[dimensions]); }

		/// <summary>Returns a 3-component vector representing the x-axis.</summary>
		public static readonly Vector<T> FactoryXAxis = new Vector<T>(_one, _zero, _zero);
		/// <summary>Returns a 3-component vector representing the y-axis.</summary>
		public static readonly Vector<T> FactoryYAxis = new Vector<T>(_zero, _one, _zero);
		/// <summary>Returns a 3-component vector representing the z-axis.</summary>
		public static readonly Vector<T> FactoryZAxis = new Vector<T>(_zero, _zero, _one);
		/// <summary>Returns a 3-component vector representing the negative x-axis.</summary>
		public static readonly Vector<T> FactoryNegXAxis = new Vector<T>(_one, _zero, _zero);
		/// <summary>Returns a 3-component vector representing the negative y-axis.</summary>
		public static readonly Vector<T> FactoryNegYAxis = new Vector<T>(_zero, _one, _zero);
		/// <summary>Returns a 3-component vector representing the negative z-axis.</summary>
		public static readonly Vector<T> FactoryNegZAxis = new Vector<T>(_zero, _zero, _one);

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector of the addition.</param>
		/// <param name="right">The second vector of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Vector<T> operator +(Vector<T> left, Vector<T> right) { return Vector<T>.Add(left, right); }
		/// <summary>Subtracts two vectors.</summary>
		/// <param name="left">The left operand of the subtraction.</param>
		/// <param name="right">The right operand of the subtraction.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Vector<T> operator -(Vector<T> left, Vector<T> right) { return Vector<T>.Subtract(left, right); }
		/// <summary>Negates a vector.</summary>
		/// <param name="vector">The vector to negate.</param>
		/// <returns>The result of the negation.</returns>
		public static Vector<T> operator -(Vector<T> vector) { return Vector<T>.Negate(vector); }
		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="left">The vector to have all its values multiplied.</param>
		/// <param name="right">The scalar to multiply all the vector values by.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> operator *(Vector<T> left, T right) { return Vector<T>.Multiply(left, right); }
		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
		/// <param name="left">The scalar to multiply all the vector values by.</param>
		/// <param name="right">The vector to have all its values multiplied.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Vector<T> operator *(T left, Vector<T> right) { return Vector<T>.Multiply(right, left); }
		/// <summary>Divides all the values in the vector by a scalar.</summary>
		/// <param name="left">The vector to have its values divided.</param>
		/// <param name="right">The scalar to divide all the vectors values by.</param>
		/// <returns>The vector after the divisions.</returns>
		public static Vector<T> operator /(Vector<T> left, T right) { return Vector<T>.Divide(left, right); }
		/// <summary>Does an equality check by value. (warning for float errors)</summary>
		/// <param name="left">The first vector of the equality check.</param>
		/// <param name="right">The second vector of the equality check.</param>
		/// <returns>true if the values are equal, false if not.</returns>
		public static bool operator ==(Vector<T> left, Vector<T> right) { return Vector<T>.EqualsValue(left, right); }
		/// <summary>Does an anti-equality check by value. (warning for float errors)</summary>
		/// <param name="left">The first vector of the anit-equality check.</param>
		/// <param name="right">The second vector of the anti-equality check.</param>
		/// <returns>true if the values are not equal, false if they are.</returns>
		public static bool operator !=(Vector<T> left, Vector<T> right) { return !Vector<T>.EqualsValue(left, right); }
		/// <summary>Implicit conversions from Vector to T[].</summary>
		/// <param name="vector">The Vector to be converted to a T[].</param>
		/// <returns>The T[] of the vector.</returns>
		public static implicit operator T[](Vector<T> vector) { return vector._vector; }
		/// <summary>Implicit conversions from Vector to T[].</summary>
		/// <param name="vector">The Vector to be converted to a T[].</param>
		/// <returns>The T[] of the vector.</returns>
		public static implicit operator Vector<T>(T[] array) { return new Vector<T>(array); }

		/// <summary>Adds two vectors together.</summary>
		/// <param name="right">The vector to add to this one.</param>
		/// <returns>The result of the vector.</returns>
		public Vector<T> Add(Vector<T> right) { return Vector<T>.Add(this, right); }
		/// <summary>Negates this vector.</summary>
		/// <returns>The result of the negation.</returns>
		public Vector<T> Negate() { return Vector<T>.Negate(this); }
		/// <summary>Subtracts another vector from this one.</summary>
		/// <param name="right">The vector to subtract from this one.</param>
		/// <returns>The result of the subtraction.</returns>
		public Vector<T> Subtract(Vector<T> right) { return Vector<T>.Subtract(this, right); }
		/// <summary>Multiplies the values in this vector by a scalar.</summary>
		/// <param name="right">The scalar to multiply these values by.</param>
		/// <returns>The result of the multiplications</returns>
		public Vector<T> Multiply(T right) { return Vector<T>.Multiply(this, right); }
		/// <summary>Divides all the values in this vector by a scalar.</summary>
		/// <param name="right">The scalar to divide the values of the vector by.</param>
		/// <returns>The resulting vector after teh divisions.</returns>
		public Vector<T> Divide(T right) { return Vector<T>.Divide(this, right); }
		/// <summary>Computes the dot product between this vector and another.</summary>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product.</returns>
		public T DotProduct(Vector<T> right) { return Vector<T>.DotProduct(this, right); }
		/// <summary>Computes the cross product between this vector and another.</summary>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public Vector<T> CrossProduct(Vector<T> right) { return Vector<T>.CrossProduct(this, right); }
		/// <summary>Normalizes this vector.</summary>
		/// <returns>The result of the normalization.</returns>
		public Vector<T> Normalize() { return Vector<T>.Normalize(this); }
		/// <summary>Computes the length of this vector.</summary>
		/// <returns>The length of this vector.</returns>
		public T Length() { return Vector<T>.Length(this); }
		/// <summary>Computes the length of this vector, but doesn't square root it for 
		/// possible optimization purposes.</summary>
		/// <returns>The squared length of the vector.</returns>
		public T LengthSquared() { return Vector<T>.LengthSquared(this); }
		/// <summary>Check for equality by value.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <returns>true if the values were equal, false if not.</returns>
		public bool EqualsValue(Vector<T> right) { return Vector<T>.EqualsValue(this, right); }
		/// <summary>Checks for equality by value with some leniency.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <param name="leniency">The ammount the values can differ but still be considered equal.</param>
		/// <returns>true if the values were cinsidered equal, false if not.</returns>
		public bool EqualsValue(Vector<T> right, T leniency) { return Vector<T>.EqualsValue(this, right, leniency); }
		/// <summary>Checks for equality by reference.</summary>
		/// <param name="right">The other vector of the equality check.</param>
		/// <returns>true if the references are equal, false if not.</returns>
		public bool EqualsReference(Vector<T> right) { return Vector<T>.EqualsReference(this, right); }
		/// <summary>Rotates this vector by quaternon values.</summary>
		/// <param name="angle">The amount of rotation about the axis.</param>
		/// <param name="x">The x component deterniming the axis of rotation.</param>
		/// <param name="y">The y component determining the axis of rotation.</param>
		/// <param name="z">The z component determining the axis of rotation.</param>
		/// <returns>The resulting vector after the rotation.</returns>
		public Vector<T> RotateBy(T angle, T x, T y, T z) { return Vector<T>.RotateBy(this, angle, x, y, z); }
		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public Vector<T> Lerp(Vector<T> right, T blend) { return Vector<T>.Lerp(this, right, blend); }
		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public Vector<T> Slerp(Vector<T> right, T blend) { return Vector<T>.Slerp(this, right, blend); }
		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
		/// <returns>The result of the rotation.</returns>
		//public Vector<T> RotateBy(Quaternion<T> rotation) { return Vector<T>.RotateBy(this, rotation); }

		/// <summary>Adds two vectors together.</summary>
		/// <param name="leftFloats">The first vector of the addition.</param>
		/// <param name="rightFloats">The second vector of the addiiton.</param>
		/// <returns>The result of the addiion.</returns>
		public static Vector<T> Add(T[] left, T[] right)
		{
			if (left.Length != right.Length)
				throw new Error("invalid dimensions on vector addition.");
			T[] result = new T[left.Length];
			for (int i = 0; i < left.Length; i++)
				result[i] = _add(left[i], right[i]);
			return result;
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector of the addition.</param>
		/// <param name="right">The second vector of the addiiton.</param>
		/// <returns>The result of the addiion.</returns>
		public static Vector<T> Add(Vector<T> left, Vector<T> right)
		{
			return Vector<T>.Add(left._vector, right._vector);
		}

		/// <summary>Negates all the values in a vector.</summary>
		/// <param name="vector">The vector to have its values negated.</param>
		/// <returns>The result of the negations.</returns>
		public static T[] Negate(T[] vector)
		{
			Vector<T> result = new T[vector.Length];
			for (int i = 0; i < vector.Length; i++)
				result[i] = _negate(vector[i]);
			return result;
		}

		/// <summary>Negates all the values in a vector.</summary>
		/// <param name="vector">The vector to have its values negated.</param>
		/// <returns>The result of the negations.</returns>
		public static Vector<T> Negate(Vector<T> vector)
		{
			return Vector<T>.Negate(vector._vector);
		}

		/// <summary>Subtracts two vectors.</summary>
		/// <param name="left">The left vector of the subtraction.</param>
		/// <param name="right">The right vector of the subtraction.</param>
		/// <returns>The result of the vector subtracton.</returns>
		public static T[] Subtract(T[] left, T[] right)
		{
			T[] result = new T[left.Length];
			if (left.Length != right.Length)
				throw new Error("invalid dimensions on vector subtraction.");
			for (int i = 0; i < left.Length; i++)
				result[i] = _subtract(left[i], right[i]);
			return result;
		}

		/// <summary>Subtracts two vectors.</summary>
		/// <param name="left">The left vector of the subtraction.</param>
		/// <param name="right">The right vector of the subtraction.</param>
		/// <returns>The result of the vector subtracton.</returns>
		public static Vector<T> Subtract(Vector<T> left, Vector<T> right)
		{
			return Vector<T>.Subtract(left._vector, right._vector);
		}

		/// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
		/// <param name="left">The vector to have the components multiplied by.</param>
		/// <param name="right">The scalars to multiply the vector components by.</param>
		/// <returns>The result of the multiplications.</returns>
		public static T[] Multiply(T[] left, T right)
		{
			T[] result = new T[left.Length];
			for (int i = 0; i < left.Length; i++)
				result[i] = _multiply(left[i], right);
			return result;
		}

		/// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
		/// <param name="left">The vector to have the components multiplied by.</param>
		/// <param name="right">The scalars to multiply the vector components by.</param>
		/// <returns>The result of the multiplications.</returns>
		public static Vector<T> Multiply(Vector<T> left, T right)
		{
			return Vector<T>.Multiply(left._vector, right);
		}

		/// <summary>Divides all the components of a vector by a scalar.</summary>
		/// <param name="left">The vector to have the components divided by.</param>
		/// <param name="right">The scalar to divide the vector components by.</param>
		/// <returns>The resulting vector after teh divisions.</returns>
		public static T[] Divide(T[] left, T right)
		{
			T[] result = new T[left.Length];
			for (int i = 0; i < left.Length; i++)
				result[i] = _divide(left[i], right);
			return result;
		}

		/// <summary>Divides all the components of a vector by a scalar.</summary>
		/// <param name="left">The vector to have the components divided by.</param>
		/// <param name="right">The scalar to divide the vector components by.</param>
		/// <returns>The resulting vector after teh divisions.</returns>
		public static Vector<T> Divide(Vector<T> left, T right)
		{
			return Vector<T>.Divide(left._vector, right);
		}

		/// <summary>Computes the dot product between two vectors.</summary>
		/// <param name="left">The first vector of the dot product operation.</param>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public static T DotProduct(T[] left, T[] right)
		{
			if (left.Length != right.Length)
				throw new Error("invalid dimensions on vector dot product.");
			T result = _zero;
			for (int i = 0; i < left.Length; i++)
				result = _add(result, _multiply(left[i], right[i]));
			return result;
		}

		/// <summary>Computes the dot product between two vectors.</summary>
		/// <param name="left">The first vector of the dot product operation.</param>
		/// <param name="right">The second vector of the dot product operation.</param>
		/// <returns>The result of the dot product operation.</returns>
		public static T DotProduct(Vector<T> left, Vector<T> right)
		{
			return Vector<T>.DotProduct(left._vector, right._vector);
		}

		/// <summary>Computes teh cross product of two vectors.</summary>
		/// <param name="left">The first vector of the cross product operation.</param>
		/// <param name="right">The second vector of the cross product operation.</param>
		/// <returns>The result of the cross product operation.</returns>
		public static T[] CrossProduct(T[] left, T[] right)
		{
			if (left.Length != right.Length)
				throw new Error("invalid cross product !(left.Dimensions == right.Dimensions)");
			if (left.Length == 3 || left.Length == 4)
			{
				return new Vector<T>(
					_subtract(_multiply(left[1], right[2]), _multiply(left[2], right[1])),
					_subtract(_multiply(left[2], right[0]), _multiply(left[0], right[2])),
					_subtract(_multiply(left[0], right[1]), _multiply(left[1], right[0])));
			}
			throw new Error("my cross product function is only defined for 3-component vectors.");
		}

		/// <summary>Computes teh cross product of two vectors.</summary>
		/// <param name="left">The first vector of the cross product operation.</param>
		/// <param name="right">The second vector of the cross product operation.</param>
		/// <returns>The result of the cross product operation.</returns>
		public static Vector<T> CrossProduct(Vector<T> left, Vector<T> right)
		{
			return Vector<T>.CrossProduct(left._vector, right._vector);
		}

		/// <summary>Normalizes a vector.</summary>
		/// <param name="vector">The vector to normalize.</param>
		/// <returns>The result of the normalization.</returns>
		public static T[] Normalize(T[] vector)
		{
			T length = Vector<T>.Length(vector);
			if (!_equate(length, _zero))
			{
				T[] result = new T[vector.Length];
				for (int i = 0; i < vector.Length; i++)
					result[i] = _divide(vector[i], length);
				return result;
			}
			else
				return new T[vector.Length];
		}

		/// <summary>Normalizes a vector.</summary>
		/// <param name="vector">The vector to normalize.</param>
		/// <returns>The result of the normalization.</returns>
		public static Vector<T> Normalize(Vector<T> vector)
		{
			return Vector<T>.Normalize(vector._vector);
		}

		/// <summary>Computes the length of a vector.</summary>
		/// <param name="vector">The vector to calculate the length of.</param>
		/// <returns>The computed length of the vector.</returns>
		public static T Length(T[] vector)
		{
			T result = _zero;
			for (int i = 0; i < vector.Length; i++)
				result = _add(result, _multiply(vector[i], vector[i]));
			return _sqrt(result);
		}

		/// <summary>Computes the length of a vector.</summary>
		/// <param name="vector">The vector to calculate the length of.</param>
		/// <returns>The computed length of the vector.</returns>
		public static T Length(Vector<T> vector)
		{
			return Vector<T>.Length(vector._vector);
		}

		/// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
		/// <param name="vector">The vector to compute the length squared of.</param>
		/// <returns>The computed length squared of the vector.</returns>
		public static T LengthSquared(T[] vector)
		{
			T result = _zero;
			for (int i = 0; i < vector.Length; i++)
				result = _add(result, _multiply(vector[i], vector[i]));
			return result;
		}

		/// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
		/// <param name="vector">The vector to compute the length squared of.</param>
		/// <returns>The computed length squared of the vector.</returns>
		public static T LengthSquared(Vector<T> vector)
		{
			return Vector<T>.LengthSquared(vector._vector);
		}

		/// <summary>Computes the angle between two vectors.</summary>
		/// <param name="first">The first vector to determine the angle between.</param>
		/// <param name="second">The second vector to determine the angle between.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public static T Angle(T[] first, T[] second)
		{
			return _acos(_divide(Vector<T>.DotProduct(first, second), (_multiply(Vector<T>.Length(first), Vector<T>.Length(second)))));
		}

		/// <summary>Computes the angle between two vectors.</summary>
		/// <param name="first">The first vector to determine the angle between.</param>
		/// <param name="second">The second vector to determine the angle between.</param>
		/// <returns>The angle between the two vectors in radians.</returns>
		public static T Angle(Vector<T> first, Vector<T> second)
		{
			return Vector<T>.Angle(first._vector, second._vector);
		}

		/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="angle">The angle of the rotation.</param>
		/// <param name="x">The x component of the axis vector to rotate about.</param>
		/// <param name="y">The y component of the axis vector to rotate about.</param>
		/// <param name="z">The z component of the axis vector to rotate about.</param>
		/// <returns>The result of the rotation.</returns>
		public static T[] RotateBy(T[] vector, T angle, T x, T y, T z)
		{
			if (vector.Length == 3)
			{
				// Note: the angle is in radians
				T sinHalfAngle = _sin(_divide(angle, _two));
				T cosHalfAngle = _cos(_divide(angle, _two));
				x = _multiply(x, sinHalfAngle);
				y = _multiply(y, sinHalfAngle);
				z = _multiply(z, sinHalfAngle);
				T x2 = _subtract(_add(_multiply(cosHalfAngle, vector[0]), _multiply(y, vector[2])), _multiply(z, vector[1]));
				T y2 = _subtract(_add(_multiply(cosHalfAngle, vector[1]), _multiply(z, vector[0])), _multiply(x, vector[2]));
				T z2 = _subtract(_add(_multiply(cosHalfAngle, vector[2]), _multiply(x, vector[1])), _multiply(y, vector[0]));
				T w2 = _add(_add(_multiply(_negate(x), vector[0]), _multiply(_negate(y), vector[1])), _multiply(_negate(z), vector[2]));
				return new T[] {
					_subtract(_add(_add(_multiply(x, w2), _multiply(cosHalfAngle, x2)), _multiply(y, z2)), _multiply(z, y2)),
					_subtract(_add(_add(_multiply(y, w2), _multiply(cosHalfAngle, y2)), _multiply(z, x2)), _multiply(x, z2)),
					_subtract(_add(_add(_multiply(z, w2), _multiply(cosHalfAngle, z2)), _multiply(x, y2)), _multiply(y, x2)) };
			}
			throw new Error("my RotateBy() function is only defined for 3-component vectors.");
		}

		/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="angle">The angle of the rotation.</param>
		/// <param name="x">The x component of the axis vector to rotate about.</param>
		/// <param name="y">The y component of the axis vector to rotate about.</param>
		/// <param name="z">The z component of the axis vector to rotate about.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> RotateBy(Vector<T> vector, T angle, T x, T y, T z)
		{
			return Vector<T>.RotateBy(vector._vector, angle, x, y, z);
		}

		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="vector">The vector to rotate.</param>
		/// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
		/// <returns>The result of the rotation.</returns>
		public static Vector<T> RotateBy(Vector<T> vector, Quaternion<T> rotation)
		{
			throw new NotImplementedException();
			//if (vector.Dimensions == 3)
			//	return Quaternion<T>.Rotate(rotation, vector);
			//else
			//	throw new Exception("my quaternion rotations are only defined for 3-component vectors.");
		}

		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="left">The starting vector of the interpolation.</param>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public static T[] Lerp(T[] left, T[] right, T blend)
		{
			throw new NotImplementedException();
			//float[] leftFloats = left.Floats;
			//float[] rightFloats = right.Floats;
			//if (blend < 0 || blend > 1.0f)
			//	throw new Error("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
			//if (leftFloats.Length != rightFloats.Length)
			//	throw new Error("invalid lerp matrix length: (left.Dimensions != right.Dimensions)");
			//Vector<T> result = new Vector<T>(leftFloats.Length);
			//float[] resultFloats = result.Floats;
			//for (int i = 0; i < leftFloats.Length; i++)
			//	resultFloats[i] = leftFloats[i] + blend * (rightFloats[i] - leftFloats[i]);
			//return result;
		}

		/// <summary>Computes the linear interpolation between two vectors.</summary>
		/// <param name="left">The starting vector of the interpolation.</param>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
		/// <returns>The result of the interpolation.</returns>
		public static Vector<T> Lerp(Vector<T> left, Vector<T> right, T blend)
		{
			return Vector<T>.Lerp(left._vector, right._vector, blend);
		}

		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="left">The starting vector of the interpolation.</param>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public static T[] Slerp(T[] left, T[] right, T blend)
		{
			throw new NotImplementedException();
			//if (blend < 0 || blend > 1.0f)
			//	throw new Error("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
			//float dot = Calc.Clamp(Vector<T>.DotProduct(left, right), -1.0f, 1.0f);
			//float theta = Calc.ArcCos(dot) * blend;
			//return left * Calc.Cos(theta) + (right - left * dot).Normalize() * Calc.Sin(theta);
		}

		/// <summary>Sphereically interpolates between two vectors.</summary>
		/// <param name="left">The starting vector of the interpolation.</param>
		/// <param name="right">The ending vector of the interpolation.</param>
		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
		/// <returns>The result of the slerp operation.</returns>
		public static Vector<T> Slerp(Vector<T> left, Vector<T> right, T blend)
		{
			return Vector<T>.Slerp(left._vector, right._vector, blend);
		}

		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		/// <param name="a">The first vector of the interpolation.</param>
		/// <param name="b">The second vector of the interpolation.</param>
		/// <param name="c">The thrid vector of the interpolation.</param>
		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
		/// <returns>The resulting vector of the barycentric interpolation.</returns>
		public static T[] Blerp(T[] a, T[] b, T[] c, T u, T v)
		{
			throw new NotImplementedException();
			//return Vector<T>.Add(Vector<T>.Add(Vector<T>.Multiply(Vector<T>.Subtract(b, a), u), Vector<T>.Multiply(Vector<T>.Subtract(c, a), v)), a);
		}

		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
		/// <param name="a">The first vector of the interpolation.</param>
		/// <param name="b">The second vector of the interpolation.</param>
		/// <param name="c">The thrid vector of the interpolation.</param>
		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
		/// <returns>The resulting vector of the barycentric interpolation.</returns>
		public static Vector<T> Blerp(Vector<T> a, Vector<T> b, Vector<T> c, T u, T v)
		{
			return Vector<T>.Blerp(a._vector, b._vector, c._vector, u, v);
		}

		/// <summary>Does a value equality check.</summary>
		/// <param name="left">The first vector to check for equality.</param>
		/// <param name="right">The second vector  to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(T[] left, T[] right)
		{
			if (left.Length != right.Length)
				return false;
			for (int i = 0; i < left.Length; i++)
				if (!_equate(left[i], right[i]))
					return false;
			return true;
		}

		/// <summary>Does a value equality check.</summary>
		/// <param name="left">The first vector to check for equality.</param>
		/// <param name="right">The second vector  to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Vector<T> left, Vector<T> right)
		{
			return Vector<T>.EqualsValue(left._vector, right._vector);
		}

		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="left">The first vector to check for equality.</param>
		/// <param name="right">The second vector to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(T[] left, T[] right, T leniency)
		{
			if (left.Length != right.Length)
				return false;
			for (int i = 0; i < left.Length; i++)
				if (_compare(_abs(_subtract(left[i], right[i])), leniency) == Comparison.Greater)
					return false;
			return true;
		}

		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="left">The first vector to check for equality.</param>
		/// <param name="right">The second vector to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Vector<T> left, Vector<T> right, T leniency)
		{
			return Vector<T>.EqualsValue(left._vector, right._vector, leniency);
		}

		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="left">The left vector of the equality check.</param>
		/// <param name="right">The right vector of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public static bool EqualsReference(T[] left, T[] right)
		{
			return object.ReferenceEquals(left, right);
		}

		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="left">The left vector of the equality check.</param>
		/// <param name="right">The right vector of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public static bool EqualsReference(Vector<T> left, Vector<T> right)
		{
			return EqualsReference(left._vector, right._vector);
		}

		///// <summary>Converts the vector into a matrix.</summary>
		///// <param name="vector">The vecotr to convert.</param>
		///// <returns>The matrix of the conversion.</returns>
		//public static Matrix_Flattened ToMatrix(Vector<T> vector)
		//{
		//	return new Matrix_Flattened(vector.Dimensions, 1, vector.Floats);
		//}

		/// <summary>Prints out a string representation of this matrix.</summary>
		/// <returns>A string representing this matrix.</returns>
		public override string ToString()
		{
			// Change this method to what ever you want.
			return base.ToString();
		}

		/// <summary>Computes a hash code from the values of this matrix.</summary>
		/// <returns>A hash code for the matrix.</returns>
		public override int GetHashCode()
		{
			// return base.GetHashCode();
			int hash = _vector[0].GetHashCode();
			for (int i = 1; i < _vector.Length; i++)
				hash ^= _vector[i].GetHashCode();
			return hash;
		}

		/// <summary>Does an equality check by reference.</summary>
		/// <param name="right">The object to compare to.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public override bool Equals(object right)
		{
			if (!(right is Vector<T>)) return false;
			return Vector<T>.EqualsReference(this, (Vector<T>)right);
		}

		private class Error : LinearAlgebra.Error
		{
			public Error(string message) : base(message) { }
		}
	}

	/// <summary>Implements a 4-component [x, y, z, w] quaternion. X, Y, and Z represent the axis of rotation, and W is the rotation ammount.</summary>
	/// <typeparam name="T">The numeric type of this Quaternion.</typeparam>
	public class Quaternion<T>
	{
		// Constants
    private static T _zero;
    private static T _one;
    private static T _two;

		// Logic
    private static Logic._Abs<T> _abs;
    private static Equate<T> _equate;
    private static Compare<T> _compare;

		// Arithmetic Operations
    private static Arithmetic._Negate<T> _negate;
    private static Arithmetic._Add<T> _add;
    private static Arithmetic._Subtract<T> _subtract;
    private static Arithmetic._Multiply<T> _multiply;
    private static Arithmetic._Divide<T> _divide;

		// Algebra Operations
    private static Algebra._ln<T> _ln;
    private static Algebra._log<T> _log;
    private static Algebra._sqrt<T> _sqrt;
    private static Algebra._root<T> _root;
    private static Algebra._exp<T> _exp;
    private static Algebra._PrimeFactors<T> _PrimeFactors;
    private static Algebra._Invert_Multiplicative<T> _Invert_Multiplicative;
    private static Algebra._Invert_Additive<T> _Invert_Additive;

		// Trigonometry
    private static Trigonometry._sin<T> _sin;
    private static Trigonometry._cos<T> _cos;

    static Quaternion()
    {
      // Constants
		  _zero = Constants.Get<T>().factory(0);
		  _one = Constants.Get<T>().factory(1);
		  _two = Constants.Get<T>().factory(2);

		  // Logic
		  _abs = Logic.Get<T>().Abs;
		  _equate = Logic.Get<T>().Equate;
		  _compare = Logic.Get<T>().Compare;

		  // Arithmetic Operations
		  _negate = Arithmetic.Get<T>().Negate;
		  _add = Arithmetic.Get<T>().Add;
		  _subtract = Arithmetic.Get<T>().Subtract;
		  _multiply = Arithmetic.Get<T>().Multiply;
		  _divide = Arithmetic.Get<T>().Divide;

		  // Algebra Operations
		  _ln = Algebra.Get<T>().ln;
		  _log = Algebra.Get<T>().log;
		  _sqrt = Algebra.Get<T>().sqrt;
		  _root = Algebra.Get<T>().root;
		  _exp = Algebra.Get<T>().exp;
		  _PrimeFactors = Algebra.Get<T>().factorPrimes;
		  _Invert_Multiplicative = Algebra.Get<T>().Invert_Multiplicative;
		  _Invert_Additive = Algebra.Get<T>().Invert_Additive;

		  // Trigonometry
		  _sin = Trigonometry.Get<T>().sin;
		  _cos = Trigonometry.Get<T>().cos;
    }

		protected T _x, _y, _z, _w;

		/// <summary>The X component of the quaternion. (axis, NOT rotation ammount)</summary>
		public T X { get { return _x; } set { _x = value; } }
		/// <summary>The Y component of the quaternion. (axis, NOT rotation ammount)</summary>
		public T Y { get { return _y; } set { _y = value; } }
		/// <summary>The Z component of the quaternion. (axis, NOT rotation ammount)</summary>
		public T Z { get { return _z; } set { _z = value; } }
		/// <summary>The W component of the quaternion. (rotation ammount, NOT axis)</summary>
		public T W { get { return _w; } set { _w = value; } }

		/// <summary>Constructs a quaternion with the desired values.</summary>
		/// <param name="x">The x component of the quaternion.</param>
		/// <param name="y">The y component of the quaternion.</param>
		/// <param name="z">The z component of the quaternion.</param>
		/// <param name="w">The w component of the quaternion.</param>
		public Quaternion(T x, T y, T z, T w) { _x = x; _y = y; _z = z; _w = w; }

		/// <summary>Returns new Quaternion(0, 0, 0, 1).</summary>
		public static readonly Quaternion<T> FactoryIdentity = new Quaternion<T>(_zero, _zero, _zero, _one);

		///// <summary>Creates a quaternion from an axis and rotation.</summary>
		///// <param name="axis">The to create the quaternion from.</param>
		///// <param name="angle">The angle to create teh quaternion from.</param>
		///// <returns>The newly created quaternion.</returns>
		//public static Quaternion<T> FactoryFromAxisAngle(Vector axis, T angle)
		//{
		//	throw new System.NotImplementedException();
		//	//if (axis.LengthSquared() == 0.0f)
		//	//	return FactoryIdentity;
		//	//T sinAngleOverAxisLength = Calc.Sin(angle / 2) / axis.Length();
		//	//return Quaternion<T>.Normalize(new Quaternion<T>(
		//	//	_multiply(axis.X, sinAngleOverAxisLength),
		//	//	axis.Y * sinAngleOverAxisLength,
		//	//	axis.Z * sinAngleOverAxisLength,
		//	//	Calc.Cos(angle / 2)));
		//}

		/// <summary>Adds two quaternions together.</summary>
		/// <param name="left">The first quaternion of the addition.</param>
		/// <param name="right">The second quaternion of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Quaternion<T> operator +(Quaternion<T> left, Quaternion<T> right) { return Quaternion<T>.Add(left, right); }
		/// <summary>Subtracts two quaternions.</summary>
		/// <param name="left">The left quaternion of the subtraction.</param>
		/// <param name="right">The right quaternion of the subtraction.</param>
		/// <returns>The resulting quaternion after the subtraction.</returns>
		public static Quaternion<T> operator -(Quaternion<T> left, Quaternion<T> right) { return Quaternion<T>.Subtract(left, right); }
		/// <summary>Multiplies two quaternions together.</summary>
		/// <param name="left">The first quaternion of the multiplication.</param>
		/// <param name="right">The second quaternion of the multiplication.</param>
		/// <returns>The resulting quaternion after the multiplication.</returns>
		public static Quaternion<T> operator *(Quaternion<T> left, Quaternion<T> right) { return Quaternion<T>.Multiply(left, right); }
		/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
		/// <param name="left">The quaternion to pre-multiply the vector by.</param>
		/// <param name="vector">The vector to be multiplied.</param>
		/// <returns>The resulting quaternion of the multiplication.</returns>
		//public static Quaternion<T> operator *(Quaternion<T> left, Vector right) { return Quaternion<T>.Multiply(left, right); }
		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
		/// <param name="left">The quaternion of the multiplication.</param>
		/// <param name="right">The scalar of the multiplication.</param>
		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
		public static Quaternion<T> operator *(Quaternion<T> left, T right) { return Quaternion<T>.Multiply(left, right); }
		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
		/// <param name="left">The scalar of the multiplication.</param>
		/// <param name="right">The quaternion of the multiplication.</param>
		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
		public static Quaternion<T> operator *(T left, Quaternion<T> right) { return Quaternion<T>.Multiply(right, left); }
		/// <summary>Checks for equality by value. (beware float errors)</summary>
		/// <param name="left">The first quaternion of the equality check.</param>
		/// <param name="right">The second quaternion of the equality check.</param>
		/// <returns>true if the values were deemed equal, false if not.</returns>
		public static bool operator ==(Quaternion<T> left, Quaternion<T> right) { return Quaternion<T>.Equals(left, right); }
		/// <summary>Checks for anti-equality by value. (beware float errors)</summary>
		/// <param name="left">The first quaternion of the anti-equality check.</param>
		/// <param name="right">The second quaternion of the anti-equality check.</param>
		/// <returns>false if the values were deemed equal, true if not.</returns>
		public static bool operator !=(Quaternion<T> left, Quaternion<T> right) { return !Quaternion<T>.Equals(left, right); }

		/// <summary>Computes the length of quaternion.</summary>
		/// <returns>The length of the given quaternion.</returns>
		public T Magnitude() { return Quaternion<T>.Magnitude(this); }
		/// <summary>Computes the length of a quaternion, but doesn't square root it
		/// for optimization possibilities.</summary>
		/// <returns>The squared length of the given quaternion.</returns>
		public T MagnitudeSquared() { return Quaternion<T>.MagnitudeSquared(this); }
		/// <summary>Gets the conjugate of the quaternion.</summary>
		/// <returns>The conjugate of teh given quaternion.</returns>
		public Quaternion<T> Conjugate() { return Quaternion<T>.Conjugate(this); }
		/// <summary>Adds two quaternions together.</summary>
		/// <param name="right">The second quaternion of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public Quaternion<T> Add(Quaternion<T> right) { return Quaternion<T>.Add(this, right); }
		/// <summary>Subtracts two quaternions.</summary>
		/// <param name="right">The right quaternion of the subtraction.</param>
		/// <returns>The resulting quaternion after the subtraction.</returns>
		public Quaternion<T> Subtract(Quaternion<T> right) { return Quaternion<T>.Subtract(this, right); }
		/// <summary>Multiplies two quaternions together.</summary>
		/// <param name="right">The second quaternion of the multiplication.</param>
		/// <returns>The resulting quaternion after the multiplication.</returns>
		public Quaternion<T> Multiply(Quaternion<T> right) { return Quaternion<T>.Multiply(this, right); }
		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
		/// <param name="right">The scalar of the multiplication.</param>
		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
		public Quaternion<T> Multiply(T right) { return Quaternion<T>.Multiply(this, right); }
		/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
		/// <param name="right">The vector to be multiplied.</param>
		/// <returns>The resulting quaternion of the multiplication.</returns>
		//public Quaternion<T> Multiply(Vector vector) { return Quaternion<T>.Multiply(this, vector); }
		/// <summary>Normalizes the quaternion.</summary>
		/// <returns>The normalization of the given quaternion.</returns>
		public Quaternion<T> Normalize() { return Quaternion<T>.Normalize(this); }
		/// <summary>Inverts a quaternion.</summary>
		/// <returns>The inverse of the given quaternion.</returns>
		public Quaternion<T> Invert() { return Quaternion<T>.Invert(this); }
		/// <summary>Lenearly interpolates between two quaternions.</summary>
		/// <param name="right">The ending point of the interpolation.</param>
		/// <param name="blend">The ratio 0.0-1.0 of how far to interpolate between the left and right quaternions.</param>
		/// <returns>The result of the interpolation.</returns>
		public Quaternion<T> Lerp(Quaternion<T> right, T blend) { return Quaternion<T>.Lerp(this, right, blend); }
		/// <summary>Sphereically interpolates between two quaternions.</summary>
		/// <param name="right">The ending point of the interpolation.</param>
		/// <param name="blend">The ratio of how far to interpolate between the left and right quaternions.</param>
		/// <returns>The result of the interpolation.</returns>
		public Quaternion<T> Slerp(Quaternion<T> right, T blend) { return Quaternion<T>.Slerp(this, right, blend); }
		/// <summary>Rotates a vector by a quaternion.</summary>
		/// <param name="vector">The vector to be rotated by.</param>
		/// <returns>The result of the rotation.</returns>
		//public Vector Rotate(Vector vector) { return Quaternion<T>.Rotate(this, vector); }
		/// <summary>Does a value equality check.</summary>
		/// <param name="right">The second quaternion  to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public bool EqualsValue(Quaternion<T> right) { return Quaternion<T>.EqualsValue(this, right); }
		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="right">The second quaternion to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public bool EqualsValue(Quaternion<T> right, T leniency) { return Quaternion<T>.EqualsValue(this, right, leniency); }
		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="right">The right quaternion of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public bool EqualsReference(Quaternion<T> right) { return Quaternion<T>.EqualsReference(this, right); }
		/// <summary>Converts a quaternion into a matrix.</summary>
		/// <returns>The resulting matrix.</returns>
		//public Matrix_Flattened ToMatrix() { return Quaternion<T>.ToMatrix(this); }

		/// <summary>Computes the length of quaternion.</summary>
		/// <param name="quaternion">The quaternion to compute the length of.</param>
		/// <returns>The length of the given quaternion.</returns>
		public static T Magnitude(Quaternion<T> quaternion)
		{
			return
				_sqrt(_add(_add(_add(
					_multiply(quaternion.X, quaternion.X),
					_multiply(quaternion.Y, quaternion.Y)),
					_multiply(quaternion.Z, quaternion.Z)),
					_multiply(quaternion.W, quaternion.W)));
		}

		/// <summary>Computes the length of a quaternion, but doesn't square root it
		/// for optimization possibilities.</summary>
		/// <param name="quaternion">The quaternion to compute the length squared of.</param>
		/// <returns>The squared length of the given quaternion.</returns>
		public static T MagnitudeSquared(Quaternion<T> quaternion)
		{
			return
				_add(_add(_add(
					_multiply(quaternion.X, quaternion.X),
					_multiply(quaternion.Y, quaternion.Y)),
					_multiply(quaternion.Z, quaternion.Z)),
					_multiply(quaternion.W, quaternion.W));
		}

		/// <summary>Gets the conjugate of the quaternion.</summary>
		/// <param name="quaternion">The quaternion to conjugate.</param>
		/// <returns>The conjugate of teh given quaternion.</returns>
		public static Quaternion<T> Conjugate(Quaternion<T> quaternion)
		{
			return new Quaternion<T>(
				_negate(quaternion.X),
				_negate(quaternion.Y),
				_negate(quaternion.Z),
				quaternion.W);
		}

		/// <summary>Adds two quaternions together.</summary>
		/// <param name="left">The first quaternion of the addition.</param>
		/// <param name="right">The second quaternion of the addition.</param>
		/// <returns>The result of the addition.</returns>
		public static Quaternion<T> Add(Quaternion<T> left, Quaternion<T> right)
		{
			return new Quaternion<T>(
				_add(left.X, right.X),
				_add(left.Y, right.Y),
				_add(left.Z, right.Z),
				_add(left.W, right.W));
		}

		/// <summary>Subtracts two quaternions.</summary>
		/// <param name="left">The left quaternion of the subtraction.</param>
		/// <param name="right">The right quaternion of the subtraction.</param>
		/// <returns>The resulting quaternion after the subtraction.</returns>
		public static Quaternion<T> Subtract(Quaternion<T> left, Quaternion<T> right)
		{
			return new Quaternion<T>(
				_subtract(left.X, right.X),
				_subtract(left.Y, right.Y),
				_subtract(left.Z, right.Z),
				_subtract(left.W, right.W));
		}

		/// <summary>Multiplies two quaternions together.</summary>
		/// <param name="left">The first quaternion of the multiplication.</param>
		/// <param name="right">The second quaternion of the multiplication.</param>
		/// <returns>The resulting quaternion after the multiplication.</returns>
		public static Quaternion<T> Multiply(Quaternion<T> left, Quaternion<T> right)
		{
			return new Quaternion<T>(
				_subtract(_add(_add(_multiply(left.X, right.W), _multiply(left.W, right.X)), _multiply(left.Y, right.Z)), _multiply(left.Z, right.Y)),
				_subtract(_add(_add(_multiply(left.Y, right.W), _multiply(left.W, right.Y)), _multiply(left.Z, right.X)), _multiply(left.X, right.Z)),
				_subtract(_add(_add(_multiply(left.Z, right.W), _multiply(left.W, right.Z)), _multiply(left.X, right.Y)), _multiply(left.Y, right.X)),
				_subtract(_subtract(_subtract(_multiply(left.W, right.W), _multiply(left.X, right.X)), _multiply(left.Y, right.Y)), _multiply(left.Z, right.Z)));
		}

		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
		/// <param name="left">The quaternion of the multiplication.</param>
		/// <param name="right">The scalar of the multiplication.</param>
		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
		public static Quaternion<T> Multiply(Quaternion<T> left, T right)
		{
			return new Quaternion<T>(
				_multiply(left.X, right),
				_multiply(left.Y, right),
				_multiply(left.Z, right),
				_multiply(left.W, right));
		}

		///// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
		///// <param name="left">The quaternion to pre-multiply the vector by.</param>
		///// <param name="right">The vector to be multiplied.</param>
		///// <returns>The resulting quaternion of the multiplication.</returns>
		//public static Quaternion<T> Multiply(Quaternion<T> left, Vector right)
		//{
		//	throw new NotImplementedException();
		//	//if (right.Dimensions == 3)
		//	//{
		//	//	return new Quaternion<T>(
		//	//		left.W * right.X + left.Y * right.Z - left.Z * right.Y,
		//	//		left.W * right.Y + left.Z * right.X - left.X * right.Z,
		//	//		left.W * right.Z + left.X * right.Y - left.Y * right.X,
		//	//		-left.X * right.X - left.Y * right.Y - left.Z * right.Z);
		//	//}
		//	//else
		//	//	throw new Error("my quaternion rotations are only defined for 3-component vectors.");
		//}

		/// <summary>Normalizes the quaternion.</summary>
		/// <param name="quaternion">The quaternion to normalize.</param>
		/// <returns>The normalization of the given quaternion.</returns>
		public static Quaternion<T> Normalize(Quaternion<T> quaternion)
		{
			T normalizer = Quaternion<T>.Magnitude(quaternion);
			if (!_equate(normalizer, _zero))
				return quaternion * _Invert_Multiplicative(normalizer);
			else
				return Quaternion<T>.FactoryIdentity;
		}

		/// <summary>Inverts a quaternion.</summary>
		/// <param name="quaternion">The quaternion to find the inverse of.</param>
		/// <returns>The inverse of the given quaternion.</returns>
		public static Quaternion<T> Invert(Quaternion<T> quaternion)
		{
			// EQUATION: invert = quaternion.Conjugate()).Normalized()
			T normalizer = Quaternion<T>.MagnitudeSquared(quaternion);
			if (_equate(normalizer, _zero))
				return new Quaternion<T>(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
			normalizer = _Invert_Multiplicative(normalizer);
			return new Quaternion<T>(
				_multiply(_negate(quaternion.X), normalizer),
				_multiply(_negate(quaternion.Y), normalizer),
				_multiply(_negate(quaternion.Z), normalizer),
				_multiply(quaternion.W, normalizer));
		}

		/// <summary>Lenearly interpolates between two quaternions.</summary>
		/// <param name="left">The starting point of the interpolation.</param>
		/// <param name="right">The ending point of the interpolation.</param>
		/// <param name="blend">The ratio 0.0-1.0 of how far to interpolate between the left and right quaternions.</param>
		/// <returns>The result of the interpolation.</returns>
		public static Quaternion<T> Lerp(Quaternion<T> left, Quaternion<T> right, T blend)
		{
			if (_compare(blend, _zero) == Comparison.Less || _compare(blend, _one) == Comparison.Greater)
				throw new Error("invalid blending value during lerp !(blend < 0.0f || blend > 1.0f).");
			if (_equate(Quaternion<T>.MagnitudeSquared(left), _zero))
			{
				if (_equate(Quaternion<T>.MagnitudeSquared(right), _zero))
					return FactoryIdentity;
				else
					return new Quaternion<T>(right.X, right.Y, right.Z, right.W);
			}
			else if (_equate(Quaternion<T>.MagnitudeSquared(right), _zero))
				return new Quaternion<T>(left.X, left.Y, left.Z, left.W);
			Quaternion<T> result = new Quaternion<T>(
				_add(_multiply(_subtract(_one, blend), left.X), _multiply(blend, right.X)),
				_add(_multiply(_subtract(_one, blend), left.Y), _multiply(blend, right.Y)),
				_add(_multiply(_subtract(_one, blend), left.Z), _multiply(blend, right.Z)),
				_add(_multiply(_subtract(_one, blend), left.W), _multiply(blend, right.W)));
			if (_compare(Quaternion<T>.MagnitudeSquared(result), _zero) == Comparison.Greater)
				return Quaternion<T>.Normalize(result);
			else
				return Quaternion<T>.FactoryIdentity;
		}

		/// <summary>Sphereically interpolates between two quaternions.</summary>
		/// <param name="left">The starting point of the interpolation.</param>
		/// <param name="right">The ending point of the interpolation.</param>
		/// <param name="blend">The ratio of how far to interpolate between the left and right quaternions.</param>
		/// <returns>The result of the interpolation.</returns>
		public static Quaternion<T> Slerp(Quaternion<T> left, Quaternion<T> right, T blend)
		{
			throw new NotImplementedException();
			//if (_compare(blend, _zero) == Comparison.Less || _compare(blend, _one) == Comparison.Greater)
			//	throw new Error("invalid blending value during lerp !(blend < 0.0f || blend > 1.0f).");
			//if (_equate(Quaternion<T>.MagnitudeSquared(left), _zero))
			//{
			//	if (_equate(Quaternion<T>.MagnitudeSquared(right), _zero))
			//		return FactoryIdentity;
			//	else
			//		return new Quaternion<T>(right.X, right.Y, right.Z, right.W);
			//}
			//else if (_equate(Quaternion<T>.MagnitudeSquared(right), _zero))
			//	return new Quaternion<T>(left.X, left.Y, left.Z, left.W);
			//float cosHalfAngle = left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
			//if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
			//	return new Quaternion<T>(left.X, left.Y, left.Z, left.W);
			//else if (cosHalfAngle < 0.0f)
			//{
			//	right = new Quaternion<T>(-left.X, -left.Y, -left.Z, -left.W);
			//	cosHalfAngle = -cosHalfAngle;
			//}
			//float halfAngle = (float)Math.Acos(cosHalfAngle);
			//float sinHalfAngle = Calc.Sin(halfAngle);
			//float blendA = Calc.Sin(halfAngle * (_subtract(_one, blend)) / sinHalfAngle;
			//float blendB = Calc.Sin(halfAngle * blend) / sinHalfAngle;
			//Quaternion<T> result = new Quaternion<T>(
			//	blendA * left.X + blendB * right.X,
			//	blendA * left.Y + blendB * right.Y,
			//	blendA * left.Z + blendB * right.Z,
			//	blendA * left.W + blendB * right.W);
			//if (_compare(Quaternion<T>.MagnitudeSquared(result), _zero) == Comparison.Greater)
			//	return Quaternion<T>.Normalize(result);
			//else
			//	return Quaternion<T>.FactoryIdentity;
		}

		///// <summary>Rotates a vector by a quaternion [v' = qvq'].</summary>
		///// <param name="rotation">The quaternion to rotate the vector by.</param>
		///// <param name="vector">The vector to be rotated by.</param>
		///// <returns>The result of the rotation.</returns>
		//public static Vector Rotate(Quaternion<T> rotation, Vector vector)
		//{
		//	throw new NotImplementedException();
		//	//if (vector.Dimensions != 3)
		//	//	throw new Error("my quaternion rotations are only defined for 3-component vectors.");
		//	//Quaternion<T> answer = Quaternion<T>.Multiply(Quaternion<T>.Multiply(rotation, vector), Quaternion<T>.Conjugate(rotation));
		//	//return new Vector(answer.X, answer.Y, answer.Z);
		//}

		/// <summary>Does a value equality check.</summary>
		/// <param name="left">The first quaternion to check for equality.</param>
		/// <param name="right">The second quaternion  to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Quaternion<T> left, Quaternion<T> right)
		{
			return
				_equate(left.X, right.X) &&
				_equate(left.Y, right.Y) &&
				_equate(left.Z, right.Z) &&
				_equate(left.W, right.W);
		}

		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="left">The first quaternion to check for equality.</param>
		/// <param name="right">The second quaternion to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Quaternion<T> left, Quaternion<T> right, T leniency)
		{
			return
				_compare(_abs(_subtract(left.X, right.X)), leniency) == Comparison.Less &&
				_compare(_abs(_subtract(left.Y, right.Y)), leniency) == Comparison.Less &&
				_compare(_abs(_subtract(left.Z, right.Z)), leniency) == Comparison.Less &&
				_compare(_abs(_subtract(left.W, right.W)), leniency) == Comparison.Less;
		}

		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="left">The left quaternion of the equality check.</param>
		/// <param name="right">The right quaternion of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public static bool EqualsReference(Quaternion<T> left, Quaternion<T> right)
		{
			return object.ReferenceEquals(left, right);
		}

		///// <summary>Converts a quaternion into a matrix.</summary>
		///// <param name="quaternion">The quaternion of the conversion.</param>
		///// <returns>The resulting matrix.</returns>
		//public static Matrix_Flattened ToMatrix(Quaternion<T> quaternion)
		//{
		//	throw new NotImplementedException();
		//	//return new Matrix_Flattened(3, 3,
		//	//	quaternion.W * quaternion.W + quaternion.X * quaternion.X - quaternion.Y * quaternion.Y - quaternion.Z * quaternion.Z,
		//	//	2 * quaternion.X * quaternion.Y - 2 * quaternion.W * quaternion.Z,
		//	//	2 * quaternion.X * quaternion.Z + 2 * quaternion.W * quaternion.Y,
		//	//	2 * quaternion.X * quaternion.Y + 2 * quaternion.W * quaternion.Z,
		//	//	quaternion.W * quaternion.W - quaternion.X * quaternion.X + quaternion.Y * quaternion.Y - quaternion.Z * quaternion.Z,
		//	//	2 * quaternion.Y * quaternion.Z + 2 * quaternion.W * quaternion.X,
		//	//	2 * quaternion.X * quaternion.Z - 2 * quaternion.W * quaternion.Y,
		//	//	2 * quaternion.Y * quaternion.Z - 2 * quaternion.W * quaternion.X,
		//	//	quaternion.W * quaternion.W - quaternion.X * quaternion.X - quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
		//}

		/// <summary>Converts the quaternion into a string.</summary>
		/// <returns>The resulting string after the conversion.</returns>
		public override string ToString()
		{
			// Chane this method to format it how you want...
			return base.ToString();
			//return "{ " + _x + ", " + _y + ", " + _z + ", " + _w + " }";
		}

		/// <summary>Computes a hash code from the values in this quaternion.</summary>
		/// <returns>The computed hash code.</returns>
		public override int GetHashCode()
		{
			return
				_x.GetHashCode() ^
				_y.GetHashCode() ^
				_z.GetHashCode() ^
				_w.GetHashCode();
		}

		/// <summary>Does a reference equality check.</summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			if (other is Quaternion<T>)
				return Quaternion<T>.EqualsReference(this, (Quaternion<T>)other);
			return false;
		}

		public class Error : LinearAlgebra.Error
		{
			public Error(string message) : base(message) { }
		}
	}

  /// <summary>A matrix wrapper for float[,] to perform matrix theory in row major order. Enjoy :)</summary>
  public class Matrix<T>
  {
    // Constants
    private static T _zero = Constants.Get<T>().factory(0);
    private static T _one = Constants.Get<T>().factory(1);
    private static T _two = Constants.Get<T>().factory(2);

    // Logic
    private static Logic._Abs<T> _abs = Logic.Get<T>().Abs;
    private static Equate<T> _equate = Logic.Get<T>().Equate;
    private static Compare<T> _compare = Logic.Get<T>().Compare;

    // Arithmetic Operations
    private static Arithmetic._Negate<T> _negate = Arithmetic.Get<T>().Negate;
    private static Arithmetic._Add<T> _add = Arithmetic.Get<T>().Add;
    private static Arithmetic._Subtract<T> _subtract = Arithmetic.Get<T>().Subtract;
    private static Arithmetic._Multiply<T> _multiply = Arithmetic.Get<T>().Multiply;
    private static Arithmetic._Divide<T> _divide = Arithmetic.Get<T>().Divide;

    // Algebra Operations
    private static Algebra._ln<T> _ln = Algebra.Get<T>().ln;
    private static Algebra._log<T> _log = Algebra.Get<T>().log;
    private static Algebra._sqrt<T> _sqrt = Algebra.Get<T>().sqrt;
    private static Algebra._root<T> _root = Algebra.Get<T>().root;
    private static Algebra._exp<T> _exp = Algebra.Get<T>().exp;
    private static Algebra._PrimeFactors<T> _PrimeFactors = Algebra.Get<T>().factorPrimes;
    private static Algebra._Invert_Multiplicative<T> _Invert_Multiplicative = Algebra.Get<T>().Invert_Multiplicative;
    private static Algebra._Invert_Additive<T> _Invert_Additive = Algebra.Get<T>().Invert_Additive;

    // Trigonometry
    private static Trigonometry._sin<T> _sin = Trigonometry.Get<T>().sin;
    private static Trigonometry._cos<T> _cos = Trigonometry.Get<T>().cos;
    private static Trigonometry._arccos<T> _acos = Trigonometry.Get<T>().arccos;

    static Matrix()
    {
      // Constants
      _zero = Constants.Get<T>().factory(0);
      _one = Constants.Get<T>().factory(1);
      _two = Constants.Get<T>().factory(2);

      // Logic
      _abs = Logic.Get<T>().Abs;
      _equate = Logic.Get<T>().Equate;
      _compare = Logic.Get<T>().Compare;

      // Arithmetic Operations
      _negate = Arithmetic.Get<T>().Negate;
      _add = Arithmetic.Get<T>().Add;
      _subtract = Arithmetic.Get<T>().Subtract;
      _multiply = Arithmetic.Get<T>().Multiply;
      _divide = Arithmetic.Get<T>().Divide;

      // Algebra Operations
      _ln = Algebra.Get<T>().ln;
      _log = Algebra.Get<T>().log;
      _sqrt = Algebra.Get<T>().sqrt;
      _root = Algebra.Get<T>().root;
      _exp = Algebra.Get<T>().exp;
      _PrimeFactors = Algebra.Get<T>().factorPrimes;
      _Invert_Multiplicative = Algebra.Get<T>().Invert_Multiplicative;
      _Invert_Additive = Algebra.Get<T>().Invert_Additive;

      // Trigonometry
      _sin = Trigonometry.Get<T>().sin;
      _cos = Trigonometry.Get<T>().cos;
      _acos = Trigonometry.Get<T>().arccos;
    }

    public readonly T[,] _matrix;

    /// <summary>The number of rows in the matrix.</summary>
    public int Rows { get { return _matrix.GetLength(0); } }
    /// <summary>The number of columns in the matrix.</summary>
    public int Columns { get { return _matrix.GetLength(1); } }
    /// <summary>Determines if the matrix is square.</summary>
    public bool IsSquare { get { return Rows == Columns; } }
    /// <summary>Determines if the matrix is a vector.</summary>
    public bool IsVector { get { return Columns == 1; } }
    /// <summary>Determines if the matrix is a 2 component vector.</summary>
    public bool Is2x1 { get { return Rows == 2 && Columns == 1; } }
    /// <summary>Determines if the matrix is a 3 component vector.</summary>
    public bool Is3x1 { get { return Rows == 3 && Columns == 1; } }
    /// <summary>Determines if the matrix is a 4 component vector.</summary>
    public bool Is4x1 { get { return Rows == 4 && Columns == 1; } }
    /// <summary>Determines if the matrix is a 2 square matrix.</summary>
    public bool Is2x2 { get { return Rows == 2 && Columns == 2; } }
    /// <summary>Determines if the matrix is a 3 square matrix.</summary>
    public bool Is3x3 { get { return Rows == 3 && Columns == 3; } }
    /// <summary>Determines if the matrix is a 4 square matrix.</summary>
    public bool Is4x4 { get { return Rows == 4 && Columns == 4; } }

    /// <summary>Standard row-major matrix indexing.</summary>
    /// <param name="row">The row index.</param>
    /// <param name="column">The column index.</param>
    /// <returns>The value at the given indeces.</returns>
    public T this[int row, int column]
    {
      get
      {
        try { return _matrix[row, column]; }
        catch { throw new Error("index out of bounds."); }
      }
      set
      {
        try { _matrix[row, column] = value; }
        catch { throw new Error("index out of bounds."); }
      }
    }

    /// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of row dimensions.</param>
    /// <param name="columns">The number of column dimensions.</param>
    public Matrix(int rows, int columns)
    {
      try { _matrix = new T[rows, columns]; }
      catch { throw new Error("invalid dimensions."); }
    }

    /// <summary>Wraps a float[,] inside of a matrix class. WARNING: still references that float[,].</summary>
    /// <param name="matrix">The float[,] to wrap in a matrix class.</param>
    public Matrix(T[,] matrix)
    {
      _matrix = matrix;
    }

    /// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed zero-matrix.</returns>
    public static Matrix<T> FactoryZero(int rows, int columns)
    {
      try { return new Matrix<T>(rows, columns); }
      catch { throw new Error("invalid dimensions."); }
    }

    /// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed identity-matrix.</returns>
    public static Matrix<T> FactoryIdentity(int rows, int columns)
    {
      Matrix<T> matrix;
      try { matrix = new Matrix<T>(rows, columns); }
      catch { throw new Error("invalid dimensions."); }
      if (rows <= columns)
        for (int i = 0; i < rows; i++)
          matrix[i, i] = _one;
      else
        for (int i = 0; i < columns; i++)
          matrix[i, i] = _one;
      return matrix;
    }

    /// <summary>Constructs a new matrix where every entry is 1.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed matrix filled with 1's.</returns>
    public static Matrix<T> FactoryOne(int rows, int columns)
    {
      Matrix<T> matrix;
      try { matrix = new Matrix<T>(rows, columns); }
      catch { throw new Error("invalid dimensions."); }
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          matrix[i, j] = _one;
      return matrix;
    }

    /// <summary>Constructs a new matrix where every entry is the same uniform value.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <param name="uniform">The value to assign every spot in the matrix.</param>
    /// <returns>The newly constructed matrix filled with the uniform value.</returns>
    public static Matrix<T> FactoryUniform(int rows, int columns, T uniform)
    {
      Matrix<T> matrix;
      try { matrix = new Matrix<T>(rows, columns); }
      catch { throw new Error("invalid dimensions."); }
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          matrix[i, j] = uniform;
      return matrix;
    }

    /// <summary>Constructs a 2-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 2-component vector matrix.</returns>
    public static Matrix<T> Factory2x1() { return new Matrix<T>(2, 1); }
    /// <summary>Constructs a 3-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 3-component vector matrix.</returns>
    public static Matrix<T> Factory3x1() { return new Matrix<T>(3, 1); }
    /// <summary>Constructs a 4-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 4-component vector matrix.</returns>
    public static Matrix<T> Factory4x1() { return new Matrix<T>(4, 1); }

    /// <summary>Constructs a 2x2 matrix with all values being 0.</summary>
    /// <returns>The constructed 2x2 matrix.</returns>
    public static Matrix<T> Factory2x2() { return new Matrix<T>(2, 2); }
    /// <summary>Constructs a 3x3 matrix with all values being 0.</summary>
    /// <returns>The constructed 3x3 matrix.</returns>
    public static Matrix<T> Factory3x3() { return new Matrix<T>(3, 3); }
    /// <summary>Constructs a 4x4 matrix with all values being 0.</summary>
    /// <returns>The constructed 4x4 matrix.</returns>
    public static Matrix<T> Factory4x4() { return new Matrix<T>(4, 4); }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix<T> Factory3x3RotationX(T angle)
    {
      T cos = _cos(angle);
      T sin = _sin(angle);
      return new Matrix<T>(new T[,] {
        { _one, _zero, _zero },
        { _zero, cos, sin },
        { _zero, _negate(sin), cos }});
    }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix<T> Factory3x3RotationY(T angle)
    {
      T cos = _cos(angle);
      T sin = _sin(angle);
      return new Matrix<T>(new T[,] {
        { cos, _zero, _negate(sin) },
        { _zero, _one, _zero },
        { sin, _zero, cos }});
    }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix<T> Factory3x3RotationZ(T angle)
    {
      T cos = _cos(angle);
      T sin = _sin(angle);
      return new Matrix<T>(new T[,] {
        { cos, _negate(sin), _zero },
        { sin, cos, _zero },
        { _zero, _zero, _zero }});
    }

    /// <param name="angleX">Angle about the X-axis in radians.</param>
    /// <param name="angleY">Angle about the Y-axis in radians.</param>
    /// <param name="angleZ">Angle about the Z-axis in radians.</param>
    public static Matrix<T> Factory3x3RotationXthenYthenZ(T angleX, T angleY, T angleZ)
    {
      T xCos = _cos(angleX), xSin = _sin(angleX),
        yCos = _cos(angleY), ySin = _sin(angleY),
        zCos = _cos(angleZ), zSin = _sin(angleZ);
      return new Matrix<T>(new T[,] {
        { _multiply(yCos, zCos), _negate(_multiply(yCos, zSin)), ySin },
        { _add(_multiply(xCos, zSin), _multiply(_multiply(xSin, ySin), zCos)), _add(_multiply(xCos, zCos), _multiply(_multiply(xSin, ySin), zSin)), _negate(_multiply(xSin, yCos)) },
        { _subtract(_multiply(xSin, zSin), _multiply(_multiply(xCos, ySin), zCos)), _add(_multiply(xSin, zCos), _multiply(_multiply(xCos, ySin), zSin)), _multiply(xCos, yCos) }});
    }

    /// <param name="angleX">Angle about the X-axis in radians.</param>
    /// <param name="angleY">Angle about the Y-axis in radians.</param>
    /// <param name="angleZ">Angle about the Z-axis in radians.</param>
    public static Matrix<T> Factory3x3RotationZthenYthenX(T angleX, T angleY, T angleZ)
    {
      T xCos = _cos(angleX), xSin = _sin(angleX),
        yCos = _cos(angleY), ySin = _sin(angleY),
        zCos = _cos(angleZ), zSin = _sin(angleZ);
      return new Matrix<T>(new T[,] {
        { _multiply(yCos, zCos), _subtract(_multiply(_multiply(zCos, xSin), ySin), _multiply(xCos, zSin)), _add(_multiply(_multiply(xCos, zCos), ySin), _multiply(xSin, zSin)) },
        { _multiply(yCos, zSin), _add(_multiply(xCos, zCos), _multiply(_multiply(xSin, ySin), zSin)), _add(_multiply(_negate(zCos), xSin), _multiply(_multiply(xCos, ySin), zSin)) },
        { _negate(ySin), _multiply(yCos, xSin), _multiply(xCos, yCos) }});
    }

    /// <summary>Creates a 3x3 matrix initialized with a shearing transformation.</summary>
    /// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
    /// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
    /// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
    /// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
    /// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
    /// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
    /// <returns>The constructed shearing matrix.</returns>
    public static Matrix<T> Factory3x3Shear(
      T shearXbyY, T shearXbyZ, T shearYbyX,
      T shearYbyZ, T shearZbyX, T shearZbyY)
    {
      return new Matrix<T>(new T[,] {
        { _one, shearYbyX, shearZbyX },
        { shearXbyY, _one, shearYbyZ },
        { shearXbyZ, shearYbyZ, _one }});
    }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static Matrix<T> operator -(Matrix<T> matrix) { return Matrix<T>.Negate(matrix); }
    /// <summary>Does a standard matrix addition.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after teh addition.</returns>
    public static Matrix<T> operator +(Matrix<T> left, Matrix<T> right) { return Matrix<T>.Add(left, right); }
    /// <summary>Does a standard matrix subtraction.</summary>
    /// <param name="left">The left matrix of the subtraction.</param>
    /// <param name="right">The right matrix of the subtraction.</param>
    /// <returns>The result of the matrix subtraction.</returns>
    public static Matrix<T> operator -(Matrix<T> left, Matrix<T> right) { return Matrix<T>.Subtract(left, right); }
    /// <summary>Multiplies a vector by a matrix.</summary>
    /// <param name="left">The matrix of the multiplication.</param>
    /// <param name="right">The vector of the multiplication.</param>
    /// <returns>The resulting vector after the multiplication.</returns>
    public static Vector<T> operator *(Matrix<T> left, Vector<T> right) { return Matrix<T>.Multiply(left, right); }
    /// <summary>Does a standard matrix multiplication.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix after the multiplication.</returns>
    public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right) { return Matrix<T>.Multiply(left, right); }
    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have its values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static Matrix<T> operator *(Matrix<T> left, T right) { return Matrix<T>.Multiply(left, right); }
    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The scalar to multiply the values by.</param>
    /// <param name="right">The matrix to have its values multiplied.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static Matrix<T> operator *(T left, Matrix<T> right) { return Matrix<T>.Multiply(right, left); }
    /// <summary>Divides all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have its values divided.</param>
    /// <param name="right">The scalar to divide the values by.</param>
    /// <returns>The resulting matrix after the divisions.</returns>
    public static Matrix<T> operator /(Matrix<T> left, T right) { return Matrix<T>.Divide(left, right); }
    /// <summary>Applies a power to a matrix.</summary>
    /// <param name="left">The matrix to apply a power to.</param>
    /// <param name="right">The power to apply to the matrix.</param>
    /// <returns>The result of the power operation.</returns>
    public static Matrix<T> operator ^(Matrix<T> left, int right) { return Matrix<T>.Power(left, right); }
    /// <summary>Checks for equality by value.</summary>
    /// <param name="left">The left matrix of the equality check.</param>
    /// <param name="right">The right matrix of the equality check.</param>
    /// <returns>True if the values of the matrices are equal, false if not.</returns>
    public static bool operator ==(Matrix<T> left, Matrix<T> right) { return Matrix<T>.EqualsByValue(left, right); }
    /// <summary>Checks for false-equality by value.</summary>
    /// <param name="left">The left matrix of the false-equality check.</param>
    /// <param name="right">The right matrix of the false-equality check.</param>
    /// <returns>True if the values of the matrices are not equal, false if they are.</returns>
    public static bool operator !=(Matrix<T> left, Matrix<T> right) { return !Matrix<T>.EqualsByValue(left, right); }
    /// <summary>Automatically converts a matrix into a float[,] if necessary.</summary>
    /// <param name="matrix">The matrix to convert to a float[,].</param>
    /// <returns>The reference to the float[,] representing the matrix.</returns>
    public static implicit operator T[,](Matrix<T> matrix) { return matrix._matrix; }
    /// <summary>Automatically converts a matrix into a float[,] if necessary.</summary>
    /// <param name="matrix">The matrix to convert to a float[,].</param>
    /// <returns>The reference to the float[,] representing the matrix.</returns>
    public static implicit operator Matrix<T>(T[,] matrix) { return new Matrix<T>(matrix); }

    /// <summary>Negates all the values in this matrix.</summary>
    /// <returns>The resulting matrix after the negations.</returns>
    private Matrix<T> Negate() { return Matrix<T>.Negate(this); }
    /// <summary>Does a standard matrix addition.</summary>
    /// <param name="right">The matrix to add to this matrix.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    private Matrix<T> Add(Matrix<T> right) { return Matrix<T>.Add(this, right); }
    /// <summary>Does a standard matrix multiplication (triple for loop).</summary>
    /// <param name="right">The matrix to multiply this matrix by.</param>
    /// <returns>The resulting matrix after the multiplication.</returns>
    private Matrix<T> Multiply(Matrix<T> right) { return Matrix<T>.Multiply(this, right); }
    /// <summary>Multiplies all the values in this matrix by a scalar.</summary>
    /// <param name="right">The scalar to multiply all the matrix values by.</param>
    /// <returns>The retulting matrix after the multiplications.</returns>
    private Matrix<T> Multiply(T right) { return Matrix<T>.Multiply(this, right); }
    /// <summary>Divides all the values in this matrix by a scalar.</summary>
    /// <param name="right">The scalar to divide the matrix values by.</param>
    /// <returns>The resulting matrix after teh divisions.</returns>
    private Matrix<T> Divide(T right) { return Matrix<T>.Divide(this, right); }
    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="row">The restricted row of the minor.</param>
    /// <param name="column">The restricted column of the minor.</param>
    /// <returns>The minor from the row/column restrictions.</returns>
    public Matrix<T> Minor(int row, int column) { return Matrix<T>.Minor(this, row, column); }
    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="right">The matrix to combine with on the right side.</param>
    /// <returns>The resulting row-wise concatination.</returns>
    public Matrix<T> ConcatenateRowWise(Matrix<T> right) { return Matrix<T>.ConcatenateRowWise(this, right); }
    /// <summary>Computes the determinent if this matrix is square.</summary>
    /// <returns>The computed determinent if this matrix is square.</returns>
    public T Determinent() { return Matrix<T>.Determinent(this); }
    /// <summary>Computes the echelon form of this matrix (aka REF).</summary>
    /// <returns>The computed echelon form of this matrix (aka REF).</returns>
    public Matrix<T> Echelon() { return Matrix<T>.Echelon(this); }
    /// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
    /// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
    public Matrix<T> ReducedEchelon() { return Matrix<T>.ReducedEchelon(this); }
    /// <summary>Computes the inverse of this matrix.</summary>
    /// <returns>The inverse of this matrix.</returns>
    public Matrix<T> Inverse() { return Matrix<T>.Inverse(this); }
    /// <summary>Gets the adjoint of this matrix.</summary>
    /// <returns>The adjoint of this matrix.</returns>
    public Matrix<T> Adjoint() { return Matrix<T>.Adjoint(this); }
    /// <summary>Transposes this matrix.</summary>
    /// <returns>The transpose of this matrix.</returns>
    public Matrix<T> Transpose() { return Matrix<T>.Transpose(this); }
    /// <summary>Copies this matrix.</summary>
    /// <returns>The copy of this matrix.</returns>
    public Matrix<T> Clone() { return Matrix<T>.Clone(this); }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static T[,] Negate(T[,] matrix)
    {
      T[,] result = new T[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = _negate(matrix[i, j]);
      return result;
    }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static Matrix<T> Negate(Matrix<T> matrix)
    {
      return Matrix<T>.Negate(matrix._matrix);
    }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static T[,] Add(T[,] left, T[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid addition (size miss-match).");
      T[,] result = new T[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          result[i, j] = _add(left[i, j], right[i, j]);
      return result;
    }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static Matrix<T> Add(Matrix<T> left, Matrix<T> right)
    {
      return Matrix<T>.Add(left._matrix, right._matrix);
    }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static T[,] Subtract(T[,] left, T[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new Error("invalid subtraction (size miss-match).");
      T[,] result = new T[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          result[i, j] = _subtract(left[i, j], right[i, j]);
      return result;
    }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static Matrix<T> Subtract(Matrix<T> left, Matrix<T> right)
    {
      return Matrix<T>.Subtract(left._matrix, right._matrix);
    }

    ///// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    ///// <param name="left">The matrix to have the values subtracted from.</param>
    ///// <param name="right">The scalar to subtract from all the matrix values.</param>
    ///// <returns>The resulting matrix after the subtractions.</returns>
    //public static Vector Subtract(float[,] left, float[] right)
    //{
    //  if (!(left.GetLength(1) == 1 && left.GetLength(0) == right.Length))
    //    throw new MatrixException("invalid subtraction (size miss-match).");
    //  Vector result = new Vector(left.GetLength(0));
    //  for (int i = 0; i < result.Dimensions; i++)
    //    result[i] = left[i, 0] - right[i];
    //  return result;
    //}

    /// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static T[,] Multiply(T[,] left, T[,] right)
    {
      if (left.GetLength(1) != right.GetLength(0))
        throw new Error("invalid multiplication (size miss-match).");
      T[,] result = new T[left.GetLength(0), right.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < right.GetLength(1); j++)
          for (int k = 0; k < left.GetLength(1); k++)
            result[i, j] = _add(result[i, j], _multiply(left[i, k], right[k, j]));
      return result;
    }

    /// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static Matrix<T> Multiply(Matrix<T> left, Matrix<T> right)
    {
      return Matrix<T>.Multiply(left._matrix, right._matrix);
    }

    /// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static T[] Multiply(T[,] left, T[] right)
    {
      if (left.GetLength(1) != right.GetLength(0))
        throw new Error("invalid multiplication (size miss-match).");
      T[] result = new T[left.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < right.GetLength(1); j++)
            result[i] = _add(result[i], _multiply(left[i, j], right[j]));
      return result;
    }

    /// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static Vector<T> Multiply(Matrix<T> left, Vector<T> right)
    {
      return Matrix<T>.Multiply(left._matrix, right._vector);
    }

    ///// <summary>Does a standard multiplication between a matrix and a vector.</summary>
    ///// <param name="left">The left matrix of the multiplication.</param>
    ///// <param name="right">The right vector of the multiplication.</param>
    ///// <returns>The resulting matrix/vector of the multiplication.</returns>
    //public static Matrix Multiply(float[,] left, float[] right)
    //{
    //  if (left.GetLength(1) != right.GetLength(0))
    //    throw new MatrixException("invalid multiplication (size miss-match).");
    //  Matrix result = new Matrix(left.GetLength(0), right.GetLength(1));
    //  for (int i = 0; i < result.Rows; i++)
    //      for (int k = 0; k < left.GetLength(1); k++)
    //        result[i, j] += left[i, k] * right[k];
    //  return result;
    //}

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static T[,] Multiply(T[,] left, T right)
    {
      T[,] result = new T[left.GetLength(0), left.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          result[i, j] = _multiply(left[i, j], right);
      return result;
    }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static Matrix<T> Multiply(Matrix<T> left, T right)
    {
      return Matrix<T>.Multiply(left._matrix, right);
    }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static T[,] Power(T[,] matrix, int power)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid power (!matrix.IsSquare).");
      if (!(power > -1))
        throw new Error("invalid power !(power > -1)");
      if (power == 0)
        return Matrix<T>.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
      T[,] result = matrix.Clone() as T[,];
      for (int i = 0; i < power; i++)
        result = Matrix<T>.Multiply(result, result);
      return result;
    }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static Matrix<T> Power(Matrix<T> matrix, int power)
    {
      return Matrix<T>.Power(matrix._matrix, power);
    }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static T[,] Divide(T[,] matrix, T right)
    {
      T[,] result = new T[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = _divide(matrix[i, j], right);
      return result;
    }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static T[,] Minor(T[,] matrix, int row, int column)
    {
      T[,] minor = new T[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
      int m = 0, n = 0;
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        if (i == row) continue;
        n = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          if (j == column) continue;
          minor[m, n] = matrix[i, j];
          n++;
        }
        m++;
      }
      return minor;
    }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static Matrix<T> Minor(Matrix<T> matrix, int row, int column)
    {
      return Matrix<T>.Minor(matrix._matrix, row, column);
    }

    private static void RowMultiplication(T[,] matrix, int row, T scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[row, j] = _multiply(matrix[row, j], scalar);
    }

    private static void RowAddition(T[,] matrix, int target, int second, T scalar)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[target, j] = _add(matrix[target, j], _multiply(matrix[second, j], scalar));
    }

    private static void SwapRows(T[,] matrix, int row1, int row2)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
      {
        T temp = matrix[row1, j];
        matrix[row1, j] = matrix[row2, j];
        matrix[row2, j] = temp;
      }
    }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static T[,] ConcatenateRowWise(T[,] left, T[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0))
        throw new Error("invalid row-wise concatenation !(left.Rows == right.Rows).");
      T[,] result = new T[left.GetLength(0), left.GetLength(1) + right.GetLength(1)];
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < result.GetLength(1); j++)
        {
          if (j < left.GetLength(1)) result[i, j] = left[i, j];
          else result[i, j] = right[i, j - left.GetLength(1)];
        }
      return result;
    }

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static Matrix<T> ConcatenateRowWise(Matrix<T> left, Matrix<T> right)
    {
      return Matrix<T>.ConcatenateRowWise(left._matrix, right._matrix);
    }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static T Determinent(T[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("invalid determinent !(matrix.IsSquare).");
      T det = _one;
      try
      {
        T[,] rref = matrix.Clone() as T[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (_equate(rref[i, i], _zero))
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (!_equate(rref[j, i], _zero))
              {
                Matrix<T>.SwapRows(rref, i, j);
                det = _negate(det);
              }
          det = _multiply(det, rref[i, i]);
          Matrix<T>.RowMultiplication(rref, i, _Invert_Multiplicative(rref[i, i]));
          for (int j = i + 1; j < rref.GetLength(0); j++)
            Matrix<T>.RowAddition(rref, j, i, _negate(rref[j, i]));
          for (int j = i - 1; j >= 0; j--)
            Matrix<T>.RowAddition(rref, j, i, _negate(rref[j, i]));
        }
        return det;
      }
      catch (Exception) { throw new Error("determinent computation failed."); }
    }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static T Determinent(Matrix<T> matrix)
    {
      return Matrix<T>.Determinent(matrix._matrix);
    }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static T[,] Echelon(T[,] matrix)
    {
      try
      {
        T[,] result = matrix.Clone() as T[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (_equate(result[i, i], _zero))
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (!_equate(result[j, i], _zero))
                Matrix<T>.SwapRows(result, i, j);
          if (_equate(result[i, i], _zero))
            continue;
          if (!_equate(result[i, i], _one))
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (_equate(result[j, i], _one))
                Matrix<T>.SwapRows(result, i, j);
          Matrix<T>.RowMultiplication(result, i, _Invert_Multiplicative(result[i, i]));
          for (int j = i + 1; j < result.GetLength(0); j++)
            Matrix<T>.RowAddition(result, j, i, _negate(result[j, i]));
        }
        return result;
      }
      catch { throw new Error("echelon computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static Matrix<T> Echelon(Matrix<T> matrix)
    {
      return Matrix<T>.Echelon(matrix._matrix);
    }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static T[,] ReducedEchelon(T[,] matrix)
    {
      try
      {
        T[,] result = matrix.Clone() as T[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (_equate(result[i, i], _one))
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (!_equate(result[j, i], _zero))
                Matrix<T>.SwapRows(result, i, j);
          if (_equate(result[i, i], _zero)) continue;
          if (!_equate(result[i, i], _one))
            for (int j = i + 1; j < result.GetLength(0); j++)
              if (_equate(result[j, i], _one))
                Matrix<T>.SwapRows(result, i, j);
          Matrix<T>.RowMultiplication(result, i, _Invert_Multiplicative(result[i, i]));
          for (int j = i + 1; j < result.GetLength(0); j++)
            Matrix<T>.RowAddition(result, j, i, _negate(result[j, i]));
          for (int j = i - 1; j >= 0; j--)
            Matrix<T>.RowAddition(result, j, i, _negate(result[j, i]));
        }
        return result;
      }
      catch { throw new Error("reduced echelon calculation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static Matrix<T> ReducedEchelon(Matrix<T> matrix)
    {
      return Matrix<T>.ReducedEchelon(matrix._matrix);
    }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static T[,] Inverse(T[,] matrix)
    {
      if (_equate(Matrix<T>.Determinent(matrix), _zero))
        throw new Error("inverse calculation failed.");
      try
      {
        T[,] identity = Matrix<T>.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
        T[,] rref = matrix.Clone() as T[,];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (_equate(rref[i, i], _zero))
            for (int j = i + 1; j < rref.GetLength(0); j++)
              if (!_equate(rref[j, i], _zero))
              {
                Matrix<T>.SwapRows(rref, i, j);
                Matrix<T>.SwapRows(identity, i, j);
              }
          Matrix<T>.RowMultiplication(identity, i, _Invert_Multiplicative(rref[i, i]));
          Matrix<T>.RowMultiplication(rref, i, _Invert_Multiplicative(rref[i, i]));
          for (int j = i + 1; j < rref.GetLength(0); j++)
          {
            Matrix<T>.RowAddition(identity, j, i, _negate(rref[j, i]));
            Matrix<T>.RowAddition(rref, j, i, _negate(rref[j, i]));
          }
          for (int j = i - 1; j >= 0; j--)
          {
            Matrix<T>.RowAddition(identity, j, i, _negate(rref[j, i]));
            Matrix<T>.RowAddition(rref, j, i, _negate(rref[j, i]));
          }
        }
        return identity;
      }
      catch { throw new Error("inverse calculation failed."); }
    }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static Matrix<T> Inverse(Matrix<T> matrix)
    {
      return Matrix<T>.Inverse(matrix._matrix);
    }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static T[,] Adjoint(T[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("Adjoint of a non-square matrix does not exists");
      T[,] AdjointMatrix = new T[matrix.GetLength(0), matrix.GetLength(1)];
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          if ((i + j) % 2 == 0)
            AdjointMatrix[i, j] = Matrix<T>.Determinent(Matrix<T>.Minor(matrix, i, j));
          else
            AdjointMatrix[i, j] = _negate(Matrix<T>.Determinent(Matrix<T>.Minor(matrix, i, j)));
      return Matrix<T>.Transpose(AdjointMatrix);
    }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static Matrix<T> Adjoint(Matrix<T> matrix)
    {
      return Matrix<T>.Adjoint(matrix._matrix);
    }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static T[,] Transpose(T[,] matrix)
    {
      T[,] transpose = new T[matrix.GetLength(1), matrix.GetLength(0)];
      for (int i = 0; i < transpose.GetLength(0); i++)
        for (int j = 0; j < transpose.GetLength(1); j++)
          transpose[i, j] = matrix[j, i];
      return transpose;
    }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static Matrix<T> Transpose(Matrix<T> matrix)
    {
      return Matrix<T>.Transpose(matrix._matrix);
    }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="lower">The computed lower triangular matrix.</param>
    /// <param name="upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(T[,] matrix, out T[,] lower, out T[,] upper)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new Error("The matrix is not square!");
      lower = Matrix<T>.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
      upper = matrix.Clone() as T[,];
      int[] permutation = new int[matrix.GetLength(0)];
      for (int i = 0; i < matrix.GetLength(0); i++) permutation[i] = i;
      T p = _zero, pom2, detOfP = _one;
      int k0 = 0, pom1 = 0;
      for (int k = 0; k < matrix.GetLength(1) - 1; k++)
      {
        p = _zero;
        for (int i = k; i < matrix.GetLength(0); i++)
          if (_compare(_abs(upper[i, k]), p) == Comparison.Greater)
          {
            p = _abs(upper[i, k]);
            k0 = i;
          }
        if (_equate(p, _zero))
          throw new Error("The matrix is singular!");
        pom1 = permutation[k];
        permutation[k] = permutation[k0];
        permutation[k0] = pom1;
        for (int i = 0; i < k; i++)
        {
          pom2 = lower[k, i];
          lower[k, i] = lower[k0, i];
          lower[k0, i] = pom2;
        }
        if (k != k0)
          detOfP = _negate(detOfP);
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
          pom2 = upper[k, i];
          upper[k, i] = upper[k0, i];
          upper[k0, i] = pom2;
        }
        for (int i = k + 1; i < matrix.GetLength(0); i++)
        {
          lower[i, k] = _divide(upper[i, k], upper[k, k]);
          for (int j = k; j < matrix.GetLength(1); j++)
            upper[i, j] = _subtract(upper[i, j], _multiply(lower[i, k], upper[k, j]));
        }
      }
    }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="lower">The computed lower triangular matrix.</param>
    /// <param name="upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(Matrix<T> matrix, out Matrix<T> lower, out Matrix<T> upper)
    {
      T[,] lower_array, upper_array;
      Matrix<T>.DecomposeLU(matrix._matrix, out lower_array, out upper_array);
      lower = lower_array;
      upper = upper_array;
    }

    /// <summary>Creates a copy of a matrix.</summary>
    /// <param name="matrix">The matrix to copy.</param>
    /// <returns>A copy of the matrix.</returns>
    public static Matrix<T> Clone(T[,] matrix)
    {
      return new Matrix<T>(matrix.Clone() as T[,]);
    }

    /// <summary>Does a value equality check.</summary>
    /// <param name="left">The first matrix to check for equality.</param>
    /// <param name="right">The second matrix to check for equality.</param>
    /// <returns>True if values are equal, false if not.</returns>
    public static bool EqualsByValue(T[,] left, T[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        return false;
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          if (!_equate(left[i, j], right[i, j]))
            return false;
      return true;
    }

    /// <summary>Does a value equality check with leniency.</summary>
    /// <param name="left">The first matrix to check for equality.</param>
    /// <param name="right">The second matrix to check for equality.</param>
    /// <param name="leniency">How much the values can vary but still be considered equal.</param>
    /// <returns>True if values are equal, false if not.</returns>
    public static bool EqualsByValue(T[,] left, T[,] right, T leniency)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        return false;
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          if (_compare(_abs(_subtract(left[i, j], right[i, j])), leniency) == Comparison.Greater)
            return false;
      return true;
    }

    /// <summary>Checks if two matrices are equal by reverences.</summary>
    /// <param name="left">The left matric of the equality check.</param>
    /// <param name="right">The right matrix of the equality check.</param>
    /// <returns>True if the references are equal, false if not.</returns>
    public static bool EqualsByReference(T[,] left, T[,] right)
    {
      return object.ReferenceEquals(left, right);
    }

    /// <summary>Checks if two matrices are equal by reverences.</summary>
    /// <param name="left">The left matric of the equality check.</param>
    /// <param name="right">The right matrix of the equality check.</param>
    /// <returns>True if the references are equal, false if not.</returns>
    public static bool EqualsByReference(Matrix<T> left, Matrix<T> right)
    {
      return Matrix<T>.ReferenceEquals(left._matrix, right._matrix);
    }

    /// <summary>Prints out a string representation of this matrix.</summary>
    /// <returns>A string representing this matrix.</returns>
    public override string ToString()
    {
      return base.ToString();
      //StringBuilder matrix = new StringBuilder();
      //for (int i = 0; i < Rows; i++)
      //{
      //  for (int j = 0; j < Columns; j++)
      //    matrix.Append(String.Format("{0:0.##}\t", _matrix[i, j]));
      //  matrix.Append("\n");
      //}
      //return matrix.ToString();
    }

    /// <summary>Computes a hash code from the values of this matrix.</summary>
    /// <returns>A hash code for the matrix.</returns>
    public override int GetHashCode()
    {
      // return base.GetHashCode();
      int hash = _matrix[0, 0].GetHashCode();
      for (int i = 0; i < Rows; i++)
        for (int j = 0; j < Columns; j++)
          hash = hash ^ _matrix[i, j].GetHashCode();
      return hash;
    }

    /// <summary>Does an equality check by reference.</summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns>True if the references are equal, false if not.</returns>
    public override bool Equals(object obj)
    {
      return base.Equals(obj) || _matrix.Equals(obj);
    }

    private class Error : LinearAlgebra.Error
    {
      public Error(string Message) : base(Message) { }
    }
  }
}

// These are the old versions of the linear algebra classes. I am keeping these here in-case
// I fucked something up whiel converting them into generic versions.
#region OLD IMPLEMENTATIONS (NON-GENERIC)

#region Quaternions

//namespace Seven.Mathematics
//{
//	/// <summary>Implements a 4-component (x, y, z, and w) quaternion. X, Y, and Z represent the axis
//	/// of rotation, and W is the rotation ammount.</summary>
//	public class _Quaternion
//	{
//		protected float _x, _y, _z, _w;

//		/// <summary>The X component of the quaternion. (axis, NOT rotation ammount)</summary>
//		public float X { get { return _x; } set { _x = value; } }
//		/// <summary>The Y component of the quaternion. (axis, NOT rotation ammount)</summary>
//		public float Y { get { return _y; } set { _y = value; } }
//		/// <summary>The Z component of the quaternion. (axis, NOT rotation ammount)</summary>
//		public float Z { get { return _z; } set { _z = value; } }
//		/// <summary>The W component of the quaternion. (rotation ammount, NOT axis)</summary>
//		public float W { get { return _w; } set { _w = value; } }

//		/// <summary>Constructs a quaternion with the desired values.</summary>
//		/// <param name="x">The x component of the quaternion.</param>
//		/// <param name="y">The y component of the quaternion.</param>
//		/// <param name="z">The z component of the quaternion.</param>
//		/// <param name="w">The w component of the quaternion.</param>
//		public _Quaternion(float x, float y, float z, float w) { _x = x; _y = y; _z = z; _w = w; }

//		/// <summary>Returns new Quaternion(0, 0, 0, 1).</summary>
//		public static readonly _Quaternion FactoryIdentity = new _Quaternion(0, 0, 0, 1);

//		/// <summary>Creates a quaternion from an axis and rotation.</summary>
//		/// <param name="axis">The to create the quaternion from.</param>
//		/// <param name="angle">The angle to create teh quaternion from.</param>
//		/// <returns>The newly created quaternion.</returns>
//		public static _Quaternion FactoryFromAxisAngle(Vector axis, float angle)
//		{
//			if (axis.LengthSquared() == 0.0f)
//				return FactoryIdentity;
//			float sinAngleOverAxisLength = Calc.Sin(angle / 2) / axis.Length();
//			return _Quaternion.Normalize(new _Quaternion(
//				axis.X * sinAngleOverAxisLength,
//				axis.Y * sinAngleOverAxisLength,
//				axis.Z * sinAngleOverAxisLength,
//				Calc.Cos(angle / 2)));
//		}

//		/// <summary>Adds two quaternions together.</summary>
//		/// <param name="left">The first quaternion of the addition.</param>
//		/// <param name="right">The second quaternion of the addition.</param>
//		/// <returns>The result of the addition.</returns>
//		public static _Quaternion operator +(_Quaternion left, _Quaternion right) { return _Quaternion.Add(left, right); }
//		/// <summary>Subtracts two quaternions.</summary>
//		/// <param name="left">The left quaternion of the subtraction.</param>
//		/// <param name="right">The right quaternion of the subtraction.</param>
//		/// <returns>The resulting quaternion after the subtraction.</returns>
//		public static _Quaternion operator -(_Quaternion left, _Quaternion right) { return _Quaternion.Subtract(left, right); }
//		/// <summary>Multiplies two quaternions together.</summary>
//		/// <param name="left">The first quaternion of the multiplication.</param>
//		/// <param name="right">The second quaternion of the multiplication.</param>
//		/// <returns>The resulting quaternion after the multiplication.</returns>
//		public static _Quaternion operator *(_Quaternion left, _Quaternion right) { return _Quaternion.Multiply(left, right); }
//		/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
//		/// <param name="left">The quaternion to pre-multiply the vector by.</param>
//		/// <param name="vector">The vector to be multiplied.</param>
//		/// <returns>The resulting quaternion of the multiplication.</returns>
//		public static _Quaternion operator *(_Quaternion left, Vector right) { return _Quaternion.Multiply(left, right); }
//		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
//		/// <param name="left">The quaternion of the multiplication.</param>
//		/// <param name="right">The scalar of the multiplication.</param>
//		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
//		public static _Quaternion operator *(_Quaternion left, float right) { return _Quaternion.Multiply(left, right); }
//		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
//		/// <param name="left">The scalar of the multiplication.</param>
//		/// <param name="right">The quaternion of the multiplication.</param>
//		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
//		public static _Quaternion operator *(float left, _Quaternion right) { return _Quaternion.Multiply(right, left); }
//		/// <summary>Checks for equality by value. (beware float errors)</summary>
//		/// <param name="left">The first quaternion of the equality check.</param>
//		/// <param name="right">The second quaternion of the equality check.</param>
//		/// <returns>true if the values were deemed equal, false if not.</returns>
//		public static bool operator ==(_Quaternion left, _Quaternion right) { return _Quaternion.Equals(left, right); }
//		/// <summary>Checks for anti-equality by value. (beware float errors)</summary>
//		/// <param name="left">The first quaternion of the anti-equality check.</param>
//		/// <param name="right">The second quaternion of the anti-equality check.</param>
//		/// <returns>false if the values were deemed equal, true if not.</returns>
//		public static bool operator !=(_Quaternion left, _Quaternion right) { return !_Quaternion.Equals(left, right); }

//		/// <summary>Computes the length of quaternion.</summary>
//		/// <returns>The length of the given quaternion.</returns>
//		public float Length() { return _Quaternion.Magnitude(this); }
//		/// <summary>Computes the length of a quaternion, but doesn't square root it
//		/// for optimization possibilities.</summary>
//		/// <returns>The squared length of the given quaternion.</returns>
//		public float LengthSquared() { return _Quaternion.MagnitudeSquared(this); }
//		/// <summary>Gets the conjugate of the quaternion.</summary>
//		/// <returns>The conjugate of teh given quaternion.</returns>
//		public _Quaternion Conjugate() { return _Quaternion.Conjugate(this); }
//		/// <summary>Adds two quaternions together.</summary>
//		/// <param name="right">The second quaternion of the addition.</param>
//		/// <returns>The result of the addition.</returns>
//		public _Quaternion Add(_Quaternion right) { return _Quaternion.Add(this, right); }
//		/// <summary>Subtracts two quaternions.</summary>
//		/// <param name="right">The right quaternion of the subtraction.</param>
//		/// <returns>The resulting quaternion after the subtraction.</returns>
//		public _Quaternion Subtract(_Quaternion right) { return _Quaternion.Subtract(this, right); }
//		/// <summary>Multiplies two quaternions together.</summary>
//		/// <param name="right">The second quaternion of the multiplication.</param>
//		/// <returns>The resulting quaternion after the multiplication.</returns>
//		public _Quaternion Multiply(_Quaternion right) { return _Quaternion.Multiply(this, right); }
//		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
//		/// <param name="right">The scalar of the multiplication.</param>
//		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
//		public _Quaternion Multiply(float right) { return _Quaternion.Multiply(this, right); }
//		/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
//		/// <param name="right">The vector to be multiplied.</param>
//		/// <returns>The resulting quaternion of the multiplication.</returns>
//		public _Quaternion Multiply(Vector vector) { return _Quaternion.Multiply(this, vector); }
//		/// <summary>Normalizes the quaternion.</summary>
//		/// <returns>The normalization of the given quaternion.</returns>
//		public _Quaternion Normalize() { return _Quaternion.Normalize(this); }
//		/// <summary>Inverts a quaternion.</summary>
//		/// <returns>The inverse of the given quaternion.</returns>
//		public _Quaternion Invert() { return _Quaternion.Invert(this); }
//		/// <summary>Lenearly interpolates between two quaternions.</summary>
//		/// <param name="right">The ending point of the interpolation.</param>
//		/// <param name="blend">The ratio 0.0-1.0 of how far to interpolate between the left and right quaternions.</param>
//		/// <returns>The result of the interpolation.</returns>
//		public _Quaternion Lerp(_Quaternion right, float blend) { return _Quaternion.Lerp(this, right, blend); }
//		/// <summary>Sphereically interpolates between two quaternions.</summary>
//		/// <param name="right">The ending point of the interpolation.</param>
//		/// <param name="blend">The ratio of how far to interpolate between the left and right quaternions.</param>
//		/// <returns>The result of the interpolation.</returns>
//		public _Quaternion Slerp(_Quaternion right, float blend) { return _Quaternion.Slerp(this, right, blend); }
//		/// <summary>Rotates a vector by a quaternion.</summary>
//		/// <param name="vector">The vector to be rotated by.</param>
//		/// <returns>The result of the rotation.</returns>
//		public Vector Rotate(Vector vector) { return _Quaternion.Rotate(this, vector); }
//		/// <summary>Does a value equality check.</summary>
//		/// <param name="right">The second quaternion  to check for equality.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public bool EqualsValue(_Quaternion right) { return _Quaternion.EqualsValue(this, right); }
//		/// <summary>Does a value equality check with leniency.</summary>
//		/// <param name="right">The second quaternion to check for equality.</param>
//		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public bool EqualsValue(_Quaternion right, float leniency) { return _Quaternion.EqualsValue(this, right, leniency); }
//		/// <summary>Checks if two matrices are equal by reverences.</summary>
//		/// <param name="right">The right quaternion of the equality check.</param>
//		/// <returns>True if the references are equal, false if not.</returns>
//		public bool EqualsReference(_Quaternion right) { return _Quaternion.EqualsReference(this, right); }
//		/// <summary>Converts a quaternion into a matrix.</summary>
//		/// <returns>The resulting matrix.</returns>
//		public Matrix_Flattened ToMatrix() { return _Quaternion.ToMatrix(this); }

//		/// <summary>Computes the length of quaternion.</summary>
//		/// <param name="quaternion">The quaternion to compute the length of.</param>
//		/// <returns>The length of the given quaternion.</returns>
//		public static float Magnitude(_Quaternion quaternion)
//		{
//			return
//				Calc.SquareRoot(
//					quaternion.X * quaternion.X +
//					quaternion.Y * quaternion.Y +
//					quaternion.Z * quaternion.Z +
//					quaternion.W * quaternion.W);
//		}

//		/// <summary>Computes the length of a quaternion, but doesn't square root it
//		/// for optimization possibilities.</summary>
//		/// <param name="quaternion">The quaternion to compute the length squared of.</param>
//		/// <returns>The squared length of the given quaternion.</returns>
//		public static float MagnitudeSquared(_Quaternion quaternion)
//		{
//			return
//				quaternion.X * quaternion.X +
//				quaternion.Y * quaternion.Y +
//				quaternion.Z * quaternion.Z +
//				quaternion.W * quaternion.W;
//		}

//		/// <summary>Gets the conjugate of the quaternion.</summary>
//		/// <param name="quaternion">The quaternion to conjugate.</param>
//		/// <returns>The conjugate of teh given quaternion.</returns>
//		public static _Quaternion Conjugate(_Quaternion quaternion)
//		{
//			return new _Quaternion(
//				-quaternion.X,
//				-quaternion.Y,
//				-quaternion.Z,
//				quaternion.W);
//		}

//		/// <summary>Adds two quaternions together.</summary>
//		/// <param name="left">The first quaternion of the addition.</param>
//		/// <param name="right">The second quaternion of the addition.</param>
//		/// <returns>The result of the addition.</returns>
//		public static _Quaternion Add(_Quaternion left, _Quaternion right)
//		{
//			return new _Quaternion(
//				left.X + right.X,
//				left.Y + right.Y,
//				left.Z + right.Z,
//				left.W + right.W);
//		}

//		/// <summary>Subtracts two quaternions.</summary>
//		/// <param name="left">The left quaternion of the subtraction.</param>
//		/// <param name="right">The right quaternion of the subtraction.</param>
//		/// <returns>The resulting quaternion after the subtraction.</returns>
//		public static _Quaternion Subtract(_Quaternion left, _Quaternion right)
//		{
//			return new _Quaternion(
//				left.X - right.X,
//				left.Y - right.Y,
//				left.Z - right.Z,
//				left.W - right.W);
//		}

//		/// <summary>Multiplies two quaternions together.</summary>
//		/// <param name="left">The first quaternion of the multiplication.</param>
//		/// <param name="right">The second quaternion of the multiplication.</param>
//		/// <returns>The resulting quaternion after the multiplication.</returns>
//		public static _Quaternion Multiply(_Quaternion left, _Quaternion right)
//		{
//			return new _Quaternion(
//				left.X * right.W + left.W * right.X + left.Y * right.Z - left.Z * right.Y,
//				left.Y * right.W + left.W * right.Y + left.Z * right.X - left.X * right.Z,
//				left.Z * right.W + left.W * right.Z + left.X * right.Y - left.Y * right.X,
//				left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z);
//		}

//		/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary>
//		/// <param name="left">The quaternion of the multiplication.</param>
//		/// <param name="right">The scalar of the multiplication.</param>
//		/// <returns>The result of multiplying all the values in the quaternion by the scalar.</returns>
//		public static _Quaternion Multiply(_Quaternion left, float right)
//		{
//			return new _Quaternion(
//				left.X * right,
//				left.Y * right,
//				left.Z * right,
//				left.W * right);
//		}

//		/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary>
//		/// <param name="left">The quaternion to pre-multiply the vector by.</param>
//		/// <param name="right">The vector to be multiplied.</param>
//		/// <returns>The resulting quaternion of the multiplication.</returns>
//		public static _Quaternion Multiply(_Quaternion left, Vector right)
//		{
//			if (right.Dimensions == 3)
//			{
//				return new _Quaternion(
//					left.W * right.X + left.Y * right.Z - left.Z * right.Y,
//					left.W * right.Y + left.Z * right.X - left.X * right.Z,
//					left.W * right.Z + left.X * right.Y - left.Y * right.X,
//					-left.X * right.X - left.Y * right.Y - left.Z * right.Z);
//			}
//			else
//				throw new QuaternionException("my quaternion rotations are only defined for 3-component vectors.");
//		}

//		/// <summary>Normalizes the quaternion.</summary>
//		/// <param name="quaternion">The quaternion to normalize.</param>
//		/// <returns>The normalization of the given quaternion.</returns>
//		public static _Quaternion Normalize(_Quaternion quaternion)
//		{
//			float normalizer =
//				Calc.SquareRoot(
//					quaternion.X * quaternion.X +
//					quaternion.Y * quaternion.Y +
//					quaternion.Z * quaternion.Z +
//					quaternion.W * quaternion.W);
//			if (normalizer != 0)
//			{
//				normalizer = 1.0f / normalizer;
//				return new _Quaternion(
//					quaternion.X * normalizer,
//					quaternion.Y * normalizer,
//					quaternion.Z * normalizer,
//					quaternion.W * normalizer);
//			}
//			else
//				return new _Quaternion(0, 0, 0, 1);
//		}

//		/// <summary>Inverts a quaternion.</summary>
//		/// <param name="quaternion">The quaternion to find the inverse of.</param>
//		/// <returns>The inverse of the given quaternion.</returns>
//		public static _Quaternion Invert(_Quaternion quaternion)
//		{
//			// EQUATION: invert = quaternion.Conjugate()).Normalized()
//			float normalizer =
//				quaternion.X * quaternion.X +
//				quaternion.Y * quaternion.Y +
//				quaternion.Z * quaternion.Z +
//				quaternion.W * quaternion.W;
//			if (normalizer == 0.0)
//				return new _Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
//			normalizer = 1.0f / normalizer;
//			return new _Quaternion(
//				-quaternion.X * normalizer,
//				-quaternion.Y * normalizer,
//				-quaternion.Z * normalizer,
//				quaternion.W * normalizer);
//		}

//		/// <summary>Lenearly interpolates between two quaternions.</summary>
//		/// <param name="left">The starting point of the interpolation.</param>
//		/// <param name="right">The ending point of the interpolation.</param>
//		/// <param name="blend">The ratio 0.0-1.0 of how far to interpolate between the left and right quaternions.</param>
//		/// <returns>The result of the interpolation.</returns>
//		public static _Quaternion Lerp(_Quaternion left, _Quaternion right, float blend)
//		{
//			if (blend < 0 || blend > 1.0f)
//				throw new QuaternionException("invalid blending value during lerp !(blend < 0.0f || blend > 1.0f).");
//			if (_Quaternion.MagnitudeSquared(left) == 0.0f)
//			{
//				if (_Quaternion.MagnitudeSquared(right) == 0.0f)
//					return FactoryIdentity;
//				else
//					return new _Quaternion(right.X, right.Y, right.Z, right.W);
//			}
//			else if (_Quaternion.MagnitudeSquared(right) == 0.0f)
//				return new _Quaternion(left.X, left.Y, left.Z, left.W);
//			_Quaternion result = new _Quaternion(
//				(1.0f - blend) * left.X + blend * right.X,
//				(1.0f - blend) * left.Y + blend * right.Y,
//				(1.0f - blend) * left.Z + blend * right.Z,
//				(1.0f - blend) * left.W + blend * right.W);
//			if (_Quaternion.MagnitudeSquared(result) > 0.0f)
//				return _Quaternion.Normalize(result);
//			else
//				return _Quaternion.FactoryIdentity;
//		}

//		/// <summary>Sphereically interpolates between two quaternions.</summary>
//		/// <param name="left">The starting point of the interpolation.</param>
//		/// <param name="right">The ending point of the interpolation.</param>
//		/// <param name="blend">The ratio of how far to interpolate between the left and right quaternions.</param>
//		/// <returns>The result of the interpolation.</returns>
//		public static _Quaternion Slerp(_Quaternion left, _Quaternion right, float blend)
//		{
//			if (blend < 0 || blend > 1.0f)
//				throw new QuaternionException("invalid blending value during slerp !(blend < 0.0f || blend > 1.0f).");
//			if (_Quaternion.MagnitudeSquared(left) == 0.0f)
//			{
//				if (_Quaternion.MagnitudeSquared(right) == 0.0f)
//					return FactoryIdentity;
//				else
//					return new _Quaternion(right.X, right.Y, right.Z, right.W);
//			}
//			else if (_Quaternion.MagnitudeSquared(right) == 0.0f)
//				return new _Quaternion(left.X, left.Y, left.Z, left.W);
//			float cosHalfAngle = left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
//			if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
//				return new _Quaternion(left.X, left.Y, left.Z, left.W);
//			else if (cosHalfAngle < 0.0f)
//			{
//				right = new _Quaternion(-left.X, -left.Y, -left.Z, -left.W);
//				cosHalfAngle = -cosHalfAngle;
//			}
//			float halfAngle = (float)Math.Acos(cosHalfAngle);
//			float sinHalfAngle = Calc.Sin(halfAngle);
//			float blendA = Calc.Sin(halfAngle * (1.0f - blend)) / sinHalfAngle;
//			float blendB = Calc.Sin(halfAngle * blend) / sinHalfAngle;
//			_Quaternion result = new _Quaternion(
//				blendA * left.X + blendB * right.X,
//				blendA * left.Y + blendB * right.Y,
//				blendA * left.Z + blendB * right.Z,
//				blendA * left.W + blendB * right.W);
//			if (_Quaternion.MagnitudeSquared(result) > 0.0f)
//				return _Quaternion.Normalize(result);
//			else
//				return _Quaternion.FactoryIdentity;
//		}

//		/// <summary>Rotates a vector by a quaternion [v' = qvq'].</summary>
//		/// <param name="rotation">The quaternion to rotate the vector by.</param>
//		/// <param name="vector">The vector to be rotated by.</param>
//		/// <returns>The result of the rotation.</returns>
//		public static Vector Rotate(_Quaternion rotation, Vector vector)
//		{
//			if (vector.Dimensions != 3)
//				throw new QuaternionException("my quaternion rotations are only defined for 3-component vectors.");
//			_Quaternion answer = _Quaternion.Multiply(_Quaternion.Multiply(rotation, vector), _Quaternion.Conjugate(rotation));
//			return new Vector(answer.X, answer.Y, answer.Z);
//		}

//		/// <summary>Does a value equality check.</summary>
//		/// <param name="left">The first quaternion to check for equality.</param>
//		/// <param name="right">The second quaternion  to check for equality.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public static bool EqualsValue(_Quaternion left, _Quaternion right)
//		{
//			return
//				left.X == right.X &&
//				left.Y == right.Y &&
//				left.Z == right.Z &&
//				left.W == right.W;
//		}

//		/// <summary>Does a value equality check with leniency.</summary>
//		/// <param name="left">The first quaternion to check for equality.</param>
//		/// <param name="right">The second quaternion to check for equality.</param>
//		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public static bool EqualsValue(_Quaternion left, _Quaternion right, float leniency)
//		{
//			return
//				Calc.Abs(left.X - right.X) < leniency &&
//				Calc.Abs(left.Y - right.Y) < leniency &&
//				Calc.Abs(left.Z - right.Z) < leniency &&
//				Calc.Abs(left.W - right.W) > leniency;
//		}

//		/// <summary>Checks if two matrices are equal by reverences.</summary>
//		/// <param name="left">The left quaternion of the equality check.</param>
//		/// <param name="right">The right quaternion of the equality check.</param>
//		/// <returns>True if the references are equal, false if not.</returns>
//		public static bool EqualsReference(_Quaternion left, _Quaternion right)
//		{
//			return object.ReferenceEquals(left, right);
//		}

//		/// <summary>Converts a quaternion into a matrix.</summary>
//		/// <param name="quaternion">The quaternion of the conversion.</param>
//		/// <returns>The resulting matrix.</returns>
//		public static Matrix_Flattened ToMatrix(_Quaternion quaternion)
//		{
//			return new Matrix_Flattened(3, 3,
//				quaternion.W * quaternion.W + quaternion.X * quaternion.X - quaternion.Y * quaternion.Y - quaternion.Z * quaternion.Z,
//				2 * quaternion.X * quaternion.Y - 2 * quaternion.W * quaternion.Z,
//				2 * quaternion.X * quaternion.Z + 2 * quaternion.W * quaternion.Y,
//				2 * quaternion.X * quaternion.Y + 2 * quaternion.W * quaternion.Z,
//				quaternion.W * quaternion.W - quaternion.X * quaternion.X + quaternion.Y * quaternion.Y - quaternion.Z * quaternion.Z,
//				2 * quaternion.Y * quaternion.Z + 2 * quaternion.W * quaternion.X,
//				2 * quaternion.X * quaternion.Z - 2 * quaternion.W * quaternion.Y,
//				2 * quaternion.Y * quaternion.Z - 2 * quaternion.W * quaternion.X,
//				quaternion.W * quaternion.W - quaternion.X * quaternion.X - quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
//		}

//		/// <summary>Converts the quaternion into a string.</summary>
//		/// <returns>The resulting string after the conversion.</returns>
//		public override string ToString()
//		{
//			// Chane this method to format it how you want...
//			return base.ToString();
//			//return "{ " + _x + ", " + _y + ", " + _z + ", " + _w + " }";
//		}

//		/// <summary>Computes a hash code from the values in this quaternion.</summary>
//		/// <returns>The computed hash code.</returns>
//		public override int GetHashCode()
//		{
//			return
//				_x.GetHashCode() ^
//				_y.GetHashCode() ^
//				_z.GetHashCode() ^
//				_w.GetHashCode();
//		}

//		/// <summary>Does a reference equality check.</summary>
//		/// <param name="other"></param>
//		/// <returns></returns>
//		public override bool Equals(object other)
//		{
//			if (other is _Quaternion)
//				return _Quaternion.EqualsReference(this, (_Quaternion)other);
//			return false;
//		}

//		private class QuaternionException : Error
//		{
//			public QuaternionException(string message) : base(message) { }
//		}
//	}
//}

#endregion

#region Vector

//namespace Seven.Mathematics
//{
//	/// <summary>Represents a vector with an arbitrary number of components.</summary>
//	public class _Vector
//	{
//		private float[] _vector;

//		/// <summary>Sane as accessing index 0.</summary>
//		public float X
//		{
//			get { return _vector[0]; }
//			set { _vector[0] = value; }
//		}

//		/// <summary>Same as accessing index 1.</summary>
//		public float Y
//		{
//			get { try { return _vector[1]; } catch { throw new Exception("vector does not contains a y component."); } }
//			set { try { _vector[1] = value; } catch { throw new Exception("vector does not contains a y component."); } }
//		}

//		/// <summary>Same as accessing index 2.</summary>
//		public float Z
//		{
//			get { try { return _vector[2]; } catch { throw new Exception("vector does not contains a z component."); } }
//			set { try { _vector[2] = value; } catch { throw new Exception("vector does not contains a z component."); } }
//		}

//		/// <summary>Same as accessing index 3.</summary>
//		public float W
//		{
//			get { try { return _vector[3]; } catch { throw new Exception("vector does not contains a w component."); } }
//			set { try { _vector[3] = value; } catch { throw new Exception("vector does not contains a w component."); } }
//		}

//		/// <summary>Gives you direct access to the values in this vector.</summary>
//		public float[] Floats { get { return _vector; } }

//		/// <summary>The number of components in this vector.</summary>
//		public int Dimensions { get { return _vector.Length; } }

//		/// <summary>Allows indexed access to this vector.</summary>
//		/// <param name="index">The index to access.</param>
//		/// <returns>The value of the given index.</returns>
//		public float this[int index]
//		{
//			get { try { return _vector[index]; } catch { throw new Exception("index out of bounds."); } }
//			set { try { _vector[index] = value; } catch { throw new Exception("index out of bounds."); } }
//		}

//		/// <summary>Creates a new vector with the given number of components.</summary>
//		/// <param name="dimensions">The number of dimensions this vector will have.</param>
//		public _Vector(int dimensions) { try { _vector = new float[dimensions]; } catch { throw new Exception("invalid dimensions on vector contruction."); } }

//		/// <summary>Creates a vector out of the given values.</summary>
//		/// <param name="vector">The values to initialize the vector to.</param>
//		public _Vector(params float[] vector)
//		{
//			float[] floats = new float[vector.Length];
//			Buffer.BlockCopy(vector, 0, floats, 0, floats.Length * sizeof(float));
//			_vector = floats;
//		}

//		/// <summary>Creates a vector with the given number of components with the values initialized to zeroes.</summary>
//		/// <param name="dimensions">The number of components in the vector.</param>
//		/// <returns>The newly constructed vector.</returns>
//		public static _Vector FactoryZero(int dimensions) { return new _Vector(dimensions); }

//		/// <summary>Creates a vector with the given number of components with the values initialized to ones.</summary>
//		/// <param name="dimensions">The number of components in the vector.</param>
//		/// <returns>The newly constructed vector.</returns>
//		public static _Vector FactoryOne(int dimensions) { return new _Vector(new float[dimensions]); }

//		/// <summary>Returns a 3-component vector representing the x-axis.</summary>
//		public static readonly _Vector FactoryXAxis = new _Vector(1, 0, 0);
//		/// <summary>Returns a 3-component vector representing the y-axis.</summary>
//		public static readonly _Vector FactoryYAxis = new _Vector(0, 1, 0);
//		/// <summary>Returns a 3-component vector representing the z-axis.</summary>
//		public static readonly _Vector FactoryZAxis = new _Vector(0, 0, 1);
//		/// <summary>Returns a 3-component vector representing the negative x-axis.</summary>
//		public static readonly _Vector FactoryNegXAxis = new _Vector(1, 0, 0);
//		/// <summary>Returns a 3-component vector representing the negative y-axis.</summary>
//		public static readonly _Vector FactoryNegYAxis = new _Vector(0, 1, 0);
//		/// <summary>Returns a 3-component vector representing the negative z-axis.</summary>
//		public static readonly _Vector FactoryNegZAxis = new _Vector(0, 0, 1);

//		/// <summary>Adds two vectors together.</summary>
//		/// <param name="left">The first vector of the addition.</param>
//		/// <param name="right">The second vector of the addition.</param>
//		/// <returns>The result of the addition.</returns>
//		public static _Vector operator +(_Vector left, _Vector right) { return _Vector.Add(left, right); }
//		/// <summary>Subtracts two vectors.</summary>
//		/// <param name="left">The left operand of the subtraction.</param>
//		/// <param name="right">The right operand of the subtraction.</param>
//		/// <returns>The result of the subtraction.</returns>
//		public static _Vector operator -(_Vector left, _Vector right) { return _Vector.Subtract(left, right); }
//		/// <summary>Negates a vector.</summary>
//		/// <param name="vector">The vector to negate.</param>
//		/// <returns>The result of the negation.</returns>
//		public static _Vector operator -(_Vector vector) { return _Vector.Negate(vector); }
//		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
//		/// <param name="left">The vector to have all its values multiplied.</param>
//		/// <param name="right">The scalar to multiply all the vector values by.</param>
//		/// <returns>The result of the multiplication.</returns>
//		public static _Vector operator *(_Vector left, float right) { return _Vector.Multiply(left, right); }
//		/// <summary>Multiplies all the values in a vector by a scalar.</summary>
//		/// <param name="left">The scalar to multiply all the vector values by.</param>
//		/// <param name="right">The vector to have all its values multiplied.</param>
//		/// <returns>The result of the multiplication.</returns>
//		public static _Vector operator *(float left, _Vector right) { return _Vector.Multiply(right, left); }
//		/// <summary>Divides all the values in the vector by a scalar.</summary>
//		/// <param name="left">The vector to have its values divided.</param>
//		/// <param name="right">The scalar to divide all the vectors values by.</param>
//		/// <returns>The vector after the divisions.</returns>
//		public static _Vector operator /(_Vector left, float right) { return _Vector.Divide(left, right); }
//		/// <summary>Does an equality check by value. (warning for float errors)</summary>
//		/// <param name="left">The first vector of the equality check.</param>
//		/// <param name="right">The second vector of the equality check.</param>
//		/// <returns>true if the values are equal, false if not.</returns>
//		public static bool operator ==(_Vector left, _Vector right) { return _Vector.EqualsValue(left, right); }
//		/// <summary>Does an anti-equality check by value. (warning for float errors)</summary>
//		/// <param name="left">The first vector of the anit-equality check.</param>
//		/// <param name="right">The second vector of the anti-equality check.</param>
//		/// <returns>true if the values are not equal, false if they are.</returns>
//		public static bool operator !=(_Vector left, _Vector right) { return !_Vector.EqualsValue(left, right); }
//		/// <summary>Automatically converts a vector into a matrix.</summary>
//		/// <param name="vector">The vector of the conversion.</param>
//		/// <returns>The result of the conversion.</returns>
//		public static implicit operator Matrix_Flattened(_Vector vector) { return Matrix_Flattened.UnsafeFactoryFromVector(vector); }

//		/// <summary>Adds two vectors together.</summary>
//		/// <param name="right">The vector to add to this one.</param>
//		/// <returns>The result of the vector.</returns>
//		public _Vector Add(_Vector right) { return _Vector.Add(this, right); }
//		/// <summary>Negates this vector.</summary>
//		/// <returns>The result of the negation.</returns>
//		public _Vector Negate() { return _Vector.Negate(this); }
//		/// <summary>Subtracts another vector from this one.</summary>
//		/// <param name="right">The vector to subtract from this one.</param>
//		/// <returns>The result of the subtraction.</returns>
//		public _Vector Subtract(_Vector right) { return _Vector.Subtract(this, right); }
//		/// <summary>Multiplies the values in this vector by a scalar.</summary>
//		/// <param name="right">The scalar to multiply these values by.</param>
//		/// <returns>The result of the multiplications</returns>
//		public _Vector Multiply(float right) { return _Vector.Multiply(this, right); }
//		/// <summary>Divides all the values in this vector by a scalar.</summary>
//		/// <param name="right">The scalar to divide the values of the vector by.</param>
//		/// <returns>The resulting vector after teh divisions.</returns>
//		public _Vector Divide(float right) { return _Vector.Divide(this, right); }
//		/// <summary>Computes the dot product between this vector and another.</summary>
//		/// <param name="right">The second vector of the dot product operation.</param>
//		/// <returns>The result of the dot product.</returns>
//		public float DotProduct(_Vector right) { return _Vector.DotProduct(this, right); }
//		/// <summary>Computes the cross product between this vector and another.</summary>
//		/// <param name="right">The second vector of the dot product operation.</param>
//		/// <returns>The result of the dot product operation.</returns>
//		public _Vector CrossProduct(_Vector right) { return _Vector.CrossProduct(this, right); }
//		/// <summary>Normalizes this vector.</summary>
//		/// <returns>The result of the normalization.</returns>
//		public _Vector Normalize() { return _Vector.Normalize(this); }
//		/// <summary>Computes the length of this vector.</summary>
//		/// <returns>The length of this vector.</returns>
//		public float Length() { return _Vector.Length(this); }
//		/// <summary>Computes the length of this vector, but doesn't square root it for 
//		/// possible optimization purposes.</summary>
//		/// <returns>The squared length of the vector.</returns>
//		public float LengthSquared() { return _Vector.LengthSquared(this); }
//		/// <summary>Check for equality by value.</summary>
//		/// <param name="right">The other vector of the equality check.</param>
//		/// <returns>true if the values were equal, false if not.</returns>
//		public bool EqualsValue(_Vector right) { return _Vector.EqualsValue(this, right); }
//		/// <summary>Checks for equality by value with some leniency.</summary>
//		/// <param name="right">The other vector of the equality check.</param>
//		/// <param name="leniency">The ammount the values can differ but still be considered equal.</param>
//		/// <returns>true if the values were cinsidered equal, false if not.</returns>
//		public bool EqualsValue(_Vector right, float leniency) { return _Vector.EqualsValue(this, right, leniency); }
//		/// <summary>Checks for equality by reference.</summary>
//		/// <param name="right">The other vector of the equality check.</param>
//		/// <returns>true if the references are equal, false if not.</returns>
//		public bool EqualsReference(_Vector right) { return _Vector.EqualsReference(this, right); }
//		/// <summary>Rotates this vector by quaternon values.</summary>
//		/// <param name="angle">The amount of rotation about the axis.</param>
//		/// <param name="x">The x component deterniming the axis of rotation.</param>
//		/// <param name="y">The y component determining the axis of rotation.</param>
//		/// <param name="z">The z component determining the axis of rotation.</param>
//		/// <returns>The resulting vector after the rotation.</returns>
//		public _Vector RotateBy(float angle, float x, float y, float z) { return _Vector.RotateBy(this, angle, x, y, z); }
//		/// <summary>Computes the linear interpolation between two vectors.</summary>
//		/// <param name="right">The ending vector of the interpolation.</param>
//		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
//		/// <returns>The result of the interpolation.</returns>
//		public _Vector Lerp(_Vector right, float blend) { return _Vector.Lerp(this, right, blend); }
//		/// <summary>Sphereically interpolates between two vectors.</summary>
//		/// <param name="right">The ending vector of the interpolation.</param>
//		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
//		/// <returns>The result of the slerp operation.</returns>
//		public _Vector Slerp(_Vector right, float blend) { return _Vector.Slerp(this, right, blend); }
//		/// <summary>Rotates a vector by a quaternion.</summary>
//		/// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
//		/// <returns>The result of the rotation.</returns>
//		//public _Vector RotateBy(_Quaternion rotation) { return _Vector.RotateBy(this, rotation); }

//		/// <summary>Adds two vectors together.</summary>
//		/// <param name="leftFloats">The first vector of the addition.</param>
//		/// <param name="rightFloats">The second vector of the addiiton.</param>
//		/// <returns>The result of the addiion.</returns>
//		public static _Vector Add(_Vector left, _Vector right)
//		{
//			float[] leftFloats = left.Floats;
//			float[] rightFloats = right.Floats;
//			if (leftFloats.Length != rightFloats.Length)
//				throw new Exception("invalid dimensions on vector addition.");
//			_Vector result = new _Vector(leftFloats.Length);
//			float[] resultFloats = result.Floats;
//			for (int i = 0; i < leftFloats.Length; i++)
//				resultFloats[i] = leftFloats[i] + rightFloats[i];
//			return result;
//		}

//		/// <summary>Negates all the values in a vector.</summary>
//		/// <param name="vector">The vector to have its values negated.</param>
//		/// <returns>The result of the negations.</returns>
//		public static _Vector Negate(_Vector vector)
//		{
//			float[] floats = vector.Floats;
//			_Vector result = new _Vector(floats.Length);
//			float[] resultFloats = result.Floats;
//			for (int i = 0; i < floats.Length; i++)
//				resultFloats[i] = -floats[i];
//			return result;
//		}

//		/// <summary>Subtracts two vectors.</summary>
//		/// <param name="left">The left vector of the subtraction.</param>
//		/// <param name="right">The right vector of the subtraction.</param>
//		/// <returns>The result of the vector subtracton.</returns>
//		public static _Vector Subtract(_Vector left, _Vector right)
//		{
//			float[] leftFloats = left.Floats;
//			float[] rightFloats = right.Floats;
//			_Vector result = new _Vector(leftFloats.Length);
//			float[] resultFloats = result.Floats;
//			if (leftFloats.Length != rightFloats.Length)
//				throw new Exception("invalid dimensions on vector subtraction.");
//			for (int i = 0; i < leftFloats.Length; i++)
//				resultFloats[i] = leftFloats[i] - rightFloats[i];
//			return result;
//		}

//		/// <summary>Multiplies all the components of a vecotr by a scalar.</summary>
//		/// <param name="left">The vector to have the components multiplied by.</param>
//		/// <param name="right">The scalars to multiply the vector components by.</param>
//		/// <returns>The result of the multiplications.</returns>
//		public static _Vector Multiply(_Vector left, float right)
//		{
//			float[] leftFloats = left.Floats;
//			_Vector result = new _Vector(leftFloats.Length);
//			float[] resultFloats = result.Floats;
//			for (int i = 0; i < leftFloats.Length; i++)
//				resultFloats[i] = leftFloats[i] * right;
//			return result;
//		}

//		/// <summary>Divides all the components of a vector by a scalar.</summary>
//		/// <param name="left">The vector to have the components divided by.</param>
//		/// <param name="right">The scalar to divide the vector components by.</param>
//		/// <returns>The resulting vector after teh divisions.</returns>
//		public static _Vector Divide(_Vector left, float right)
//		{
//			float[] floats = left.Floats;
//			_Vector result = new _Vector(floats.Length);
//			float[] resultFloats = result.Floats;
//			int arrayLength = floats.Length;
//			for (int i = 0; i < arrayLength; i++)
//				resultFloats[i] = floats[i] / right;
//			return result;
//		}

//		/// <summary>Computes the dot product between two vectors.</summary>
//		/// <param name="left">The first vector of the dot product operation.</param>
//		/// <param name="right">The second vector of the dot product operation.</param>
//		/// <returns>The result of the dot product operation.</returns>
//		public static float DotProduct(_Vector left, _Vector right)
//		{
//			float[] leftFloats = left.Floats;
//			float[] rightFloats = right.Floats;
//			if (leftFloats.Length != rightFloats.Length)
//				throw new Exception("invalid dimensions on vector dot product.");
//			float result = 0;
//			for (int i = 0; i < leftFloats.Length; i++)
//				result += (leftFloats[i] * rightFloats[i]);
//			return result;
//		}

//		/// <summary>Computes teh cross product of two vectors.</summary>
//		/// <param name="left">The first vector of the cross product operation.</param>
//		/// <param name="right">The second vector of the cross product operation.</param>
//		/// <returns>The result of the cross product operation.</returns>
//		public static _Vector CrossProduct(_Vector left, _Vector right)
//		{
//			if (left.Dimensions != right.Dimensions)
//				throw new Exception("invalid cross product !(left.Dimensions == right.Dimensions)");
//			if (left.Dimensions == 3 || left.Dimensions == 4)
//			{
//				return new _Vector(
//					left[1] * right[2] - left[2] * right[1],
//					left[2] * right[0] - left[0] * right[2],
//					left[0] * right[1] - left[1] * right[0]);
//			}
//			throw new Exception("my cross product function is only defined for 3-component vectors.");
//		}

//		/// <summary>Normalizes a vector.</summary>
//		/// <param name="vector">The vector to normalize.</param>
//		/// <returns>The result of the normalization.</returns>
//		public static _Vector Normalize(_Vector vector)
//		{
//			float length = _Vector.Length(vector);
//			if (length != 0.0)
//			{
//				float[] floats = vector.Floats;
//				_Vector result = new _Vector(floats.Length);
//				float[] resultFloats = result.Floats;
//				int arrayLength = floats.Length;
//				for (int i = 0; i < arrayLength; i++)
//					resultFloats[i] = floats[i] / length;
//				return result;
//			}
//			else
//				return new _Vector(vector.Dimensions);
//		}

//		/// <summary>Computes the length of a vector.</summary>
//		/// <param name="vector">The vector to calculate the length of.</param>
//		/// <returns>The computed length of the vector.</returns>
//		public static float Length(_Vector vector)
//		{
//			float[] floats = vector.Floats;
//			if (floats.Length == 3)
//			{
//				return Calc.SquareRoot(
//					floats[0] * floats[0] +
//					floats[1] * floats[1] +
//					floats[2] * floats[2]);
//			}
//			float result = 0;
//			int arrayLength = floats.Length;
//			for (int i = 0; i < arrayLength; i++)
//				result += (floats[i] * floats[i]);
//			return Calc.SquareRoot(result);
//		}

//		/// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary>
//		/// <param name="vector">The vector to compute the length squared of.</param>
//		/// <returns>The computed length squared of the vector.</returns>
//		public static float LengthSquared(_Vector vector)
//		{
//			float[] floats = vector.Floats;
//			if (floats.Length == 3)
//			{
//				return
//					floats[0] * floats[0] +
//					floats[1] * floats[1] +
//					floats[2] * floats[2];
//			}
//			float result = 0;
//			int arrayLength = floats.Length;
//			for (int i = 0; i < arrayLength; i++)
//				result += (floats[i] * floats[i]);
//			return result;
//		}

//		/// <summary>Computes the angle between two vectors.</summary>
//		/// <param name="first">The first vector to determine the angle between.</param>
//		/// <param name="second">The second vector to determine the angle between.</param>
//		/// <returns>The angle between the two vectors in radians.</returns>
//		public static float Angle(_Vector first, _Vector second)
//		{
//			return Calc.ArcCos(_Vector.DotProduct(first, second) / (_Vector.Length(first) * _Vector.Length(second)));
//		}

//		/// <summary>Rotates a vector by the specified axis and rotation values.</summary>
//		/// <param name="vector">The vector to rotate.</param>
//		/// <param name="angle">The angle of the rotation.</param>
//		/// <param name="x">The x component of the axis vector to rotate about.</param>
//		/// <param name="y">The y component of the axis vector to rotate about.</param>
//		/// <param name="z">The z component of the axis vector to rotate about.</param>
//		/// <returns>The result of the rotation.</returns>
//		public static _Vector RotateBy(_Vector vector, float angle, float x, float y, float z)
//		{
//			if (vector.Dimensions == 3)
//			{
//				float[] floats = vector.Floats;
//				// Note: the angle is in radians
//				float sinHalfAngle = Calc.Sin(angle / 2);
//				float cosHalfAngle = Calc.Cos(angle / 2);
//				x *= sinHalfAngle;
//				y *= sinHalfAngle;
//				z *= sinHalfAngle;
//				float x2 = cosHalfAngle * floats[0] + y * floats[2] - z * floats[1];
//				float y2 = cosHalfAngle * floats[1] + z * floats[0] - x * floats[2];
//				float z2 = cosHalfAngle * floats[2] + x * floats[1] - y * floats[0];
//				float w2 = -x * floats[0] - y * floats[1] - z * floats[2];
//				return new _Vector(
//					x * w2 + cosHalfAngle * x2 + y * z2 - z * y2,
//					y * w2 + cosHalfAngle * y2 + z * x2 - x * z2,
//					z * w2 + cosHalfAngle * z2 + x * y2 - y * x2);
//			}
//			throw new Exception("my RotateBy() function is only defined for 3-component vectors.");
//		}

//		///// <summary>Rotates a vector by a quaternion.</summary>
//		///// <param name="vector">The vector to rotate.</param>
//		///// <param name="rotation">The quaternion to rotate the 3-component vector by.</param>
//		///// <returns>The result of the rotation.</returns>
//		//public static _Vector RotateBy(_Vector vector, _Quaternion rotation)
//		//{
//		//	if (vector.Dimensions == 3)
//		//		return _Quaternion.Rotate(rotation, vector);
//		//	else
//		//		throw new Exception("my quaternion rotations are only defined for 3-component vectors.");
//		//}

//		/// <summary>Computes the linear interpolation between two vectors.</summary>
//		/// <param name="left">The starting vector of the interpolation.</param>
//		/// <param name="right">The ending vector of the interpolation.</param>
//		/// <param name="blend">The ratio 0.0 to 1.0 of the interpolation between the start and end.</param>
//		/// <returns>The result of the interpolation.</returns>
//		public static _Vector Lerp(_Vector left, _Vector right, float blend)
//		{
//			float[] leftFloats = left.Floats;
//			float[] rightFloats = right.Floats;
//			if (blend < 0 || blend > 1.0f)
//				throw new Exception("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
//			if (leftFloats.Length != rightFloats.Length)
//				throw new Exception("invalid lerp matrix length: (left.Dimensions != right.Dimensions)");
//			_Vector result = new _Vector(leftFloats.Length);
//			float[] resultFloats = result.Floats;
//			for (int i = 0; i < leftFloats.Length; i++)
//				resultFloats[i] = leftFloats[i] + blend * (rightFloats[i] - leftFloats[i]);
//			return result;
//		}

//		/// <summary>Sphereically interpolates between two vectors.</summary>
//		/// <param name="left">The starting vector of the interpolation.</param>
//		/// <param name="right">The ending vector of the interpolation.</param>
//		/// <param name="blend">The ratio 0.0 to 1.0 defining the interpolation distance between the two vectors.</param>
//		/// <returns>The result of the slerp operation.</returns>
//		public static _Vector Slerp(_Vector left, _Vector right, float blend)
//		{
//			if (blend < 0 || blend > 1.0f)
//				throw new Exception("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
//			float dot = Calc.Clamp(_Vector.DotProduct(left, right), -1.0f, 1.0f);
//			float theta = Calc.ArcCos(dot) * blend;
//			return left * Calc.Cos(theta) + (right - left * dot).Normalize() * Calc.Sin(theta);
//		}

//		/// <summary>Interpolates between three vectors using barycentric coordinates.</summary>
//		/// <param name="a">The first vector of the interpolation.</param>
//		/// <param name="b">The second vector of the interpolation.</param>
//		/// <param name="c">The thrid vector of the interpolation.</param>
//		/// <param name="u">The "U" value of the barycentric interpolation equation.</param>
//		/// <param name="v">The "V" value of the barycentric interpolation equation.</param>
//		/// <returns>The resulting vector of the barycentric interpolation.</returns>
//		public static _Vector Blerp(_Vector a, _Vector b, _Vector c, float u, float v)
//		{
//			return _Vector.Add(_Vector.Add(_Vector.Multiply(_Vector.Subtract(b, a), u), _Vector.Multiply(_Vector.Subtract(c, a), v)), a);
//		}

//		/// <summary>Does a value equality check.</summary>
//		/// <param name="left">The first vector to check for equality.</param>
//		/// <param name="right">The second vector  to check for equality.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public static bool EqualsValue(_Vector left, _Vector right)
//		{
//			float[] leftFloats = left.Floats;
//			float[] rightFloats = right.Floats;
//			if (leftFloats.GetLength(0) != rightFloats.GetLength(0))
//				return false;
//			for (int i = 0; i < leftFloats.GetLength(0); i++)
//				if (leftFloats[i] != rightFloats[i])
//					return false;
//			return true;
//		}

//		/// <summary>Does a value equality check with leniency.</summary>
//		/// <param name="left">The first vector to check for equality.</param>
//		/// <param name="right">The second vector to check for equality.</param>
//		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public static bool EqualsValue(_Vector left, _Vector right, float leniency)
//		{
//			float[] leftFloats = left.Floats;
//			float[] rightFloats = right.Floats;
//			if (leftFloats.GetLength(0) != rightFloats.GetLength(0))
//				return false;
//			for (int i = 0; i < leftFloats.GetLength(0); i++)
//				if (Calc.Abs(leftFloats[i] - rightFloats[i]) > leniency)
//					return false;
//			return true;
//		}

//		/// <summary>Checks if two matrices are equal by reverences.</summary>
//		/// <param name="left">The left vector of the equality check.</param>
//		/// <param name="right">The right vector of the equality check.</param>
//		/// <returns>True if the references are equal, false if not.</returns>
//		public static bool EqualsReference(_Vector left, _Vector right)
//		{
//			return object.ReferenceEquals(left, right) ||
//				// also, if they point to the same float array
//				object.ReferenceEquals(left.Floats, right.Floats);
//		}

//		/// <summary>Converts the vector into a matrix.</summary>
//		/// <param name="vector">The vecotr to convert.</param>
//		/// <returns>The matrix of the conversion.</returns>
//		public static Matrix_Flattened ToMatrix(_Vector vector)
//		{
//			return new Matrix_Flattened(vector.Dimensions, 1, vector.Floats);
//		}

//		/// <summary>Prints out a string representation of this matrix.</summary>
//		/// <returns>A string representing this matrix.</returns>
//		public override string ToString()
//		{
//			// Change this method to what ever you want.
//			return base.ToString();
//		}

//		/// <summary>Computes a hash code from the values of this matrix.</summary>
//		/// <returns>A hash code for the matrix.</returns>
//		public override int GetHashCode()
//		{
//			// return base.GetHashCode();
//			int hash = _vector[0].GetHashCode();
//			for (int i = 1; i < _vector.Length; i++)
//				hash ^= _vector[i].GetHashCode();
//			return hash;
//		}

//		/// <summary>Does an equality check by reference.</summary>
//		/// <param name="right">The object to compare to.</param>
//		/// <returns>True if the references are equal, false if not.</returns>
//		public override bool Equals(object right)
//		{
//			if (!(right is _Vector)) return false;
//			return _Vector.EqualsReference(this, (_Vector)right);
//		}

//		private class Exception : Error
//		{
//			public Exception(string message) : base(message) { }
//		}
//	}

//	// This is my old version of vectors. It only supported 3-component vectors.
//	// The newer version supports arbitrary sized  vectors as well as multiplications
//	// with arbitrary sized matrices. Learn your linear algebra!
//	#region Vector-OLD
//	///// <summary>Implements a 3-component (x, y, z) vector matrix.</summary>
//	//public class Vector37
//	//{
//	//  protected float _x, _y, _z;

//	//  public float X { get { return _x; } set { _x = value; } }
//	//  public float Y { get { return _y; } set { _y = value; } }
//	//  public float Z { get { return _z; } set { _z = value; } }

//	//  public Vector37(float x, float y, float z) { _x = x; _y = y; _z = z; }

//	//  public static readonly Vector37 FactoryZero = new Vector37(0, 0, 0);

//	//  public static Vector37 operator +(Vector37 left, Vector37 right) { return Vector37.Add(left, right); }
//	//  public static Vector37 operator -(Vector37 left, Vector37 right) { return Vector37.Subtract(left, right); }
//	//  public static Vector37 operator -(Vector37 vector) { return Vector37.Negate(vector); }
//	//  public static Vector37 operator *(Vector37 left, float right) { return Vector37.Multiply(left, right); }
//	//  public static Vector37 operator *(float left, Vector37 right) { return Vector37.Multiply(right, left); }
//	//  public static Vector37 operator /(Vector37 left, float right) { return Vector37.Divide(left, right); }
//	//  public static bool operator ==(Vector37 left, Vector37 right) { return Vector37.Equals(left, right); }
//	//  public static bool operator !=(Vector37 left, Vector37 right) { return !Vector37.Equals(left, right); }
//	//  public static implicit operator float[,](Vector37 vector) { return Vector37.ToFloats(vector); }
//	//  public static implicit operator Matrix(Vector37 vector) { return Vector37.ToMatrix(vector); }

//	//  public Vector37 Add(Vector37 right) { return Vector37.Add(this, right); }
//	//  public Vector37 Negate() { return Vector37.Negate(this); }
//	//  public Vector37 Subtract(Vector37 right) { return Vector37.Subtract(this, right); }
//	//  public Vector37 Multiply(float right) { return Vector37.Multiply(this, right); }
//	//  public Vector37 Divide(float right) { return Vector37.Divide(this, right); }
//	//  public float DotProduct(Vector37 right) { return Vector37.DotProduct(this, right); }
//	//  public Vector37 CrossProduct(Vector37 right) { return Vector37.CrossProduct(this, right); }
//	//  public Vector37 Normalize() { return Vector37.Normalize(this); }
//	//  public float Length() { return Vector37.Length(this); }
//	//  public float LengthSquared() { return Vector37.LengthSquared(this); }
//	//  public bool Equals(Vector37 right) { return Vector37.Equals(this, right); }
//	//  public bool Equals(Vector37 right, float leniency) { return Vector37.Equals(this, right, leniency); }
//	//  public Vector37 RotateBy(float angle, float x, float y, float z) { return Vector37.RotateBy(this, angle, x, y, z); }
//	//  public Vector37 Lerp(Vector37 right, float blend) { return Vector37.InterpolateLinear(this, right, blend); }
//	//  public Vector37 Slerp(Vector37 right, float blend) { return Vector37.InterpolateSphereical(this, right, blend); }
//	//  public Vector37 RotateBy(Quaternion rotation) { return Vector37.RotateBy(this, rotation); }

//	//  public static Vector37 Add(Vector37 left, Vector37 right)
//	//  {
//	//    return new Vector37(
//	//      left.X + right.X,
//	//      left.Y + right.Y,
//	//      left.Z + right.Z);
//	//  }

//	//  public static Vector37 Negate(Vector37 vector)
//	//  {
//	//    return new Vector37(
//	//      -vector.X,
//	//      -vector.Y,
//	//      -vector.Z);
//	//  }

//	//  public static Vector37 Subtract(Vector37 left, Vector37 right)
//	//  {
//	//    return new Vector37(
//	//      left.X - right.X,
//	//      left.Y - right.Y,
//	//      left.Z - right.Z);
//	//  }

//	//  public static Vector37 Multiply(Vector37 left, float right)
//	//  {
//	//    return new Vector37(
//	//      left.X * right,
//	//      left.Y * right,
//	//      left.Z * right);
//	//  }

//	//  public static Vector37 Divide(Vector37 left, float right)
//	//  {
//	//    return new Vector37(
//	//      left.X / right,
//	//      left.Y / right,
//	//      left.Z / right);
//	//  }

//	//  public static float DotProduct(Vector37 left, Vector37 right)
//	//  {
//	//    return
//	//      left.X * right.X +
//	//      left.Y * right.Y +
//	//      left.Z * right.Z;
//	//  }

//	//  public static Vector37 CrossProduct(Vector37 left, Vector37 right)
//	//  {
//	//    return new Vector37(
//	//      left.Y * right.Z - left.Z * right.Y,
//	//      left.Z * right.X - left.X * right.Z,
//	//      left.X * right.Y - left.Y * right.X);
//	//  }

//	//  public static Vector37 Normalize(Vector37 vector)
//	//  {
//	//    float length = vector.Length();
//	//    if (length != 0.0)
//	//      return new Vector37(
//	//        vector.X / length,
//	//        vector.Y / length,
//	//        vector.Z / length);
//	//    else
//	//      return Vector37.FactoryZero;
//	//  }

//	//  public static float Length(Vector37 vector)
//	//  {
//	//    return Calc.SquareRoot(
//	//      vector.X * vector.X +
//	//      vector.Y * vector.Y +
//	//      vector.Z * vector.Z);
//	//  }

//	//  public static float LengthSquared(Vector37 vector)
//	//  {
//	//    return
//	//      vector.X * vector.X +
//	//      vector.Y * vector.Y +
//	//      vector.Z * vector.Z;
//	//  }

//	//  public static bool Equals(Vector37 left, Vector37 right)
//	//  {
//	//    if (object.ReferenceEquals(left, right))
//	//      return true;
//	//    else if (object.ReferenceEquals(left, null) || object.ReferenceEquals(null, right))
//	//      return false;
//	//    else return
//	//      left.X == right.X && 
//	//      left.Y == right.Y &&
//	//      left.Z == right.Z;
//	//  }

//	//  public static bool Equals(Vector37 left, Vector37 right, float leniency)
//	//  {
//	//    if (object.ReferenceEquals(left, right))
//	//      return true;
//	//    else if (object.ReferenceEquals(left, null) || object.ReferenceEquals(null, right))
//	//      return false;
//	//    else return
//	//      Calc.Abs(left.X - right.X) < leniency &&
//	//      Calc.Abs(left.Y - right.Y) < leniency &&
//	//      Calc.Abs(left.Z - right.Z) < leniency;
//	//  }

//	//  public static Vector37 DirectionTowardsPosition(Vector37 from, Vector37 to)
//	//  {
//	//    return (to - from).Normalize();
//	//  }

//	//  public static Vector37 MoveTowardsPosition(Vector37 position, Vector37 goal, float distance)
//	//  {
//	//    Vector37 direction = DirectionTowardsPosition(position, goal);
//	//    return new Vector37(
//	//      position.X + direction.X * distance,
//	//      position.Y + direction.Y * distance,
//	//      position.Z + direction.Z * distance);
//	//  }

//	//  public static Vector37 MoveTowardsDirection(Vector37 position, Vector37 direction, float distance)
//	//  {
//	//    direction = direction.Normalize();
//	//    return new Vector37(
//	//      position.X + direction.X * distance,
//	//      position.Y + direction.Y * distance,
//	//      position.Z + direction.Z * distance);
//	//  }

//	//  public static float AngleBetween(Vector37 first, Vector37 second)
//	//  {
//	//    return Calc.ArcCos(Vector37.DotProduct(first, second) / (first.Length() * second.Length()));
//	//  }

//	//  public static Vector37 RotateBy(Vector37 vector, float angle, float x, float y, float z)
//	//  {
//	//    // Note: the angle is in radians
//	//    float sinHalfAngle = Calc.Sin(angle / 2);
//	//    float cosHalfAngle = Calc.Cos(angle / 2);
//	//    x *= sinHalfAngle;
//	//    y *= sinHalfAngle;
//	//    z *= sinHalfAngle;
//	//    float x2 = cosHalfAngle * vector.X + y * vector.Z - z * vector.Y;
//	//    float y2 = cosHalfAngle * vector.Y + z * vector.X - x * vector.Z;
//	//    float z2 = cosHalfAngle * vector.Z + x * vector.Y - y * vector.X;
//	//    float w2 = -x * vector.X - y * vector.Y - z * vector.Z;
//	//    return new Vector37(
//	//      x * w2 + cosHalfAngle * x2 + y * z2 - z * y2,
//	//      y * w2 + cosHalfAngle * y2 + z * x2 - x * z2,
//	//      z * w2 + cosHalfAngle * z2 + x * y2 - y * x2);
//	//  }

//	//  public static Vector37 RotateBy(Vector37 vector, Quaternion rotation)
//	//  {
//	//    Quaternion answer = (rotation * vector) * Quaternion.Conjugate(rotation);
//	//    return new Vector37(answer.X, answer.Y, answer.Z);
//	//  }

//	//  public static Vector37 InterpolateLinear(Vector37 a, Vector37 b, float blend)
//	//  {
//	//    if (blend < 0 || blend > 1.0f)
//	//      throw new VectorException("invalid lerp blend value: (blend < 0.0f || blend > 1.0f).");
//	//    return new Vector37(
//	//      a.X + blend * (b.X - a.X),
//	//      a.Y + blend * (b.Y - a.Y),
//	//      a.Z + blend * (b.Z - a.Z));
//	//  }

//	//  public static Vector37 InterpolateSphereical(Vector37 a, Vector37 b, float blend)
//	//  {
//	//    if (blend < 0 || blend > 1.0f)
//	//      throw new VectorException("invalid slerp blend value: (blend < 0.0f || blend > 1.0f).");
//	//    float dot = Calc.Clamp(Vector37.DotProduct(a, b), -1.0f, 1.0f);
//	//    float theta = Calc.ArcCos(dot) * blend;
//	//    return a * Calc.Cos(theta) + (b - a * dot).Normalize() * Calc.Sin(theta);
//	//  }

//	//  public static Vector37 InterpolateBarycentric(Vector37 a, Vector37 b, Vector37 c, float u, float v)
//	//  {
//	//    return a + u * (b - a) + v * (c - a);
//	//  }

//	//  public static float[,] ToFloats(Vector37 vector)
//	//  {
//	//    return new float[,] { { vector.X }, { vector.Y }, { vector.Z } };
//	//  }

//	//  public static Matrix ToMatrix(Vector37 vector)
//	//  {
//	//    return new Matrix(3, 1, new float[] { vector.X, vector.Y, vector.Z });
//	//    //Matrix matrix = new Matrix(vector.X, vector.Y, vector.Z);
//	//    //matrix[0, 0] = vector.X;
//	//    //matrix[1, 0] = vector.Y;
//	//    //matrix[2, 0] = vector.Z;
//	//    //return matrix;
//	//  }

//	//  public override string ToString()
//	//  {
//	//    return base.ToString();
//	//    //return
//	//    //  X.ToString() + "\n" + 
//	//    //  Y.ToString() + "\n" +
//	//    //  Z.ToString() + "\n";
//	//  }

//	//  public override int GetHashCode()
//	//  {
//	//    return
//	//      X.GetHashCode() ^
//	//      Y.GetHashCode() ^
//	//      Z.GetHashCode();
//	//  }

//	//  public override bool Equals(object obj)
//	//  {
//	//    return base.Equals(obj);
//	//  }

//	//  private class VectorException : Exception
//	//  {
//	//    public VectorException(string message) : base(message) { }
//	//  }
//	//}
//	#endregion
//}

#endregion

#region Matrix

//namespace Seven.Mathematics
//{
//	public interface Matrix<T>
//	{
//		/// <summary>The number of rows in the matrix.</summary>
//		int Rows { get; }
//		/// <summary>The number of columns in the matrix.</summary>
//		int Columns { get; }
//		/// <summary>Determines if the matrix is square.</summary>
//		bool IsSquare { get; }
//		/// <summary>Determines if the matrix is a vector.</summary>
//		bool IsVector { get; }
//		/// <summary>Determines if the matrix is a 2 component vector.</summary>
//		bool Is2x1 { get; }
//		/// <summary>Determines if the matrix is a 3 component vector.</summary>
//		bool Is3x1 { get; }
//		/// <summary>Determines if the matrix is a 4 component vector.</summary>
//		bool Is4x1 { get; }
//		/// <summary>Determines if the matrix is a 2 square matrix.</summary>
//		bool Is2x2 { get; }
//		/// <summary>Determines if the matrix is a 3 square matrix.</summary>
//		bool Is3x3 { get; }
//		/// <summary>Determines if the matrix is a 4 square matrix.</summary>
//		bool Is4x4 { get; }

//		/// <summary>Standard row-major matrix indexing.</summary>
//		/// <param name="row">The row index.</param>
//		/// <param name="column">The column index.</param>
//		/// <returns>The value at the given indeces.</returns>
//		T this[int row, int column] { get; set; }

//		/// <summary>Negates all the values in this matrix.</summary>
//		/// <returns>The resulting matrix after the negations.</returns>
//		Matrix<T> Negate();
//		/// <summary>Does a standard matrix addition.</summary>
//		/// <param name="right">The matrix to add to this matrix.</param>
//		/// <returns>The resulting matrix after the addition.</returns>
//		Matrix<T> Add(Matrix<T> right);
//		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
//		/// <param name="right">The matrix to multiply this matrix by.</param>
//		/// <returns>The resulting matrix after the multiplication.</returns>
//		Matrix<T> Multiply(Matrix<T> right);
//		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
//		/// <param name="right">The scalar to multiply all the matrix values by.</param>
//		/// <returns>The retulting matrix after the multiplications.</returns>
//		Matrix<T> Multiply(float right);
//		/// <summary>Divides all the values in this matrix by a scalar.</summary>
//		/// <param name="right">The scalar to divide the matrix values by.</param>
//		/// <returns>The resulting matrix after teh divisions.</returns>
//		Matrix<T> Divide(float right);
//		/// <summary>Gets the minor of a matrix.</summary>
//		/// <param name="row">The restricted row of the minor.</param>
//		/// <param name="column">The restricted column of the minor.</param>
//		/// <returns>The minor from the row/column restrictions.</returns>
//		Matrix<T> Minor(int row, int column);
//		/// <summary>Combines two matrices from left to right 
//		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
//		/// <param name="right">The matrix to combine with on the right side.</param>
//		/// <returns>The resulting row-wise concatination.</returns>
//		Matrix<T> ConcatenateRowWise(Matrix<T> right);
//		/// <summary>Computes the determinent if this matrix is square.</summary>
//		/// <returns>The computed determinent if this matrix is square.</returns>
//		T Determinent();
//		/// <summary>Computes the echelon form of this matrix (aka REF).</summary>
//		/// <returns>The computed echelon form of this matrix (aka REF).</returns>
//		Matrix<T> Echelon();
//		/// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
//		/// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
//		Matrix<T> ReducedEchelon();
//		/// <summary>Computes the inverse of this matrix.</summary>
//		/// <returns>The inverse of this matrix.</returns>
//		Matrix<T> Inverse();
//		/// <summary>Gets the adjoint of this matrix.</summary>
//		/// <returns>The adjoint of this matrix.</returns>
//		Matrix<T> Adjoint();
//		/// <summary>Transposes this matrix.</summary>
//		/// <returns>The transpose of this matrix.</returns>
//		Matrix<T> Transpose();
//		/// <summary>Copies this matrix.</summary>
//		/// <returns>The copy of this matrix.</returns>
//		Matrix<T> Clone();
//	}

//	/// <summary>A matrix implemented as a flattened float array to perform matrix theory in row major order. Enjoy :)</summary>
//	public class Matrix_Flattened
//	{
//		private float[] _matrix;
//		private int _columns;
//		private int _rows;

//		/// <summary>The float[] reference of this matrix.</summary>
//		public float[] Floats
//		{
//			get { return _matrix; }
//			set
//			{
//				if (value.Length == _rows * _columns) _matrix = value;
//				else throw new MatrixException("you cannot change the dimension of matrix when setting its float values");
//			}
//		}
//		/// <summary>The number of rows in the matrix.</summary>
//		public int Rows { get { return _rows; } }
//		/// <summary>The number of columns in the matrix.</summary>
//		public int Columns { get { return _columns; } }
//		/// <summary>The number of elements in the matrix (rows * columns).</summary>
//		public int Size { get { return _matrix.Length; } }
//		/// <summary>Determines if the matrix is square.</summary>
//		public bool IsSquare { get { return _rows == _columns; } }
//		/// <summary>Determines if the matrix is a vector.</summary>
//		public bool IsVector { get { return _columns == 1; } }
//		/// <summary>Determines if the matrix is a 2 component vector.</summary>
//		public bool Is2x1 { get { return _rows == 2 && _columns == 1; } }
//		/// <summary>Determines if the matrix is a 3 component vector.</summary>
//		public bool Is3x1 { get { return _rows == 3 && _columns == 1; } }
//		/// <summary>Determines if the matrix is a 4 component vector.</summary>
//		public bool Is4x1 { get { return _rows == 4 && _columns == 1; } }
//		/// <summary>Determines if the matrix is a 2 square matrix.</summary>
//		public bool Is2x2 { get { return _rows == 2 && _columns == 2; } }
//		/// <summary>Determines if the matrix is a 3 square matrix.</summary>
//		public bool Is3x3 { get { return _rows == 3 && _columns == 3; } }
//		/// <summary>Determines if the matrix is a 4 square matrix.</summary>
//		public bool Is4x4 { get { return _rows == 4 && _columns == 4; } }

//		/// <summary>Standard row-major matrix indexing.</summary>
//		/// <param name="row">The row index.</param>
//		/// <param name="column">The column index.</param>
//		/// <returns>The value at the given indeces.</returns>
//		public float this[int row, int column]
//		{
//			get
//			{
//				if (row > _rows - 1 || column > _columns - 1)
//					throw new MatrixException("index out of bounds.");
//				return _matrix[row * _columns + column];
//			}
//			set
//			{
//				if (row > _rows - 1 || column > _columns - 1)
//					throw new MatrixException("index out of bounds.");
//				else _matrix[row * _columns + column] = value;
//			}
//		}

//		/// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
//		/// <param name="rows">The number of row dimensions.</param>
//		/// <param name="columns">The number of column dimensions.</param>
//		public Matrix_Flattened(int rows, int columns)
//		{
//			_rows = rows;
//			_columns = columns;
//			try { _matrix = new float[rows * columns]; }
//			catch { throw new MatrixException("invalid dimensions."); }
//		}

//		/// <summary>Wraps a float[] inside of a matrix class.</summary>
//		/// <param name="matrix">The float[] to wrap in a matrix class.</param>
//		public Matrix_Flattened(int rows, int columns, params float[] matrix)
//		{
//			float[] floats = new float[matrix.Length];
//			Buffer.BlockCopy(matrix, 0, floats, 0, floats.Length * sizeof(float));
//			_matrix = matrix;
//			_columns = columns;
//			_rows = rows;
//		}

//		/// <summary>This is a special constructor to make a vector into a matrix
//		/// without copying the data for efficiency purposes.</summary>
//		/// <param name="vector">The values the new matrix will point to.</param>
//		private Matrix_Flattened(Vector vector)
//		{
//			_matrix = vector.Floats;
//			_rows = _matrix.Length;
//			_columns = 1;
//		}

//		/// <summary>Constructs a matrix that points to the values in a vector. So the vector and this
//		/// new matrix point to the same float[].</summary>
//		/// <param name="vector">The vector who will share the data as the constructed matrix.</param>
//		/// <returns>The constructed matrix sharing the data with the vector.</returns>
//		public static Matrix_Flattened UnsafeFactoryFromVector(Vector vector) { return new Matrix_Flattened(vector); }

//		/// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <returns>The newly constructed zero-matrix.</returns>
//		public static Matrix_Flattened FactoryZero(int rows, int columns)
//		{
//			try { return new Matrix_Flattened(rows, columns); }
//			catch { throw new MatrixException("invalid dimensions."); }
//		}

//		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <returns>The newly constructed identity-matrix.</returns>
//		public static Matrix_Flattened FactoryIdentity(int rows, int columns)
//		{
//			Matrix_Flattened matrix;
//			try { matrix = new Matrix_Flattened(rows, columns); }
//			catch { throw new MatrixException("invalid dimensions."); }
//			if (rows <= columns)
//				for (int i = 0; i < rows; i++)
//					matrix[i, i] = 1;
//			else
//				for (int i = 0; i < columns; i++)
//					matrix[i, i] = 1;
//			return matrix;
//		}

//		/// <summary>Constructs a new matrix where every entry is 1.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <returns>The newly constructed matrix filled with 1's.</returns>
//		public static Matrix_Flattened FactoryOne(int rows, int columns)
//		{
//			Matrix_Flattened matrix;
//			try { matrix = new Matrix_Flattened(rows, columns); }
//			catch { throw new MatrixException("invalid dimensions."); }
//			for (int i = 0; i < rows; i++)
//				for (int j = 0; j < columns; j++)
//					matrix[i, j] = 1;
//			return matrix;
//		}

//		/// <summary>Constructs a new matrix where every entry is the same uniform value.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <param name="uniform">The value to assign every spot in the matrix.</param>
//		/// <returns>The newly constructed matrix filled with the uniform value.</returns>
//		public static Matrix_Flattened FactoryUniform(int rows, int columns, float uniform)
//		{
//			Matrix_Flattened matrix;
//			try { matrix = new Matrix_Flattened(rows, columns); }
//			catch { throw new MatrixException("invalid dimensions."); }
//			for (int i = 0; i < rows; i++)
//				for (int j = 0; j < columns; j++)
//					matrix[i, j] = uniform;
//			return matrix;
//		}

//		/// <summary>Constructs a 2-component vector matrix with all values being 0.</summary>
//		/// <returns>The constructed 2-component vector matrix.</returns>
//		public static Matrix_Flattened Factory2x1() { return new Matrix_Flattened(2, 1); }
//		/// <summary>Constructs a 3-component vector matrix with all values being 0.</summary>
//		/// <returns>The constructed 3-component vector matrix.</returns>
//		public static Matrix_Flattened Factory3x1() { return new Matrix_Flattened(3, 1); }
//		/// <summary>Constructs a 4-component vector matrix with all values being 0.</summary>
//		/// <returns>The constructed 4-component vector matrix.</returns>
//		public static Matrix_Flattened Factory4x1() { return new Matrix_Flattened(4, 1); }

//		/// <summary>Constructs a 2x2 matrix with all values being 0.</summary>
//		/// <returns>The constructed 2x2 matrix.</returns>
//		public static Matrix_Flattened Factory2x2() { return new Matrix_Flattened(2, 2); }
//		/// <summary>Constructs a 3x3 matrix with all values being 0.</summary>
//		/// <returns>The constructed 3x3 matrix.</returns>
//		public static Matrix_Flattened Factory3x3() { return new Matrix_Flattened(3, 3); }
//		/// <summary>Constructs a 4x4 matrix with all values being 0.</summary>
//		/// <returns>The constructed 4x4 matrix.</returns>
//		public static Matrix_Flattened Factory4x4() { return new Matrix_Flattened(4, 4); }

//		/// <param name="angle">Angle of rotation in radians.</param>
//		public static Matrix_Flattened Factory3x3RotationX(float angle)
//		{
//			float cos = Calc.Cos(angle);
//			float sin = Calc.Sin(angle);
//			return new Matrix_Flattened(3, 3, new float[] { 1, 0, 0, 0, cos, sin, 0, -sin, cos });
//		}

//		/// <param name="angle">Angle of rotation in radians.</param>
//		public static Matrix_Flattened Factory3x3RotationY(float angle)
//		{
//			float cos = Calc.Cos(angle);
//			float sin = Calc.Sin(angle);
//			return new Matrix_Flattened(3, 3, new float[] { cos, 0, -sin, 0, 1, 0, sin, 0, cos });
//		}

//		/// <param name="angle">Angle of rotation in radians.</param>
//		public static Matrix_Flattened Factory3x3RotationZ(float angle)
//		{
//			float cos = Calc.Cos(angle);
//			float sin = Calc.Sin(angle);
//			return new Matrix_Flattened(3, 3, new float[] { cos, -sin, 0, sin, cos, 0, 0, 0, 1 });
//		}

//		/// <param name="angleX">Angle about the X-axis in radians.</param>
//		/// <param name="angleY">Angle about the Y-axis in radians.</param>
//		/// <param name="angleZ">Angle about the Z-axis in radians.</param>
//		public static Matrix_Flattened Factory3x3RotationXthenYthenZ(float angleX, float angleY, float angleZ)
//		{
//			float
//				xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
//				yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
//				zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
//			return new Matrix_Flattened(3, 3,
//				new float[] {
//					yCos * zCos, -yCos * zSin, ySin,
//					xCos * zSin + xSin * ySin * zCos, xCos * zCos + xSin * ySin * zSin, -xSin * yCos,
//					xSin * zSin - xCos * ySin * zCos, xSin * zCos + xCos * ySin * zSin, xCos * yCos });
//		}

//		/// <param name="angleX">Angle about the X-axis in radians.</param>
//		/// <param name="angleY">Angle about the Y-axis in radians.</param>
//		/// <param name="angleZ">Angle about the Z-axis in radians.</param>
//		public static Matrix_Flattened Factory3x3RotationZthenYthenX(float angleX, float angleY, float angleZ)
//		{
//			float
//				xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
//				yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
//				zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
//			return new Matrix_Flattened(3, 3, new float[] { yCos * zCos, zCos * xSin * ySin - xCos * zSin, xCos * zCos * ySin + xSin * zSin,
//				yCos * zSin, xCos * zCos + xSin * ySin * zSin, -zCos * xSin + xCos * ySin * zSin, -ySin, yCos * xSin, xCos * yCos });
//		}

//		/// <summary>Creates a 3x3 matrix initialized with a shearing transformation.</summary>
//		/// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
//		/// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
//		/// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
//		/// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
//		/// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
//		/// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
//		/// <returns>The constructed shearing matrix.</returns>
//		public static Matrix_Flattened Factory3x3Shear(
//			float shearXbyY, float shearXbyZ, float shearYbyX,
//			float shearYbyZ, float shearZbyX, float shearZbyY)
//		{
//			return new Matrix_Flattened(3, 3, new float[] { 1, shearYbyX, shearZbyX, shearXbyY, 1, shearYbyZ, shearXbyZ, shearYbyZ, 1 });
//		}

//		/// <summary>Negates all the values in a matrix.</summary>
//		/// <param name="matrix">The matrix to have its values negated.</param>
//		/// <returns>The resulting matrix after the negations.</returns>
//		public static Matrix_Flattened operator -(Matrix_Flattened matrix) { return Matrix_Flattened.Negate(matrix); }
//		/// <summary>Does a standard matrix addition.</summary>
//		/// <param name="left">The left matrix of the addition.</param>
//		/// <param name="right">The right matrix of the addition.</param>
//		/// <returns>The resulting matrix after teh addition.</returns>
//		public static Matrix_Flattened operator +(Matrix_Flattened left, Matrix_Flattened right) { return Matrix_Flattened.Add(left, right); }
//		/// <summary>Does a standard matrix subtraction.</summary>
//		/// <param name="left">The left matrix of the subtraction.</param>
//		/// <param name="right">The right matrix of the subtraction.</param>
//		/// <returns>The result of the matrix subtraction.</returns>
//		public static Matrix_Flattened operator -(Matrix_Flattened left, Matrix_Flattened right) { return Matrix_Flattened.Subtract(left, right); }
//		/// <summary>Does a standard matrix multiplication.</summary>
//		/// <param name="left">The left matrix of the multiplication.</param>
//		/// <param name="right">The right matrix of the multiplication.</param>
//		/// <returns>The resulting matrix after the multiplication.</returns>
//		public static Matrix_Flattened operator *(Matrix_Flattened left, Matrix_Flattened right) { return Matrix_Flattened.Multiply(left, right); }
//		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
//		/// <param name="left">The matrix to have its values multiplied.</param>
//		/// <param name="right">The scalar to multiply the values by.</param>
//		/// <returns>The resulting matrix after the multiplications.</returns>
//		public static Matrix_Flattened operator *(Matrix_Flattened left, float right) { return Matrix_Flattened.Multiply(left, right); }
//		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
//		/// <param name="left">The scalar to multiply the values by.</param>
//		/// <param name="right">The matrix to have its values multiplied.</param>
//		/// <returns>The resulting matrix after the multiplications.</returns>
//		public static Matrix_Flattened operator *(float left, Matrix_Flattened right) { return Matrix_Flattened.Multiply(right, left); }
//		/// <summary>Divides all the values in a matrix by a scalar.</summary>
//		/// <param name="left">The matrix to have its values divided.</param>
//		/// <param name="right">The scalar to divide the values by.</param>
//		/// <returns>The resulting matrix after the divisions.</returns>
//		public static Matrix_Flattened operator /(Matrix_Flattened left, float right) { return Matrix_Flattened.Divide(left, right); }
//		/// <summary>Applies a power to a matrix.</summary>
//		/// <param name="left">The matrix to apply a power to.</param>
//		/// <param name="right">The power to apply to the matrix.</param>
//		/// <returns>The result of the power operation.</returns>
//		public static Matrix_Flattened operator ^(Matrix_Flattened left, int right) { return Matrix_Flattened.Power(left, right); }
//		/// <summary>Checks for equality by value.</summary>
//		/// <param name="left">The left matrix of the equality check.</param>
//		/// <param name="right">The right matrix of the equality check.</param>
//		/// <returns>True if the values of the matrices are equal, false if not.</returns>
//		public static bool operator ==(Matrix_Flattened left, Matrix_Flattened right) { return Matrix_Flattened.EqualsByValue(left, right); }
//		/// <summary>Checks for false-equality by value.</summary>
//		/// <param name="left">The left matrix of the false-equality check.</param>
//		/// <param name="right">The right matrix of the false-equality check.</param>
//		/// <returns>True if the values of the matrices are not equal, false if they are.</returns>
//		public static bool operator !=(Matrix_Flattened left, Matrix_Flattened right) { return !Matrix_Flattened.EqualsByValue(left, right); }
//		/// <summary>Automatically converts a matrix into a float[,] if necessary.</summary>
//		/// <param name="matrix">The matrix to convert to a float[,].</param>
//		/// <returns>The reference to the float[,] representing the matrix.</returns>
//		//public static implicit operator float[](Matrix matrix) { return matrix.Floats; }

//		/// <summary>Negates all the values in this matrix.</summary>
//		/// <returns>The resulting matrix after the negations.</returns>
//		private Matrix_Flattened Negate() { return Matrix_Flattened.Negate(this); }
//		/// <summary>Does a standard matrix addition.</summary>
//		/// <param name="right">The matrix to add to this matrix.</param>
//		/// <returns>The resulting matrix after the addition.</returns>
//		private Matrix_Flattened Add(Matrix_Flattened right) { return Matrix_Flattened.Add(this, right); }
//		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
//		/// <param name="right">The matrix to multiply this matrix by.</param>
//		/// <returns>The resulting matrix after the multiplication.</returns>
//		private Matrix_Flattened Multiply(Matrix_Flattened right) { return Matrix_Flattened.Multiply(this, right); }
//		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
//		/// <param name="right">The scalar to multiply all the matrix values by.</param>
//		/// <returns>The retulting matrix after the multiplications.</returns>
//		private Matrix_Flattened Multiply(float right) { return Matrix_Flattened.Multiply(this, right); }
//		/// <summary>Divides all the values in this matrix by a scalar.</summary>
//		/// <param name="right">The scalar to divide the matrix values by.</param>
//		/// <returns>The resulting matrix after teh divisions.</returns>
//		private Matrix_Flattened Divide(float right) { return Matrix_Flattened.Divide(this, right); }
//		/// <summary>Gets the minor of a matrix.</summary>
//		/// <param name="row">The restricted row of the minor.</param>
//		/// <param name="column">The restricted column of the minor.</param>
//		/// <returns>The minor from the row/column restrictions.</returns>
//		public Matrix_Flattened Minor(int row, int column) { return Matrix_Flattened.Minor(this, row, column); }
//		/// <summary>Combines two matrices from left to right (result.Columns == left.Columns + right.Columns).</summary>
//		/// <param name="right">The matrix to combine with on the right side.</param>
//		/// <returns>The resulting row-wise concatination.</returns>
//		public Matrix_Flattened ConcatenateRowWise(Matrix_Flattened right) { return Matrix_Flattened.ConcatenateRowWise(this, right); }
//		/// <summary>Computes the determinent if this matrix is square.</summary>
//		/// <returns>The computed determinent if this matrix is square.</returns>
//		public float Determinent() { return Matrix_Flattened.Determinent(this); }
//		/// <summary>Computes the echelon form of this matrix (aka REF).</summary>
//		/// <returns>The computed echelon form of this matrix (aka REF).</returns>
//		public Matrix_Flattened Echelon() { return Matrix_Flattened.Echelon(this); }
//		/// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
//		/// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
//		public Matrix_Flattened ReducedEchelon() { return Matrix_Flattened.ReducedEchelon(this); }
//		/// <summary>Computes the inverse of this matrix.</summary>
//		/// <returns>The inverse of this matrix.</returns>
//		public Matrix_Flattened Inverse() { return Matrix_Flattened.Inverse(this); }
//		/// <summary>Gets the adjoint of this matrix.</summary>
//		/// <returns>The adjoint of this matrix.</returns>
//		public Matrix_Flattened Adjoint() { return Matrix_Flattened.Adjoint(this); }
//		/// <summary>Transposes this matrix.</summary>
//		/// <returns>The transpose of this matrix.</returns>
//		public Matrix_Flattened Transpose() { return Matrix_Flattened.Transpose(this); }
//		/// <summary>Copies this matrix.</summary>
//		/// <returns>A copy of this matrix.</returns>
//		public Matrix_Flattened Clone() { return Matrix_Flattened.Clone(this); }

//		/// <summary>Negates all the values in a matrix.</summary>
//		/// <param name="matrix">The matrix to have its values negated.</param>
//		/// <returns>The resulting matrix after the negations.</returns>
//		public static Matrix_Flattened Negate(Matrix_Flattened matrix)
//		{
//			Matrix_Flattened result = new Matrix_Flattened(matrix.Rows, matrix.Columns, matrix.Floats);
//			float[] resultFloats = result.Floats;
//			float[] matrixFloats = matrix.Floats;
//			int length = resultFloats.Length;
//			for (int i = 0; i < length; i++)
//				resultFloats[i] = -matrixFloats[i];
//			return result;
//		}

//		/// <summary>Does standard addition of two matrices.</summary>
//		/// <param name="left">The left matrix of the addition.</param>
//		/// <param name="right">The right matrix of the addition.</param>
//		/// <returns>The resulting matrix after the addition.</returns>
//		public static Matrix_Flattened Add(Matrix_Flattened left, Matrix_Flattened right)
//		{
//			if (left.Rows != right.Rows || left.Columns != right.Columns)
//				throw new MatrixException("invalid addition (size miss-match).");
//			Matrix_Flattened result = new Matrix_Flattened(left.Rows, left.Columns);
//			float[]
//				resultFloats = result.Floats,
//				leftFloats = left.Floats,
//				rightFloats = right.Floats;
//			int length = resultFloats.Length;
//			for (int i = 0; i < length; i++)
//				resultFloats[i] = leftFloats[i] + rightFloats[i];
//			return result;
//		}

//		/// <summary>Subtracts a scalar from all the values in a matrix.</summary>
//		/// <param name="left">The matrix to have the values subtracted from.</param>
//		/// <param name="right">The scalar to subtract from all the matrix values.</param>
//		/// <returns>The resulting matrix after the subtractions.</returns>
//		public static Matrix_Flattened Subtract(Matrix_Flattened left, Matrix_Flattened right)
//		{
//			if (left.Rows != right.Rows || left.Columns != right.Columns)
//				throw new MatrixException("invalid subtraction (size miss-match).");
//			Matrix_Flattened result = new Matrix_Flattened(left.Rows, left.Columns);
//			float[] resultFloats = result.Floats,
//				leftFloats = left.Floats,
//				rightFloats = right.Floats;
//			int length = resultFloats.Length;
//			for (int i = 0; i < length; i++)
//				resultFloats[i] = leftFloats[i] - rightFloats[i];
//			return result;
//		}

//		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
//		/// <param name="left">The left matrix of the multiplication.</param>
//		/// <param name="right">The right matrix of the multiplication.</param>
//		/// <returns>The resulting matrix of the multiplication.</returns>
//		public static Matrix_Flattened Multiply(Matrix_Flattened left, Matrix_Flattened right)
//		{
//			float[] leftFloats = left.Floats, rightFloats = right.Floats, resultFloats;
//			int leftRows = left.Rows, leftCols = left.Columns, rightRows = right.Rows, rightCols = right.Columns;
//			Matrix_Flattened result = new Matrix_Flattened(leftRows, rightCols);
//			resultFloats = result.Floats;
//			#region Optimizations
//			if (leftRows < 5)
//			{
//				if (leftRows == 4 && leftCols == 4 && rightRows == 4 && rightCols == 4)
//				{
//					resultFloats = new float[16];
//					float
//						l11 = leftFloats[0], l12 = leftFloats[1], l13 = leftFloats[2], l14 = leftFloats[3],
//						l21 = leftFloats[4], l22 = leftFloats[5], l23 = leftFloats[6], l24 = leftFloats[7],
//						l31 = leftFloats[8], l32 = leftFloats[9], l33 = leftFloats[10], l34 = leftFloats[11],
//						l41 = leftFloats[12], l42 = leftFloats[13], l43 = leftFloats[14], l44 = leftFloats[15],
//						r11 = rightFloats[0], r12 = rightFloats[1], r13 = rightFloats[2], r14 = rightFloats[3],
//						r21 = rightFloats[4], r22 = rightFloats[5], r23 = rightFloats[6], r24 = rightFloats[7],
//						r31 = rightFloats[8], r32 = rightFloats[9], r33 = rightFloats[10], r34 = rightFloats[11],
//						r41 = rightFloats[12], r42 = rightFloats[13], r43 = rightFloats[14], r44 = rightFloats[15];
//					resultFloats[0] = l11 * r11 + l12 * r21 + l13 * r31 + l14 * r41;
//					resultFloats[1] = l11 * r12 + l12 * r22 + l13 * r32 + l14 * r42;
//					resultFloats[2] = l11 * r13 + l12 * r23 + l13 * r33 + l14 * r43;
//					resultFloats[3] = l11 * r14 + l12 * r24 + l13 * r34 + l14 * r44;
//					resultFloats[4] = l21 * r11 + l22 * r21 + l23 * r31 + l24 * r41;
//					resultFloats[5] = l21 * r12 + l22 * r22 + l23 * r32 + l24 * r42;
//					resultFloats[6] = l21 * r13 + l22 * r23 + l23 * r33 + l24 * r43;
//					resultFloats[7] = l21 * r14 + l22 * r24 + l23 * r34 + l24 * r44;
//					resultFloats[8] = l31 * r11 + l32 * r21 + l33 * r31 + l34 * r41;
//					resultFloats[9] = l31 * r12 + l32 * r22 + l33 * r32 + l34 * r42;
//					resultFloats[10] = l31 * r13 + l32 * r23 + l33 * r33 + l34 * r43;
//					resultFloats[11] = l31 * r14 + l32 * r24 + l33 * r34 + l34 * r44;
//					resultFloats[12] = l41 * r11 + l42 * r21 + l43 * r31 + l44 * r41;
//					resultFloats[13] = l41 * r12 + l42 * r22 + l43 * r32 + l44 * r42;
//					resultFloats[14] = l41 * r13 + l42 * r23 + l43 * r33 + l44 * r43;
//					resultFloats[15] = l41 * r14 + l42 * r24 + l43 * r34 + l44 * r44;
//					return result;
//				}
//			}
//			#endregion
//			if (leftCols != right.Rows)
//				throw new MatrixException("invalid multiplication (size miss-match).");
//			resultFloats = new float[leftRows * rightCols];
//			for (int i = 0; i < leftRows; i++)
//				for (int j = 0; j < rightCols; j++)
//					for (int k = 0; k < leftCols; k++)
//						resultFloats[i * rightCols + j] += leftFloats[i * leftCols + k] * rightFloats[k * rightCols + j];
//			return result;
//		}

//		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
//		/// <param name="left">The matrix to have the values multiplied.</param>
//		/// <param name="right">The scalar to multiply the values by.</param>
//		/// <returns>The resulting matrix after the multiplications.</returns>
//		public static Matrix_Flattened Multiply(Matrix_Flattened left, float right)
//		{
//			Matrix_Flattened result = new Matrix_Flattened(left.Rows, left.Columns);
//			float[] resultFloats = result.Floats;
//			float[] leftFloats = left.Floats;
//			for (int i = 0; i < resultFloats.Length; i++)
//				resultFloats[i] = leftFloats[i] * right;
//			return result;
//		}

//		/// <summary>Applies a power to a square matrix.</summary>
//		/// <param name="matrix">The matrix to be powered by.</param>
//		/// <param name="power">The power to apply to the matrix.</param>
//		/// <returns>The resulting matrix of the power operation.</returns>
//		public static Matrix_Flattened Power(Matrix_Flattened matrix, int power)
//		{
//			if (!(matrix.Rows == matrix.Columns))
//				throw new MatrixException("invalid power (!matrix.IsSquare).");
//			if (!(power > -1))
//				throw new MatrixException("invalid power !(power > -1)");
//			if (power == 0)
//				return Matrix_Flattened.FactoryIdentity(matrix.Rows, matrix.Columns);
//			Matrix_Flattened result = matrix.Clone();
//			for (int i = 0; i < power; i++)
//				result *= matrix;
//			return result;
//		}

//		/// <summary>Divides all the values in the matrix by a scalar.</summary>
//		/// <param name="left">The matrix to divide the values of.</param>
//		/// <param name="right">The scalar to divide all the matrix values by.</param>
//		/// <returns>The resulting matrix with the divided values.</returns>
//		public static Matrix_Flattened Divide(Matrix_Flattened left, float right) { return Matrix_Flattened.Multiply(left, 1.0f / right); }

//		/// <summary>Gets the minor of a matrix.</summary>
//		/// <param name="matrix">The matrix to get the minor of.</param>
//		/// <param name="row">The restricted row to form the minor.</param>
//		/// <param name="column">The restricted column to form the minor.</param>
//		/// <returns>The minor of the matrix.</returns>
//		public static Matrix_Flattened Minor(Matrix_Flattened matrix, int row, int column)
//		{
//			int matrixRows = matrix.Rows, matrixCols = matrix.Columns, resultCols = matrix.Columns - 1;
//			Matrix_Flattened result = new Matrix_Flattened(matrix.Rows - 1, resultCols);
//			float[] resultFloats = result.Floats;
//			float[] matrixFloats = matrix.Floats;
//			int m = 0, n = 0;
//			for (int i = 0; i < matrixRows; i++)
//			{
//				if (i == row) continue;
//				n = 0;
//				for (int j = 0; j < matrixCols; j++)
//					if (j == column) continue;
//					else resultFloats[m * resultCols + n++] = matrixFloats[i * matrixCols + j];
//				m++;
//			}
//			return result;
//		}

//		private static void RowMultiplication(Matrix_Flattened matrix, int row, float scalar)
//		{
//			float[] matrixFloats = matrix.Floats;
//			int start = row * matrix.Columns, end = row * matrix.Columns + matrix.Columns;
//			for (int j = start; j < end; j++)
//				matrixFloats[j] *= scalar;
//		}

//		private static void RowAddition(Matrix_Flattened matrix, int target, int second, float scalar)
//		{
//			float[] matrixfloats = matrix.Floats;
//			int columns = matrix.Columns,
//				targetOffset = target * columns,
//				secondOffset = second * columns;
//			for (int j = 0; j < columns; j++)
//				matrixfloats[targetOffset + j] += (matrixfloats[secondOffset + j] * scalar);
//		}

//		private static void SwapRows(Matrix_Flattened matrix, int row1, int row2)
//		{
//			float[] matrixFloats = matrix.Floats;
//			int columns = matrix.Columns, row1Offset = row1 * columns, row2Offset = row2 * columns;
//			for (int j = 0; j < columns; j++)
//			{
//				float temp = matrixFloats[row1Offset + j];
//				matrixFloats[row1Offset + j] = matrixFloats[row2Offset + j];
//				matrixFloats[row2Offset + j] = temp;
//			}
//		}

//		/// <summary>Combines two matrices from left to right 
//		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
//		/// <param name="left">The left matrix of the concatenation.</param>
//		/// <param name="right">The right matrix of the concatenation.</param>
//		/// <returns>The resulting matrix of the concatenation.</returns>
//		public static Matrix_Flattened ConcatenateRowWise(Matrix_Flattened left, Matrix_Flattened right)
//		{
//			if (left.Rows != right.Rows)
//				throw new MatrixException("invalid row-wise concatenation !(left.Rows == right.Rows).");
//			int resultRows = left.Rows, resultCols = left.Columns + right.Columns,
//				leftCols = left.Columns, rightCols = right.Columns;
//			Matrix_Flattened result = new Matrix_Flattened(resultRows, resultCols);
//			float[]
//				resultfloats = result.Floats,
//				leftFloats = left.Floats,
//				rightFloats = right.Floats;
//			for (int i = 0; i < resultRows; i++)
//				for (int j = 0; j < resultCols; j++)
//				{
//					if (j < left.Columns) resultfloats[i * resultCols + j] = leftFloats[i * leftCols + j];
//					else resultfloats[i * resultCols + j] = rightFloats[i * rightCols + j - leftCols];
//				}
//			return result;
//		}

//		/// <summary>Calculates the determinent of a square matrix.</summary>
//		/// <param name="matrix">The matrix to calculate the determinent of.</param>
//		/// <returns>The determinent of the matrix.</returns>
//		public static float Determinent(Matrix_Flattened matrix)
//		{
//			int rows = matrix.Rows, columns = matrix.Columns;
//			if (!(rows == matrix.Columns))
//				throw new MatrixException("invalid determinent !(matrix.IsSquare).");
//			float det = 1.0f;
//			try
//			{
//				float[] rref = new float[matrix.Floats.Length];
//				Buffer.BlockCopy(matrix.Floats, 0, rref, 0, rref.Length * sizeof(float));
//				for (int i = 0; i < rows; i++)
//				{
//					if (rref[i * columns + i] == 0)
//						for (int j = i + 1; j < rows; j++)
//							if (rref[j * columns + i] != 0)
//							{
//								Matrix_Flattened.SwapRows(matrix, i, j);
//								det *= -1;
//							}
//					det *= rref[i * columns + i];
//					Matrix_Flattened.RowMultiplication(matrix, i, 1 / rref[i * columns + i]);
//					for (int j = i + 1; j < rows; j++)
//						Matrix_Flattened.RowAddition(matrix, j, i, -rref[j * columns + i]);
//					for (int j = i - 1; j >= 0; j--)
//						Matrix_Flattened.RowAddition(matrix, j, i, -rref[j * columns + i]);
//				}
//				return det;
//			}
//			catch (Exception) { throw new MatrixException("determinent computation failed."); }
//		}

//		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
//		/// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
//		/// <returns>The echelon of the matrix (aka REF).</returns>
//		public static Matrix_Flattened Echelon(Matrix_Flattened matrix)
//		{
//			try
//			{
//				int rows = matrix.Rows, columns = matrix.Columns;
//				Matrix_Flattened result = new Matrix_Flattened(rows, columns, matrix.Floats);
//				float[] resultfloats = result.Floats;
//				for (int i = 0; i < rows; i++)
//				{
//					if (resultfloats[i * columns + i] == 0)
//						for (int j = i + 1; j < rows; j++)
//							if (resultfloats[j * columns + i] != 0)
//								Matrix_Flattened.SwapRows(result, i, j);
//					if (resultfloats[i * columns + i] == 0)
//						continue;
//					if (resultfloats[i * columns + i] != 1)
//						for (int j = i + 1; j < rows; j++)
//							if (resultfloats[j * columns + i] == 1)
//								Matrix_Flattened.SwapRows(result, i, j);
//					Matrix_Flattened.RowMultiplication(result, i, 1 / resultfloats[i * columns + i]);
//					for (int j = i + 1; j < rows; j++)
//						Matrix_Flattened.RowAddition(result, j, i, -resultfloats[j * columns + i]);
//				}
//				return result;
//			}
//			catch { throw new MatrixException("echelon computation failed."); }
//		}

//		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
//		/// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
//		/// <returns>The reduced echelon of the matrix (aka RREF).</returns>
//		public static Matrix_Flattened ReducedEchelon(Matrix_Flattened matrix)
//		{
//			try
//			{
//				int rows = matrix.Rows, columns = matrix.Columns;
//				Matrix_Flattened result = new Matrix_Flattened(rows, columns, matrix.Floats);
//				float[] resultFloats = result.Floats;
//				for (int i = 0; i < rows; i++)
//				{
//					if (resultFloats[i * columns + i] == 0)
//						for (int j = i + 1; j < rows; j++)
//							if (resultFloats[j * columns + i] != 0)
//								Matrix_Flattened.SwapRows(result, i, j);
//					if (resultFloats[i * columns + i] == 0) continue;
//					if (resultFloats[i * columns + i] != 1)
//						for (int j = i + 1; j < rows; j++)
//							if (resultFloats[j * columns + i] == 1)
//								Matrix_Flattened.SwapRows(result, i, j);
//					Matrix_Flattened.RowMultiplication(result, i, 1 / resultFloats[i * columns + i]);
//					for (int j = i + 1; j < rows; j++)
//						Matrix_Flattened.RowAddition(result, j, i, -resultFloats[j * columns + i]);
//					for (int j = i - 1; j >= 0; j--)
//						Matrix_Flattened.RowAddition(result, j, i, -resultFloats[j * columns + i]);
//				}
//				return result;
//			}
//			catch { throw new MatrixException("reduced echelon calculation failed."); }
//		}

//		/// <summary>Calculates the inverse of a matrix.</summary>
//		/// <param name="matrix">The matrix to calculate the inverse of.</param>
//		/// <returns>The inverse of the matrix.</returns>
//		public static Matrix_Flattened Inverse(Matrix_Flattened matrix)
//		{
//			if (Matrix_Flattened.Determinent(matrix) == 0)
//				throw new MatrixException("inverse calculation failed.");
//			try
//			{
//				Matrix_Flattened identity = Matrix_Flattened.FactoryIdentity(matrix.Rows, matrix.Columns);
//				Matrix_Flattened rref = Matrix_Flattened.Clone(matrix);
//				for (int i = 0; i < matrix.Rows; i++)
//				{
//					if (rref[i, i] == 0)
//						for (int j = i + 1; j < rref.Rows; j++)
//							if (rref[j, i] != 0)
//							{
//								Matrix_Flattened.SwapRows(rref, i, j);
//								Matrix_Flattened.SwapRows(identity, i, j);
//							}
//					Matrix_Flattened.RowMultiplication(identity, i, 1 / rref[i, i]);
//					Matrix_Flattened.RowMultiplication(rref, i, 1 / rref[i, i]);
//					for (int j = i + 1; j < rref.Rows; j++)
//					{
//						Matrix_Flattened.RowAddition(identity, j, i, -rref[j, i]);
//						Matrix_Flattened.RowAddition(rref, j, i, -rref[j, i]);
//					}
//					for (int j = i - 1; j >= 0; j--)
//					{
//						Matrix_Flattened.RowAddition(identity, j, i, -rref[j, i]);
//						Matrix_Flattened.RowAddition(rref, j, i, -rref[j, i]);
//					}
//				}
//				return identity;
//			}
//			catch { throw new MatrixException("inverse calculation failed."); }
//		}

//		/// <summary>Calculates the adjoint of a matrix.</summary>
//		/// <param name="matrix">The matrix to calculate the adjoint of.</param>
//		/// <returns>The adjoint of the matrix.</returns>
//		public static Matrix_Flattened Adjoint(Matrix_Flattened matrix)
//		{
//			if (!(matrix.Rows == matrix.Columns))
//				throw new MatrixException("Adjoint of a non-square matrix does not exists");
//			Matrix_Flattened AdjointMatrix = new Matrix_Flattened(matrix.Rows, matrix.Columns);
//			for (int i = 0; i < matrix.Rows; i++)
//				for (int j = 0; j < matrix.Columns; j++)
//					if ((i + j) % 2 == 0)
//						AdjointMatrix[i, j] = Matrix_Flattened.Determinent(Matrix_Flattened.Minor(matrix, i, j));
//					else
//						AdjointMatrix[i, j] = -Matrix_Flattened.Determinent(Matrix_Flattened.Minor(matrix, i, j));
//			return Matrix_Flattened.Transpose(AdjointMatrix);
//		}

//		/// <summary>Returns the transpose of a matrix.</summary>
//		/// <param name="matrix">The matrix to transpose.</param>
//		/// <returns>The transpose of the matrix.</returns>
//		public static Matrix_Flattened Transpose(Matrix_Flattened matrix)
//		{
//			Matrix_Flattened result = new Matrix_Flattened(matrix.Columns, matrix.Rows);
//			float[] matrixfloats = matrix.Floats;
//			int rows = matrix.Columns, columns = matrix.Rows;
//			float[] resultFloats = result.Floats;
//			for (int i = 0; i < rows; i++)
//				for (int j = 0; j < columns; j++)
//					resultFloats[i * columns + j] = matrixfloats[j * rows + i];
//			return result;
//		}

//		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
//		/// <param name="matrix">The matrix to decompose.</param>
//		/// <param name="Lower">The computed lower triangular matrix.</param>
//		/// <param name="Upper">The computed upper triangular matrix.</param>
//		public static void DecomposeLU(Matrix_Flattened matrix, out Matrix_Flattened Lower, out Matrix_Flattened Upper)
//		{
//			if (!(matrix.Rows == matrix.Columns))
//				throw new MatrixException("The matrix is not square!");
//			Lower = Matrix_Flattened.FactoryIdentity(matrix.Rows, matrix.Columns);
//			Upper = Matrix_Flattened.Clone(matrix);
//			int[] permutation = new int[matrix.Rows];
//			for (int i = 0; i < matrix.Rows; i++) permutation[i] = i;
//			float p = 0, pom2, detOfP = 1;
//			int k0 = 0, pom1 = 0;
//			for (int k = 0; k < matrix.Columns - 1; k++)
//			{
//				p = 0;
//				for (int i = k; i < matrix.Rows; i++)
//					if (Calc.Abs(Upper[i, k]) > p)
//					{
//						p = Calc.Abs(Upper[i, k]);
//						k0 = i;
//					}
//				if (p == 0)
//					throw new MatrixException("The matrix is singular!");
//				pom1 = permutation[k];
//				permutation[k] = permutation[k0];
//				permutation[k0] = pom1;
//				for (int i = 0; i < k; i++)
//				{
//					pom2 = Lower[k, i];
//					Lower[k, i] = Lower[k0, i];
//					Lower[k0, i] = pom2;
//				}
//				if (k != k0)
//					detOfP *= -1;
//				for (int i = 0; i < matrix.Columns; i++)
//				{
//					pom2 = Upper[k, i];
//					Upper[k, i] = Upper[k0, i];
//					Upper[k0, i] = pom2;
//				}
//				for (int i = k + 1; i < matrix.Rows; i++)
//				{
//					Lower[i, k] = Upper[i, k] / Upper[k, k];
//					for (int j = k; j < matrix.Columns; j++)
//						Upper[i, j] = Upper[i, j] - Lower[i, k] * Upper[k, j];
//				}
//			}
//		}

//		/// <summary>Does a value equality check.</summary>
//		/// <param name="left">The first matrix to check for equality.</param>
//		/// <param name="right">The second matrix to check for equality.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public static bool EqualsByValue(Matrix_Flattened left, Matrix_Flattened right)
//		{
//			int rows = left.Rows, columns = left.Columns;
//			if (rows != right.Rows || columns != right.Columns)
//				return false;
//			float[] leftFloats = left.Floats;
//			float[] rightFloats = right.Floats;
//			for (int i = 0; i < leftFloats.Length; i++)
//				if (leftFloats[i] != rightFloats[i])
//					return false;
//			return true;
//		}

//		/// <summary>Does a value equality check with leniency.</summary>
//		/// <param name="left">The first matrix to check for equality.</param>
//		/// <param name="right">The second matrix to check for equality.</param>
//		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public static bool EqualsByValue(Matrix_Flattened left, Matrix_Flattened right, float leniency)
//		{
//			int rows = left.Rows, columns = left.Columns;
//			if (rows != right.Rows || columns != right.Columns)
//				return false;
//			float[] leftFloats = left.Floats;
//			float[] rightFloats = right.Floats;
//			for (int i = 0; i < leftFloats.Length; i++)
//				if (Calc.Abs(leftFloats[i] - rightFloats[i]) > leniency)
//					return false;
//			return true;
//		}

//		/// <summary>Checks if two matrices are equal by reverences.</summary>
//		/// <param name="left">The left matric of the equality check.</param>
//		/// <param name="right">The right matrix of the equality check.</param>
//		/// <returns>True if the references are equal, false if not.</returns>
//		public static bool EqualsByReference(Matrix_Flattened left, Matrix_Flattened right)
//		{
//			if (left == null || right == null) return false;
//			return object.ReferenceEquals(left, right) || object.ReferenceEquals(left.Floats, right.Floats);
//		}

//		/// <summary>Copies a matrix.</summary>
//		/// <returns>The copy of this matrix.</returns>
//		public static Matrix_Flattened Clone(Matrix_Flattened matrix)
//		{
//			float[] floats = new float[matrix.Size];
//			Buffer.BlockCopy(matrix.Floats, 0, floats, 0, floats.Length * sizeof(float));
//			return new Matrix_Flattened(matrix.Rows, matrix.Columns, floats);
//		}

//		/// <summary>Converts the matrix into a vector if (matrix.IsVector).</summary>
//		/// <param name="matrix">The matrix to convert.</param>
//		/// <returns>The resulting vector.</returns>
//		public static Vector ToVector(Matrix_Flattened matrix)
//		{
//			if (!matrix.IsVector)
//				throw new MatrixException("invalid conversion from matrix to vector.");
//			return new Vector(matrix.Floats);
//		}

//		/// <summary>Prints out a string representation of this matrix.</summary>
//		/// <returns>A string representing this matrix.</returns>
//		public override string ToString()
//		{
//			return base.ToString();
//			//StringBuilder matrix = new StringBuilder();
//			//for (int i = 0; i < Rows; i++)
//			//{
//			//  for (int j = 0; j < Columns; j++)
//			//    matrix.Append(String.Format("{0:0.##}\t", _matrix[i, j]));
//			//  matrix.Append("\n");
//			//}
//			//return matrix.ToString();
//		}

//		/// <summary>Computes a hash code from the values of this matrix.</summary>
//		/// <returns>A hash code for the matrix.</returns>
//		public override int GetHashCode()
//		{
//			// return base.GetHashCode();
//			int hash = _matrix[0].GetHashCode();
//			for (int i = 0; i < _matrix.Length; i++)
//				hash = hash ^ _matrix[i].GetHashCode();
//			return hash;
//		}

//		/// <summary>Does an equality check by reference.</summary>
//		/// <param name="obj">The object to compare to.</param>
//		/// <returns>True if the references are equal, false if not.</returns>
//		public override bool Equals(object right)
//		{
//			if (!(right is Matrix_Flattened)) return false;
//			return Matrix_Flattened.EqualsByReference(this, (Matrix_Flattened)right);
//		}

//		private class MatrixException : Error
//		{
//			public MatrixException(string Message) : base(Message) { }
//		}
//	}

//	/// <summary>A matrix wrapper for float[,] to perform matrix theory in row major order. Enjoy :)</summary>
//	public class Matrix_2dArray //: Matrix<float>
//	{
//		private float[,] _matrix;

//		/// <summary>The float[,] reference of this matrix.</summary>
//		public float[,] Floats { get { return _matrix; } }
//		/// <summary>The number of rows in the matrix.</summary>
//		public int Rows { get { return _matrix.GetLength(0); } }
//		/// <summary>The number of columns in the matrix.</summary>
//		public int Columns { get { return _matrix.GetLength(1); } }
//		/// <summary>Determines if the matrix is square.</summary>
//		public bool IsSquare { get { return Rows == Columns; } }
//		/// <summary>Determines if the matrix is a vector.</summary>
//		public bool IsVector { get { return Columns == 1; } }
//		/// <summary>Determines if the matrix is a 2 component vector.</summary>
//		public bool Is2x1 { get { return Rows == 2 && Columns == 1; } }
//		/// <summary>Determines if the matrix is a 3 component vector.</summary>
//		public bool Is3x1 { get { return Rows == 3 && Columns == 1; } }
//		/// <summary>Determines if the matrix is a 4 component vector.</summary>
//		public bool Is4x1 { get { return Rows == 4 && Columns == 1; } }
//		/// <summary>Determines if the matrix is a 2 square matrix.</summary>
//		public bool Is2x2 { get { return Rows == 2 && Columns == 2; } }
//		/// <summary>Determines if the matrix is a 3 square matrix.</summary>
//		public bool Is3x3 { get { return Rows == 3 && Columns == 3; } }
//		/// <summary>Determines if the matrix is a 4 square matrix.</summary>
//		public bool Is4x4 { get { return Rows == 4 && Columns == 4; } }

//		/// <summary>Standard row-major matrix indexing.</summary>
//		/// <param name="row">The row index.</param>
//		/// <param name="column">The column index.</param>
//		/// <returns>The value at the given indeces.</returns>
//		public float this[int row, int column]
//		{
//			get
//			{
//				try { return _matrix[row, column]; }
//				catch { throw new MatrixException("index out of bounds."); }
//			}
//			set
//			{
//				try { _matrix[row, column] = value; }
//				catch { throw new MatrixException("index out of bounds."); }
//			}
//		}

//		/// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
//		/// <param name="rows">The number of row dimensions.</param>
//		/// <param name="columns">The number of column dimensions.</param>
//		public Matrix_2dArray(int rows, int columns)
//		{
//			try { _matrix = new float[rows, columns]; }
//			catch { throw new MatrixException("invalid dimensions."); }
//		}

//		/// <summary>Constructs a new array given row/column dimensions and the values to fill the matrix with.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <param name="values">The values to fill the matrix with.</param>
//		public Matrix_2dArray(int rows, int columns, params float[] values)
//		{
//			if (values.Length != rows * columns)
//				throw new MatrixException("invalid construction (number of values does not match dimensions.)");
//			float[,] matrix;
//			try { matrix = new float[rows, columns]; }
//			catch { throw new MatrixException("invalid dimensions."); }
//			int k = 0;
//			for (int i = 0; i < rows; i++)
//				for (int j = 0; j < columns; j++)
//					matrix[i, j] = values[k++];
//			_matrix = matrix;
//		}

//		/// <summary>Wraps a float[,] inside of a matrix class. WARNING: still references that float[,].</summary>
//		/// <param name="matrix">The float[,] to wrap in a matrix class.</param>
//		public Matrix_2dArray(float[,] matrix)
//		{
//			_matrix = matrix;
//		}

//		/// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <returns>The newly constructed zero-matrix.</returns>
//		public static Matrix_2dArray FactoryZero(int rows, int columns)
//		{
//			try { return new Matrix_2dArray(rows, columns); }
//			catch { throw new MatrixException("invalid dimensions."); }
//		}

//		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <returns>The newly constructed identity-matrix.</returns>
//		public static Matrix_2dArray FactoryIdentity(int rows, int columns)
//		{
//			Matrix_2dArray matrix;
//			try { matrix = new Matrix_2dArray(rows, columns); }
//			catch { throw new MatrixException("invalid dimensions."); }
//			if (rows <= columns)
//				for (int i = 0; i < rows; i++)
//					matrix[i, i] = 1;
//			else
//				for (int i = 0; i < columns; i++)
//					matrix[i, i] = 1;
//			return matrix;
//		}

//		/// <summary>Constructs a new matrix where every entry is 1.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <returns>The newly constructed matrix filled with 1's.</returns>
//		public static Matrix_2dArray FactoryOne(int rows, int columns)
//		{
//			Matrix_2dArray matrix;
//			try { matrix = new Matrix_2dArray(rows, columns); }
//			catch { throw new MatrixException("invalid dimensions."); }
//			for (int i = 0; i < rows; i++)
//				for (int j = 0; j < columns; j++)
//					matrix[i, j] = 1;
//			return matrix;
//		}

//		/// <summary>Constructs a new matrix where every entry is the same uniform value.</summary>
//		/// <param name="rows">The number of rows of the matrix.</param>
//		/// <param name="columns">The number of columns of the matrix.</param>
//		/// <param name="uniform">The value to assign every spot in the matrix.</param>
//		/// <returns>The newly constructed matrix filled with the uniform value.</returns>
//		public static Matrix_2dArray FactoryUniform(int rows, int columns, float uniform)
//		{
//			Matrix_2dArray matrix;
//			try { matrix = new Matrix_2dArray(rows, columns); }
//			catch { throw new MatrixException("invalid dimensions."); }
//			for (int i = 0; i < rows; i++)
//				for (int j = 0; j < columns; j++)
//					matrix[i, j] = uniform;
//			return matrix;
//		}

//		/// <summary>Constructs a 2-component vector matrix with all values being 0.</summary>
//		/// <returns>The constructed 2-component vector matrix.</returns>
//		public static Matrix_2dArray Factory2x1() { return new Matrix_2dArray(2, 1); }
//		/// <summary>Constructs a 3-component vector matrix with all values being 0.</summary>
//		/// <returns>The constructed 3-component vector matrix.</returns>
//		public static Matrix_2dArray Factory3x1() { return new Matrix_2dArray(3, 1); }
//		/// <summary>Constructs a 4-component vector matrix with all values being 0.</summary>
//		/// <returns>The constructed 4-component vector matrix.</returns>
//		public static Matrix_2dArray Factory4x1() { return new Matrix_2dArray(4, 1); }

//		/// <summary>Constructs a 2x2 matrix with all values being 0.</summary>
//		/// <returns>The constructed 2x2 matrix.</returns>
//		public static Matrix_2dArray Factory2x2() { return new Matrix_2dArray(2, 2); }
//		/// <summary>Constructs a 3x3 matrix with all values being 0.</summary>
//		/// <returns>The constructed 3x3 matrix.</returns>
//		public static Matrix_2dArray Factory3x3() { return new Matrix_2dArray(3, 3); }
//		/// <summary>Constructs a 4x4 matrix with all values being 0.</summary>
//		/// <returns>The constructed 4x4 matrix.</returns>
//		public static Matrix_2dArray Factory4x4() { return new Matrix_2dArray(4, 4); }

//		/// <param name="angle">Angle of rotation in radians.</param>
//		public static Matrix_2dArray Factory3x3RotationX(float angle)
//		{
//			float cos = Calc.Cos(angle);
//			float sin = Calc.Sin(angle);
//			return new Matrix_2dArray(new float[,] {
//				{ 1, 0, 0 },
//				{ 0, cos, sin },
//				{ 0, -sin, cos }});
//		}

//		/// <param name="angle">Angle of rotation in radians.</param>
//		public static Matrix_2dArray Factory3x3RotationY(float angle)
//		{
//			float cos = Calc.Cos(angle);
//			float sin = Calc.Sin(angle);
//			return new Matrix_2dArray(new float[,] {
//				{ cos, 0, -sin },
//				{ 0, 1, 0 },
//				{ sin, 0, cos }});
//		}

//		/// <param name="angle">Angle of rotation in radians.</param>
//		public static Matrix_2dArray Factory3x3RotationZ(float angle)
//		{
//			float cos = Calc.Cos(angle);
//			float sin = Calc.Sin(angle);
//			return new Matrix_2dArray(new float[,] {
//				{ cos, -sin, 0 },
//				{ sin, cos, 0 },
//				{ 0, 0, 1 }});
//		}

//		/// <param name="angleX">Angle about the X-axis in radians.</param>
//		/// <param name="angleY">Angle about the Y-axis in radians.</param>
//		/// <param name="angleZ">Angle about the Z-axis in radians.</param>
//		public static Matrix_2dArray Factory3x3RotationXthenYthenZ(float angleX, float angleY, float angleZ)
//		{
//			float
//				xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
//				yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
//				zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
//			return new Matrix_2dArray(new float[,] {
//				{ yCos * zCos, -yCos * zSin, ySin },
//				{ xCos * zSin + xSin * ySin * zCos, xCos * zCos + xSin * ySin * zSin, -xSin * yCos },
//				{ xSin * zSin - xCos * ySin * zCos, xSin * zCos + xCos * ySin * zSin, xCos * yCos }});
//		}

//		/// <param name="angleX">Angle about the X-axis in radians.</param>
//		/// <param name="angleY">Angle about the Y-axis in radians.</param>
//		/// <param name="angleZ">Angle about the Z-axis in radians.</param>
//		public static Matrix_2dArray Factory3x3RotationZthenYthenX(float angleX, float angleY, float angleZ)
//		{
//			float
//				xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
//				yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
//				zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
//			return new Matrix_2dArray(new float[,] {
//				{ yCos * zCos, zCos * xSin * ySin - xCos * zSin, xCos * zCos * ySin + xSin * zSin },
//				{ yCos * zSin, xCos * zCos + xSin * ySin * zSin, -zCos * xSin + xCos * ySin * zSin },
//				{ -ySin, yCos * xSin, xCos * yCos }});
//		}

//		/// <summary>Creates a 3x3 matrix initialized with a shearing transformation.</summary>
//		/// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
//		/// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
//		/// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
//		/// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
//		/// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
//		/// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
//		/// <returns>The constructed shearing matrix.</returns>
//		public static Matrix_2dArray Factory3x3Shear(
//			float shearXbyY, float shearXbyZ, float shearYbyX,
//			float shearYbyZ, float shearZbyX, float shearZbyY)
//		{
//			return new Matrix_2dArray(new float[,] {
//				{ 1, shearYbyX, shearZbyX },
//				{ shearXbyY, 1, shearYbyZ },
//				{ shearXbyZ, shearYbyZ, 1 }});
//		}

//		/// <summary>Negates all the values in a matrix.</summary>
//		/// <param name="matrix">The matrix to have its values negated.</param>
//		/// <returns>The resulting matrix after the negations.</returns>
//		public static Matrix_2dArray operator -(Matrix_2dArray matrix) { return Matrix_2dArray.Negate(matrix); }
//		/// <summary>Does a standard matrix addition.</summary>
//		/// <param name="left">The left matrix of the addition.</param>
//		/// <param name="right">The right matrix of the addition.</param>
//		/// <returns>The resulting matrix after teh addition.</returns>
//		public static Matrix_2dArray operator +(Matrix_2dArray left, Matrix_2dArray right) { return Matrix_2dArray.Add(left, right); }
//		/// <summary>Does a standard matrix subtraction.</summary>
//		/// <param name="left">The left matrix of the subtraction.</param>
//		/// <param name="right">The right matrix of the subtraction.</param>
//		/// <returns>The result of the matrix subtraction.</returns>
//		public static Matrix_2dArray operator -(Matrix_2dArray left, Matrix_2dArray right) { return Matrix_2dArray.Subtract(left, right); }
//		/// <summary>Does a standard matrix multiplication.</summary>
//		/// <param name="left">The left matrix of the multiplication.</param>
//		/// <param name="right">The right matrix of the multiplication.</param>
//		/// <returns>The resulting matrix after the multiplication.</returns>
//		public static Matrix_2dArray operator *(Matrix_2dArray left, Matrix_2dArray right) { return Matrix_2dArray.Multiply(left, right); }
//		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
//		/// <param name="left">The matrix to have its values multiplied.</param>
//		/// <param name="right">The scalar to multiply the values by.</param>
//		/// <returns>The resulting matrix after the multiplications.</returns>
//		public static Matrix_2dArray operator *(Matrix_2dArray left, float right) { return Matrix_2dArray.Multiply(left, right); }
//		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
//		/// <param name="left">The scalar to multiply the values by.</param>
//		/// <param name="right">The matrix to have its values multiplied.</param>
//		/// <returns>The resulting matrix after the multiplications.</returns>
//		public static Matrix_2dArray operator *(float left, Matrix_2dArray right) { return Matrix_2dArray.Multiply(right, left); }
//		/// <summary>Divides all the values in a matrix by a scalar.</summary>
//		/// <param name="left">The matrix to have its values divided.</param>
//		/// <param name="right">The scalar to divide the values by.</param>
//		/// <returns>The resulting matrix after the divisions.</returns>
//		public static Matrix_2dArray operator /(Matrix_2dArray left, float right) { return Matrix_2dArray.Divide(left, right); }
//		/// <summary>Applies a power to a matrix.</summary>
//		/// <param name="left">The matrix to apply a power to.</param>
//		/// <param name="right">The power to apply to the matrix.</param>
//		/// <returns>The result of the power operation.</returns>
//		public static Matrix_2dArray operator ^(Matrix_2dArray left, int right) { return Matrix_2dArray.Power(left, right); }
//		/// <summary>Checks for equality by value.</summary>
//		/// <param name="left">The left matrix of the equality check.</param>
//		/// <param name="right">The right matrix of the equality check.</param>
//		/// <returns>True if the values of the matrices are equal, false if not.</returns>
//		public static bool operator ==(Matrix_2dArray left, Matrix_2dArray right) { return Matrix_2dArray.EqualsByValue(left, right); }
//		/// <summary>Checks for false-equality by value.</summary>
//		/// <param name="left">The left matrix of the false-equality check.</param>
//		/// <param name="right">The right matrix of the false-equality check.</param>
//		/// <returns>True if the values of the matrices are not equal, false if they are.</returns>
//		public static bool operator !=(Matrix_2dArray left, Matrix_2dArray right) { return !Matrix_2dArray.EqualsByValue(left, right); }
//		/// <summary>Automatically converts a matrix into a float[,] if necessary.</summary>
//		/// <param name="matrix">The matrix to convert to a float[,].</param>
//		/// <returns>The reference to the float[,] representing the matrix.</returns>
//		public static implicit operator float[,](Matrix_2dArray matrix) { return matrix.Floats; }

//		/// <summary>Negates all the values in this matrix.</summary>
//		/// <returns>The resulting matrix after the negations.</returns>
//		private Matrix_2dArray Negate() { return Matrix_2dArray.Negate(this); }
//		/// <summary>Does a standard matrix addition.</summary>
//		/// <param name="right">The matrix to add to this matrix.</param>
//		/// <returns>The resulting matrix after the addition.</returns>
//		private Matrix_2dArray Add(Matrix_2dArray right) { return Matrix_2dArray.Add(this, right); }
//		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
//		/// <param name="right">The matrix to multiply this matrix by.</param>
//		/// <returns>The resulting matrix after the multiplication.</returns>
//		private Matrix_2dArray Multiply(Matrix_2dArray right) { return Matrix_2dArray.Multiply(this, right); }
//		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
//		/// <param name="right">The scalar to multiply all the matrix values by.</param>
//		/// <returns>The retulting matrix after the multiplications.</returns>
//		private Matrix_2dArray Multiply(float right) { return Matrix_2dArray.Multiply(this, right); }
//		/// <summary>Divides all the values in this matrix by a scalar.</summary>
//		/// <param name="right">The scalar to divide the matrix values by.</param>
//		/// <returns>The resulting matrix after teh divisions.</returns>
//		private Matrix_2dArray Divide(float right) { return Matrix_2dArray.Divide(this, right); }
//		/// <summary>Gets the minor of a matrix.</summary>
//		/// <param name="row">The restricted row of the minor.</param>
//		/// <param name="column">The restricted column of the minor.</param>
//		/// <returns>The minor from the row/column restrictions.</returns>
//		public Matrix_2dArray Minor(int row, int column) { return Matrix_2dArray.Minor(this, row, column); }
//		/// <summary>Combines two matrices from left to right 
//		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
//		/// <param name="right">The matrix to combine with on the right side.</param>
//		/// <returns>The resulting row-wise concatination.</returns>
//		public Matrix_2dArray ConcatenateRowWise(Matrix_2dArray right) { return Matrix_2dArray.ConcatenateRowWise(this, right); }
//		/// <summary>Computes the determinent if this matrix is square.</summary>
//		/// <returns>The computed determinent if this matrix is square.</returns>
//		public float Determinent() { return Matrix_2dArray.Determinent(this); }
//		/// <summary>Computes the echelon form of this matrix (aka REF).</summary>
//		/// <returns>The computed echelon form of this matrix (aka REF).</returns>
//		public Matrix_2dArray Echelon() { return Matrix_2dArray.Echelon(this); }
//		/// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
//		/// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
//		public Matrix_2dArray ReducedEchelon() { return Matrix_2dArray.ReducedEchelon(this); }
//		/// <summary>Computes the inverse of this matrix.</summary>
//		/// <returns>The inverse of this matrix.</returns>
//		public Matrix_2dArray Inverse() { return Matrix_2dArray.Inverse(this); }
//		/// <summary>Gets the adjoint of this matrix.</summary>
//		/// <returns>The adjoint of this matrix.</returns>
//		public Matrix_2dArray Adjoint() { return Matrix_2dArray.Adjoint(this); }
//		/// <summary>Transposes this matrix.</summary>
//		/// <returns>The transpose of this matrix.</returns>
//		public Matrix_2dArray Transpose() { return Matrix_2dArray.Transpose(this); }
//		/// <summary>Copies this matrix.</summary>
//		/// <returns>The copy of this matrix.</returns>
//		public Matrix_2dArray Clone() { return Matrix_2dArray.Clone(this); }

//		/// <summary>Negates all the values in a matrix.</summary>
//		/// <param name="matrix">The matrix to have its values negated.</param>
//		/// <returns>The resulting matrix after the negations.</returns>
//		public static Matrix_2dArray Negate(float[,] matrix)
//		{
//			Matrix_2dArray result = new Matrix_2dArray(matrix.GetLength(0), matrix.GetLength(1));
//			for (int i = 0; i < matrix.GetLength(0); i++)
//				for (int j = 0; j < matrix.GetLength(1); j++)
//					result[i, j] = -matrix[i, j];
//			return result;
//		}

//		/// <summary>Does standard addition of two matrices.</summary>
//		/// <param name="left">The left matrix of the addition.</param>
//		/// <param name="right">The right matrix of the addition.</param>
//		/// <returns>The resulting matrix after the addition.</returns>
//		public static Matrix_2dArray Add(float[,] left, float[,] right)
//		{
//			if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
//				throw new MatrixException("invalid addition (size miss-match).");
//			Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), left.GetLength(1));
//			for (int i = 0; i < result.Rows; i++)
//				for (int j = 0; j < result.Columns; j++)
//					result[i, j] = left[i, j] + right[i, j];
//			return result;
//		}

//		/// <summary>Subtracts a scalar from all the values in a matrix.</summary>
//		/// <param name="left">The matrix to have the values subtracted from.</param>
//		/// <param name="right">The scalar to subtract from all the matrix values.</param>
//		/// <returns>The resulting matrix after the subtractions.</returns>
//		public static Matrix_2dArray Subtract(float[,] left, float[,] right)
//		{
//			if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
//				throw new MatrixException("invalid subtraction (size miss-match).");
//			Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), left.GetLength(1));
//			for (int i = 0; i < result.Rows; i++)
//				for (int j = 0; j < result.Columns; j++)
//					result[i, j] = left[i, j] - right[i, j];
//			return result;
//		}

//		///// <summary>Subtracts a scalar from all the values in a matrix.</summary>
//		///// <param name="left">The matrix to have the values subtracted from.</param>
//		///// <param name="right">The scalar to subtract from all the matrix values.</param>
//		///// <returns>The resulting matrix after the subtractions.</returns>
//		//public static Vector Subtract(float[,] left, float[] right)
//		//{
//		//  if (!(left.GetLength(1) == 1 && left.GetLength(0) == right.Length))
//		//    throw new MatrixException("invalid subtraction (size miss-match).");
//		//  Vector result = new Vector(left.GetLength(0));
//		//  for (int i = 0; i < result.Dimensions; i++)
//		//    result[i] = left[i, 0] - right[i];
//		//  return result;
//		//}

//		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
//		/// <param name="left">The left matrix of the multiplication.</param>
//		/// <param name="right">The right matrix of the multiplication.</param>
//		/// <returns>The resulting matrix of the multiplication.</returns>
//		public static Matrix_2dArray Multiply(float[,] left, float[,] right)
//		{
//			if (left.GetLength(1) != right.GetLength(0))
//				throw new MatrixException("invalid multiplication (size miss-match).");
//			Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), right.GetLength(1));
//			for (int i = 0; i < result.Rows; i++)
//				for (int j = 0; j < result.Columns; j++)
//					for (int k = 0; k < left.GetLength(1); k++)
//						result[i, j] += left[i, k] * right[k, j];
//			return result;
//		}

//		///// <summary>Does a standard multiplication between a matrix and a vector.</summary>
//		///// <param name="left">The left matrix of the multiplication.</param>
//		///// <param name="right">The right vector of the multiplication.</param>
//		///// <returns>The resulting matrix/vector of the multiplication.</returns>
//		//public static Matrix Multiply(float[,] left, float[] right)
//		//{
//		//  if (left.GetLength(1) != right.GetLength(0))
//		//    throw new MatrixException("invalid multiplication (size miss-match).");
//		//  Matrix result = new Matrix(left.GetLength(0), right.GetLength(1));
//		//  for (int i = 0; i < result.Rows; i++)
//		//      for (int k = 0; k < left.GetLength(1); k++)
//		//        result[i, j] += left[i, k] * right[k];
//		//  return result;
//		//}

//		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
//		/// <param name="left">The matrix to have the values multiplied.</param>
//		/// <param name="right">The scalar to multiply the values by.</param>
//		/// <returns>The resulting matrix after the multiplications.</returns>
//		public static Matrix_2dArray Multiply(float[,] left, float right)
//		{
//			Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), left.GetLength(1));
//			for (int i = 0; i < left.GetLength(0); i++)
//				for (int j = 0; j < left.GetLength(1); j++)
//					result[i, j] = left[i, j] * right;
//			return result;
//		}

//		/// <summary>Applies a power to a square matrix.</summary>
//		/// <param name="matrix">The matrix to be powered by.</param>
//		/// <param name="power">The power to apply to the matrix.</param>
//		/// <returns>The resulting matrix of the power operation.</returns>
//		public static Matrix_2dArray Power(float[,] matrix, int power)
//		{
//			if (!(matrix.GetLength(0) == matrix.GetLength(1)))
//				throw new MatrixException("invalid power (!matrix.IsSquare).");
//			if (!(power > -1))
//				throw new MatrixException("invalid power !(power > -1)");
//			if (power == 0)
//				return Matrix_2dArray.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
//			Matrix_2dArray result = Matrix_2dArray.Clone(matrix);
//			for (int i = 0; i < power; i++)
//				result *= result;
//			return result;
//		}

//		/// <summary>Divides all the values in the matrix by a scalar.</summary>
//		/// <param name="matrix">The matrix to divide the values of.</param>
//		/// <param name="right">The scalar to divide all the matrix values by.</param>
//		/// <returns>The resulting matrix with the divided values.</returns>
//		public static Matrix_2dArray Divide(float[,] matrix, float right)
//		{
//			Matrix_2dArray result = new Matrix_2dArray(matrix.GetLength(0), matrix.GetLength(1));
//			for (int i = 0; i < matrix.GetLength(0); i++)
//				for (int j = 0; j < matrix.GetLength(1); j++)
//					result[i, j] = matrix[i, j] / right;
//			return result;
//		}

//		/// <summary>Gets the minor of a matrix.</summary>
//		/// <param name="matrix">The matrix to get the minor of.</param>
//		/// <param name="row">The restricted row to form the minor.</param>
//		/// <param name="column">The restricted column to form the minor.</param>
//		/// <returns>The minor of the matrix.</returns>
//		public static Matrix_2dArray Minor(float[,] matrix, int row, int column)
//		{
//			Matrix_2dArray minor = new Matrix_2dArray(matrix.GetLength(0) - 1, matrix.GetLength(1) - 1);
//			int m = 0, n = 0;
//			for (int i = 0; i < matrix.GetLength(0); i++)
//			{
//				if (i == row) continue;
//				n = 0;
//				for (int j = 0; j < matrix.GetLength(1); j++)
//				{
//					if (j == column) continue;
//					minor[m, n] = matrix[i, j];
//					n++;
//				}
//				m++;
//			}
//			return minor;
//		}

//		private static void RowMultiplication(float[,] matrix, int row, float scalar)
//		{
//			for (int j = 0; j < matrix.GetLength(1); j++)
//				matrix[row, j] *= scalar;
//		}

//		private static void RowAddition(float[,] matrix, int target, int second, float scalar)
//		{
//			for (int j = 0; j < matrix.GetLength(1); j++)
//				matrix[target, j] += (matrix[second, j] * scalar);
//		}

//		private static void SwapRows(float[,] matrix, int row1, int row2)
//		{
//			for (int j = 0; j < matrix.GetLength(1); j++)
//			{
//				float temp = matrix[row1, j];
//				matrix[row1, j] = matrix[row2, j];
//				matrix[row2, j] = temp;
//			}
//		}

//		/// <summary>Combines two matrices from left to right 
//		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
//		/// <param name="left">The left matrix of the concatenation.</param>
//		/// <param name="right">The right matrix of the concatenation.</param>
//		/// <returns>The resulting matrix of the concatenation.</returns>
//		public static Matrix_2dArray ConcatenateRowWise(float[,] left, float[,] right)
//		{
//			if (left.GetLength(0) != right.GetLength(0))
//				throw new MatrixException("invalid row-wise concatenation !(left.Rows == right.Rows).");
//			Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), left.GetLength(1) + right.GetLength(1));
//			for (int i = 0; i < result.Rows; i++)
//				for (int j = 0; j < result.Columns; j++)
//				{
//					if (j < left.GetLength(1)) result[i, j] = left[i, j];
//					else result[i, j] = right[i, j - left.GetLength(1)];
//				}
//			return result;
//		}

//		/// <summary>Calculates the determinent of a square matrix.</summary>
//		/// <param name="matrix">The matrix to calculate the determinent of.</param>
//		/// <returns>The determinent of the matrix.</returns>
//		public static float Determinent(float[,] matrix)
//		{
//			if (!(matrix.GetLength(0) == matrix.GetLength(1)))
//				throw new MatrixException("invalid determinent !(matrix.IsSquare).");
//			float det = 1.0f;
//			try
//			{
//				Matrix_2dArray rref = Matrix_2dArray.Clone(matrix);
//				for (int i = 0; i < matrix.GetLength(0); i++)
//				{
//					if (rref[i, i] == 0)
//						for (int j = i + 1; j < rref.Rows; j++)
//							if (rref[j, i] != 0)
//							{
//								Matrix_2dArray.SwapRows(rref, i, j);
//								det *= -1;
//							}
//					det *= rref[i, i];
//					Matrix_2dArray.RowMultiplication(rref, i, 1 / rref[i, i]);
//					for (int j = i + 1; j < rref.Rows; j++)
//						Matrix_2dArray.RowAddition(rref, j, i, -rref[j, i]);
//					for (int j = i - 1; j >= 0; j--)
//						Matrix_2dArray.RowAddition(rref, j, i, -rref[j, i]);
//				}
//				return det;
//			}
//			catch (Exception) { throw new MatrixException("determinent computation failed."); }
//		}

//		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
//		/// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
//		/// <returns>The echelon of the matrix (aka REF).</returns>
//		public static Matrix_2dArray Echelon(float[,] matrix)
//		{
//			try
//			{
//				Matrix_2dArray result = Matrix_2dArray.Clone(matrix);
//				for (int i = 0; i < matrix.GetLength(0); i++)
//				{
//					if (result[i, i] == 0)
//						for (int j = i + 1; j < result.Rows; j++)
//							if (result[j, i] != 0)
//								Matrix_2dArray.SwapRows(result, i, j);
//					if (result[i, i] == 0)
//						continue;
//					if (result[i, i] != 1)
//						for (int j = i + 1; j < result.Rows; j++)
//							if (result[j, i] == 1)
//								Matrix_2dArray.SwapRows(result, i, j);
//					Matrix_2dArray.RowMultiplication(result, i, 1 / result[i, i]);
//					for (int j = i + 1; j < result.Rows; j++)
//						Matrix_2dArray.RowAddition(result, j, i, -result[j, i]);
//				}
//				return result;
//			}
//			catch { throw new MatrixException("echelon computation failed."); }
//		}

//		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
//		/// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
//		/// <returns>The reduced echelon of the matrix (aka RREF).</returns>
//		public static Matrix_2dArray ReducedEchelon(float[,] matrix)
//		{
//			try
//			{
//				Matrix_2dArray result = Matrix_2dArray.Clone(matrix);
//				for (int i = 0; i < matrix.GetLength(0); i++)
//				{
//					if (result[i, i] == 0)
//						for (int j = i + 1; j < result.Rows; j++)
//							if (result[j, i] != 0)
//								Matrix_2dArray.SwapRows(result, i, j);
//					if (result[i, i] == 0) continue;
//					if (result[i, i] != 1)
//						for (int j = i + 1; j < result.Rows; j++)
//							if (result[j, i] == 1)
//								Matrix_2dArray.SwapRows(result, i, j);
//					Matrix_2dArray.RowMultiplication(result, i, 1 / result[i, i]);
//					for (int j = i + 1; j < result.Rows; j++)
//						Matrix_2dArray.RowAddition(result, j, i, -result[j, i]);
//					for (int j = i - 1; j >= 0; j--)
//						Matrix_2dArray.RowAddition(result, j, i, -result[j, i]);
//				}
//				return result;
//			}
//			catch { throw new MatrixException("reduced echelon calculation failed."); }
//		}

//		/// <summary>Calculates the inverse of a matrix.</summary>
//		/// <param name="matrix">The matrix to calculate the inverse of.</param>
//		/// <returns>The inverse of the matrix.</returns>
//		public static Matrix_2dArray Inverse(float[,] matrix)
//		{
//			if (Matrix_2dArray.Determinent(matrix) == 0)
//				throw new MatrixException("inverse calculation failed.");
//			try
//			{
//				Matrix_2dArray identity = Matrix_2dArray.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
//				Matrix_2dArray rref = Matrix_2dArray.Clone(matrix);
//				for (int i = 0; i < matrix.GetLength(0); i++)
//				{
//					if (rref[i, i] == 0)
//						for (int j = i + 1; j < rref.Rows; j++)
//							if (rref[j, i] != 0)
//							{
//								Matrix_2dArray.SwapRows(rref, i, j);
//								Matrix_2dArray.SwapRows(identity, i, j);
//							}
//					Matrix_2dArray.RowMultiplication(identity, i, 1 / rref[i, i]);
//					Matrix_2dArray.RowMultiplication(rref, i, 1 / rref[i, i]);
//					for (int j = i + 1; j < rref.Rows; j++)
//					{
//						Matrix_2dArray.RowAddition(identity, j, i, -rref[j, i]);
//						Matrix_2dArray.RowAddition(rref, j, i, -rref[j, i]);
//					}
//					for (int j = i - 1; j >= 0; j--)
//					{
//						Matrix_2dArray.RowAddition(identity, j, i, -rref[j, i]);
//						Matrix_2dArray.RowAddition(rref, j, i, -rref[j, i]);
//					}
//				}
//				return identity;
//			}
//			catch { throw new MatrixException("inverse calculation failed."); }
//		}

//		/// <summary>Calculates the adjoint of a matrix.</summary>
//		/// <param name="matrix">The matrix to calculate the adjoint of.</param>
//		/// <returns>The adjoint of the matrix.</returns>
//		public static Matrix_2dArray Adjoint(float[,] matrix)
//		{
//			if (!(matrix.GetLength(0) == matrix.GetLength(1)))
//				throw new MatrixException("Adjoint of a non-square matrix does not exists");
//			Matrix_2dArray AdjointMatrix = new Matrix_2dArray(matrix.GetLength(0), matrix.GetLength(1));
//			for (int i = 0; i < matrix.GetLength(0); i++)
//				for (int j = 0; j < matrix.GetLength(1); j++)
//					if ((i + j) % 2 == 0)
//						AdjointMatrix[i, j] = Matrix_2dArray.Determinent(Matrix_2dArray.Minor(matrix, i, j));
//					else
//						AdjointMatrix[i, j] = -Matrix_2dArray.Determinent(Matrix_2dArray.Minor(matrix, i, j));
//			return Matrix_2dArray.Transpose(AdjointMatrix);
//		}

//		/// <summary>Returns the transpose of a matrix.</summary>
//		/// <param name="matrix">The matrix to transpose.</param>
//		/// <returns>The transpose of the matrix.</returns>
//		public static Matrix_2dArray Transpose(float[,] matrix)
//		{
//			Matrix_2dArray transpose = new Matrix_2dArray(matrix.GetLength(1), matrix.GetLength(0));
//			for (int i = 0; i < transpose.Rows; i++)
//				for (int j = 0; j < transpose.Columns; j++)
//					transpose[i, j] = matrix[j, i];
//			return transpose;
//		}

//		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
//		/// <param name="matrix">The matrix to decompose.</param>
//		/// <param name="Lower">The computed lower triangular matrix.</param>
//		/// <param name="Upper">The computed upper triangular matrix.</param>
//		public static void DecomposeLU(float[,] matrix, out Matrix_2dArray Lower, out Matrix_2dArray Upper)
//		{
//			if (!(matrix.GetLength(0) == matrix.GetLength(1)))
//				throw new MatrixException("The matrix is not square!");
//			Lower = Matrix_2dArray.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
//			Upper = Matrix_2dArray.Clone(matrix);
//			int[] permutation = new int[matrix.GetLength(0)];
//			for (int i = 0; i < matrix.GetLength(0); i++) permutation[i] = i;
//			float p = 0, pom2, detOfP = 1;
//			int k0 = 0, pom1 = 0;
//			for (int k = 0; k < matrix.GetLength(1) - 1; k++)
//			{
//				p = 0;
//				for (int i = k; i < matrix.GetLength(0); i++)
//					if (Calc.Abs(Upper[i, k]) > p)
//					{
//						p = Calc.Abs(Upper[i, k]);
//						k0 = i;
//					}
//				if (p == 0)
//					throw new MatrixException("The matrix is singular!");
//				pom1 = permutation[k];
//				permutation[k] = permutation[k0];
//				permutation[k0] = pom1;
//				for (int i = 0; i < k; i++)
//				{
//					pom2 = Lower[k, i];
//					Lower[k, i] = Lower[k0, i];
//					Lower[k0, i] = pom2;
//				}
//				if (k != k0)
//					detOfP *= -1;
//				for (int i = 0; i < matrix.GetLength(1); i++)
//				{
//					pom2 = Upper[k, i];
//					Upper[k, i] = Upper[k0, i];
//					Upper[k0, i] = pom2;
//				}
//				for (int i = k + 1; i < matrix.GetLength(0); i++)
//				{
//					Lower[i, k] = Upper[i, k] / Upper[k, k];
//					for (int j = k; j < matrix.GetLength(1); j++)
//						Upper[i, j] = Upper[i, j] - Lower[i, k] * Upper[k, j];
//				}
//			}
//		}

//		/// <summary>Creates a copy of a matrix.</summary>
//		/// <param name="matrix">The matrix to copy.</param>
//		/// <returns>A copy of the matrix.</returns>
//		public static Matrix_2dArray Clone(float[,] matrix)
//		{
//			Matrix_2dArray result = new Matrix_2dArray(matrix.GetLength(0), matrix.GetLength(1));
//			for (int i = 0; i < matrix.GetLength(0); i++)
//				for (int j = 0; j < matrix.GetLength(1); j++)
//					result[i, j] = matrix[i, j];
//			return result;
//		}

//		/// <summary>Does a value equality check.</summary>
//		/// <param name="left">The first matrix to check for equality.</param>
//		/// <param name="right">The second matrix to check for equality.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public static bool EqualsByValue(float[,] left, float[,] right)
//		{
//			if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
//				return false;
//			for (int i = 0; i < left.GetLength(0); i++)
//				for (int j = 0; j < left.GetLength(1); j++)
//					if (left[i, j] != right[i, j])
//						return false;
//			return true;
//		}

//		/// <summary>Does a value equality check with leniency.</summary>
//		/// <param name="left">The first matrix to check for equality.</param>
//		/// <param name="right">The second matrix to check for equality.</param>
//		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
//		/// <returns>True if values are equal, false if not.</returns>
//		public static bool EqualsByValue(float[,] left, float[,] right, float leniency)
//		{
//			if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
//				return false;
//			for (int i = 0; i < left.GetLength(0); i++)
//				for (int j = 0; j < left.GetLength(1); j++)
//					if (Calc.Abs(left[i, j] - right[i, j]) > leniency)
//						return false;
//			return true;
//		}

//		/// <summary>Checks if two matrices are equal by reverences.</summary>
//		/// <param name="left">The left matric of the equality check.</param>
//		/// <param name="right">The right matrix of the equality check.</param>
//		/// <returns>True if the references are equal, false if not.</returns>
//		public static bool EqualsByReference(float[,] left, float[,] right)
//		{
//			return left.Equals(right);
//		}

//		/// <summary>Prints out a string representation of this matrix.</summary>
//		/// <returns>A string representing this matrix.</returns>
//		public override string ToString()
//		{
//			return base.ToString();
//			//StringBuilder matrix = new StringBuilder();
//			//for (int i = 0; i < Rows; i++)
//			//{
//			//  for (int j = 0; j < Columns; j++)
//			//    matrix.Append(String.Format("{0:0.##}\t", _matrix[i, j]));
//			//  matrix.Append("\n");
//			//}
//			//return matrix.ToString();
//		}

//		/// <summary>Computes a hash code from the values of this matrix.</summary>
//		/// <returns>A hash code for the matrix.</returns>
//		public override int GetHashCode()
//		{
//			// return base.GetHashCode();
//			int hash = _matrix[0, 0].GetHashCode();
//			for (int i = 0; i < Rows; i++)
//				for (int j = 0; j < Columns; j++)
//					hash = hash ^ _matrix[i, j].GetHashCode();
//			return hash;
//		}

//		/// <summary>Does an equality check by reference.</summary>
//		/// <param name="obj">The object to compare to.</param>
//		/// <returns>True if the references are equal, false if not.</returns>
//		public override bool Equals(object obj)
//		{
//			return base.Equals(obj) || _matrix.Equals(obj);
//		}

//		private class MatrixException : Exception
//		{
//			public MatrixException(string Message) : base(Message) { }
//		}

//		#region Alternate Compututation Methods

//		//public static float Determinent(Matrix matrix)
//		//{
//		//  if (!matrix.IsSquare)
//		//    throw new MatrixException("invalid determinent !(matrix.IsSquare).");
//		//  return DeterminentRecursive(matrix);
//		//}
//		//private static float DeterminentRecursive(Matrix matrix)
//		//{
//		//  if (matrix.Rows == 1)
//		//    return matrix[0, 0];
//		//  float det = 0.0f;
//		//  for (int j = 0; j < matrix.Columns; j++)
//		//    det += (matrix[0, j] * DeterminentRecursive(Matrix.Minor(matrix, 0, j)) * (int)System.Math.Pow(-1, 0 + j));
//		//  return det;
//		//}

//		//public static Matrix Inverse(Matrix matrix)
//		//{
//		//  float determinent = Matrix.Determinent(matrix);
//		//  if (determinent == 0)
//		//    throw new MatrixException("inverse calculation failed.");
//		//  return Matrix.Adjoint(matrix) / determinent;
//		//}

//		#endregion
//	}

//	// This is the original version of matrices that I used in my engine. It only supported
//	// 3x3 matrices, which obviously needed improvement.
//	#region Matrix-OLD
//	///// <summary>An optimized matrix class for 3x3 tranfromation matrices only.</summary>
//	//public struct Matrix3x3
//	//{
//	//  private float
//	//    _r0c0, _r0c1, _r0c2,
//	//    _r1c0, _r1c1, _r1c2,
//	//    _r2c0, _r2c1, _r2c2;

//	//  public float this[int row, int column]
//	//  {
//	//    get
//	//    {
//	//      switch (row)
//	//      {
//	//        case 0:
//	//          switch (column)
//	//          {
//	//            case 0: return _r0c0;
//	//            case 1: return _r0c1;
//	//            case 2: return _r0c2;
//	//          }
//	//          break;
//	//        case 1:
//	//          switch (column)
//	//          {
//	//            case 0: return _r1c0;
//	//            case 1: return _r1c1;
//	//            case 2: return _r1c2;
//	//          }
//	//          break;
//	//        case 2:
//	//          switch (column)
//	//          {
//	//            case 0: return _r2c0;
//	//            case 1: return _r2c1;
//	//            case 2: return _r2c2;
//	//          }
//	//          break;
//	//      }
//	//      throw new MatrixException("index out of range.");
//	//    }
//	//    set
//	//    {
//	//      switch (row)
//	//      {
//	//        case 0:
//	//          switch (column)
//	//          {
//	//            case 0: _r0c0 = value; return;
//	//            case 1: _r0c1 = value; return;
//	//            case 2: _r0c2 = value; return;
//	//          }
//	//          break;
//	//        case 1:
//	//          switch (column)
//	//          {
//	//            case 0: _r1c0 = value; return;
//	//            case 1: _r1c1 = value; return;
//	//            case 2: _r1c2 = value; return;
//	//          }
//	//          break;
//	//        case 2:
//	//          switch (column)
//	//          {
//	//            case 0: _r2c0 = value; return;
//	//            case 1: _r2c1 = value; return;
//	//            case 2: _r2c2 = value; return;
//	//          }
//	//          break;
//	//      }
//	//      throw new MatrixException("index out of range.");
//	//    }
//	//  }

//	//  public Matrix3x3(
//	//    float r0c0, float r0c1, float r0c2,
//	//    float r1c0, float r1c1, float r1c2,
//	//    float r2c0, float r2c1, float r2c2)
//	//  {
//	//    _r0c0 = r0c0; _r0c1 = r0c1; _r0c2 = r0c2;
//	//    _r1c0 = r1c0; _r1c1 = r1c1; _r1c2 = r1c2;
//	//    _r2c0 = r2c0; _r2c1 = r2c1; _r2c2 = r2c2;
//	//  }

//	//  public Matrix3x3(float[,] floatArray)
//	//  {
//	//    if (floatArray == null)
//	//      throw new MatrixException("Attempting to create a matrix with an null float[,].");
//	//    else if (floatArray.GetLength(0) != 3)
//	//      throw new MatrixException("Attempting to create a matrix with an invalid sized float[,].");
//	//    else if (floatArray.GetLength(1) != 3)
//	//      throw new MatrixException("Attempting to create a matrix with an invalid sized float[,].");
//	//    _r0c0 = floatArray[0, 0]; _r0c1 = floatArray[0, 1]; _r0c2 = floatArray[0, 2];
//	//    _r1c0 = floatArray[1, 0]; _r1c1 = floatArray[1, 1]; _r1c2 = floatArray[1, 2];
//	//    _r2c0 = floatArray[2, 0]; _r2c1 = floatArray[2, 1]; _r2c2 = floatArray[2, 2];
//	//  }

//	//  public static Matrix3x3 FactoryZero = new Matrix3x3(0, 0, 0, 0, 0, 0, 0, 0, 0);
//	//  public static Matrix3x3 FactoryIdentity = new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);

//	//  /// <param name="angle">Angle of rotation in radians.</param>
//	//  public static Matrix3x3 FactoryRotationX(float angle)
//	//  {
//	//    float cos = Calc.Cos(angle);
//	//    float sin = Calc.Sin(angle);
//	//    return new Matrix3x3(1, 0, 0, 0, cos, sin, 0, -sin, cos);
//	//  }

//	//  /// <param name="angle">Angle of rotation in radians.</param>
//	//  public static Matrix3x3 FactoryRotationY(float angle)
//	//  {
//	//    float cos = Calc.Cos(angle);
//	//    float sin = Calc.Sin(angle);
//	//    return new Matrix3x3(cos, 0, -sin, 0, 1, 0, sin, 0, cos);
//	//  }

//	//  /// <param name="angle">Angle of rotation in radians.</param>
//	//  public static Matrix3x3 FactoryRotationZ(float angle)
//	//  {
//	//    float cos = Calc.Cos(angle);
//	//    float sin = Calc.Sin(angle);
//	//    return new Matrix3x3(cos, -sin, 0, sin, cos, 0, 0, 0, 1);
//	//  }

//	//  /// <param name="angleX">Angle about the X-axis in radians.</param>
//	//  /// <param name="angleY">Angle about the Y-axis in radians.</param>
//	//  /// <param name="angleZ">Angle about the Z-axis in radians.</param>
//	//  public static Matrix3x3 FactoryRotationXthenYthenZ(float angleX, float angleY, float angleZ)
//	//  {
//	//    float
//	//      xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
//	//      yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
//	//      zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
//	//    return new Matrix3x3(
//	//      yCos * zCos, -yCos * zSin, ySin,
//	//      xCos * zSin + xSin * ySin * zCos, xCos * zCos + xSin * ySin * zSin, -xSin * yCos,
//	//      xSin * zSin - xCos * ySin * zCos, xSin * zCos + xCos * ySin * zSin, xCos * yCos);
//	//  }

//	//  /// <param name="angleX">Angle about the X-axis in radians.</param>
//	//  /// <param name="angleY">Angle about the Y-axis in radians.</param>
//	//  /// <param name="angleZ">Angle about the Z-axis in radians.</param>
//	//  public static Matrix3x3 FactoryRotationZthenYthenX(float angleX, float angleY, float angleZ)
//	//  {
//	//    float
//	//      xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
//	//      yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
//	//      zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
//	//    return new Matrix3x3(
//	//      yCos * zCos, zCos * xSin * ySin - xCos * zSin, xCos * zCos * ySin + xSin * zSin,
//	//      yCos * zSin, xCos * zCos + xSin * ySin * zSin, -zCos * xSin + xCos * ySin * zSin,
//	//      -ySin, yCos * xSin, xCos * yCos);
//	//  }

//	//  /// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
//	//  /// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
//	//  /// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
//	//  /// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
//	//  /// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
//	//  /// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
//	//  public static Matrix3x3 FactoryShear(
//	//    float shearXbyY, float shearXbyZ, float shearYbyX,
//	//    float shearYbyZ, float shearZbyX, float shearZbyY)
//	//  {
//	//    return new Matrix3x3(
//	//      1, shearYbyX, shearZbyX,
//	//      shearXbyY, 1, shearYbyZ,
//	//      shearXbyZ, shearYbyZ, 1);
//	//  }

//	//  public static Matrix3x3 operator +(Matrix3x3 left, Matrix3x3 right) { return left.Add(right); }
//	//  public static Matrix3x3 operator -(Matrix3x3 left, Matrix3x3 right) { return left.Add(-right); }
//	//  public static Matrix3x3 operator -(Matrix3x3 matrix) { return matrix.Negate(); }
//	//  public static Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right) { return left.Multiply(right); }
//	//  public static Vector operator *(Matrix3x3 matrix, Vector vector) { return matrix.Multiply(vector); }
//	//  public static Matrix3x3 operator *(Matrix3x3 matrix, float scalar) { return matrix.Multiply(scalar); }
//	//  public static Matrix3x3 operator /(Matrix3x3 matrix, float scalar) { return matrix.Divide(scalar); }
//	//  public static Matrix3x3 operator ^(Matrix3x3 matrix, int power) { return matrix.Power(power); }
//	//  public static implicit operator Matrix(Matrix3x3 matrix) { return Matrix3x3.ToMatrix(matrix); }
//	//  public static implicit operator float[,](Matrix3x3 matrix) { return Matrix3x3.ToFloats(matrix); }

//	//  public float Determinant
//	//  {
//	//    get
//	//    {
//	//      return
//	//        _r0c0 * _r1c1 * _r2c2 -
//	//        _r0c0 * _r1c2 * _r2c1 -
//	//        _r0c1 * _r1c0 * _r2c2 +
//	//        _r0c2 * _r1c0 * _r2c1 +
//	//        _r0c1 * _r1c2 * _r2c0 -
//	//        _r0c2 * _r1c1 * _r2c0;
//	//    }
//	//  }

//	//  public bool EqualsApproximation(Matrix3x3 matrix, float tolerance)
//	//  {
//	//    return
//	//      Calc.Abs(_r0c0 - matrix._r0c0) <= tolerance &&
//	//      Calc.Abs(_r0c1 - matrix._r0c1) <= tolerance &&
//	//      Calc.Abs(_r0c2 - matrix._r0c2) <= tolerance &&
//	//      Calc.Abs(_r1c0 - matrix._r1c0) <= tolerance &&
//	//      Calc.Abs(_r1c1 - matrix._r1c1) <= tolerance &&
//	//      Calc.Abs(_r1c2 - matrix._r1c2) <= tolerance &&
//	//      Calc.Abs(_r2c0 - matrix._r2c0) <= tolerance &&
//	//      Calc.Abs(_r2c1 - matrix._r2c1) <= tolerance &&
//	//      Calc.Abs(_r2c2 - matrix._r2c2) <= tolerance;
//	//  }

//	//  public Matrix3x3 Negate()
//	//  {
//	//    return new Matrix3x3(
//	//      -_r0c0, -_r0c1, -_r0c2,
//	//      -_r1c0, -_r1c1, -_r1c2,
//	//      -_r2c0, -_r2c1, -_r2c2);
//	//  }

//	//  public Matrix3x3 Add(Matrix3x3 matrix)
//	//  {
//	//    return new Matrix3x3(
//	//      _r0c0 + matrix._r0c0, _r0c1 + matrix._r0c1, _r0c2 + matrix._r0c2,
//	//      _r1c0 + matrix._r1c0, _r1c1 + matrix._r1c1, _r1c2 + matrix._r1c2,
//	//      _r2c0 + matrix._r2c0, _r2c1 + matrix._r2c1, _r2c2 + matrix._r2c2);
//	//  }

//	//  public Matrix3x3 Multiply(Matrix3x3 matrix)
//	//  {
//	//    return new Matrix3x3(
//	//      matrix._r0c0 * _r0c0 + matrix._r0c1 * _r1c0 + matrix._r0c2 * _r2c0,
//	//      matrix._r0c0 * _r0c1 + matrix._r0c1 * _r1c1 + matrix._r0c2 * _r2c1,
//	//      matrix._r0c0 * _r0c2 + matrix._r0c1 * _r1c2 + matrix._r0c2 * _r2c2,
//	//      matrix._r1c0 * _r0c0 + matrix._r1c1 * _r1c0 + matrix._r1c2 * _r2c0,
//	//      matrix._r1c0 * _r0c1 + matrix._r1c1 * _r1c1 + matrix._r1c2 * _r2c1,
//	//      matrix._r1c0 * _r0c2 + matrix._r1c1 * _r1c2 + matrix._r1c2 * _r2c2,
//	//      matrix._r2c0 * _r0c0 + matrix._r2c1 * _r1c0 + matrix._r2c2 * _r2c0,
//	//      matrix._r2c0 * _r0c1 + matrix._r2c1 * _r1c1 + matrix._r2c2 * _r2c1,
//	//      matrix._r2c0 * _r0c2 + matrix._r2c1 * _r1c2 + matrix._r2c2 * _r2c2);
//	//  }

//	//  public Vector Multiply(Vector vector)
//	//  {
//	//    return new Vector(
//	//      _r0c0 * vector.X + _r0c1 * vector.Y + _r0c2 * vector.Z,
//	//      _r1c0 * vector.X + _r1c1 * vector.Y + _r1c2 * vector.Z,
//	//      _r2c0 * vector.X + _r2c1 * vector.Y + _r2c2 * vector.Z);
//	//  }

//	//  public Matrix3x3 Multiply(float scalar)
//	//  {
//	//    return new Matrix3x3(
//	//      scalar * _r0c0, scalar * _r0c1, scalar * _r0c2,
//	//      scalar * _r1c0, scalar * _r1c1, scalar * _r1c2,
//	//      scalar * _r2c0, scalar * _r2c1, scalar * _r2c2);
//	//  }

//	//  public Matrix3x3 Divide(float scalar)
//	//  {
//	//    return new Matrix3x3(
//	//      _r0c0 / scalar, _r0c1 / scalar, _r0c2 / scalar,
//	//      _r1c0 / scalar, _r1c1 / scalar, _r1c2 / scalar,
//	//      _r2c0 / scalar, _r2c1 / scalar, _r2c2 / scalar);
//	//  }

//	//  public Matrix3x3 Power(int power)
//	//  {
//	//    if (power < 0)
//	//      throw new MatrixException("Attempting to raise a matrix by a power less than zero. (can't do dat)");
//	//    else if (power == 0)
//	//      return FactoryIdentity;
//	//    else
//	//    {
//	//      Matrix3x3 result = Clone();
//	//      for (int i = 1; i < power; i++)
//	//        result = result * result;
//	//      return result;
//	//    }
//	//  }

//	//  public Matrix3x3 Transpose()
//	//  {
//	//    return new Matrix3x3(
//	//      _r0c0, _r1c0, _r2c0,
//	//      _r0c1, _r1c1, _r2c1,
//	//      _r0c2, _r1c1, _r2c2);
//	//  }

//	//  public Quaternion ToQuaternion()
//	//  {
//	//    float qX = (_r0c0 + _r1c1 + _r2c2 + 1.0f) / 4.0f;
//	//    float qY = (_r0c0 - _r1c1 - _r2c2 + 1.0f) / 4.0f;
//	//    float qZ = (-_r0c0 + _r1c1 - _r2c2 + 1.0f) / 4.0f;
//	//    float qW = (-_r0c0 - _r1c1 + _r2c2 + 1.0f) / 4.0f;

//	//    if (qX < 0.0f) qX = 0.0f;
//	//    if (qY < 0.0f) qY = 0.0f;
//	//    if (qZ < 0.0f) qZ = 0.0f;
//	//    if (qW < 0.0f) qW = 0.0f;

//	//    qX = Calc.SquareRoot(qX);
//	//    qY = Calc.SquareRoot(qY);
//	//    qZ = Calc.SquareRoot(qZ);
//	//    qW = Calc.SquareRoot(qW);

//	//    if (qX >= qY && qX >= qZ && qX >= qW)
//	//    {
//	//      qX *= +1.0f;
//	//      qY *= Calc.Sin(_r2c1 - _r1c2);
//	//      qZ *= Calc.Sin(_r0c2 - _r2c0);
//	//      qW *= Calc.Sin(_r1c0 - _r0c1);
//	//    }
//	//    else if (qY >= qX && qY >= qZ && qY >= qW)
//	//    {
//	//      qX *= Calc.Sin(_r2c1 - _r1c2);
//	//      qY *= +1.0f;
//	//      qZ *= Calc.Sin(_r1c0 + _r0c1);
//	//      qW *= Calc.Sin(_r0c2 + _r2c0);
//	//    }
//	//    else if (qZ >= qX && qZ >= qY && qZ >= qW)
//	//    {
//	//      qX *= Calc.Sin(_r0c2 - _r2c0);
//	//      qY *= Calc.Sin(_r1c0 + _r0c1);
//	//      qZ *= +1.0f;
//	//      qW *= Calc.Sin(_r2c1 + _r1c2);
//	//    }
//	//    else if (qW >= qX && qW >= qY && qW >= qZ)
//	//    {
//	//      qX *= Calc.Sin(_r1c0 - _r0c1);
//	//      qY *= Calc.Sin(_r2c0 + _r0c2);
//	//      qZ *= Calc.Sin(_r2c1 + _r1c2);
//	//      qW *= +1.0f;
//	//    }
//	//    else
//	//      throw new MatrixException("There is a glitch in my my matrix to quaternion function. Sorry..");

//	//    float length = Calc.SquareRoot(qX * qX + qY * qY + qZ * qZ + qW * qW);

//	//    return new Quaternion(
//	//      qX /= length,
//	//      qY /= length,
//	//      qZ /= length,
//	//      qW /= length);
//	//  }

//	//  public Matrix3x3 Clone()
//	//  {
//	//    return new Matrix3x3(
//	//      _r0c0, _r0c1, _r0c2,
//	//      _r1c0, _r1c1, _r1c2,
//	//      _r2c0, _r2c1, _r2c2);
//	//  }

//	//  public bool Equals(Matrix3x3 matrix)
//	//  {
//	//    return
//	//      _r0c0 == matrix._r0c0 && _r0c1 == matrix._r0c1 && _r0c2 == matrix._r0c2 &&
//	//      _r1c0 == matrix._r1c0 && _r1c1 == matrix._r1c1 && _r1c2 == matrix._r1c2 &&
//	//      _r2c0 == matrix._r2c0 && _r2c1 == matrix._r2c1 && _r2c2 == matrix._r2c2;
//	//  }

//	//  public static Matrix ToMatrix(Matrix3x3 matrix)
//	//  {
//	//    Matrix result = new Matrix(3, 3);
//	//    result[0, 0] = matrix._r0c0; result[0, 1] = matrix._r0c1; result[0, 2] = matrix._r0c2;
//	//    result[1, 0] = matrix._r1c0; result[1, 1] = matrix._r1c1; result[1, 2] = matrix._r1c2;
//	//    result[1, 0] = matrix._r2c0; result[2, 1] = matrix._r2c1; result[2, 2] = matrix._r2c2;
//	//    return result;
//	//  }

//	//  public static float[,] ToFloats(Matrix3x3 matrix)
//	//  {
//	//    return new float[,]
//	//      { { matrix[0, 0], matrix[0, 1], matrix[0, 2] },
//	//      { matrix[1, 0], matrix[1, 1], matrix[1, 2] },
//	//      { matrix[2, 0], matrix[2, 1], matrix[2, 2] } };
//	//  }

//	//  public override int GetHashCode()
//	//  {
//	//    return
//	//      _r0c0.GetHashCode() ^ _r0c1.GetHashCode() ^ _r0c2.GetHashCode() ^
//	//      _r1c0.GetHashCode() ^ _r1c1.GetHashCode() ^ _r1c2.GetHashCode() ^
//	//      _r2c0.GetHashCode() ^ _r2c1.GetHashCode() ^ _r2c2.GetHashCode();
//	//  }

//	//  public override string ToString()
//	//  {
//	//    return base.ToString();
//	//    //return
//	//    //  _r0c0 + " " + _r0c1 + " " + _r0c2 + "\n" +
//	//    //  _r1c0 + " " + _r1c1 + " " + _r1c2 + "\n" +
//	//    //  _r2c0 + " " + _r2c1 + " " + _r2c2 + "\n";
//	//  }

//	//  private class MatrixException : Exception
//	//  {
//	//    public MatrixException(string message) : base(message) { }
//	//  }
//	//}
//	#endregion
//}

#endregion

#endregion