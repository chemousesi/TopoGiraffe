using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;


namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {

        MainPage _mainPage;

        public MapPage(MainPage mainPage)
        {
            InitializeComponent();
            _mainPage = mainPage;
            Resources.Clear();

            Resources.MergedDictionaries.Clear();

          AddResourceDictionary(MainPage.CurrentMode);
        }
        private void AddResourceDictionary(string src)
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(src, UriKind.Relative) });
        }

        private void goback_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(_mainPage);
        }

        private void exporter_Click(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(1300, 600, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(myMap);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            string filepath = "./../../../maps/Capture1.png";
            using (Stream fileStream = File.Create(filepath))
            {

                pngImage.Save(fileStream);
            }

            MessageBox.Show("Capture enregistré dans le Dossier \" TopoGiraffe/maps \" ");

        }
    }
}
