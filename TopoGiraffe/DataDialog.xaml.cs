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
using System.Windows.Shapes;

namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour DataDialog.xaml
    /// </summary>
    public partial class DataDialog : Window
    {
        public DataDialog()
        {
            InitializeComponent();
        }
        public string Equidistance
        {
            get { return EquidistanceTextBox.Text; }
            set { EquidistanceTextBox.Text = value; }
        }
        public string Max
        {
            get { return MaxTextBox.Text; }
            set { MaxTextBox.Text = value; }
        }
        public string Min
        {
            get { return MinTextBox.Text; }
            set { MinTextBox.Text = value; }
        }


        //public string Echelle
        //{
        //    get { return EchelleTextBox.Text; }
        //    set { EchelleTextBox.Text = value; }
        //}

        
        private void OnOKClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
