using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public interface IRule<T>
    {
        IValidationMessage Error { get; }

        bool Check(T obj);
    }
}
