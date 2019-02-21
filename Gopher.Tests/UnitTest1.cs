using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gopher.Tests
{
    [TestClass]
    public class UnitTest1
    {
        int columnCenter = 500;
        int rowMin ;
        int rowMax;
        int seed = 0;
        [TestMethod]
        public void TestMethod2()
        {
            LinkedList<string> inputs = new LinkedList<string>();
            bool[,] matrix = null;
            GopherManager gopherManager = new GopherManager(3, delegate ()
            {
                var result = inputs.First.Value; inputs.RemoveFirst(); return result;
            }, delegate (string output)
            {
                int[] numbers = GetNumbers(output);
                int i = numbers[0];
                int j = numbers[1];
                Assert.AreNotEqual(i, 1);
                Assert.AreNotEqual(j, 1);

                if (i <= rowMin || i >= rowMax || j != columnCenter)
                    throw new Exception();

                var newT = GetRandom(new Tuple<int, int>(i, j));
                int iM = newT.Item1 - rowMin;
                int jM = newT.Item2 == 499 ? 0 : (newT.Item2 == 500 ? 1 : 2);
                matrix[iM, jM] = true;

                if (AllFalse(matrix))
                {
                    inputs.AddLast("0 0");
                }
                else
                {
                    inputs.AddLast(newT.Item1 + " " + newT.Item2);
                }

            });

            rowMin = gopherManager.MinRow;
            rowMax = gopherManager.MaxRow;
            columnCenter = gopherManager.ColumnCenter;
            matrix = new bool[rowMax - rowMin + 1, 3];

            gopherManager.Start();
            Assert.IsTrue(gopherManager.Finished);

        }

        public int [] GetNumbers (string input)
        {
            string[] parts = input.Split(' ');
            return parts.Select(s => int.Parse(s)).ToArray();
        }

        public Tuple<int, int> GetRandom(Tuple<int, int> ij)
        {
                Tuple<int, int>[] values = new Tuple<int, int>[]
            {
                new Tuple<int, int>(ij.Item1,ij.Item2),
                new Tuple<int, int>(ij.Item1+1,ij.Item2),
                new Tuple<int, int>(ij.Item1-1,ij.Item2),
                new Tuple<int, int>(ij.Item1,ij.Item2+1),
                new Tuple<int, int>(ij.Item1,ij.Item2-1),
                new Tuple<int, int>(ij.Item1+1,ij.Item2+1),
                new Tuple<int, int>(ij.Item1+1,ij.Item2-1),
                new Tuple<int, int>(ij.Item1-1,ij.Item2+1),
                new Tuple<int, int>(ij.Item1-1,ij.Item2-1),
                };

                Random random = new Random(seed++);
                var index = random.Next(9);
                return values[index];
        }

        //[TestMethod]
        public void TestMethod1()
        {
            int inputCount = 0;
            bool[,] matrix = new bool[3, 3];
            LinkedList<string> outputs = new LinkedList<string>();
            LinkedList<string> inputs = new LinkedList<string>();
            GopherManager gopherManager = new GopherManager(3, () => GetInput(inputs), 
                delegate(string output) 
            {
                var spliParts = output.Split(' ');
                var numbers = spliParts.Select(s => int.Parse(s)).ToArray();
                var column = numbers[1] - 500 == -1 ? 0 : (numbers[1] - 500 == 0 ? 1 : 2);
                var row = numbers[0];
                var result = "";
                if ( matrix[row-1,column] )
                {
                    if (!matrix[row - 1, 0])
                    {
                        result = "" + row + " " + 499;
                        matrix[row - 1, 0] = true;
                    }
                    else if (!matrix[row - 1, 1])
                    {
                        result = "" + row + " " + 500;
                        matrix[row - 1, 1] = true;
                    }
                    else
                    {
                        result = "" + row + " " + 501;
                        matrix[row - 1, 2] = true;
                    }
                }
                else
                {
                    result = "" + row + " " + (column == 0 ? 499 : (column == 1 ? 500 : 501));
                    matrix[row - 1, column] = true;
                }

                if (AllFalse(matrix))
                    inputs.AddLast("0 0");
                else
                {
                    inputs.AddLast(result);
                    inputCount++;
                }
            });

            gopherManager.Start();
            Assert.IsTrue(gopherManager.Finished);
        }

        private bool AllFalse(bool[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!matrix[i, j])
                        return false;
                }
            }
            return true;
        }

        string GetInput(LinkedList<string> inputs)
        {
            var node = inputs.First;
            string result = node.Value;
            inputs.Remove(node);
            return result;
        }
    }
}
