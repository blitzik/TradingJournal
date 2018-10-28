using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class MarketStats : PropertyChangedBase
    {
        private string _marketSymbol;
        public string MarketSymbol
        {
            get { return _marketSymbol; }
            private set { Set(ref _marketSymbol, value); }
        }


        private Market _market;
        public Market Market
        {
            get { return _market; }
            private set { Set(ref _market, value); }
        }


        private StatsPeriod _period;
        public StatsPeriod Period
        {
            get { return _period; }
            private set { Set(ref _period, value); }
        }


        private int _year;
        public int Year
        {
            get { return _year; }
            private set { Set(ref _year, value); }
        }


        private int _month;
        public int Month
        {
            get { return _month; }
            private set { Set(ref _month, value); }
        }


        private int _week;
        public int Week
        {
            get { return _week; }
            private set { Set(ref _week, value); }
        }


        private int _day;
        public int Day
        {
            get { return _day; }
            private set { Set(ref _day, value); }
        }



        private Stats _stats;
        public Stats Stats
        {
            get { return _stats; }
            private set { Set(ref _stats, value); }
        }


        private MarketStats() { }

        public MarketStats(Market market, StatsPeriod statsPeriod = StatsPeriod.TOTAL, int year = 0, int month = 0, int week = 0, int day = 0)
        {
            MarketSymbol = market.Symbol;
            Market = market;

            Stats = new Stats(statsPeriod, year, month, week, day);
            Period = statsPeriod;
            Year = year;
            Month = month;
            Week = week;
            Day = day;
        }


        public void AddTrade(Trade trade)
        {
            Stats.AddTrade(trade);
        }

    }
}
