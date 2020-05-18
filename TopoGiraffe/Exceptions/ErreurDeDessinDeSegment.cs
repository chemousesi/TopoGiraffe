using System;
using TopoSurf.MessageBoxStyle;

namespace TopoGiraffe.Exceptions
{
    class ErreurDeDessinDeSegment : Exception
    {
        public ErreurDeDessinDeSegment(String Message)
        {
            new MssgBox(Message).ShowDialog();
        }
    }
}
