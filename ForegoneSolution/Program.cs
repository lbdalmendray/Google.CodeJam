using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForegoneSolution
{
    public class Solution
    {
        static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());
            for (int i = 1; i <= T; i++)
            {
                string Nstring = Console.ReadLine();
                var N = int.Parse(Nstring);

                var result = Solve(Nstring);
                Console.WriteLine("Case #" + i.ToString() + ": " + result.ToString());

            }
        }

        public static string Solve(string Nstring)
        {
            char[] A = new char[Nstring.Length];
            char[] B = new char[Nstring.Length];
            for (int i = 0; i < Nstring.Length; i++)
            {
                if ( Nstring[i]=='4')
                {
                    A[i] = '2';
                    B[i] = '2';
                }
                else
                {
                    A[i] = Nstring[i];
                    B[i] = '0';
                }
            }
            int index = 0;
            while(B[index]=='0')
            {
                index++;
            }

            if(index > 0)
                B = B.Skip(index).ToArray();

            string result = new string(Sum(A, B).ToArray());
            return result;           
        }

        public static IEnumerable<char> Sum(char [] A ,  char [] B)
        {
            foreach (var aElement in A)
            {
                yield return aElement;
            }

            yield return ' ';

            foreach (var bElement in B)
            {
                yield return bElement;
            }
        }
    }
}
