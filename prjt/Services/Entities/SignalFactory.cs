using prjt.Domain;
using prjt.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Services.Entities
{
    public class SignalFactory : BaseEntityFactory
    {
        public SignalFactory(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(identity, perstStorageFactory, storagePool)
        {
        }

        public Signal Create(string name)
        {
            return new Signal(name);
        }
    }
}
