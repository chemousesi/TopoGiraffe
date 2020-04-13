using System.Windows.Shapes;

namespace TopoGiraffe.Noyau
{
    class CourbeNiveau
    {
        private Polyline courbe;
        private double altitude;
        private string couleur;
        private string styleDessin;// pour faire la difference avec les courbes maitresses et les courbes intermediares


        public CourbeNiveau(Polyline courbe, double altitude, string couleur)
        {
            this.courbe = courbe;
            this.altitude = altitude;
            this.couleur = couleur;
        }

        public Polyline Courbe
        {
            get => courbe;
            set => courbe = value;
        }

        public double Altitude
        {
            get => altitude;
            set => altitude = value;
        }


        public string Couleur
        {
            get => couleur;
            set => couleur = value;
        }

        public string StyleDessin
        {
            get => styleDessin;
            set => styleDessin = value;
        }

    }
}
