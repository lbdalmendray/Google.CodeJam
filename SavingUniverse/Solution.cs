using System;
using System.Collections.Generic;

public class Solution
{
    public static void Main(string[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 1; i <= T; i++)
        {
            var splitParts = Console.ReadLine().Split(' ');
            int D = int.Parse(splitParts[0]);
            string P = splitParts[1];

            var result = Solve(D, P);
            string prefixString = "Case #" + i + ": " ;
            if (result == -1)
                Console.WriteLine(prefixString + "IMPOSSIBLE");
            else Console.WriteLine(prefixString + result);
        }
    }

    public static int Solve(int D , string P)
    {
        int totalDamage = 0;
        LinkedList<Pair> instructions = CompactInstruction(P, out totalDamage);

        var CurrentNode = instructions.Last;
        int result = 0;
        if (totalDamage > D)
        {
            while (CurrentNode != null)
            {
                if (CurrentNode.Value.Instruction == 'C')
                    CurrentNode = CurrentNode.Previous;
                else
                {
                    if ( CurrentNode == instructions.First)
                    {
                        if (totalDamage <= D)
                        {
                            return result;
                        }
                        else
                            return -1;
                    }
                    else
                    {
                        int minOperations = GetMinOperations(D, totalDamage, CurrentNode, out totalDamage);
                        result += minOperations;
                        if (totalDamage <= D)
                            return result;
                        else
                        {
                            if (CurrentNode.Previous == instructions.First)
                                return -1;
                            else
                            {
                                CurrentNode.Previous.Previous.Value.Count += CurrentNode.Value.Count;
                                CurrentNode = CurrentNode.Previous.Previous;
                            }
                        }
                    }
                }
            }
            return -1;
        }
        else
            return 0;               
    }

    private static int GetMinOperations(int D, int TotalDamage, LinkedListNode<Pair> CurrentNodeSOper, out int TotalDamageNew)
    {
        var CurrentNoeCOper = CurrentNodeSOper.Previous;

        var form1 = ((double)(D - TotalDamage) )/ ((double)(CurrentNodeSOper.Value.BeamStrength * CurrentNodeSOper.Value.Count));
        var logValue = Math.Log(form1 + 1, 0.5);
        int jmin = (int)Math.Ceiling(logValue) - 1;
        jmin = Math.Min(jmin, CurrentNoeCOper.Value.Count-1);
        int result;

        if (jmin == 0)
        {
            result = 0;
        }
        else
        {
            TotalDamage -= ((int)(((double)(CurrentNodeSOper.Value.BeamStrength * CurrentNodeSOper.Value.Count)) * (1 - Math.Pow(0.5, jmin))));
            result = jmin * CurrentNodeSOper.Value.Count;
        }

        var currentBeamStrength = (int)(CurrentNodeSOper.Value.BeamStrength * Math.Pow(0.5, jmin));

        int possibleValue = (int)Math.Ceiling(((double)(TotalDamage - D) * 2) / (currentBeamStrength));
        if (possibleValue <= CurrentNodeSOper.Value.Count)
        {
            result += possibleValue;
            TotalDamage -= possibleValue * currentBeamStrength / 2;
        }
        else
        {
            result += CurrentNodeSOper.Value.Count;
            TotalDamage -= CurrentNodeSOper.Value.Count * currentBeamStrength / 2;
        }

        TotalDamageNew = TotalDamage;
        return result;
    }

    private static int GetMinOperations2(int D, int TotalDamage, LinkedListNode<Pair> CurrentNodeSOper, out int TotalDamageNew)
    {
        var CurrentNoeCOper = CurrentNodeSOper.Previous;
        int result = 0;
        for (int i = 0; i < CurrentNoeCOper.Value.Count; i++)
        {
            int possibleValue = (int)Math.Ceiling(((double)(TotalDamage - D) * 2) / (CurrentNodeSOper.Value.BeamStrength));
            if ( possibleValue <= CurrentNodeSOper.Value.Count )
            {
                result += possibleValue;
                TotalDamage -= possibleValue * CurrentNodeSOper.Value.BeamStrength / 2;
                break;
            }
            else
            {
                result += CurrentNodeSOper.Value.Count;
                TotalDamage -= CurrentNodeSOper.Value.Count * CurrentNodeSOper.Value.BeamStrength / 2;
            }
        }
        TotalDamageNew = TotalDamage;
        return result;
    }

    private static LinkedList<Pair> CompactInstruction(string p, out int totalDamage)
    {
        LinkedList<Pair> result = new LinkedList<Pair>();

        int currentIndex = 0;
        int currentBeamStrength = 1;
        totalDamage = 0;
        result.AddLast(new Pair { Instruction = p[currentIndex], Count = 1, BeamStrength = currentBeamStrength });
        
        if (p[currentIndex] == 'C')
            currentBeamStrength *= 2;
        else
        {
            totalDamage += currentBeamStrength;
        }
        currentIndex++;
        for (; currentIndex < p.Length; currentIndex++)
        {
            if ( p[currentIndex] == result.Last.Value.Instruction)
            {
                result.Last.Value.Count++;
            }
            else
            {
                result.AddLast(new Pair { Instruction = p[currentIndex], Count = 1, BeamStrength = currentBeamStrength });
            }

            if (p[currentIndex] == 'C')
                currentBeamStrength *= 2;
            else
            {
                totalDamage += currentBeamStrength;
            }
        }

        return result;
    }
}

public class Pair
{
    public char Instruction { get; set; }
    public int Count { get; set; }
    public int BeamStrength { get; set; }
}

