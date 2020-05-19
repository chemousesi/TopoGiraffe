using System;
using TopoSurf.MessageBoxStyle;


namespace TopoGiraffe.Exceptions
{
    internal class ErreurAltitudeExcpetion : Exception
    {
        public ErreurAltitudeExcpetion(string message)
        {
            new MssgBox(message).ShowDialog();
        }
    }
}
