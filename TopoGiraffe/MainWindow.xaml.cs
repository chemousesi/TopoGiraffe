    using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TopoGiraffe.Noyau;
using TopoSurf.MessageBoxStyle;
using System.Windows.Controls.Primitives;

//using TopoGiraffe.MessageBoxStyle;


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
        bool btn2Clicked = false; bool addLineClicked = false; bool navClicked = false;
        bool finish = false;
        Polyline poly = new Polyline();
        int LinePointscpt = 0;
        Boolean firstPoint = true;
        List<int> Altitudes = new List<int>();

        List<List<ArtPoint>> PointsGlobal = new List<List<ArtPoint>>();
        int nbCourbes = 0;

        Plan plan;
        Line scaleLine;
        Boolean drawingScale = false;
        Echelle mainScale;




        public MainWindow()
        {
            InitializeComponent();
            this.Title = "TopoGiraffe";






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
            // colors end here

            styleCourbeCmb.SelectedIndex = 0;



          

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


            imgPhoto.Opacity = .5;
            OpenInitialDialogBox();


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
                courbeActuelle = polylines[polylines.Count - 1];
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
       

        private void border_KeyDown(object sender, KeyEventArgs e)
        {

        }


        // for a live preview of the line 

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            double x = Mouse.GetPosition(mainCanvas).X;
            double y = Mouse.GetPosition(mainCanvas).Y;


            if (btn2Clicked == true && (courbeActuelle.Points.Count > 0))
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
                MouseMoveOnDraw(sender, e);

            }
            if (addLineClicked == true)
            {
                MouseMoveOnAddLine(sender, e);
            }
            if (navClicked == true)
            {






            }


        }

        Polyline temporaryFigure;
        private void MouseMoveOnDraw(object sender, MouseEventArgs e)
        {
            

            if (finalCtrlPoint == false && btn2Clicked == true)
            {

                if (currentCurveCtrlPts.Count != 0) //to handle real-time drawing
                {
                    if (courbeActuelle.Points.Last().Equals(mousePos))
                    {
                        courbeActuelle.Points.RemoveAt(courbeActuelle.Points.Count - 1);
                    }
                    if (polylines.Contains(temporaryFigure))
                    {
                        polylines.Remove(temporaryFigure);
                    }
                    mousePos = new Point(e.GetPosition(this.mainCanvas).X, e.GetPosition(this.mainCanvas).Y);
                    temporaryFigure = courbeActuelle;
                    courbeActuelle.Points.Add(mousePos);

                }
               
                    polylines.Add(courbeActuelle);
                
            }
           

        }
        private void MouseMoveOnAddLine(object sender, MouseEventArgs e)
        {



            if (courbeActuelle.Points.Count != 0) //to handle real-time drawing
            {
                if (courbeActuelle.Points.Last().Equals(mousePos))
                {
                    courbeActuelle.Points.RemoveAt(courbeActuelle.Points.Count - 1);
                }
                if (polylines.Contains(temporaryFigure))
                {
                    polylines.Remove(temporaryFigure);
                }
                mousePos = new Point(e.GetPosition(this.mainCanvas).X, e.GetPosition(this.mainCanvas).Y);
                temporaryFigure = courbeActuelle;
                courbeActuelle.Points.Add(mousePos);
            }



            polylines.Add(courbeActuelle);

        }



        private void dessinerButton_Click(object sender, RoutedEventArgs e)
        {
            btn2Clicked = true;
            dragbool = false;

            Polyline myPolyline = new Polyline();
            polylines.Add(myPolyline);
            activerDessinCheckBox.IsChecked = true; navClicked = false;

            // styling

            myPolyline.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString((colorComboBox.SelectedItem as RectangleName).Name);
            //myPolyline.StrokeThickness = 2;
            //myPolyline.FillRule = FillRule.EvenOdd;
            StyleCmbToRealStyle(myPolyline, styleCourbeCmb.SelectedIndex);
            myPolyline.MouseMove += new System.Windows.Input.MouseEventHandler(Path_MouseMove);
            myPolyline.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(Path_MouseLeftButtonUp);
            myPolyline.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Path_MouseLeftButtonDown);

            courbeActuelle = myPolyline;
            // (courbeActuelle).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Polyline_MouseDown);
            mainCanvas.Children.Add(courbeActuelle);
            PointsGlobal.Add(new List<ArtPoint>());
            currentCurveCtrlPts = PointsGlobal[PointsGlobal.Count - 1];
            indexPoints++;
            finalCtrlPoint = false;


            OpenCourbeinfo();







        }




        private void mainCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (activerDessinCheckBox.IsChecked == true)
            {
                mainCanvas.Cursor = Cursors.Cross;
            }
        }

        


        // to eliminate placeholders

        private void altitudeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            altitudeTextBox.Text = "";
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
            }
            else
            {
                if (courbeActuelle.Points.Count > 0)
                {
                    courbeActuelle.Points.Remove(courbeActuelle.Points[courbeActuelle.Points.Count - 1]);
                    foreach (Ellipse cercle in cercles)
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



        bool Move = false;
        int indexPoints = -1;




        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double x = Mouse.GetPosition(mainCanvas).X;
            double y = Mouse.GetPosition(mainCanvas).Y;
            bool inter = false;

          
                if (btn2Clicked == true)
                {
                    firstPoint = true;
                    Point lastPoint = new Point(x, y);

                // ajout des points d'articulation----------------------------------------------------------------------
            
                    if (finalCtrlPoint == false)
                    {
                   
                   
                        // verify that the polyline doesn't intersect itself



                        if (courbeActuelle.Points.Count > 2)
                        {
                            Line l = new Line();
                            l.X1 = courbeActuelle.Points[courbeActuelle.Points.Count - 2].X;
                            l.Y1 = courbeActuelle.Points[courbeActuelle.Points.Count - 2].Y;
                            l.X2 = lastPoint.X;
                            l.Y2 = lastPoint.Y;

                             inter = FindIntersection1(courbeActuelle, l);


                            if (inter == true)
                            {
                               
                                new MssgBox("       Erreur de Dessin de la Courbe !\n veuillez dessiner votre segment à nouveau ").ShowDialog();

                            }

                        }
                        if (inter == false)
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

                            mainCanvas.Children.Add(circle);

                            PointsGlobal[indexPoints].Add(artPoint);
                        }

                    }
                    else
                    // when click on the last (first) point 
                    {
                        courbeActuelle.Points.RemoveAt(courbeActuelle.Points.Count - 1);
                        courbeActuelle.Points.Add(courbeActuelle.Points[0]);
                        btn2Clicked = false;
                        dragbool = true;
                        //PointsGlobal[indexPoints].Add(artPoint);

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
                    
                    if (scaleLinePointsCount == 2)
                    {
                        MessageBox.Show(" Distance :" + Math.Round(mainScale.FindDistanceOnField(Outils.DistanceBtwTwoPoints(poly.Points[0], poly.Points[1])), 2) + " mètres");

                    }

                    addLineClicked = false;
                }

            }
            else if (drawingScale == true)
                
            {

                scaleLinePointsCount++;
                scalePolyline.Points.Add(new Point(x, y));

                if (scaleLinePointsCount == 2)
                {
                    mainScale.scaleDistanceOnCanvas = Outils.DistanceBtwTwoPoints(scalePolyline.Points[0], scalePolyline.Points[1]);
                    
                    string message = "Echelle sur plan "+ Math.Round(mainScale.scaleDistanceOnCanvas, 3) + "------>"+ mainScale.scaleDistanceOnField+" mètres";
                    MessageBox.Show(message);
                    drawingScale = false;
                }

            }
                

        }
        List<object> Skew = new List<object>();

         

        List<IntersectionDetail> IntersectionPoints = new List<IntersectionDetail>();

        // code d'intersection -------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------


        Line line = new Line();
        public void FindIntersection(Polyline p, Line line)
        {
            Line myLine = new Line();
            IntersectionDetail inter;

            for (int i = 0; i < p.Points.Count - 1; i++)
            {
                myLine.X1 = p.Points[i].X; myLine.Y1 = p.Points[i].Y;
                myLine.X2 = p.Points[i + 1].X; myLine.Y2 = p.Points[i + 1].Y;
                inter = intersectLines(myLine, line);
                if (inter.intersect == true)
                {
                    IntersectionPoints.Add(inter);
                }


            }


        }
        public bool FindIntersection1(Polyline p, Line line)
        {
            Line myLine = new Line();
            IntersectionDetail inter;
            bool inters = false;

            for (int i = 0; i < p.Points.Count - 1; i++)
            {
                myLine.X1 = p.Points[i].X; myLine.Y1 = p.Points[i].Y;
                myLine.X2 = p.Points[i + 1].X; myLine.Y2 = p.Points[i + 1].Y;
                inter = intersectLines(myLine, line);
                if (inter.intersect == true && ((Math.Max(myLine.X1, myLine.X2) > inter.point.X) && (inter.point.X > Math.Min(myLine.X1, myLine.X2))
                && (Math.Max(myLine.Y1, myLine.Y2) > inter.point.Y) && (inter.point.Y > Math.Min(myLine.Y1, myLine.Y2))
                && (Math.Max(line.X1, line.X2) > inter.point.X) && (inter.point.X > Math.Min(line.X1, line.X2))
                && (Math.Max(line.Y1, line.Y2) > inter.point.Y) && (inter.point.Y > Math.Min(line.Y1, line.Y2))))
                {
                    inters= inter.intersect;
                }


            }
            return inters;

        }
        // intersection d'un segment avec une ligne 
        public IntersectionDetail intersectLines(Line line1, Line line2)
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
            }
            else
            {

                interscetionPoint.X = -(equation1.b - equation2.b) / (equation1.a - equation2.a);
                interscetionPoint.Y = (equation2.a * interscetionPoint.X) + equation2.b;

                if ((Math.Max(line1.X1, line1.X2) >= interscetionPoint.X) && (interscetionPoint.X >= Math.Min(line1.X1, line1.X2))
                && (Math.Max(line1.Y1, line1.Y2) >= interscetionPoint.Y) && (interscetionPoint.Y >= Math.Min(line1.Y1, line1.Y2))
                && (Math.Max(line2.X1, line2.X2) >= interscetionPoint.X) && (interscetionPoint.X >= Math.Min(line2.X1, line2.X2))
                && (Math.Max(line2.Y1, line2.Y2) >= interscetionPoint.Y) && (interscetionPoint.Y >= Math.Min(line2.Y1, line2.Y2)))
                {
                    intersect = true;
                }

            }

            return new IntersectionDetail(interscetionPoint, intersect);

        }


        public Equation GetSegmentEquation(Point a, Point b)
        {
            Equation equation = new Equation();

            equation.a = (a.Y - b.Y) / (a.X - b.X);
            equation.b = -(equation.a) * (a.X) + a.Y;

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





        public void StyleCmbToRealStyle(Polyline pol, int index)
        {
            switch (index)
            {
                case 0: //courbe simple
                    pol.StrokeThickness = 2;
                    pol.StrokeDashArray = null;

                    break;
                case 1:// courbe intermediaire
                    pol.StrokeThickness = 2;
                    pol.StrokeDashArray.Add(1);
                    pol.StrokeDashArray.Add(3);
                    pol.StrokeDashArray.Add(1);
                    break;
                case 2:// courbe maitresse
                    pol.StrokeThickness = 3.5;
                    pol.StrokeDashArray = null;

                    break;
                default:
                    break;

            }

        }




        private void mainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                MessageBox.Show("back key pressed");
            }
        }








        public int NbCourbes
        {
            get { return nbCourbes; }
            set { nbCourbes = value; }
        }






        private List<object> hitResultsList = new List<object>();



        private void mainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn2Clicked = false;
            courbeActuelle.Points.RemoveAt(courbeActuelle.Points.Count - 1);
            if (polylines.Contains(courbeActuelle) == false)
            {
                polylines.Add(courbeActuelle);
            }
            if (polylines.Count != 0)
            {
                int indexp = polylines.IndexOf(courbeActuelle);
                
            }
        }


        private void nav_Click(object sender, RoutedEventArgs e)
        {
            navClicked = true;
            addLineClicked = false;
            btn2Clicked = false;
            finalCtrlPoint = false;
            // EditPolyline = courbeActuelle;
        }

        int cptdebug = 0; int index2 = 0;
        FrameworkElement elDraggingEllipse;
        Point ptMouseStart, ptElementStart;

        //------------------------------------------------------------------------------------------------------------------------------------------------
        // code to handle Modifying Control Points --------------------------------------------------------------------------------------------------------


        Polyline polytest = new Polyline();

        bool dragbool;
        public void Cercle_Mousemove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ArtPoint elDraggingEllip = new ArtPoint();
            if (((Ellipse)elDraggingEllipse) == null) return;

            if (e.LeftButton == MouseButtonState.Pressed && navClicked == true)
            {

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
                    int cpt3 = 0;
                    foreach (List<ArtPoint> ae in PointsGlobal)
                    {

                        foreach (ArtPoint a in ae)
                        {
                            if (a.cercle.Equals(elDraggingEllipse))
                            {
                                elDraggingEllip.cercle = a.cercle;
                                elDraggingEllip.P = a.P;

                                index2 = cpt3;
                            }
                        }

                        cpt3++;
                    }
                    EditPolyline = polylines[index2];
                    //Polyline_Modify();


                    Interpoly = new Polyline();
                    Interpoly.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString((colorComboBox.SelectedItem as RectangleName).Name);
                    StyleCmbToRealStyle(Interpoly, styleCourbeCmb.SelectedIndex);

                    //Interpoly.StrokeThickness = 2;
                    Interpoly.Points.Clear();
                    //Interpoly.FillRule = FillRule.EvenOdd;
                    //courbeActuelle = Interpoly;

                    //   mainCanvas.Children.Remove(EditPolyline);


                    int i = 0;
                    PointsGlobal[index2].Remove(elDraggingEllip);

                    foreach (Point point in EditPolyline.Points)
                    {
                        if (point.Equals(elDraggingEllip.P))
                        {
                            Interpoly.Points.Add(ptMouse);
                            elDraggingEllip = new ArtPoint((Ellipse)elDraggingEllipse, ptMouse);

                            PointsGlobal[index2].Add(elDraggingEllip);
                        }
                        else
                        {
                            if (dragbool == true && i == EditPolyline.Points.Count - 1)
                            {
                                Interpoly.Points.Add(EditPolyline.Points[0]);

                            }
                            else
                            {
                                Interpoly.Points.Add(EditPolyline.Points[i]);
                            }
                        }
                        i++;
                    }
                   


                    //mainCanvas.Children.Remove(courbeActuelle);
                    mainCanvas.Children.Add(Interpoly);
                    mainCanvas.Children.Remove(EditPolyline);

                    EditPolyline = Interpoly;
                    polylines[index2] = Interpoly;







                }
                //}

            }
        }
        void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (((Ellipse)elDraggingEllipse) == null) return;

            //if (((Ellipse)elDraggingEllipse) == null) return;
            //((Ellipse)elDraggingEllipse).Cursor = Cursors.Arrow; //change the cursor
            ((Ellipse)elDraggingEllipse).ReleaseMouseCapture();
            // EditPolyline.Points.Clear();
            int i1 = 0;
            if (navClicked)
            {
                foreach (ArtPoint el in PointsGlobal[index2])
                {
                    mainCanvas.Children.Remove(el.cercle);
                    mainCanvas.Children.Add(el.cercle);

                }
            }

            cptdebug++;
            Ellipse elli = (Ellipse)elDraggingEllipse;
            elli.Fill = Brushes.Purple;

            ((Ellipse)elDraggingEllipse).Cursor = Cursors.Arrow;

        }

        void Ellipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {


            elDraggingEllipse = (this).InputHitTest(e.GetPosition(this)) as FrameworkElement;
            if (elDraggingEllipse == null) return;
            if (elDraggingEllipse != null && elDraggingEllipse is Ellipse)
            {

                ((Ellipse)elDraggingEllipse).Cursor = Cursors.ScrollAll;

                Mouse.Capture((elDraggingEllipse));
            }
            if (btn2Clicked == true) // clicking on an ellipse while drawing
            {
                finalCtrlPoint = true;
              
            }
            Ellipse elli = (Ellipse)elDraggingEllipse;
            elli.Fill = Brushes.Orange;


        }


        Polyline Interpoly;
        Polyline EditPolyline;
        bool finalCtrlPoint = false;
        List<ArtPoint> currentCurveCtrlPts;
        Point mousePos;


        //------------------------------------------------------------------------------------------------------------------------------------------------
        // code to handle dragging of the poyline --------------------------------------------------------------------------------------------------------
        bool isDragging, mvCtrl = true;
        FrameworkElement elDragging, selectedPath, selectedPolyline;
        double minX, minY, maxX, maxY;
        int indexdrag = 0;
        List<ArtPoint> DragPoints;
        Thickness margin;
      

        void Path_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            selectedPolyline = (this).InputHitTest(e.GetPosition(this)) as FrameworkElement;
            if (selectedPolyline == null) return;
            if (selectedPolyline != null && selectedPolyline is Polyline)
            {
                courbeActuelle = (Polyline) selectedPolyline;

                if (navClicked == true)
                {
                   


                    mvCtrl = true;
                    ptMouseStart = e.GetPosition(this);
                    elDragging = (this).InputHitTest(ptMouseStart) as FrameworkElement;
                    if (elDragging == null) return;
                    if (elDragging != null && elDragging is Polyline)
                    {
                        ptElementStart = new Point(elDragging.Margin.Left, elDragging.Margin.Top);
                        margin = new Thickness(elDragging.Margin.Left, elDragging.Margin.Top, 0, 0);
                        elDragging.Cursor = Cursors.ScrollAll;
                        Mouse.Capture((elDragging));
                        isDragging = true;
                        //maxX = MaxPtCtrlX((Polyline)elDragging) - ptMouseStart.X;
                        //maxY = MaxPtCtrlY((Polyline)elDragging) - ptMouseStart.Y;
                        //minX = -MinPtCtrlX((Polyline)elDragging) + ptMouseStart.X;
                        //minY = -MinPtCtrlY((Polyline)elDragging) + ptMouseStart.Y;
                    }
                    foreach (Polyline polyline in polylines)
                    {
                        if (elDragging == polyline)
                        {
                            DragPoints = PointsGlobal[polylines.IndexOf(polyline)];
                        }
                    }
                    int i = polylines.IndexOf((Polyline) selectedPolyline);
                    AltitudeLabel.Visibility = Visibility.Visible;
                    AltitudeLabel.Content = Altitudes[i];
                }
                else return;

            }
        }

        

        bool test = false;
        void Path_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Point pnt = e.GetPosition(this);
            if (elDragging == null) return;

            if (isDragging)
            {
                if (!mvCtrl)
                {
                    elDragging.Margin = margin;

                    foreach (ArtPoint ell in DragPoints)
                    {
                        ell.cercle.Margin = margin;
                    }

                }


                //foreach(Polyline polyline in polylines){

                //    if (polyline.Equals(elDragging))
                //    {

                //        for (int i = 0; i < polyline.Points.Count; i++)
                //        {
                //            polyline.Points[i] = new Point(polyline.Points[i].X + margin.Left - ptElementStart.X, polyline.Points[i].Y + margin.Top - ptElementStart.Y);
                //        }
                //        //for (int i = 0; i < r.Item3.Count; i++)
                //        //{
                //        //    r.Item3[i] = new Point(r.Item3[i].X + margin.Left - ptElementStart.X, r.Item3[i].Y + margin.Top - ptElementStart.Y);
                //        //}
                //        break;



                //    }

                //}

            }
            isDragging = false;
            (elDragging).Cursor = Cursors.Arrow;
            (elDragging).ReleaseMouseCapture();
            elDragging = null;
            test = true;
        }

        /* ------------------------------------------------------------------------------- POPUP DEBUT ---------------------------------------------------- */
        private void Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = btn14;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Settings";
        }

        private void Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void import_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = import;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Importer une carte";
        }

        private void import_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void display_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = display;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Afficher le dessin";
        }

        private void display_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }


        private void GenererProfil_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = GenererProfil;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Générer le profil";
        }

        private void GenererProfil_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void add_line_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = add_line;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Tracer un segment";
        }

        private void add_line_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void dessinerButton_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = dessinerButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Tracer une courbe";
        }

        private void dessinerButton_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void deleteAllButton_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = deleteAllButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Supprimer tout";
        }

        private void deleteAllButton_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        /* ----------------------------------------------------------------------------- POPUP FIN ------------------------------------------ */

        void Path_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {//path dragging
            if ((elDragging) == null) return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point ptMouse = e.GetPosition(this);
                if (isDragging)
                {

                    if (elDragging == null)
                        elDragging = (elDragging);
                    double left = ptElementStart.X + ptMouse.X - ptMouseStart.X;
                    double top = ptElementStart.Y + ptMouse.Y - ptMouseStart.Y;

                    foreach (ArtPoint ell in DragPoints)
                    {

                        ell.cercle.Margin = new Thickness(left, top, 0, 0);// modify the margin to move the curve

                    }

                    elDragging.Margin = new Thickness(left, top, 0, 0);// modify the margin to move the curve
                    if (mvCtrl)
                    {
                        margin = elDragging.Margin;
                    }
                    //if (!(ptMouse.X + maxX <= 920 && ptMouse.X - minX >= 5 && ptMouse.Y - minY >= 5 && ptMouse.Y + maxY <= 525))
                    //{
                    //    //in order to the curve stay in the canvas
                    //    if (ptMouse.X + maxX > 920 && mvCtrl)
                    //        margin.Left = margin.Left - ptMouse.X - maxX + 920;
                    //    if (ptMouse.X - minX < 5 && mvCtrl)
                    //        margin.Left = margin.Left + 5 - ptMouse.X + minX;
                    //    if (ptMouse.Y + maxY > 525 && mvCtrl)
                    //        margin.Top = margin.Top + 525 - ptMouse.Y - maxY;
                    //    if (ptMouse.Y - minY < 5 && mvCtrl)
                    //        margin.Top = margin.Top + 5 - ptMouse.Y + minY;
                    //    mvCtrl = false;
                    //}

                }
            }
        }
        int indexAltitude = 0;


        //private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    finish = true;
        //}
        //#region Navigation



        //private void Menu_Click(object sender, RoutedEventArgs e)
        //{
        //    this.NavigationService.Navigate(new MenuPage(im, canvas));
        //}

        public void OpenInitialDialogBox()
        {
            DataDialog dataDialog = new DataDialog();
            //dataDialog.Owner = this;
            dataDialog.ShowDialog();



            if (dataDialog.DialogResult == true)
            {

                mainScale = new Echelle(Convert.ToInt32(dataDialog.EchelleTextBox.Text));
                plan = new Plan(Convert.ToInt32(dataDialog.Equidistance), Convert.ToInt32(dataDialog.Min), Convert.ToInt32(dataDialog.Max), mainScale);


            }

        }

       

        public void OpenCourbeinfo()
        {
            Window1 Window1 = new Window1();

            Window1.ShowDialog();
            if (Window1.DialogResult == true)
            {
               

                Altitudes.Add(Convert.ToInt32(Window1.Altitude.Text));
               // StyleCmbToRealStyle(courbeActuelle,Convert.ToInt32(Window1.Type.SelectedIndex));
                indexAltitude++;

            }



        }


        Polyline scalePolyline;

        int scaleLinePointsCount = 0;
        private void scaleButton_Click(object sender, RoutedEventArgs e)
        {
            //Echelle testScale = new Echelle(10, 100);
            
            
            drawingScale = true;
            btn2Clicked = false;
            addLineClicked = false;
            navClicked = false;

            // making the polyline
            scalePolyline = new Polyline();

            scalePolyline.Stroke = Brushes.Indigo;
            scalePolyline.StrokeThickness = 3;
            scalePolyline.FillRule = FillRule.EvenOdd;

            mainCanvas.Children.Add(scalePolyline);











            //double result = testScale.FindDistanceOnField(20);

            //MessageBox.Show(result.ToString());


        }

        
        public void ShowSauvgardeWindow_Click(object sender, RoutedEventArgs e)
        {
             SauvgardePage pg = new SauvgardePage();
            this.Content = pg; 
           /* _mainFrame.Content = new SauvgardePage(); */
        }



    }
    class RectangleName
    {
        public Rectangle Rect { get; set; }
        public string Name { get; set; }
    }
   
   

}
