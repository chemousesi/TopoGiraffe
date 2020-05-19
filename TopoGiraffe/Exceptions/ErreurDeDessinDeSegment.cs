using System;
using TopoSurf.MessageBoxStyle;

namespace TopoGiraffe.Exceptions
{
    internal class ErreurDeDessinDeSegment : Exception
    {
        public ErreurDeDessinDeSegment(String Message)
        {
            new MssgBox(Message).ShowDialog();
        }
    }
}
