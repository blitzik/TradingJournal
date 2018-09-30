using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public class ValidationMessage : IValidationMessage
    {
        private string _message;
        public string Message
        {
            get { return _message; }
        }


        private Severity _severity;
        public Severity Severity
        {
            get { return _severity; }
        }


        public ValidationMessage(string message, Severity severity)
        {
            _message = message;
            _severity = severity;
        }
    }
}
