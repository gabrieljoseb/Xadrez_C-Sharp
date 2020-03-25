using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    class DomainException : Exception
    {
        public DomainException(string msg) : base(msg)
        {
        }
    }
}
