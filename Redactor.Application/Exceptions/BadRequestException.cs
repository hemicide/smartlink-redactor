using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redactor.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public string[] Messages { get; set; }

        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string[] messages) : base(string.Join("\n", messages)) { Messages = messages; }
    }
}
