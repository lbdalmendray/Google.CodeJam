using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public static void Main(string[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 1; i <= T; i++)
        {
            var stringParts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int N = int.Parse(stringParts[0]);
            int L = int.Parse(stringParts[0]);

            stringParts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            LinkedList<int> knowLanguageCount = new LinkedList<int>();
            for (int j = 0; j < stringParts.Length; j++)
            {
                knowLanguageCount.AddLast(int.Parse(stringParts[j]));
            }
            int result = Solve(N, knowLanguageCount);

            string prefixString = "Case #" + i + ": ";

            Console.WriteLine(prefixString + result);
        }
    }

    public static int Solve(int N, LinkedList<int> knowLanguageCountList)
    {
        if (100 % N == 0)
            return 100;
        
        int sum = knowLanguageCountList.Sum();
        int deltaInt = N - sum;
        int[] knowLanguageCountListArray = knowLanguageCountList.ToArray();

        int[] knowLanguageCount = new int[knowLanguageCountList.Count + deltaInt].Select((v, i) => i < knowLanguageCountListArray.Length ? knowLanguageCountListArray[i] : 0).ToArray();

        if (deltaInt == 0)
            return knowLanguageCount.Sum(e => Round(e));

        /*
         int[,] calculating = new int[knowLanguageCount.Length + 1, deltaInt+1];
        bool[,] isCalculated = new bool[knowLanguageCount.Length + 1, deltaInt+1];
        return Calculate(0, deltaInt, calculating, isCalculated,knowLanguageCount,N);
        */

        int[,] calculating = new int[knowLanguageCount.Length + deltaInt, deltaInt + 1];
        bool[,] isCalculated = new bool[knowLanguageCount.Length + deltaInt, deltaInt + 1];
        return Calculate(0, deltaInt, calculating, isCalculated, knowLanguageCount, N);

    }

    public static int Calculate(int index,int value , int[,] calculating, bool[,] isCalculated, int[] knowLanguageCount, int N)
    {
        if (index >= knowLanguageCount.Length)
            return 0;
        
        if ( isCalculated[index,value])
        {
            return calculating[index, value];
        }

        if ( value == 0 )
        {
            calculating[index, 0] = Round(((double)(knowLanguageCount[index]) * 100) / N);
            calculating[index, 0] += Calculate(index + 1, 0, calculating, isCalculated, knowLanguageCount, N); ;
            isCalculated[index, 0] = true;
            return calculating[index, 0];
        }

        for (int i = 0; i <= value; i++)
        {
            var auxValue = Round(((double)(knowLanguageCount[index] + i) * 100) / N);
            auxValue += Calculate(index + 1, value - i, calculating, isCalculated, knowLanguageCount, N);
            if ( auxValue > calculating[index,value])
            {
                calculating[index, value] = auxValue;
            }
        }        

        isCalculated[index, value] = true;
        return calculating[index, value];
    }

    public static int Round ( double number )
    {
        double number1 = Math.Floor(number);
        double delta = number - number1;
        if (delta < 0.5)
            return (int)number1;
        else
            return ((int)number1 + 1);
    }

}
