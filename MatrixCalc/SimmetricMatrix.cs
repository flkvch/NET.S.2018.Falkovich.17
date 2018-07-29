using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class SimmetricMatrix<T> : SquareMatrix<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimmetricMatrix{T}"/> class.
        /// </summary>
        /// <param name="nrows">The nrows.</param>
        public SimmetricMatrix(int nrows) : base(nrows)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimmetricMatrix{T}"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        public SimmetricMatrix(T[,] array) : base(array)
        {                     
        }
    }
}
