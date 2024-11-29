using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redactor.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Problems { get; set; }

        public ValidationException() { }

        public ValidationException(string message) : base (message) { }

        public ValidationException(string message, IDictionary<string, string[]> problems) : base(message) {  Problems = problems; }
    }
}
