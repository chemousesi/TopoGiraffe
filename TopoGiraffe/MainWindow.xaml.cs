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

        private void btn3_Click(object sender, RoutedEventArgs e)
        {

            PathGeometry myPathGeometry = new PathGeometry();
            PathFigure pathFigure2 = new PathFigure();
            PolyLineSegment myPolyLineSegment = new PolyLineSegment();
            myPolyLineSegment.Points = polylines[1].Points;
            pathFigure2.Segments.Add(myPolyLineSegment);
            myPathGeometry.Figures.Add(pathFigure2);


            // line
            PathGeometry myPathGeometry1 = new PathGeometry();
            PathFigure pathFigure3 = new PathFigure();
            PolyLineSegment myPolyLineSegment1 = new PolyLineSegment();
            myPolyLineSegment1.Points = polylines[0].Points;
            pathFigure3.Segments.Add(myPolyLineSegment1);
            myPathGeometry1.Figures.Add(pathFigure3);
            VisualTreeHelper.HitTest(polylines[0], null, new HitTestResultCallback(HitTestCallback), new GeometryHitTestParameters(myPathGeometry));
     
            MessageBox.Show("hey :" +hits.Count);
         
        }
        private List<DrawingVisual> hits = new List<DrawingVisual>();
       

        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            GeometryHitTestResult geometryResult = (GeometryHitTestResult)result;
             DrawingVisual visual = result.VisualHit as DrawingVisual;
            // Only include matches that are DrawingVisual objects and
            // that are completely inside the geometry.
            MessageBox.Show("heyyouu");

            if (visual != null &&
            geometryResult.IntersectionDetail == IntersectionDetail.Intersects)
            {
                hits.Add(visual);
            }
            return HitTestResultBehavior.Continue;
        }
        private List<object> hitResultsList = new List<object>();

        

        private void mainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            PathGeometry myPathGeometry = new PathGeometry();
            PathFigure pathFigure2 = new PathFigure();
            PolyLineSegment myPolyLineSegment = new PolyLineSegment();
            myPolyLineSegment.Points = polylines[1].Points;
            pathFigure2.Segments.Add(myPolyLineSegment);
            myPathGeometry.Figures.Add(pathFigure2);


            // Retrieve the coordinate of the mouse position.
            Point pt = e.GetPosition((UIElement)sender);

            // Clear the contents of the list used for hit test results.
            hitResultsList.Clear();

            // Set up a callback to receive the hit test result enumeration.
            VisualTreeHelper.HitTest(polylines[0], new HitTestFilterCallback(MyHitTestFilter),
                new HitTestResultCallback(MyHitTestResult),
                new GeometryHitTestParameters(myPathGeometry));

            // Perform actions on the hit test results list.
            if (hitResultsList.Count >= 0)
            {
                MessageBox.Show("Number of Visuals Hit: " + hitResultsList.Count);
            }
        }
        public HitTestResultBehavior MyHitTestResult(HitTestResult result)
        {
            // Add the hit test result to the list that will be processed after the enumeration.
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
    }
}
