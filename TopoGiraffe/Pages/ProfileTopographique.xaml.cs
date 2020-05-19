using iTextSharp.text;
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
        List<IntersectionDetail> IntersectionPoints;   //La liste des points d'intersection
        List<double> distancesListe;  //La liste des deistances entre chaque deux points 
        Echelle mainScale;
        String penteText;

        public ProfileTopographique(List<IntersectionDetail> aIntersectionPoints, List<double> adistancesList, Echelle mainScale, String pente)
        {
            this.IntersectionPoints = aIntersectionPoints;
            // this.distancesListe = adistancesList;
            this.mainScale = mainScale;    //L'échelle des x 
            this.penteText = pente;        // Creation du dailogebox du pente
            Resources.Clear();

            Resources.MergedDictionaries.Clear();

            AddResourceDictionary(MainPage.CurrentMode);
            //AddResourceDictionary("pack://application:,,,/LiveCharts.Wpf;component/Themes/base.xaml");


            InitializeComponent();

            penteTextBox.Text = penteText; // L'affichage du pente 
            //Echelle scale = new Echelle(130, 1000);
            MyValues = new ChartValues<ObservablePoint>(); //Creation de la liste des valeurs 




            foreach (IntersectionDetail IntersectionPoint in IntersectionPoints) //Boucle pour parcourir la liste des points d'intersections
            {
                double distance = System.Math.Round(mainScale.FindDistanceOnField(IntersectionPoint.distance), 3);
                MyValues.Add(new ObservablePoint(distance, IntersectionPoint.altitude)); // Afectation les valeurs a la liste des valeurs "les x et les y"
            }
            SeriesCollection = new SeriesCollection   //La creation de la courbe 
                {
                new LineSeries
                {
                    Title = " Profil Topgraphique", //Titre de graph
                   // Fill = Brushes.Red,
                   // StrokeThickness = 4,
                    Values = MyValues,           // Afectation des valeurs au graph 
                 //   PointGeometrySize = 4,
                    AreaLimit = 0,
                    LineSmoothness = 0.4,    //Ajuster la finesse des lignes
                    
                    //DataLabels = true,
                }
            };

            DataContext = this;
        }
        private void AddResourceDictionary(string src)
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(src, UriKind.Relative) });
        }



        // les variables externes
        public SeriesCollection SeriesCollection { get; set; }
        public LineSeries LineSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ChartValues<ObservablePoint> MyValues { get; set; }


        public void SaveToPng(FrameworkElement visual, string fileName)  //Sauvgader le graph en image
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder) //Creation du fichier pdf
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }

        private void Button_Click(object sender, RoutedEventArgs e)   //Sauvgarder le profile topographique en pdf
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)chart.ActualWidth, (int)chart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(visual: chart);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            png.Save(stream);
            SaveToPng(chart, "MyChart.png"); //le nom de l'image

            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 10, 40, 42, 35); //les dimensions du fichier pdf


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
            catch (ArgumentNullException excp)
            {
                MessageBox.Show("il faut specifier un fichier pdf"); //le cas de ne pas selectioner un fichier
            }


            //L'exportation en pdf


            doc.Open();
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("./MyChart.PNG");
            jpg.Border = iTextSharp.text.Rectangle.BOX;
            jpg.BorderWidth = 5f;
            doc.Add(jpg);
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph();
            paragraph.Font = new Font(FontFactory.GetFont("Arial", 16, Font.BOLD));
            paragraph.Alignment = Element.ALIGN_CENTER;//here is the change
            paragraph.Add("Profile Topographique");
            doc.Add(paragraph);

            Viewbox viewbox = new Viewbox(); //Ajuster la photo dans un cadre





            doc.Close();
            Export.IsEnabled = false; //desactivation du button de l'exportation
        }
    }
}

