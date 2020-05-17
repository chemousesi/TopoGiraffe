using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
           // this.distancesListe = adistancesList;
            this.mainScale = mainScale;
            this.penteText = pente;
            Resources.Clear();

            Resources.MergedDictionaries.Clear();

            AddResourceDictionary(MainPage.CurrentMode);
            //AddResourceDictionary("pack://application:,,,/LiveCharts.Wpf;component/Themes/base.xaml");


            InitializeComponent();

            penteTextBox.Text = penteText;
#pragma warning disable CS0168 // La variable 'compteur' est déclarée, mais jamais utilisée
            int compteur;
#pragma warning restore CS0168 // La variable 'compteur' est déclarée, mais jamais utilisée
            //Echelle scale = new Echelle(130, 1000);
            MyValues = new ChartValues<ObservablePoint>();




            foreach (IntersectionDetail IntersectionPoint in IntersectionPoints)
            {
                double distance = (double)System.Math.Round(mainScale.FindDistanceOnField(IntersectionPoint.distance), 3);
                MyValues.Add(new ObservablePoint(distance, IntersectionPoint.altitude));
            }
            SeriesCollection = new SeriesCollection
                {
                new LineSeries
                {
                    Title = " Profil Topgraphique",
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
        private void AddResourceDictionary(string src)
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(src, UriKind.Relative) });
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
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)chart.ActualWidth, (int)chart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(visual: chart);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            png.Save(stream);
            SaveToPng(chart, "MyChart.png");

            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 10, 40, 42, 35);


                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                //dlg.Filter = "Text documents (.topo)|*.topo"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
                string filename = null;
                if (result == true)
                {
                    // Open document
                    filename = dlg.FileName;
                   
                }
            try
            {

                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
            }
            catch(ArgumentNullException excp)
            {
                MessageBox.Show("il faut specifier un fichier pdf");
            }



            doc.Open();
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("./MyChart.PNG");
            jpg.Border = iTextSharp.text.Rectangle.BOX;
            jpg.BorderWidth = 5f;
            doc.Add(jpg);
            doc.Add(new iTextSharp.text.Paragraph("Original Width: "));

            Viewbox viewbox = new Viewbox();





            doc.Close();
            Export.IsEnabled = false;
        }
    }
}

