using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pylons;

namespace PylonsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LinkedList<Tuple<int, int>> outPut;
            var result = Solution.Solve(2, 2, out outPut);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            LinkedList<Tuple<int, int>> outPut;
            var result = Solution.Solve(2, 5, out outPut);
            Assert.IsTrue(result);
        }
    }
}
