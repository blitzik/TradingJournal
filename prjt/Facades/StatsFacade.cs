using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjt.Domain;
using prjt.Services;
using prjt.Services.Identity;

namespace prjt.Facades
{
    public class StatsFacade : BaseFacade
    {
        public StatsFacade(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(identity, perstStorageFactory, storagePool)
        {
        }


        public Stats GetTotalStats()
        {
            return (from Stats s in AccountDataRoot().TradeStats where s.Period == StatsPeriod.TOTAL select s).FirstOrDefault();
        }


        public IEnumerable<Stats> FindStats()
        {
            IEnumerable<Stats> stats = from Stats s in AccountDataRoot().TradeStats
                                       where s.Period == StatsPeriod.YEAR
                                       select s;

            return stats;
        }


        public IEnumerable<Stats> FindStats(int year)
        {
            IEnumerable<Stats> stats = from Stats s in AccountDataRoot().TradeStats
                                       where s.Period == StatsPeriod.MONTH &&
                                             s.Year == year
                                       select s;

            return stats;
        }


        public IEnumerable<Stats> FindStats(int year, int month)
        {
            IEnumerable<Stats> stats = from Stats s in AccountDataRoot().TradeStats
                                       where s.Period == StatsPeriod.WEEK &&
                                             s.Year == year &&
                                             s.Month == month
                                       select s;

            return stats;
        }


        public IEnumerable<Stats> FindStats(int year, int month, int week)
        {
            IEnumerable<Stats> stats = from Stats s in AccountDataRoot().TradeStats
                                       where s.Period == StatsPeriod.DAY &&
                                             s.Year == year &&
                                             s.Month == month &&
                                             s.Week == week
                                       select s;

            return stats;
        }


        public IEnumerable<int> GetYears()
        {
            return (from Stats s in AccountDataRoot().TradeStats where s.Period == StatsPeriod.YEAR select s.Year).Distinct();
        }
    }
}
