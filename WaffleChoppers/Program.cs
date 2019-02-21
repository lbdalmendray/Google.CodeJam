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
            var splitParts = Console.ReadLine().Split(' ');
            int R = int.Parse(splitParts[0]);
            int C = int.Parse(splitParts[1]);
            int H = int.Parse(splitParts[2]);
            int V = int.Parse(splitParts[3]);

            string[] waffles = new string[R];

            for (int j = 0; j < R; j++)
            {
                waffles[j] = Console.ReadLine();
            }

            bool result = Solve(waffles, R, C, H, V);

            string prefixString = "Case #" + i + ": ";
            if (!result)
                Console.WriteLine(prefixString + "IMPOSSIBLE");
            else
                Console.WriteLine(prefixString + "POSSIBLE");
        }
        //Console.ReadLine();
    }

    public static bool Solve(string[] waffles, int R, int C, int H, int V)
    {
        int chipsTotal = 0;
        LinkedList<int> chipsHorizontal = new LinkedList<int>();
        LinkedList<int> chipsVertical = new LinkedList<int>();
        for (int i = 0; i < R; i++)
        {
            int currentChipsHorizontal = waffles[i].Where(c => c == '@').Count();
            chipsHorizontal.AddLast(currentChipsHorizontal);
            chipsTotal += currentChipsHorizontal;
        }

        if (R <= H || C <= V || R <= 0 || C <= 0 || H <=0  )
            return false;

        for (int i = 0; i < C; i++)
        {
            int currentChipsVertical = 0;
            for (int j = 0; j < R; j++)
            {
                if (waffles[j][i] == '@')
                    currentChipsVertical++;
            }
            chipsVertical.AddLast(currentChipsVertical);
        }

        int product = (H+1) * (V+1);
        if ( chipsTotal % ( product ) != 0 )
        {
            return false;
        }

        int cocientHorizontal = chipsTotal / (H + 1);
        int cocientVertical = chipsTotal / (V + 1);
        int cocientHorVert = chipsTotal / ((H + 1) * (V + 1)); 

        int[] rectangleIndexes;
        int[] dontCare;
        if (!(SolveDimension(chipsHorizontal, cocientHorizontal, out rectangleIndexes) && SolveDimension(chipsVertical, cocientVertical,out dontCare)))
            return false;

        int[][] groups = new int[rectangleIndexes.Length/2][];

        for (int i = 0; i < rectangleIndexes.Length ; i+=2)
        {
            bool possible;
            groups[i/2] = CreateGroupByRectangle(waffles, rectangleIndexes[i], rectangleIndexes[i+1], cocientHorVert,out possible);
            if (!possible)
                return false;
        }

        
        for (int i = 0; i < groups[0].Length-1; i+=2)
        {
            var maxIndex = groups.Max(g => g[i + 1]);
            if ( i+2 < groups[0].Length)
            if (groups.Any(g => g[i + 2] <= maxIndex))
                return false;
        }

        return true;

    }

    private static int[] CreateGroupByRectangle(string[] waffles, int indexRow1, int indexRow2, int cocientHorVert, out bool possible)
    {
        int[] rectangleIndexes;
        LinkedList<int> chipsVertical = new LinkedList<int>();

        for (int i = 0; i < waffles[0].Length ; i++)
        {
            int currentChipsVertical = 0;
            for (int j = indexRow1; j <= indexRow2 ; j++)
            {
                if (waffles[j][i] == '@')
                    currentChipsVertical++;
            }
            chipsVertical.AddLast(currentChipsVertical);
        }

        possible = SolveDimension(chipsVertical, cocientHorVert,out rectangleIndexes);
        return rectangleIndexes;
    }

    private static bool SolveDimension(LinkedList<int> chipsDimension, int cocient, out int[] rectangleIndexes)
    {
        int chipsCount = 0;
        LinkedList<int> result = new LinkedList<int>();
        int index = 0;
        bool first = true;
        foreach (var chips in chipsDimension)
        {
            chipsCount += chips;
            if (first && chipsCount != 0)
            {
                result.AddLast(index);
                first = false;
            }

            if (chipsCount == cocient)
            {
                result.AddLast(index);                
                chipsCount = 0;
                first = true;
            }
            else if (chipsCount > cocient)
            {
                rectangleIndexes = null;
                return false;
            }
            index++;
        }
        rectangleIndexes = result.ToArray();
        return true;
    }

    
}
