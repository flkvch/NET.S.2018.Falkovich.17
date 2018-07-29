using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class ElementChangeEventArgs<T> : EventArgs
    {
        public ElementChangeEventArgs(int i, int j, T elementWas, T elementBecame)
        {
            IndexI = i;
            IndexJ = j;
            ElementWas = elementWas;
            ElementBecame = elementBecame;
        }

        public int IndexI { get; }

        public int IndexJ { get; }

        public T ElementWas { get; }

        public T ElementBecame { get; }
    }
}
