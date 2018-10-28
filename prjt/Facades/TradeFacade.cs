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

            SaveTradeStats(trade, StatsPeriod.TOTAL);
            SaveTradeStats(trade, StatsPeriod.YEAR, trade.Year);
            SaveTradeStats(trade, StatsPeriod.MONTH, trade.Year, trade.Month);
            SaveTradeStats(trade, StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber);
            SaveTradeStats(trade, StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day);

            SaveMarketStats(trade, StatsPeriod.TOTAL);
            SaveMarketStats(trade, StatsPeriod.YEAR, trade.Year);
            SaveMarketStats(trade, StatsPeriod.MONTH, trade.Year, trade.Month);
            SaveMarketStats(trade, StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber);
            SaveMarketStats(trade, StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day);
                             
            SaveSignalStats(trade, StatsPeriod.TOTAL);
            SaveSignalStats(trade, StatsPeriod.YEAR, trade.Year);
            SaveSignalStats(trade, StatsPeriod.MONTH, trade.Year, trade.Month);
            SaveSignalStats(trade, StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber);
            SaveSignalStats(trade, StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day);

            //AccountDataStorage().Commit();
        }


        public void GenerateData()
        {
            Random r = new Random();

            Signal[] signals = {
                new Signal(System.Guid.NewGuid().ToString()),
                new Signal(System.Guid.NewGuid().ToString()),
                new Signal(System.Guid.NewGuid().ToString()),
                new Signal(System.Guid.NewGuid().ToString()),
                new Signal(System.Guid.NewGuid().ToString()),
                new Signal(System.Guid.NewGuid().ToString()),
            };
            foreach (Signal s in signals) {
                AccountDataStorage().Store(s);
                AccountDataRoot().Signals.Put(s);
            }

            Market[] markets = {
                new Market("EUR / USD"),
                new Market("NZD / USD"),
                new Market("AUD / USD"),
                new Market("CAD / USD"),
                new Market("USD / JPY"),
                new Market("GBP / USD"),
                new Market("FRA40"),
                new Market("DE30"),
                new Market("US200"),
                new Market("ITA40"),
            };
            foreach (Market m in markets) {
                AccountDataStorage().Store(m);
                AccountDataRoot().Markets.Put(m);
            }

            for (int i = 0; i <= 3000; i++) {
                DateTime tradeOpen = new DateTime(r.Next(2010, 2019), r.Next(1, 13), r.Next(1, 29), r.Next(0, 23), r.Next(0, 60), r.Next(0, 60));
                DateTime tradeClose = tradeOpen.AddMinutes(r.Next(5, 301));
                double entryPrice = r.Next(200, 5001);
                double exitPrice = r.Next(200, 5001);
                double spread = r.Next(100, 500);

                Trade trade = new Trade(
                    Identity.Account.CurrentBalance,
                    tradeOpen,
                    markets[r.Next(0, markets.Length - 1)],
                    signals[r.Next(0, signals.Length - 1)],
                    r.Next(0, 11) <= 5 ? Direction.LONG : Direction.SHORT,
                    r.Next(1, 100),
                    entryPrice,
                    spread,
                    0,
                    0,
                    0
                );
                trade.CloseTrade(tradeClose, exitPrice, exitPrice - entryPrice, 0);

                SaveTrade(trade);
            }
            AccountDataStorage().Commit();
        }


        private void SaveTradeStats(Trade trade, StatsPeriod period, int year = 0, int month = 0, int week = 0, int day = 0)
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
                AccountDataStorage().Store(stats);
                AccountDataRoot().TradeStats.Put(new Key(new object[] { period, year, month, week, day }), stats);
            }
            stats.AddTrade(trade);
            AccountDataStorage().Modify(stats);
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
                AccountDataStorage().Store(stats);
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
                AccountDataStorage().Store(stats);
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
