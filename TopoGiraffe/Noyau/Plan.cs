using System.Collections.Generic;
using System.Windows.Shapes;

namespace TopoGiraffe.Noyau
{
    class Plan
    // cette classe va contenir toutes nos courbes, nos point et les données dessinées

    {
        private int equidistance;
        private double minAltitude;
        private double maxAltitude;
        private Echelle echelle;
        private List<Polygon> courbes;// je vais remplacer polygon par courbeNiveau



        public Plan(int aEquidistance, double aMinAltitude, double aMaxAltitude, Echelle aEchelle)
        {
            equidistance = aEquidistance;
            minAltitude = aMinAltitude;
            maxAltitude = aMaxAltitude;
            echelle = aEchelle;
        }



        public int Equidistance
        {
            get => equidistance;
            set => equidistance = value;
        }

        public double MinAltitude
        {
            get => minAltitude;
            set => minAltitude = value;
        }

        public double MaxAltitude
        {
            get => maxAltitude;
            set => maxAltitude = value;
        }

        

        public void AjouterCourbe(Polygon courbe)
        {
            courbes.Add(courbe);
        }

        public void SupprimerCourbe(Polygon courbe)
        {
            courbes.Remove(courbe);
        }


    }
}
