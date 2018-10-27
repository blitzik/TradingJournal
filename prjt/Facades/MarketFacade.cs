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
    public class MarketFacade : BaseFacade
    {
        public MarketFacade(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(identity, perstStorageFactory, storagePool)
        {
        }


        public Market StoreMarket(Market market)
        {
            AccountDataStorage().Store(market);
            AccountDataRoot().Markets.Put(market);

            AccountDataStorage().Commit();

            return market;
        }


        public void UpdateMarket(Market market)
        {
            AccountDataStorage().Modify(market);
            AccountDataStorage().Commit();
        }


        public IReadOnlyCollection<Market> FindMarketSymbols()
        {
            IEnumerable<Market> markets = from Market m in AccountDataRoot().Markets select m;

            return new ReadOnlyCollection<Market>(new List<Market>(markets));
        }
    }
}
