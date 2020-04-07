using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Windows.Shapes;

namespace TopoGiraffe
{
    class ArtPoint
    {
        public Ellipse cercle;
        public Point p; 

        public ArtPoint(Ellipse cercle , Point p)
        {
            this.cercle = cercle;
            this.p = p;
        }
    }
}
