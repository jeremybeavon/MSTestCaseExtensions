using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTestCaseExtensions
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [ProvidesTestCases]
        [DataSource("MSTestCaseExtensions", "UnitTest1", "TestAdd", DataAccessMethod.Sequential)]
        public void TestAdd()
        {
        }

        [TestCase(1, 10, 11)]
        [TestCase(2, 20, 22)]
        [TestCase(3, 30, 33)]
        [TestCase(4, 40, 44)]
        [TestCase(5, 50, 55)]
        public void TestAdd(int value1, int value2, int expectedResult)
        {
            Assert.AreEqual(expectedResult, value1 + value2);
        }
    }
}
