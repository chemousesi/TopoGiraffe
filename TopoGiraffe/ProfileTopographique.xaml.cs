using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using TopoGiraffe.Noyau;

namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour ProfileTopographique.xaml
    /// </summary>
    public partial class ProfileTopographique : Window
    {
        List<IntersectionDetail> IntersectionPoints;
        List<double> distancesListe;
        Echelle mainScale;
        String penteText;

        public ProfileTopographique(List<IntersectionDetail> aIntersectionPoints, List<double> adistancesList, Echelle mainScale, String pente)
        {
            this.IntersectionPoints = aIntersectionPoints;
            this.distancesListe = adistancesList;
            this.mainScale = mainScale;
            this.penteText = pente;
           

            InitializeComponent();
            
            penteTextBox.Text = penteText;
            int compteur;
            //Echelle scale = new Echelle(130, 1000);
            MyValues = new ChartValues<ObservablePoint>();



            foreach (IntersectionDetail IntersectionPoint in IntersectionPoints)
            {
                MyValues.Add(new ObservablePoint(mainScale.FindDistanceOnField(IntersectionPoint.distance), IntersectionPoint.altitude));
            }
            SeriesCollection = new SeriesCollection
                {
                new LineSeries
                {
                    Title = "Topgraphie",
                   // Fill = Brushes.Red,
                   // StrokeThickness = 4,
                    Values = MyValues,//les valeurs
                 //   PointGeometrySize = 4,
                    AreaLimit = 0,
                    LineSmoothness = 0.4,
                    //DataLabels = true,
                }
            };

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public LineSeries LineSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ChartValues<ObservablePoint> MyValues { get; set; }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

