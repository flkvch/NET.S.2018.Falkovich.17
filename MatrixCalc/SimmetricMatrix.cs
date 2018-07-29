using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class SimmetricMatrix<T> : SquareMatrix<T>
    {
        public SimmetricMatrix(int nrows) : base(nrows)
        {

        }

        public SimmetricMatrix(T[,] array) : base(array)
        {
                     
        }
    }
}
