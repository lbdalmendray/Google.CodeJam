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
            int L = int.Parse(stringParts[1]);
            LinkedList<string> liststring = new LinkedList<string>(); 
            for (int j = 0; j < N; j++)
            {
                liststring.AddLast(Console.ReadLine());
            }
           
            string result = Solve(liststring.ToArray(),L);

            string prefixString = "Case #" + i + ": ";

            Console.WriteLine(prefixString + result);
        }
    }

    private static string Solve(string[] words, int Length)
    {
        Dictionary<char, int>[] dictionaries = new Dictionary<char, int>[Length];
        Dictionary<int, char>[] dictionariesInverse = new Dictionary<int, char>[Length];
        int[] PositionCount = new int[Length];
        for (int i = 0; i < Length; i++)
        {
            dictionaries[i] = new Dictionary<char, int>();
            dictionariesInverse[i] = new Dictionary<int, char>();
        }

        for (int i = 0; i < Length; i++)
        {
            int number = 0;
            for (int j = 0; j < words.Length; j++)
            {
                if(!dictionaries[i].ContainsKey(words[j][i]))
                {
                    dictionaries[i].Add(words[j][i], number);
                    dictionariesInverse[i].Add(number, words[j][i]);
                    number++;
                }
            }
            PositionCount[i] = number;
        }

        /// FULL WORD LIST /////////////
        int product = 1;
        for (int i = 0; i < PositionCount.Length; i++)
        {
            product *= PositionCount[i];
        }
        if (product == words.Length)
            return "-";

        /////////////////////
        
        int[][] numbers = words.Select(s => s.Select((c, index) => dictionaries[index][c]).ToArray()).ToArray();
        numbers = SortNumbers(numbers, PositionCount);

        var lastNumber = new int[Length].Select((n, ii) => PositionCount[ii] - 1).ToArray();
        if (!NumberEqual(numbers[0], new int[Length]))
        {
            return NumberToString(new int[Length], dictionariesInverse);
        }
        else if (!NumberEqual(numbers[numbers.Length - 1], lastNumber))
            return NumberToString(lastNumber,dictionariesInverse);
        else
        {
            for (int i = 0; i < numbers.Length-1 ; i++)
            {
                int[] number1SumOne;
                if (ExistDifferenceGT1(numbers[i],numbers[i+1],PositionCount,out number1SumOne))
                {
                    return NumberToString(number1SumOne, dictionariesInverse);
                }
            }
        }

        return "-";
    }

    private static bool ExistDifferenceGT1(int[] number1, int[] number2, int[] PositionCount, out int [] number1SumOne)
    {
        number1SumOne = SumOne(number1, PositionCount);
        return !NumberEqual(number1SumOne, number2);
    }

    private static int[] SumOne(int[] number1, int[] PositionCount)
    {
        int[] newNumber = number1.Select(v => v).ToArray();
        int add = 1;
        for (int i = number1.Length-1; i >= 0; i--)
        {
            if ( newNumber[i] + add == PositionCount[i])
            {
                newNumber[i] = 0;
            }
            else
            {
                newNumber[i] += add;
                break;
            }
        }

        return newNumber;
    }

    static string NumberToString(int [] number , Dictionary<int, char>[] dictionariesInverse)
    {
        return new string(number.Select((n, ii) => dictionariesInverse[ii][n]).ToArray());
    }

    static private bool NumberEqual(int [] n1 , int [] n2)
    {
        for (int i = 0; i < n1.Length; i++)
        {
            if (n1[i] != n2[i])
                return false;
        }
        return true;
    }

    private static int[][] SortNumbers(int[][] numbers, int[] PositionCount)
    {
        for (int i = PositionCount.Length-1; i >=0 ; i--)
        {
            LinkedList<int>[] sorting = new LinkedList<int>[PositionCount[i]];
            for (int j = 0; j < sorting.Length; j++)
            {
                sorting[j] = new LinkedList<int>();
            }

            for (int j = 0; j < numbers.Length; j++)
            {
                sorting[numbers[j][i]].AddLast(j);
            }
            int[][] newNumbers = new int[numbers.Length].Select(e=>new int[numbers[0].Length]).ToArray();
            int posIndex = 0;
            for (int j = 0; j < sorting.Length; j++)
            {
                foreach (var index in sorting[j])
                {
                    newNumbers[posIndex] = numbers[index];
                    posIndex++;
                }
            }
            numbers = newNumbers;
        }

        return numbers;
    }

    private static int[][] SortNumbers2(int[][] numbers, int[] PositionCount)
    {
        for (int i = PositionCount.Length - 1; i >= 0; i--)
        {
            LinkedList<int>[] sorting = new LinkedList<int>[PositionCount[i]];
            for (int j = 0; j < sorting.Length; j++)
            {
                sorting[j] = new LinkedList<int>();
            }

            for (int j = 0; j < numbers.Length; j++)
            {
                sorting[numbers[j][i]].AddLast(j);
            }
            int[][] newNumbers = new int[numbers.Length].Select(e => new int[numbers[0].Length]).ToArray();
            int posIndex = 0;
            for (int j = 0; j < sorting.Length; j++)
            {
                foreach (var index in sorting[j])
                {
                    newNumbers[posIndex] = numbers[index];
                    posIndex++;
                }
            }
            numbers = newNumbers;
        }

        return numbers;
    }
}
