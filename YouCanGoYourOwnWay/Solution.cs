using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCanGoYourOwnWay
{
    public class Solution
    {
        public static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());
            for (int i = 1; i <= T; i++)
            {
                var N = int.Parse(Console.ReadLine());
                var Path = Console.ReadLine();
                var result = Solve(N,Path);
                Console.WriteLine("Case #" + i.ToString() + ": " + result.ToString());
            }
        }

        public static string Solve(int N, string Path)
        {
            char[,] solution = new char[N, N];
            bool[,] solutionCalculated = new bool[N, N];
            int [][] pathPositions = calculatePathPositions(Path,N).Skip(1).ToArray();
            solutionCalculated[0, 0] = true;
            for (int k = 1; k < N; k++)
            {
                for (int m = 1; m < N; m++)
                {
                    SolveAux(k, m, solutionCalculated, solution, Path, pathPositions,(m+1) + (k+1)-2 - 1 );
                }
            }

            LinkedList<char> result = new LinkedList<char>();
            int i = N - 1;
            int j = N - 1;
            while(!(i== 0 && j == 0) )
            {
                result.AddFirst(solution[i, j]);
                if (solution[i, j] == 'E')
                    j--;
                else
                    i--;
            }

            return new string(result.ToArray());
        }

        public static string Solve2(int N, string Path)
        {
            char[,] solution = new char[N, N];
            bool[,] solutionCalculated = new bool[N, N];
            int[][] pathPositions = calculatePathPositions(Path, N).Skip(1).ToArray();
            solutionCalculated[0, 0] = true;
            SolveAux(N - 1, N - 1, solutionCalculated, solution, Path, pathPositions, 2 * N - 2 - 1);

            LinkedList<char> result = new LinkedList<char>();
            int i = N - 1;
            int j = N - 1;
            while (!(i == 0 && j == 0))
            {
                result.AddFirst(solution[i, j]);
                if (solution[i, j] == 'E')
                    j--;
                else
                    i--;
            }

            return new string(result.ToArray());
        }

        private static int[][] calculatePathPositions(string path, int N)
        {
            int[][] result = new int[path.Length+1][];

            result[path.Length] = new int[] { N - 1, N - 1 };

            var lastPosition = new int[] { N - 1, N - 1 };

            for (int i = path.Length-1; i >=0 ; i--)
            {
                if( path[i] == 'E')
                    result[i] = new int[] { lastPosition[0] , --lastPosition[1] };
                else
                    result[i] = new int[] { --lastPosition[0], lastPosition[1] };
            }

            return result; 
        }

        private static char SolveAux(int i, int j, bool[,] solutionCalculated, char[,] solution, string path, int[][] pathPositions, int pathPositionIndex)
        {
            if (solutionCalculated[i,j])
            {
                return solution[i,j] ;
            }

            solutionCalculated[i, j] = true;

            if ( i > 0 && !IsIncorrectMovement(path,pathPositions,pathPositionIndex,i,j,'S') )
            {
                if(!solutionCalculated[i-1,j])
                {
                    SolveAux(i - 1, j, solutionCalculated, solution, path, pathPositions, pathPositionIndex - 1);
                }
                if( solution[i-1,j] != 'N')
                {
                    solution[i, j] = 'S';
                    return solution[i, j];
                }
            }
            if (j > 0 && !IsIncorrectMovement(path, pathPositions, pathPositionIndex, i, j, 'E'))
            {
                if (!solutionCalculated[i , j-1])
                {
                    SolveAux(i, j-1, solutionCalculated, solution, path, pathPositions, pathPositionIndex - 1);
                }
                if (solution[i, j-1] != 'N')
                {
                    solution[i, j] = 'E';
                    return solution[i, j];
                }
            }

            solution[i, j] = 'N';
            return solution[i, j];
        }

        public static bool IsIncorrectMovement(string path, int[][] pathPositions, int pathPositionIndex, int i,int j , char movement)
        {
            return pathPositions[pathPositionIndex][0] == i && pathPositions[pathPositionIndex][1] == j && path[pathPositionIndex] == movement;
        }
    }

}
