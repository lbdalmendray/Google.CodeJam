using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroubleSortConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateNumbersTxt();
            using (StreamReader sr = new StreamReader("Numbers.txt"))
            {
                int T = int.Parse(sr.ReadLine());
                for (int i = 1; i <= T; i++)
                {
                    var line = sr.ReadLine();
                    int N = int.Parse(line);

                    int[] numbers = Solution.ReadNumbers(N, () => (char)sr.Read());

                    var result = Solution.Solve(numbers);
                    string prefixString = "Case #" + i + ": ";
                    if (result == -1)
                        Console.WriteLine(prefixString + "OK");
                    else Console.WriteLine(prefixString + result);
                }
                Console.ReadLine();
            }
        }

        static void CreateNumbersTxt()
        {
            char[] numberChar = new char[8] { '1', '0', '0', '0', '0', '0', '0', '9' };
            char[] numbers = new char[100000 * 9 + 100000 - 1];
            int currentIndex = 8;
            numbers[0] = '1';
            numbers[1] = '0';
            numbers[2] = '0';
            numbers[3] = '0';
            numbers[4] = '0';
            numbers[5] = '0';
            numbers[6] = '0';
            numbers[7] = '9';

            int countNumber = 0;
            for (; countNumber < 100000;  countNumber++)
            {
                    numbers[currentIndex++] = ' ';
                    for (int k = 0; k < 8; k++)
                    {
                        numbers[currentIndex++] = numberChar[k];
                    }
                    
            }
            if (File.Exists("Numbers.txt"))
                File.Delete("Numbers.txt");
            using (StreamWriter sw = new StreamWriter("Numbers.txt"))
            {

                sw.WriteLine("1");
                sw.WriteLine("100000");
                sw.WriteLine(numbers);
            }
        }

        static void CreateNumbersTxt2()
        {
            char[] numberChar = new char[8] {  '0', '0', '0', '0', '0', '0', '0', '9' };
            char[] numbers = new char[100000 * 9 + 100000 - 1];
            int currentIndex = 8;
            numbers[0] = '9';
            numbers[1] = '9';
            numbers[2] = '9';
            numbers[3] = '9';
            numbers[4] = '9';
            numbers[5] = '9';
            numbers[6] = '9';
            numbers[7] = '9';

            int countNumber = 0;
            for (int i = 0; i < 9; i++)
            {
                if (countNumber == 100000)
                    break;

                for (int j = 0; j < 10 ; j++)
                {
                    if (countNumber == 100000)
                        break;

                    numberChar[i] = j.ToString()[0];
                    numbers[currentIndex++] = ' ';
                    for (int k = 0; k < 9; k++)
                    {
                        numbers[currentIndex++] = numberChar[i]; 
                    }

                    countNumber++;
                }
            }
            if (File.Exists("Numbers.txt"))
                File.Delete("Numbers.txt");
            using (StreamWriter sw = new StreamWriter("Numbers.txt"))
            {

                sw.WriteLine("1");
                sw.WriteLine("100000");
                sw.WriteLine(numbers);
            }
        }
    }
}
