using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
    /// <summary>
    /// Logique d'interaction pour ProfileTopographique.xaml
    /// </summary>
    public partial class ProfileTopographique : Window
    {
        
        public ProfileTopographique()
        {
            InitializeComponent();
            int compteur;

            MyValues = new ChartValues<ObservableValue>
            {
                new ObservableValue(20),
                new ObservableValue(80),
                new ObservableValue(200),
                new ObservableValue(10),
                new ObservableValue(100),
                new ObservableValue(60),


            };

            for (compteur = 0; compteur < 1; compteur++)
            {
                MyValues.Add(new ObservableValue(250));//We add Y ! 

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
                   // LineSmoothness = 0,
                    //DataLabels = true,
                   
                    

                },
               
                
                
              /*  new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }*/
            };


            Labels = new[] { "20m", "30m", "40m", "50m", "60m", "70m", "80m", "90m" };// l'absis des x  
            YFormatter = value => value.ToString("C");


            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public LineSeries LineSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ChartValues<ObservableValue> MyValues { get; set; }
    }
}

