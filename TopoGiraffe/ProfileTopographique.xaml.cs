using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TopoGiraffe.Noyau;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Controls;

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
#pragma warning disable CS0168 // La variable 'compteur' est déclarée, mais jamais utilisée
            int compteur;
#pragma warning restore CS0168 // La variable 'compteur' est déclarée, mais jamais utilisée
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

        public void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("ISM.pdf", FileMode.Create));


            Viewbox viewbox = new Viewbox();



            RenderTargetBitmap rtb = new RenderTargetBitmap((int)chart.ActualWidth, (int)chart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(visual: chart);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            png.Save(stream);

            SaveToPng(chart, "MyChart.png");
        }
    }
}

