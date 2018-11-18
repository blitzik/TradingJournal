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
using System.Diagnostics;
using Common.ExtensionMethods;
using prjt.Services.Generators;

namespace prjt.Facades
{
    public class TradeFacade : BaseFacade
    {
        private readonly TradesDataGenerator _tradesDataGenerator;

        public TradeFacade(
            IIdentity identity,
            PerstStorageFactory perstStorageFactory,
            StoragePool storagePool,
            TradesDataGenerator tradesDataGenerator) : base(identity, perstStorageFactory, storagePool)
        {
            _tradesDataGenerator = tradesDataGenerator;
        }


        public void SaveTrade(Trade trade)
        {
            AccountDataStorage().Store(trade);
            AccountDataRoot().Trades.Put(new Key(new object[] { trade.Year, trade.Month, trade.Day }), trade);

            int weekNumber = Date.GetWeekNumber(trade.Year, trade.Month, trade.Day);

            Stats total = SaveTradeStats(trade, StatsPeriod.TOTAL);
            Stats year = SaveTradeStats(trade, StatsPeriod.YEAR, trade.Year);
            Stats month = SaveTradeStats(trade, StatsPeriod.MONTH, trade.Year, trade.Month);
            Stats week = SaveTradeStats(trade, StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber);
            Stats day = SaveTradeStats(trade, StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day);

            //SaveMarketStats(trade, StatsPeriod.TOTAL);
            //SaveMarketStats(trade, StatsPeriod.YEAR, trade.Year);
            //SaveMarketStats(trade, StatsPeriod.MONTH, trade.Year, trade.Month);
            //SaveMarketStats(trade, StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber);
            //SaveMarketStats(trade, StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day);
                             
            //SaveSignalStats(trade, StatsPeriod.TOTAL);
            //SaveSignalStats(trade, StatsPeriod.YEAR, trade.Year);
            //SaveSignalStats(trade, StatsPeriod.MONTH, trade.Year, trade.Month);
            //SaveSignalStats(trade, StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber);
            //SaveSignalStats(trade, StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day);

            Identity.Account.CurrentBalance += trade.ProfitLoss;
            AccountDataStorage().Modify(Identity.Account);

            AccountDataStorage().Commit();
        }


        public void GenerateData()
        {
            _tradesDataGenerator.GenerateData(AccountDataStorage(), AccountDataRoot(), Identity);
        }


        public IEnumerable<Trade> FindTrades(int year)
        {
            IEnumerable<Trade> trades = from Trade t in AccountDataRoot().Trades
                                        where t.Year == year
                                        select t;

            return trades;
        }


        public IEnumerable<Trade> FindTrades(int year, int month)
        {
            IEnumerable<Trade> trades = from Trade t in AccountDataRoot().Trades
                                        where t.Year == year && t.Month == month
                                        select t;

            return trades;
        }


        public IEnumerable<Trade> FindTrades(int year, int month, int day)
        {
            IEnumerable<Trade> trades = from Trade t in AccountDataRoot().Trades
                                        where t.Year == year &&
                                              t.Month == month &&
                                              t.Day == day
                                        select t;

            return trades;
        }


        public IEnumerable<int> GetYears()
        {
            return (from Trade t in AccountDataRoot().Trades select t.Year).Distinct();
        }
        

        private Stats SaveTradeStats(Trade trade, StatsPeriod period, int year = 0, int month = 0, int week = 0, int day = 0)
        {
            Stats stats = (from Stats s in AccountDataRoot().TradeStats
                           where s.Period == period &&
                                 s.Year == year &&
                                 s.Month == month &&
                                 s.Week == week &&
                                 s.Day == day
                           select s).FirstOrDefault();
            if (stats == null) {
                stats = new Stats(period, year, month, week, day);
                AccountDataRoot().TradeStats.Put(new Key(new object[] { period, year, month, week, day }), stats);
            }
            stats.AddTrade(trade);
            AccountDataStorage().Modify(stats);

            return stats;
        }


        private void SaveMarketStats(Trade trade, StatsPeriod period, int year = 0, int month = 0, int week = 0, int day = 0)
        {
            MarketStats stats = (from MarketStats s in AccountDataRoot().MarketStats
                           where s.Period == period &&
                                 s.Year == year &&
                                 s.Month == month &&
                                 s.Week == week &&
                                 s.Day == day
                           select s).FirstOrDefault();
            if (stats == null) {
                stats = new MarketStats(trade.Market, period, year, month, week, day);
                AccountDataRoot().MarketStats.Put(new Key(new object[] { trade.Market.Symbol, period, year, month, week, day }), stats);
            }
            stats.AddTrade(trade);
            AccountDataStorage().Modify(stats);
        }


        private void SaveSignalStats(Trade trade, StatsPeriod period, int year = 0, int month = 0, int week = 0, int day = 0)
        {
            SignalStats stats = (from SignalStats s in AccountDataRoot().SignalStats
                                 where s.Period == period &&
                                       s.Year == year &&
                                       s.Month == month &&
                                       s.Week == week &&
                                       s.Day == day
                                 select s).FirstOrDefault();
            if (stats == null) {
                stats = new SignalStats(trade.Signal, period, year, month, week, day);
                AccountDataRoot().SignalStats.Put(new Key(new object[] { trade.Signal.Name, period, year, month, week, day }), stats);
            }
            stats.AddTrade(trade);
            AccountDataStorage().Modify(stats);
        }


        public IEnumerable<Stats> LoadStats()
        {
            return from Stats s in AccountDataRoot().TradeStats
                   /*where s.Period == StatsPeriod.TOTAL &&
                         s.Year == 0 &&
                         s.Month == 0 &&
                         s.Week == 0 &&
                         s.Day == 0*/
                   select s;
        }
    }
}
