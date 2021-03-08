using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2sharp;

namespace UnitTestProject
{
    [TestClass]
    public class SolveSystemUnitTests
    {
        /* The system has no solutions: code 0 */

        [TestMethod]
        public void TestMethod_Code0_T1()
        {
            //Arrange
            double a = 0, b = 0, c = 0, d = 0, e = 1, f = 2;
            string expectedResult = "0";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        /* The system has infinitely many solutions, each of which has the form y=kx+n: code 1 */

        [TestMethod]
        public void TestMethod_Code1_T1()
        {
            //Arrange
            double a = 0, b = 0, c = 1, d = 2, e = 0, f = 3;
            string expectedResult = "1 -0,5 1,5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code1_T2()
        {
            //Arrange
            double a = 1, b = 2, c = 0, d = 0, e = 3, f = 0;
            string expectedResult = "1 -0,5 1,5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code1_T3()
        {
            //Arrange
            double a = 1, b = 2, c = 2, d = 4, e = 0, f = 0;
            string expectedResult = "1 -0,5 0";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        /* The system has a single solution (x0, y0): code 2 */

        [TestMethod]
        public void TestMethod_Code2_T1()
        {
            //Arrange
            double a = 1, b = 2, c = 3, d = 4, e = 5, f = 6;
            string expectedResult = "2 -4 4,5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code2_T2()
        {
            //Arrange
            double a = 1, b = 1, c = 1, d = 2, e = 0, f = 0;
            string expectedResult = "2 0 0";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code2_T3()
        {
            //Arrange
            double a = 1, b = 20, c = 0, d = 8, e = 0, f = 0;
            string expectedResult = "2 0 0";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code2_T4()
        {
            //Arrange
            double a = 0, b = 8, c = 1, d = 20, e = 0, f = 0;
            string expectedResult = "2 0 0";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        /* The system has infinitely many solutions of the form x = x0, y - any: code 3 */

        [TestMethod]
        public void TestMethod_Code3_T1()
        {
            //Arrange
            double a = 0, b = 0, c = 4, d = 0, e = 0, f = 2;
            string expectedResult = "3 0,5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code3_T2()
        {
            //Arrange
            double a = 6, b = 0, c = 0, d = 0, e = 3, f = 0;
            string expectedResult = "3 0,5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code3_T3()
        {
            //Arrange
            double a = 10, b = 0, c = 7, d = 0, e = 0, f = 0;
            string expectedResult = "3 0";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code3_T4()
        {
            //Arrange
            double a = 1, b = 0, c = 1, d = 0, e = 1, f = 1;
            string expectedResult = "3 1";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        /* The system has infinitely many solutions of the form y = y0, x - any: code 4 */

        [TestMethod]
        public void TestMethod_Code4_T1()
        {
            //Arrange
            double a = 0, b = 0, c = 0, d = 4, e = 0, f = 2;
            string expectedResult = "4 0,5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code4_T2()
        {
            //Arrange
            double a = 0, b = 6, c = 0, d = 0, e = 3, f = 0;
            string expectedResult = "4 0,5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code4_T3()
        {
            //Arrange
            double a = 0, b = 6, c = 0, d = 5, e = 0, f = 0;
            string expectedResult = "4 0";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        [TestMethod]
        public void TestMethod_Code4_T4()
        {
            //Arrange
            double a = 0, b = 8, c = 0, d = 16, e = 100, f = 200;
            string expectedResult = "4 12,5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }

        /* Any pair of numbers (x, y) is a solution: code 5 */

        [TestMethod]
        public void TestMethod_Code5_T1()
        {
            //Arrange
            double a = 0, b = 0, c = 0, d = 0, e = 0, f = 0;
            string expectedResult = "5";
            string receivedResult;

            //Act
            receivedResult = Program.SolveSystem(a, b, c, d, e, f);

            //Assert
            Assert.AreEqual(expectedResult, receivedResult);
        }
    }
}
