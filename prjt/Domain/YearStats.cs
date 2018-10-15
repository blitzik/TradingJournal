using Caliburn.Micro;
using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class YearStats : PropertyChangedBase
    {
        private int _number;
        public int Number
        {
            get { return _number; }
            private set { Set(ref _number, value); }
        }


        private Stats _stats;
        public Stats Stats
        {
            get { return _stats; }
            private set { Set(ref _stats, value); }
        }


        private IPersistentMap<int, MonthStats> _months;
        public IPersistentMap<int, MonthStats> Months
        {
            get { return _months; }
            private set { Set(ref _months, value); }
        }

    }
}
