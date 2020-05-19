using System;
using System.Windows;

namespace TopoGiraffe.Boites_de_dialogue
{
    /// <summary>
    /// Interaction logic for Pente.xaml
    /// </summary>
    public partial class Pente : Window
    {
        public Pente(String pente)
        {
            InitializeComponent();
            penteb.Text = pente;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
