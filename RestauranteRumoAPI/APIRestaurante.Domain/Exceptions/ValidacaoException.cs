using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRestaurante.Domain.Exceptions
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException() { }

        public ValidacaoException(string message) : base(message) { }

        public ValidacaoException(string message, Exception exception) : base(message, exception) { }
    }
}
