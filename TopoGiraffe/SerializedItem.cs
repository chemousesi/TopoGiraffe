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
        public List<List<ArtPoint>> listOfPoints = new List<List<ArtPoint>>();
        public Line segmentIntr = new Line();
        public List<Polyline> courbe = new List<Polyline>();

        

        public SerializedItem( List<List<ArtPoint>> listOfPoints , Line segmentIntr , List <Polyline> courbe)
        {
            this.listOfPoints = listOfPoints;
            this.segmentIntr = segmentIntr;
            this.courbe = courbe;

        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("listOfPoints", listOfPoints);
           // info.AddValue("segmentIntr", segmentIntr);
           // info.AddValue("courbe", courbe);
        }

        protected SerializedItem(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            listOfPoints = (List<List<ArtPoint>>)serializationInfo.GetValue("listOfPoints", typeof(List<List<ArtPoint>>));
           // segmentIntr = (Line)serializationInfo.GetValue("segmentIntr", typeof(Line));
           // courbe = (List<Polyline>)serializationInfo.GetValue("courbe", typeof(List<Polyline>));

        }
    }
}
