using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
using System.Windows.Controls.Primitives;

namespace TopoGiraffe
{
    /// <summary>
    /// Logique d'interaction pour MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        //Liste de courbes representer en liste e liste de points (intersections detail)
        List<List<IntersectionDetail>> curves = new List<List<IntersectionDetail>>();


        // declaring variables

        List<Polyline> polylines = new List<Polyline>();
        List<CourbeNiveau> CourbesNiveau = new List<CourbeNiveau>();

        //PointCollection myPointCollection2 = new PointCollection();

        CourbeNiveau courbeActuelle;

        List<Ellipse> cercles = new List<Ellipse>();

        PolyLineSegment polylinesegment = new PolyLineSegment();
        bool btn2Clicked = false; bool addLineClicked = false; bool navClicked = false;
#pragma warning disable CS0414 // Le champ 'MainWindow.finish' est assigné, mais sa valeur n'est jamais utilisée
        bool finish = false;
#pragma warning restore CS0414 // Le champ 'MainWindow.finish' est assigné, mais sa valeur n'est jamais utilisée
        Polyline poly = new Polyline();
        int LinePointscpt = 0;
#pragma warning disable CS0414 // Le champ 'MainWindow.firstPoint' est assigné, mais sa valeur n'est jamais utilisée
        Boolean firstPoint = true;
#pragma warning restore CS0414 // Le champ 'MainWindow.firstPoint' est assigné, mais sa valeur n'est jamais utilisée
        List<int> Altitudes = new List<int>();

        List<List<ArtPoint>> PointsGlobal = new List<List<ArtPoint>>();
#pragma warning disable CS0414 // Le champ 'MainWindow.NbCourbes' est assigné, mais sa valeur n'est jamais utilisée
        int NbCourbes = 0;
#pragma warning restore CS0414 // Le champ 'MainWindow.NbCourbes' est assigné, mais sa valeur n'est jamais utilisée





        Plan plan;
#pragma warning disable CS0169 // Le champ 'MainWindow.scaleLine' n'est jamais utilisé
        Line scaleLine;
#pragma warning restore CS0169 // Le champ 'MainWindow.scaleLine' n'est jamais utilisé
        Boolean drawingScale = false;
        Echelle mainScale;


        public MainPage()
        {






            InitializeComponent();
            this.Title = "TopoGiraffe";
            this.DataContext = this;



            //Point pt = new Point(2, 3);
            //IntersectionDetail intd = new IntersectionDetail(pt, true);
            //this.Serializee(intd);









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
            if (CourbesNiveau.Count == 0)
            {
                MessageBox.Show("Il Faut avoir au moins une courbe");
                activerDessinCheckBox.IsChecked = false;

            }
            else
            {
                courbeActuelle = CourbesNiveau[CourbesNiveau.Count - 1];
            }
        }






        private void border_KeyDown(object sender, KeyEventArgs e)
        {

        }


        // for a live preview of the line 

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            double x = Mouse.GetPosition(mainCanvas).X;
            double y = Mouse.GetPosition(mainCanvas).Y;


