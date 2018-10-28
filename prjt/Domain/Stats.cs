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


        // -----


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


        public double WinRate
        {
            get { return ((WinLongCount + WinShortCount) / TotalTradesCount) * 100; }
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


        // fees


        private double _totalCommisionOpen;
        public double TotalCommisionOpen
        {
            get { return _totalCommisionOpen; }
            private set { Set(ref _totalCommisionOpen, value); }
        }


        private double _totalCommisionClose;
        public double TotalCommisionClose
        {
            get { return _totalCommisionClose; }
            private set { Set(ref _totalCommisionClose, value); }
        }


        private double _totalSpread;
        public double TotalSpread
        {
            get { return _totalSpread; }
            private set { Set(ref _totalSpread, value); }
        }


        // RRR


        private double _totalExpectedRRRCount;
        public double TotalExpectedRRRCount
        {
            get { return _totalExpectedRRRCount; }
            private set { Set(ref _totalExpectedRRRCount, value); }
        }


        private double _sumOfExpectedRRRs;
        public double SumOfExpectedRRRs
        {
            get { return _sumOfExpectedRRRs; }
            private set { Set(ref _sumOfExpectedRRRs, value); }
        }


        public double AverageRRR
        {
            get { return (SumOfExpectedRRRs / TotalExpectedRRRCount) * 100; }
        }


        // best / worst trades


        private double _bestTrade;
        public double BestTrade
        {
            get { return _bestTrade; }
            private set { Set(ref _bestTrade, value); }
        }


        private double _bestTradePCT;
        public double BestTradePCT
        {
            get { return _bestTradePCT; }
            private set { Set(ref _bestTradePCT, value); }
        }


        private double _worstTrade;
        public double WorstTrade
        {
            get { return _worstTrade; }
            private set { Set(ref _worstTrade, value); }
        }


        private double _worstTradePCT;
        public double WorstTradePCT
        {
            get { return _worstTradePCT; }
            private set { Set(ref _worstTradePCT, value); }
        }


        // longest W/L streaks


        private bool _isWinningStreak;
        private bool IsWinningStreak
        {
            get { return _isWinningStreak; }
            set { Set(ref _isWinningStreak, value); }
        }


        private int _currentStreak;
        public int CurrentStreak
        {
            get { return _currentStreak; }
            private set { Set(ref _currentStreak, value); }
        }


        private int _longestWinStreak;
        public int LongestWinStreak
        {
            get { return _longestWinStreak; }
            private set { Set(ref _longestWinStreak, value); }
        }


        private int _longestLoseStreak;
        public int LongestLoseStreak
        {
            get { return _longestLoseStreak; }
            private set { Set(ref _longestLoseStreak, value); }
        }



        private Stats() { }


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
            TotalTradesCount += 1;
            TotalNetPL += trade.ProfitLoss;
            TotalCommisionOpen += trade.CommissionOpen;
            TotalCommisionClose += trade.CommissionClose;
            TotalSpread += trade.Spread;

            if (trade.ExpectedRiskRewardRatio != null) {
                TotalExpectedRRRCount += 1;
                SumOfExpectedRRRs += trade.ExpectedRiskRewardRatio ?? 0;
            }

            if (trade.IsWin()) {
                if (trade.Direction == Direction.LONG) {
                    WinLongCount += 1;
                    WinLong += trade.ProfitLoss;
                } else {
                    WinShortCount += 1;
                    WinShort += trade.ProfitLoss;
                }

                if (IsWinningStreak == true) {
                    CurrentStreak += 1;
                    if (LongestWinStreak < CurrentStreak) {
                        LongestWinStreak = CurrentStreak;
                    }
                } else {
                    CurrentStreak = 0;
                    IsWinningStreak = true;
                }

            } else {
                if (trade.Direction == Direction.LONG) {
                    LossLongCount += 1;
                    LossLong += trade.ProfitLoss;

                } else {
                    LossShortCount += 1;
                    LossShort += trade.ProfitLoss;
                }

                if (IsWinningStreak == true) {
                    CurrentStreak = 0;
                    IsWinningStreak = false;
                } else {
                    CurrentStreak += 1;
                    if (LongestLoseStreak < CurrentStreak) {
                        LongestLoseStreak = CurrentStreak;
                    }
                }
            }

            double bwTradePCT = (trade.ProfitLoss / trade.AccountBalanceBeforeTrade) * 100;
            if (BestTrade < trade.ProfitLoss) {
                BestTrade = trade.ProfitLoss;
                BestTradePCT = bwTradePCT;
            }

            if (WorstTrade > trade.ProfitLoss) {
                WorstTrade = trade.ProfitLoss;
                WorstTradePCT = bwTradePCT;
            }
        }
    }
}
