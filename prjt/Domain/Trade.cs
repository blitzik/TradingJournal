using Caliburn.Micro;
using prjt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class Trade : PropertyChangedBase
    {
        private DateTime _dateOpen;
        public DateTime DateOpen
        {
            get { return _dateOpen; }
            private set { Set(ref _dateOpen, value); }
        }


        private DateTime _dateClose;
        public DateTime DateClose
        {
            get { return _dateClose; }
            private set
            {
                Set(ref _dateClose, value);

                Year = _dateClose.Year;
                Month = _dateClose.Month;
                Day = _dateClose.Day;
            }
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


        private int _day;
        public int Day
        {
            get { return _day; }
            private set { Set(ref _day, value); }
        }


        private double _accountBalanceBeforeTrade;
        public double AccountBalanceBeforeTrade
        {
            get { return _accountBalanceBeforeTrade; }
            private set { Set(ref _accountBalanceBeforeTrade, value); }
        }


        private double _accountBalanceAfterTrade;
        public double AccountBalanceAfterTrade
        {
            get { return _accountBalanceAfterTrade; }
            private set { Set(ref _accountBalanceAfterTrade, value); }
        }


        private Market _market;
        public Market Market
        {
            get { return _market; }
            private set { Set(ref _market, value); }
        }


        private double _volume;
        public double Volume
        {
            get { return _volume; }
            private set { Set(ref _volume, value); }
        }


        private double _spread;
        public double Spread
        {
            get { return _spread; }
            private set { Set(ref _spread, value); }
        }


        private Signal _signal;
        public Signal Signal
        {
            get { return _signal; }
            private set { Set(ref _signal, value); }
        }


        private double _commissionOpen;
        public double CommissionOpen
        {
            get { return _commissionOpen; }
            private set { Set(ref _commissionOpen, value); }
        }


        private double _commissionClose;
        public double CommissionClose
        {
            get { return _commissionClose; }
            private set { Set(ref _commissionClose, value); }
        }


        private Direction _direction;
        public Direction Direction
        {
            get { return _direction; }
            private set { Set(ref _direction, value); }
        }


        private double _entryPrice;
        public double EntryPrice
        {
            get { return _entryPrice; }
            private set { Set(ref _entryPrice, value); }
        }


        private double _exitPrice;
        public double ExitPrice
        {
            get { return _exitPrice; }
            private set { Set(ref _exitPrice, value); }
        }


        private double _profitLoss;
        public double ProfitLoss
        {
            get { return _profitLoss; }
            private set { Set(ref _profitLoss, value); }
        }


        private double? _stopLoss;
        public double? StopLoss
        {
            get { return _stopLoss; }
            private set { Set(ref _stopLoss, value); }
        }


        private double? _targetProfit;
        public double? TargetProfit
        {
            get { return _targetProfit; }
            private set { Set(ref _targetProfit, value); }
        }


        private double? _expectedRiskRewardRatio;
        public double? ExpectedRiskRewardRatio
        {
            get { return _expectedRiskRewardRatio; }
            private set { Set(ref _expectedRiskRewardRatio, value); }
        }


        private bool? _hitStopLoss;
        public bool? HitStopLoss
        {
            get { return _hitStopLoss; }
            private set { Set(ref _hitStopLoss, value); }
        }


        private bool? _hitTargetProfit;
        public bool? HitTargetProfit
        {
            get { return _hitTargetProfit; }
            private set { Set(ref _hitTargetProfit, value); }
        }


        private Trade() { }

        public Trade(
            double accountBalance,
            DateTime dateOpen,
            Market market,
            Signal signal,
            Direction direction,
            double volume,
            double entryPrice,
            double spread,
            double commissionOpen = 0,
            double? stopLoss = null,
            double? targetProfit = null
        ) {
            AccountBalanceBeforeTrade = accountBalance;
            DateOpen = dateOpen;
            Market = market;
            Signal = signal;
            Direction = direction;
            Volume = volume;
            EntryPrice = entryPrice;
            Spread = spread;
            CommissionOpen = CommissionOpen;
            StopLoss = stopLoss;
            TargetProfit = targetProfit;
        }


        public bool IsWin()
        {
            return _profitLoss > 0;
        }


        public void CloseTrade(DateTime dateClose, double exitPrice, double profitLoss, double commissionClose = 0)
        {
            DateClose = dateClose;
            ExitPrice = exitPrice;
            CommissionClose = commissionClose;
            ProfitLoss = profitLoss;
            AccountBalanceAfterTrade = AccountBalanceBeforeTrade + profitLoss;

            if (StopLoss != null && TargetProfit != null) {
                // calculated for LONG position
                double rrr = ((TargetProfit ?? 0) - EntryPrice) / (EntryPrice - (StopLoss ?? 0));
                if (Direction == Direction.SHORT) {
                    rrr = Math.Pow(rrr, -1);
                }
                ExpectedRiskRewardRatio = rrr;
            }
        }


        public void CloseByStopLoss(DateTime dateClose, double exitPrice, double profitLoss, double commissionClose)
        {
            CloseTrade(dateClose, exitPrice, profitLoss, commissionClose);
            HitStopLoss = true;
        }


        public void CloseByTargetProfit(DateTime dateClose, double exitPrice, double profitLoss, double commissionClose)
        {
            CloseTrade(dateClose, exitPrice, profitLoss, commissionClose);
            HitStopLoss = false;
        }


    }
}
