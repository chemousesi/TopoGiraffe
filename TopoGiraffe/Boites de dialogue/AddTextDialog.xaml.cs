using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    /// 

    public partial class AddTextDialog : Window
    {


        //Contructor
        public AddTextDialog()
        {
            InitializeComponent();

        }


        //buttons behaviour
        void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        //number textbox control
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

        }


        SolidColorBrush clr;










    }


}


