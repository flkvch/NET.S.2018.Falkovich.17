using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class DiagonalMatrix<T> : Matrix<T>
    {
        private readonly T[] array;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagonalMatrix{T}"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        public DiagonalMatrix(T[,] array) : base(array)
        {
            Size = array.GetUpperBound(0) + 1;
            this.array = new T[Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i == j) 
                    {
                        this.array[i] = array[i, j];
                    }
                }
            }
        }

        protected override T GetValue(int i, int j)
        {
            if (i == j) 
            {
                return array[i];
            }
            else
            {
                return default(T);
            }
        }

        protected override void SetValue(T value, int i, int j)
        {
            if (i == j)
            {
                array[i] = value;
            }
            else
            {
                throw new ArgumentException("Can't set element that don't belong to diagonal.");
            }
        }
    }
}
