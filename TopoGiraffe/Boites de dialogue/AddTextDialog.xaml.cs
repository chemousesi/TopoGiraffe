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
        private void OkButton_Click(object sender, RoutedEventArgs e)
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

#pragma warning disable CS0169 // Le champ 'AddTextDialog.clr' n'est jamais utilisé
        private readonly SolidColorBrush clr;
#pragma warning restore CS0169 // Le champ 'AddTextDialog.clr' n'est jamais utilisé










    }


}


