using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public class RuleSet<T> : IDelegateRuleSet<T>
    {
        private List<IValidationMessage> _errors;
        public List<IValidationMessage> Errors
        {
            get { return _errors; }
        }


        private List<IRule<T>> _rules;

        public RuleSet() : base()
        {
            _errors = new List<IValidationMessage>();
            _rules = new List<IRule<T>>();
        }


        public RuleSet<T> AddRule(Func<T, bool> rule, string message, Severity severity = Severity.WARNING)
        {
            _rules.Add(new Rule<T>(rule, message, severity));
            return this;
        }


        public RuleSet<T> AddRule(IRule<T> rule)
        {
            _rules.Add(rule);
            return this;
        }


        public bool Check(T obj)
        {
            Errors.Clear();
            foreach (IRule<T> rule in _rules) {
                if (!rule.Check(obj)) {
                    Errors.Add(rule.Error);
                }
            }

            if (Errors.Count > 0) {
                return false;
            }
            return true;
        }
    }
}
