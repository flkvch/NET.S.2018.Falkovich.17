using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class ElementChangeListener<T>
    {
        /// <summary>
        /// Registers the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public void Register(Matrix<T> matrix)
        {
            matrix.ElementChange += Message;
            Console.WriteLine($"The subscriber registered.");
        }

        /// <summary>
        /// Unregisters the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public void Unregister(Matrix<T> matrix)
        {
            matrix.ElementChange -= Message;
            Console.WriteLine($"The subscriber unregistered.");
        }

        private void Message(object sender, ElementChangeEventArgs<T> e)
        {
            Console.WriteLine($"Element [{e.IndexI},{e.IndexJ}] has changed from {e.ElementWas} to {e.ElementBecame}. ");
        }
    }
}
