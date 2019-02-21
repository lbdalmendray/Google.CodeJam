using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphabetCake.Tests
{
    [TestClass]
    public class AlphabetCakeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            char[,] cake = new char[3, 3] 
            { 
                  { 'G', '?', '?' }
                , { '?', 'C', '?' }
                , { '?', '?', 'J' }
            };

            Solution.Solve(cake);

            char[,] cakeResult = new char[3, 3]
            {
                  { 'G', 'G', 'G' }
                , { 'C', 'C', 'C' }
                , { 'J', 'J', 'J' }
            };
            CollectionAssert.AreEqual(cake, cakeResult);

        }
    }
}
