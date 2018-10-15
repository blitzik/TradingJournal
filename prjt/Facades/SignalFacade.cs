using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjt.Domain;
using prjt.Services;
using prjt.Services.Identity;

namespace prjt.Facades
{
    public class SignalFacade : BaseFacade
    {
        public SignalFacade(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(identity, perstStorageFactory, storagePool)
        {
        }


        public Signal StoreSignal(Signal signal)
        {
            AccountDataStorage().Store(signal);
            AccountDataRoot().Signals.Put(signal);

            AccountDataStorage().Commit();

            return signal;
        }


        public IReadOnlyCollection<Signal> FindSignals()
        {
            IEnumerable<Signal> signals = from Signal s in AccountDataRoot().Signals select s;

            return new ReadOnlyCollection<Signal>(new List<Signal>(signals));
        }
    }
}
