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


        public Root(Storage db)
        {
            
        }

    }
}
