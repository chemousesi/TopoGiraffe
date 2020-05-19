﻿using System.Collections.Generic;
using System.Windows.Shapes;

namespace TopoGiraffe.Noyau
{
    class Plan
    // cette classe va contenir toutes nos courbes, nos point et les données dessinées
    //---------------------------------------------------------------------------------

    {
        private int equidistance;
        private double minAltitude;
        private double maxAltitude;
        private Echelle echelle;

        private List<Polygon> courbes;


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
