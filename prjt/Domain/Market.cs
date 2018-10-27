using Caliburn.Micro;
using Perst;
using prjt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class Market : PropertyChangedBase
    {
        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set { Set(ref _symbol, value); }
        }


        public Market() { }

        public Market(Storage db, string symbol)
        {
            Symbol = symbol;
        }
    }
}
