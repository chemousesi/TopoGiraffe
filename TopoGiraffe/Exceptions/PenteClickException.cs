using System;
using TopoSurf.MessageBoxStyle;

namespace TopoGiraffe.Exceptions
{
    internal class PenteClickException : Exception
    {
        public PenteClickException(String Message)
        {
            new MssgBox(Message).ShowDialog();
        }
    }
}
