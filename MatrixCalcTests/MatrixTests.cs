using NUnit.Framework;
using System;

namespace MatrixCalc.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        private int[,] array0 = new int[,]
           {
                { 1, 0, 0, 7, 3 },
                { 0, 2, 0, 8, 4 },
                { 0, 0, 3, 0, 1 },
                { 0, 8, 0, 5, 8 },
                { 0, 1, 0, 5, 7 },
                { 0, 1, 0, 5, 7 }
           };

        private int[,] array = new int[,]
            {
                { 1, 0, 0, 7, 3 },
                { 0, 2, 0, 8, 4 },
                { 0, 0, 3, 0, 1 },
                { 0, 8, 0, 5, 8 },
                { 0, 1, 0, 5, 7 }
            };

        private int[,] array2 = new int[,]
        {
                { 1, 0, 0},
                { 0, 2, 0 },
                { 0, 0, 3}
        };

        private int[,] array3 = new int[,]
        {
                { 10, 0, 0, 0, 0 },
                {  0, 5, 0, 0, 0 },
                {  0, 0, 1, 0, 0 },
                {  0, 0, 0, 7, 0 },
                { 0, 0, 0, 0, 12 }
        };

        private int[,] array4 = new int[,]
        {
                { 1, 0, 2, 0, 0 },
                {  0, 1, 0, 5, 0 },
                {  2, 0, 1, 0, 0 },
                {  0, 5, 0, 1, 10 },
                { 0, 0, 0, 10, 1 }
        };


        private int[,] array5 = new int[,]
        {
                { 2, 0, 2, 7, 3 },
                {  0, 3, 0, 13, 4 },
                {  2, 0, 4, 0, 1 },
                {  0, 13, 0, 6, 18 },
                { 0, 1, 0, 15, 8 }
        };


        private int[,] array6 = new int[,]
        {
                { 2, 0, 4, 0, 0 },
                {  0, 2, 0, 10, 0 },
                {  4, 0, 2, 0, 0 },
                {  0, 10, 0, 2, 20 },
                { 0, 0, 0, 20, 2 }
        };


        private string[,] notSquareStringArray = new string[,]
            {
            {"a", null, "d", null },
            {null, "b",  null, null },
            {"d", null, "c" , null}
            };

        private string[,] diagonalStringArray = new string[,]
        {
            {"a", null, "y" },
            {null, "b",  null },
            {null, null, "c" }
        };

        private string[,] simmetricStringArray = new string[,]
        {
            {"a", null, "d" },
            {null, "b",  null },
            {"d", "u", "c" }
        };

        private double[,] darray = new double[,]
        {
                { 1.2, 0, 0},
                { 0, 2.2, 8.2 },
                { 0, 8.2, 3.5}
         };

        private double[,] darray2 = new double[,]
        {
                { 2.4, 0, 0},
                { 0, 4.4, 16.4 },
                { 0, 16.4, 7}
        };

        [Test]
        public void SquareMatrixTest()
        {
            Matrix<int> matrix;
            Matrix<string> matrixString;
            Assert.Throws<ArgumentException>(() => matrix = new SquareMatrix<int>(array0));
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
            Assert.AreEqual(new SquareMatrix<int>(array5), new SquareMatrix<int>(array).Add(new SimmetricMatrix<int>(array4)));
            Assert.AreEqual(new SimmetricMatrix<int>(array6), new SimmetricMatrix<int>(array4).Add(new SimmetricMatrix<int>(array4)));
            Assert.AreEqual(new SimmetricMatrix<double>(darray2), new SimmetricMatrix<double>(darray).Add(new SimmetricMatrix<double>(darray)));
        }

        [Test]
        public void AdditionFailTest()
        {
            Assert.Throws<InvalidOperationException>(() => new SquareMatrix<int>(array2).Add(new SquareMatrix<int>(array4)));
        }
    }
}