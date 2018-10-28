using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class Account : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }


        private bool _isDefault;
        public bool IsDefault
        {
            get { return _isDefault; }
            set { Set(ref _isDefault, value); }
        }


        private double _startingBalance;
        public double StartingBalance
        {
            get { return _startingBalance; }
            set { Set(ref _startingBalance, value); }
        }


        private double _currentBalance;
        public double CurrentBalance
        {
            get { return _currentBalance; }
            private set { Set(ref _currentBalance, value); }
        }


        private Account() { }

        public Account(string name, double balance, bool isDefault = false)
        {
            Name = name;
            StartingBalance = balance;
            CurrentBalance = balance;
            IsDefault = isDefault;
        }
    }
}
