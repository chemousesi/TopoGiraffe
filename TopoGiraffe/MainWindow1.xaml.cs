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
    public partial class MainWindow : Window
    {
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
            SauvgardePage pg = new SauvgardePage();

            this.Content = pg;
            /* _mainFrame.Content = new SauvgardePage(); */
        }


        public void Button_Click(object sender, RoutedEventArgs e)
        {

            List<Polyline> curve = polylines;

            List<List<IntersectionDetail>> itm2 = new List<List<IntersectionDetail>>();





            itm2 = this.DeSerialize();


            for (int i = 0; i < itm2.Count(); i++)
            {
                Polyline li = new Polyline();
                mainCanvas.Children.Add(li);


                for (int j = 0; j < itm2[i].Count(); j++)
                {

                    li.FillRule = FillRule.EvenOdd;
                    li.StrokeThickness = 4;
                    li.Stroke = Brushes.Black;
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

                if (i == (itm2.Count() - 1))
                {

                    li.Stroke = Brushes.Purple;
                    li.StrokeThickness = 7;


                }
                else
                {

                    li.Stroke = Brushes.Black;
                    li.StrokeThickness = 2;
                }



            }


            //mainCanvas.Children.Add(circle);


        }

        //serialization

        public void Serializee(List<List<IntersectionDetail>> objet)
        {
            OpenFileDialog op = new OpenFileDialog();

            Stream s = File.Open("test.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, objet);
            s.Close();
        }

        public List<List<IntersectionDetail>> DeSerialize()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a ";
            op.FileName = "Document"; // Default file name
            op.DefaultExt = ".dat"; // Default file extension
            op.Filter = "Text documents (.dat)|*.dat"; // Filter files by extension
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
        List<IntersectionDetail> PenteIntersectionPoints = new List<IntersectionDetail>();
        double pente;
        private void Pente_Click(object sender, RoutedEventArgs e)
        {
            Echelle echel = new Echelle(200, 200);

            pente = CalcPente(PenteIntersectionPoints, mainScale);
            MessageBox.Show(" la pente est de   :" + pente.ToString() + " % ");
        }


        public double CalcPente(List<IntersectionDetail> points, Echelle sc)
        {
            double sum = 0;

            Line l = new Line();
            //MessageBox.Show(points.Count().ToString());
            for (int i = 0; i < points.Count() - 1; i++)
            {

                l.X1 = points[i + 1].point.X; l.Y1 = points[i + 1].point.Y;
                l.X2 = points[i].point.X; l.Y2 = points[i].point.Y;
                sum += ((points[i + 1].altitude - points[i].altitude) * 100 / sc.FindDistanceOnField(l));
            }
            return (sum / (points.Count() - 1));
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
    }

}
