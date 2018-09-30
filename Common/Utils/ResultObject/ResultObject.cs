using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.ResultObject
{
    public enum ResultObjectMessageSeverity
    {
        INFO,
        SUCCESS,
        WARNING,
        ERROR
    }


    public class ResultObject<T> where T : class
    {
        private readonly bool _success;
        public bool Success
        {
            get { return _success; }
        }


        private T _result;
        public T Result
        {
            get { return _result; }
        }


        private List<ResultMessage> _messages;

        public ResultObject(bool success, T result = null)
        {
            _messages = new List<ResultMessage>();
            _success = success;
            _result = result;
        }


        public void AddMessage(string message, ResultObjectMessageSeverity severity = ResultObjectMessageSeverity.ERROR)
        {
            _messages.Add(new ResultMessage(message, severity));
        }


        public List<ResultMessage> GetMessages()
        {
            return new List<ResultMessage>(_messages);
        }


        public ResultMessage GetLastMessage()
        {
            return _messages.Last();
        }
    }
}
