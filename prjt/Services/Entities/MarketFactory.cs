using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjt.Domain;
using prjt.Services.Identity;

namespace prjt.Services.Entities
{
    public class MarketFactory : BaseEntityFactory
    {
        public MarketFactory(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(identity, perstStorageFactory, storagePool)
        {
        }


        public Market Create(string symbol)
        {
            return new Market(symbol);
        }
    }
}
