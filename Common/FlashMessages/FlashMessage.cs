using Caliburn.Micro;
using System;

namespace Common.FlashMessages
{
    public class FlashMessage : PropertyChangedBase, IFlashMessage
    {
        private string _message;
        public string Message
        {
            get { return _message; }
        }


        private Type _type;
        public Type Type
        {
            get { return _type; }
        }


        private bool _canBeDisposed;
        public bool CanBeDisposed
        {
            get { return _canBeDisposed; }
        }


        private TimeSpan _lifespan;
        public TimeSpan Lifespan
        {
            get { return _lifespan; }
        }


        public FlashMessage(string message, Type type, TimeSpan? lifespan = null)
        {
            _message = message;
            _type = type;
            _canBeDisposed = false;
            _lifespan = lifespan ?? TimeSpan.FromMilliseconds(3000);
        }


        public void MarkAsDisposable()
        {
            Set(ref _canBeDisposed, true);
        }
    }
}