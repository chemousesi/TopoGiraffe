using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        Ellipse cerclePremierPoint = new Ellipse();

        // List<Ellipse> cercles = new List<Ellipse>();


       

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "TopoGiraffe";
            cmbColors.ItemsSource = typeof(Colors).GetProperties();


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
                
       
            }

        }


            /*      cette fonction va colorer le dernier point de la courbe quand on souhaite la fermer
             *   
       
        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(Mouse.GetPosition(mainCanvas).X);
            int y = Convert.ToInt32(Mouse.GetPosition(mainCanvas).Y);
            Boolean cercleDuPremierPointDessine = false;




            if (polylines.Count != 0)
            {
                PointCollection points = polylines[polylines.Count - 1].Points;


                if (points.Count > 2)
                {

                    Point premierPoint = points[0];
                    
                    

                    if ((Math.Abs(premierPoint.X - x) < 20) && (Math.Abs(premierPoint.Y - y) < 20) && (cercleDuPremierPointDessine==false) )
                    {
                        cercleDuPremierPointDessine = true;
                        
                        cerclePremierPoint.Width = 10;
                        cerclePremierPoint.Height = 10;
                        cerclePremierPoint.Fill = System.Windows.Media.Brushes.Red;

                        Canvas.SetLeft(cerclePremierPoint, premierPoint.X - (cerclePremierPoint.Width / 2));
                        Canvas.SetTop(cerclePremierPoint, premierPoint.Y - (cerclePremierPoint.Height / 2));
                        
                        mainCanvas.Children.Add(cerclePremierPoint);
                       

                    }

                    if ((Math.Abs(premierPoint.X - x) > 20) && (Math.Abs(premierPoint.Y - y) > 20) && (cercleDuPremierPointDessine = true))
                    {
                        if (mainCanvas.Children.Contains(cerclePremierPoint))
                        {
                            mainCanvas.Children.Remove(cerclePremierPoint);

                        }



                    }  

                    

                }

            }
        }

    */








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






       
        public void RemoveText(object sender, EventArgs e)
                {
                    if (altitudeTextBox.Text == "Enter text here...")
                    {
                        altitudeTextBox.Text = "";
                    }
                }

          public void AddText(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(altitudeTextBox.Text))
                altitudeTextBox.Text = "Enter text here...";
            }
          








        // getters and setters
        public Ellipse CerclePremierPoint
        {
            get { return cerclePremierPoint; }
        }


        // to eliminate placeholders

        private void altitudeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            altitudeTextBox.Text = "";
        }

        private void maxTextBox_GotFocus(object sender, RoutedEventArgs e)
        { 
            maxTextBox.Text = "";
        }

        private void minTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            minTextBox.Text = "";
        }

        private void longueurTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            longueurTextBox.Text = "";
        }

        private void deleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (mainCanvas.Children.Count == 0)
            {
                MessageBox.Show("no polygone to delete");
            }
            else
            {
                mainCanvas.Children.Remove(courbeActuelle);
            }
        }

        private void deletePreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainCanvas.Children.Count == 0)
            {
                MessageBox.Show("no polygone to delete");
            }else
            {
                if (courbeActuelle.Points.Count > 0)
                {
                    courbeActuelle.Points.Remove(courbeActuelle.Points[courbeActuelle.Points.Count - 1]);

                }
                else
                {
                    
                    MessageBox.Show("You cant get back anymore");
                }
            }
        }
    }
}