            if (btn2Clicked == true && (courbeActuelle.polyline.Points.Count > 0))
            {



                Line newLine = new Line();



                newLine.Stroke = System.Windows.Media.Brushes.Black;
                newLine.X1 = courbeActuelle.polyline.Points[courbeActuelle.polyline.Points.Count - 1].X;
                newLine.Y1 = courbeActuelle.polyline.Points[courbeActuelle.polyline.Points.Count - 1].Y;

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

        CourbeNiveau temporaryFigure;
        private void MouseMoveOnDraw(object sender, MouseEventArgs e)
        {


            if (finalCtrlPoint == false && btn2Clicked == true)
            {

                if (currentCurveCtrlPts.Count != 0) //to handle real-time drawing
                {
                    if (courbeActuelle.polyline.Points.Last().Equals(mousePos))
                    {
                        courbeActuelle.polyline.Points.RemoveAt(courbeActuelle.polyline.Points.Count - 1);
                    }
                    if (CourbesNiveau.Contains(temporaryFigure))
                    {
                        CourbesNiveau.Remove(temporaryFigure);
                    }
                    mousePos = new Point(e.GetPosition(this.mainCanvas).X, e.GetPosition(this.mainCanvas).Y);
                    temporaryFigure = courbeActuelle;
                    courbeActuelle.polyline.Points.Add(mousePos);

                }



                CourbesNiveau.Add(courbeActuelle);

            }




        }
        private void MouseMoveOnAddLine(object sender, MouseEventArgs e)
        {



            if (courbeActuelle.polyline.Points.Count != 0) //to handle real-time drawing
            {
                if (courbeActuelle.polyline.Points.Last().Equals(mousePos))
                {
                    courbeActuelle.polyline.Points.RemoveAt(courbeActuelle.polyline.Points.Count - 1);
                }
                if (CourbesNiveau.Contains(temporaryFigure))
                {
                    CourbesNiveau.Remove(temporaryFigure);
                }
                mousePos = new Point(e.GetPosition(this.mainCanvas).X, e.GetPosition(this.mainCanvas).Y);
                temporaryFigure = courbeActuelle;
                courbeActuelle.polyline.Points.Add(mousePos);
            }



            CourbesNiveau.Add(courbeActuelle);

        }



        private void dessinerButton_Click(object sender, RoutedEventArgs e)
        {
            btn2Clicked = true;
            dragbool = false;
            Cursor = Cursors.Cross;
            CourbeNiveau myCurve = DrawNewCurve();

            CourbesNiveau.Add(myCurve);

            activerDessinCheckBox.IsChecked = true; navClicked = false;


            // styling


            courbeActuelle = myCurve;
            AltSlider.Value = Convert.ToInt32(AltitudeString);

            courbeActuelle.polyline.Stroke = new SolidColorBrush(AltitudeToColor(Convert.ToInt32(AltitudeString)));


            //myPolyline.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString((colorComboBox.SelectedItem as RectangleName).Name);
            //myPolyline = StyleCmbToRealStyle(myPolyline, Window1.SelectedIndex);


            //myPolyline.StrokeThickness = 2;
            //myPolyline.FillRule = FillRule.EvenOdd;
            //StyleCmbToRealStyle(myPolyline, styleCourbeCmb.SelectedIndex);

            myCurve.polyline.MouseMove += new System.Windows.Input.MouseEventHandler(Path_MouseMove);
            myCurve.polyline.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(Path_MouseLeftButtonUp);
            myCurve.polyline.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Path_MouseLeftButtonDown);


            // (courbeActuelle).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Polyline_MouseDown);

            mainCanvas.Children.Add(courbeActuelle.polyline);
            PointsGlobal.Add(new List<ArtPoint>());
            currentCurveCtrlPts = PointsGlobal[PointsGlobal.Count - 1];
            indexPoints++;
            finalCtrlPoint = false;











        }




        private void mainCanvas_MouseEnter(object sender, MouseEventArgs e)
        {

        }




        // to eliminate placeholders





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
                foreach (List<ArtPoint> ae in PointsGlobal)
                {
                    ae.Clear();
                }
                PointsGlobal.Clear();
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
            //else
            //{
            //    if (courbeActuelle.Points.Count > 0)
            //    {
            //        courbeActuelle.Points.Remove(courbeActuelle.Points[courbeActuelle.Points.Count - 1]);
            //        foreach (Ellipse cercle in cercles)
            //        {
            //            mainCanvas.Children.Remove(cercle);

            //        }
            //        cercles.Clear();
            //        IntersectionPoints.Clear();
            //    }
            //    else
            //    {
            //        polylines.Remove(polylines[polylines.Count - 1]);

            //        if (polylines.Count > 0)
            //        {
            //            //courbeActuelle = polylines[polylines.Count - 1];

            //        }
            //        else
            //        {
            //            MessageBox.Show("no polygone to delete");
            //        }

            //    }
            //}
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


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
           
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



                    if (courbeActuelle.polyline.Points.Count > 2)
                    {
                        Line l = new Line();
                        l.X1 = courbeActuelle.polyline.Points[courbeActuelle.polyline.Points.Count - 2].X;
                        l.Y1 = courbeActuelle.polyline.Points[courbeActuelle.polyline.Points.Count - 2].Y;
                        l.X2 = lastPoint.X;
                        l.Y2 = lastPoint.Y;

                        inter = FindIntersection1(courbeActuelle.polyline, l);


                        if (inter == true)
                        {

                            new MssgBox("       Erreur de Dessin de la Courbe !\n veuillez dessiner votre segment à nouveau ").ShowDialog();

                        }

                    }
                    if (inter == false)
                    {
                        courbeActuelle.polyline.Points.Add(lastPoint);
                        Ellipse circle = new Ellipse();
                        ArtPoint artPoint = new ArtPoint(circle, lastPoint);
                        PointsGlobal[indexPoints].Add(artPoint);
                        circle.Width = 10;
                        circle.Height = 10;
                        circle.Fill = Brushes.Purple;
                        circle.MouseMove += new System.Windows.Input.MouseEventHandler(Cercle_Mousemove);
                        circle.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(Ellipse_MouseLeftButtonUp);
                        circle.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Ellipse_MouseLeftButtonDown);

                        Canvas.SetLeft(circle, lastPoint.X - (circle.Width / 2));
                        Canvas.SetTop(circle, lastPoint.Y - (circle.Height / 2));
                        mainCanvas.Children.Add(circle);


