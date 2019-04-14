using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouCanGoYourOwnWay;

namespace YouCanGoYourOwnWayTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string result = Solution.Solve(2, "SE");
            Assert.AreEqual("ES", result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string result = Solution.Solve(2, "ES");
            Assert.AreEqual("SE", result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            int N = 2;
            string Path = "ES";

            string result = Solution.Solve(N, Path);
            Assert.IsTrue(SameCount(result));

            var resultRelations = Solution2.CreateRelationsFromEastSouthCodification(result,N);
            var PathRelations = Solution2.CreateRelationsFromEastSouthCodification(Path, N);
            Assert.IsTrue(AreNotIntersectedPathRelations(resultRelations, PathRelations));
        }

        [TestMethod]
        public void TestMethod31()
        {
            int N = 3;
            string Path = "EESS";

            string result = Solution.Solve(N, Path);
            Assert.IsTrue(SameCount(result));

            var resultRelations = Solution2.CreateRelationsFromEastSouthCodification(result, N);
            var PathRelations = Solution2.CreateRelationsFromEastSouthCodification(Path, N);
            Assert.IsTrue(AreNotIntersectedPathRelations(resultRelations, PathRelations));
        }


        [TestMethod]
        public void TestMethod4()
        {
            int N = 5;
            string Path = "SEEESSES";

            string result = Solution.Solve(N, Path);
            Assert.IsTrue(SameCount(result));

            var resultRelations = Solution2.CreateRelationsFromEastSouthCodification(result, N);
            var PathRelations = Solution2.CreateRelationsFromEastSouthCodification(Path, N);
            Assert.IsTrue(AreNotIntersectedPathRelations(resultRelations, PathRelations));
        }

        [TestMethod]
        public void TestMethod5()
        {
            int N = 1000;
            Tuple<int, string> test = null;
            test = GenerateTest(N, 0);
            for (int i = 0; i < 100 ; i++)
            {
                
                try
                {
                   //test = GenerateTest(N, i * 10);
                }
                catch(Exception ee)
                {
                    Console.WriteLine("Generate Test Exception:" + ee.ToString());
                }
                if (test == null)
                    Assert.IsTrue(false);
                //int N = test.item1;
                string Path = test.Item2;

                string result = Solution.Solve(N, Path);
                var sameCount =SameCount(result);
                if (!sameCount)
                {
                    WriteProblemInstacenAndResult(Path, N, result);
                }

                Assert.IsTrue(sameCount);

                var resultRelations = Solution2.CreateRelationsFromEastSouthCodification(result, N);
                var PathRelations = Solution2.CreateRelationsFromEastSouthCodification(Path, N);

                var areNotIntersectedPathRelations = AreNotIntersectedPathRelations(resultRelations, PathRelations);

                if (!areNotIntersectedPathRelations)
                {
                    WriteProblemInstacenAndResult(Path, N, result);
                }
                Assert.IsTrue(areNotIntersectedPathRelations);

            }
        }

        [TestMethod]
        public void TestMethod6()
        {
            int N = 10000;
            for (int i = 0; i < 100; i++)
            {
                Tuple<int, string> test = null;
                try
                {
                    test = GenerateTest(N, i * 10);
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Generate Test Exception:" + ee.ToString());
                }
                if (test == null)
                    Assert.IsTrue(false);
                //int N = test.item1;
                string Path = test.Item2;

                string result = Solution.Solve(N, Path);
                var sameCount = SameCount(result);
                if (!sameCount)
                {
                    WriteProblemInstacenAndResult(Path, N, result);
                }

                Assert.IsTrue(sameCount);

                var resultRelations = Solution2.CreateRelationsFromEastSouthCodification(result, N);
                var PathRelations = Solution2.CreateRelationsFromEastSouthCodification(Path, N);

                var areNotIntersectedPathRelations = AreNotIntersectedPathRelations(resultRelations, PathRelations);

                if (!areNotIntersectedPathRelations)
                {
                    WriteProblemInstacenAndResult(Path, N, result);
                }
                Assert.IsTrue(areNotIntersectedPathRelations);

            }
        }

        public void WriteProblemInstacenAndResult(string Path,int N, string Result)
        {
            Console.WriteLine("Problem");
            Console.WriteLine("N: " + N);
            Console.WriteLine("Path: " + Path);
            Console.WriteLine("Result: " + Result);
        }

        public bool AreNotIntersectedPathRelations(LinkedList<int[]> resultRelations , LinkedList<int[]> PathRelations)
        {
            foreach (var resultRelation in resultRelations)
            {
                foreach (var pathRelation in PathRelations)
                {
                    if (resultRelation[0] == pathRelation[0] && resultRelation[1] == pathRelation[1])
                        return false;
                }
            }
            return true;
        }


        public bool SameCount(string result)
        {
            return result.Where(e => e == 'E').Count() == result.Where(e => e == 'S').Count();
        }

        public Tuple<int,string> GenerateTest(int N, int seed)
        {
            int eCount = N - 1;
            int sCount = N - 1;
            LinkedList<char> Path = new LinkedList<char>();
            for (int i = 0; i < 2*N-2 ; i++)
            {
                Random r = new Random(seed + i);
                if (r.Next(2) == 0)
                {
                    Path.AddLast('E');
                    eCount--;
                    if (eCount == 0)
                    {
                        while (sCount > 0)
                        {
                            Path.AddLast('S');
                            sCount--;
                        }
                        break;
                    }
                }
                else
                {
                    Path.AddLast('S');
                    sCount--;
                    if (sCount == 0)
                    {
                        while (eCount > 0)
                        {
                            Path.AddLast('E');
                            eCount--;
                        }
                        break;
                    }
                }
            }

            return new Tuple<int, string>(N, new string(Path.ToArray()));
        }
    }
}
