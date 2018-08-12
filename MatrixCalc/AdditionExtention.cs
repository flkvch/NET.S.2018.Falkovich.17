using System;
using System.Linq.Expressions;

namespace MatrixCalc
{
    /// <summary>
    /// Add Addtition Operation to Matrix
    /// </summary>
    public static class AdditionExtention
    {
        /// <summary>
        /// Adds the specified RHS.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <param name="addition">The addition.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Matrix<T> Add<T>(this Matrix<T> lhs, Matrix<T> rhs)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException($"{nameof(lhs)} and {nameof(rhs)} can't be null");
            }

            if (lhs.Size != rhs.Size)
            {
                throw new InvalidOperationException($"Cant't add \n{lhs} \nto \n{rhs}: matrises have different sizes.");
            }

            T[,] array = new T[rhs.Size, rhs.Size];
            for (int i = 0; i < rhs.Size; i++)
            {
                for (int j = 0; j < rhs.Size; j++)
                {
                    array[i, j] = AddFunc(lhs[i, j], rhs[i, j]);
                }
            }

            if (lhs.GetType() == typeof(SimmetricMatrix<T>) || rhs.GetType() == typeof(SimmetricMatrix<T>))
            {
                if (lhs.GetType() != typeof(SquareMatrix<T>) && rhs.GetType() != typeof(SquareMatrix<T>))
                {
                    return new SimmetricMatrix<T>(array);
                }
            }

            if (lhs.GetType() == typeof(DiagonalMatrix<T>) && rhs.GetType() == typeof(DiagonalMatrix<T>))
            {
                return new DiagonalMatrix<T>(array);
            }

            return new SquareMatrix<T>(array);
        }

        private static T AddFunc<T>(T lhs, T rhs)
        {
            ParameterExpression plhs = Expression.Parameter(typeof(T), "lhs");
            ParameterExpression prhs = Expression.Parameter(typeof(T), "rhs");

            Func<T, T, T> add = Expression.Lambda<Func<T, T, T>>(Expression.Add(plhs, prhs), plhs, prhs).Compile();

            return add(lhs, rhs);
        }
    }
}
