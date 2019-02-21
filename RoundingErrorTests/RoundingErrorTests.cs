using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoundingErrorTests
{
    [TestClass]
    public class RoundingErrorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Solution.Round(10.5), 11);
            Assert.AreEqual(Solution.Round(10.1), 10);
            Assert.AreEqual(Solution.Round(10.2), 10);
            Assert.AreEqual(Solution.Round(10.3), 10);
            Assert.AreEqual(Solution.Round(10.4), 10);
            Assert.AreEqual(Solution.Round(10.6), 11);
            Assert.AreEqual(Solution.Round(10.7), 11);
            Assert.AreEqual(Solution.Round(10.8), 11);

        }
    }
}
