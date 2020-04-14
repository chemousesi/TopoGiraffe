using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TopoSurf.MessageBoxStyle
{
    /// <summary>
    /// Interaction logic for MssgBox.xaml
    /// </summary>
    public partial class MssgBox : Window
    {

        public MssgBox(string mssage)
        {
            InitializeComponent();
            Message.Text = mssage;
        }
        public MssgBox()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
          
        }
    }
}
