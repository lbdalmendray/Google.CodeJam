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
            int A = int.Parse(Console.ReadLine());
            GopherManager gopherManager = new GopherManager(A, () => Console.ReadLine(), (s) => Console.WriteLine(s));
            if (gopherManager.Start() == Response.SomeThingWrong)
                break;
        }
    }    
}

public class GopherManager
{
    protected LinkedList<Tuple<int, int>> unpreparedCells;
    protected LinkedListNode<Tuple<int, int>> [,] unpreparedRectCells;
    public int MinRow { get; protected set; }
    public int MaxRow { get; protected set; }
    public int ColumnCenter { get; protected set; }

    public int MinArea { get; private set; }

    private Func<string> readFunction;
    private Action<string> writeFunc;

    public GopherManager(int minArea, Func<string> readFunction, Action<string> writeFunc )
    {
        this.MinArea = minArea;
        this.readFunction = readFunction;
        this.writeFunc = writeFunc;
        CreateCells();

    }

    public Response Start()
    {
        while (unpreparedCells.Count != 0)
        {
            ProposeDeploy();
            var tuple = GetCellPrepared();
            if (IsRectanglePrepared(tuple))
                return Response.Good;
            if (SomeThingWrong(tuple))
                return Response.SomeThingWrong;
            PrepareCell(tuple);
        }
        return Response.SomeThingWrong;
    }

    public bool Finished { get; protected set; }

    private bool IsRectanglePrepared(Tuple<int, int> tuple)
    {
        Finished = tuple.Item1 == 0 && tuple.Item2 == 0;
        return Finished;
    }

    private bool SomeThingWrong(Tuple<int, int> tuple)
    {
        Finished = tuple.Item1 == -1 && tuple.Item2 == -1;
        return Finished;
    }

    private Tuple<int,int> GetCellPrepared()
    {
        //var splitParts = Console.ReadLine().Split(' ');
        var splitParts = readFunction().Split(' ');
        return new Tuple<int, int>(int.Parse(splitParts[0]), int.Parse(splitParts[1]));
    }

    private void ProposeDeploy()
    {
        var tuple = unpreparedCells.First.Value;
        int row;
        if (tuple.Item1 == MinRow)
            row = MinRow + 1;
        else if (tuple.Item1 == MaxRow)
            row = MaxRow - 1;
        else
            row = tuple.Item1;
        writeFunc(row + " " + ColumnCenter);
    }

    protected void PrepareCell(Tuple<int,int> ijInput)
    {
        Tuple<int, int> rowColumnOut;
        GetUnpreparedRectCellsLoc(ijInput, out rowColumnOut);
        if (unpreparedRectCells[rowColumnOut.Item1, rowColumnOut.Item2] != null)
        {
            var node = unpreparedRectCells[rowColumnOut.Item1, rowColumnOut.Item2];
            unpreparedCells.Remove(node);
            unpreparedRectCells[rowColumnOut.Item1, rowColumnOut.Item2] = null;
        }
    }

    private void GetUnpreparedRectCellsLoc(Tuple<int,int> tuple, out Tuple<int,int> tupleOut)
    {
        int columnDiference = tuple.Item2 - ColumnCenter;
        int column;
        if (columnDiference == -1)
            column = 0;
        else if (columnDiference == 0)
            column = 1;
        else //if (columnDiference == 1)
            column = 2;

        var row = tuple.Item1- MinRow;
        tupleOut = new Tuple<int, int>(row, column);
    }

    protected void CreateCells()
    {
        unpreparedCells = new LinkedList<Tuple<int, int>>();
        int cellCount;
        ColumnCenter = 500;

        if (MinArea <= 9)
            cellCount = 9;
        else if ( MinArea % 3 == 0 )
        {
            cellCount = MinArea;
        }
        else if ( (MinArea+1) %3 == 0 )
        {
            cellCount = MinArea + 1;
        }
        else
        {
            cellCount = MinArea + 2;
        }

        int height = cellCount / 3;
        unpreparedRectCells = new LinkedListNode<Tuple<int, int>>[height, 3];
        MinRow = 2;
        MaxRow = height + 1;
        for (int i = MinRow; i <= MaxRow; i++)
        {
            unpreparedCells.AddLast(new Tuple<int, int>(i, ColumnCenter - 1));
            unpreparedRectCells[i-MinRow, 0] = unpreparedCells.Last;

            unpreparedCells.AddLast(new Tuple<int, int>(i, ColumnCenter));
            unpreparedRectCells[i-MinRow, 1] = unpreparedCells.Last;

            unpreparedCells.AddLast(new Tuple<int, int>(i, ColumnCenter + 1));
            unpreparedRectCells[i-MinRow, 2] = unpreparedCells.Last;
        }

    }    
}

public enum Response
{
    Good,
    SomeThingWrong
}
