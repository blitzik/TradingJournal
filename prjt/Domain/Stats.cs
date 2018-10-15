using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class Stats : PropertyChangedBase
    {
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



    }
}
