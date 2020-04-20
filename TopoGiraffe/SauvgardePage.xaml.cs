using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TopoGiraffe.Noyau;


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
