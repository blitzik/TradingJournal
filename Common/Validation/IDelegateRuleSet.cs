using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public interface IDelegateRuleSet<T> : IRuleSet
    {
        bool Check(T obj);
    }
}
