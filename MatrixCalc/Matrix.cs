using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class Matrix<T>
    {
        readonly T[,] array;
        public event EventHandler<ElementChangeEventArgs<T>> ElementChange;

        public Matrix(int nrows, int ncolomns)
        {
            if(nrows < 1 || ncolomns < 1)
            {
                throw new ArgumentException($"{nrows} or {ncolomns} should be 1 or bigger.");
            }

            Nrows = nrows;
            Ncolomns = ncolomns;
            array = new T[Nrows, Ncolomns];
        }

        public Matrix(T[,] array)
        {
            Nrows = array.GetUpperBound(0) + 1;
            Ncolomns = array.Length / Nrows;
            IsValidArray(array);
            this.array = array;
        }

        public int Nrows { get; }

        public int Ncolomns { get; }

        #region Indexer
        public T this[int i, int j]
        {
            get
            {
                if (i >= Nrows && j >= Ncolomns)
                {
                    throw new IndexOutOfRangeException();
                }

                return array[i, j];
            }
            set
            {
                if (i >= Nrows && j >= Ncolomns)
                {
                    throw new IndexOutOfRangeException();
                }

                T elementWas = array[i, j];
                array[i, j] = value;

                OnElementChange(this, new ElementChangeEventArgs<T>(i, j, elementWas, value));
            }
        }
        #endregion

        #region Event
        private void OnElementChange(object source, ElementChangeEventArgs<T> e)
        {
            ElementChange?.Invoke(this, e);
        }
        #endregion

        private void IsValidArray(T[,] array)
        {
            if (GetType() == typeof(SquareMatrix<T>))
            {
                if (Nrows != Ncolomns)
                {
                    throw new ArgumentException($"Can't build matrix form {array}.", nameof(array));
                }
            }

            if (GetType() == typeof(SimmetricMatrix<T>))
            {
                for (int i = 0; i < Nrows; i++)
                {
                    for (int j = i; j < Nrows; j++)
                    {
                        if (!array[i, j].Equals(array[j, i]))
                        {
                            throw new ArgumentException($"Can't build simmetric matrix form {array}.", nameof(array));
                        }
                    }
                }
            }

            if (GetType() == typeof(DiagonalMatrix<T>))
            {
                for (int i = 0; i < Nrows; i++)
                {
                    for (int j = i + 1; j < Nrows; j++)
                    {
                        if (!array[i, j].Equals(default(T)) || !array[j, i].Equals(default(T)))
                        {
                            throw new ArgumentException($"Can't build diagonal matrix form {array}.", nameof(array));
                        }
                    }
                }
            }
        }

        #region Overrides Object
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Nrows; i++)
            {
                for (int j = 0; j < Ncolomns; j++)
                {
                    result += array[i, j].ToString() + " ";
                }

                if(i != Nrows - 1)
                {
                    result += "\n";
                }
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            Matrix<T> matrix = obj as Matrix<T>;
            if (matrix == null)
            {
                return false;
            }

            if (Nrows != matrix.Nrows || Ncolomns != matrix.Ncolomns) 
            {
                return false;
            }

            for (int i = 0; i < Nrows; i++) 
            {
                for (int j = 0; j < Ncolomns; j++)
                {
                    if (array[i, j].Equals(matrix.array[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            for (int i = 0; i < Nrows; i++)
            {
                for (int j = 0; j < Ncolomns; j++)
                {
                    result += array[i, j].GetHashCode() * i* j;
                }
            }

            return result;
        }
        #endregion
    }
}
