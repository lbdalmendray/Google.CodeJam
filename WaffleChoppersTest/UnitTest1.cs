using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaffleChoppersTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] waffles = new string[]
            {
                ".@@..@",
                ".....@",
                "@.@.@@"
            };

            Assert.IsTrue(Solution.Solve(waffles, 3, 6, 1, 1)); 
;        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] waffles = new string[]
            {
               "@@@",
               "@.@",
               "@.@",
               "@@@"
            };

            Assert.IsFalse(Solution.Solve(waffles, 4, 3, 1, 1));
            ;
        }

        [TestMethod]
        public void TestMethod3()
        {
            string[] waffles = new string[]
            {
               ".....",
               ".....",
               ".....",
               "....."
            };

            Assert.IsTrue(Solution.Solve(waffles, 4, 5, 1, 1));
            ;
        }

        [TestMethod]
        public void TestMethod4()
        {
            string[] waffles = new string[]
            {
               "..@@",
               "..@@",
               "@@..",
               "@@.."
            };

            Assert.IsFalse(Solution.Solve(waffles, 4, 4, 1, 1));
            ;
        }

        [TestMethod]
        public void TestMethod5()
        {
            string[] waffles = new string[]
            {
               "@.@@",
               "@@.@",
               "@.@@"
            };

            Assert.IsTrue(Solution.Solve(waffles, 3, 4, 2, 2));
            ;
        }
    }
}
