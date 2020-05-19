
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    /// 

    public partial class Window1 : Window
    {

        //colors related 
        class RectangleName
        {
            public Rectangle Rect { get; set; }
            public string Name { get; set; }
        }


        //Contructor
        public Window1()
        {
            InitializeComponent();

            // this here is for the colors

            var values = typeof(Brushes).GetProperties().
                Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            var brushNames = values.Select(v => v.Name);




            List<RectangleName> rectangleNames = new List<RectangleName>();

            foreach (string brushName in brushNames)
            {

                RectangleName rn = new RectangleName { Rect = new Rectangle { Fill = new BrushConverter().ConvertFromString(brushName) as Brush }, Name = brushName };
                rectangleNames.Add(rn);
            }

            
            //  colors end here

            styleCourbeCmbInDialogBox.SelectedIndex = 0;


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



        

     

        //binding Altitude
        string alti;
        private void Altitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            alti = Altitude.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        //number textbox control






    }





}


