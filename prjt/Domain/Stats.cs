using Caliburn.Micro;
using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public enum StatsPeriod
    {
        TOTAL, YEAR, MONTH, WEEK, DAY
    }


    public class Stats : PropertyChangedBase
    {
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


        private int _totalTradesCount;
        public int TotalTradesCount
        {
            get { return _totalTradesCount; }
            private set { Set(ref _totalTradesCount, value); }
        }


        private int _winLongCount;
        public int WinLongCount
        {
            get { return _winLongCount; }
            private set { Set(ref _winLongCount, value); }
        }


        private int _lossLongCount;
        public int LossLongCount
        {
            get { return _lossLongCount; }
            private set { Set(ref _lossLongCount, value); }
        }


        private int _winShortCount;
        public int WinShortCount
        {
            get { return _winShortCount; }
            private set { Set(ref _winShortCount, value); }
        }


        private int _lossShortCount;
        public int LossShortCount
        {
            get { return _lossShortCount; }
            private set { Set(ref _lossShortCount, value); }
        }


        // -----


        private double _totalNetPL; // Total Net Profit/Loss
        public double TotalNetPL
        {
            get { return _totalNetPL; }
            private set { Set(ref _totalNetPL, value); }
        }


        private double _winLong;
        public double WinLong
        {
            get { return _winLong; }
            private set { Set(ref _winLong, value); }
        }


        private double _lossLong;
        public double LossLong
        {
            get { return _lossLong; }
            private set { Set(ref _lossLong, value); }
        }


        private double _winShort;
        public double WinShort
        {
            get { return _winShort; }
            private set { Set(ref _winShort, value); }
        }


        private double _lossShort;
        public double LossShort
        {
            get { return _lossShort; }
            private set { Set(ref _lossShort, value); }
        }


        public Stats() { }


        public Stats(StatsPeriod period, int year, int month, int week, int day)
        {
            Period = period;
            Year = year < 0 ? 0 : year;
            Month = month < 0 ? 0 : month;
            Week = week < 0 ? 0 : week;
            Day = day < 0 ? 0 : day;
        }


        public Stats(int year)
        {
            Year = year < 0 ? 0 : year;
            Period = StatsPeriod.YEAR;
        }


        public Stats(int year, int month) : this(year)
        {
            Month = month < 0 ? 0 : month;
            Period = StatsPeriod.MONTH;
        }


        public Stats(int year, int month, int week) : this(year, month)
        {
            Week = week < 0 ? 0 : week;
            Period = StatsPeriod.WEEK;
        }


        public Stats(int year, int month, int week, int day) : this(year, month, week)
        {
            Day = day < 0 ? 0 : day;
            Period = StatsPeriod.DAY;
        }


        public void AddTrade(Trade trade)
        {
            _totalTradesCount += 1;
            _totalNetPL += trade.ProfitLoss;
            if (trade.IsWin()) {
                if (trade.Direction == Direction.LONG) {
                    _winLongCount += 1;
                    _winLong += trade.ProfitLoss;
                } else {
                    _winShortCount += 1;
                    _winShort += trade.ProfitLoss;
                }

            } else {
                if (trade.Direction == Direction.LONG) {
                    _lossLongCount += 1;
                    _lossLong += trade.ProfitLoss;

                } else {
                    _lossShortCount += 1;
                    _lossShort += trade.ProfitLoss;
                }
            }
        }
    }
}