                        PointsGlobal[indexPoints].Add(artPoint);
                    }

                }
                else
                // when click on the last (first) point 
                {
                    courbeActuelle.polyline.Points.RemoveAt(courbeActuelle.polyline.Points.Count - 1);
                    courbeActuelle.polyline.Points.Add(courbeActuelle.polyline.Points[0]);
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

                    foreach (CourbeNiveau courbe in CourbesNiveau)
                    {
                        FindIntersection(courbe, line);
                    }

                    curves.Add(IntersectionPoints);



                    MessageBox.Show("la taille de curves lakhra " + curves[curves.Count() - 1].Count());
                    for (int a = 0; a < (curves[curves.Count() - 1].Count()); a++) { MessageBox.Show(curves[curves.Count() - 1][a].altitude.ToString()); }
                    this.Serializee(curves);

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

                    IntersectionPoints = IntersectionPoints.OrderBy(o => o.distance).ToList();
                    int AltitudeFinale = IntersectionPoints[IntersectionPoints.Count - 1].altitude;
                    int AltitudeIni = IntersectionPoints[0].altitude;




                    if (scaleLinePointsCount == 2)
                    {
                        MessageBox.Show(" Distance :" + Math.Round(mainScale.FindDistanceOnField(Outils.DistanceBtwTwoPoints(poly.Points[0], poly.Points[1])), 2) + " mètres");

                    }


                    addLineClicked = false;
                    double distance1 = Outils.DistanceBtwTwoPoints(new Point(line.X1, line.Y1), new Point(line.X2, line.Y2));// this can be optimized by using line.x, line.y
                    IntersectionPoints.Add(new IntersectionDetail(new Point(line.X2, line.Y2), AltitudeFinale, distance1));
                    IntersectionPoints.Add(new IntersectionDetail(new Point(line.X1, line.Y1), AltitudeIni, 0));
                    IntersectionPoints = IntersectionPoints.OrderBy(o => o.distance).ToList();

                }

            }
            else if (drawingScale == true)

            {

                scaleLinePointsCount++;
                scalePolyline.Points.Add(new Point(x, y));

                if (scaleLinePointsCount == 2)
                {
                    mainScale.scaleDistanceOnCanvas = Outils.DistanceBtwTwoPoints(scalePolyline.Points[0], scalePolyline.Points[1]);

                    string message = "Echelle sur plan " + Math.Round(mainScale.scaleDistanceOnCanvas, 3) + "------>" + mainScale.scaleDistanceOnField + " mètres";
                    MessageBox.Show(message);
                    mainCanvas.Children.Remove(scalePolyline);
                    drawingScale = false;
                }


            }
            else if (drawPointsClicked == true)
            {

                drawPointsClicked = false;


                if (pointAltitudeActuel != null)
                {
                    pointAltitudeActuel.point = new Point(x, y);
                    pointAltitudeActuel.DrawShape(mainCanvas);
                    pointsAltitude.Add(pointAltitudeActuel);

                }




            }

        }
        List<object> Skew = new List<object>();
        List<PointAltitude> pointsAltitude = new List<PointAltitude>();


        List<IntersectionDetail> IntersectionPoints = new List<IntersectionDetail>();

        // code d'intersection -------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------


        Line line = new Line();
        public void FindIntersection(CourbeNiveau p, Line line)
        {
            Line myLine = new Line();
            IntersectionDetail inter;

            for (int i = 0; i < p.polyline.Points.Count - 1; i++)
            {
                myLine.X1 = p.polyline.Points[i].X; myLine.Y1 = p.polyline.Points[i].Y;
                myLine.X2 = p.polyline.Points[i + 1].X; myLine.Y2 = p.polyline.Points[i + 1].Y;
                inter = intersectLines(myLine, line);

                inter.altitude = Convert.ToInt32(p.altitude);

                if (inter.intersect == true)
                {

                    IntersectionPoints.Add(inter);
                    PenteIntersectionPoints.Add(inter);

                }


            }


            distances();

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
                    inters = inter.intersect;
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

        //Calcule des distances
        List<double> distancesListe = new List<double>();

        public void distances()
        {

            foreach (IntersectionDetail intersectionDetail in IntersectionPoints)
            {
                double x1 = line.X1;//IntersectionPoints[1].point.X;
                double y1 = line.Y1;//IntersectionPoints[1].point.Y;

                double distance = Outils.DistanceBtwTwoPoints(new Point(x1, y1), intersectionDetail.point);// this can be optimized by using line.x, line.y


                //distancesListe.Add(distance);
                intersectionDetail.distance = distance;
            }
            //IntersectionPoints.Sort();
            IntersectionPoints = IntersectionPoints.OrderBy(o => o.distance).ToList();

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
            CourbeNiveau polyC = new CourbeNiveau(poly, -1);
            CourbesNiveau.Add(polyC);
            courbeActuelle = polyC;
            mainCanvas.Children.Add(poly);

        }





        public Polyline StyleCmbToRealStyle(Polyline pol, int index)
        {
            if (pol != null)
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
            return pol;

        }




        private void mainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                MessageBox.Show("back key pressed");
            }
        }








        private List<object> hitResultsList = new List<object>();



        private void mainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {


            if (btn2Clicked == false) return;
            else
            {
                btn2Clicked = false;


                if (courbeActuelle.polyline != null)
                {
                    courbeActuelle.polyline.Points.RemoveAt(courbeActuelle.polyline.Points.Count - 1);
                }
                else
                {
                    MessageBox.Show("vous n'avez pas de courbe");
                }



                if (CourbesNiveau.Contains(courbeActuelle) == false)
                {
                    CourbesNiveau.Add(courbeActuelle);
                }
                if (polylines.Count != 0)
                {
                    //int indexp = polylines.IndexOf(courbeActuelle);

                }
                //curve is a list of points
                List<IntersectionDetail> curve = new List<IntersectionDetail>();
                for (int k = 0; k < courbeActuelle.polyline.Points.Count(); k++) { curve.Add(new IntersectionDetail(courbeActuelle.polyline.Points[k], false)); }
                curves.Add(curve);
            }
        }


        private void nav_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Arrow;
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



        CourbeNiveau Interpoly;
        CourbeNiveau EditPolyline;

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


