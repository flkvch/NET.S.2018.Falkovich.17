using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class SquareMatrix<T> : Matrix<T>
    {
        private readonly T[] array;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix{T}"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public SquareMatrix(T[,] array) : base(array)
        {
            Size = array.GetUpperBound(0) + 1;
            this.array = new T[array.Length];
            int k = 0;
            foreach (T i in array) 
            {
                this.array[k++] = i;
            }           
        }

        protected override T GetValue(int i, int j)
        {
            return array[(i * Size) + j];
        }

        protected override void SetValue(T value, int i, int j)
        {
            array[(i * Size) + j] = value;
        }
    }
}
