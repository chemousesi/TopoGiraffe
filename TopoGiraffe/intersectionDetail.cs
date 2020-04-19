using System;
using System.Windows;


namespace TopoGiraffe
{
    public class IntersectionDetail
    {
        public Point point;
        public Boolean intersect;
        public int altitude;

        public IntersectionDetail(Point point, Boolean intersect, int altitude)
        {
            this.point = point;
            this.intersect = intersect;
            this.altitude = altitude;
        }
        public IntersectionDetail (Point point, Boolean intersect)
        {
            this.point = point;
            this.intersect = intersect;
        }

    }
}