#pragma warning disable CS0168 // La variable 'ellipse' est déclarée, mais jamais utilisée
                    Ellipse ellipse;
#pragma warning restore CS0168 // La variable 'ellipse' est déclarée, mais jamais utilisée
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
                    EditPolyline = CourbesNiveau[index2];
                    //courbeActuelle = CourbesNiveau[index2]; 
                    //Polyline_Modify();


                    Interpoly = new CourbeNiveau(new Polyline(), courbeActuelle.altitude);
                    Interpoly.polyline.Stroke = new SolidColorBrush(AltitudeToColor(courbeActuelle.altitude));

                    StyleCmbToRealStyle(Interpoly.polyline, styleCourbeCmb.SelectedIndex);
                    Interpoly.polyline.StrokeThickness = ThickSlider.Value;
                    Interpoly.polyline.Points.Clear();
                    //Interpoly.FillRule = FillRule.EvenOdd;
                    //courbeActuelle = Interpoly;

                    //   mainCanvas.Children.Remove(EditPolyline);


                    int i = 0;
                    PointsGlobal[index2].Remove(elDraggingEllip);

                    foreach (Point point in EditPolyline.polyline.Points)
                    {
                        if (point.Equals(elDraggingEllip.P))
                        {
                            Interpoly.polyline.Points.Add(ptMouse);
                            elDraggingEllip = new ArtPoint((Ellipse)elDraggingEllipse, ptMouse);

                            PointsGlobal[index2].Add(elDraggingEllip);
                        }
                        else
                        {
                            if (dragbool == true && i == EditPolyline.polyline.Points.Count - 1)
                            {
                                Interpoly.polyline.Points.Add(EditPolyline.polyline.Points[0]);

                            }
                            else
                            {
                                Interpoly.polyline.Points.Add(EditPolyline.polyline.Points[i]);
                            }
                        }
                        i++;
                    }



                    //mainCanvas.Children.Remove(courbeActuelle);
                    mainCanvas.Children.Add(Interpoly.polyline);
                    mainCanvas.Children.Remove(EditPolyline.polyline);

                    EditPolyline = Interpoly;
                    CourbesNiveau[index2] = Interpoly;







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
#pragma warning disable CS0219 // La variable 'i1' est assignée, mais sa valeur n'est jamais utilisée
            int i1 = 0;
#pragma warning restore CS0219 // La variable 'i1' est assignée, mais sa valeur n'est jamais utilisée
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
                courbeActuelle.polyline.Points.RemoveAt(courbeActuelle.polyline.Points.Count - 1);
                courbeActuelle.polyline.Points.Add(courbeActuelle.polyline.Points[0]);
                btn2Clicked = false;
                dragbool = true;
                btn2Clicked = false;
                //curve is a list of points
                List<IntersectionDetail> curve = new List<IntersectionDetail>();
                for (int k = 0; k < courbeActuelle.polyline.Points.Count(); k++) { curve.Add(new IntersectionDetail(courbeActuelle.polyline.Points[k], false)); }
                curves.Add(curve);



            }
            else if (navClicked == true)
            {
                int cpt4 = 0;
                foreach (List<ArtPoint> ae in PointsGlobal)
                {

                    foreach (ArtPoint a in ae)
                    {
                        if (a.cercle.Equals(elDraggingEllipse))
                        {
                            courbeActuelle = CourbesNiveau[cpt4];
                            ThickSlider.Value = courbeActuelle.polyline.StrokeThickness;
                        }
                    }

                    cpt4++;
                }
            }
            Ellipse elli = (Ellipse)elDraggingEllipse;
            elli.Fill = Brushes.Orange;
            AltSlider.Value = courbeActuelle.altitude;



        }



        bool finalCtrlPoint = false;
        List<ArtPoint> currentCurveCtrlPts;
        Point mousePos;


        //------------------------------------------------------------------------------------------------------------------------------------------------
        // code to handle dragging of the poyline --------------------------------------------------------------------------------------------------------
        bool isDragging, mvCtrl = true;
        FrameworkElement elDragging, selectedPath, selectedPolyline, selectedTriangle;
        double minX, minY, maxX, maxY;
        int indexdrag = 0;


        private void btn13_Click(object sender, RoutedEventArgs e)
        {

            if (mainScale != null)
            {
                String penteText = " la pente est de   :" + pente.ToString() + " % ";
                ProfileTopographique profile = new ProfileTopographique(IntersectionPoints, distancesListe, mainScale, penteText);
                profile.Show();

            }
            else
            {
                MessageBox.Show("Echelle non connue !");
            }





        }

        List<ArtPoint> DragPoints;
        Thickness margin;


        void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedPolyline = (this).InputHitTest(e.GetPosition(this)) as FrameworkElement;
            if (selectedPolyline == null) return;
            if (selectedPolyline != null && selectedPolyline is Polyline)
            {
                foreach (CourbeNiveau courbe in CourbesNiveau)
                {

                    if (courbe.polyline.Equals((Polyline)selectedPolyline))
                    {
                        courbeActuelle = courbe;

                    }


                }

                if (navClicked == true)
                {


                    AltSlider.Value = courbeActuelle.altitude;
                    ThickSlider.Value = courbeActuelle.polyline.StrokeThickness;
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

                    }
                    //foreach (Polyline polyline in polylines)
                    //{
                    //    if (elDragging == polyline)
                    //    {
                    //        DragPoints = PointsGlobal[CourbesNiveau.IndexOf(polyline)];
                    //    }
                    //}

                }
                else return;

            }
        }



