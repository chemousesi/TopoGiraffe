using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Navigation;
using System.Windows.Documents;

#pragma warning disable CS0105 // La directive using de 'System.Windows' est apparue précédemment dans cet espace de noms
#pragma warning restore CS0105 // La directive using de 'System.Windows' est apparue précédemment dans cet espace de noms
#pragma warning disable CS0105 // La directive using de 'System.Windows.Controls' est apparue précédemment dans cet espace de noms
#pragma warning restore CS0105 // La directive using de 'System.Windows.Controls' est apparue précédemment dans cet espace de noms



namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour SauvgardePage.xaml
    /// </summary>
    public partial class SauvgardePage : Page
    {
        List<List<IntersectionDetail>> curves;
        private MainPage _mainPage;
        string filename;

        public SauvgardePage(List<List<IntersectionDetail>> curves, MainPage mainPage)
        {
            this.curves = curves;
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
        public void Serializee(List<List<IntersectionDetail>> objet, string filename)
        {

            OpenFileDialog op = new OpenFileDialog();

            Stream s = File.Open(path: filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, objet);
            s.Close();
        }
        public void saveFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".topo"; // Default file extension
            dlg.Filter = "Text documents (.topo)|*.topo"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dlg.FileName;
                this.Serializee(curves, filename);
            }
        }



        public void goback_Click(object sender, RoutedEventArgs e)
        {


            this.NavigationService.Navigate(_mainPage);

        }

        private void enregistrerSous_Click(object sender, RoutedEventArgs e)
        {
            saveFile();
            // MainWindow .saveFile()
        }



        private void enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Serializee(curves, filename);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("fichier de sauvegarde non choisis, clickez enregistrer sous d'abord");

            }

        }


        void HandleRequestNavigate(object sender, RoutedEventArgs e)
        {

        }

        private void nouvelle_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Voulez vous sauvgarder ?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                saveFile();
            }
            _mainPage.prepareCanvas_Click(new object(), new RoutedEventArgs());
            this.NavigationService.Navigate(_mainPage);

        }


      


        

        private void openManual_Click(object sender, RoutedEventArgs e)
        {

            //var prs = new ProcessStartInfo("iexplore.exe");
            //prs.Arguments = "http://facebook.com/";
            //Process.Start(prs);

            // System.Diagnostics.Process.Start("microsoft-edge:http://www.google.com");


            //System.Diagnostics.Process.Start("IExplore.exe");

            //ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
            //startInfo.WindowStyle = ProcessWindowStyle.Minimized;

            //Process.Start(startInfo);

            //startInfo.Arguments = "www.google.com";

            //Process.Start(startInfo);




            //System.Diagnostics.Process.Start("IExplore.exe", "www.northwindtraders.com");

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            _mainPage.imprt_Click();
            this.NavigationService.Navigate(_mainPage);

        }
    }
}
