using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public class Rule<T> : IRule<T>
    {
        private IValidationMessage _error;
        public IValidationMessage Error
        {
            get { return _error; }
        }


        private Func<T, bool> _rule;

        public Rule(Func<T, bool> rule, string message, Severity severity)
        {
            _rule = rule;
            _error = new ValidationMessage(message, severity);    
        }


        public bool Check(T obj)
        {
            if (!_rule.Invoke(obj)) {
                return true;
            }
            return false;
        }
    }
}
