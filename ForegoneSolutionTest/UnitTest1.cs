using System;
using ForegoneSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForegoneSolutionTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int number = 444444;
            var result = Solution.Solve(number.ToString());
            var splitParts = result.Split(' ');
            int A = int.Parse(splitParts[0]);
            int B = int.Parse(splitParts[1]);

            Assert.AreEqual(A + B, number);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int number = 123456789;
            var result = Solution.Solve(number.ToString());
            var splitParts = result.Split(' ');
            int A = int.Parse(splitParts[0]);
            int B = int.Parse(splitParts[1]);

            Assert.AreEqual(A + B, number);
        }


        [TestMethod]
        public void TestMethod3()
        {
            int number = 4;
            var result = Solution.Solve(number.ToString());
            var splitParts = result.Split(' ');
            int A = int.Parse(splitParts[0]);
            int B = int.Parse(splitParts[1]);

            Assert.AreEqual(A + B, number);
        }
    }
}
