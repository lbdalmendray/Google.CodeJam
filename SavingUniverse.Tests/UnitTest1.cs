using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SavingUniverse.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Solution.Solve(1, "CS"), 1);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(Solution.Solve(2, "CS"), 0);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(Solution.Solve(1, "SS"), -1);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(Solution.Solve(6, "SCCSSC"), 2);
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(Solution.Solve(2, "CC"), 0);
        }

        [TestMethod]
        public void TestMethod6()
        {
            Assert.AreEqual(Solution.Solve(3, "CSCSS"), 5);
        }

        [TestMethod]
        public void TestMethod7()
        {
            Assert.AreEqual(Solution.Solve(8, "CCCCSS"), 4);
        }

        [TestMethod]
        public void TestMethod8()
        {
            Assert.AreEqual(Solution.Solve(3, "CSCCCSS"), 9);
        }
    }
}
