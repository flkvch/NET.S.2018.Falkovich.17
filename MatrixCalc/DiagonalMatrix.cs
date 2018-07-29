using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class DiagonalMatrix<T> : SimmetricMatrix<T>
    {
        public DiagonalMatrix(int nrows) : base(nrows)
        {

        }

        public DiagonalMatrix(T[,] array) : base(array)
        {

        }
    }
}
