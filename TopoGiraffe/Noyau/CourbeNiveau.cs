using System;
using System.Windows.Shapes;

namespace TopoGiraffe.Noyau
{
    public  class CourbeNiveau
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
        //public Polyline polyline
        //{
        //    get => polyline;
        //    set => polyline = value;
        //}

        //public float Altitude
        //{
        //    get => altitude;
        //    set => altitude = value;
        //}


        //public string Couleur
        //{
        //    get => couleur;
        //    set => couleur = value;
        //}

        //public string StyleDessin
        //{
        //    get => styleDessin;
        //    set => styleDessin = value;
        //}
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
