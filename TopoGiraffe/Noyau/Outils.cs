
using System;
using System.Windows;

namespace TopoGiraffe
{
    public static class Outils
    {




        public static double DistanceBtwTwoPoints(Point pointA, Point pointB)
        {
            return Math.Sqrt(Math.Pow((pointB.X - pointA.X), 2) + Math.Pow((pointB.Y - pointA.Y), 2));
        }







    }
}
