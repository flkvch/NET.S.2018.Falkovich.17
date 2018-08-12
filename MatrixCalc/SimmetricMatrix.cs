using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class SimmetricMatrix<T> : Matrix<T>
    {
        private readonly T[] array;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimmetricMatrix{T}"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        public SimmetricMatrix(T[,] array) : base(array)
        {
            Size = array.GetUpperBound(0) + 1;
            this.array = new T[(Size + array.Length) / 2];
            for (int i = 0, k = 0; i < Size; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    this.array[k++] = array[i, j];
                }
            }
        }

        protected override T GetValue(int i, int j)
        {
            if (i < j) 
            {
                Swap(ref i, ref j);
            }

            return array[FindIndex(i, j)];
        }

        protected override void SetValue(T value, int i, int j)
        {
            if (i < j)
            {
                Swap(ref i, ref j);
            }

            array[FindIndex(i, j)] = value;
        }

        private int FindIndex(int i, int j)
        {
            int index = 0;
            for (int it = 0; it < Size; it++)
            {
                for (int jt = 0; jt <= it; jt++)
                {
                    if ((i == it) && (j == jt))
                    {
                        return index;
                    }

                    index++;
                }
            }

            return -1;
        }

        private void Swap(ref int i, ref int j)
        {
            int temp = i;
            i = j;
            j = temp;
        }
    }
}
