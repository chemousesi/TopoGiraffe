using System;
using System.Windows.Shapes;

namespace TopoGiraffe.Noyau
{
    //--------------------------------------Definition d'une courbe de niveau ----------------------------------
    //-------------------------------------------------------------------------------------------------------------
    public class CourbeNiveau
    {
        public Polyline polyline;
        public float altitude;
        public string couleur;
        public string styleDessin;// pour faire la difference avec les courbes maitresses et les courbes intermediares


        public CourbeNiveau(Polyline polyline, float altitude, string couleur)
        {
            this.polyline = polyline;
            this.altitude = altitude;
            this.couleur = couleur;
        }
        public CourbeNiveau(Polyline polyline, float altitude)
        {
            this.polyline = polyline;
            this.altitude = altitude;
        }
        public CourbeNiveau(Polyline polyline)
        {
            this.polyline = polyline;

        }
        public CourbeNiveau()
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
                CourbeNiveau cour = (CourbeNiveau)obj;
                return (this.altitude == cour.altitude) && (this.polyline == cour.polyline);
            }
        }

    }
}
