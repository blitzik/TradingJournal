using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public enum Severity
    {
        INFO,
        WARNING,
        ERROR
    }


    public interface IValidationMessage
    {
        string Message { get; }
        Severity Severity { get; }
    }
}