#pragma warning disable CS0414 // Le champ 'MainWindow.test' est assigné, mais sa valeur n'est jamais utilisée
        bool test = false;
#pragma warning restore CS0414 // Le champ 'MainWindow.test' est assigné, mais sa valeur n'est jamais utilisée
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




            }
            isDragging = false;
            (elDragging).Cursor = Cursors.Arrow;
            (elDragging).ReleaseMouseCapture();
            elDragging = null;
            test = true;
        }

        SolidColorBrush chosenBrush;
        long colorVal;
        private void cp_SelectedColorChanged_1(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (cp.SelectedColor.HasValue)
            {
                Color C = cp.SelectedColor.Value;
                byte Red = C.R;
                byte Green = C.G;
                byte Blue = C.B;
                colorVal = Convert.ToInt64(Blue * (Math.Pow(256, 0)) + Green * (Math.Pow(256, 1)) + Red * (Math.Pow(256, 2)));
                chosenBrush = new SolidColorBrush(Color.FromRgb(Red, Green, Blue));
                // update courbe actulle 
                if (courbeActuelle != null)
                {
                    courbeActuelle.polyline.Stroke = chosenBrush;
                }
                else
                {
                    MessageBox.Show("Créer votre courbe d'abord");
                }

            }

        }

        private void styleCourbeCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (courbeActuelle != null)
            {

                courbeActuelle.polyline = StyleCmbToRealStyle(courbeActuelle.polyline, styleCourbeCmb.SelectedIndex);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

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


                }
            }
        }




        public void DrawCtrlPoints(Polyline polyline)
        {
            if (polyline == null) return;

            int index = polylines.IndexOf(polyline);

            foreach (ArtPoint Ctrl in PointsGlobal[index])
            {




            }

        }
        public void RemoveCtrlPoints(Polyline polyline)
        {
            if (polyline == null) return;

            int index = polylines.IndexOf(polyline);

            foreach (ArtPoint Ctrl in PointsGlobal[index])
            {

                mainCanvas.Children.Remove(Ctrl.cercle);


            }




        }

        int indexAltitude = 0;
        int Equidistance;
        static float AltitudeMin;
        static float AltitudeMax;




        public void OpenInitialDialogBox()
        {
            DataDialog dataDialog = new DataDialog();
            //dataDialog.Owner = this;
            dataDialog.ShowDialog();



            if (dataDialog.DialogResult == true)
            {


                AltSlider.Minimum = Convert.ToInt32(dataDialog.MinTextBox.Text);
                AltitudeMin = Convert.ToInt32(dataDialog.MinTextBox.Text);
                AltitudeMax = Convert.ToInt32(dataDialog.MaxTextBox.Text);
                AltSlider.Maximum = Convert.ToInt32(dataDialog.MaxTextBox.Text);
                ThickSlider.Value = 2;
                Equidistance = Convert.ToInt32(dataDialog.EquidistanceTextBox.Text);
                if (int.TryParse(dataDialog.EchelleOnCanvas, out int result1) && int.TryParse(dataDialog.EchelleOnField, out int result2))
                {
                    mainScale = new Echelle(result1, result2);
                    plan = new Plan(Convert.ToInt32(dataDialog.Equidistance), Convert.ToInt32(dataDialog.Min), Convert.ToInt32(dataDialog.Max), mainScale);


                }
                else
                {
                    MessageBox.Show("Erreur !\n Entrée non valide, le plan n'est pas créé");
                }








            }

        }

        String AltitudeString;

        public CourbeNiveau DrawNewCurve()
        // this function generates a dialog box , create , style a polyline from that dialog box entries
        // and return a new polyline, :)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();

            CourbeNiveau Courbe = null;

            if (window1.DialogResult == true)
            {
                // taking the altitude from the dialog box

                if (int.TryParse(window1.Altitude.Text, out int result))
                {
                    Altitudes.Add(result);
                    indexAltitude++;




                    AltitudeString = window1.Altitude.Text;

                    // StyleCmbToRealStyle(courbeActuelle,Convert.ToInt32(Window1.Type.SelectedIndex));
                    Courbe = new CourbeNiveau(new Polyline(), result);
                    //colorComboBox.SelectedIndex = window1.colorComboBox.SelectedIndex;
                    //newPolyline.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString((colorComboBox.SelectedItem as RectangleName).Name);
                    //newPolyline.Stroke = System.Windows.Media.Brushes.Black;
                    Courbe.polyline.StrokeThickness = 2;
                    Courbe.polyline.FillRule = FillRule.EvenOdd;

                    Courbe.polyline = StyleCmbToRealStyle(Courbe.polyline, window1.styleCourbeCmbInDialogBox.SelectedIndex); // styling it 

                }
                else
                {
                    MessageBox.Show("Erreur! \n votre altitude n'est pas un entier");
                }

            }

            return Courbe;
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

        private void altitudeSliderValueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {//change altitude
            changeSelectedCurveAltitude((float)AltSlider.Value);
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (courbeActuelle == null)
            {
                return;
            }
            AltSlider.Value += Equidistance;
            changeSelectedCurveAltitude((float)AltSlider.Value);

            //Color color = Color.AliceBlue;
            //Console.Write(color.);
        }
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {

            if (courbeActuelle == null)
            {
                return;
            }
            AltSlider.Value -= Equidistance;
            changeSelectedCurveAltitude((float)AltSlider.Value);
        }


        private void changeSelectedCurveAltitude(float altit)
        {//the real change
            if (courbeActuelle == null)
            {
                return;
            }


            (courbeActuelle).polyline.Stroke = new SolidColorBrush(AltitudeToColor(altit));
            courbeActuelle.altitude = altit;

        }
        private void keyDownOnAltitude(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    float altit = int.Parse(AltitudeBox.Text);
                    changeSelectedCurveAltitude(altit);
                }
                catch (System.FormatException)
                {
                    (new MssgBox("Enter a numeric value to the altitude !")).ShowDialog();
                }
                catch (System.NullReferenceException)
                {
                }
            }
        }

        public static Color AltitudeToColor(float altit)
        {//implemeting the predefined altitude levels

            float range = (AltitudeMax - AltitudeMin) / 10;
            range = Convert.ToInt32(range);
            Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");

            if (altit < 0) return Colors.Black;
            if (altit <= AltitudeMin + range) return (Color)ColorConverter.ConvertFromString("#6600CC");
            if (altit <= AltitudeMin + 2 * range) return (Color)ColorConverter.ConvertFromString("#0000CC");
            if (altit <= AltitudeMin + 3 * range) return (Color)ColorConverter.ConvertFromString("#0066CC");
            if (altit <= AltitudeMin + 4 * range) return (Color)ColorConverter.ConvertFromString("#00CCCC");
            if (altit <= AltitudeMin + 5 * range) return (Color)ColorConverter.ConvertFromString("#00CC66");
            if (altit <= AltitudeMin + 6 * range) return (Color)ColorConverter.ConvertFromString("#00CC00");
            if (altit <= AltitudeMin + 7 * range) return (Color)ColorConverter.ConvertFromString("#66CC00");
            if (altit <= AltitudeMin + 8 * range) return (Color)ColorConverter.ConvertFromString("#FFFF00");
            if (altit <= AltitudeMin + 9 * range) return (Color)ColorConverter.ConvertFromString("#FF8000");
            if (altit <= AltitudeMin + 10 * range) return (Color)ColorConverter.ConvertFromString("#FF0000");
            return Colors.Black;




        }
        /* --------------------------------------------------------------------------- aide contextuel ------------------------------------------------*/
        /* -------------------------------------------------------------------------------------------------------------------------------------------*/

        private void Profil_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = btn13;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Générer le profile";
        }

        private void Profil_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Parametres_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = btn14;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Paramètres";
        }

        private void Parametres_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void DessinDeCourbes_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = dessinerButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Dessin de courbes";
        }

        private void DessinDeCourbes_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void DessinDeSegment_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = add_line;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Dessin de segments ";
        }

        private void DessinDeSegment_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void SupprimerTout_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = deleteAllButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Supprimer tout";
        }

        private void SupprimerTout_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void SupprimerPrécedent_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = deletePreviousButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Supprimer précedent";
        }

        private void SupprimerPrécedent_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Import_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = import;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Importer une carte";
        }

        private void Import_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Zoom_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = btn1;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Zoom";
        }

        private void Zoom_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void display_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = display;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "afficher";
        }

        private void display_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Epaisseur_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = ThickSlider;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Modifier l'épaisseur de la courde";
        }

        private void Epaisseur_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Altitude_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = AltSlider;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Modifier l'altitude de la courde";
        }

        private void Altitude_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void ColorPicker_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = cp;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Choisir la couleur de la courde";
        }

        private void ColorPicker_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void TypeCourbe_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = styleCourbeCmb;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Choisir le type de la courde";
        }

        private void TypeCourbe_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Pen_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = btn11;
            popup_uc.Placement = PlacementMode.Bottom;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Pen";
        }

        private void Pen_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        // --------------------------------------------------------------------------- popup fin -------------------------------------//
        // --------------------------------------------------------------------------------------------------------------------------//

        private void ThickSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {//change thickness
            if (courbeActuelle != null)
            {
                (courbeActuelle).polyline.StrokeThickness = ThickSlider.Value;
                ThickText.Text = ThickSlider.Value.ToString();
            }
        }


        public void ShowSauvgardeWindow_Click(object sender, RoutedEventArgs e)
        {
            //SauvgardePage pg = new SauvgardePage();

            //this.Content = pg;
            this.NavigationService.Navigate(new SauvgardePage());

            /* _mainFrame.Content = new SauvgardePage(); */
        }


        public void Button_Click(object sender, RoutedEventArgs e)
        {

            List<Polyline> curve = polylines;

            List<List<IntersectionDetail>> itm2 = new List<List<IntersectionDetail>>();
            List<IntersectionDetail> alts = new List<IntersectionDetail>();




            itm2 = this.DeSerialize();

            alts = itm2[itm2.Count() - 1];

            alts = alts.GetRange(0, itm2.Count() - 1);
            alts.Reverse();
            int h = 0;
            for (int v = 0; v < alts.Count(); v++) { MessageBox.Show("wanted message is " + alts[v].altitude.ToString()); }
            for (int i = 0; i < itm2.Count(); i++)
            {

                Polyline li = new Polyline();
                mainCanvas.Children.Add(li);

                for (int j = 0; j < itm2[i].Count(); j++)
                {

                    li.FillRule = FillRule.EvenOdd;
                    li.StrokeThickness = 4;
                    if (i < itm2.Count() - 1)
                    {
                        li.Stroke = new SolidColorBrush(AltitudeToColor(alts[i].altitude));
                    }
                    else
                    {
                        li.Stroke = Brushes.Black;
                    }

                    li.Visibility = System.Windows.Visibility.Visible;
                    Ellipse circle = new Ellipse();
                    circle.Width = 10;
                    circle.Height = 10;
                    if (i == itm2.Count() - 1)
                    {

                        circle.Fill = Brushes.Red;

                    }
                    else
                    {


                        circle.Fill = Brushes.Purple;
                    }



                    Canvas.SetLeft(circle, itm2[i][j].point.X - (circle.Width / 2));
                    Canvas.SetTop(circle, itm2[i][j].point.Y - (circle.Height / 2));
                    Point ps = new Point(itm2[i][j].point.X, itm2[i][j].point.Y);
                    li.Points.Add(ps);
                    mainCanvas.Children.Add(circle);

                }
                li.FillRule = FillRule.EvenOdd;
                li.Visibility = System.Windows.Visibility.Visible;
                h++;
                //if (h == alts.Count())
                //{
                //    li.Stroke = Brushes.Purple;
                //}
                //else
                //{
                //    li.Stroke = new SolidColorBrush(AltitudeToColor(alts[h].altitude));
                //}
                //h++;
                //MessageBox.Show("point par point" + itm2[itm2.Count()-1][h].altitude.ToString());




                if (i == (itm2.Count() - 1))
                {

                    li.Stroke = Brushes.Purple;
                    li.StrokeThickness = 7;


                }
                else
                {


                    li.StrokeThickness = 2;
                }



            }





        }

        //serialization

        public void Serializee(List<List<IntersectionDetail>> objet)
        {

            OpenFileDialog op = new OpenFileDialog();

            Stream s = File.Open("autosave.topo", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, objet);
            s.Close();
        }
        public void Serializee(List<List<IntersectionDetail>> objet, string filename)
        {

            OpenFileDialog op = new OpenFileDialog();

            Stream s = File.Open(path: filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, objet);
            s.Close();
        }

        public List<List<IntersectionDetail>> DeSerialize()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a ";
            op.FileName = "Document"; // Default file name
            op.DefaultExt = ".topo"; // Default file extension
            op.Filter = "Text documents (.topo)|*.topo"; // Filter files by extension
            if (op.ShowDialog() == true)
            {
                string fiName = op.FileName;

            }
            Stream s = File.Open(path: op.FileName, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            List<List<IntersectionDetail>> objet = (List<List<IntersectionDetail>>)bf.Deserialize(s);
            s.Close();


            return objet;
        }


        double pente;
        List<IntersectionDetail> PenteIntersectionPoints = new List<IntersectionDetail>();


        private void Pente_Click(object sender, RoutedEventArgs e)
        {

            //this.DeSerialize()[this.DeSerialize().Count()-1];

            Echelle echel = new Echelle(200, 200);


            pente = CalcPente(PenteIntersectionPoints, mainScale);
            MessageBox.Show(" la pente est de   :" + pente.ToString() + " % ");
        }

        //methode de calcul de pente
        public double CalcPente(List<IntersectionDetail> points, Echelle sc)
        {
            double sum = 0;

            Line l = new Line();

            for (int i = 0; i < points.Count() - 1; i++)
            {

                l.X1 = points[i + 1].point.X; l.Y1 = points[i + 1].point.Y;
                l.X2 = points[i].point.X; l.Y2 = points[i].point.Y;
                sum += ((points[i + 1].altitude - points[i].altitude) * 100 / sc.FindDistanceOnField(l));
                // MessageBox.Show( "altitudes are " + points[i + 1].altitude.ToString() + " " + points[i].altitude.ToString() );
            }
            return (sum / (points.Count() - 1));
        }




        PointAltitude pointAltitudeActuel = null;


        public void MakeNewPoint()
        // this method creates a point and assigns it to PointAltitudeActuel
        {
            PointAltBox pointAltBox = new PointAltBox();
            pointAltBox.ShowDialog();


            if (pointAltBox.DialogResult == true)
            {
                if (int.TryParse(pointAltBox.Altitude.Text, out int result))

                {
                    pointAltitudeActuel = new PointAltitude(result, pointAltBox.typePointCmb.SelectedIndex);
                    

                    pointAltitudeActuel.triangle.MouseLeftButtonDown += new MouseButtonEventHandler(Control_MouseLeftButtonDown2);
                    pointAltitudeActuel.triangle.MouseLeftButtonUp += new MouseButtonEventHandler(Control_MouseLeftButtonUp2);
                    pointAltitudeActuel.triangle.MouseMove += new MouseEventHandler(Control_MouseMove2);


                }
                else
                {
                    MessageBox.Show("Altitude Fausse !");
                }

            }


        }

        Boolean drawPointsClicked = false;



        private void drawPoint_Clicked(object sender, RoutedEventArgs e)
        // this method is when the trianngle button is clicked, it puts all other options to false, except the one for drawing the point
        {


            drawPointsClicked = true;
            addLineClicked = false;
            btn2Clicked = false;
            drawingScale = false;

            MakeNewPoint();


        }
      

        private void Control_MouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            selectedTriangle = (this).InputHitTest(e.GetPosition(this)) as FrameworkElement;
            if (selectedTriangle == null) return;
            if (selectedTriangle != null && selectedTriangle is Polygon)
            {
                
                if (navClicked == true)
                {


                    foreach (PointAltitude pointAltitude in pointsAltitude)
                    {

                        if (pointAltitude.triangle.Equals((Polyline)selectedPolyline))
                        {
                            pointAltitudeActuel = pointAltitude ;
                            mainCanvas.Children.Remove(pointAltitudeActuel.altitudeTextBlock);

                        }


                    }







                    //AltSlider.Value = courbeActuelle.altitude;
                    //ThickSlider.Value = courbeActuelle.polyline.StrokeThickness;
                    mvCtrl = true;
                    ptMouseStart = e.GetPosition(this);
                    elDragging = (this).InputHitTest(ptMouseStart) as FrameworkElement;
                    if (elDragging == null) return;
                    if (elDragging != null && elDragging is Polygon)
                    {
                        ptElementStart = new Point(elDragging.Margin.Left, elDragging.Margin.Top);
                        margin = new Thickness(elDragging.Margin.Left, elDragging.Margin.Top, 0, 0);
                        elDragging.Cursor = Cursors.ScrollAll;
                        Mouse.Capture((elDragging));
                        isDragging = true;

                    }
                    

                }
                else return;

            }
        }

        private void Control_MouseLeftButtonUp2(object sender, MouseButtonEventArgs e)
        {
            Point pnt = e.GetPosition(this);
            if (elDragging == null) return;

                   

            if (isDragging)
            {
                if (!mvCtrl)
                {
                    elDragging.Margin = margin;

                }

            }
            isDragging = false;
            (elDragging).Cursor = Cursors.Arrow;
            (elDragging).ReleaseMouseCapture();
            elDragging = null;
            test = true;
        }

        private void Control_MouseMove2(object sender, MouseEventArgs e)
        {
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


                    elDragging.Margin = new Thickness(left, top, 0, 0);// modify the margin to move the curve
                    if (mvCtrl)
                    {
                        margin = elDragging.Margin;
                    }


                }
            }
        }































    }
    class RectangleName
    {
        public Rectangle Rect { get; set; }
        public string Name { get; set; }
    }
}
