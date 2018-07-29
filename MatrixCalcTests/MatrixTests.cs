using NUnit.Framework;
using MatrixCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.Tests
{
    [TestFixture()]
    public class MatrixTests
    {
        int[,] array = new int[,]
            {
                { 1, 0, 0, 7, 3 },
                { 0, 2, 0, 8, 4 },
                { 0, 0, 3, 0, 1 },
                { 0, 8, 0, 5, 8 },
                { 0, 1, 0, 5, 7 }
            };

        int[,] array2 = new int[,]
        {
                { 1, 0, 0, 7 },
                { 0, 2, 0, 8 },
                { 0, 0, 3, 0},
                { 0, 8, 0, 5},
                { 0, 1, 0, 5 }
        };

        int[,] array3 = new int[,]
        {
                { 10, 0, 0, 0, 0 },
                {  0, 5, 0, 0, 0 },
                {  0, 0, 1, 0, 0 },
                {  0, 0, 0, 7, 0 },
                { 0, 0, 0, 0, 12 }
        };

        int[,] array4 = new int[,]
        {
                { 1, 0, 2, 0, 0 },
                {  0, 1, 0, 5, 0 },
                {  2, 0, 1, 0, 0 },
                {  0, 5, 0, 1, 10 },
                { 0, 0, 0, 10, 1 }
        };


        int[,] array5 = new int[,]
        {
                { 2, 0, 2, 7, 3 },
                {  0, 3, 0, 13, 4 },
                {  2, 0, 4, 0, 1 },
                {  0, 13, 0, 6, 18 },
                { 0, 1, 0, 15, 8 }
        };

        string[,] notSquareStringArray = new string[,]
{
            {"a", null, "d", null },
            {null, "b",  null, null },
            {"d", null, "c" , null}
};

        string[,] diagonalStringArray = new string[,]
        {
            {"a", null, "y" },
            {null, "b",  null },
            {null, null, "c" }
        };

        string[,] simmetricStringArray = new string[,]
        {
            {"a", null, "d" },
            {null, "b",  null },
            {"d", "u", "c" }
        };

        string[,] additionArray = new string[,]
        {
            {"aa", null, "dy" },
            {null, "bb",  null },
            {"d", "u", "cc" }
        };

        [Test]
        public void SquareMatrixTest()
        {
            Matrix<int> matrix;
            Matrix<string> matrixString;
            Assert.Throws<ArgumentException>(() => matrix = new SquareMatrix<int>(array2));
            Assert.Throws<ArgumentException>(() => matrixString = new SquareMatrix<string>(notSquareStringArray));
        }

        [Test]
        public void DiagonalMatrixTest()
        {
            Matrix<int> matrix;
            Matrix<string> matrixString;
            Assert.Throws<ArgumentException>(() => matrix = new DiagonalMatrix<int>(array4));
            Assert.Throws<ArgumentException>(() => matrixString = new DiagonalMatrix<string>(diagonalStringArray));

        }

        [Test]
        public void SimmetriclMatrixTest()
        {
            Matrix<int> matrix;
            Matrix<string> matrixString;
            Assert.Throws<ArgumentException>(() => matrix = new SimmetricMatrix<int>(array));
            Assert.Throws<ArgumentException>(() => matrixString = new SimmetricMatrix<string>(simmetricStringArray));
        }

        [Test]
        public void AdditionTest()
        {
            Assert.AreEqual(new Matrix<int>(array5), new Matrix<int>(array).Add(new Matrix<int>(array4), (x, y) => x + y));
            Assert.AreEqual(new Matrix<string>(additionArray), new Matrix<string>(simmetricStringArray).Add(new Matrix<string>(diagonalStringArray), StringAdd));
        }

        [Test]
        public void AdditionFailTest()
        {
            Assert.Throws<InvalidOperationException>(() => new Matrix<int>(array2).Add(new Matrix<int>(array4), (x, y) => x + y));
        }

        private string StringAdd (string lhs, string rhs)
        {
            if (lhs == null && rhs == null)
            {
                return null;
            }

            return lhs + rhs;
        }

    }
}