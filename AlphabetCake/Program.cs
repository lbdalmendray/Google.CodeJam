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
            int R = int.Parse(splitParts[0]);
            int C = int.Parse(splitParts[1]);
            char[,] cake = new char[R, C];
            for (int j = 0; j < R ; j++)
            {                
                line = Console.ReadLine();
                for (int k = 0; k < C; k++)
                {
                    cake[j, k] = line[k];
                }
            }

            Solve(cake);
            string prefixString = "Case #" + i + ": ";
            Console.WriteLine(prefixString);

            for (int j = 0; i < R; i++)
            {
                for (int k = 0; k < C; k++)
                {
                    Console.Write(cake[j, k]);
                }
                Console.WriteLine();
            }                       
        }
    }

    public static void Solve(char[,] cake)
    {
        LinkedList<Node> difLetters = new LinkedList<Node>();
        for (int i = 0; i < cake.GetLength(0); i++)
        {
            for (int j = 0; j < cake.GetLength(1); j++)
            {
                if (cake[i, j] != '?')
                    difLetters.AddLast(new Node { Letter = cake[i, j], Pos = new Tuple<int, int>(i, j) });
            }
        }

        foreach (Node node in difLetters)
        {
            Tuple<int, int> width = GetWidth(node,cake);
            Tuple<int,int> height = GetHeight(node, width, cake);
            ChangeCake(cake, node, width, height);
        }
    }

    private static void ChangeCake(char[,] cake, Node node, Tuple<int, int> width, Tuple<int, int> height)
    {
        for (int i = height.Item1 ; i <= height.Item2 ; i++)
        {
            for (int j = width.Item1 ; j <= width.Item2; j++)
            {
                cake[i, j] = node.Letter;
            }
        }
    }

    private static Tuple<int, int> GetHeight(Node node, Tuple<int, int> width, char[,] cake)
    {
        var y1 = node.Pos.Item1 - 1;

        while ( y1 >=0 && IsInterrogateLine(cake,new Node { Letter = node.Letter,  Pos = new Tuple<int, int>(y1,node.Pos.Item2) },width))
        {
            y1--;
        }
        y1++;


        var y2 = node.Pos.Item1 + 1;

        while (y2 < cake.GetLength(0) && IsInterrogateLine(cake, new Node { Letter = node.Letter, Pos = new Tuple<int, int>(y2, node.Pos.Item2) }, width))
        {
            y2++;
        }
        y2--;

        return new Tuple<int, int>(y1, y2);
    }

    private static bool IsInterrogateLine(char[,] cake, Node newNode, Tuple<int, int> width)
    {
        var currentWidth = GetWidth(newNode, cake);
        return currentWidth.Item1 == width.Item1 && currentWidth.Item2 == width.Item2;
    }

    private static Tuple<int, int> GetWidth(Node node, char[,] cake)
    {
        var x1 = node.Pos.Item2 - 1;

        while(x1>=0 && cake[node.Pos.Item1, x1] == '?')
        {
            //cake[node.Pos.Item1, x1] = node.Letter;
            x1--;
        }
        x1++;

        var x2 = node.Pos.Item2 + 1;

        while (x2 < cake.GetLength(1) && cake[node.Pos.Item1, x2] == '?')
        {
            //cake[node.Pos.Item1, x2] = node.Letter;
            x2++;
        }
        x2--;

        return new Tuple<int, int>(x1, x2);
    }
}

public class Node
{
    public char Letter { get; set; }
    public Tuple<int,int> Pos { get; set; }    
}
