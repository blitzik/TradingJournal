using Caliburn.Micro;
using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class Signal : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }


        private Stats _stats;
        public Stats Stats
        {
            get { return _stats; }
            private set { Set(ref _stats, value); }
        }


        private IPersistentMap<int, YearStats> _periodStats;
        public IPersistentMap<int, YearStats> PeriodStats
        {
            get { return _periodStats; }
            private set { Set(ref _periodStats, value); }
        }


        public Signal(Storage db, string name)
        {
            _periodStats = db.CreateMap<int, YearStats>();
            Stats = new Stats();

            Name = name;
        }
    }
}
