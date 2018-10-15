using prjt.Services;
using Perst;
using System.IO;
using System;
using prjt.Services.Identity;

namespace prjt.Facades
{
    abstract public class BaseFacade
    {
        private StoragePool _storagePool;
        protected StoragePool StoragePool
        {
            get { return _storagePool; }
            set { _storagePool = value; }
        }


        private IIdentity _identity;
        protected IIdentity Identity
        {
            get { return _identity; }
        }


        protected PerstStorageFactory _perstStorageFactory;

        public BaseFacade(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool)
        {
            _identity = identity;
            _perstStorageFactory = perstStorageFactory;
            _storagePool = storagePool;
        }


        protected Storage AccountDataStorage(string accountName = null)
        {
            string accName = string.IsNullOrEmpty(accountName) ? Identity.Account.Name : accountName;
            if (!StoragePool.ContainsByName(accName)) {
                StoragePool.Add(accName, _perstStorageFactory.OpenConnection<Root>(GetAccountDataStorageFilePath(accName)));
            }
            return StoragePool.GetByName(accName);
        }


        protected Root AccountDataRoot(string accountName = null)
        {
            return (Root)AccountDataStorage(accountName).Root;
        }


        protected Storage Storage(string name)
        {
            return StoragePool.GetByName(name);
        }


        protected TRoot Root<TRoot>(string storageName)
        {
            return (TRoot)StoragePool.GetByName(storageName).Root;
        }


        protected string GetAccountDataStorageFilePath(string accountName)
        {
            string path = Path.Combine(PerstStorageFactory.GetDatabaseDirectoryPath(), accountName);

            return Path.Combine(path, string.Format("{0}_data.{1}", accountName, PerstStorageFactory.DATABASE_EXTENSION));
        }
    }
}
