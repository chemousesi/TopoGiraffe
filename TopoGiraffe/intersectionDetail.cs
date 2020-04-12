using System;
using System.Windows;


namespace TopoGiraffe
{
    public class IntersectionDetail
    {
        public Point point;
        public Boolean intersect;

        public IntersectionDetail(Point point, Boolean intersect)
        {
            this.point = point;
            this.intersect = intersect;
        }

    }
}
