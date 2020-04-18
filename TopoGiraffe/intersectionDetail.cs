using System;
using System.Windows;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TopoGiraffe
{
    [Serializable()]
    public class IntersectionDetail : ISerializable
    {
        public Point point;
        public Boolean intersect;

        public IntersectionDetail(Point point, Boolean intersect)
        {
            this.point = point;
            this.intersect = intersect;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("point", point);
            info.AddValue("intersect", intersect);

        }

        protected IntersectionDetail(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            point = (Point)serializationInfo.GetValue("point", typeof(Point));
            intersect = (bool)serializationInfo.GetValue("intersect", typeof(bool));

        }
    }
}
