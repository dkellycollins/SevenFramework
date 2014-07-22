using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven.Mathematics
{
	/// <summary>Supplies linear algebra mathematics for generic types.</summary>
	/// <typeparam name="T">The type this linear algebra library can perform on.</typeparam>
	public interface LinearAlgebra<T>
	{
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
		public delegate T[,] _Negate<T>(T[,] matrix);
		public delegate T[,] _Add<T>(T[,] left, T[,] right);
		public delegate T[,] _Multiply<T>(T[,] left, T[,] right);
		public delegate T[,] _Multiply_scalar<T>(T[,] matrix, T right);
		public delegate T[,] _Divide<T>(T[,] left, T right);
		public delegate T[,] _Minor<T>(T[,] matrix, int row, int column);
		public delegate T[,] _ConcatenateRowWise<T>(T[,] left, T[,] right);
		public delegate T _Determinent<T>(T[,] matrix);
		public delegate T[,] _Echelon<T>(T[,] matrix);
		public delegate T[,] _ReducedEchelon<T>(T[,] matrix);
		public delegate T[,] _Inverse<T>(T[,] matrix);
		public delegate T[,] _Adjoint<T>(T[,] matrix);
		public delegate T[,] _Transpose<T>(T[,] matrix);
		public delegate T[,] _Clone<T>(T[,] matrix);

		// COPY THESE DEFINITIONS INTO CLASSES NEEDING ARITHMETIC
		//private static Arithmetic.Negate<T> _negate = Arithmetic.Get<T>().Negate;
		//private static Arithmetic.Add<T> _add = Arithmetic.Get<T>().Add;
		//private static Arithmetic.Subtract<T> _subtract = Arithmetic.Get<T>().Subtract;
		//private static Arithmetic.Multiply<T> _multiply = Arithmetic.Get<T>().Multiply;
		//private static Arithmetic.Divide<T> _divide = Arithmetic.Get<T>().Divide;
		//private static Arithmetic.Power<T> _power = Arithmetic.Get<T>().Power;

		public static LinearAlgebra<int> _int
		{ get { return (LinearAlgebra<int>)LinearAlgebra_int.Get; } }
		public static LinearAlgebra<int> GetLinearAlgebra(this int integer)
		{ return (LinearAlgebra<int>)LinearAlgebra_int.Get; }

		public static LinearAlgebra<double> _double
		{ get { return (LinearAlgebra<double>)LinearAlgebra_double.Get; } }
		public static LinearAlgebra<double> GetLinearAlgebra(this double integer)
		{ return (LinearAlgebra<double>)LinearAlgebra_double.Get; }

		public static LinearAlgebra<float> _float
		{ get { return (LinearAlgebra<float>)LinearAlgebra_float.Get; } }
		public static LinearAlgebra<float> GetLinearAlgebra(this float integer)
		{ return (LinearAlgebra<float>)LinearAlgebra_float.Get; }

		public static LinearAlgebra<long> _long
		{ get { return (LinearAlgebra<long>)LinearAlgebra_long.Get; } }
		public static LinearAlgebra<long> GetLinearAlgebra(this long integer)
		{ return (LinearAlgebra<long>)LinearAlgebra_long.Get; }

		public static LinearAlgebra<decimal> _decimal
		{ get { return (LinearAlgebra<decimal>)LinearAlgebra_decimal.Get; } }
		public static LinearAlgebra<decimal> GetLinearAlgebra(this decimal integer)
		{ return (LinearAlgebra<decimal>)LinearAlgebra_decimal.Get; }

		public static Seven.Structures.Map<object, System.Type> _linearAlgebras =
			new Seven.Structures.Map_Linked<object, System.Type>(
				(System.Type left, System.Type right) => { return left == right; },
				(System.Type type) => { return type.GetHashCode(); })
				{
					{ typeof(int), LinearAlgebra_int.Get },
					{ typeof(double), LinearAlgebra_double.Get },
					{ typeof(float), LinearAlgebra_float.Get },
					{ typeof(decimal), LinearAlgebra_decimal.Get },
					{ typeof(long), LinearAlgebra_long.Get }
				};

		public static LinearAlgebra<T> Get<T>()
		{
			try { return (LinearAlgebra<T>)_linearAlgebras[typeof(T)]; }
			catch { throw new Error("LinearAlgebra does not yet exist for " + typeof(T).ToString()); }
		}



		#region Implementations

		#region decimal

		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static decimal[,] FactoryIdentity_decimal(int rows, int columns)
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
				return LinearAlgebra.FactoryIdentity_decimal(matrix.GetLength(0), matrix.GetLength(1));
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
				decimal[,] identity = LinearAlgebra.FactoryIdentity_decimal(matrix.GetLength(0), matrix.GetLength(1));
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
			Lower = LinearAlgebra.FactoryIdentity_decimal(matrix.GetLength(0), matrix.GetLength(1));
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

		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static double[,] FactoryIdentity_double(int rows, int columns)
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
				return LinearAlgebra.FactoryIdentity_double(matrix.GetLength(0), matrix.GetLength(1));
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
				double[,] identity = LinearAlgebra.FactoryIdentity_double(matrix.GetLength(0), matrix.GetLength(1));
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
			Lower = LinearAlgebra.FactoryIdentity_double(matrix.GetLength(0), matrix.GetLength(1));
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

		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static float[,] FactoryIdentity_float(int rows, int columns)
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
				return LinearAlgebra.FactoryIdentity_float(matrix.GetLength(0), matrix.GetLength(1));
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
				float[,] identity = LinearAlgebra.FactoryIdentity_float(matrix.GetLength(0), matrix.GetLength(1));
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
			Lower = LinearAlgebra.FactoryIdentity_float(matrix.GetLength(0), matrix.GetLength(1));
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

		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static long[,] FactoryIdentity_long(int rows, int columns)
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
				return LinearAlgebra.FactoryIdentity_long(matrix.GetLength(0), matrix.GetLength(1));
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
				long[,] identity = LinearAlgebra.FactoryIdentity_long(matrix.GetLength(0), matrix.GetLength(1));
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
			Lower = LinearAlgebra.FactoryIdentity_long(matrix.GetLength(0), matrix.GetLength(1));
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

		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static int[,] FactoryIdentity_int(int rows, int columns)
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
				return LinearAlgebra.FactoryIdentity_int(matrix.GetLength(0), matrix.GetLength(1));
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
				int[,] identity = LinearAlgebra.FactoryIdentity_int(matrix.GetLength(0), matrix.GetLength(1));
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
			Lower = LinearAlgebra.FactoryIdentity_int(matrix.GetLength(0), matrix.GetLength(1));
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

	public class LinearAlgebra_long : LinearAlgebra<long>
	{
		private LinearAlgebra_long() { _instance = this; }
		private static LinearAlgebra_long _instance;
		private static LinearAlgebra_long Instance
		{
			get
			{
				if (_instance == null)
					return _instance = new LinearAlgebra_long();
				else
					return _instance;
			}
		}

		/// <summary>Gets Arithmetic for "byte" types.</summary>
		public static LinearAlgebra_long Get { get { return Instance; } }

		/// <summary>Negates all the values in this matrix.</summary>
		public long[,] Negate(long[,] matrix) { return LinearAlgebra.Negate(matrix); }
		/// <summary>Does a standard matrix addition.</summary>
		public long[,] Add(long[,] left, long[,] right) { return LinearAlgebra.Add(left, right); }
		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
		public long[,] Multiply(long[,] left, long[,] right) { return LinearAlgebra.Multiply(left, right); }
		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
		public long[,] Multiply(long[,] matrix, long scalar) { return LinearAlgebra.Multiply(matrix, scalar); }
		/// <summary>Divides all the values in this matrix by a scalar.</summary>
		public long[,] Divide(long[,] left, long right) { return LinearAlgebra.Divide(left, right); }
		/// <summary>Gets the minor of a matrix.</summary>
		public long[,] Minor(long[,] matrix, int row, int column) { return LinearAlgebra.Minor(matrix, row, column); }
		/// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		public long[,] ConcatenateRowWise(long[,] left, long[,] right) { return LinearAlgebra.ConcatenateRowWise(left, right); }
		/// <summary>Computes the determinent if this matrix is square.</summary>
		public long Determinent(long[,] matrix) { return LinearAlgebra.Determinent(matrix); }
		/// <summary>Computes the echelon form of this matrix (aka REF).</summary>
		public long[,] Echelon(long[,] matrix) { return LinearAlgebra.Echelon(matrix); }
		/// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
		public long[,] ReducedEchelon(long[,] matrix) { return LinearAlgebra.ReducedEchelon(matrix); }
		/// <summary>Computes the inverse of this matrix.</summary>
		public long[,] Inverse(long[,] matrix) { return LinearAlgebra.Inverse(matrix); }
		/// <summary>Gets the adjoint of this matrix.</summary>
		public long[,] Adjoint(long[,] matrix) { return LinearAlgebra.Adjoint(matrix); }
		/// <summary>Transposes this matrix.</summary>
		public long[,] Transpose(long[,] matrix) { return LinearAlgebra.Transpose(matrix); }
		/// <summary>Copies this matrix.</summary>
		public long[,] Clone(long[,] matrix) { return LinearAlgebra.Clone(matrix); }
	}

	public class LinearAlgebra_int : LinearAlgebra<int>
	{
		private LinearAlgebra_int() { _instance = this; }
		private static LinearAlgebra_int _instance;
		private static LinearAlgebra_int Instance
		{
			get
			{
				if (_instance == null)
					return _instance = new LinearAlgebra_int();
				else
					return _instance;
			}
		}

		/// <summary>Gets Arithmetic for "byte" types.</summary>
		public static LinearAlgebra_int Get { get { return Instance; } }

		/// <summary>Negates all the values in this matrix.</summary>
		public int[,] Negate(int[,] matrix) { return LinearAlgebra.Negate(matrix); }
		/// <summary>Does a standard matrix addition.</summary>
		public int[,] Add(int[,] left, int[,] right) { return LinearAlgebra.Add(left, right); }
		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
		public int[,] Multiply(int[,] left, int[,] right) { return LinearAlgebra.Multiply(left, right); }
		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
		public int[,] Multiply(int[,] matrix, int scalar) { return LinearAlgebra.Multiply(matrix, scalar); }
		/// <summary>Divides all the values in this matrix by a scalar.</summary>
		public int[,] Divide(int[,] left, int right) { return LinearAlgebra.Divide(left, right); }
		/// <summary>Gets the minor of a matrix.</summary>
		public int[,] Minor(int[,] matrix, int row, int column) { return LinearAlgebra.Minor(matrix, row, column); }
		/// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		public int[,] ConcatenateRowWise(int[,] left, int[,] right) { return LinearAlgebra.ConcatenateRowWise(left, right); }
		/// <summary>Computes the determinent if this matrix is square.</summary>
		public int Determinent(int[,] matrix) { return LinearAlgebra.Determinent(matrix); }
		/// <summary>Computes the echelon form of this matrix (aka REF).</summary>
		public int[,] Echelon(int[,] matrix) { return LinearAlgebra.Echelon(matrix); }
		/// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
		public int[,] ReducedEchelon(int[,] matrix) { return LinearAlgebra.ReducedEchelon(matrix); }
		/// <summary>Computes the inverse of this matrix.</summary>
		public int[,] Inverse(int[,] matrix) { return LinearAlgebra.Inverse(matrix); }
		/// <summary>Gets the adjoint of this matrix.</summary>
		public int[,] Adjoint(int[,] matrix) { return LinearAlgebra.Adjoint(matrix); }
		/// <summary>Transposes this matrix.</summary>
		public int[,] Transpose(int[,] matrix) { return LinearAlgebra.Transpose(matrix); }
		/// <summary>Copies this matrix.</summary>
		public int[,] Clone(int[,] matrix) { return LinearAlgebra.Clone(matrix); }
	}
}

#region OLD DESIGN

namespace Seven.Mathematics
{
  public interface Matrix<T>
  {
    /// <summary>The number of rows in the matrix.</summary>
    int Rows { get; }
    /// <summary>The number of columns in the matrix.</summary>
    int Columns { get; }
    /// <summary>Determines if the matrix is square.</summary>
    bool IsSquare { get; }
    /// <summary>Determines if the matrix is a vector.</summary>
    bool IsVector { get; }
    /// <summary>Determines if the matrix is a 2 component vector.</summary>
    bool Is2x1 { get; }
    /// <summary>Determines if the matrix is a 3 component vector.</summary>
    bool Is3x1 { get; }
    /// <summary>Determines if the matrix is a 4 component vector.</summary>
    bool Is4x1 { get; }
    /// <summary>Determines if the matrix is a 2 square matrix.</summary>
    bool Is2x2 { get; }
    /// <summary>Determines if the matrix is a 3 square matrix.</summary>
    bool Is3x3 { get; }
    /// <summary>Determines if the matrix is a 4 square matrix.</summary>
    bool Is4x4 { get; }

    /// <summary>Standard row-major matrix indexing.</summary>
    /// <param name="row">The row index.</param>
    /// <param name="column">The column index.</param>
    /// <returns>The value at the given indeces.</returns>
    T this[int row, int column] { get; set; }

    /// <summary>Negates all the values in this matrix.</summary>
    /// <returns>The resulting matrix after the negations.</returns>
    Matrix<T> Negate();
    /// <summary>Does a standard matrix addition.</summary>
    /// <param name="right">The matrix to add to this matrix.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    Matrix<T> Add(Matrix<T> right);
    /// <summary>Does a standard matrix multiplication (triple for loop).</summary>
    /// <param name="right">The matrix to multiply this matrix by.</param>
    /// <returns>The resulting matrix after the multiplication.</returns>
    Matrix<T> Multiply(Matrix<T> right);
    /// <summary>Multiplies all the values in this matrix by a scalar.</summary>
    /// <param name="right">The scalar to multiply all the matrix values by.</param>
    /// <returns>The retulting matrix after the multiplications.</returns>
    Matrix<T> Multiply(float right);
    /// <summary>Divides all the values in this matrix by a scalar.</summary>
    /// <param name="right">The scalar to divide the matrix values by.</param>
    /// <returns>The resulting matrix after teh divisions.</returns>
    Matrix<T> Divide(float right);
    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="row">The restricted row of the minor.</param>
    /// <param name="column">The restricted column of the minor.</param>
    /// <returns>The minor from the row/column restrictions.</returns>
    Matrix<T> Minor(int row, int column);
    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="right">The matrix to combine with on the right side.</param>
    /// <returns>The resulting row-wise concatination.</returns>
    Matrix<T> ConcatenateRowWise(Matrix<T> right);
    /// <summary>Computes the determinent if this matrix is square.</summary>
    /// <returns>The computed determinent if this matrix is square.</returns>
    T Determinent();
    /// <summary>Computes the echelon form of this matrix (aka REF).</summary>
    /// <returns>The computed echelon form of this matrix (aka REF).</returns>
    Matrix<T> Echelon();
    /// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
    /// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
    Matrix<T> ReducedEchelon();
    /// <summary>Computes the inverse of this matrix.</summary>
    /// <returns>The inverse of this matrix.</returns>
    Matrix<T> Inverse();
    /// <summary>Gets the adjoint of this matrix.</summary>
    /// <returns>The adjoint of this matrix.</returns>
    Matrix<T> Adjoint();
    /// <summary>Transposes this matrix.</summary>
    /// <returns>The transpose of this matrix.</returns>
    Matrix<T> Transpose();
    /// <summary>Copies this matrix.</summary>
    /// <returns>The copy of this matrix.</returns>
    Matrix<T> Clone();
  }

  /// <summary>A matrix implemented as a flattened float array to perform matrix theory in row major order. Enjoy :)</summary>
  public class Matrix_Flattened
  {
    private float[] _matrix;
    private int _columns;
    private int _rows;

    /// <summary>The float[] reference of this matrix.</summary>
    public float[] Floats 
    {
      get { return _matrix; }
      set
      {
        if (value.Length == _rows * _columns) _matrix = value; 
        else throw new MatrixException("you cannot change the dimension of matrix when setting its float values");
      }
    }
    /// <summary>The number of rows in the matrix.</summary>
    public int Rows { get { return _rows; } }
    /// <summary>The number of columns in the matrix.</summary>
    public int Columns { get { return _columns; } }
    /// <summary>The number of elements in the matrix (rows * columns).</summary>
    public int Size { get { return _matrix.Length; } }
    /// <summary>Determines if the matrix is square.</summary>
    public bool IsSquare { get { return _rows == _columns; } }
    /// <summary>Determines if the matrix is a vector.</summary>
    public bool IsVector { get { return _columns == 1; } }
    /// <summary>Determines if the matrix is a 2 component vector.</summary>
    public bool Is2x1 { get { return _rows == 2 && _columns == 1; } }
    /// <summary>Determines if the matrix is a 3 component vector.</summary>
    public bool Is3x1 { get { return _rows == 3 && _columns == 1; } }
    /// <summary>Determines if the matrix is a 4 component vector.</summary>
    public bool Is4x1 { get { return _rows == 4 && _columns == 1; } }
    /// <summary>Determines if the matrix is a 2 square matrix.</summary>
    public bool Is2x2 { get { return _rows == 2 && _columns == 2; } }
    /// <summary>Determines if the matrix is a 3 square matrix.</summary>
    public bool Is3x3 { get { return _rows == 3 && _columns == 3; } }
    /// <summary>Determines if the matrix is a 4 square matrix.</summary>
    public bool Is4x4 { get { return _rows == 4 && _columns == 4; } }

    /// <summary>Standard row-major matrix indexing.</summary>
    /// <param name="row">The row index.</param>
    /// <param name="column">The column index.</param>
    /// <returns>The value at the given indeces.</returns>
    public float this[int row, int column]
    {
      get
      {
        if (row > _rows - 1 || column > _columns - 1)
          throw new MatrixException("index out of bounds.");
        return _matrix[row * _columns + column];
      }
      set
      {
        if (row > _rows - 1 || column > _columns - 1)
          throw new MatrixException("index out of bounds.");
        else _matrix[row * _columns + column] = value;
      }
    }

    /// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of row dimensions.</param>
    /// <param name="columns">The number of column dimensions.</param>
    public Matrix_Flattened(int rows, int columns)
    {
      _rows = rows;
      _columns = columns;
      try { _matrix = new float[rows * columns]; }
      catch { throw new MatrixException("invalid dimensions."); }
    }

    /// <summary>Wraps a float[] inside of a matrix class.</summary>
    /// <param name="matrix">The float[] to wrap in a matrix class.</param>
    public Matrix_Flattened(int rows, int columns, params float[] matrix)
    {
      float[] floats = new float[matrix.Length];
      Buffer.BlockCopy(matrix, 0, floats, 0, floats.Length * sizeof(float));
      _matrix = matrix;
      _columns = columns;
      _rows = rows;
    }

    /// <summary>This is a special constructor to make a vector into a matrix
    /// without copying the data for efficiency purposes.</summary>
    /// <param name="vector">The values the new matrix will point to.</param>
    private Matrix_Flattened(Vector vector)
    {
      _matrix = vector.Floats;
      _rows = _matrix.Length;
      _columns = 1;
    }

    /// <summary>Constructs a matrix that points to the values in a vector. So the vector and this
    /// new matrix point to the same float[].</summary>
    /// <param name="vector">The vector who will share the data as the constructed matrix.</param>
    /// <returns>The constructed matrix sharing the data with the vector.</returns>
    public static Matrix_Flattened UnsafeFactoryFromVector(Vector vector) { return new Matrix_Flattened(vector); }

    /// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed zero-matrix.</returns>
    public static Matrix_Flattened FactoryZero(int rows, int columns)
    {
      try { return new Matrix_Flattened(rows, columns); }
      catch { throw new MatrixException("invalid dimensions."); }
    }

    /// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed identity-matrix.</returns>
    public static Matrix_Flattened FactoryIdentity(int rows, int columns)
    {
      Matrix_Flattened matrix;
      try { matrix = new Matrix_Flattened(rows, columns); }
      catch { throw new MatrixException("invalid dimensions."); }
      if (rows <= columns)
        for (int i = 0; i < rows; i++)
          matrix[i, i] = 1;
      else
        for (int i = 0; i < columns; i++)
          matrix[i, i] = 1;
      return matrix;
    }

    /// <summary>Constructs a new matrix where every entry is 1.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed matrix filled with 1's.</returns>
    public static Matrix_Flattened FactoryOne(int rows, int columns)
    {
      Matrix_Flattened matrix;
      try { matrix = new Matrix_Flattened(rows, columns); }
      catch { throw new MatrixException("invalid dimensions."); }
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          matrix[i, j] = 1;
      return matrix;
    }

    /// <summary>Constructs a new matrix where every entry is the same uniform value.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <param name="uniform">The value to assign every spot in the matrix.</param>
    /// <returns>The newly constructed matrix filled with the uniform value.</returns>
    public static Matrix_Flattened FactoryUniform(int rows, int columns, float uniform)
    {
      Matrix_Flattened matrix;
      try { matrix = new Matrix_Flattened(rows, columns); }
      catch { throw new MatrixException("invalid dimensions."); }
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          matrix[i, j] = uniform;
      return matrix;
    }

    /// <summary>Constructs a 2-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 2-component vector matrix.</returns>
    public static Matrix_Flattened Factory2x1() { return new Matrix_Flattened(2, 1); }
    /// <summary>Constructs a 3-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 3-component vector matrix.</returns>
    public static Matrix_Flattened Factory3x1() { return new Matrix_Flattened(3, 1); }
    /// <summary>Constructs a 4-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 4-component vector matrix.</returns>
    public static Matrix_Flattened Factory4x1() { return new Matrix_Flattened(4, 1); }

    /// <summary>Constructs a 2x2 matrix with all values being 0.</summary>
    /// <returns>The constructed 2x2 matrix.</returns>
    public static Matrix_Flattened Factory2x2() { return new Matrix_Flattened(2, 2); }
    /// <summary>Constructs a 3x3 matrix with all values being 0.</summary>
    /// <returns>The constructed 3x3 matrix.</returns>
    public static Matrix_Flattened Factory3x3() { return new Matrix_Flattened(3, 3); }
    /// <summary>Constructs a 4x4 matrix with all values being 0.</summary>
    /// <returns>The constructed 4x4 matrix.</returns>
    public static Matrix_Flattened Factory4x4() { return new Matrix_Flattened(4, 4); }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix_Flattened Factory3x3RotationX(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix_Flattened(3, 3, new float[] { 1, 0, 0, 0, cos, sin, 0, -sin, cos });
    }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix_Flattened Factory3x3RotationY(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix_Flattened(3, 3, new float[] { cos, 0, -sin, 0, 1, 0, sin, 0, cos });
    }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix_Flattened Factory3x3RotationZ(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix_Flattened(3, 3, new float[] { cos, -sin, 0, sin, cos, 0, 0, 0, 1 });
    }

    /// <param name="angleX">Angle about the X-axis in radians.</param>
    /// <param name="angleY">Angle about the Y-axis in radians.</param>
    /// <param name="angleZ">Angle about the Z-axis in radians.</param>
    public static Matrix_Flattened Factory3x3RotationXthenYthenZ(float angleX, float angleY, float angleZ)
    {
      float
        xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
        yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
        zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
      return new Matrix_Flattened(3, 3,
        new float[] {
          yCos * zCos, -yCos * zSin, ySin,
          xCos * zSin + xSin * ySin * zCos, xCos * zCos + xSin * ySin * zSin, -xSin * yCos,
          xSin * zSin - xCos * ySin * zCos, xSin * zCos + xCos * ySin * zSin, xCos * yCos });
    }

    /// <param name="angleX">Angle about the X-axis in radians.</param>
    /// <param name="angleY">Angle about the Y-axis in radians.</param>
    /// <param name="angleZ">Angle about the Z-axis in radians.</param>
    public static Matrix_Flattened Factory3x3RotationZthenYthenX(float angleX, float angleY, float angleZ)
    {
      float
        xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
        yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
        zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
      return new Matrix_Flattened(3, 3, new float[] { yCos * zCos, zCos * xSin * ySin - xCos * zSin, xCos * zCos * ySin + xSin * zSin,
        yCos * zSin, xCos * zCos + xSin * ySin * zSin, -zCos * xSin + xCos * ySin * zSin, -ySin, yCos * xSin, xCos * yCos });
    }

    /// <summary>Creates a 3x3 matrix initialized with a shearing transformation.</summary>
    /// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
    /// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
    /// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
    /// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
    /// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
    /// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
    /// <returns>The constructed shearing matrix.</returns>
    public static Matrix_Flattened Factory3x3Shear(
      float shearXbyY, float shearXbyZ, float shearYbyX,
      float shearYbyZ, float shearZbyX, float shearZbyY)
    {
      return new Matrix_Flattened(3, 3, new float[] { 1, shearYbyX, shearZbyX, shearXbyY, 1, shearYbyZ, shearXbyZ, shearYbyZ, 1 });
    }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static Matrix_Flattened operator -(Matrix_Flattened matrix) { return Matrix_Flattened.Negate(matrix); }
    /// <summary>Does a standard matrix addition.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after teh addition.</returns>
    public static Matrix_Flattened operator +(Matrix_Flattened left, Matrix_Flattened right) { return Matrix_Flattened.Add(left, right); }
    /// <summary>Does a standard matrix subtraction.</summary>
    /// <param name="left">The left matrix of the subtraction.</param>
    /// <param name="right">The right matrix of the subtraction.</param>
    /// <returns>The result of the matrix subtraction.</returns>
    public static Matrix_Flattened operator -(Matrix_Flattened left, Matrix_Flattened right) { return Matrix_Flattened.Subtract(left, right); }
    /// <summary>Does a standard matrix multiplication.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix after the multiplication.</returns>
    public static Matrix_Flattened operator *(Matrix_Flattened left, Matrix_Flattened right) { return Matrix_Flattened.Multiply(left, right); }
    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have its values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static Matrix_Flattened operator *(Matrix_Flattened left, float right) { return Matrix_Flattened.Multiply(left, right); }
    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The scalar to multiply the values by.</param>
    /// <param name="right">The matrix to have its values multiplied.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static Matrix_Flattened operator *(float left, Matrix_Flattened right) { return Matrix_Flattened.Multiply(right, left); }
    /// <summary>Divides all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have its values divided.</param>
    /// <param name="right">The scalar to divide the values by.</param>
    /// <returns>The resulting matrix after the divisions.</returns>
    public static Matrix_Flattened operator /(Matrix_Flattened left, float right) { return Matrix_Flattened.Divide(left, right); }
    /// <summary>Applies a power to a matrix.</summary>
    /// <param name="left">The matrix to apply a power to.</param>
    /// <param name="right">The power to apply to the matrix.</param>
    /// <returns>The result of the power operation.</returns>
    public static Matrix_Flattened operator ^(Matrix_Flattened left, int right) { return Matrix_Flattened.Power(left, right); }
    /// <summary>Checks for equality by value.</summary>
    /// <param name="left">The left matrix of the equality check.</param>
    /// <param name="right">The right matrix of the equality check.</param>
    /// <returns>True if the values of the matrices are equal, false if not.</returns>
    public static bool operator ==(Matrix_Flattened left, Matrix_Flattened right) { return Matrix_Flattened.EqualsByValue(left, right); }
    /// <summary>Checks for false-equality by value.</summary>
    /// <param name="left">The left matrix of the false-equality check.</param>
    /// <param name="right">The right matrix of the false-equality check.</param>
    /// <returns>True if the values of the matrices are not equal, false if they are.</returns>
    public static bool operator !=(Matrix_Flattened left, Matrix_Flattened right) { return !Matrix_Flattened.EqualsByValue(left, right); }
    /// <summary>Automatically converts a matrix into a float[,] if necessary.</summary>
    /// <param name="matrix">The matrix to convert to a float[,].</param>
    /// <returns>The reference to the float[,] representing the matrix.</returns>
    //public static implicit operator float[](Matrix matrix) { return matrix.Floats; }

    /// <summary>Negates all the values in this matrix.</summary>
    /// <returns>The resulting matrix after the negations.</returns>
    private Matrix_Flattened Negate() { return Matrix_Flattened.Negate(this); }
    /// <summary>Does a standard matrix addition.</summary>
    /// <param name="right">The matrix to add to this matrix.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    private Matrix_Flattened Add(Matrix_Flattened right) { return Matrix_Flattened.Add(this, right); }
    /// <summary>Does a standard matrix multiplication (triple for loop).</summary>
    /// <param name="right">The matrix to multiply this matrix by.</param>
    /// <returns>The resulting matrix after the multiplication.</returns>
    private Matrix_Flattened Multiply(Matrix_Flattened right) { return Matrix_Flattened.Multiply(this, right); }
    /// <summary>Multiplies all the values in this matrix by a scalar.</summary>
    /// <param name="right">The scalar to multiply all the matrix values by.</param>
    /// <returns>The retulting matrix after the multiplications.</returns>
    private Matrix_Flattened Multiply(float right) { return Matrix_Flattened.Multiply(this, right); }
    /// <summary>Divides all the values in this matrix by a scalar.</summary>
    /// <param name="right">The scalar to divide the matrix values by.</param>
    /// <returns>The resulting matrix after teh divisions.</returns>
    private Matrix_Flattened Divide(float right) { return Matrix_Flattened.Divide(this, right); }
    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="row">The restricted row of the minor.</param>
    /// <param name="column">The restricted column of the minor.</param>
    /// <returns>The minor from the row/column restrictions.</returns>
    public Matrix_Flattened Minor(int row, int column) { return Matrix_Flattened.Minor(this, row, column); }
    /// <summary>Combines two matrices from left to right (result.Columns == left.Columns + right.Columns).</summary>
    /// <param name="right">The matrix to combine with on the right side.</param>
    /// <returns>The resulting row-wise concatination.</returns>
    public Matrix_Flattened ConcatenateRowWise(Matrix_Flattened right) { return Matrix_Flattened.ConcatenateRowWise(this, right); }
    /// <summary>Computes the determinent if this matrix is square.</summary>
    /// <returns>The computed determinent if this matrix is square.</returns>
    public float Determinent() { return Matrix_Flattened.Determinent(this); }
    /// <summary>Computes the echelon form of this matrix (aka REF).</summary>
    /// <returns>The computed echelon form of this matrix (aka REF).</returns>
    public Matrix_Flattened Echelon() { return Matrix_Flattened.Echelon(this); }
    /// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
    /// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
    public Matrix_Flattened ReducedEchelon() { return Matrix_Flattened.ReducedEchelon(this); }
    /// <summary>Computes the inverse of this matrix.</summary>
    /// <returns>The inverse of this matrix.</returns>
    public Matrix_Flattened Inverse() { return Matrix_Flattened.Inverse(this); }
    /// <summary>Gets the adjoint of this matrix.</summary>
    /// <returns>The adjoint of this matrix.</returns>
    public Matrix_Flattened Adjoint() { return Matrix_Flattened.Adjoint(this); }
    /// <summary>Transposes this matrix.</summary>
    /// <returns>The transpose of this matrix.</returns>
    public Matrix_Flattened Transpose() { return Matrix_Flattened.Transpose(this); }
    /// <summary>Copies this matrix.</summary>
    /// <returns>A copy of this matrix.</returns>
    public Matrix_Flattened Clone() { return Matrix_Flattened.Clone(this); }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static Matrix_Flattened Negate(Matrix_Flattened matrix)
    {
      Matrix_Flattened result = new Matrix_Flattened(matrix.Rows, matrix.Columns, matrix.Floats);
      float[] resultFloats = result.Floats;
      float[] matrixFloats = matrix.Floats;
      int length = resultFloats.Length;
      for (int i = 0; i < length; i++)
        resultFloats[i] = -matrixFloats[i];
      return result;
    }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static Matrix_Flattened Add(Matrix_Flattened left, Matrix_Flattened right)
    {
      if (left.Rows != right.Rows || left.Columns != right.Columns)
        throw new MatrixException("invalid addition (size miss-match).");
      Matrix_Flattened result = new Matrix_Flattened(left.Rows, left.Columns);
      float[]
        resultFloats = result.Floats,
        leftFloats = left.Floats,
        rightFloats = right.Floats;
      int length = resultFloats.Length;
      for (int i = 0; i < length; i++)
        resultFloats[i] = leftFloats[i] + rightFloats[i];
      return result;
    }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static Matrix_Flattened Subtract(Matrix_Flattened left, Matrix_Flattened right)
    {
      if (left.Rows != right.Rows || left.Columns != right.Columns)
        throw new MatrixException("invalid subtraction (size miss-match).");
      Matrix_Flattened result = new Matrix_Flattened(left.Rows, left.Columns);
      float[] resultFloats = result.Floats,
        leftFloats = left.Floats,
        rightFloats = right.Floats;
      int length = resultFloats.Length;
      for (int i = 0; i < length; i++)
        resultFloats[i] = leftFloats[i] - rightFloats[i];
      return result;
    }

    /// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix of the multiplication.</returns>
    public static Matrix_Flattened Multiply(Matrix_Flattened left, Matrix_Flattened right)
    {
      float[] leftFloats = left.Floats, rightFloats = right.Floats, resultFloats;
      int leftRows = left.Rows, leftCols = left.Columns, rightRows = right.Rows, rightCols = right.Columns;
      Matrix_Flattened result = new Matrix_Flattened(leftRows, rightCols);
      resultFloats = result.Floats;
      #region Optimizations
      if (leftRows < 5)
      {
        if (leftRows == 4 && leftCols == 4 && rightRows == 4 && rightCols == 4)
        {
          resultFloats = new float[16];
          float
            l11 = leftFloats[0], l12 = leftFloats[1], l13 = leftFloats[2], l14 = leftFloats[3],
            l21 = leftFloats[4], l22 = leftFloats[5], l23 = leftFloats[6], l24 = leftFloats[7],
            l31 = leftFloats[8], l32 = leftFloats[9], l33 = leftFloats[10], l34 = leftFloats[11],
            l41 = leftFloats[12], l42 = leftFloats[13], l43 = leftFloats[14], l44 = leftFloats[15],
            r11 = rightFloats[0], r12 = rightFloats[1], r13 = rightFloats[2], r14 = rightFloats[3],
            r21 = rightFloats[4], r22 = rightFloats[5], r23 = rightFloats[6], r24 = rightFloats[7],
            r31 = rightFloats[8], r32 = rightFloats[9], r33 = rightFloats[10], r34 = rightFloats[11],
            r41 = rightFloats[12], r42 = rightFloats[13], r43 = rightFloats[14], r44 = rightFloats[15];
          resultFloats[0] = l11 * r11 + l12 * r21 + l13 * r31 + l14 * r41;
          resultFloats[1] = l11 * r12 + l12 * r22 + l13 * r32 + l14 * r42;
          resultFloats[2] = l11 * r13 + l12 * r23 + l13 * r33 + l14 * r43;
          resultFloats[3] = l11 * r14 + l12 * r24 + l13 * r34 + l14 * r44;
          resultFloats[4] = l21 * r11 + l22 * r21 + l23 * r31 + l24 * r41;
          resultFloats[5] = l21 * r12 + l22 * r22 + l23 * r32 + l24 * r42;
          resultFloats[6] = l21 * r13 + l22 * r23 + l23 * r33 + l24 * r43;
          resultFloats[7] = l21 * r14 + l22 * r24 + l23 * r34 + l24 * r44;
          resultFloats[8] = l31 * r11 + l32 * r21 + l33 * r31 + l34 * r41;
          resultFloats[9] = l31 * r12 + l32 * r22 + l33 * r32 + l34 * r42;
          resultFloats[10] = l31 * r13 + l32 * r23 + l33 * r33 + l34 * r43;
          resultFloats[11] = l31 * r14 + l32 * r24 + l33 * r34 + l34 * r44;
          resultFloats[12] = l41 * r11 + l42 * r21 + l43 * r31 + l44 * r41;
          resultFloats[13] = l41 * r12 + l42 * r22 + l43 * r32 + l44 * r42;
          resultFloats[14] = l41 * r13 + l42 * r23 + l43 * r33 + l44 * r43;
          resultFloats[15] = l41 * r14 + l42 * r24 + l43 * r34 + l44 * r44;
          return result;
        }
      }
      #endregion
      if (leftCols != right.Rows)
        throw new MatrixException("invalid multiplication (size miss-match).");
      resultFloats = new float[leftRows * rightCols];
      for (int i = 0; i < leftRows; i++)
        for (int j = 0; j < rightCols; j++)
          for (int k = 0; k < leftCols; k++)
            resultFloats[i * rightCols + j] += leftFloats[i * leftCols + k] * rightFloats[k * rightCols + j];
      return result;
    }

    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have the values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static Matrix_Flattened Multiply(Matrix_Flattened left, float right)
    {
      Matrix_Flattened result = new Matrix_Flattened(left.Rows, left.Columns);
      float[] resultFloats = result.Floats;
      float[] leftFloats = left.Floats;
      for (int i = 0; i < resultFloats.Length; i++)
        resultFloats[i] = leftFloats[i] * right;
      return result;
    }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static Matrix_Flattened Power(Matrix_Flattened matrix, int power)
    {
      if (!(matrix.Rows == matrix.Columns))
        throw new MatrixException("invalid power (!matrix.IsSquare).");
      if (!(power > -1))
        throw new MatrixException("invalid power !(power > -1)");
      if (power == 0)
        return Matrix_Flattened.FactoryIdentity(matrix.Rows, matrix.Columns);
      Matrix_Flattened result = matrix.Clone();
      for (int i = 0; i < power; i++)
        result *= matrix;
      return result;
    }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="left">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static Matrix_Flattened Divide(Matrix_Flattened left, float right) { return Matrix_Flattened.Multiply(left, 1.0f / right); }

    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="matrix">The matrix to get the minor of.</param>
    /// <param name="row">The restricted row to form the minor.</param>
    /// <param name="column">The restricted column to form the minor.</param>
    /// <returns>The minor of the matrix.</returns>
    public static Matrix_Flattened Minor(Matrix_Flattened matrix, int row, int column)
    {
      int matrixRows = matrix.Rows, matrixCols = matrix.Columns, resultCols = matrix.Columns - 1;
      Matrix_Flattened result = new Matrix_Flattened(matrix.Rows - 1, resultCols);
      float[] resultFloats = result.Floats;
      float[] matrixFloats = matrix.Floats;
      int m = 0, n = 0;
      for (int i = 0; i < matrixRows; i++)
      {
        if (i == row) continue;
        n = 0;
        for (int j = 0; j < matrixCols; j++)
          if (j == column) continue;
          else resultFloats[m * resultCols + n++] = matrixFloats[i * matrixCols + j];
        m++;
      }
      return result;
    }

    private static void RowMultiplication(Matrix_Flattened matrix, int row, float scalar)
    {
      float[] matrixFloats = matrix.Floats;
      int start = row * matrix.Columns, end = row * matrix.Columns + matrix.Columns;
      for (int j = start; j < end; j++)
        matrixFloats[j] *= scalar;
    }

    private static void RowAddition(Matrix_Flattened matrix, int target, int second, float scalar)
    {
      float[] matrixfloats = matrix.Floats;
      int columns = matrix.Columns,
        targetOffset = target * columns,
        secondOffset = second * columns;
      for (int j = 0; j < columns; j++)
        matrixfloats[targetOffset + j] += (matrixfloats[secondOffset + j] * scalar);
    }

    private static void SwapRows(Matrix_Flattened matrix, int row1, int row2)
    {
      float[] matrixFloats = matrix.Floats;
      int columns = matrix.Columns, row1Offset = row1 * columns, row2Offset = row2 * columns;
      for (int j = 0; j < columns; j++)
      {
        float temp = matrixFloats[row1Offset + j];
        matrixFloats[row1Offset + j] = matrixFloats[row2Offset + j];
        matrixFloats[row2Offset + j] = temp;
      }
    }
		
    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static Matrix_Flattened ConcatenateRowWise(Matrix_Flattened left, Matrix_Flattened right)
    {
      if (left.Rows != right.Rows)
        throw new MatrixException("invalid row-wise concatenation !(left.Rows == right.Rows).");
      int resultRows = left.Rows, resultCols = left.Columns + right.Columns,
        leftCols = left.Columns, rightCols = right.Columns;
      Matrix_Flattened result = new Matrix_Flattened(resultRows, resultCols);
      float[]
        resultfloats = result.Floats,
        leftFloats = left.Floats,
        rightFloats = right.Floats;
      for (int i = 0; i < resultRows; i++)
        for (int j = 0; j < resultCols; j++)
        {
          if (j < left.Columns) resultfloats[i * resultCols + j] = leftFloats[i * leftCols + j];
          else resultfloats[i * resultCols + j] = rightFloats[i * rightCols + j - leftCols];
        }
      return result;
    }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static float Determinent(Matrix_Flattened matrix)
    {
      int rows = matrix.Rows, columns = matrix.Columns;
      if (!(rows == matrix.Columns))
        throw new MatrixException("invalid determinent !(matrix.IsSquare).");
      float det = 1.0f;
      try
      {
        float[] rref = new float[matrix.Floats.Length];
        Buffer.BlockCopy(matrix.Floats, 0, rref, 0, rref.Length * sizeof(float));
        for (int i = 0; i < rows; i++)
        {
          if (rref[i * columns + i] == 0)
            for (int j = i + 1; j < rows; j++)
              if (rref[j * columns + i] != 0)
              {
                Matrix_Flattened.SwapRows(matrix, i, j);
                det *= -1;
              }
          det *= rref[i * columns + i];
          Matrix_Flattened.RowMultiplication(matrix, i, 1 / rref[i * columns + i]);
          for (int j = i + 1; j < rows; j++)
            Matrix_Flattened.RowAddition(matrix, j, i, -rref[j * columns + i]);
          for (int j = i - 1; j >= 0; j--)
            Matrix_Flattened.RowAddition(matrix, j, i, -rref[j * columns + i]);
        }
        return det;
      }
      catch (Exception) { throw new MatrixException("determinent computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static Matrix_Flattened Echelon(Matrix_Flattened matrix)
    {
      try
      {
        int rows = matrix.Rows, columns = matrix.Columns;
        Matrix_Flattened result = new Matrix_Flattened(rows, columns, matrix.Floats);
        float[] resultfloats = result.Floats;
        for (int i = 0; i < rows; i++)
        {
          if (resultfloats[i * columns + i] == 0)
            for (int j = i + 1; j < rows; j++)
              if (resultfloats[j * columns + i] != 0)
                Matrix_Flattened.SwapRows(result, i, j);
          if (resultfloats[i * columns + i] == 0)
            continue;
          if (resultfloats[i * columns + i] != 1)
            for (int j = i + 1; j < rows; j++)
              if (resultfloats[j * columns + i] == 1)
                Matrix_Flattened.SwapRows(result, i, j);
          Matrix_Flattened.RowMultiplication(result, i, 1 / resultfloats[i * columns + i]);
          for (int j = i + 1; j < rows; j++)
            Matrix_Flattened.RowAddition(result, j, i, -resultfloats[j * columns + i]);
        }
        return result;
      }
      catch { throw new MatrixException("echelon computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static Matrix_Flattened ReducedEchelon(Matrix_Flattened matrix)
    {
      try
      {
        int rows = matrix.Rows, columns = matrix.Columns;
        Matrix_Flattened result = new Matrix_Flattened(rows, columns, matrix.Floats);
        float[] resultFloats = result.Floats;
        for (int i = 0; i < rows; i++)
        {
          if (resultFloats[i * columns + i] == 0)
            for (int j = i + 1; j < rows; j++)
              if (resultFloats[j * columns + i] != 0)
                Matrix_Flattened.SwapRows(result, i, j);
          if (resultFloats[i * columns + i] == 0) continue;
          if (resultFloats[i * columns + i] != 1)
            for (int j = i + 1; j < rows; j++)
              if (resultFloats[j * columns + i] == 1)
                Matrix_Flattened.SwapRows(result, i, j);
          Matrix_Flattened.RowMultiplication(result, i, 1 / resultFloats[i * columns + i]);
          for (int j = i + 1; j < rows; j++)
            Matrix_Flattened.RowAddition(result, j, i, -resultFloats[j * columns + i]);
          for (int j = i - 1; j >= 0; j--)
            Matrix_Flattened.RowAddition(result, j, i, -resultFloats[j * columns + i]);
        }
        return result;
      }
      catch { throw new MatrixException("reduced echelon calculation failed."); }
    }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static Matrix_Flattened Inverse(Matrix_Flattened matrix)
    {
      if (Matrix_Flattened.Determinent(matrix) == 0)
        throw new MatrixException("inverse calculation failed.");
      try
      {
        Matrix_Flattened identity = Matrix_Flattened.FactoryIdentity(matrix.Rows, matrix.Columns);
        Matrix_Flattened rref = Matrix_Flattened.Clone(matrix);
        for (int i = 0; i < matrix.Rows; i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.Rows; j++)
              if (rref[j, i] != 0)
              {
                Matrix_Flattened.SwapRows(rref, i, j);
                Matrix_Flattened.SwapRows(identity, i, j);
              }
          Matrix_Flattened.RowMultiplication(identity, i, 1 / rref[i, i]);
          Matrix_Flattened.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.Rows; j++)
          {
            Matrix_Flattened.RowAddition(identity, j, i, -rref[j, i]);
            Matrix_Flattened.RowAddition(rref, j, i, -rref[j, i]);
          }
          for (int j = i - 1; j >= 0; j--)
          {
            Matrix_Flattened.RowAddition(identity, j, i, -rref[j, i]);
            Matrix_Flattened.RowAddition(rref, j, i, -rref[j, i]);
          }
        }
        return identity;
      }
      catch { throw new MatrixException("inverse calculation failed."); }
    }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static Matrix_Flattened Adjoint(Matrix_Flattened matrix)
    {
      if (!(matrix.Rows == matrix.Columns))
        throw new MatrixException("Adjoint of a non-square matrix does not exists");
      Matrix_Flattened AdjointMatrix = new Matrix_Flattened(matrix.Rows, matrix.Columns);
      for (int i = 0; i < matrix.Rows; i++)
        for (int j = 0; j < matrix.Columns; j++)
          if ((i + j) % 2 == 0)
            AdjointMatrix[i, j] = Matrix_Flattened.Determinent(Matrix_Flattened.Minor(matrix, i, j));
          else
            AdjointMatrix[i, j] = -Matrix_Flattened.Determinent(Matrix_Flattened.Minor(matrix, i, j));
      return Matrix_Flattened.Transpose(AdjointMatrix);
    }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static Matrix_Flattened Transpose(Matrix_Flattened matrix)
    {
      Matrix_Flattened result = new Matrix_Flattened(matrix.Columns, matrix.Rows);
      float[] matrixfloats = matrix.Floats;
      int rows = matrix.Columns, columns = matrix.Rows;
      float[] resultFloats = result.Floats;
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          resultFloats[i * columns + j] = matrixfloats[j * rows + i];
      return result;
    }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="Lower">The computed lower triangular matrix.</param>
    /// <param name="Upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(Matrix_Flattened matrix, out Matrix_Flattened Lower, out Matrix_Flattened Upper)
    {
      if (!(matrix.Rows == matrix.Columns))
        throw new MatrixException("The matrix is not square!");
      Lower = Matrix_Flattened.FactoryIdentity(matrix.Rows, matrix.Columns);
      Upper = Matrix_Flattened.Clone(matrix);
      int[] permutation = new int[matrix.Rows];
      for (int i = 0; i < matrix.Rows; i++) permutation[i] = i;
      float p = 0, pom2, detOfP = 1;
      int k0 = 0, pom1 = 0;
      for (int k = 0; k < matrix.Columns - 1; k++)
      {
        p = 0;
        for (int i = k; i < matrix.Rows; i++)
          if (Calc.Abs(Upper[i, k]) > p)
          {
            p = Calc.Abs(Upper[i, k]);
            k0 = i;
          }
        if (p == 0)
          throw new MatrixException("The matrix is singular!");
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
        for (int i = 0; i < matrix.Columns; i++)
        {
          pom2 = Upper[k, i];
          Upper[k, i] = Upper[k0, i];
          Upper[k0, i] = pom2;
        }
        for (int i = k + 1; i < matrix.Rows; i++)
        {
          Lower[i, k] = Upper[i, k] / Upper[k, k];
          for (int j = k; j < matrix.Columns; j++)
            Upper[i, j] = Upper[i, j] - Lower[i, k] * Upper[k, j];
        }
      }
    }

    /// <summary>Does a value equality check.</summary>
    /// <param name="left">The first matrix to check for equality.</param>
    /// <param name="right">The second matrix to check for equality.</param>
    /// <returns>True if values are equal, false if not.</returns>
    public static bool EqualsByValue(Matrix_Flattened left, Matrix_Flattened right)
    {
      int rows = left.Rows, columns = left.Columns;
      if (rows != right.Rows || columns != right.Columns)
        return false;
      float[] leftFloats = left.Floats;
      float[] rightFloats = right.Floats;
      for (int i = 0; i < leftFloats.Length; i++)
        if (leftFloats[i] != rightFloats[i])
          return false;
      return true;
    }

    /// <summary>Does a value equality check with leniency.</summary>
    /// <param name="left">The first matrix to check for equality.</param>
    /// <param name="right">The second matrix to check for equality.</param>
    /// <param name="leniency">How much the values can vary but still be considered equal.</param>
    /// <returns>True if values are equal, false if not.</returns>
    public static bool EqualsByValue(Matrix_Flattened left, Matrix_Flattened right, float leniency)
    {
      int rows = left.Rows, columns = left.Columns;
      if (rows != right.Rows || columns != right.Columns)
        return false;
      float[] leftFloats = left.Floats;
      float[] rightFloats = right.Floats;
      for (int i = 0; i < leftFloats.Length; i++)
        if (Calc.Abs(leftFloats[i] - rightFloats[i]) > leniency)
          return false;
      return true;
    }

    /// <summary>Checks if two matrices are equal by reverences.</summary>
    /// <param name="left">The left matric of the equality check.</param>
    /// <param name="right">The right matrix of the equality check.</param>
    /// <returns>True if the references are equal, false if not.</returns>
    public static bool EqualsByReference(Matrix_Flattened left, Matrix_Flattened right)
    {
      if (left == null || right == null) return false;
      return object.ReferenceEquals(left, right) || object.ReferenceEquals(left.Floats, right.Floats);
    }

    /// <summary>Copies a matrix.</summary>
    /// <returns>The copy of this matrix.</returns>
    public static Matrix_Flattened Clone(Matrix_Flattened matrix)
    {
      float[] floats = new float[matrix.Size];
      Buffer.BlockCopy(matrix.Floats, 0, floats, 0, floats.Length * sizeof(float));
      return new Matrix_Flattened(matrix.Rows, matrix.Columns, floats);
    }

    /// <summary>Converts the matrix into a vector if (matrix.IsVector).</summary>
    /// <param name="matrix">The matrix to convert.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector ToVector(Matrix_Flattened matrix)
    {
      if (!matrix.IsVector)
        throw new MatrixException("invalid conversion from matrix to vector.");
      return new Vector(matrix.Floats);
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
      int hash = _matrix[0].GetHashCode();
      for (int i = 0; i < _matrix.Length; i++)
        hash = hash ^ _matrix[i].GetHashCode();
      return hash;
    }

    /// <summary>Does an equality check by reference.</summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns>True if the references are equal, false if not.</returns>
    public override bool Equals(object right)
    {
      if (!(right is Matrix_Flattened)) return false;
      return Matrix_Flattened.EqualsByReference(this, (Matrix_Flattened)right);
    }

    private class MatrixException : Error
    {
      public MatrixException(string Message) : base(Message) { }
    }
  }

  /// <summary>A matrix wrapper for float[,] to perform matrix theory in row major order. Enjoy :)</summary>
  public class Matrix_2dArray //: Matrix<float>
  {
    private float[,] _matrix;

    /// <summary>The float[,] reference of this matrix.</summary>
    public float[,] Floats { get { return _matrix; } }
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
    public float this[int row, int column]
    {
      get
      {
        try { return _matrix[row, column]; }
        catch { throw new MatrixException("index out of bounds."); }
      }
      set
      {
        try { _matrix[row, column] = value; }
        catch { throw new MatrixException("index out of bounds."); }
      }
    }

    /// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of row dimensions.</param>
    /// <param name="columns">The number of column dimensions.</param>
    public Matrix_2dArray(int rows, int columns)
    {
      try { _matrix = new float[rows, columns]; }
      catch { throw new MatrixException("invalid dimensions."); }
    }

    /// <summary>Constructs a new array given row/column dimensions and the values to fill the matrix with.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <param name="values">The values to fill the matrix with.</param>
    public Matrix_2dArray(int rows, int columns, params float[] values)
    {
      if (values.Length != rows * columns)
        throw new MatrixException("invalid construction (number of values does not match dimensions.)");
      float[,] matrix;
      try { matrix = new float[rows, columns]; }
      catch { throw new MatrixException("invalid dimensions."); }
      int k = 0;
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          matrix[i, j] = values[k++];
      _matrix = matrix;
    }

    /// <summary>Wraps a float[,] inside of a matrix class. WARNING: still references that float[,].</summary>
    /// <param name="matrix">The float[,] to wrap in a matrix class.</param>
    public Matrix_2dArray(float[,] matrix)
    {
      _matrix = matrix;
    }

    /// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed zero-matrix.</returns>
    public static Matrix_2dArray FactoryZero(int rows, int columns)
    {
      try { return new Matrix_2dArray(rows, columns); }
      catch { throw new MatrixException("invalid dimensions."); }
    }

    /// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed identity-matrix.</returns>
    public static Matrix_2dArray FactoryIdentity(int rows, int columns)
    {
      Matrix_2dArray matrix;
      try { matrix = new Matrix_2dArray(rows, columns); }
      catch { throw new MatrixException("invalid dimensions."); }
      if (rows <= columns)
        for (int i = 0; i < rows; i++)
          matrix[i, i] = 1;
      else
        for (int i = 0; i < columns; i++)
          matrix[i, i] = 1;
      return matrix;
    }

    /// <summary>Constructs a new matrix where every entry is 1.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <returns>The newly constructed matrix filled with 1's.</returns>
    public static Matrix_2dArray FactoryOne(int rows, int columns)
    {
      Matrix_2dArray matrix;
      try { matrix = new Matrix_2dArray(rows, columns); }
      catch { throw new MatrixException("invalid dimensions."); }
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          matrix[i, j] = 1;
      return matrix;
    }

    /// <summary>Constructs a new matrix where every entry is the same uniform value.</summary>
    /// <param name="rows">The number of rows of the matrix.</param>
    /// <param name="columns">The number of columns of the matrix.</param>
    /// <param name="uniform">The value to assign every spot in the matrix.</param>
    /// <returns>The newly constructed matrix filled with the uniform value.</returns>
    public static Matrix_2dArray FactoryUniform(int rows, int columns, float uniform)
    {
      Matrix_2dArray matrix;
      try { matrix = new Matrix_2dArray(rows, columns); }
      catch { throw new MatrixException("invalid dimensions."); }
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
          matrix[i, j] = uniform;
      return matrix;
    }

    /// <summary>Constructs a 2-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 2-component vector matrix.</returns>
    public static Matrix_2dArray Factory2x1() { return new Matrix_2dArray(2, 1); }
    /// <summary>Constructs a 3-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 3-component vector matrix.</returns>
    public static Matrix_2dArray Factory3x1() { return new Matrix_2dArray(3, 1); }
    /// <summary>Constructs a 4-component vector matrix with all values being 0.</summary>
    /// <returns>The constructed 4-component vector matrix.</returns>
    public static Matrix_2dArray Factory4x1() { return new Matrix_2dArray(4, 1); }

    /// <summary>Constructs a 2x2 matrix with all values being 0.</summary>
    /// <returns>The constructed 2x2 matrix.</returns>
    public static Matrix_2dArray Factory2x2() { return new Matrix_2dArray(2, 2); }
    /// <summary>Constructs a 3x3 matrix with all values being 0.</summary>
    /// <returns>The constructed 3x3 matrix.</returns>
    public static Matrix_2dArray Factory3x3() { return new Matrix_2dArray(3, 3); }
    /// <summary>Constructs a 4x4 matrix with all values being 0.</summary>
    /// <returns>The constructed 4x4 matrix.</returns>
    public static Matrix_2dArray Factory4x4() { return new Matrix_2dArray(4, 4); }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix_2dArray Factory3x3RotationX(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix_2dArray(new float[,] {
        { 1, 0, 0 },
        { 0, cos, sin },
        { 0, -sin, cos }});
    }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix_2dArray Factory3x3RotationY(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix_2dArray(new float[,] {
        { cos, 0, -sin },
        { 0, 1, 0 },
        { sin, 0, cos }});
    }

    /// <param name="angle">Angle of rotation in radians.</param>
    public static Matrix_2dArray Factory3x3RotationZ(float angle)
    {
      float cos = Calc.Cos(angle);
      float sin = Calc.Sin(angle);
      return new Matrix_2dArray(new float[,] {
        { cos, -sin, 0 },
        { sin, cos, 0 },
        { 0, 0, 1 }});
    }

    /// <param name="angleX">Angle about the X-axis in radians.</param>
    /// <param name="angleY">Angle about the Y-axis in radians.</param>
    /// <param name="angleZ">Angle about the Z-axis in radians.</param>
    public static Matrix_2dArray Factory3x3RotationXthenYthenZ(float angleX, float angleY, float angleZ)
    {
      float
        xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
        yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
        zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
      return new Matrix_2dArray(new float[,] {
        { yCos * zCos, -yCos * zSin, ySin },
        { xCos * zSin + xSin * ySin * zCos, xCos * zCos + xSin * ySin * zSin, -xSin * yCos },
        { xSin * zSin - xCos * ySin * zCos, xSin * zCos + xCos * ySin * zSin, xCos * yCos }});
    }

    /// <param name="angleX">Angle about the X-axis in radians.</param>
    /// <param name="angleY">Angle about the Y-axis in radians.</param>
    /// <param name="angleZ">Angle about the Z-axis in radians.</param>
    public static Matrix_2dArray Factory3x3RotationZthenYthenX(float angleX, float angleY, float angleZ)
    {
      float
        xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
        yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
        zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
      return new Matrix_2dArray(new float[,] {
        { yCos * zCos, zCos * xSin * ySin - xCos * zSin, xCos * zCos * ySin + xSin * zSin },
        { yCos * zSin, xCos * zCos + xSin * ySin * zSin, -zCos * xSin + xCos * ySin * zSin },
        { -ySin, yCos * xSin, xCos * yCos }});
    }

    /// <summary>Creates a 3x3 matrix initialized with a shearing transformation.</summary>
    /// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
    /// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
    /// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
    /// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
    /// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
    /// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
    /// <returns>The constructed shearing matrix.</returns>
    public static Matrix_2dArray Factory3x3Shear(
      float shearXbyY, float shearXbyZ, float shearYbyX,
      float shearYbyZ, float shearZbyX, float shearZbyY)
    {
      return new Matrix_2dArray(new float[,] {
        { 1, shearYbyX, shearZbyX },
        { shearXbyY, 1, shearYbyZ },
        { shearXbyZ, shearYbyZ, 1 }});
    }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static Matrix_2dArray operator -(Matrix_2dArray matrix) { return Matrix_2dArray.Negate(matrix); }
    /// <summary>Does a standard matrix addition.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after teh addition.</returns>
    public static Matrix_2dArray operator +(Matrix_2dArray left, Matrix_2dArray right) { return Matrix_2dArray.Add(left, right); }
    /// <summary>Does a standard matrix subtraction.</summary>
    /// <param name="left">The left matrix of the subtraction.</param>
    /// <param name="right">The right matrix of the subtraction.</param>
    /// <returns>The result of the matrix subtraction.</returns>
    public static Matrix_2dArray operator -(Matrix_2dArray left, Matrix_2dArray right) { return Matrix_2dArray.Subtract(left, right); }
    /// <summary>Does a standard matrix multiplication.</summary>
    /// <param name="left">The left matrix of the multiplication.</param>
    /// <param name="right">The right matrix of the multiplication.</param>
    /// <returns>The resulting matrix after the multiplication.</returns>
    public static Matrix_2dArray operator *(Matrix_2dArray left, Matrix_2dArray right) { return Matrix_2dArray.Multiply(left, right); }
    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have its values multiplied.</param>
    /// <param name="right">The scalar to multiply the values by.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static Matrix_2dArray operator *(Matrix_2dArray left, float right) { return Matrix_2dArray.Multiply(left, right); }
    /// <summary>Multiplies all the values in a matrix by a scalar.</summary>
    /// <param name="left">The scalar to multiply the values by.</param>
    /// <param name="right">The matrix to have its values multiplied.</param>
    /// <returns>The resulting matrix after the multiplications.</returns>
    public static Matrix_2dArray operator *(float left, Matrix_2dArray right) { return Matrix_2dArray.Multiply(right, left); }
    /// <summary>Divides all the values in a matrix by a scalar.</summary>
    /// <param name="left">The matrix to have its values divided.</param>
    /// <param name="right">The scalar to divide the values by.</param>
    /// <returns>The resulting matrix after the divisions.</returns>
    public static Matrix_2dArray operator /(Matrix_2dArray left, float right) { return Matrix_2dArray.Divide(left, right); }
    /// <summary>Applies a power to a matrix.</summary>
    /// <param name="left">The matrix to apply a power to.</param>
    /// <param name="right">The power to apply to the matrix.</param>
    /// <returns>The result of the power operation.</returns>
    public static Matrix_2dArray operator ^(Matrix_2dArray left, int right) { return Matrix_2dArray.Power(left, right); }
    /// <summary>Checks for equality by value.</summary>
    /// <param name="left">The left matrix of the equality check.</param>
    /// <param name="right">The right matrix of the equality check.</param>
    /// <returns>True if the values of the matrices are equal, false if not.</returns>
    public static bool operator ==(Matrix_2dArray left, Matrix_2dArray right) { return Matrix_2dArray.EqualsByValue(left, right); }
    /// <summary>Checks for false-equality by value.</summary>
    /// <param name="left">The left matrix of the false-equality check.</param>
    /// <param name="right">The right matrix of the false-equality check.</param>
    /// <returns>True if the values of the matrices are not equal, false if they are.</returns>
    public static bool operator !=(Matrix_2dArray left, Matrix_2dArray right) { return !Matrix_2dArray.EqualsByValue(left, right); }
    /// <summary>Automatically converts a matrix into a float[,] if necessary.</summary>
    /// <param name="matrix">The matrix to convert to a float[,].</param>
    /// <returns>The reference to the float[,] representing the matrix.</returns>
    public static implicit operator float[,](Matrix_2dArray matrix) { return matrix.Floats; }

    /// <summary>Negates all the values in this matrix.</summary>
    /// <returns>The resulting matrix after the negations.</returns>
    private Matrix_2dArray Negate() { return Matrix_2dArray.Negate(this); }
    /// <summary>Does a standard matrix addition.</summary>
    /// <param name="right">The matrix to add to this matrix.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    private Matrix_2dArray Add(Matrix_2dArray right) { return Matrix_2dArray.Add(this, right); }
    /// <summary>Does a standard matrix multiplication (triple for loop).</summary>
    /// <param name="right">The matrix to multiply this matrix by.</param>
    /// <returns>The resulting matrix after the multiplication.</returns>
    private Matrix_2dArray Multiply(Matrix_2dArray right) { return Matrix_2dArray.Multiply(this, right); }
    /// <summary>Multiplies all the values in this matrix by a scalar.</summary>
    /// <param name="right">The scalar to multiply all the matrix values by.</param>
    /// <returns>The retulting matrix after the multiplications.</returns>
    private Matrix_2dArray Multiply(float right) { return Matrix_2dArray.Multiply(this, right); }
    /// <summary>Divides all the values in this matrix by a scalar.</summary>
    /// <param name="right">The scalar to divide the matrix values by.</param>
    /// <returns>The resulting matrix after teh divisions.</returns>
    private Matrix_2dArray Divide(float right) { return Matrix_2dArray.Divide(this, right); }
    /// <summary>Gets the minor of a matrix.</summary>
    /// <param name="row">The restricted row of the minor.</param>
    /// <param name="column">The restricted column of the minor.</param>
    /// <returns>The minor from the row/column restrictions.</returns>
    public Matrix_2dArray Minor(int row, int column) { return Matrix_2dArray.Minor(this, row, column); }
    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="right">The matrix to combine with on the right side.</param>
    /// <returns>The resulting row-wise concatination.</returns>
    public Matrix_2dArray ConcatenateRowWise(Matrix_2dArray right) { return Matrix_2dArray.ConcatenateRowWise(this, right); }
    /// <summary>Computes the determinent if this matrix is square.</summary>
    /// <returns>The computed determinent if this matrix is square.</returns>
    public float Determinent() { return Matrix_2dArray.Determinent(this); }
    /// <summary>Computes the echelon form of this matrix (aka REF).</summary>
    /// <returns>The computed echelon form of this matrix (aka REF).</returns>
    public Matrix_2dArray Echelon() { return Matrix_2dArray.Echelon(this); }
    /// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary>
    /// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
    public Matrix_2dArray ReducedEchelon() { return Matrix_2dArray.ReducedEchelon(this); }
    /// <summary>Computes the inverse of this matrix.</summary>
    /// <returns>The inverse of this matrix.</returns>
    public Matrix_2dArray Inverse() { return Matrix_2dArray.Inverse(this); }
    /// <summary>Gets the adjoint of this matrix.</summary>
    /// <returns>The adjoint of this matrix.</returns>
    public Matrix_2dArray Adjoint() { return Matrix_2dArray.Adjoint(this); }
    /// <summary>Transposes this matrix.</summary>
    /// <returns>The transpose of this matrix.</returns>
    public Matrix_2dArray Transpose() { return Matrix_2dArray.Transpose(this); }
    /// <summary>Copies this matrix.</summary>
    /// <returns>The copy of this matrix.</returns>
    public Matrix_2dArray Clone() { return Matrix_2dArray.Clone(this); }

    /// <summary>Negates all the values in a matrix.</summary>
    /// <param name="matrix">The matrix to have its values negated.</param>
    /// <returns>The resulting matrix after the negations.</returns>
    public static Matrix_2dArray Negate(float[,] matrix)
    {
      Matrix_2dArray result = new Matrix_2dArray(matrix.GetLength(0), matrix.GetLength(1));
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = -matrix[i, j];
      return result;
    }

    /// <summary>Does standard addition of two matrices.</summary>
    /// <param name="left">The left matrix of the addition.</param>
    /// <param name="right">The right matrix of the addition.</param>
    /// <returns>The resulting matrix after the addition.</returns>
    public static Matrix_2dArray Add(float[,] left, float[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new MatrixException("invalid addition (size miss-match).");
      Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), left.GetLength(1));
      for (int i = 0; i < result.Rows; i++)
        for (int j = 0; j < result.Columns; j++)
          result[i, j] = left[i, j] + right[i, j];
      return result;
    }

    /// <summary>Subtracts a scalar from all the values in a matrix.</summary>
    /// <param name="left">The matrix to have the values subtracted from.</param>
    /// <param name="right">The scalar to subtract from all the matrix values.</param>
    /// <returns>The resulting matrix after the subtractions.</returns>
    public static Matrix_2dArray Subtract(float[,] left, float[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        throw new MatrixException("invalid subtraction (size miss-match).");
      Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), left.GetLength(1));
      for (int i = 0; i < result.Rows; i++)
        for (int j = 0; j < result.Columns; j++)
          result[i, j] = left[i, j] - right[i, j];
      return result;
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
    public static Matrix_2dArray Multiply(float[,] left, float[,] right)
    {
      if (left.GetLength(1) != right.GetLength(0))
        throw new MatrixException("invalid multiplication (size miss-match).");
      Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), right.GetLength(1));
      for (int i = 0; i < result.Rows; i++)
        for (int j = 0; j < result.Columns; j++)
          for (int k = 0; k < left.GetLength(1); k++)
            result[i, j] += left[i, k] * right[k, j];
      return result;
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
    public static Matrix_2dArray Multiply(float[,] left, float right)
    {
      Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), left.GetLength(1));
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          result[i, j] = left[i, j] * right;
      return result;
    }

    /// <summary>Applies a power to a square matrix.</summary>
    /// <param name="matrix">The matrix to be powered by.</param>
    /// <param name="power">The power to apply to the matrix.</param>
    /// <returns>The resulting matrix of the power operation.</returns>
    public static Matrix_2dArray Power(float[,] matrix, int power)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new MatrixException("invalid power (!matrix.IsSquare).");
      if (!(power > -1))
        throw new MatrixException("invalid power !(power > -1)");
      if (power == 0)
        return Matrix_2dArray.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
      Matrix_2dArray result = Matrix_2dArray.Clone(matrix);
      for (int i = 0; i < power; i++)
        result *= result;
      return result;
    }

    /// <summary>Divides all the values in the matrix by a scalar.</summary>
    /// <param name="matrix">The matrix to divide the values of.</param>
    /// <param name="right">The scalar to divide all the matrix values by.</param>
    /// <returns>The resulting matrix with the divided values.</returns>
    public static Matrix_2dArray Divide(float[,] matrix, float right)
    {
      Matrix_2dArray result = new Matrix_2dArray(matrix.GetLength(0), matrix.GetLength(1));
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
    public static Matrix_2dArray Minor(float[,] matrix, int row, int column)
    {
      Matrix_2dArray minor = new Matrix_2dArray(matrix.GetLength(0) - 1, matrix.GetLength(1) - 1);
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

    /// <summary>Combines two matrices from left to right 
    /// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
    /// <param name="left">The left matrix of the concatenation.</param>
    /// <param name="right">The right matrix of the concatenation.</param>
    /// <returns>The resulting matrix of the concatenation.</returns>
    public static Matrix_2dArray ConcatenateRowWise(float[,] left, float[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0))
        throw new MatrixException("invalid row-wise concatenation !(left.Rows == right.Rows).");
      Matrix_2dArray result = new Matrix_2dArray(left.GetLength(0), left.GetLength(1) + right.GetLength(1));
      for (int i = 0; i < result.Rows; i++)
        for (int j = 0; j < result.Columns; j++)
        {
          if (j < left.GetLength(1)) result[i, j] = left[i, j];
          else result[i, j] = right[i, j - left.GetLength(1)];
        }
      return result;
    }

    /// <summary>Calculates the determinent of a square matrix.</summary>
    /// <param name="matrix">The matrix to calculate the determinent of.</param>
    /// <returns>The determinent of the matrix.</returns>
    public static float Determinent(float[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new MatrixException("invalid determinent !(matrix.IsSquare).");
      float det = 1.0f;
      try
      {
        Matrix_2dArray rref = Matrix_2dArray.Clone(matrix);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.Rows; j++)
              if (rref[j, i] != 0)
              {
                Matrix_2dArray.SwapRows(rref, i, j);
                det *= -1;
              }
          det *= rref[i, i];
          Matrix_2dArray.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.Rows; j++)
            Matrix_2dArray.RowAddition(rref, j, i, -rref[j, i]);
          for (int j = i - 1; j >= 0; j--)
            Matrix_2dArray.RowAddition(rref, j, i, -rref[j, i]);
        }
        return det;
      }
      catch (Exception) { throw new MatrixException("determinent computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix (aka REF).</summary>
    /// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
    /// <returns>The echelon of the matrix (aka REF).</returns>
    public static Matrix_2dArray Echelon(float[,] matrix)
    {
      try
      {
        Matrix_2dArray result = Matrix_2dArray.Clone(matrix);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.Rows; j++)
              if (result[j, i] != 0)
                Matrix_2dArray.SwapRows(result, i, j);
          if (result[i, i] == 0)
            continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.Rows; j++)
              if (result[j, i] == 1)
                Matrix_2dArray.SwapRows(result, i, j);
          Matrix_2dArray.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.Rows; j++)
            Matrix_2dArray.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new MatrixException("echelon computation failed."); }
    }

    /// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
    /// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
    /// <returns>The reduced echelon of the matrix (aka RREF).</returns>
    public static Matrix_2dArray ReducedEchelon(float[,] matrix)
    {
      try
      {
        Matrix_2dArray result = Matrix_2dArray.Clone(matrix);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (result[i, i] == 0)
            for (int j = i + 1; j < result.Rows; j++)
              if (result[j, i] != 0)
                Matrix_2dArray.SwapRows(result, i, j);
          if (result[i, i] == 0) continue;
          if (result[i, i] != 1)
            for (int j = i + 1; j < result.Rows; j++)
              if (result[j, i] == 1)
                Matrix_2dArray.SwapRows(result, i, j);
          Matrix_2dArray.RowMultiplication(result, i, 1 / result[i, i]);
          for (int j = i + 1; j < result.Rows; j++)
            Matrix_2dArray.RowAddition(result, j, i, -result[j, i]);
          for (int j = i - 1; j >= 0; j--)
            Matrix_2dArray.RowAddition(result, j, i, -result[j, i]);
        }
        return result;
      }
      catch { throw new MatrixException("reduced echelon calculation failed."); }
    }

    /// <summary>Calculates the inverse of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the inverse of.</param>
    /// <returns>The inverse of the matrix.</returns>
    public static Matrix_2dArray Inverse(float[,] matrix)
    {
      if (Matrix_2dArray.Determinent(matrix) == 0)
        throw new MatrixException("inverse calculation failed.");
      try
      {
        Matrix_2dArray identity = Matrix_2dArray.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
        Matrix_2dArray rref = Matrix_2dArray.Clone(matrix);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          if (rref[i, i] == 0)
            for (int j = i + 1; j < rref.Rows; j++)
              if (rref[j, i] != 0)
              {
                Matrix_2dArray.SwapRows(rref, i, j);
                Matrix_2dArray.SwapRows(identity, i, j);
              }
          Matrix_2dArray.RowMultiplication(identity, i, 1 / rref[i, i]);
          Matrix_2dArray.RowMultiplication(rref, i, 1 / rref[i, i]);
          for (int j = i + 1; j < rref.Rows; j++)
          {
            Matrix_2dArray.RowAddition(identity, j, i, -rref[j, i]);
            Matrix_2dArray.RowAddition(rref, j, i, -rref[j, i]);
          }
          for (int j = i - 1; j >= 0; j--)
          {
            Matrix_2dArray.RowAddition(identity, j, i, -rref[j, i]);
            Matrix_2dArray.RowAddition(rref, j, i, -rref[j, i]);
          }
        }
        return identity;
      }
      catch { throw new MatrixException("inverse calculation failed."); }
    }

    /// <summary>Calculates the adjoint of a matrix.</summary>
    /// <param name="matrix">The matrix to calculate the adjoint of.</param>
    /// <returns>The adjoint of the matrix.</returns>
    public static Matrix_2dArray Adjoint(float[,] matrix)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new MatrixException("Adjoint of a non-square matrix does not exists");
      Matrix_2dArray AdjointMatrix = new Matrix_2dArray(matrix.GetLength(0), matrix.GetLength(1));
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          if ((i + j) % 2 == 0)
            AdjointMatrix[i, j] = Matrix_2dArray.Determinent(Matrix_2dArray.Minor(matrix, i, j));
          else
            AdjointMatrix[i, j] = -Matrix_2dArray.Determinent(Matrix_2dArray.Minor(matrix, i, j));
      return Matrix_2dArray.Transpose(AdjointMatrix);
    }

    /// <summary>Returns the transpose of a matrix.</summary>
    /// <param name="matrix">The matrix to transpose.</param>
    /// <returns>The transpose of the matrix.</returns>
    public static Matrix_2dArray Transpose(float[,] matrix)
    {
      Matrix_2dArray transpose = new Matrix_2dArray(matrix.GetLength(1), matrix.GetLength(0));
      for (int i = 0; i < transpose.Rows; i++)
        for (int j = 0; j < transpose.Columns; j++)
          transpose[i, j] = matrix[j, i];
      return transpose;
    }

    /// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
    /// <param name="matrix">The matrix to decompose.</param>
    /// <param name="Lower">The computed lower triangular matrix.</param>
    /// <param name="Upper">The computed upper triangular matrix.</param>
    public static void DecomposeLU(float[,] matrix, out Matrix_2dArray Lower, out Matrix_2dArray Upper)
    {
      if (!(matrix.GetLength(0) == matrix.GetLength(1)))
        throw new MatrixException("The matrix is not square!");
      Lower = Matrix_2dArray.FactoryIdentity(matrix.GetLength(0), matrix.GetLength(1));
      Upper = Matrix_2dArray.Clone(matrix);
      int[] permutation = new int[matrix.GetLength(0)];
      for (int i = 0; i < matrix.GetLength(0); i++) permutation[i] = i;
      float p = 0, pom2, detOfP = 1;
      int k0 = 0, pom1 = 0;
      for (int k = 0; k < matrix.GetLength(1) - 1; k++)
      {
        p = 0;
        for (int i = k; i < matrix.GetLength(0); i++)
          if (Calc.Abs(Upper[i, k]) > p)
          {
            p = Calc.Abs(Upper[i, k]);
            k0 = i;
          }
        if (p == 0)
          throw new MatrixException("The matrix is singular!");
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
    public static Matrix_2dArray Clone(float[,] matrix)
    {
      Matrix_2dArray result = new Matrix_2dArray(matrix.GetLength(0), matrix.GetLength(1));
      for (int i = 0; i < matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(1); j++)
          result[i, j] = matrix[i, j];
      return result;
    }

    /// <summary>Does a value equality check.</summary>
    /// <param name="left">The first matrix to check for equality.</param>
    /// <param name="right">The second matrix to check for equality.</param>
    /// <returns>True if values are equal, false if not.</returns>
    public static bool EqualsByValue(float[,] left, float[,] right)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        return false;
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          if (left[i, j] != right[i, j])
            return false;
      return true;
    }

    /// <summary>Does a value equality check with leniency.</summary>
    /// <param name="left">The first matrix to check for equality.</param>
    /// <param name="right">The second matrix to check for equality.</param>
    /// <param name="leniency">How much the values can vary but still be considered equal.</param>
    /// <returns>True if values are equal, false if not.</returns>
    public static bool EqualsByValue(float[,] left, float[,] right, float leniency)
    {
      if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
        return false;
      for (int i = 0; i < left.GetLength(0); i++)
        for (int j = 0; j < left.GetLength(1); j++)
          if (Calc.Abs(left[i, j] - right[i, j]) > leniency)
            return false;
      return true;
    }

    /// <summary>Checks if two matrices are equal by reverences.</summary>
    /// <param name="left">The left matric of the equality check.</param>
    /// <param name="right">The right matrix of the equality check.</param>
    /// <returns>True if the references are equal, false if not.</returns>
    public static bool EqualsByReference(float[,] left, float[,] right)
    {
      return left.Equals(right);
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

    private class MatrixException : Exception
    {
      public MatrixException(string Message) : base(Message) { }
    }

    #region Alternate Compututation Methods

    //public static float Determinent(Matrix matrix)
    //{
    //  if (!matrix.IsSquare)
    //    throw new MatrixException("invalid determinent !(matrix.IsSquare).");
    //  return DeterminentRecursive(matrix);
    //}
    //private static float DeterminentRecursive(Matrix matrix)
    //{
    //  if (matrix.Rows == 1)
    //    return matrix[0, 0];
    //  float det = 0.0f;
    //  for (int j = 0; j < matrix.Columns; j++)
    //    det += (matrix[0, j] * DeterminentRecursive(Matrix.Minor(matrix, 0, j)) * (int)System.Math.Pow(-1, 0 + j));
    //  return det;
    //}

    //public static Matrix Inverse(Matrix matrix)
    //{
    //  float determinent = Matrix.Determinent(matrix);
    //  if (determinent == 0)
    //    throw new MatrixException("inverse calculation failed.");
    //  return Matrix.Adjoint(matrix) / determinent;
    //}

    #endregion
  }

  // This is the original version of matrices that I used in my engine. It only supported
  // 3x3 matrices, which obviously needed improvement.
  #region Matrix-OLD
  ///// <summary>An optimized matrix class for 3x3 tranfromation matrices only.</summary>
  //public struct Matrix3x3
  //{
  //  private float
  //    _r0c0, _r0c1, _r0c2,
  //    _r1c0, _r1c1, _r1c2,
  //    _r2c0, _r2c1, _r2c2;

  //  public float this[int row, int column]
  //  {
  //    get
  //    {
  //      switch (row)
  //      {
  //        case 0:
  //          switch (column)
  //          {
  //            case 0: return _r0c0;
  //            case 1: return _r0c1;
  //            case 2: return _r0c2;
  //          }
  //          break;
  //        case 1:
  //          switch (column)
  //          {
  //            case 0: return _r1c0;
  //            case 1: return _r1c1;
  //            case 2: return _r1c2;
  //          }
  //          break;
  //        case 2:
  //          switch (column)
  //          {
  //            case 0: return _r2c0;
  //            case 1: return _r2c1;
  //            case 2: return _r2c2;
  //          }
  //          break;
  //      }
  //      throw new MatrixException("index out of range.");
  //    }
  //    set
  //    {
  //      switch (row)
  //      {
  //        case 0:
  //          switch (column)
  //          {
  //            case 0: _r0c0 = value; return;
  //            case 1: _r0c1 = value; return;
  //            case 2: _r0c2 = value; return;
  //          }
  //          break;
  //        case 1:
  //          switch (column)
  //          {
  //            case 0: _r1c0 = value; return;
  //            case 1: _r1c1 = value; return;
  //            case 2: _r1c2 = value; return;
  //          }
  //          break;
  //        case 2:
  //          switch (column)
  //          {
  //            case 0: _r2c0 = value; return;
  //            case 1: _r2c1 = value; return;
  //            case 2: _r2c2 = value; return;
  //          }
  //          break;
  //      }
  //      throw new MatrixException("index out of range.");
  //    }
  //  }

  //  public Matrix3x3(
  //    float r0c0, float r0c1, float r0c2,
  //    float r1c0, float r1c1, float r1c2,
  //    float r2c0, float r2c1, float r2c2)
  //  {
  //    _r0c0 = r0c0; _r0c1 = r0c1; _r0c2 = r0c2;
  //    _r1c0 = r1c0; _r1c1 = r1c1; _r1c2 = r1c2;
  //    _r2c0 = r2c0; _r2c1 = r2c1; _r2c2 = r2c2;
  //  }

  //  public Matrix3x3(float[,] floatArray)
  //  {
  //    if (floatArray == null)
  //      throw new MatrixException("Attempting to create a matrix with an null float[,].");
  //    else if (floatArray.GetLength(0) != 3)
  //      throw new MatrixException("Attempting to create a matrix with an invalid sized float[,].");
  //    else if (floatArray.GetLength(1) != 3)
  //      throw new MatrixException("Attempting to create a matrix with an invalid sized float[,].");
  //    _r0c0 = floatArray[0, 0]; _r0c1 = floatArray[0, 1]; _r0c2 = floatArray[0, 2];
  //    _r1c0 = floatArray[1, 0]; _r1c1 = floatArray[1, 1]; _r1c2 = floatArray[1, 2];
  //    _r2c0 = floatArray[2, 0]; _r2c1 = floatArray[2, 1]; _r2c2 = floatArray[2, 2];
  //  }

  //  public static Matrix3x3 FactoryZero = new Matrix3x3(0, 0, 0, 0, 0, 0, 0, 0, 0);
  //  public static Matrix3x3 FactoryIdentity = new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);

  //  /// <param name="angle">Angle of rotation in radians.</param>
  //  public static Matrix3x3 FactoryRotationX(float angle)
  //  {
  //    float cos = Calc.Cos(angle);
  //    float sin = Calc.Sin(angle);
  //    return new Matrix3x3(1, 0, 0, 0, cos, sin, 0, -sin, cos);
  //  }

  //  /// <param name="angle">Angle of rotation in radians.</param>
  //  public static Matrix3x3 FactoryRotationY(float angle)
  //  {
  //    float cos = Calc.Cos(angle);
  //    float sin = Calc.Sin(angle);
  //    return new Matrix3x3(cos, 0, -sin, 0, 1, 0, sin, 0, cos);
  //  }

  //  /// <param name="angle">Angle of rotation in radians.</param>
  //  public static Matrix3x3 FactoryRotationZ(float angle)
  //  {
  //    float cos = Calc.Cos(angle);
  //    float sin = Calc.Sin(angle);
  //    return new Matrix3x3(cos, -sin, 0, sin, cos, 0, 0, 0, 1);
  //  }

  //  /// <param name="angleX">Angle about the X-axis in radians.</param>
  //  /// <param name="angleY">Angle about the Y-axis in radians.</param>
  //  /// <param name="angleZ">Angle about the Z-axis in radians.</param>
  //  public static Matrix3x3 FactoryRotationXthenYthenZ(float angleX, float angleY, float angleZ)
  //  {
  //    float
  //      xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
  //      yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
  //      zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
  //    return new Matrix3x3(
  //      yCos * zCos, -yCos * zSin, ySin,
  //      xCos * zSin + xSin * ySin * zCos, xCos * zCos + xSin * ySin * zSin, -xSin * yCos,
  //      xSin * zSin - xCos * ySin * zCos, xSin * zCos + xCos * ySin * zSin, xCos * yCos);
  //  }

  //  /// <param name="angleX">Angle about the X-axis in radians.</param>
  //  /// <param name="angleY">Angle about the Y-axis in radians.</param>
  //  /// <param name="angleZ">Angle about the Z-axis in radians.</param>
  //  public static Matrix3x3 FactoryRotationZthenYthenX(float angleX, float angleY, float angleZ)
  //  {
  //    float
  //      xCos = Calc.Cos(angleX), xSin = Calc.Sin(angleX),
  //      yCos = Calc.Cos(angleY), ySin = Calc.Sin(angleY),
  //      zCos = Calc.Cos(angleZ), zSin = Calc.Sin(angleZ);
  //    return new Matrix3x3(
  //      yCos * zCos, zCos * xSin * ySin - xCos * zSin, xCos * zCos * ySin + xSin * zSin,
  //      yCos * zSin, xCos * zCos + xSin * ySin * zSin, -zCos * xSin + xCos * ySin * zSin,
  //      -ySin, yCos * xSin, xCos * yCos);
  //  }

  //  /// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
  //  /// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
  //  /// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
  //  /// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
  //  /// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
  //  /// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
  //  public static Matrix3x3 FactoryShear(
  //    float shearXbyY, float shearXbyZ, float shearYbyX,
  //    float shearYbyZ, float shearZbyX, float shearZbyY)
  //  {
  //    return new Matrix3x3(
  //      1, shearYbyX, shearZbyX,
  //      shearXbyY, 1, shearYbyZ,
  //      shearXbyZ, shearYbyZ, 1);
  //  }

  //  public static Matrix3x3 operator +(Matrix3x3 left, Matrix3x3 right) { return left.Add(right); }
  //  public static Matrix3x3 operator -(Matrix3x3 left, Matrix3x3 right) { return left.Add(-right); }
  //  public static Matrix3x3 operator -(Matrix3x3 matrix) { return matrix.Negate(); }
  //  public static Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right) { return left.Multiply(right); }
  //  public static Vector operator *(Matrix3x3 matrix, Vector vector) { return matrix.Multiply(vector); }
  //  public static Matrix3x3 operator *(Matrix3x3 matrix, float scalar) { return matrix.Multiply(scalar); }
  //  public static Matrix3x3 operator /(Matrix3x3 matrix, float scalar) { return matrix.Divide(scalar); }
  //  public static Matrix3x3 operator ^(Matrix3x3 matrix, int power) { return matrix.Power(power); }
  //  public static implicit operator Matrix(Matrix3x3 matrix) { return Matrix3x3.ToMatrix(matrix); }
  //  public static implicit operator float[,](Matrix3x3 matrix) { return Matrix3x3.ToFloats(matrix); }

  //  public float Determinant
  //  {
  //    get
  //    {
  //      return
  //        _r0c0 * _r1c1 * _r2c2 -
  //        _r0c0 * _r1c2 * _r2c1 -
  //        _r0c1 * _r1c0 * _r2c2 +
  //        _r0c2 * _r1c0 * _r2c1 +
  //        _r0c1 * _r1c2 * _r2c0 -
  //        _r0c2 * _r1c1 * _r2c0;
  //    }
  //  }

  //  public bool EqualsApproximation(Matrix3x3 matrix, float tolerance)
  //  {
  //    return
  //      Calc.Abs(_r0c0 - matrix._r0c0) <= tolerance &&
  //      Calc.Abs(_r0c1 - matrix._r0c1) <= tolerance &&
  //      Calc.Abs(_r0c2 - matrix._r0c2) <= tolerance &&
  //      Calc.Abs(_r1c0 - matrix._r1c0) <= tolerance &&
  //      Calc.Abs(_r1c1 - matrix._r1c1) <= tolerance &&
  //      Calc.Abs(_r1c2 - matrix._r1c2) <= tolerance &&
  //      Calc.Abs(_r2c0 - matrix._r2c0) <= tolerance &&
  //      Calc.Abs(_r2c1 - matrix._r2c1) <= tolerance &&
  //      Calc.Abs(_r2c2 - matrix._r2c2) <= tolerance;
  //  }

  //  public Matrix3x3 Negate()
  //  {
  //    return new Matrix3x3(
  //      -_r0c0, -_r0c1, -_r0c2,
  //      -_r1c0, -_r1c1, -_r1c2,
  //      -_r2c0, -_r2c1, -_r2c2);
  //  }

  //  public Matrix3x3 Add(Matrix3x3 matrix)
  //  {
  //    return new Matrix3x3(
  //      _r0c0 + matrix._r0c0, _r0c1 + matrix._r0c1, _r0c2 + matrix._r0c2,
  //      _r1c0 + matrix._r1c0, _r1c1 + matrix._r1c1, _r1c2 + matrix._r1c2,
  //      _r2c0 + matrix._r2c0, _r2c1 + matrix._r2c1, _r2c2 + matrix._r2c2);
  //  }

  //  public Matrix3x3 Multiply(Matrix3x3 matrix)
  //  {
  //    return new Matrix3x3(
  //      matrix._r0c0 * _r0c0 + matrix._r0c1 * _r1c0 + matrix._r0c2 * _r2c0,
  //      matrix._r0c0 * _r0c1 + matrix._r0c1 * _r1c1 + matrix._r0c2 * _r2c1,
  //      matrix._r0c0 * _r0c2 + matrix._r0c1 * _r1c2 + matrix._r0c2 * _r2c2,
  //      matrix._r1c0 * _r0c0 + matrix._r1c1 * _r1c0 + matrix._r1c2 * _r2c0,
  //      matrix._r1c0 * _r0c1 + matrix._r1c1 * _r1c1 + matrix._r1c2 * _r2c1,
  //      matrix._r1c0 * _r0c2 + matrix._r1c1 * _r1c2 + matrix._r1c2 * _r2c2,
  //      matrix._r2c0 * _r0c0 + matrix._r2c1 * _r1c0 + matrix._r2c2 * _r2c0,
  //      matrix._r2c0 * _r0c1 + matrix._r2c1 * _r1c1 + matrix._r2c2 * _r2c1,
  //      matrix._r2c0 * _r0c2 + matrix._r2c1 * _r1c2 + matrix._r2c2 * _r2c2);
  //  }

  //  public Vector Multiply(Vector vector)
  //  {
  //    return new Vector(
  //      _r0c0 * vector.X + _r0c1 * vector.Y + _r0c2 * vector.Z,
  //      _r1c0 * vector.X + _r1c1 * vector.Y + _r1c2 * vector.Z,
  //      _r2c0 * vector.X + _r2c1 * vector.Y + _r2c2 * vector.Z);
  //  }

  //  public Matrix3x3 Multiply(float scalar)
  //  {
  //    return new Matrix3x3(
  //      scalar * _r0c0, scalar * _r0c1, scalar * _r0c2,
  //      scalar * _r1c0, scalar * _r1c1, scalar * _r1c2,
  //      scalar * _r2c0, scalar * _r2c1, scalar * _r2c2);
  //  }

  //  public Matrix3x3 Divide(float scalar)
  //  {
  //    return new Matrix3x3(
  //      _r0c0 / scalar, _r0c1 / scalar, _r0c2 / scalar,
  //      _r1c0 / scalar, _r1c1 / scalar, _r1c2 / scalar,
  //      _r2c0 / scalar, _r2c1 / scalar, _r2c2 / scalar);
  //  }

  //  public Matrix3x3 Power(int power)
  //  {
  //    if (power < 0)
  //      throw new MatrixException("Attempting to raise a matrix by a power less than zero. (can't do dat)");
  //    else if (power == 0)
  //      return FactoryIdentity;
  //    else
  //    {
  //      Matrix3x3 result = Clone();
  //      for (int i = 1; i < power; i++)
  //        result = result * result;
  //      return result;
  //    }
  //  }

  //  public Matrix3x3 Transpose()
  //  {
  //    return new Matrix3x3(
  //      _r0c0, _r1c0, _r2c0,
  //      _r0c1, _r1c1, _r2c1,
  //      _r0c2, _r1c1, _r2c2);
  //  }

  //  public Quaternion ToQuaternion()
  //  {
  //    float qX = (_r0c0 + _r1c1 + _r2c2 + 1.0f) / 4.0f;
  //    float qY = (_r0c0 - _r1c1 - _r2c2 + 1.0f) / 4.0f;
  //    float qZ = (-_r0c0 + _r1c1 - _r2c2 + 1.0f) / 4.0f;
  //    float qW = (-_r0c0 - _r1c1 + _r2c2 + 1.0f) / 4.0f;

  //    if (qX < 0.0f) qX = 0.0f;
  //    if (qY < 0.0f) qY = 0.0f;
  //    if (qZ < 0.0f) qZ = 0.0f;
  //    if (qW < 0.0f) qW = 0.0f;

  //    qX = Calc.SquareRoot(qX);
  //    qY = Calc.SquareRoot(qY);
  //    qZ = Calc.SquareRoot(qZ);
  //    qW = Calc.SquareRoot(qW);

  //    if (qX >= qY && qX >= qZ && qX >= qW)
  //    {
  //      qX *= +1.0f;
  //      qY *= Calc.Sin(_r2c1 - _r1c2);
  //      qZ *= Calc.Sin(_r0c2 - _r2c0);
  //      qW *= Calc.Sin(_r1c0 - _r0c1);
  //    }
  //    else if (qY >= qX && qY >= qZ && qY >= qW)
  //    {
  //      qX *= Calc.Sin(_r2c1 - _r1c2);
  //      qY *= +1.0f;
  //      qZ *= Calc.Sin(_r1c0 + _r0c1);
  //      qW *= Calc.Sin(_r0c2 + _r2c0);
  //    }
  //    else if (qZ >= qX && qZ >= qY && qZ >= qW)
  //    {
  //      qX *= Calc.Sin(_r0c2 - _r2c0);
  //      qY *= Calc.Sin(_r1c0 + _r0c1);
  //      qZ *= +1.0f;
  //      qW *= Calc.Sin(_r2c1 + _r1c2);
  //    }
  //    else if (qW >= qX && qW >= qY && qW >= qZ)
  //    {
  //      qX *= Calc.Sin(_r1c0 - _r0c1);
  //      qY *= Calc.Sin(_r2c0 + _r0c2);
  //      qZ *= Calc.Sin(_r2c1 + _r1c2);
  //      qW *= +1.0f;
  //    }
  //    else
  //      throw new MatrixException("There is a glitch in my my matrix to quaternion function. Sorry..");

  //    float length = Calc.SquareRoot(qX * qX + qY * qY + qZ * qZ + qW * qW);

  //    return new Quaternion(
  //      qX /= length,
  //      qY /= length,
  //      qZ /= length,
  //      qW /= length);
  //  }

  //  public Matrix3x3 Clone()
  //  {
  //    return new Matrix3x3(
  //      _r0c0, _r0c1, _r0c2,
  //      _r1c0, _r1c1, _r1c2,
  //      _r2c0, _r2c1, _r2c2);
  //  }

  //  public bool Equals(Matrix3x3 matrix)
  //  {
  //    return
  //      _r0c0 == matrix._r0c0 && _r0c1 == matrix._r0c1 && _r0c2 == matrix._r0c2 &&
  //      _r1c0 == matrix._r1c0 && _r1c1 == matrix._r1c1 && _r1c2 == matrix._r1c2 &&
  //      _r2c0 == matrix._r2c0 && _r2c1 == matrix._r2c1 && _r2c2 == matrix._r2c2;
  //  }

  //  public static Matrix ToMatrix(Matrix3x3 matrix)
  //  {
  //    Matrix result = new Matrix(3, 3);
  //    result[0, 0] = matrix._r0c0; result[0, 1] = matrix._r0c1; result[0, 2] = matrix._r0c2;
  //    result[1, 0] = matrix._r1c0; result[1, 1] = matrix._r1c1; result[1, 2] = matrix._r1c2;
  //    result[1, 0] = matrix._r2c0; result[2, 1] = matrix._r2c1; result[2, 2] = matrix._r2c2;
  //    return result;
  //  }

  //  public static float[,] ToFloats(Matrix3x3 matrix)
  //  {
  //    return new float[,]
  //      { { matrix[0, 0], matrix[0, 1], matrix[0, 2] },
  //      { matrix[1, 0], matrix[1, 1], matrix[1, 2] },
  //      { matrix[2, 0], matrix[2, 1], matrix[2, 2] } };
  //  }

  //  public override int GetHashCode()
  //  {
  //    return
  //      _r0c0.GetHashCode() ^ _r0c1.GetHashCode() ^ _r0c2.GetHashCode() ^
  //      _r1c0.GetHashCode() ^ _r1c1.GetHashCode() ^ _r1c2.GetHashCode() ^
  //      _r2c0.GetHashCode() ^ _r2c1.GetHashCode() ^ _r2c2.GetHashCode();
  //  }

  //  public override string ToString()
  //  {
  //    return base.ToString();
  //    //return
  //    //  _r0c0 + " " + _r0c1 + " " + _r0c2 + "\n" +
  //    //  _r1c0 + " " + _r1c1 + " " + _r1c2 + "\n" +
  //    //  _r2c0 + " " + _r2c1 + " " + _r2c2 + "\n";
  //  }

  //  private class MatrixException : Exception
  //  {
  //    public MatrixException(string message) : base(message) { }
  //  }
  //}
  #endregion
}


#endregion