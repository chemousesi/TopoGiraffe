using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    /// 

    public partial class PointAltBox : Window
    {


        //Contructor
        public PointAltBox()
        {
            InitializeComponent();
            typePointCmb.SelectedIndex = 0;

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

#pragma warning disable CS0169 // Le champ 'PointAltBox.clr' n'est jamais utilisé
        private readonly SolidColorBrush clr;
#pragma warning restore CS0169 // Le champ 'PointAltBox.clr' n'est jamais utilisé


        //binding Altitude
        private string alti;
        private void Altitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            alti = Altitude.Text;
        }
        //number textbox control







    }


}


