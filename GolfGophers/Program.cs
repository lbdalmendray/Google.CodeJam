using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGophers
{
    public class Solution
    {
        public static void Main(string[] args)
        {
            var numbers = Array.ConvertAll( Console.ReadLine().Split(' ') , int.Parse);
            int T = numbers[0];
            int N = numbers[1];
            int M = numbers[2];
            for (int i = 1; i <= T; i++)
            {
                int[] results = new int[N];
                for (int j = 0; j < N ; j++)
                {
                    var request = MakeRequest(18, 18);
                    Console.WriteLine(request);
                    string result = Console.ReadLine();
                    if(result =="-1")
                    {
                        i = T + 1;
                        break;
                    }
                    numbers = Array.ConvertAll(result.Split(' '), int.Parse);
                    results[j] += numbers.Sum();
                }               
                Console.WriteLine(Average(results));
                string result2 = Console.ReadLine();
                if (result2 == "-1")
                {
                    break;
                }
            }
        }

        private static double Average(int[] results)
        {
            double result = results.Sum();
            return result / (double)results.Length;
        }

        private static string MakeRequest(int count, int element)
        {
            var result = "";
            result += element.ToString();
            for (int i = 0; i < count-1; i++)
            {
                result += " " + element;
            }

            return result;
        }
    }
}
