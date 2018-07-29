using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    /// <summary>
    /// Matrix
    /// </summary>
    /// <typeparam name="T">
    /// Type of elemnts in matrix
    /// </typeparam>
    public class Matrix<T>
    {
        private readonly T[,] array;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
        /// </summary>
        /// <param name="nrows">The nrows.</param>
        /// <param name="ncolomns">The ncolomns.</param>
        /// <exception cref="ArgumentException"></exception>
        public Matrix(int nrows, int ncolomns)
        {
            if (nrows < 1 || ncolomns < 1) 
            {
                throw new ArgumentException($"{nrows} or {ncolomns} should be 1 or bigger.");
            }

            Nrows = nrows;
            Ncolomns = ncolomns;
            array = new T[Nrows, Ncolomns];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        public Matrix(T[,] array)
        {
            Nrows = array.GetUpperBound(0) + 1;
            Ncolomns = array.Length / Nrows;
            IsValidArray(array);
            this.array = array;
        }

        /// <summary>
        /// Occurs when [element change].
        /// </summary>
        public event EventHandler<ElementChangeEventArgs<T>> ElementChange;

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        /// <value>
        /// The nrows.
        /// </value>
        public int Nrows { get; }

        /// <summary>
        /// Gets the number of colomns.
        /// </summary>
        /// <value>
        /// The ncolomns.
        /// </value>
        public int Ncolomns { get; }

        #region Indexer        
        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified i.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">
        /// </exception>
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
        /// <summary>
        /// Called when [element change].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="ElementChangeEventArgs{T}"/> instance containing the event data.</param>
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
                if (Nrows != Ncolomns)
                {
                    throw new ArgumentException($"Can't build matrix form {array}.", nameof(array));
                }

                for (int i = 0; i < Nrows; i++)
                {
                    for (int j = i; j < Nrows; j++)
                    {
                        if (!Equals(array[i, j], array[j, i]))
                        {
                            throw new ArgumentException($"Can't build simmetric matrix form {array}.", nameof(array));
                        }
                    }
                }
            }

            if (GetType() == typeof(DiagonalMatrix<T>))
            {
                if (Nrows != Ncolomns)
                {
                    throw new ArgumentException($"Can't build matrix form {array}.", nameof(array));
                }

                for (int i = 0; i < Nrows; i++)
                {
                    for (int j = i + 1; j < Nrows; j++)
                    {
                        if (!(Equals(default(T), array[i, j])) || !Equals(default(T), array[j, i]))
                        {
                            throw new ArgumentException($"Can't build diagonal matrix form {array}.", nameof(array));
                        }
                    }
                }
            }
        }

        #region Overrides Object        
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < Nrows; i++)
            {
                for (int j = 0; j < Ncolomns; j++)
                {
                    if (array[i, j] == null)
                    {
                        result += "null" + " ";
                    }
                    else
                    {
                        result += array[i, j].ToString() + " ";
                    }
                }

                if (i != Nrows - 1) 
                {
                    result += "\n";
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null) 
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
                    if (!Equals(matrix.array[i, j], array[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int result = 0;
            for (int i = 0; i < Nrows; i++)
            {
                for (int j = 0; j < Ncolomns; j++)
                {
                    result += array[i, j].GetHashCode() * i * j;
                }
            }

            return result;
        }
        #endregion
    }
}
