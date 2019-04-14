using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCanGoYourOwnWay
{
    public class Solution2
    {
        public static void Main2(string[] args)
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
            var graph = CreateGraph(N);
            var deleteRelations = CreateRelationsFromEastSouthCodification(Path, N);
            graph.DeleteRelations(deleteRelations);
            var BFSResult = graph.BFS(0, N * N - 1);
            var result = VertexListToEastSoudCodification(BFSResult,N);
            return new string(result.ToArray());
        }

        public static LinkedList<char> VertexListToEastSoudCodification(LinkedList<int> BFSResult , int N )
        {
            var BFSResultPositions = BFSResult.Select(vertex => GetCurrentPosition(vertex, N)).ToArray();

            LinkedList<char> result = new LinkedList<char>();

            for (int i = 0; i < BFSResultPositions.Length - 1 ; i++)
            {
                if (BFSResultPositions[i][1] < BFSResultPositions[i + 1][1])
                {
                    result.AddLast('E');
                }
                else
                    result.AddLast('S');
            }

            return result;
        }

        public static Graph CreateGraph(int N)
        {
            int[][] relations = new int[2*N*N-2*N][];
            int relationIndex = 0;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if ( j < N-1)
                    {
                        relations[relationIndex++] = new int[] { GetCurrentVertex(i,j,N), GetCurrentVertex(i, j+1, N) };
                    }
                    if( i < N-1)
                    {
                        relations[relationIndex++] = new int[] { GetCurrentVertex(i, j, N), GetCurrentVertex(i+1, j, N) };
                    }
                } 
            }

            return new Graph(N*N, relations, relationIndex);
        }

        public static Graph CreateGraph1(int N)
        {
            LinkedList<int[]> relations = new LinkedList<int[]>();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (j < N - 1)
                    {
                        relations.AddLast(new int[] { GetCurrentVertex(i, j, N), GetCurrentVertex(i, j + 1, N) });
                    }
                    if (i < N - 1)
                    {
                        relations.AddLast(new int[] { GetCurrentVertex(i, j, N), GetCurrentVertex(i + 1, j, N) });
                    }
                }
            }

            return new Graph(N * N, relations);
        }

        public static LinkedList<int[]> CreateRelationsFromEastSouthCodification(string Path,int N)
        {
            LinkedList<int[]> result = new LinkedList<int[]>();
            int i = 0;
            int j = 0;

            foreach (var movement in Path)
            {
                if( movement == 'E')
                {
                    result.AddLast(new int[] { GetCurrentVertex(i, j,N), GetCurrentVertex(i, j + 1,N) });
                    j++;
                }
                else // movement == 'S'
                {
                    result.AddLast(new int[] { GetCurrentVertex(i, j, N), GetCurrentVertex(i+1, j, N) });
                    i++;
                }
            }

            return result;        
        }

        public static int GetCurrentVertex(int i , int j , int N)
        {
            return N * i + j;
        }

        public static int [] GetCurrentPosition(int vertex , int N)
        {
            var result = new int[2];
            result[0] = vertex / N;
            result[1] = vertex % N;

            return result;
        }
    }

    public class Graph
    {
        Dictionary<int, int>[] Adj ;

        public Graph(int vertexCount, int[][] relations, int length)
        {
            Adj = new Dictionary<int, int>[vertexCount];
            for (int i = 0; i < Adj.Length; i++)
            {
                Adj[i] = new Dictionary<int, int>(2);
            }

            for (int i = 0; i < length; i++)
            {
                Adj[relations[i][0]].Add(relations[i][1], relations[i][1]);
            }
        }

        public Graph(int vertexCount , LinkedList<int[]> relations)
        {
            Adj = new Dictionary<int, int>[vertexCount];
            for (int i = 0; i < Adj.Length; i++)
            {
                Adj[i] = new Dictionary<int, int>(2);
            }

            foreach (var relation in relations)
            {
                Adj[relation[0]].Add(relation[1],relation[1]);
            }
        }

        public void DeleteRelations(LinkedList<int[]> deleteRelationList )
        {
            foreach (var deleteRelation in deleteRelationList)
            {
                Adj[deleteRelation[0]].Remove(deleteRelation[1]);
            }
        }

        public LinkedList<int> BFS1(int startVertex, int endVertex)
        {
            LinkedList<int> result = new LinkedList<int>();
            bool[] useVertexList = new bool[Adj.Length];
            BFSAux(startVertex, endVertex, result);
            return result;
        }

        public bool BFSAux(int startVertex, int endVertex, LinkedList<int> result)
        {
            result.AddLast(startVertex);
            if (startVertex == endVertex)
                return true;

            foreach (var childVertex in Adj[startVertex].Keys)
            {
                if (BFSAux(childVertex, endVertex, result))
                    return true;                
            }

            result.RemoveLast();

            return false;
        }

        public LinkedList<int> BFS(int startVertex, int endVertex)
        {
            LinkedList<int> result = new LinkedList<int>();
            bool[] useVertexList = new bool[Adj.Length];
            useVertexList[startVertex] = true;
            int[] parent = new int[Adj.Length];

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startVertex);
            while (queue.Count != 0)
            {
                int currentVertex = queue.Dequeue();
                if (currentVertex == endVertex)
                    break;
                foreach (var childVertex in Adj[currentVertex].Keys)
                {
                    if (!useVertexList[childVertex])
                    {
                        useVertexList[childVertex] = true;
                        parent[childVertex] = currentVertex;
                        queue.Enqueue(childVertex);
                    }
                }
            }

            int currentVertexPath = endVertex;
            result.AddFirst(endVertex);
            while (currentVertexPath != startVertex)
            {
                currentVertexPath = parent[currentVertexPath];
                result.AddFirst(currentVertexPath);

            }

            return result;
        }
    }
}
