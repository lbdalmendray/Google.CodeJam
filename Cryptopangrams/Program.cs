using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptopangrams
{
    public class Solution
    {
        public static void Main(string[] args)
        {
            //.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            int T = int.Parse(Console.ReadLine());
            for (int i = 1; i <= T; i++)
            {
                string line = Console.ReadLine();
                var splitParts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if ( splitParts.All(splitPart=>splitPart.Length <= 5) )
                {
                    var numbers = Array.ConvertAll(splitParts, int.Parse);
                    int N = numbers[0];
                    int L = numbers[1];
                
                    if(N <= 10000)
                    { 
                        line = Console.ReadLine();                
                        var primeProducts = Array.ConvertAll(line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                        var result = Solve(N, primeProducts);
                        Console.WriteLine("Case #" + i.ToString() + ": " + result.ToString());
                    }
                    else
                    {
                        line = Console.ReadLine();
                        Console.WriteLine("Case #" + i.ToString() + ": " + "NORESULT");
                    }
                }
                else
                {
                    line = Console.ReadLine();
                    Console.WriteLine("Case #" + i.ToString() + ": " + "NORESULT");
                }
            }
        }

        public static string Solve(int N, int[] primeProducts)
        {
            var findPrimeResult = FindPrime(primeProducts, N);

            var primes = FindAllPrimes(findPrimeResult, primeProducts);

            var primesArray = primes.ToArray();
            Array.Sort(primesArray);
            var alphabet = primesArray.Distinct();
            Dictionary<int, char> dictionary = new Dictionary<int, char>();
            //int aLetterInt = (int)'A';
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int index = 0;
            foreach (var letter in alphabet)
            {
                dictionary.Add(letter, letters[index]);
                index++;
            }

            var result = primes.Select(e => dictionary[e]).ToArray();

            return new string(result);
        }

        public static string Solve2(int N, int[] primeProducts)
        {
            var findPrimeResult = FindPrime(primeProducts, N);

            var primes = FindAllPrimes(findPrimeResult, primeProducts);

            var primesArray = primes.ToArray();
            Array.Sort(primesArray);
            var alphabet = primesArray.Distinct();
            Dictionary<int, char> dictionary = new Dictionary<int, char>();
            int aLetterInt = (int)'A';

            int index = 0;
            foreach (var letter in alphabet)
            {
                dictionary.Add(letter, Convert.ToChar(aLetterInt + index));
                index++;
            }

            var result = primes.Select(e => dictionary[e]).ToArray();

            return new string(result);
        }

        private static IEnumerable<int> FindAllPrimes(Tuple<int, int> findPrimeResult, int[] primeProducts)
        {
            int prime1 = findPrimeResult.Item1;
            int prime2 = primeProducts[findPrimeResult.Item2] / prime1;

            int primeSelectedMiddlePart = -1;

            if ( findPrimeResult.Item2 > 0)
            {
                int index = findPrimeResult.Item2 - 1;

                if ( primeProducts[index] % prime1 == 0 )
                {
                    primeSelectedMiddlePart = prime1;
                }
                else
                {
                    primeSelectedMiddlePart = prime2;
                }
                int primeNextSelectePart = primeSelectedMiddlePart;
                
                while(index > -1)
                {
                    int primeSelectedFirstPart = primeProducts[index] / primeNextSelectePart;
                    yield return primeSelectedFirstPart;
                    primeNextSelectePart = primeSelectedFirstPart;
                    index--;
                }
            }
            else
            {
                primeSelectedMiddlePart = primeProducts[1] % prime1 != 0 ? prime1 : prime2;
            }

            yield return primeSelectedMiddlePart;

            if ( findPrimeResult.Item2 < primeProducts.Length-1)
            {
                int index = findPrimeResult.Item2 ;

                int primeBackSelectePart = primeSelectedMiddlePart;

                while (index < primeProducts.Length)
                {
                    int primeSelectedSecondPart = primeProducts[index] / primeBackSelectePart;
                    yield return primeSelectedSecondPart;
                    primeBackSelectePart = primeSelectedSecondPart;
                    index++;
                }
            }
        }

        static Tuple<int,int> FindPrime(int[] primeProducts,int N)
        {
            int firstPrimeProduct = primeProducts.First();
            foreach (var prime in Primes(N))
            {
                if ( firstPrimeProduct%prime == 0 )
                {
                    return new Tuple<int, int>(prime, 0);
                }
            }
            return new Tuple<int, int>(-1, -1);
        }

        public static IEnumerable<int> Primes(int N)
        {
            for (int i = 2; i <= N; i++)
            {
                if (isPrime(i))
                    yield return i;
            }
        }

        public static bool isPrime(int number)
        {
            var sqrt = Math.Sqrt(number);
            for (int i = 2; i <= sqrt; i++)
            {
                if (number % i  == 0)
                    return false;
            }

            return true;
        }
    }
}
