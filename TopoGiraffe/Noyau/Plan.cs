using System.Collections.Generic;
using System.Windows.Shapes;

namespace TopoGiraffe.Noyau
{
    internal class Plan
    // cette classe va contenir toutes nos courbes, nos point et les données dessinées
    //---------------------------------------------------------------------------------

    {
        private readonly int equidistance;
        private readonly double minAltitude;
        private readonly double maxAltitude;
        private readonly Echelle echelle;

        private readonly List<Polygon> courbes;


        //------------------------------------Constructeur-----------------------------------------
        //-----------------------------------------------------------------------------------------

        public Plan(int aEquidistance, double aMinAltitude, double aMaxAltitude, Echelle aEchelle)
        {
            equidistance = aEquidistance;
            minAltitude = aMinAltitude;
            maxAltitude = aMaxAltitude;
            echelle = aEchelle;
        }

        public Plan() { }

        public int Equidistance
        {
            get; set;
        }

        public double MinAltitude
        {
            get; set;
        }

        public double MaxAltitude
        {
            get; set;
        }

        //---------------------------------------------methodes d'ajout de courbes a la liste des courbes----------------------

        //---------------------------------------------------------------------------------------------------------------------

        public void AjouterCourbe(Polygon courbe)
        {
            courbes.Add(courbe);
        }


        //--------------------------------------------methodes de suppression de courbes ---------------------------
        //----------------------------------------------------------------------------------------------------------
        public void SupprimerCourbe(Polygon courbe)
        {
            courbes.Remove(courbe);
        }


    }
}
