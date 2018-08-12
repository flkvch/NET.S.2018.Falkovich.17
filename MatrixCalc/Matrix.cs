using System;
using System.Collections;
using System.Collections.Generic;

namespace MatrixCalc
{
    /// <summary>
    /// Matrix
    /// </summary>
    /// <typeparam name="T">
    /// Type of elemnts in matrix
    /// </typeparam>
    public abstract class Matrix<T> : IEnumerable<T>
    {
        public Matrix(T[,] array)
        {
            IsValidArray(array);
        }

        /// <summary>
        /// Occurs when [element change].
        /// </summary>
        public event EventHandler<ElementChangeEventArgs<T>> ElementChange;

        public int Size { get; protected set;  }

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
                if (i >= Size || j >= Size || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                return GetValue(i, j);
            }

            set
            {
                if (i >= Size || j >= Size || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                T elementWas = GetValue(i, j);
                SetValue(value, i, j);

                OnElementChange(this, new ElementChangeEventArgs<T>(i, j, elementWas, value));
            }
        }

        protected abstract T GetValue(int i, int j);

        protected abstract void SetValue(T value, int i, int j);
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

        #region Validation of array???
        private void IsValidArray(T[,] array)
        {
            int nrows = array.GetUpperBound(0) + 1;
            int ncolomns = array.Length / (array.GetUpperBound(0) + 1);
            if (GetType() == typeof(SquareMatrix<T>))
            {
                if (nrows != ncolomns)
                {
                    throw new ArgumentException($"Can't build matrix form {array}.", nameof(array));
                }
            }

            if (GetType() == typeof(SimmetricMatrix<T>))
            {
                if (nrows != ncolomns)
                {
                    throw new ArgumentException($"Can't build matrix form {array}.", nameof(array));
                }

                for (int i = 0; i < nrows; i++)
                {
                    for (int j = i; j < nrows; j++)
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
                if (nrows != ncolomns)
                {
                    throw new ArgumentException($"Can't build matrix form {array}.", nameof(array));
                }

                for (int i = 0; i < nrows; i++)
                {
                    for (int j = i + 1; j < nrows; j++)
                    {
                        if (!Equals(default(T), array[i, j]) || !Equals(default(T), array[j, i]))
                        {
                            throw new ArgumentException($"Can't build diagonal matrix form {array}.", nameof(array));
                        }
                    }
                }
            }
        }
        #endregion

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
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (this[i, j] == null)
                    {
                        result += "null" + " ";
                    }
                    else
                    {
                        result += this[i, j].ToString() + " ";
                    }
                }

                if (i != Size - 1)
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

            if (Size != matrix.Size)
            {
                return false;
            }

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!Equals(matrix[i, j], this[i, j]))
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
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    result += this[i, j].GetHashCode() * i * j;
                }
            }

            return result;
        }
        #endregion

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    yield return this[i, j];
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
