using System.Windows;
using System.Windows.Controls;



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


            MainWindow pg = new MainWindow();
            this.Content = pg;

        }
    }
}
