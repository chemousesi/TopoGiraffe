namespace TopoGiraffe
{
    using OxyPlot;
    using System.Collections.Generic;

    public partial class MainViewModel
    {
        public MainViewModel()
        {


            this.Title = "Profil Topographique";
            int cpt = 0;// Pour parcourir la list des intersections
            int point1 = 1; //Pour cree la liste
            double x, y;

            //  for ()                                     // parcour liste


            //   {                                          // debut de parcour

            x = 40; // afectation des x avec echele
            y = 12; // affectation des altitudes 



            /*************************************First Point + creation ***********************************/
            if (point1 == 1)
            {
                Points = new List<DataPoint>
                {
                    new DataPoint(x, y),
                };
                point1 = 2;
            }


            /************************************* The rest of the points ***********************************/
            //Suivant de x et y dans la liste .
            for (cpt = 0; cpt < 10; cpt++)//Instead of 10 put the number of intersections -2;
            {

                Points.Add(new DataPoint(300, 120));
                Points.Add(new DataPoint(100, 450));
                Points.Add(new DataPoint(500, 200));
                Points.Add(new DataPoint(600, 200));
                Points.Add(new DataPoint(300, 150));
                Points.Add(new DataPoint(300, 220));
                Points.Add(new DataPoint(300, 200));

            }



            // }                                           //Fin parcour liste



        }


        public string Title { get; private set; }

        public IList<DataPoint> Points { get; private set; }
        public object Plot { get; }
    }
}
