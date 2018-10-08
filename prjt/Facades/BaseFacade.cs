using prjt.Services;
using Perst;
using System.IO;
using System;

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
   

        public BaseFacade(StoragePool storagePool)
        {
            _storagePool = storagePool;
        }


        protected Storage Storage(string name = null)
        {
            return StoragePool.GetByName(name);
        }


        protected TRoot Root<TRoot>(string name = null)
        {
            return (TRoot)StoragePool.GetByName(name).Root;
        }


        protected string GetAccountDataStorageFilePath(string accountName)
        {
            string path = Path.Combine(PerstStorageFactory.GetDatabaseDirectoryPath(), accountName);

            return Path.Combine(path, string.Format("{0}_data.{1}", accountName, PerstStorageFactory.DATABASE_EXTENSION));
        }
    }
}
