using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public interface IValidationObject : INotifyDataErrorInfo
    {
        Dictionary<string, IRuleSet> RuleSets { get; }
        Dictionary<string, List<IValidationMessage>> Errors { get; }

        void AddRuleSet<P>(string propertyName, IDelegateRuleSet<P> ruleSet);

        bool Check<T>(string propertyName, T obj, bool collectErrors = false);

        void RaiseErrorsChanged(string propertyName);
    }
}
