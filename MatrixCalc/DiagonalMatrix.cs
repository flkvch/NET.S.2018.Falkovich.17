using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class DiagonalMatrix<T> : SimmetricMatrix<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiagonalMatrix{T}"/> class.
        /// </summary>
        /// <param name="nrows">The nrows.</param>
        public DiagonalMatrix(int nrows) : base(nrows)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagonalMatrix{T}"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        public DiagonalMatrix(T[,] array) : base(array)
        {
        }
    }
}
