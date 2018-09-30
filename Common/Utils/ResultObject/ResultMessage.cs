using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.ResultObject
{
    public class ResultMessage
    {
        private string _text;
        public string Text
        {
            get { return _text; }
        }


        private ResultObjectMessageSeverity _severity;
        public ResultObjectMessageSeverity Severity
        {
            get { return _severity; }
        }


        public ResultMessage(string text, ResultObjectMessageSeverity severity)
        {
            _text = text;
            _severity = severity;
        }


        public override string ToString()
        {
            return Text;
        }

    }
}
