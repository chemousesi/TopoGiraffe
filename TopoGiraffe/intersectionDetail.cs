using System;
using System.Windows;


namespace TopoGiraffe
{
    public class IntersectionDetail
    {
        public Point point;
        public Boolean intersect;
        public int altitude;
        public double distance;

        public IntersectionDetail(Point point, Boolean intersect, int altitude, double adistances)
        {
            this.point = point;
            this.intersect = intersect;
            this.altitude = altitude;
            this.distance = adistances;
        }
        public IntersectionDetail(Point point, Boolean intersect)
        {
            this.point = point;
            this.intersect = intersect;
        }

        public int CompareTo(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return 1;
            }
            else
            {
                IntersectionDetail inter = (IntersectionDetail)obj;
                return this.distance.CompareTo(inter.distance);
            }
        }


    }
}
