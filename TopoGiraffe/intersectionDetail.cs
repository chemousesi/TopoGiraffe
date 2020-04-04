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
     public class IntersectionDetail
    {
        public Point point;
        public Boolean intersect;

        public IntersectionDetail(Point point , Boolean intersect)
        {
            this.point = point;
            this.intersect = intersect;
        }

    }
}
