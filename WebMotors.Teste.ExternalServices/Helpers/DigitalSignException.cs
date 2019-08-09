using System;
using System.Collections.Generic;
using System.Text;

namespace Amil.Atendimentos.ExternalServices.Helpers
{
    public class DigitalSignException : Exception
    {
        public DigitalSignException(string message) : base(message)
        { }

        public DigitalSignException(string message, Exception inner) : base(message, inner)
        { }
    }
}
