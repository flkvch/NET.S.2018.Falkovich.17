using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class SquareMatrix<T> : Matrix<T>
    {
        public SquareMatrix(int nrows) : base(nrows, nrows)
        {

        }

        public SquareMatrix(T[,] array) : base(array)
        {

        }
    }
}
