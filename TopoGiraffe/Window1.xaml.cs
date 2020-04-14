using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

            colorComboBox.ItemsSource = rectangleNames;
            colorComboBox.SelectedIndex = 7;
          //  colors end here

            


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



        //binding colors combobox
        //Clr is the variable to use
        SolidColorBrush clr;
        private void colorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            clr = (SolidColorBrush)new BrushConverter().ConvertFromString((colorComboBox.SelectedItem as RectangleName).Name);
            //clr = (RectangleName)(colorComboBox.SelectedItem);

        }

        //binding  type combobox
        string typ ;
        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            typ = Type.SelectedValue.ToString();
            
        }

        //binding Altitude
        string alti;
        private void Altitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            alti = Altitude.Text;
        }
        //number textbox control

        




    }

    



}
