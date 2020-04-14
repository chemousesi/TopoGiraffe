using System.Windows;
using System.Windows.Shapes;

namespace TopoGiraffe.Noyau
{
    public class Echelle
    {
        public double scaleDistanceOnCanvas;
        public double scaleDistanceOnField;

        public Echelle(double aDistanceOnField)
        {
            scaleDistanceOnField = aDistanceOnField;

        }

        public Echelle(double aDistanceOnCanvas, double aDistanceOnField)
        {
            scaleDistanceOnCanvas = aDistanceOnCanvas;
            scaleDistanceOnField = aDistanceOnField;

        }



        public double FindDistanceOnField(Line line)
        // real distance should be in meters
        {
            Point pointA = new Point(line.X1, line.Y1);
            Point pointB = new Point(line.X2, line.Y2);

            double tempDistance = Outils.DistanceBtwTwoPoints(pointA, pointB);


            return (tempDistance * scaleDistanceOnField) / scaleDistanceOnCanvas;


        }

        //overriding the previous method

        public double FindDistanceOnField(double aDistanceOnCanvas)
        // takes a distance from the canvas (distance btw two points) and returns distance in meters
        {
            return (aDistanceOnCanvas * scaleDistanceOnField) / scaleDistanceOnCanvas;
        }



    }
}
