using Perst;
using prjt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Services
{
    public class Root : Persistent
    {
        private double _startingBalance;
        public double StartingBalance
        {
            get { return _startingBalance; }
            set { _startingBalance = value; }
        }


        private double _balance;
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }


        private Stats _stats;
        public Stats Stats
        {
            get { return _stats; }
            set { _stats = value; }
        }


        private IPersistentMap<int, YearStats> _years;
        public IPersistentMap<int, YearStats> Years
        {
            get { return _years; }
            private set { _years = value; }
        }


        private IPersistentMap<int, MonthStats> _months;
        public IPersistentMap<int, MonthStats> Months
        {
            get { return _months; }
            private set { _months = value; }
        }


        private IPersistentMap<int, WeekStats> _weeks;
        public IPersistentMap<int, WeekStats> Weeks
        {
            get { return _weeks; }
            private set { _weeks = value; }
        }


        private IPersistentMap<int, DayStats> _days;
        public IPersistentMap<int, DayStats> Days
        {
            get { return _days; }
            private set { _days = value; }
        }


        private CompoundIndex<Trade> _trades;
        public CompoundIndex<Trade> Trades
        {
            get { return _trades; }
            private set { _trades = value; }
        }


        private FieldIndex<string, Signal> _signals;
        public FieldIndex<string, Signal> Signals
        {
            get { return _signals; }
            private set { _signals = value; }
        }


        public Root(Storage db)
        {
            Stats = new Stats();
            Years = db.CreateMap<int, YearStats>();
            Months = db.CreateMap<int, MonthStats>();
            Weeks = db.CreateMap<int, WeekStats>();
            Days = db.CreateMap<int, DayStats>();

            Trades = db.CreateIndex<Trade>(new Type[] { typeof(int)/*Year*/, typeof(int)/*Month*/, typeof(int)/*Day*/ }, false);
            Signals = db.CreateFieldIndex<string, Signal>("_name", true);
        }

    }
}
