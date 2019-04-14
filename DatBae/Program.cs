using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatBae
{
    public class Solution
    {
        public static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());
            for (int i = 1; i <= T; i++)
            {
                string numbersString = Console.ReadLine();
                int [] numbers = Array.ConvertAll(numbersString.Split(' '), int.Parse);
                int N = numbers[0];
                int B = numbers[1];
                int F = numbers[2];



            }
        }
    }

    public class DatBaeSolver
    {
        int WorkerCount;
        int BrokenWorkerCount;
        int RequestCount;
        bool[] bits;
        LinkedList<GroupSolver> groups = new LinkedList<GroupSolver>();
        Func<string> readFunction;
        Action<string> writeFunction;

        public DatBaeSolverState CurrentState { get; protected set; } 

        public DatBaeSolver(int WorkerCount, int BrokenWorkerCount, int RequestCount , Func<string> readFunction, Action<string> writeFunc)
        {
            this.WorkerCount = WorkerCount;
            this.BrokenWorkerCount = BrokenWorkerCount;
            this.RequestCount = RequestCount;
            this.bits = new bool[this.WorkerCount];
            groups.AddFirst(new GroupSolver(this.BrokenWorkerCount, this, 0, WorkerCount - 1));
            this.readFunction = readFunction;
            this.writeFunction = writeFunc;
            CurrentState = DatBaeSolverState.Initialized;
        }

        public void Start()
        {
            CurrentState = DatBaeSolverState.Running;
            while(RequestCount>0)
            {
                foreach (var group in groups)
                {
                    group.SetSendMessage();
                }

                SendMessage();
                ReadResponse();
                if(CurrentState == DatBaeSolverState.RequestExceededCount)
                {
                    break;
                }
                RequestCount--;
            }
        }

        private void SendMessage()
        {
            char [] bitCharArray = this.bits.Select(bit => bit ? '1' : '0').ToArray();
            this.writeFunction(new string(bitCharArray));
        }

        private void ReadResponse()
        {
            string result = this.readFunction();
            if (result == "-1")
            {
                CurrentState = DatBaeSolverState.RequestExceededCount;
            }
            else
            {
                for (int i = 0; i < bits.Length; i++)
                {
                    bits[i] = result[i] == '1' ? true : false;
                }
            }
        }
    }

    public class GroupSolver
    {
        int BrokenWorkerCount;
        int firstIndex;
        int lastIndex;
        DatBaeSolver DatBaeSolver;
        LinkedList<int> brokenIndexes = new LinkedList<int>();

        public bool Finish { get; protected set; }

        public GroupSolver(int BrokenWorkerCount, DatBaeSolver DatBaeSolver, int firstIndex, int lastIndex)
        {
            Finish = false;
            this.BrokenWorkerCount = BrokenWorkerCount;
            this.firstIndex = firstIndex;
            this.lastIndex = lastIndex;
            this.DatBaeSolver = DatBaeSolver;
        }

        public void SetSendMessage()
        {

        }

        public void AnalizeResponse()
        {

        }
    }

    public enum DatBaeSolverState
    {
        Initialized,
        Finished,
        Running,
        RequestExceededCount,
    }
}
