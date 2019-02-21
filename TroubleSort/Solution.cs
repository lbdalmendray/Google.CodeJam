using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public static void Main(string[] args)
    {
        var line = Console.ReadLine();
        int T = int.Parse(line);
        for (int i = 1; i <= T; i++)
        {
            line = Console.ReadLine();
            int N = int.Parse(line);

            int[] numbers = ReadNumbers(N, () => (char)Console.Read());

            var result = Solve(numbers);
            string prefixString = "Case #" + i + ": ";
            if (result == -1)
                Console.WriteLine(prefixString + "OK");
            else Console.WriteLine(prefixString + result);
        }        
    }
    
    public static int [] ReadNumbers(int N, Func<char> CharRead)
    {
        string[] splitParts = Console.ReadLine().Split(' ');
        var result = splitParts.Select(sp => int.Parse(sp)).ToArray();
        return result;
    }

    public static int[] ReadNumbers2(int N, Func<char> CharRead)
    {
        int[] result = new int[N];

        for (int i = 0; i < N; i++)
        {
            result[i] = ReadNumber(CharRead);
        }

        return result;
    }

    private static int ReadNumber(Func<char> CharRead)
    {
        LinkedList<char> charNumber = new LinkedList<char>();
        char lastRead = CharRead();
        while(char.IsDigit (lastRead))
        {
            charNumber.AddLast(lastRead);
            lastRead = CharRead();
        }
        var stringValue = new string(charNumber.ToArray());
        return int.Parse(stringValue);
    }

    public static int Solve(int [] numbers)
    {
        int[] numbers1 = new int[(int)Math.Ceiling(((double)numbers.Length) / 2)];
        int[] numbers2 = new int[numbers.Length / 2];

        for (int i = 0, index = 0; i < numbers.Length; i+=2, index++)
        {
            numbers1[index] = numbers[i];
        }

        for (int i = 1, index = 0; i < numbers.Length; i += 2, index++)
        {
            numbers2[index] = numbers[i];
        }

        Array.Sort(numbers1);
        Array.Sort(numbers2);

        for (int i = 0, index = 0; i < numbers.Length; i += 2, index++)
        {
            numbers[i] = numbers1[index];
        }

        for (int i = 1, index = 0; i < numbers.Length; i += 2, index++)
        {
            numbers[i] = numbers2[index];
        }

        for (int i = 0; i < numbers.Length-1; i++)
        {
            if ( numbers[i] > numbers[i+1])
            {
                return i;
            }
        }
        return -1;
    }
}
