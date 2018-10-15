using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjt.Services;
using prjt.Services.Identity;

namespace prjt.Facades
{
    public class MarketFacade : BaseFacade
    {
        public MarketFacade(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(identity, perstStorageFactory, storagePool)
        {
        }
    }
}
