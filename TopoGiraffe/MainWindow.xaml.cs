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
        List<ArtPoint> PointsArticulation = new List<ArtPoint>();
         List<Ellipse> cercles = new List<Ellipse>();

        PolyLineSegment polylinesegment = new PolyLineSegment();
        bool btn2Clicked = false; bool addLineClicked = false; bool navClicked = false;
        bool finish = false;
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
        FrameworkElement elDraggingEllipse;
        Point ptMouseStart, ptElementStart;

        // for a live preview of the line
        //public void Polyline_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Thickness margin = new Thickness(0, 0, 0, 0);
        //    ptMouseStart = e.GetPosition(this);
        //    elDragging = (this).InputHitTest(ptMouseStart) as FrameworkElement;
        //    if (elDragging == null) return;
        //    if (elDragging != null && elDragging is Polyline)
        //    {
        //        courbeActuelle = (Polyline)elDragging;
        //        //  courbeActuelle.StrokeThickness = 10;
        //        margin = courbeActuelle.Margin;
        //        margin.Left = courbeActuelle.Margin.Left + 100;
        //        courbeActuelle.Margin = margin;
        //    }
        //
        Polyline polytest = new Polyline();
        ArtPoint elDraggingEllip = new ArtPoint();
        int cpt2 = 0;
        public void Cercle_Mousemove(object sender, System.Windows.Input.MouseEventArgs e)
        {
           
            if (((Ellipse)elDraggingEllipse) == null) return;

            if (e.LeftButton == MouseButtonState.Pressed)
            {   
                mainCanvas.Children.Remove(Interpoly);
                //elDraggingEllipse = (this).InputHitTest(e.GetPosition(this)) as FrameworkElement;
                Point ptMouse = e.GetPosition(this.mainCanvas);
                //if (ptMouse.X > 0 && ptMouse.X < mainCanvas.Width && ptMouse.Y > 0 && ptMouse.Y < mainCanvas.Height)
                //{
                Move = true;
                    if (Move == true)
                    {

                        if (elDraggingEllipse == null)
                            elDraggingEllipse = ((Ellipse)elDraggingEllipse);
                        //change the position of the ellipse that represent the control point
                        Canvas.SetLeft(elDraggingEllipse, ptMouse.X - 10 / 2);
                        Canvas.SetTop(elDraggingEllipse, ptMouse.Y - 10 / 2);

                       
                        Ellipse ellipse;

                        foreach (ArtPoint a in PointsArticulation)
                        {
                            if (a.cercle.Equals(elDraggingEllipse))
                            {
                                elDraggingEllip.cercle = a.cercle;
                                elDraggingEllip.p = a.p;
                            }
                        }
               
                       
                        Polyline_Modify( elDraggingEllip.p, ptMouse);
                    
                        cpt2++;


                    }
                //}

            }
        }
        void Ellipse_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((Ellipse)elDraggingEllipse) == null) return;

            //if (((Ellipse)elDraggingEllipse) == null) return;
            //((Ellipse)elDraggingEllipse).Cursor = Cursors.Arrow; //change the cursor
            ((Ellipse)elDraggingEllipse).ReleaseMouseCapture();
            ((Ellipse)elDraggingEllipse).Cursor = Cursors.Arrow; 
        }
        void Ellipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {


           // EditPolyline = courbeActuelle;
          
            elDraggingEllipse = (this).InputHitTest(e.GetPosition(this)) as FrameworkElement;
            if (elDraggingEllipse == null) return;
            if (elDraggingEllipse != null && elDraggingEllipse is System.Windows.Shapes.Ellipse)
            {

                ((Ellipse)elDraggingEllipse).Cursor = Cursors.ScrollAll;

                Mouse.Capture((elDraggingEllipse));
            }

        }


        Polyline Interpoly;
        Polyline EditPolyline;
        int cpt = 0;

        // -------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------- editing polylines

        public void Polyline_Modify( Point p , Point p2)
            {
            
                EditPolyline = courbeActuelle;
            
                

            Interpoly = new Polyline();
            Interpoly.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString((colorComboBox.SelectedItem as RectangleName).Name);
            Interpoly.StrokeThickness = 2;
            Interpoly.Points.Clear();
            Interpoly.FillRule = FillRule.EvenOdd;


         //   mainCanvas.Children.Remove(EditPolyline);


            int i = 0;
                    foreach(Point point in EditPolyline.Points)
                    {
                        if (point == p)
                        {
                         Interpoly.Points.Add(p2);
                         elDraggingEllip.p = p2;

                    }
                    else
                        {
                        Interpoly.Points.Add(EditPolyline.Points[i]);

                        }
                     i++;
                    }
            mainCanvas.Children.Add(Interpoly);
            mainCanvas.Children.Remove(courbeActuelle);
            //courbeActuelle = Interpoly; 
            //mainCanvas.Children.Remove(Interpoly);




            cpt++;
                  
            }
        
            
        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            double x = Mouse.GetPosition(mainCanvas).X;
            double y = Mouse.GetPosition(mainCanvas).Y;
         

            if ((activerDessinCheckBox.IsChecked == true) && (courbeActuelle.Points.Count > 0))
            {



                Line newLine = new Line();
               


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
            if (navClicked == true)
            {

               


               

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
           // (courbeActuelle).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Polyline_MouseDown);
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

        bool Move = false;
       

        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double x = Mouse.GetPosition(mainCanvas).X;
            double y = Mouse.GetPosition(mainCanvas).Y;


            if (btn2Clicked == true)
            {
                Point lastPoint = new Point(x, y);

                


                // ajout des points d'articulation----------------------------------------------------------------------
                //creation d'un Hit test
                Point pt = e.GetPosition((UIElement)sender);
                hitResultsList.Clear();

                // verifier si on veut relier la courbe ou pas pour creer un point d'articulation

                VisualTreeHelper.HitTest(mainCanvas, new HitTestFilterCallback(MyHitTestFilter), new HitTestResultCallback(MyHitTestResult),
                    new PointHitTestParameters(pt));
                bool el = false;
                foreach (Object o in hitResultsList)
                {
                    if (o.GetType() == typeof(Ellipse))
                    {
                        el = true;
                    }
                }
                if (el == false)
                {
                    courbeActuelle.Points.Add(lastPoint);
                    Ellipse circle = new Ellipse();
                    ArtPoint artPoint = new ArtPoint(circle, lastPoint);
                    circle.Width = 15;
                    circle.Height = 15;
                    circle.Fill = Brushes.Purple;
                    (circle).MouseMove += new System.Windows.Input.MouseEventHandler(Cercle_Mousemove);
                    (circle).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(Ellipse_MouseLeftButtonUp);
                    (circle).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Ellipse_MouseLeftButtonDown);
                    Canvas.SetLeft(circle, lastPoint.X - (circle.Width / 2));
                    Canvas.SetTop(circle, lastPoint.Y - (circle.Height / 2));
                    PointsArticulation.Add(artPoint);
                    mainCanvas.Children.Add(circle);
                    el = false;
                }
                else
                {
                    courbeActuelle.Points.Add(courbeActuelle.Points[0]);
                }

            }
            else if (addLineClicked == true)
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

            } else if (navClicked == true)
            {
                Point pt = e.GetPosition((UIElement)sender);
                hitResultsList.Clear();
                Ellipse circle;

                // verifier si on veut relier la courbe ou pas pour creer un point d'articulation

                VisualTreeHelper.HitTest(mainCanvas, new HitTestFilterCallback(MyHitTestFilter), new HitTestResultCallback(MyHitTestResult),
                    new PointHitTestParameters(pt));
                //foreach (Object o in hitResultsList)
                //{
                //   if(o.GetType() == typeof(Polyline))
                //    {
                //        EditPolyline = (Polyline) o;
                //        Move = true;
                //        finish = false;

                //    }
                //}
                foreach(ArtPoint a in PointsArticulation) { 
                    if (a.cercle.IsMouseDirectlyOver == true)
                    {
                        
                        a.cercle.Fill = Brushes.Orange;
                    }
                    else { a.cercle.Fill = Brushes.Purple; }
                }
            }
            
        }
        List<object> Skew =  new List<object>();


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
        //code using C# propreties ------------------------------------------------------------
        //PolyLineSegment polylinesegment = new PolyLineSegment();

        private List<object> hitResultsList = new List<object>();



        private void mainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // transformation de l'objet polyline à un objet pathfigure ( herite de geometry)

            //PathGeometry myPathGeometry = new PathGeometry();
            //PathFigure pathFigure2 = new PathFigure();
            //PolyLineSegment myPolyLineSegment = new PolyLineSegment();
            //myPolyLineSegment.Points = polylines[0].Points;
            //pathFigure2.Segments.Add(myPolyLineSegment);
            //myPathGeometry.Figures.Add(pathFigure2);
            Point pt = e.GetPosition((UIElement)sender);

            // vider la liste des resultats du Hit Test

            hitResultsList.Clear();

       
                // set up d'une CallBack funtion pour recevoir l'enum du resultat du hit test
                VisualTreeHelper.HitTest(mainCanvas, new HitTestFilterCallback(MyHitTestFilter),
                    new HitTestResultCallback(MyHitTestResult),
                     new PointHitTestParameters(pt));

            


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
            return HitTestResultBehavior.Continue;
        }
        public HitTestFilterBehavior MyHitTestFilter(DependencyObject o)
        {
            // Test for the object value you want to filter.
            if (o.GetType() == typeof(Canvas) /*|| o.GetType() == typeof(Polyline)*/)
            {
                // Visual object and descendants are NOT part of hit test results enumeration.
                return HitTestFilterBehavior.ContinueSkipSelf;
            }
            else
            {
                // Visual object is part of hit test results enumeration.
                return HitTestFilterBehavior.Continue;
            }
        }
        
        private void nav_Click(object sender, RoutedEventArgs e)
        {
            navClicked = true;
            addLineClicked = false;
            btn2Clicked = false;
        }

       
        //private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    finish = true;
        //}
    }
}
