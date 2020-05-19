using System;
using System.Collections.Generic;
using System.Text;
using TopoSurf.MessageBoxStyle;


namespace TopoGiraffe.Exceptions
{
    class ErreurAltitudeExcpetion : Exception
    {
        public ErreurAltitudeExcpetion(string message)
        {
            new MssgBox(message).ShowDialog();
        }
    }
}
