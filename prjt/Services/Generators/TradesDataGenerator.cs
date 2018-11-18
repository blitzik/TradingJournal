using Perst;
using prjt.Domain;
using prjt.Services.Identity;
using prjt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ExtensionMethods;
using System.Diagnostics;

namespace prjt.Services.Generators
{
    public class TradesDataGenerator
    {
        public TradesDataGenerator()
        {
        }


        private string GetKey(StatsPeriod period, int year, int month, int week, int day)
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}", period.ToString(), year, month, week, day);
        }


        public void GenerateData(Storage storage, Root root, IIdentity identity)
        {
            Dictionary<string, Stats> statsStorage = new Dictionary<string, Stats>();
            List<Trade> allTrades = new List<Trade>();
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
                storage.Store(s);
                root.Signals.Put(s);
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
                storage.Store(m);
                root.Markets.Put(m);
            }

            Stopwatch t = new Stopwatch();
            t.Start();
            for (int i = 0; i < 1000000; i++) {
                if (i % 15000 == 0) {
                    Debug.WriteLine(string.Format("Added objects: {0}", i));
                }

                DateTime tradeOpen = new DateTime(r.Next(2000, 2019), r.Next(1, 13), r.Next(1, 29), r.Next(0, 23), r.Next(0, 60), r.Next(0, 60));
                DateTime tradeClose = tradeOpen.AddMinutes(r.Next(5, 301));
                double entryPrice = r.Next(200, 5001);
                double exitPrice = r.Next(200, 5001);
                double spread = r.Next(100, 500);
                Direction direction = r.Next(0, 11) <= 5 ? Direction.LONG : Direction.SHORT;

                Trade trade = new Trade(
                    identity.Account.CurrentBalance,
                    tradeOpen,
                    markets[r.Next(0, markets.Length - 1)],
                    signals[r.Next(0, signals.Length - 1)],
                    direction,
                    r.Next(1, 100),
                    entryPrice,
                    spread,
                    0,
                    0,
                    0
                );
                double profitLoss = exitPrice - entryPrice;
                trade.CloseTrade(tradeClose, exitPrice, profitLoss, 0);

                allTrades.Add(trade);

                int weekNumber = Date.GetWeekNumber(trade.Year, trade.Month, trade.Day);

                // TOTAL
                Stats stats = statsStorage.GetValue(GetKey(StatsPeriod.TOTAL, 0, 0, 0, 0));
                if (stats == null) {
                    /*stats = (from Stats s in root.TradeStats
                             where s.Period == StatsPeriod.TOTAL &&
                                   s.Year == 0 &&
                                   s.Month == 0 &&
                                   s.Week == 0 &&
                                   s.Day == 0
                             select s).FirstOrDefault();

                    if (stats == null) {*/
                        stats = new Stats(StatsPeriod.TOTAL, 0, 0, 0, 0);
                        /*root.TradeStats.Put(new Key(new object[] { StatsPeriod.TOTAL, 0, 0, 0, 0 }), stats);
                    }*/
                    statsStorage.Add(GetKey(StatsPeriod.TOTAL, 0, 0, 0, 0), stats);
                }
                stats.AddTrade(trade);

                // YEAR
                stats = statsStorage.GetValue(GetKey(StatsPeriod.YEAR, trade.Year, 0, 0, 0));
                if (stats == null) {
                    /*stats = (from Stats s in root.TradeStats
                             where s.Period == StatsPeriod.YEAR &&
                                   s.Year == trade.Year &&
                                   s.Month == 0 &&
                                   s.Week == 0 &&
                                   s.Day == 0
                             select s).FirstOrDefault();

                    if (stats == null) {*/
                        stats = new Stats(StatsPeriod.YEAR, trade.Year, 0, 0, 0);
                        /*root.TradeStats.Put(new Key(new object[] { StatsPeriod.YEAR, trade.Year, 0, 0, 0 }), stats);
                    }*/
                    statsStorage.Add(GetKey(StatsPeriod.YEAR, trade.Year, 0, 0, 0), stats);
                }
                stats.AddTrade(trade);

                // MONTH
                stats = statsStorage.GetValue(GetKey(StatsPeriod.MONTH, trade.Year, trade.Month, 0, 0));
                if (stats == null) {
                    /*stats = (from Stats s in root.TradeStats
                             where s.Period == StatsPeriod.MONTH &&
                                   s.Year == trade.Year &&
                                   s.Month == trade.Month &&
                                   s.Week == 0 &&
                                   s.Day == 0
                             select s).FirstOrDefault();

                    if (stats == null) {*/
                        stats = new Stats(StatsPeriod.MONTH, trade.Year, trade.Month, 0, 0);
                        /*root.TradeStats.Put(new Key(new object[] { StatsPeriod.MONTH, trade.Year, trade.Month, 0, 0 }), stats);
                    }*/
                    statsStorage.Add(GetKey(StatsPeriod.MONTH, trade.Year, trade.Month, 0, 0), stats);
                }
                stats.AddTrade(trade);

                // WEEK
                stats = statsStorage.GetValue(GetKey(StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber, 0));
                if (stats == null) {
                    /*stats = (from Stats s in root.TradeStats
                             where s.Period == StatsPeriod.WEEK &&
                                   s.Year == trade.Year &&
                                   s.Month == trade.Month &&
                                   s.Week == weekNumber &&
                                   s.Day == 0
                             select s).FirstOrDefault();

                    if (stats == null) {*/
                        stats = new Stats(StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber, 0);
                        /*root.TradeStats.Put(new Key(new object[] { StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber, 0 }), stats);
                    }*/
                    statsStorage.Add(GetKey(StatsPeriod.WEEK, trade.Year, trade.Month, weekNumber, 0), stats);
                }
                stats.AddTrade(trade);

                // DAY
                stats = statsStorage.GetValue(GetKey(StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day));
                if (stats == null) {
                    /*stats = (from Stats s in root.TradeStats
                             where s.Period == StatsPeriod.DAY &&
                                   s.Year == trade.Year &&
                                   s.Month == trade.Month &&
                                   s.Week == weekNumber &&
                                   s.Day == trade.Day
                             select s).FirstOrDefault();

                    if (stats == null) {*/
                        stats = new Stats(StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day);
                        /*root.TradeStats.Put(new Key(new object[] { StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day }), stats);
                    }*/
                    statsStorage.Add(GetKey(StatsPeriod.DAY, trade.Year, trade.Month, weekNumber, trade.Day), stats);
                }
                stats.AddTrade(trade);
            }
            t.Stop();
            Debug.WriteLine(string.Format("For Cycle done in: {0}", t.ElapsedMilliseconds));
            Debug.WriteLine(string.Format("Total Trades generated: {0}", allTrades.Count));

            t.Reset();
            t.Start();
            foreach (Trade tr in allTrades) {
                storage.Store(tr);
                root.Trades.Put(new Key(new object[] { tr.Year, tr.Month, tr.Day }), tr);
                identity.Account.CurrentBalance += tr.ProfitLoss;
            }
            t.Stop();
            Debug.WriteLine(string.Format("Trades persisted in: {0}", t.ElapsedMilliseconds));
            

            storage.Modify(identity.Account);


            t.Reset();
            t.Start();
            foreach (KeyValuePair<string, Stats> i in statsStorage) {
                Stats s = i.Value;
                storage.Store(s);
                root.TradeStats.Put(new Key(new object[] { s.Period, s.Year, s.Month, s.Week, s.Day }), s);
            }
            t.Stop();
            Debug.WriteLine(string.Format("Stats persisted in: {0}", t.ElapsedMilliseconds));

            storage.Commit();
        }
    }
}
