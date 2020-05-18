using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour DataDialog.xaml
    /// </summary>
    /// 


    public partial class ScaleDialog : Window
    {
        public ScaleDialog()
        {
            InitializeComponent();
        }



        public string EchelleOnCanvas
        {
            get => EchelleTextBoxOnCanvasScaleDB.Text;
            set => EchelleTextBoxOnCanvasScaleDB.Text = value;
        }

        public string EchelleOnField
        {
            get => EchelleTextBoxOnFieldScaleDB.Text;
            set => EchelleTextBoxOnFieldScaleDB.Text = value;
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
