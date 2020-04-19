using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace TopoGiraffe
{
    using System.Collections.Generic;

    using OxyPlot;
    using OxyPlot.Wpf;

    public class MainViewModel
    {
        public MainViewModel()
        {


            this.Title = "Profil Topographique";
            int cpt = 0;// Pour parcourir la list des intersections
            int point1 = 1; //Pour cree la liste
            double x, y;

            //  for ()                                     // parcour liste


            //   {                                          // debut de parcour

            x = 40; // afectation des x avec echele
            y = 12; // affectation des altitudes 



            /*************************************First Point + creation ***********************************/
            if (point1 == 1)
            {
                Points = new List<DataPoint>
            {
                new DataPoint(x, y),
            };
                point1 = 2;
            }


            /************************************* The rest of the points ***********************************/
            //Suivant de x et y dans la liste .
            for (cpt = 0; cpt < 10; cpt++)//Instead of 10 put the number of intersections -2;
            {

                Points.Add(new DataPoint(x + 1, y + 1));

            }



            // }                                           //Fin parcour liste



        }


        public string Title { get; private set; }

        public IList<DataPoint> Points { get; private set; }
        public object Plot { get; }
    }
}




/*using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TopoGiraffe.Noyau;

namespace TopoGiraffe
{
    using System.Collections.Generic;

    using OxyPlot;
    using OxyPlot.Wpf;
    /// <summary>
    /// Logique d'interaction pour ProfilTopographique.xaml
    /// </summary>
    public partial class ProfilTopographique : Window
    {

        List<IntersectionDetail> IntersectionPoints;
        List<double> distancesListe;
        Echelle mainScale;
        public ProfilTopographique(List<IntersectionDetail> aIntersectionPoints, List<double> adistancesList, Echelle mainScale)
        {
            this.IntersectionPoints = aIntersectionPoints;
            this.distancesListe = adistancesList;
            this.mainScale = mainScale;
            InitializeComponent();

        }
    }
}*/
