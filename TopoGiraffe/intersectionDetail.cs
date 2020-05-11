using System;
using System.Runtime.Serialization;
using System.Windows;

namespace TopoGiraffe
{
    [Serializable()]
    public class IntersectionDetail : ISerializable
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


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("point", point);
            info.AddValue("intersect", intersect);
            info.AddValue("altitude", altitude);

        }

        protected IntersectionDetail(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            point = (Point)serializationInfo.GetValue("point", typeof(Point));
            intersect = (bool)serializationInfo.GetValue("intersect", typeof(bool));
            altitude = (int)serializationInfo.GetValue("altitude", typeof(int));

        }

        public IntersectionDetail(Point point, int Altitude, double distance)
        {
            this.point = point;
            this.altitude = Altitude;
            this.distance = distance;
        }
        public IntersectionDetail(Point point, int Altitude)
        {
            this.point = point;
            this.altitude = Altitude;
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
