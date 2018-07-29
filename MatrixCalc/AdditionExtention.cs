using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public static class AdditionExtention
    {
        public static Matrix<T> Add<T>(this Matrix<T> lhs, Matrix<T> rhs, Func<T, T, T> addition)
        {
            if (!(lhs.Ncolomns == rhs.Ncolomns) || !(lhs.Nrows == rhs.Nrows))
            {
                throw new InvalidOperationException($"Cant't add \n{lhs} \nto \n{rhs}: matrises have different sizes.");
            }

            T[,] array = new T[rhs.Nrows, rhs.Ncolomns];
            for (int i = 0; i < rhs.Nrows; i++)
            {
                for (int j = 0; j < rhs.Ncolomns; j++)
                {
                    array[i, j] = addition(lhs[i, j], rhs[i, j]);
                }
            }

            return new Matrix<T>(array);
        }
    }
}
