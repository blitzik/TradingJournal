using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjt.Domain;
using prjt.Services;
using prjt.Services.Identity;
using Perst;
using prjt.Utils;

namespace prjt.Facades
{
    public class TradeFacade : BaseFacade
    {
        public TradeFacade(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(identity, perstStorageFactory, storagePool)
        {
        }


        public void SaveTrade(Trade trade)
        {
            AccountDataStorage().Store(trade);
            AccountDataRoot().Trades.Put(new Key(new object[] { trade.Year, trade.Month, trade.Day }), trade);

            int weekNumber = Date.GetWeekNumber(trade.Year, trade.Month, trade.Day);

            SaveStats(trade, StatsPeriod.TOTAL);
            SaveStats(trade, StatsPeriod.YEAR, trade.Year);
            SaveStats(trade, StatsPeriod.MONTH, trade.Year, trade.Month);
            SaveStats(trade, StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber);
            SaveStats(trade, StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day);
            
            AccountDataStorage().Commit();
        }


        private void SaveStats(Trade trade, StatsPeriod period, int year = 0, int month = 0, int week = 0, int day = 0)
        {
            Stats stats = (from Stats s in AccountDataRoot().PeriodStats
                           where s.Period == period &&
                                 s.Year == year &&
                                 s.Month == month &&
                                 s.Week == week &&
                                 s.Day == day
                           select s).FirstOrDefault();
            if (stats == null) {
                stats = new Stats(period, year, month, week, day);
                AccountDataStorage().Store(stats);
                AccountDataRoot().PeriodStats.Put(new Key(new object[] { period, year, month, week, day }), stats);
            }
            stats.AddTrade(trade);
            AccountDataStorage().Modify(stats);
        }


        public IEnumerable<Stats> LoadStats()
        {
            return from Stats s in AccountDataRoot().PeriodStats
                   /*where s.Period == StatsPeriod.DAY &&
                         s.Year == 2018 &&
                         s.Month == 10 &&
                         s.Week == 43 &&
                         s.Day == 26*/
                   select s;
        }
    }
}
