using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TroubleSort.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Solution.Solve(new int[] { 5, 6, 8, 4, 3 }), -1);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(Solution.Solve(new int[] { 8, 9, 7 }), 1);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(Solution.Solve(new int[] { 8, 9, 7, 6 }), 0);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(Solution.Solve(new int[] { 5, 6, 8, 4, 3, 9 }), -1);
        }
    }
}
