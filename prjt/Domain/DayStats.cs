using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class DayStats : PropertyChangedBase
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
    }
}
