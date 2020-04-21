using System.Windows;

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
