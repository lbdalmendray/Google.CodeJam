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
            int N = int.Parse(splitParts[0]);
            int P = int.Parse(splitParts[1]);
            LinkedList<Rectangle> rectangles = new LinkedList<Rectangle>();
            for (int j = 0; j < N; j++)
            {
                splitParts = Console.ReadLine().Split(' ');
                int width = int.Parse(splitParts[0]);
                int height = int.Parse(splitParts[1]);
                rectangles.AddLast(new Rectangle { Width = width, Height = height });
            }
            double result = Solve(rectangles, P);

            string prefixString = "Case #" + i + ": ";
            if (i != T)
                Console.WriteLine(prefixString + result.ToString("N6"));
            else
                Console.Write(prefixString + result.ToString("N6"));
        }
    }

    private static double Solve(LinkedList<Rectangle> rectangles, double perimeter)
    {
        LinkedList<Interval> intervals = new LinkedList<Interval>();
        intervals.AddLast(new Interval());
        var minMaxValues = rectangles.Select(r => new MinMax { Min = Math.Min(r.Height, r.Width), Max = Math.Sqrt(r.Height * r.Height + r.Width * r.Width) }).ToArray() ;
        Array.Sort(minMaxValues.Select(e => e.Min).ToArray(),minMaxValues);

        double basePerimeter = 0;
        foreach (var  curPerimeter in rectangles.Select(e=>2*e.Height+ 2*e.Width))
        {
            basePerimeter += curPerimeter;
        }

        foreach (var minMax in minMaxValues)
        {
            if (GenerateIntervals(intervals, minMax, (perimeter - basePerimeter)/2))
                return perimeter;
        }

        return basePerimeter + 2*intervals.Last.Value.B;
    }

    private static bool GenerateIntervals(LinkedList<Interval> intervals, MinMax minMax, double perimeter)
    {
        var node1 = intervals.First;
        while(node1 != null)
        {
            var curInterval = node1.Value;

            var interval = CalculateIntervalResult(curInterval, minMax);
            if ( interval != curInterval )
            {
                intervals.AddAfter(node1, interval);
                curInterval = interval;
                node1 = node1.Next;
                if (curInterval.B >= perimeter)
                    return true;
            }

            var node2 = node1.Next;
            while (node2 != null)
            {
                if (Intersect(curInterval, node2.Value))
                {
                    curInterval.B = node2.Value.B + minMax.Max;
                    if (curInterval.B >= perimeter)
                        return true;
                    intervals.Remove(node2);
                }
                else
                    break;
                node2 = node1.Next;                
            }            

            node1 = node1.Next;
        }
        return false;
    }

    private static bool Intersect(Interval currentInterval, Interval interval1)
    {
        return interval1.A <= currentInterval.B;
    }

    private static Interval CalculateIntervalResult(Interval curInterval, MinMax minMax)
    {
        if (minMax.Min + curInterval.A <= curInterval.B)
        {
            curInterval.B = minMax.Max + curInterval.B;
            return curInterval;
        }
        else
            return new Interval { A = minMax.Min + curInterval.A, B = minMax.Max + curInterval.B };
    }
}

public class MinMax
{
    public double Min { get; set; }
    public double Max { get; set; }
}

public class Interval
{
    public double A { get; set; }
    public double B { get; set; }
}

public class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
}
