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


        public SauvgardePage()
        {

            InitializeComponent();
            


        }

        public void goback_Click(object sender, RoutedEventArgs e)
        {


           this.NavigationService.GoBack();

        }

        private void enregistrerSous_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
            //pg.saveFile();
        }
    }
}
