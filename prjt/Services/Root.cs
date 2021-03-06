﻿using Perst;
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
        private CompoundIndex<Stats> _tradeStats;
        public CompoundIndex<Stats> TradeStats
        {
            get { return _tradeStats; }
            private set { _tradeStats = value; }
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


        private CompoundIndex<SignalStats> _signalStats;
        public CompoundIndex<SignalStats> SignalStats
        {
            get { return _signalStats; }
            private set { _signalStats = value; }
        }


        private FieldIndex<string, Market> _markets;
        public FieldIndex<string, Market> Markets
        {
            get { return _markets; }
            private set { _markets = value; }
        }


        private CompoundIndex<MarketStats> _marketStats;
        public CompoundIndex<MarketStats> MarketStats
        {
            get { return _marketStats; }
            private set { _marketStats = value; }
        }


        public Root(Storage db)
        {
            TradeStats = db.CreateIndex<Stats>(new Type[] { typeof(StatsPeriod), typeof(int), typeof(int), typeof(int), typeof(int) }, true);

            Trades = db.CreateIndex<Trade>(new Type[] { typeof(int)/*Year*/, typeof(int)/*Month*/, typeof(int)/*Day*/ }, false);

            Signals = db.CreateFieldIndex<string, Signal>("_name", true);
            SignalStats = db.CreateIndex<SignalStats>(new Type[] { typeof(string), typeof(StatsPeriod), typeof(int), typeof(int), typeof(int), typeof(int) }, true);

            Markets = db.CreateFieldIndex<string, Market>("_symbol", true);
            MarketStats = db.CreateIndex<MarketStats>(new Type[] { typeof(string), typeof(StatsPeriod), typeof(int), typeof(int), typeof(int), typeof(int) }, true);
        }

    }
}
