using Microsoft.Win32;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TopoGiraffe.Noyau;
using TopoSurf.MessageBoxStyle;

//using TopoGiraffe.MessageBoxStyle;



namespace TopoGiraffe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {






            InitializeComponent();


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new SauvgardePage();
        }
    }

}
