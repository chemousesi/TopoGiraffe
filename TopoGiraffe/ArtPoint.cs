using System;
using System.Windows;
using System.Windows.Shapes;

namespace TopoGiraffe
{
#pragma warning disable CS0659 // 'ArtPoint' se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
    public class ArtPoint
#pragma warning restore CS0659 // 'ArtPoint' se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
    {
        public Ellipse cercle;
        private Point p;

        public Point P { get => p; set => p = value; }

        public ArtPoint(Ellipse cercle, Point p)
        {
            this.cercle = cercle;
            this.P = p;
        }
        public ArtPoint()
        {

        }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                ArtPoint p1 = (ArtPoint)obj;
                return (this.p == p1.p) && (this.cercle == p1.cercle);
            }
        }

    }
}
