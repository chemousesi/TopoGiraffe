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
        PolyLineSegment polylinesegment = new PolyLineSegment();
        




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


        Polyline poly = new Polyline();

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            double x = Mouse.GetPosition(mainCanvas).X;
            double y = Mouse.GetPosition(mainCanvas).Y;
            if (checkb.IsChecked == true)
            {
                MessageBox.Show("le point est " + x + " , " + y);
            }


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

            }else {
                poly.Points.Add(new Point(x, y));
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


        private List<DrawingVisual> hits = new List<DrawingVisual>();
        List<IntersectionDetail> IntersectionPoints  = new List<IntersectionDetail>();
      
        // code d'intersection ------------------------------------------------------------------

            public void FindIntersection( Polyline p , Line line)
            {
                Line myLine = new Line();
                IntersectionDetail inter;

                for (int i = 0; i < p.Points.Count - 2; i++)
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















            //code using C# propreties ------------------------------------------------------------

        private List<object> hitResultsList = new List<object>();

        

        private void mainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // transformation de l'objet polyline à un objet pathfigure ( herite de geometry)

            PathGeometry myPathGeometry = new PathGeometry();
            PathFigure pathFigure2 = new PathFigure();
            PolyLineSegment myPolyLineSegment = new PolyLineSegment();
            myPolyLineSegment.Points = polylines[0].Points;
            pathFigure2.Segments.Add(myPolyLineSegment);
            myPathGeometry.Figures.Add(pathFigure2);

            // vider la liste des resultats du Hit Test
            hitResultsList.Clear();

            foreach (Polyline p in  polylines){
                // set up d'une CallBack funtion pour recevoir l'enum du resultat du hit test
                VisualTreeHelper.HitTest(p, new HitTestFilterCallback(MyHitTestFilter),
                    new HitTestResultCallback(MyHitTestResult),
                    new GeometryHitTestParameters(myPathGeometry));

            }
           

            // affichage du nombre d'intersections.
            if (hitResultsList.Count >= 0)
            {
                MessageBox.Show("Number of Visuals Hit: " + hitResultsList.Count);
            }
        }

        // fonction Callback qui determine le comportement du Hittest
        public HitTestResultBehavior MyHitTestResult(HitTestResult result)
        {
            // Add the hit test result to the list that will be processed after the enumeration.
            //ajout du resultat à la liste 
            hitResultsList.Add(result.VisualHit);

            // Set the behavior to return visuals at all z-order levels.
            return HitTestResultBehavior.Stop;
        }
        public HitTestFilterBehavior MyHitTestFilter(DependencyObject o)
        {
            // Test for the object value you want to filter.
            if (o.GetType() == typeof(Canvas))
            {
                // Visual object and descendants are NOT part of hit test results enumeration.
                return HitTestFilterBehavior.Continue;
            }
            else
            {
                // Visual object is part of hit test results enumeration.
                return HitTestFilterBehavior.Continue;
            }
        }
        Line line = new Line();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            activerDessinCheckBox.IsChecked = false;

            // styling

            line.X1 = poly.Points[0].X;
            line.Y1 = poly.Points[0].Y;
            line.X2 = poly.Points[1].X;
            line.Y2 = poly.Points[1].Y;


            line.Stroke = System.Windows.Media.Brushes.Black;
            line.StrokeThickness = 2;

            poly.Stroke = System.Windows.Media.Brushes.Black;
            poly.StrokeThickness = 2;
            poly.FillRule = FillRule.EvenOdd;
            mainCanvas.Children.Add(poly);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            foreach(Polyline polyline in polylines) { 
                 FindIntersection(polyline, line);
            }
            foreach (IntersectionDetail inters in IntersectionPoints)
            {
                //MessageBox.Show("le point est " + inters.point.X + "," + inters.point.Y);
                Ellipse cercle = new Ellipse();

                cercle.Width = 15;
                cercle.Height = 15;
                cercle.Fill = System.Windows.Media.Brushes.Red;
                Canvas.SetLeft(cercle, inters.point.X - (cercle.Width / 2));
                Canvas.SetTop(cercle, inters.point.Y - (cercle.Height / 2));
                cercles.Add(cercle);
                mainCanvas.Children.Add(cercle);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
