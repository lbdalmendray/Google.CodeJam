using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cryptopangrams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptopangramsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var aaa = int.MaxValue; 
            bool isPrime = Solution.isPrime(9999);
            var lastElemnt = Solution.Primes(10000).ToArray();

            string input = "217 1891 4819 2291 2987 3811 1739 2491 4717 445 65 1079 8383 5353 901 187 649 1003 697 3239 7663 291 123 779 1007 3551 1943 2117 1679 989 3053";
            int [] productPrimes = Array.ConvertAll(input.Split(' '), int.Parse);
            var result = Solution.Solve(103, productPrimes);
            Assert.AreEqual(result, "CJQUIZKNOWBEVYOFDPFLUXALGORITHMS");
        }

        [TestMethod]
        public void TestMethod2()
        {
            string input = "3292937 175597 18779 50429 375469 1651121 2102 3722 2376497 611683 489059 2328901 3150061 829981 421301 76409 38477 291931 730241 959821 1664197 3057407 4267589 4729181 5335543";
            int[] productPrimes = Array.ConvertAll(input.Split(' '), int.Parse);
            var result = Solution.Solve(10000, productPrimes);

            GenerateInput("inputTest2.txt",10000,productPrimes);

            Assert.AreEqual(result, "SUBDERMATOGLYPHICFJKNQVWXZ");
        }

        [TestMethod]
        public void TestMethod3()
        {
            for (int j = 0; j < 100; j++)
            {
                string product = "99460729";
                string input = ""+ product;
                for (int i = 0; i < 99; i++)
                {
                    input += " "+product;
                }
                string response = "A";
                for (int i = 0; i < 100; i++)
                {
                    response += "A";
                }
                int[] productPrimes = Array.ConvertAll(input.Split(' '), int.Parse);
                var result = Solution.Solve(10000, productPrimes);

                GenerateInput("inputTest3.txt", 10000, productPrimes);


                Assert.AreEqual(result, response);
            }
        }

        [TestMethod]
        public void TestMethod31()
        {
            for (int j = 0; j < 100; j++)
            {
                string product = "99460729";
                string input = "" + product;
                for (int i = 0; i < 99; i++)
                {
                    input += " " + product;
                }
                string response = "A";
                for (int i = 0; i < 100; i++)
                {
                    response += "A";
                }
                int[] productPrimes = Array.ConvertAll(input.Split(' '), int.Parse);
                var result = Solution.Solve(9973, productPrimes);

                if (j == 0 )
                {
                    GenerateInput("inputTest31.txt", 9973, productPrimes);
                }

                Assert.AreEqual(result, response);
            }
        }

        [TestMethod]
        public void TestMethod4()
        {

            int[] primes = {13,17,19,23,31};
            Dictionary<char, int> letterPrime = new Dictionary<char, int>();
            int aInt = (int)'A';
            for (int i = 0; i < primes.Length; i++)
            {
                letterPrime.Add(Convert.ToChar(aInt + i), primes[i]);
            }
            for (int j = 0; j < 5; j++)
            {
                string response = Rotate("BADEC",j);

                int[] products = new int[response.Length - 1];

                for (int i = 0; i < response.Length-1; i++)
                {
                    products[i] = letterPrime[response[i]] * letterPrime[response[i + 1]];
                }

                string productsString = "" + products[0];
                for (int i = 1; i < products.Length; i++)
                {
                    productsString += " " + products[i];
                }

                var result = Solution.Solve(31, products);
                GenerateInput("inputTest4.txt", 31, products);

                Assert.AreEqual(result, response);
            }
        }

        [TestMethod]
        public void TestMethod5()
        {

            int[] primes = { 2,3,5,7,11,13, 17, 19, 23, 29, 31,37,41,43,47,53,59,61,67,71,73,79,83,89,97 ,101 };
            Dictionary<char, int> letterPrime = new Dictionary<char, int>();
            int aInt = (int)'A';
            for (int i = 0; i < primes.Length; i++)
            {
                letterPrime.Add(Convert.ToChar(aInt + i), primes[i]);
            }
            for (int j = 0; j < 25; j++)
            {
                string response = Rotate("ABCDEFGHIJKLMNOPQRSTUVWXYZ", j);

                int[] products = new int[response.Length - 1];

                for (int i = 0; i < response.Length - 1; i++)
                {
                    products[i] = letterPrime[response[i]] * letterPrime[response[i + 1]];
                }

                string productsString = "" + products[0];
                for (int i = 1; i < products.Length; i++)
                {
                    productsString += " " + products[i];
                }

                var result = Solution.Solve(101, products);
                GenerateInput("inputTest5.txt", 101, products);

                Assert.AreEqual(result, response);
            }
        }

        private string Rotate(string value, int position)
        {
            int rotation = position % value.Length;
            string result = "";
            for (int i = rotation; i < value.Length; i++)
            {
                result += value[i];
            }

            for (int i = 0; i < rotation; i++)
            {
                result += value[i];
            }

            return result; 
        }

        private void GenerateInput(string fileName, int N , int[] products)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            using(StreamWriter sw = new StreamWriter(fileName))
            {
                string productsString = "" + products[0];
                for (int i = 1; i < products.Length; i++)
                {
                    productsString += " " + products[i];
                }
                sw.WriteLine(1);
                sw.WriteLine(""+N + " " + products.Length);
                sw.WriteLine(productsString);
            }
        }
    }
}
