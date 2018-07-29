using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class ElementChangeListener<T>
    {
        private void Message(object sender, ElementChangeEventArgs<T> e)
        {
            Console.WriteLine($"Element [{e.IndexI},{e.IndexJ}] has changed from {e.ElementWas} to {e.ElementBecame}. ");
        }

        public void Register(Matrix<T> matrix)
        {
            matrix.ElementChange += Message;
            Console.WriteLine($"The subscriber registered.");
        }

        public void Unregister(Matrix<T> matrix)
        {
            matrix.ElementChange -= Message;
            Console.WriteLine($"The subscriber unregistered.");
        }
    }
}
