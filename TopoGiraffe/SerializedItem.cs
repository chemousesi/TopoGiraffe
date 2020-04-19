using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;

namespace TopoGiraffe
{   [Serializable()]
    public class SerializedItem : ISerializable
    {
        //a modifier l'encapsulation
        public List<List<IntersectionDetail>> listOfPoints = new List<List<IntersectionDetail>>();
       

        

        public SerializedItem( List<List<IntersectionDetail>> listOfPoints)
        {
            this.listOfPoints = listOfPoints;
           

        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("listOfPoints", listOfPoints);
           
        }

        protected SerializedItem(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            listOfPoints = (List<List<IntersectionDetail>>)serializationInfo.GetValue("listOfPoints", typeof(List<List<IntersectionDetail>>));
           // segmentIntr = (Line)serializationInfo.GetValue("segmentIntr", typeof(Line));
           // courbe = (List<Polyline>)serializationInfo.GetValue("courbe", typeof(List<Polyline>));

        }
    }
}
