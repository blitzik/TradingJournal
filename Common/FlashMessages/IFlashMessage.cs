using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FlashMessages
{
    public interface IFlashMessage
    {
        string Message { get; }
        Type Type { get; }
        bool CanBeDisposed { get; }
        TimeSpan Lifespan { get; }

        void MarkAsDisposable();
    }
}
