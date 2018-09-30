using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventAggregator.Messages
{
    public interface IChangeViewMessage<T>
    {
        Type Type { get; }
        T ViewModel { get; }

        void Apply(T viewModel);
    }
}
