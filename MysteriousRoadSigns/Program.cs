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
            string line = Console.ReadLine();
            int S = int.Parse(line);
            LinkedList<Sign> signs = new LinkedList<Sign>();
            for (int j = 0; j < S; j++)
            {
                var stringParts = Console.ReadLine().Split(' ');
                int D = int.Parse(stringParts[0]);    
                int A = int.Parse(stringParts[1]);    
                int B = int.Parse(stringParts[2]);
                signs.AddLast(new Sign { Destination = D, West = A, East = B });
            }
            Solve(signs.ToArray(), out int result1, out int result2);
            string prefixString = "Case #" + i + ": ";

            Console.WriteLine(prefixString + result1 + " " + result2);
        }
    }

    private static void Solve(Sign[] signs, out int result1, out int result2)
    {
        result1 = 0;
        result2 = 0;

        int[] westMax = new int[signs.Length].Select((v, i) => i).ToArray();
        int[] eastMin = new int[signs.Length].Select((v, i) => i).ToArray();
        bool[] maxCalculated = new bool[signs.Length];
        for (int i = 0; i < signs.Length; i++)
            CalculateWestMaxSequence(westMax, i, maxCalculated,signs[i], signs);
        bool[] minCalculated = new bool[signs.Length];
        for (int i = 0; i < signs.Length; i++)
            CalculateEastMinSequence(eastMin, i, minCalculated, signs[i], signs);

        int[] westMaxComplete = westMax.Select(v=>v).ToArray();
        int[] eastMinComplete = eastMin.Select(v => v).ToArray();
        for (int i = 0; i < signs.Length; i++)
                for (int j = westMax[i]+1; j < signs.Length; j++)
                {
                    if (signs[i].WestMax == signs[j].WestMax || signs[westMax[i] + 1].EastMin == signs[j].EastMin)
                        westMaxComplete[i] = j;
                }

        for (int i = 0; i < signs.Length; i++)
            for (int j = eastMin[i] - 1; j > -1; j--)
            {
                if (signs[i].EastMin == signs[j].EastMin || signs[eastMin[i] - 1].WestMax == signs[j].WestMax)
                    eastMinComplete[i] = j;
            }

        var westGroups = westMaxComplete.Select((v, i) => new Result { Count = v - i + 1, Index1 = i , Index2 = v }).GroupBy(v => v.Count).ToArray();
        var eastGroups = eastMinComplete.Select((v, i) => new Result { Count = i - v + 1, Index1 = v, Index2 = i }).GroupBy(v => v.Count).ToArray();

        int maxCountWest = -1;
        int indexWest = 0;
        for (int i = 0; i < westGroups.Length; i++)
        {
            if (westGroups[i].Key > maxCountWest)
            {
                maxCountWest = westGroups[i].Key;
                indexWest = i;
            }
        }
        int indexEast = 0;
        int maxCountEast = -1;

        for (int i = 0; i < eastGroups.Length; i++)
        {
            if (eastGroups[i].Key > maxCountEast)
            {
                maxCountEast = eastGroups[i].Key;
                indexEast = i;
            }
        }

        if ( maxCountEast > maxCountWest)
        {
            result1 = eastGroups[indexEast].Key;
            result1 = eastGroups[indexEast].Count();
        }
        else if (maxCountEast < maxCountWest)
        {
            result1 = westGroups[indexWest].Key;
            result1 = westGroups[indexWest].Count();
        }
        else
        {
            LinkedList<Result> all = new LinkedList<Result>();
            foreach (var element in westGroups[indexWest])
            {
                all.AddLast(element);
            }

            foreach (var element in eastGroups[indexEast])
            {
                all.AddLast(element);
            }

            var allArray = all.ToArray();
            for(int i = 0; i < allArray.Length;i++)
            {
                var element = allArray[i];
                if (element == null)
                    continue;
                var node = all.First;
                int j = 0;
                while(node != null)
                {
                    if ( element == node.Value)
                    {
                        node = node.Next;
                        j++;
                        continue;
                    }
                    if ( node.Value.Index1 == element.Index1 && node.Value.Index2 == element.Index2)
                    {
                        var nodeAux = node;
                        node = node.Next;
                        all.Remove(nodeAux);
                        allArray[j++] = null;
                        continue;
                    }
                    node = node.Next;
                    j++;
                }
            }

            result1 = all.First.Value.Count;
            result2 = all.Count;
        }        
    }

    

    private static int CalculateEastMinSequence(int[] eastMin, int i, bool[] minCalculated, Sign current, Sign[] signs)
    {
        if (i <= 0)
            return signs.Length;

        if (minCalculated[i])
            return eastMin[i];

        if (current.EastMin == signs[i].EastMin)
        {
            eastMin[i] = i;
            minCalculated[i] = true;
            var result = CalculateEastMinSequence(eastMin, i - 1, minCalculated, current, signs);
            if (i > result)
            {
                eastMin[i] = result;
                result = i;
            }

            return result;
        }
        else
        {
            return signs.Length;
        }
    }

    private static int CalculateWestMaxSequence(int[] westMax, int i, bool[] maxCalculated, Sign current, Sign [] signs )
    {
        if (i >= westMax.Length)
            return -1;

        if (maxCalculated[i])
            return westMax[i];
        
        if ( current.WestMax == signs[i].WestMax)
        {
            westMax[i] = i;
            maxCalculated[i] = true;
            var result = CalculateWestMaxSequence(westMax, i + 1, maxCalculated, current, signs);
            if (i < result)
            {
                westMax[i] = result;
                result = i;
            }

            return result;
        }
        else
        {
            return -1;
        }        
    }
}

public class Sign
{
    public int Destination { get; set; }
    public int West { get; set; }
    public int East { get; set; }
    int? westMax;
    public int WestMax
    {
        get
        {
            if (!westMax.HasValue)
                westMax = Destination + West;
            return westMax.Value;
        }
    }

    int? eastMin;
    public int EastMin
    {
        get
        {
            if (!eastMin.HasValue)
                eastMin = Destination + West;
            return eastMin.Value;
        }
    }
}

//new { count = v - i + 1, index1 = i , index2 = v }

public class Result
{
    public int Count { get;set; }
    public int Index1 { get;set; }
    public int Index2 { get;set; }
}
