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
            double A = double.Parse(Console.ReadLine());
        }
    }

    public void Solve(double Area , out Tuple<double,double> point1, out Tuple<double, double> point2, out Tuple<double, double> point3)
    {
        point1 = point2 = point3 = null;
    }    
}

public class Cube
{
    public Point center;
    public Point c1;
    public Point c2;
    public Point c3;
    public Point c4;
    public Point c5;
    public Point c6;
    public Point c7;
    public Point c8;

    //double error = 0.000001;

    double sideLength;
    public Cube()
    { 
        center = new Point(0, 0, 0);
        sideLength = 1;

        c1 = new Point(+0.5, +0.5, +0.5);
        c2 = new Point(+0.5, +0.5, -0.5);
        c3 = new Point(+0.5, -0.5, +0.5);
        c4 = new Point(+0.5, -0.5, -0.5);
        c5 = new Point(-0.5, +0.5, +0.5);
        c6 = new Point(-0.5, +0.5, -0.5);
        c7 = new Point(-0.5, -0.5, +0.5);
        c8 = new Point(-0.5, -0.5, -0.5);
    }
    public double CalculateArea()
    {

        return 0;
    }
}

public class Point
{
    double X { get; set; }
    double Y { get; set; }
    double Z { get; set; }

    public Point (double X,double Y, double Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }
}


