using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;

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
            catch (ArgumentNullException excp)
            {
                MessageBox.Show("fichier de sauvegarde non choisis, clickez enregistrer sous d'abord");

            }

        }
    }
}
