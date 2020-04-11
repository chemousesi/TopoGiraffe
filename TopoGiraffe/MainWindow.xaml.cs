using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
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

         List<Ellipse> cercles = new List<Ellipse>();

        PolyLineSegment polylinesegment = new PolyLineSegment();
        bool btn2Clicked = false; bool addLineClicked = false;
        Polyline poly = new Polyline();
        int LinePointscpt = 0;





        class RectangleName
        {
            public Rectangle Rect { get; set; }
            public string Name { get; set; }
        }






                     
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "TopoGiraffe";




            // this here is for the colors
            var values = typeof(Brushes).GetProperties().
                Select(p => new {Name =  p.Name, Brush = p.GetValue(null) as Brush }).
                ToArray();
            var brushNames = values.Select(v => v.Name);

         

            List<RectangleName> rectangleNames = new List<RectangleName>();

            foreach(string brushName in brushNames) { 
         
                RectangleName rn = new RectangleName { Rect = new Rectangle { Fill = new BrushConverter().ConvertFromString(brushName) as Brush }, Name = brushName };
                rectangleNames.Add(rn);
            }

            colorComboBox.ItemsSource = rectangleNames;
            colorComboBox.SelectedIndex = 7;
            // colors end here

              
          


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


            // for a live preview of the line 
            
        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {

            if ((activerDessinCheckBox.IsChecked == true) && (courbeActuelle.Points.Count > 0))
            {



                Line newLine = new Line();
                double x = Mouse.GetPosition(mainCanvas).X;
                double y = Mouse.GetPosition(mainCanvas).Y;


                newLine.Stroke = System.Windows.Media.Brushes.Black;
                newLine.X1 = courbeActuelle.Points[courbeActuelle.Points.Count - 1].X;
                newLine.Y1 = courbeActuelle.Points[courbeActuelle.Points.Count - 1].Y;

                newLine.X2 = x;
                newLine.Y2 = y;
                //newLine.HorizontalAlignment = HorizontalAlignment.Left;
                //newLine.VerticalAlignment = VerticalAlignment.Center;
                newLine.StrokeThickness = 3; 
                mainCanvas.Children.Add(newLine);
                //Thread.Sleep(10);
                

                mainCanvas.Children.Remove(newLine);

            }
        }
    
       


        private void dessinerButton_Click(object sender, RoutedEventArgs e)
        {
            btn2Clicked = true;
            Polyline myPolyline = new Polyline();
            polylines.Add(myPolyline);
            activerDessinCheckBox.IsChecked = true;
            // styling

            myPolyline.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString((colorComboBox.SelectedItem as RectangleName).Name);
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
                polylines.Clear();
                mainCanvas.Children.Clear();
                foreach (Ellipse cercle in cercles)
                {
                    mainCanvas.Children.Remove(cercle);

                }
                cercles.Clear();
                IntersectionPoints.Clear();
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
                    foreach(Ellipse cercle in cercles)
                    {
                        mainCanvas.Children.Remove(cercle);
                       
                    }
                    cercles.Clear();
                    IntersectionPoints.Clear();
                }
                else
                {
                    polylines.Remove(polylines[polylines.Count - 1]);
                    
                    if (polylines.Count > 0)
                    {
                        courbeActuelle = polylines[polylines.Count - 1];

                    }
                    else
                    {
                        MessageBox.Show("no polygone to delete");
                    }

                }
            }
        }






        // image visibilty with the display button
        private void display_Click(object sender, RoutedEventArgs e)
        { 
           if (imgPhoto.Visibility == Visibility.Visible)
            {
                imgPhoto.Visibility = Visibility.Hidden;
            }
            else
            {
                imgPhoto.Visibility = Visibility.Visible;
            }
        }

        private void altitudeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (altitudeTextBox.Text == "")
            {
                altitudeTextBox.Text = "Altitude";
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void border_KeyDown(object sender, KeyEventArgs e)
        {

        }


       

        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double x = Mouse.GetPosition(mainCanvas).X;
            double y = Mouse.GetPosition(mainCanvas).Y;


            if (btn2Clicked == true)
            {
                Point lastPoint = new Point(x, y);

                courbeActuelle.Points.Add(lastPoint);

            }else if (addLineClicked == true)
                    {
                        LinePointscpt++;
                        poly.Points.Add(new Point(x, y));
                        // calcul des points d'intersection
                        if (LinePointscpt == 2)
                        {

                            line.X1 = poly.Points[0].X;
                            line.Y1 = poly.Points[0].Y;
                            line.X2 = poly.Points[1].X;
                            line.Y2 = poly.Points[1].Y;

                            foreach (Polyline polyline in polylines)
                            {
                                FindIntersection(polyline, line);
                            }

                            // dessin des cercles representant les points d'intersection
                            foreach (IntersectionDetail inters in IntersectionPoints)
                            {
                                Ellipse cercle = new Ellipse();
                                cercle.Width = 15;
                                cercle.Height = 15;
                                cercle.Fill = System.Windows.Media.Brushes.Red;
                                Canvas.SetLeft(cercle, inters.point.X - (cercle.Width / 2));
                                Canvas.SetTop(cercle, inters.point.Y - (cercle.Height / 2));
                                cercles.Add(cercle);
                                mainCanvas.Children.Add(cercle);
                            }

                            addLineClicked = false;
                        }

                    } // finish here
        }
            


           List<IntersectionDetail> IntersectionPoints  = new List<IntersectionDetail>();

            // code d'intersection -------------------------------------------------------------------------------------------------------------------------------
            //----------------------------------------------------------------------------------------------------------------------------------------------------


            Line line = new Line();
            public void FindIntersection( Polyline p , Line line)
                {
                    Line myLine = new Line();
                    IntersectionDetail inter;

                    for (int i = 0; i < p.Points.Count - 1; i++)
                    {
                        myLine.X1 = p.Points[i].X;      myLine.Y1 = p.Points[i].Y;
                        myLine.X2 = p.Points[i + 1].X;  myLine.Y2 = p.Points[i + 1].Y;
                        inter = intersectLines(myLine, line);
                        if ( inter.intersect == true )
                        {
                            IntersectionPoints.Add(inter);
                        }


                     }


                }
                   // intersection d'un segment avec une ligne 
                public IntersectionDetail intersectLines(Line line1 , Line line2)
                {
                    Equation equation1;
                    Equation equation2;
                    Boolean intersect = new Boolean();

                    Point interscetionPoint = new Point();
                    Point a1 = new Point(line1.X1, line1.Y1);
                    Point b1 = new Point(line1.X2, line1.Y2);
                    Point a2 = new Point(line2.X1, line2.Y1);
                    Point b2 = new Point(line2.X2, line2.Y2);

                    equation1 = GetSegmentEquation(a1, b1);
                    equation2 = GetSegmentEquation(a2, b2);

                    if (equation1.a == equation2.a)
                    {
                    intersect = false;
                    } else {
                
                    interscetionPoint.X = -(equation1.b - equation2.b) / (equation1.a - equation2.a);
                    interscetionPoint.Y = (equation2.a * interscetionPoint.X) + equation2.b;

                        if ( (Math.Max(line1.X1, line1.X2) >= interscetionPoint.X) && ( interscetionPoint.X >=  Math.Min(line1.X1, line1.X2)) 
                        && (Math.Max(line1.Y1, line1.Y2) >= interscetionPoint.Y) && (interscetionPoint.Y >= Math.Min(line1.Y1, line1.Y2))
                        && (Math.Max(line2.X1, line2.X2) >= interscetionPoint.X) && (interscetionPoint.X >= Math.Min(line2.X1, line2.X2))
                        && (Math.Max(line2.Y1, line2.Y2) >= interscetionPoint.Y) && (interscetionPoint.Y >= Math.Min(line2.Y1, line2.Y2)))
                        {
                             intersect = true;
                        }

                    }

                 return new IntersectionDetail(interscetionPoint, intersect);

                } 


                public Equation GetSegmentEquation(Point a , Point b)
                {
                    Equation equation = new Equation();

                equation.a = (a.Y - b.Y) / (a.X - b.X);
                equation.b = -(equation.a)*(a.X) + a.Y;

                    return equation;
                }

                private void add_line_Click(object sender, RoutedEventArgs e)
                {
                    poly = new Polyline();
                    addLineClicked = true;
                    btn2Clicked = false;
                    LinePointscpt = 0;
                    poly.Stroke = Brushes.Indigo;
                    poly.StrokeThickness = 5;
                    poly.FillRule = FillRule.EvenOdd;
                    polylines.Add(poly);
                    courbeActuelle = poly;
                    mainCanvas.Children.Add(poly);

            }
     
    

        
    }
}
