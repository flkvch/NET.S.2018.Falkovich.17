using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class ElementChangeEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementChangeEventArgs{T}"/> class.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="elementWas">The element was.</param>
        /// <param name="elementBecame">The element became.</param>
        public ElementChangeEventArgs(int i, int j, T elementWas, T elementBecame)
        {
            IndexI = i;
            IndexJ = j;
            ElementWas = elementWas;
            ElementBecame = elementBecame;
        }

        /// <summary>
        /// Gets the index i.
        /// </summary>
        /// <value>
        /// The index i.
        /// </value>
        public int IndexI { get; }

        /// <summary>
        /// Gets the index j.
        /// </summary>
        /// <value>
        /// The index j.
        /// </value>
        public int IndexJ { get; }

        /// <summary>
        /// Gets the element was.
        /// </summary>
        /// <value>
        /// The element was.
        /// </value>
        public T ElementWas { get; }

        /// <summary>
        /// Gets the element became.
        /// </summary>
        /// <value>
        /// The element became.
        /// </value>
        public T ElementBecame { get; }
    }
}
