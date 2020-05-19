using System;
using TopoSurf.MessageBoxStyle;

namespace TopoGiraffe.Exceptions
{
    internal class ErreurDeSaisieException : Exception
    {
        public ErreurDeSaisieException(string message)
        {
            new MssgBox(message).ShowDialog();
        }
    }
}
