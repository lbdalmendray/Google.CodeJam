using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pylons
{
    public class Solution
    {
        static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());
            for (int i = 1; i <= T; i++)
            {
                var numbers = Array.ConvertAll(Console.ReadLine().Split(' '),int.Parse);
                LinkedList<Tuple<int, int>> outPut;
                var result = Solve(numbers[0], numbers[1], out outPut);
                if (result)
                {
                    Console.WriteLine("Case #" + i.ToString() + ": " + "POSSIBLE");
                    foreach (var outputElement in outPut)
                    {
                        Console.WriteLine("" + (outputElement.Item1+1) + " " + (outputElement.Item2+1));
                    }
                }
                else
                {
                    Console.WriteLine("Case #" + i.ToString() + ": " + "IMPOSSIBLE");
                }
            }
        }

        public static  bool Solve(int R, int C, out LinkedList<Tuple<int, int>> outPut)
        {
            var graph = CreateGraph(R, C);
            bool[] nodeUsed = new bool[R * C];
            outPut = new LinkedList<Tuple<int, int>>();
            var outPutAux = new LinkedList<int>();
            var result = SolveGraph(graph, 0, nodeUsed, R * C, ref outPutAux);
            if(result)
                Convert(outPutAux, outPut,  R, C);
            return result;
        }

        private static void Convert(LinkedList<int> outPutAux, LinkedList<Tuple<int, int>> outPut, int R, int C)
        {
            foreach (var number in outPutAux)
            {
                outPut.AddLast(new Tuple<int, int>(number / C, number % C));
            } 
        }

        private static bool SolveGraph(Dictionary<int, int>[] graph, int v, bool[] nodeUsed, int N, ref LinkedList<int> outPut)
        {
            outPut.AddLast(v);
            nodeUsed[v] = true;
            if (outPut.Count == N)
                return true;

            foreach (var key in graph[v].Keys)
            {
                if (nodeUsed[key])
                    continue;
                if (SolveGraph(graph, key, nodeUsed, N, ref outPut))
                    return true;
            }
            nodeUsed[v] = false;
            outPut.RemoveLast();
            return false;            
        }

        public static Dictionary<int,int> [] CreateGraph(int R, int C)
        {
            Dictionary<int, int>[] result = new Dictionary<int, int>[R * C];
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    result[i * C + j] = new Dictionary<int, int>();
                    FillNode(result[i * C + j], i, j,R,C);
                }
            }

            return result; 
        }

        public static void FillNode(Dictionary<int, int> node, int iIndex , int jIndex , int R , int C)
        {
            int rest = iIndex - jIndex;
            int sum = iIndex + jIndex;

            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    int cRest = i - j;
                    int cSum = i + j;
                    if (i == iIndex || j == jIndex ||
                        cRest == rest || sum == cSum)
                        continue;
                    int index = i* C +j;
                    node.Add(index, index);                   
                }
            }
        }
    }

    

}
