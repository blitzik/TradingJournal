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


        private CompoundIndex<Stats> _periodStats;
        public CompoundIndex<Stats> PeriodStats
        {
            get { return _periodStats; }
            private set { _periodStats = value; }
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


        private FieldIndex<string, Market> _markets;
        public FieldIndex<string, Market> Markets
        {
            get { return _markets; }
            private set { _markets = value; }
        }


        public Root(Storage db)
        {
            PeriodStats = db.CreateIndex<Stats>(new Type[] { typeof(StatsPeriod), typeof(int), typeof(int), typeof(int), typeof(int) }, true);

            Trades = db.CreateIndex<Trade>(new Type[] { typeof(int)/*Year*/, typeof(int)/*Month*/, typeof(int)/*Day*/ }, false);
            Signals = db.CreateFieldIndex<string, Signal>("_name", true);
            Markets = db.CreateFieldIndex<string, Market>("_symbol", true);
        }

    }
}
