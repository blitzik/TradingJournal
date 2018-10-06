using prjt.Services;
using Perst;

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
    }
}
