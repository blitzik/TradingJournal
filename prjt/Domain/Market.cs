using Caliburn.Micro;
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
            private set { Set(ref _symbol, value); }
        }


        public Market(string symbol)
        {
            Symbol = symbol;
        }
    }
}
