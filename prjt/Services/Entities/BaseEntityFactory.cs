using Perst;
using prjt.Services.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Services.Entities
{
    public abstract class BaseEntityFactory
    {
        private IIdentity _identity;
        private PerstStorageFactory _perstStorageFactory;
        private StoragePool _storagePool;

        public BaseEntityFactory(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool)
        {
            _identity = identity;
            _perstStorageFactory = perstStorageFactory;
            _storagePool = storagePool;
        }


        protected Storage GetStorage()
        {
            if (!_storagePool.ContainsByName(_identity.Account.Name)) {
                _storagePool.Add(_identity.Account.Name, _perstStorageFactory.OpenConnection<Root>(DatabaseNames.GetAccountDataStorageFilePath(_identity.Account.Name)));
            }

            return _storagePool.GetByName(_identity.Account.Name);
        }

    }
}
