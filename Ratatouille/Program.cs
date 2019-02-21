using System;
using System.Collections.Generic;

public class Solution
{
    public static void Main(string[] args)
    {
        var line = Console.ReadLine();
        int T = int.Parse(line);
        for (int i = 1; i <= T; i++)
        {
            var splitParts = Console.ReadLine().Split(' ');
            int N = int.Parse(splitParts[0]);
            int P = int.Parse(splitParts[1]);

            splitParts = Console.ReadLine().Split(' ');
            var recipe = new int[N];
            for (int j = 0; j < N; j++)
            {
                recipe[j] = int.Parse(splitParts[j]);
            }

            int[][] recipePackages = new int[N][];

            for (int j = 0; j < N; j++)
            {
                splitParts = Console.ReadLine().Split(' ');
                recipePackages[j] = new int[P];
                for (int k = 0; k <P; k++)
                {
                    recipePackages[j][k] = int.Parse(splitParts[k]);
                }
            }

            int result = Solve(recipe, recipePackages);
            string prefixString = "Case #" + i + ": ";
            Console.WriteLine(prefixString + result);
        }
    }

    private static int Solve(int[] recipe, int[][] recipePackages)
    {
        throw new NotImplementedException();
    }
}
