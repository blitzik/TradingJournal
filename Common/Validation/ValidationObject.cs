using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public class ValidationObject : IValidationObject
    {
        private Dictionary<string, List<IValidationMessage>> _errors;
        public Dictionary<string, List<IValidationMessage>> Errors
        {
            get { return _errors; }
        }


        private Dictionary<string, IRuleSet> _ruleSets;
        public Dictionary<string, IRuleSet> RuleSets
        {
            get { return _ruleSets; }
        }


        public ValidationObject()
        {
            _ruleSets = new Dictionary<string, IRuleSet>();
            _errors = new Dictionary<string, List<IValidationMessage>>();
        }


        public void AddRuleSet<P>(string propertyName, IDelegateRuleSet<P> ruleSet)
        {
            if (string.IsNullOrEmpty(propertyName)) {
                throw new Exception("Property name cannot be empty string or null");
            }

            if (RuleSets.ContainsKey(propertyName)) {
                throw new Exception(string.Format("RuleSet already exists for property \"{0}\"", propertyName));
            }

            RuleSets.Add(propertyName, ruleSet);
        }


        public bool Check<T>(string propertyName, T obj, bool collectErrors = false)
        {
            if (string.IsNullOrEmpty(propertyName)) {
                throw new ArgumentException("Argument \"propertyName\" cannot be NULL");
            }

            if (!RuleSets.ContainsKey(propertyName)) {
                return true;
            }

            if (collectErrors == true && Errors.ContainsKey(propertyName)) {
                Errors.Remove(propertyName);
            }

            IDelegateRuleSet<T> ruleSet = (IDelegateRuleSet<T>)RuleSets[propertyName];
            if (!ruleSet.Check(obj)) {
                if (collectErrors == true) {
                    Errors.Add(propertyName, ruleSet.Errors);
                    RaiseErrorsChanged(propertyName);
                }
                return false;
            }

            return true;
        }


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public void RaiseErrorsChanged(string propertyname)
        {
            var handler = ErrorsChanged;
            if (handler != null) {
                handler(this, new DataErrorsChangedEventArgs(propertyname));
            }
        }


        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }


        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) {
                return Errors.Values;
            }

            return Errors[propertyName];
        }
    }
}
