using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2sharp;

namespace UnitTestProject
{
    [TestClass]
    public class ConsoleInputUnitTests
    {
        /* Entering a string instead of double */
        [TestMethod]
        public void TestMethod_ReceivedString()
        {
            //Arrange
            string received = "aaa";
            double receivedResult;
            bool err = false;

            //Act
            try
            {
                receivedResult = Program.GetDoubleFromConsole(received);
            }
            catch
            {
                err = true;
            }

            //Assert
            Assert.IsFalse(err);
        }

        /* Enter a double with exp */
        [TestMethod]
        public void TestMethod_ReceivedDoubleWithExp()
        {
            //Arrange
            string received = "1e-35";
            double expectedResult = 1E-35;
            double receivedResult = new double();
            bool err = false;

            //Act
            try
            {
                receivedResult = Program.GetDoubleFromConsole(received);
            }
            catch
            {
                err = true;
            }

            //Assert
            Assert.IsFalse(err);
            Assert.AreEqual(expectedResult, receivedResult);
        }

        /* Entering an integer instead of double */

        [TestMethod]
        public void TestMethod_ReceivedInt()
        {
            //Arrange
            string received = "1";
            double expectedResult = 1.0;
            double receivedResult = new double();
            bool err = false;

            //Act
            try
            {
                receivedResult = Program.GetDoubleFromConsole(received);
            }
            catch
            {
                err = true;
            }

            //Assert
            Assert.IsFalse(err);
            Assert.AreEqual(expectedResult, receivedResult);
        }

        /* Entering a double with a comma separator */

        [TestMethod]
        public void TestMethod_ReceivedDoubleWithComma()
        {
            //Arrange
            string received = "1,0";
            double expectedResult = 1.0;
            double receivedResult = new double();
            bool err = false;

            //Act
            try
            {
                receivedResult = Program.GetDoubleFromConsole(received);
            }
            catch
            {
                err = true;
            }

            //Assert
            Assert.IsFalse(err);
            Assert.AreEqual(expectedResult, receivedResult);
        }

        /* Entering a double with a dot separator */

        [TestMethod]
        public void TestMethod_ReceivedDoubleWithDot()
        {
            //Arrange
            string received = "1.0";
            double expectedResult = 1.0;
            double receivedResult = new double();
            bool err = false;

            //Act
            try
            {
                receivedResult = Program.GetDoubleFromConsole(received);
            }
            catch
            {
                err = true;
            }

            //Assert
            Assert.IsFalse(err);
            Assert.AreEqual(expectedResult, receivedResult);
        }
    }
}
