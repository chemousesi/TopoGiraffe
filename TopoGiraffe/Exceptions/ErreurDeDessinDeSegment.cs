using System;
using System.Collections.Generic;
using System.Text;
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
