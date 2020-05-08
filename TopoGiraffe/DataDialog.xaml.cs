using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour DataDialog.xaml
    /// </summary>
    /// 
    public class Customer
    {
        private string _equid;

        public string Equid
        {
            get { return _equid; }
            set
            {
                _equid = value;
                if (String.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Equidistance is mandatory.");
                }
            }


        }
    }



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


        public string EchelleOnCanvas
        {
            get { return EchelleTextBoxOnCanvas.Text; }
            set { EchelleTextBoxOnCanvas.Text = value; }
        }

        public string EchelleOnField
        {
            get { return EchelleTextBoxOnField.Text; }
            set { EchelleTextBoxOnField.Text = value; }
        }




        private void OnOKClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
