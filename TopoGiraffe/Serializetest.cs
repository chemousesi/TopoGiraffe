using System;
using System.Collections.Generic;
using System.Text;
using TopoGiraffe.Noyau;
using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using Point = System.Windows.Point;

namespace TopoGiraffe
{
    public class Serializetest
    {
        public  void SerializeNow(Point point, bool intersect)
        {           
            IntersectionDetail intd = new IntersectionDetail(point, intersect);
            Stream s = File.Open("test.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, intd);  
            s.Close();

        }
        public  void DeSerializeNow(Point point, bool intersect)
        {
            
            IntersectionDetail intd = new IntersectionDetail(point, intersect); 
            Stream s = File.Open("test.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            
            
            intd = (IntersectionDetail)bf.Deserialize(s);
            s.Close();
           
            
        }

        
    }
}
 
