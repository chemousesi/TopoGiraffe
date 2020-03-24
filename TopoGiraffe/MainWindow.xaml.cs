using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TopoGiraffe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        // declaring variables

        List<Polyline> polylines = new List<Polyline>();

        //PointCollection myPointCollection2 = new PointCollection();

        Polyline courbeActuelle;
        
        List<Ellipse> cercles = new List<Ellipse>();




        public MainWindow()
        {
            InitializeComponent();
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "*.jpg,.png,.jpeg|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              " (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));

            }



        }


        


        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            double x = Mouse.GetPosition(mainCanvas).X;
            double y = Mouse.GetPosition(mainCanvas).Y;


            if (activerDessinCheckBox.IsChecked == true)
            {

                
                courbeActuelle.Points.Add(new Point(x, y));

                Ellipse cerclePoint = new Ellipse();

                cerclePoint.Width = 8;
                cerclePoint.Height = 8;
                cerclePoint.Fill = System.Windows.Media.Brushes.BlueViolet;
                Canvas.SetLeft(cerclePoint, x - (cerclePoint.Width / 2));
                Canvas.SetTop(cerclePoint, y - (cerclePoint.Height / 2));
                cercles.Add(cerclePoint);
                mainCanvas.Children.Add(cerclePoint);

            }
        }


        private void activerDessinCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            

            if (polylines.Count == 0)
            {
                MessageBox.Show("Il Faut avoir au moins une courbe");
                activerDessinCheckBox.IsChecked = false;

            }
            else
            {

                courbeActuelle = (Polyline)polylines[polylines.Count - 1];
                
                //myPolyline.Points = myPointCollection2;

            }

        }


       

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(Mouse.GetPosition(mainCanvas).X);
            int y = Convert.ToInt32(Mouse.GetPosition(mainCanvas).Y);
            Boolean cercleDuPremierPointDessine = false;
            Ellipse cerclePremierPoint;



            if (polylines.Count != 0)
            {
                PointCollection points = polylines[polylines.Count - 1].Points;


                if (points.Count > 2)
                {

                    Point premierPoint = points[0];
                    cerclePremierPoint = cercles[0];
                    

                    if ((Math.Abs(premierPoint.X - x) < 20) && (Math.Abs(premierPoint.Y - y) < 20) && (!cercleDuPremierPointDessine) )
                    {

                        
                        cerclePremierPoint.Width = 10;
                        cerclePremierPoint.Height = 10;
                        cerclePremierPoint.Fill = System.Windows.Media.Brushes.Red;
                        cercleDuPremierPointDessine = true;
                        

                    }
                    else
                    {
                        if((Math.Abs(premierPoint.X - x) > 20) && (Math.Abs(premierPoint.Y - y) > 20))
                        {
                            cerclePremierPoint.Width = 8;
                            cerclePremierPoint.Height = 8;
                            cerclePremierPoint.Fill = System.Windows.Media.Brushes.BlueViolet;
                        }

                    }

                }

            }
        }




        private void btn2_Click(object sender, RoutedEventArgs e)
        {

            Polyline myPolyline = new Polyline();
            polylines.Add(myPolyline);
            activerDessinCheckBox.IsChecked = true;

            // styling
            
            
            myPolyline.Stroke = System.Windows.Media.Brushes.Black;
            myPolyline.StrokeThickness = 2;
            myPolyline.FillRule = FillRule.EvenOdd;
            


            courbeActuelle = myPolyline;

            mainCanvas.Children.Add(courbeActuelle);


        }




        private void mainCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (activerDessinCheckBox.IsChecked == true)
            {
                mainCanvas.Cursor = Cursors.Cross;
            }
        }
    }
}
