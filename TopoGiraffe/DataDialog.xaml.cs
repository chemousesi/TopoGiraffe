using System.Windows;

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
            get => EquidistanceTextBox.Text;
            set => EquidistanceTextBox.Text = value;
        }
        public string Max
        {
            get => MaxTextBox.Text;
            set => MaxTextBox.Text = value;
        }
        public string Min
        {
            get => MinTextBox.Text;
            set => MinTextBox.Text = value;
        }


        public string EchelleOnCanvas
        {
            get => EchelleTextBoxOnCanvas.Text;
            set => EchelleTextBoxOnCanvas.Text = value;
        }

        public string EchelleOnField
        {
            get => EchelleTextBoxOnField.Text;
            set => EchelleTextBoxOnField.Text = value;
        }




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
