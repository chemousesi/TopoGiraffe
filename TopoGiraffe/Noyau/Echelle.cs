using System.Windows;
using System.Windows.Shapes;

namespace TopoGiraffe.Noyau
{
    public class Echelle
    {
       

        public Echelle(double aDistanceOnField)
        {
            ScaleDistanceOnField = aDistanceOnField;

        }

        public Echelle(double aDistanceOnCanvas, double aDistanceOnField)
        {
            ScaleDistanceOnCanvas = aDistanceOnCanvas;
            ScaleDistanceOnField = aDistanceOnField;

        }

        public Echelle()
        {

        }

        public double FindDistanceOnField(Line line)
        // real distance should be in meters
        {
            Point pointA = new Point(line.X1, line.Y1);
            Point pointB = new Point(line.X2, line.Y2);

            double tempDistance = Outils.DistanceBtwTwoPoints(pointA, pointB);


            return (tempDistance * ScaleDistanceOnField) / ScaleDistanceOnCanvas;


        }

        //overriding the previous method

        public double FindDistanceOnField(double aDistanceOnCanvas)//pixels
        // takes a distance from the canvas (distance btw two points) and returns distance in meters
        {
            return (aDistanceOnCanvas * ScaleDistanceOnField) / ScaleDistanceOnCanvas;
        }



        public double ScaleDistanceOnCanvas
        {
            get;
            set;
        }
        public double ScaleDistanceOnField
        {
            get; set;
        }








    }
}
